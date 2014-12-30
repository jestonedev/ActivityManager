using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ReportModule;
using IOModule;
using ExtendedTypes;

namespace UnitTest
{
    [TestClass]
    public class ReportModuleUnitTests
    {
        /// <summary>
        /// Установка корректного значения пути до файла шаблона
        /// </summary>
        [TestMethod]
        public void ReportSetTemplateFileValid()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.odt");
            ReportPlug report = new ReportPlug();
            report.ReportSetTemplateFile(reportFile);
        }

        /// <summary>
        /// Установка некорректного значения пути до файла шаблона
        /// </summary>
        [TestMethod]
        public void ReportSetTemplateFileInvalid()
        {
            string reportFile = "c:\\aasdfa.xml";
            ReportPlug report = new ReportPlug();
            try
            {
                report.ReportSetTemplateFile(reportFile);
                Assert.Fail();
            } catch(ReportException)
            {
            }
        }

        /// <summary>
        /// Установка null-значения пути до файла шаблона
        /// </summary>
        [TestMethod]
        public void ReportSetTemplateFileNullValue()
        {
            string reportFile = null;
            ReportPlug report = new ReportPlug();
            try
            {
                report.ReportSetTemplateFile(reportFile);
                Assert.Fail();
            }
            catch (ReportException)
            {
            }
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetStringValue в файл odt
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueOdtTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.odt");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportSetStringValue("test7", "Test7");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetStringValue в файл ott
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueOttTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.ott");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportSetStringValue("test7", "Test7");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка null-строк и пустых строк ReportModuleSetStringValue в файл odt
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueOdtTest2()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.odt");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", null);
            report.ReportSetStringValue("test2", "");
            report.ReportSetStringValue("test3", null);
            report.ReportSetStringValue("test4", "");
            report.ReportSetStringValue("test5", null);
            report.ReportSetStringValue("test6", "");
            report.ReportSetStringValue("test7", null);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetStringValue в файл dotx
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueDotxTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.dotx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportSetStringValue("test7", "Test7");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetStringValue в файл docm
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueDocmTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.docm");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportSetStringValue("test7", "Test7");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetStringValue в файл dotm
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueDotmTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.dotm");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportSetStringValue("test7", "Test7");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetStringValue в файл docx
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueDocxTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.docx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportSetStringValue("test7", "Test7");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка null-строк и пустых строк ReportModuleSetStringValue в файл docx
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueDocxTest2()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.docx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", null);
            report.ReportSetStringValue("test2", "");
            report.ReportSetStringValue("test3", null);
            report.ReportSetStringValue("test4", "");
            report.ReportSetStringValue("test5", null);
            report.ReportSetStringValue("test6", "");
            report.ReportSetStringValue("test7", null);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetStringValue в файл ots
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueOtsTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.ots");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetStringValue в файл ods
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueOdsTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.ods");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка null-строк и пустых строк ReportModuleSetStringValue в файл ods
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueOdsTest2()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.ods");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", null);
            report.ReportSetStringValue("test2", "");
            report.ReportSetStringValue("test3", null);
            report.ReportSetStringValue("test4", "");
            report.ReportSetStringValue("test5", null);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetStringValue в файл xltx
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueXltxTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.xltx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetStringValue в файл xlsm
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueXlsmTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.xlsm");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetStringValue в файл xltm
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueXltmTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.xltm");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetStringValue в файл xlsx
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueXlsxTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.xlsx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка null-строк и пустых строк ReportModuleSetStringValue в файл xlsx
        /// </summary>
        [TestMethod]
        public void ReportModuleSetStringValueXlsxTest2()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetStringValueTests.xlsx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", null);
            report.ReportSetStringValue("test2", "");
            report.ReportSetStringValue("test3", null);
            report.ReportSetStringValue("test4", "");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл ott, xmlContractor = Row
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueOttTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.ott");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Row);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл odt, xmlContractor = Row
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueOdtTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.odt");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10-i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Row);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл odt, xmlContractor = Table
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueOdtTest2()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.odt");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Table);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл odt, xmlContractor = Paragraph
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueOdtTest3()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.odt");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Paragraph);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл odt, xmlContractor = Cell
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueOdtTest4()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.odt");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Cell);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка null в качестве объекта table
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueOdtTest5()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.odt");
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = null;
            report.ReportSetTemplateFile(reportFile);
            try
            {
                report.ReportSetTableValue(table, XmlContractor.Cell);
                Assert.Fail();
            } catch (ReportException)
            {
            }
        }

        /// <summary>
        /// Установка null в качестве объекта ReportRow
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueOdtTest6()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.odt");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                if (i == 3)
                {
                    ReportRow row = null;
                    table.Add(row);
                }
                else
                {
                    ReportRow row = new ReportRow(table);
                    row.Add(new ReportCell(row, rand.Next().ToString()));
                    row.Add(new ReportCell(row, i.ToString()));
                    row.Add(new ReportCell(row, (10 - i).ToString()));
                    row.Add(new ReportCell(row, DateTime.Now.ToString()));
                    table.Add(row);
                }
            }
            report.ReportSetTemplateFile(reportFile);
            try
            {
                report.ReportSetTableValue(table, XmlContractor.Row);
                report.ReportGenerate(out resultFileName);
                Assert.Fail();
            }
            catch (ReportException)
            {
            }
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл ots, xmlContractor = Row
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueOtsTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.ots");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Row);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл ods, xmlContractor = Row
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueOdsTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.ods");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Row);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл ods, xmlContractor = Table
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueOdsTest2()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.ods");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Table);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл ods, xmlContractor = Paragraph
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueOdsTest3()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.ods");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Paragraph);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл ods, xmlContractor = Cell
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueOdsTest4()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueCellTests.ods");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Cell);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл docx, xmlContractor = Row
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueDocxTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.docx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Row);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл dotx, xmlContractor = Row
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueDotxTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.dotx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Row);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл dotm, xmlContractor = Row
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueDotmTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.dotm");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Row);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл docm, xmlContractor = Row
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueDocmTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.docm");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Row);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл docx, xmlContractor = Table
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueDocxTest2()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.docx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Table);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл docx, xmlContractor = Paragraph
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueDocxTest3()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.docx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Paragraph);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл docx, xmlContractor = Cell
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueDocxTest4()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.docx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Cell);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл xltx, xmlContractor = Row
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueXltxTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.xltx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Row);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл xlsm, xmlContractor = Row
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueXlsmTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.xlsm");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Row);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл xltm, xmlContractor = Row
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueXltmTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.xltm");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Row);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл xlsx, xmlContractor = Row
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueXlsxTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.xlsx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Row);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл xlsx, xmlContractor = Table
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueXlsxTest2()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTableClouser.xlsx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Table);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл xlsx, xmlContractor = Paragraph
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueXlsxTest3()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueTests.xlsx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            table.Columns.Add("surname");
            table.Columns.Add("patronymic");
            table.Columns.Add("date");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                row.Add(new ReportCell(row, DateTime.Now.ToString()));
                table.Add(row);
            }
            try
            {
                report.ReportSetTableValue(table, XmlContractor.Paragraph);
                report.ReportGenerate(out resultFileName);
                Assert.Fail();
            }
            catch (ReportException)
            {
            }
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл xltx, xmlContractor = Cell
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueXltxTest2()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueCellTests.xltx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Cell);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл xlsm, xmlContractor = Cell
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueXlsmTest2()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueCellTests.xlsm");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Cell);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл xltm, xmlContractor = Cell
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueXltmTest2()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueCellTests.xltm");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Cell);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Установка корректных значений ReportModuleSetTableValue в файл xlsx, xmlContractor = Cell
        /// </summary>
        [TestMethod]
        public void ReportModuleSetTableValueXlsxTest4()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSetTableValueCellTests.xlsx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("name");
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, rand.Next().ToString()));
                table.Add(row);
            }
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetTableValue(table, XmlContractor.Cell);
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Тест проверки правильности работы спец. тэгов в файлах odt
        /// </summary>
        [TestMethod]
        public void ReportModuleSpecTagsOdtTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSpecTagsTests.odt");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Тест проверки правильности работы спец. тэгов в файлах ott
        /// </summary>
        [TestMethod]
        public void ReportModuleSpecTagsOttTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSpecTagsTests.ott");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Тест проверки правильности работы спец. тэгов в файлах ots
        /// </summary>
        [TestMethod]
        public void ReportModuleSpecTagsOtsTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSpecTagsTests.ots");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Тест проверки правильности работы спец. тэгов в файлах ods
        /// </summary>
        [TestMethod]
        public void ReportModuleSpecTagsOdsTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSpecTagsTests.ods");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Тест проверки правильности работы спец. тэгов в файлах docx
        /// </summary>
        [TestMethod]
        public void ReportModuleSpecTagsDocxTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSpecTagsTests.docx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test f1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Тест проверки правильности работы спец. тэгов в файлах dotx
        /// </summary>
        [TestMethod]
        public void ReportModuleSpecTagsDotxTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSpecTagsTests.dotx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test f1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Тест проверки правильности работы спец. тэгов в файлах dotm
        /// </summary>
        [TestMethod]
        public void ReportModuleSpecTagsDotmTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSpecTagsTests.dotm");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test f1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Тест проверки правильности работы спец. тэгов в файлах docm
        /// </summary>
        [TestMethod]
        public void ReportModuleSpecTagsDocmTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSpecTagsTests.docm");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test f1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Тест проверки правильности работы спец. тэгов в файлах xltx
        /// </summary>
        [TestMethod]
        public void ReportModuleSpecTagsXltxTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSpecTagsTests.xltx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test f1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Тест проверки правильности работы спец. тэгов в файлах xlsm
        /// </summary>
        [TestMethod]
        public void ReportModuleSpecTagsXlsmTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSpecTagsTests.xlsm");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test f1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Тест проверки правильности работы спец. тэгов в файлах xltm
        /// </summary>
        [TestMethod]
        public void ReportModuleSpecTagsXltmTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSpecTagsTests.xltm");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test f1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }

        /// <summary>
        /// Тест проверки правильности работы спец. тэгов в файлах xlsx
        /// </summary>
        [TestMethod]
        public void ReportModuleSpecTagsXlsxTest1()
        {
            string reportFile = Path.Combine(Directory.GetCurrentDirectory(), "templates", "ReportModuleSpecTagsTests.xlsx");
            string resultFileName = "";
            ReportPlug report = new ReportPlug();
            IOPlug io = new IOPlug();
            report.ReportSetTemplateFile(reportFile);
            report.ReportSetStringValue("test1", "Test f1");
            report.ReportSetStringValue("test2", "Test2");
            report.ReportSetStringValue("test3", "Test3");
            report.ReportSetStringValue("test4", "Test4");
            report.ReportSetStringValue("test5", "Test5");
            report.ReportSetStringValue("test6", "Test6");
            report.ReportGenerate(out resultFileName);
            io.IOOpenFile(resultFileName, null);
        }
    }
}
