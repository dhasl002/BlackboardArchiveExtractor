using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ArchiveExtractorBusinessCode.Resources;

namespace ArchiveExtractorBusinessCode
{
    public class Output
    {
        public string TargetDir { get; set; }
        
        public Output(string targetDir)
        {
            TargetDir = targetDir;
        }

        public bool CreateDir()
        {
            try
            {
                //Don't want to flood existing directory, or do we?
                if (Directory.Exists(TargetDir))
                {
                    return false;
                }
                DirectoryInfo dirInfo = Directory.CreateDirectory(TargetDir);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }
        public bool CreateDir(string targetDir)
        {
            TargetDir = targetDir;
            try
            {
                CreateDir();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }

        public static bool CreateRootIndex(List<CourseContent> content, string targetDirectory)
        {
            Directory.CreateDirectory(targetDirectory);
            //var for elements in the table
            string pageHtml = "";

            foreach (CourseContent element in content)
            {

                if (CreateResourceHtml(element.Children, element.Resources,
                    targetDirectory + @"/" + element.RefId + @".html"))
                {
                    pageHtml += "<a href='" + element.RefId + @".html'>" + element.Name + "</a><br />";
                }
                else
                {
                    pageHtml += "<div>" + element.Name + "</div>";
                }
            }

            //Grab index template
            string indexString = "";
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "CS411Crystal.ArchiveExtractorBusinessCode.StaticFiles.index.html";

            
            File.AppendAllText(targetDirectory + @"/index.html", pageHtml);
            return true;
        }

        public static bool CreateResourceHtml(List<CourseContent> content, List<BlackBoardResource> resources, string targetPath)
        {
            string pageHtml = "<html>";

            if (content.Count <= 0 && resources.All(f => string.IsNullOrEmpty(f.Text)))
            {
                if (resources.All(f => string.IsNullOrEmpty(((TextResource)f).Url)))
                {
                    if (resources.All(f => string.IsNullOrEmpty(((TextResource)f).FileName)))
                    {
                        return false;
                    }
                }
            }

            foreach (CourseContent pageContent in content)
            {
                if (CreateResourceHtml(pageContent.Children, pageContent.Resources,
                    Path.GetDirectoryName(targetPath) + @"/" + pageContent.RefId + @".html"))
                {
                    pageHtml += "<a href='" + pageContent.RefId + @".html'>" + pageContent.Name + "</a><br />";
                }
                else
                {
                    pageHtml += "<div>" + pageContent.Name + "</div>";
                }
            }

            foreach (BlackBoardResource res in resources)
            {
                if (!string.IsNullOrEmpty(res.Text))
                {
                    pageHtml += res.Text;
                    pageHtml += "<hr>";
                }

                var resource = res as TextResource;
                if (resource != null)
                {
                    if (!string.IsNullOrEmpty(resource.Url))
                    {
                        pageHtml += "<a href='" + resource.Url + "'> Link </a>";
                    }
                    if (!string.IsNullOrEmpty(resource.FileName))
                    {
                        string directoryPath = Path.GetDirectoryName(targetPath);
                        List<string> files = Directory.GetFiles(directoryPath + @"/Archive", "*" + resource.FileName.Replace("/","") + "*", SearchOption.AllDirectories).ToList();
                        if (files.Any())
                        {
                            string filename = Path.GetFileName(files[0]);
                            if (!File.Exists(directoryPath + "/" + filename))
                            {
                                File.Copy(files[0], directoryPath + "/" + filename);
                                pageHtml += "<a href='" + filename + "'> Link </a>";
                            }
                        }
                    }
                }
            }
            

            pageHtml += "</html>";
            if (!File.Exists(targetPath))
            {
                File.AppendAllText(targetPath, pageHtml);
            }
            return true;
        }
    }
}
