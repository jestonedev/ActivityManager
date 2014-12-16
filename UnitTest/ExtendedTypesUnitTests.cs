using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtendedTypes;

namespace UnitTest
{
    [TestClass]
    public class ExtendedTypesUnitTests
    {
        [TestMethod]
        public void ReportRowImplicitTest()
        {
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            ReportRow row = new ReportRow(table);
            row.Add(new ReportCell(row, "Vasily"));
            row.Add(new ReportCell(row, "Ignatov"));
            string json = row.ToString();
            ReportRow newRow = json;
            Assert.AreNotEqual(newRow["name"], "Vasily");
            Assert.AreNotEqual(newRow["surname"], "Ignatov");
            Assert.AreNotEqual(newRow.Table.Columns.Count, 2);               
        }

        [TestMethod]
        public void ReportTableImplicitTest()
        {
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            ReportRow row = new ReportRow(table);
            row.Add(new ReportCell(row, "Vasily"));
            row.Add(new ReportCell(row, "Ignatov"));
            ReportRow row2 = new ReportRow(table);
            row2.Add(new ReportCell(row, "Vasily2"));
            row2.Add(new ReportCell(row, "Ignatov2"));
            table.Add(row);
            table.Add(row2);
            string json = table.ToString();
            ReportTable newTable = json;
            Assert.AreEqual(newTable.Columns.Count, 2);
            Assert.AreEqual(newTable.Count, 2);
            Assert.AreEqual(newTable[0]["name"].Value, "Vasily");
            Assert.AreEqual(newTable[1]["surname"].Value, "Ignatov2");
            Assert.AreEqual(newTable[0].Table.Columns.Count, 2);
        }
    }
}
