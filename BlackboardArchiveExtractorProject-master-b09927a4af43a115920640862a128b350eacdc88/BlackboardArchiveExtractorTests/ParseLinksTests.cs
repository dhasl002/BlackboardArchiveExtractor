using ArchiveExtractorBusinessCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CS411Crystal.Tests
{
    [TestClass]
    public class ParseLinksTests
    {
        /// <summary>
        /// Constructor Test
        /// </summary>
        [TestMethod]
        public void ParseLinksTest()
        {
            ParseLinks parser = new ParseLinks("http://www.cs.odu.edu/~gatkins");
            Assert.AreEqual(parser.Uri, "http://www.cs.odu.edu/~gatkins");
            Assert.AreEqual(parser.FinalUri, "");
        }

        /// <summary>
        /// Request Tests
        /// </summary>
        [TestMethod]
        public void RequestUrlTest()
        {
            // C:\Users\Grant\Downloads\imsmanifest.xml
            // init and check valid Uri
            // alive page - 200
            ParseLinks parser = new ParseLinks("http://www.cs.odu.edu/~gatkins");
            bool isAlive = parser.RequestUrl();
            Assert.AreEqual("http://www.cs.odu.edu/~gatkins/",parser.FinalUri);
            Assert.AreEqual(true, isAlive);
            // check 404 page - 404
            parser = new ParseLinks("http://www.cs.odu.edu/~gatkins/blahblah404definitely");
            isAlive = parser.RequestUrl();
            Assert.AreEqual("", parser.FinalUri);
            Assert.AreEqual(false, isAlive);
            // check redirect to an alive page - 200
            parser = new ParseLinks("http://www.cs.odu.edu/~gatkins/cs532/redirect.php");
            isAlive = parser.RequestUrl();
            Assert.AreEqual("http://www.cs.odu.edu/~mln/teaching/cs532-s17/test/pdfs.html", parser.FinalUri);
            Assert.AreEqual(true, isAlive);
            // check infinite redirect error - 404
            parser = new ParseLinks("http://www.cs.odu.edu/~gatkins/cs532/redirect2.php");
            isAlive = parser.RequestUrl();
            Assert.AreEqual("", parser.FinalUri);
            Assert.AreEqual(false, isAlive);
        }

        /// <summary>
        /// Test isAbsoluteUri Function. Check relative path, absolute path, and no path reference
        /// Only absolute path should return true.
        /// </summary>
        [TestMethod]
        public void AbsolutePathTest()
        {
            ParseLinks parser = new ParseLinks("./test.html");
            bool relativePath = parser.IsAbsoluteUri();
            Assert.AreEqual(relativePath,false);
            
            parser.Uri = "http://www.cs.odu.edu";
            bool absolutePath = parser.IsAbsoluteUri();
            Assert.AreEqual(absolutePath, true);
            
            parser.Uri = "";
            bool errorTest = parser.IsAbsoluteUri();
            Assert.AreEqual(errorTest, false);
        }
    }
}
