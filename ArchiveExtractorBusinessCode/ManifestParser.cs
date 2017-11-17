using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ArchiveExtractorBusinessCode
{
    public class ManifestParser
    {
        public static List<XElement> GetOrganizationElements(XElement manifestXml)
        {
            return manifestXml.Element("organizations").Element("organization").Elements("item").ToList();
        }

        public static List<XElement> GetResourceElements(XElement manifestXml)
        {
            
            return manifestXml.Element("resources").Elements("resource").ToList();
        }
    }
}