using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ArchiveExtractorBusinessCode
{
    public class AnnouncementResource : BlackBoardResource
    {
        public AnnouncementResource(string text, string refId)
        {
            RefId = refId;
            Text = text;
        }

        public AnnouncementResource(string pathToResourceFile)
        {
            var xml = File.ReadAllText(pathToResourceFile);
            var xele = XElement.Parse(xml);
            if (xele.Descendants("TEXT").Any())
            {
                List<XElement> texts = xele.Descendants("TEXT").ToList();
                Text = texts[0].Value;
            }
            else
            {
                Text = "";
            }
            RefId = Path.GetFileNameWithoutExtension(PathToResourceFile);
            PathToResourceFile = pathToResourceFile;
        }

        public string RefId { get; set; }
        public override string Text { get; set; }
        public string PathToResourceFile { get; set; }
        public string Url { get; set; }
    }
}