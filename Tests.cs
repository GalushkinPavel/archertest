using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ArcherTestTask
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase(new object[] { "" })]
        [TestCase(new object[] { ">>" })]
        [TestCase(new object[] { "e", "s" })]
        [TestCase(new object[] { "e", "s", "dsf" })]
        public void checkParam(object[] args)
        {
            String[] g = new String[args.Length];
            for (int i = 0; i < args.Length; i++)
                g[i] = (String)args[i];
            Assert.Throws<ArgumentException>(() => Utils.checkParam(g));
        }

        [Test]
        public void testContextException()
        {
            Context context = new Context();
            Assert.Throws<ArgumentNullException>(() => context.Strategy = null);
        }

        [Test]
        [TestCase(@"f\bla\ra\t.dat")]
        public void testAllHandler(String fullName)
        {
            AllHandler ah = new AllHandler();
            String res = ah.fileHandle(fullName);
            Assert.AreEqual(res, fullName);
        }

        [Test]
        [TestCase(@"f\bla\ra\t.dat", ExpectedResult = "")]
        [TestCase(@"f\bla\ra\t.cpp", ExpectedResult = @"f\bla\ra\t.cpp /")]
        public String testCppHandler(String fullName)
        {
            CppHandler ch = new CppHandler();
            return ch.fileHandle(fullName);
        }

        [Test]
        [TestCase("", ExpectedResult = "")]
        [TestCase(@"f\bla\ra\t.dat", ExpectedResult = @"t.dat\ra\bla\f")]
        public String testReverseOneHandler(String fullName)
        {
            ReverseOneHandler rh = new ReverseOneHandler();
            return rh.fileHandle(fullName);
        }

        [Test]
        [TestCase("", ExpectedResult = "")]
        [TestCase(@"f\bla\ra\t.dat", ExpectedResult = @"tad.t\ar\alb\f")]
        public String testReverseTwoHandler(String fullName)
        {
            ReverseTwoHandler rh = new ReverseTwoHandler();
            return rh.fileHandle(fullName);
        }

        [Test]
        public void testCrawlerConstrException()
        {
            Assert.Throws<ArgumentNullException>(() => new DirCrawler(null));
        }

        [Test]
        public void testCrawlerSetterException()
        {
            DirCrawler dc = new DirCrawler(new Context());
            Assert.Throws<ArgumentNullException>(() => dc.context = null);
        }

        [Test]
        [TestCase(@"c:\f\bla\ra\t.dat", @"c:\f\bla", ExpectedResult = @"ra\t.dat")]
        public string testGetRelativePath(string filespec, string folder)
        {
            return new Context().GetRelativePath(filespec, folder);
        }

        [Test]
        [TestCase("", ExpectedResult = "")]
        [TestCase("  ", ExpectedResult = "  ")]
        [TestCase(@"c:\f\bla\ra\t.dat", ExpectedResult = @"ra\t.dat")]
        public string testExecuteOperation(String s) {
            Utils.startFolder = @"c:\f\bla";
            Context c = new Context();
            c.Strategy = new AllHandler();
            return c.ExecuteOperation(s);
        }
    }
}
