using System.IO;
using ArchiveExtractorBusinessCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CS411Crystal.Tests
{
    [TestClass]
    public class OutputTests
    {
        [TestMethod]
        public void OutputTest()
        {
            Output output = new Output(Directory.GetCurrentDirectory() + "\testDir");
            Assert.AreEqual(output.TargetDir, Directory.GetCurrentDirectory() + "\testDir");
        }

        [TestMethod]
        public void CreateDirTest()
        {
            //Initialize and test creating directory
            Output output = new Output(Directory.GetCurrentDirectory() + "\testDir");
            Assert.AreEqual(output.CreateDir(), true);
            //Delete directory
            Directory.Delete(Directory.GetCurrentDirectory() + "\testDir");
            //Check nonexistent directory location returns false
            Assert.AreEqual(output.CreateDir("\\Doesntexist"), false);
            //Check creating existing directory returns false
            Assert.AreEqual(output.CreateDir(Directory.GetCurrentDirectory()), false);
        }
    }
}
