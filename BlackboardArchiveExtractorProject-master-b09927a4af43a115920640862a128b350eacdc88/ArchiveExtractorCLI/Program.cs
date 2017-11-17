using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ArchiveExtractorBusinessCode;

namespace ArchiveExtractorCLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string archiveLocation = "";
            string extractDestination = "";
            string tempLocation = "";

            try
            {
                archiveLocation = args[0];
                extractDestination = args[1];
                tempLocation = extractDestination + @"/Archive";
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Usage: ArchiveExtractor 'ArchiveToExtract' 'TargetLocation'");
                return;
            }
            if (Directory.Exists(tempLocation))
            {
                Directory.Delete(tempLocation, true);
            }
            
            Console.WriteLine("Extracting Zip...");


            if (!ExtractArchive(archiveLocation, ref tempLocation, ref extractDestination))
            {
                Console.WriteLine("Usage: ArchiveExtractor 'ArchiveToExtract' 'TargetLocation'");
                Console.WriteLine("Error BAE001: File Not found");
                return; 
            }

            string xml = File.ReadAllText(tempLocation + "/imsmanifest.xml");
            XElement manifest = XElement.Parse(xml);

            
            List<XElement> xele = ManifestParser.GetOrganizationElements(manifest);
            List<XElement> xres = ManifestParser.GetResourceElements(manifest);

            List<CourseContent> course = new List<CourseContent>();
            foreach (XElement x in xele)
            {
                //Console.WriteLine(x);
                CourseContent cc = new CourseContent(x, tempLocation);

                if (cc.Name == "Announcements")
                {
                    foreach (XElement res in  xres.Where(f => f.Attribute("type").Value == "resource/x-bb-announcement"))
                    {
                        string path = tempLocation + "/" + res.Attribute("identifier").Value + ".dat";
                        cc.Resources.Add(new AnnouncementResource(path));
                    }
                }

                course.Add(cc);
            }
            Output.CreateRootIndex(course, extractDestination);
            Directory.Delete(tempLocation, true);
            Console.ReadKey();
        }

        /// <summary>
        /// Extracts the archive and returns true if successful
        /// </summary>
        /// <param name="archiveLocation"></param>
        /// <param name="tempLocation"></param>
        /// <param name="extractDestination"></param>
        /// <returns></returns>
        private static bool ExtractArchive(string archiveLocation, ref string tempLocation, ref string extractDestination)
        {
            try
            {
                int count = 0;
                string fileNameOnly = Path.GetFileNameWithoutExtension(tempLocation);
                string extension = Path.GetExtension(tempLocation);
                string path = Path.GetDirectoryName(tempLocation);
                string newFullPath = tempLocation;
                string originalDesination = extractDestination;
                while (Directory.Exists(extractDestination))
                {
                    Console.WriteLine(count);
                    string tempFileName = originalDesination + "(" + (++count) + ")";
                    extractDestination = tempFileName;
                    tempLocation = extractDestination + @"/Archive";
                }
                Archive.ExtractArchive(archiveLocation, tempLocation);
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            return true;
        }
    }
}
