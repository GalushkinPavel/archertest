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
            Context context = new Context();
            context.Strategy = new AllHandler();
            String res = context.ExecuteOperation(fullName);
            Assert.AreEqual(res, fullName);
        }

        [Test]
        [TestCase(@"f\bla\ra\t.dat", ExpectedResult = "")]
        [TestCase(@"f\bla\ra\t.cpp", ExpectedResult = @"f\bla\ra\t.cpp /")]
        public String testCppHandler(String fullName)
        {
            Context context = new Context();
            context.Strategy = new CppHandler();
            return context.ExecuteOperation(fullName);
        }

        [Test]
        [TestCase("", ExpectedResult = "")]
        [TestCase(@"f\bla\ra\t.dat", ExpectedResult = @"t.dat\ra\bla\f")]
        public String testReverseOneHandler(String fullName)
        {
            Context context = new Context();
            context.Strategy = new ReverseOneHandler();
            return context.ExecuteOperation(fullName);
        }

        [Test]
        [TestCase("", ExpectedResult = "")]
        [TestCase(@"f\bla\ra\t.dat", ExpectedResult = @"tad.t\ar\alb\f")]
        public String testReverseTwoHandler(String fullName)
        {
            Context context = new Context();
            context.Strategy = new ReverseTwoHandler();
            return context.ExecuteOperation(fullName);
        }

        [Test]
        public void testCrawlerException()
        {
            Assert.Throws<ArgumentNullException>(() => new DirCrawler(null));
        }
    }
}
