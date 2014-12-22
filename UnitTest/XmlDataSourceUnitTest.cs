using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XmlDataSource;
using ExtendedTypes;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class XmlDataSourceUnitTest
    {
        [TestMethod]
        public void XmlDataSourceTest1()
        {
            XmlDataSourcePlug xmlplug = new XmlDataSourcePlug();
            ReportTable table = new ReportTable();
            xmlplug.XmlSelectTable("<xml><row column1=\"val1\" column2=\"val2\">val3</row><row column1=\"val11\" column2=\"val22\">val33</row></xml>",
                @"/xml/row", "{\"col1\":\"@column1\",\"col2\":\"@column2\",\"col3\":\"text()\"}", out table);
            Assert.AreEqual(table.Count, 2);
            Assert.AreEqual(table[0]["col1"].Value,"val1");
            Assert.AreEqual(table[1]["col2"].Value, "val22");
            Assert.AreEqual(table[1]["col3"].Value, "val33");
        }

        [TestMethod]
        public void XmlDataSourceTest2()
        {
            XmlDataSourcePlug xmlplug = new XmlDataSourcePlug();
            ReportTable table = new ReportTable();
            xmlplug.XmlSelectTable(Path.Combine(Directory.GetCurrentDirectory(), "XmlDataSourceTest2.xml"),
                @"//member", "{\"col1\":\"@name\",\"col2\":\"summary/text()\"}", out table);
            Assert.AreEqual(table.Count, 11);
            Assert.AreEqual(table[0]["col1"].Value, "T:XmlDataSource.IPlug");
            Assert.AreEqual(table[1]["col2"].Value, "\n            Выборка данных из xml-файла\n            ");
        }

        [TestMethod]
        public void XmlDataSourceTest3()
        {
            XmlDataSourcePlug xmlplug = new XmlDataSourcePlug();
            object value = null;
            xmlplug.XmlSelectScalar("<xml><row column1=\"val1\" column2=\"val2\">val3</row><row column1=\"val11\" column2=\"val22\">val33</row></xml>",
                @"//xml/row/text()", out value);
            Assert.AreEqual(value.ToString(), "val3val33");
            xmlplug.XmlSelectScalar("<xml><row column1=\"val1\" column2=\"val2\">val3</row><row column1=\"val11\" column2=\"val22\">val33</row></xml>",
                "//xml/row[@column1=\"val1\"]/text()", out value);
            Assert.AreEqual(value.ToString(), "val3");
        }

        [TestMethod]
        public void XmlDataSourceTest4()
        {
            XmlDataSourcePlug xmlplug = new XmlDataSourcePlug();
            object value = null;
            try
            {
                xmlplug.XmlSelectScalar("<xml><row column1=\"val1\" column2=\"val2\">val3</row><row column1=\"val11\" column2=\"val22\">val33</row></xml>",
                    @"//xml/row/text(", out value);
                Assert.Fail();
            } catch (XmlDataSourceException)
            {
            }
        }

        [TestMethod]
        public void XmlDataSourceTest5()
        {
            XmlDataSourcePlug xmlplug = new XmlDataSourcePlug();
            ReportTable table = new ReportTable();
            try
            {
                xmlplug.XmlSelectTable("<xml><row column1=\"val1\" column2=\"val2\">val3</row><row column1=\"val11\" column2=\"val22\">val33</row></xml>",
                    @"/xml/row@*", "{\"col1\":\"@column1\",\"col2\":\"@column2\",\"col3\":\"text()\"}", out table);
                Assert.Fail();
            }
            catch (XmlDataSourceException)
            {
            }
        }

        [TestMethod]
        public void XmlDataSourceTest6()
        {
            XmlDataSourcePlug xmlplug = new XmlDataSourcePlug();
            ReportTable table = new ReportTable();
            try
            {
                xmlplug.XmlSelectTable("<xml><row column1=\"val1\" column2=\"val2\">val3</row><row column1=\"val11\" column2=\"val22\">val33</row></xml",
                    @"/xml/row", "{\"col1\":\"@column1\",\"col2\":\"@column2\",\"col3\":\"text()\"}", out table);
                Assert.Fail();
            }
            catch (XmlDataSourceException)
            {
            }
        }

        [TestMethod]
        public void XmlDataSourceTest7()
        {
            XmlDataSourcePlug xmlplug = new XmlDataSourcePlug();
            ReportTable table = new ReportTable();
            try
            {
                xmlplug.XmlSelectTable("<xml><row column1=\"val1\" column2=\"val2\">val3</row><row column1=\"val11\" column2=\"val22\">val33</row></xml>",
                    @"/xml/row", "{\"col1\":\"@column1\",\"col2\":\"@column2\",\"col3\":\"text()\"", out table);
                Assert.Fail();
            }
            catch (XmlDataSourceException)
            {
            }
        }
    }
}
