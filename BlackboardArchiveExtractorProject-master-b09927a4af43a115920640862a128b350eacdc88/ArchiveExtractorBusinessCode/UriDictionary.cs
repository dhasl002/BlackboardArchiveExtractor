using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveExtractorBusinessCode
{
    /// <summary>
    /// Static dictionary to hold uris and file names
    /// Key: URI string
    /// Value: Array of file names
    /// </summary>
    static class UriDictionary
    {
        public static Dictionary<string,URLObj> UriDict = new Dictionary<string, URLObj>();
    }

    /// <summary>
    /// URL obj for URIDictionary
    /// </summary>
    public class URLObj
    {
        public List<string> filesFound = new List<string>();
        public bool isAlive = false;
    }

    static class linkArgs
    {
        public static bool checkLinks = false;
    }

}
