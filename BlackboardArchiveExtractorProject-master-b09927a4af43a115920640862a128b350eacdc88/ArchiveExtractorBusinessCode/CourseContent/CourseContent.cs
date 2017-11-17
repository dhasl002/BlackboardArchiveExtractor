using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ArchiveExtractorBusinessCode.Resources;

namespace ArchiveExtractorBusinessCode
{
    public class CourseContent
    {
        public string RefId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public CourseContent Parent { get; set; }
        private List<CourseContent> children = new List<CourseContent>();
        public readonly List<BlackBoardResource> Resources = new List<BlackBoardResource>();

        public List<CourseContent> Children 
        {
            get { return children; }
        }
        public CourseContent()
        {
            //Generic Constructor to make the compiler happy
            RefId = "";
            Name = "";
            Parent = null;
        }

        public CourseContent(CourseContent parent, string refId, string name)
        {
            Parent = parent;
            RefId = refId;
            Name = name;
        }

        public CourseContent(string refId, string name)
        {
            RefId = refId;
            Name = name;
        }

        public CourseContent(XElement XmlItemManifestElement, string tempLocation)
        {
            if (XmlItemManifestElement.Descendants("title").ToList().Any())
            {
                Name = XmlItemManifestElement.Descendants("title").ToList()[0].Value;
            }
            Id = XmlItemManifestElement.Attributes().ToList()[0].Value;
            RefId = XmlItemManifestElement.Attributes().ToList()[1].Value;

            //Recursively create child elements here
            if (XmlItemManifestElement.Descendants("item").ToList().Any())
            {
                foreach (XElement xmlChildElement in XmlItemManifestElement.Descendants("item").ToList())
                {
                    children.Add(new CourseContent(xmlChildElement, tempLocation));
                }
            }

            if (File.Exists(tempLocation + @"/" + RefId + ".dat"))
            {
                Resources.Add(new TextResource(tempLocation + @"/" + RefId + ".dat"));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
