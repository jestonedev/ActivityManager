using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConvertModule;
using ExtendedTypes;
using System.Globalization;

namespace UnitTest
{
    [TestClass]
    public class ConvertModuleUnitTests
    {
        [TestMethod]
        public void ConvertModuleConvertIntTest1()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i+=10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Genitive, Sex.Male, false, false, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "ноля");
            Assert.AreEqual(table[1]["id"].Value, "одного");
            Assert.AreEqual(table[2]["id"].Value, "двух");
            Assert.AreEqual(table[3]["id"].Value, "трех");
            Assert.AreEqual(table[4]["id"].Value, "четырех");
            Assert.AreEqual(table[5]["id"].Value, "пяти");
            Assert.AreEqual(table[6]["id"].Value, "шести");
            Assert.AreEqual(table[7]["id"].Value, "семи");
            Assert.AreEqual(table[8]["id"].Value, "восьми");
            Assert.AreEqual(table[9]["id"].Value, "девяти");
            Assert.AreEqual(table[10]["id"].Value, "десяти");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцати");
            Assert.AreEqual(table[12]["id"].Value, "двенадцати");
            Assert.AreEqual(table[13]["id"].Value, "тринадцати");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцати");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцати");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцати");
            Assert.AreEqual(table[17]["id"].Value, "семнадцати");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцати");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцати");
            Assert.AreEqual(table[20]["id"].Value, "двадцати");
            Assert.AreEqual(table[21]["id"].Value, "тридцати");
            Assert.AreEqual(table[22]["id"].Value, "сорока");
            Assert.AreEqual(table[23]["id"].Value, "пятидесяти");
            Assert.AreEqual(table[24]["id"].Value, "шестидесяти");
            Assert.AreEqual(table[25]["id"].Value, "семидесяти");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесяти");
            Assert.AreEqual(table[27]["id"].Value, "девяноста");
            Assert.AreEqual(table[28]["id"].Value, "ста");
            Assert.AreEqual(table[29]["id"].Value, "двухсот");
            Assert.AreEqual(table[30]["id"].Value, "трехсот");
            Assert.AreEqual(table[31]["id"].Value, "четырехсот");
            Assert.AreEqual(table[32]["id"].Value, "пятисот");
            Assert.AreEqual(table[33]["id"].Value, "шестисот");
            Assert.AreEqual(table[34]["id"].Value, "семисот");
            Assert.AreEqual(table[35]["id"].Value, "восьмисот");
            Assert.AreEqual(table[36]["id"].Value, "девятисот");
            Assert.AreEqual(table[37]["id"].Value, "одной тысячи");
            Assert.AreEqual(table[38]["id"].Value, "двух тысяч");
            Assert.AreEqual(table[39]["id"].Value, "трех тысяч");
            Assert.AreEqual(table[40]["id"].Value, "четырех тысяч");
            Assert.AreEqual(table[41]["id"].Value, "пяти тысяч");
            Assert.AreEqual(table[42]["id"].Value, "шести тысяч");
            Assert.AreEqual(table[43]["id"].Value, "семи тысяч");
            Assert.AreEqual(table[44]["id"].Value, "восьми тысяч");
            Assert.AreEqual(table[45]["id"].Value, "девяти тысяч");
            Assert.AreEqual(table[46]["id"].Value, "десяти тысяч");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцати тысяч");
            Assert.AreEqual(table[48]["id"].Value, "двенадцати тысяч");
            Assert.AreEqual(table[49]["id"].Value, "тринадцати тысяч");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцати тысяч");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцати тысяч");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцати тысяч");
            Assert.AreEqual(table[53]["id"].Value, "семнадцати тысяч");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцати тысяч");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцати тысяч");
            Assert.AreEqual(table[56]["id"].Value, "одного миллиона");
            Assert.AreEqual(table[57]["id"].Value, "двух миллионов");
            Assert.AreEqual(table[58]["id"].Value, "трех миллионов");
            Assert.AreEqual(table[59]["id"].Value, "четырех миллионов");
            Assert.AreEqual(table[60]["id"].Value, "пяти миллионов");
            Assert.AreEqual(table[61]["id"].Value, "шести миллионов");
            Assert.AreEqual(table[62]["id"].Value, "семи миллионов");
            Assert.AreEqual(table[63]["id"].Value, "восьми миллионов");
            Assert.AreEqual(table[64]["id"].Value, "девяти миллионов");
            Assert.AreEqual(table[65]["id"].Value, "десяти миллионов");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцати миллионов");
            Assert.AreEqual(table[67]["id"].Value, "двенадцати миллионов");
            Assert.AreEqual(table[68]["id"].Value, "тринадцати миллионов");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцати миллионов");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцати миллионов");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцати миллионов");
            Assert.AreEqual(table[72]["id"].Value, "семнадцати миллионов");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцати миллионов");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцати миллионов");
            Assert.AreEqual(table[75]["id"].Value, "одного миллиарда");
            Assert.AreEqual(table[76]["id"].Value, "двух миллиардов");
            Assert.AreEqual(table[77]["id"].Value, "трех миллиардов");
            Assert.AreEqual(table[78]["id"].Value, "четырех миллиардов");
            Assert.AreEqual(table[79]["id"].Value, "пяти миллиардов");
            Assert.AreEqual(table[80]["id"].Value, "шести миллиардов");
            Assert.AreEqual(table[81]["id"].Value, "семи миллиардов");
            Assert.AreEqual(table[82]["id"].Value, "восьми миллиардов");
            Assert.AreEqual(table[83]["id"].Value, "девяти миллиардов");
            Assert.AreEqual(table[84]["id"].Value, "десяти миллиардов");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцати миллиардов");
            Assert.AreEqual(table[86]["id"].Value, "двенадцати миллиардов");
            Assert.AreEqual(table[87]["id"].Value, "тринадцати миллиардов");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцати миллиардов");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцати миллиардов");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцати миллиардов");
            Assert.AreEqual(table[91]["id"].Value, "семнадцати миллиардов");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцати миллиардов");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцати миллиардов");
            Assert.AreEqual(table[94]["id"].Value, "одного триллиона");
            Assert.AreEqual(table[95]["id"].Value, "двух триллионов");
            Assert.AreEqual(table[96]["id"].Value, "трех триллионов");
            Assert.AreEqual(table[97]["id"].Value, "четырех триллионов");
            Assert.AreEqual(table[98]["id"].Value, "пяти триллионов");
            Assert.AreEqual(table[99]["id"].Value, "шести триллионов");
            Assert.AreEqual(table[100]["id"].Value, "семи триллионов");
            Assert.AreEqual(table[101]["id"].Value, "восьми триллионов");
            Assert.AreEqual(table[102]["id"].Value, "девяти триллионов");
            Assert.AreEqual(table[103]["id"].Value, "десяти триллионов");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцати триллионов");
            Assert.AreEqual(table[105]["id"].Value, "двенадцати триллионов");
            Assert.AreEqual(table[106]["id"].Value, "тринадцати триллионов");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцати триллионов");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцати триллионов");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцати триллионов");
            Assert.AreEqual(table[110]["id"].Value, "семнадцати триллионов");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцати триллионов");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцати триллионов");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest2()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Dative, Sex.Male, false, false, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нолю");
            Assert.AreEqual(table[1]["id"].Value, "одному");
            Assert.AreEqual(table[2]["id"].Value, "двум");
            Assert.AreEqual(table[3]["id"].Value, "трем");
            Assert.AreEqual(table[4]["id"].Value, "четырем");
            Assert.AreEqual(table[5]["id"].Value, "пяти");
            Assert.AreEqual(table[6]["id"].Value, "шести");
            Assert.AreEqual(table[7]["id"].Value, "семи");
            Assert.AreEqual(table[8]["id"].Value, "восьми");
            Assert.AreEqual(table[9]["id"].Value, "девяти");
            Assert.AreEqual(table[10]["id"].Value, "десяти");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцати");
            Assert.AreEqual(table[12]["id"].Value, "двенадцати");
            Assert.AreEqual(table[13]["id"].Value, "тринадцати");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцати");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцати");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцати");
            Assert.AreEqual(table[17]["id"].Value, "семнадцати");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцати");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцати");
            Assert.AreEqual(table[20]["id"].Value, "двадцати");
            Assert.AreEqual(table[21]["id"].Value, "тридцати");
            Assert.AreEqual(table[22]["id"].Value, "сорока");
            Assert.AreEqual(table[23]["id"].Value, "пятидесяти");
            Assert.AreEqual(table[24]["id"].Value, "шестидесяти");
            Assert.AreEqual(table[25]["id"].Value, "семидесяти");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесяти");
            Assert.AreEqual(table[27]["id"].Value, "девяноста");
            Assert.AreEqual(table[28]["id"].Value, "ста");
            Assert.AreEqual(table[29]["id"].Value, "двумстам");
            Assert.AreEqual(table[30]["id"].Value, "тремстам");
            Assert.AreEqual(table[31]["id"].Value, "четыремстам");
            Assert.AreEqual(table[32]["id"].Value, "пятистам");
            Assert.AreEqual(table[33]["id"].Value, "шестистам");
            Assert.AreEqual(table[34]["id"].Value, "семистам");
            Assert.AreEqual(table[35]["id"].Value, "восьмистам");
            Assert.AreEqual(table[36]["id"].Value, "девятистам");
            Assert.AreEqual(table[37]["id"].Value, "одной тысяче");
            Assert.AreEqual(table[38]["id"].Value, "двум тысячам");
            Assert.AreEqual(table[39]["id"].Value, "трем тысячам");
            Assert.AreEqual(table[40]["id"].Value, "четырем тысячам");
            Assert.AreEqual(table[41]["id"].Value, "пяти тысячам");
            Assert.AreEqual(table[42]["id"].Value, "шести тысячам");
            Assert.AreEqual(table[43]["id"].Value, "семи тысячам");
            Assert.AreEqual(table[44]["id"].Value, "восьми тысячам");
            Assert.AreEqual(table[45]["id"].Value, "девяти тысячам");
            Assert.AreEqual(table[46]["id"].Value, "десяти тысячам");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцати тысячам");
            Assert.AreEqual(table[48]["id"].Value, "двенадцати тысячам");
            Assert.AreEqual(table[49]["id"].Value, "тринадцати тысячам");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцати тысячам");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцати тысячам");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцати тысячам");
            Assert.AreEqual(table[53]["id"].Value, "семнадцати тысячам");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцати тысячам");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцати тысячам");
            Assert.AreEqual(table[56]["id"].Value, "одному миллиону");
            Assert.AreEqual(table[57]["id"].Value, "двум миллионам");
            Assert.AreEqual(table[58]["id"].Value, "трем миллионам");
            Assert.AreEqual(table[59]["id"].Value, "четырем миллионам");
            Assert.AreEqual(table[60]["id"].Value, "пяти миллионам");
            Assert.AreEqual(table[61]["id"].Value, "шести миллионам");
            Assert.AreEqual(table[62]["id"].Value, "семи миллионам");
            Assert.AreEqual(table[63]["id"].Value, "восьми миллионам");
            Assert.AreEqual(table[64]["id"].Value, "девяти миллионам");
            Assert.AreEqual(table[65]["id"].Value, "десяти миллионам");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцати миллионам");
            Assert.AreEqual(table[67]["id"].Value, "двенадцати миллионам");
            Assert.AreEqual(table[68]["id"].Value, "тринадцати миллионам");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцати миллионам");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцати миллионам");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцати миллионам");
            Assert.AreEqual(table[72]["id"].Value, "семнадцати миллионам");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцати миллионам");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцати миллионам");
            Assert.AreEqual(table[75]["id"].Value, "одному миллиарду");
            Assert.AreEqual(table[76]["id"].Value, "двум миллиардам");
            Assert.AreEqual(table[77]["id"].Value, "трем миллиардам");
            Assert.AreEqual(table[78]["id"].Value, "четырем миллиардам");
            Assert.AreEqual(table[79]["id"].Value, "пяти миллиардам");
            Assert.AreEqual(table[80]["id"].Value, "шести миллиардам");
            Assert.AreEqual(table[81]["id"].Value, "семи миллиардам");
            Assert.AreEqual(table[82]["id"].Value, "восьми миллиардам");
            Assert.AreEqual(table[83]["id"].Value, "девяти миллиардам");
            Assert.AreEqual(table[84]["id"].Value, "десяти миллиардам");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцати миллиардам");
            Assert.AreEqual(table[86]["id"].Value, "двенадцати миллиардам");
            Assert.AreEqual(table[87]["id"].Value, "тринадцати миллиардам");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцати миллиардам");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцати миллиардам");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцати миллиардам");
            Assert.AreEqual(table[91]["id"].Value, "семнадцати миллиардам");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцати миллиардам");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцати миллиардам");
            Assert.AreEqual(table[94]["id"].Value, "одному триллиону");
            Assert.AreEqual(table[95]["id"].Value, "двум триллионам");
            Assert.AreEqual(table[96]["id"].Value, "трем триллионам");
            Assert.AreEqual(table[97]["id"].Value, "четырем триллионам");
            Assert.AreEqual(table[98]["id"].Value, "пяти триллионам");
            Assert.AreEqual(table[99]["id"].Value, "шести триллионам");
            Assert.AreEqual(table[100]["id"].Value, "семи триллионам");
            Assert.AreEqual(table[101]["id"].Value, "восьми триллионам");
            Assert.AreEqual(table[102]["id"].Value, "девяти триллионам");
            Assert.AreEqual(table[103]["id"].Value, "десяти триллионам");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцати триллионам");
            Assert.AreEqual(table[105]["id"].Value, "двенадцати триллионам");
            Assert.AreEqual(table[106]["id"].Value, "тринадцати триллионам");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцати триллионам");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцати триллионам");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцати триллионам");
            Assert.AreEqual(table[110]["id"].Value, "семнадцати триллионам");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцати триллионам");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцати триллионам");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest3()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Accusative, Sex.Male, false, false, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "ноль");
            Assert.AreEqual(table[1]["id"].Value, "один");
            Assert.AreEqual(table[2]["id"].Value, "два");
            Assert.AreEqual(table[3]["id"].Value, "три");
            Assert.AreEqual(table[4]["id"].Value, "четыре");
            Assert.AreEqual(table[5]["id"].Value, "пять");
            Assert.AreEqual(table[6]["id"].Value, "шесть");
            Assert.AreEqual(table[7]["id"].Value, "семь");
            Assert.AreEqual(table[8]["id"].Value, "восемь");
            Assert.AreEqual(table[9]["id"].Value, "девять");
            Assert.AreEqual(table[10]["id"].Value, "десять");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцать");
            Assert.AreEqual(table[12]["id"].Value, "двенадцать");
            Assert.AreEqual(table[13]["id"].Value, "тринадцать");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцать");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцать");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцать");
            Assert.AreEqual(table[17]["id"].Value, "семнадцать");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцать");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцать");
            Assert.AreEqual(table[20]["id"].Value, "двадцать");
            Assert.AreEqual(table[21]["id"].Value, "тридцать");
            Assert.AreEqual(table[22]["id"].Value, "сорок");
            Assert.AreEqual(table[23]["id"].Value, "пятьдесят");
            Assert.AreEqual(table[24]["id"].Value, "шестьдесят");
            Assert.AreEqual(table[25]["id"].Value, "семьдесят");
            Assert.AreEqual(table[26]["id"].Value, "восемьдесят");
            Assert.AreEqual(table[27]["id"].Value, "девяносто");
            Assert.AreEqual(table[28]["id"].Value, "сто");
            Assert.AreEqual(table[29]["id"].Value, "двести");
            Assert.AreEqual(table[30]["id"].Value, "триста");
            Assert.AreEqual(table[31]["id"].Value, "четыреста");
            Assert.AreEqual(table[32]["id"].Value, "пятьсот");
            Assert.AreEqual(table[33]["id"].Value, "шестьсот");
            Assert.AreEqual(table[34]["id"].Value, "семьсот");
            Assert.AreEqual(table[35]["id"].Value, "восемьсот");
            Assert.AreEqual(table[36]["id"].Value, "девятьсот");
            Assert.AreEqual(table[37]["id"].Value, "одну тысячу");
            Assert.AreEqual(table[38]["id"].Value, "две тысячи");
            Assert.AreEqual(table[39]["id"].Value, "три тысячи");
            Assert.AreEqual(table[40]["id"].Value, "четыре тысячи");
            Assert.AreEqual(table[41]["id"].Value, "пять тысяч");
            Assert.AreEqual(table[42]["id"].Value, "шесть тысяч");
            Assert.AreEqual(table[43]["id"].Value, "семь тысяч");
            Assert.AreEqual(table[44]["id"].Value, "восемь тысяч");
            Assert.AreEqual(table[45]["id"].Value, "девять тысяч");
            Assert.AreEqual(table[46]["id"].Value, "десять тысяч");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцать тысяч");
            Assert.AreEqual(table[48]["id"].Value, "двенадцать тысяч");
            Assert.AreEqual(table[49]["id"].Value, "тринадцать тысяч");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцать тысяч");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцать тысяч");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцать тысяч");
            Assert.AreEqual(table[53]["id"].Value, "семнадцать тысяч");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцать тысяч");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцать тысяч");
            Assert.AreEqual(table[56]["id"].Value, "один миллион");
            Assert.AreEqual(table[57]["id"].Value, "два миллиона");
            Assert.AreEqual(table[58]["id"].Value, "три миллиона");
            Assert.AreEqual(table[59]["id"].Value, "четыре миллиона");
            Assert.AreEqual(table[60]["id"].Value, "пять миллионов");
            Assert.AreEqual(table[61]["id"].Value, "шесть миллионов");
            Assert.AreEqual(table[62]["id"].Value, "семь миллионов");
            Assert.AreEqual(table[63]["id"].Value, "восемь миллионов");
            Assert.AreEqual(table[64]["id"].Value, "девять миллионов");
            Assert.AreEqual(table[65]["id"].Value, "десять миллионов");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцать миллионов");
            Assert.AreEqual(table[67]["id"].Value, "двенадцать миллионов");
            Assert.AreEqual(table[68]["id"].Value, "тринадцать миллионов");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцать миллионов");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцать миллионов");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцать миллионов");
            Assert.AreEqual(table[72]["id"].Value, "семнадцать миллионов");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцать миллионов");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцать миллионов");
            Assert.AreEqual(table[75]["id"].Value, "один миллиард");
            Assert.AreEqual(table[76]["id"].Value, "два миллиарда");
            Assert.AreEqual(table[77]["id"].Value, "три миллиарда");
            Assert.AreEqual(table[78]["id"].Value, "четыре миллиарда");
            Assert.AreEqual(table[79]["id"].Value, "пять миллиардов");
            Assert.AreEqual(table[80]["id"].Value, "шесть миллиардов");
            Assert.AreEqual(table[81]["id"].Value, "семь миллиардов");
            Assert.AreEqual(table[82]["id"].Value, "восемь миллиардов");
            Assert.AreEqual(table[83]["id"].Value, "девять миллиардов");
            Assert.AreEqual(table[84]["id"].Value, "десять миллиардов");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцать миллиардов");
            Assert.AreEqual(table[86]["id"].Value, "двенадцать миллиардов");
            Assert.AreEqual(table[87]["id"].Value, "тринадцать миллиардов");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцать миллиардов");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцать миллиардов");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцать миллиардов");
            Assert.AreEqual(table[91]["id"].Value, "семнадцать миллиардов");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцать миллиардов");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцать миллиардов");
            Assert.AreEqual(table[94]["id"].Value, "один триллион");
            Assert.AreEqual(table[95]["id"].Value, "два триллиона");
            Assert.AreEqual(table[96]["id"].Value, "три триллиона");
            Assert.AreEqual(table[97]["id"].Value, "четыре триллиона");
            Assert.AreEqual(table[98]["id"].Value, "пять триллионов");
            Assert.AreEqual(table[99]["id"].Value, "шесть триллионов");
            Assert.AreEqual(table[100]["id"].Value, "семь триллионов");
            Assert.AreEqual(table[101]["id"].Value, "восемь триллионов");
            Assert.AreEqual(table[102]["id"].Value, "девять триллионов");
            Assert.AreEqual(table[103]["id"].Value, "десять триллионов");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцать триллионов");
            Assert.AreEqual(table[105]["id"].Value, "двенадцать триллионов");
            Assert.AreEqual(table[106]["id"].Value, "тринадцать триллионов");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцать триллионов");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцать триллионов");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцать триллионов");
            Assert.AreEqual(table[110]["id"].Value, "семнадцать триллионов");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцать триллионов");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцать триллионов");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest4()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Instrumental, Sex.Male, false, false, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нолем");
            Assert.AreEqual(table[1]["id"].Value, "одним");
            Assert.AreEqual(table[2]["id"].Value, "двумя");
            Assert.AreEqual(table[3]["id"].Value, "тремя");
            Assert.AreEqual(table[4]["id"].Value, "четыремя");
            Assert.AreEqual(table[5]["id"].Value, "пятью");
            Assert.AreEqual(table[6]["id"].Value, "шестью");
            Assert.AreEqual(table[7]["id"].Value, "семью");
            Assert.AreEqual(table[8]["id"].Value, "восьмью");
            Assert.AreEqual(table[9]["id"].Value, "девятью");
            Assert.AreEqual(table[10]["id"].Value, "десятью");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатью");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатью");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатью");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатью");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатью");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатью");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатью");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатью");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатью");
            Assert.AreEqual(table[20]["id"].Value, "двадцатью");
            Assert.AreEqual(table[21]["id"].Value, "тридцатью");
            Assert.AreEqual(table[22]["id"].Value, "сорока");
            Assert.AreEqual(table[23]["id"].Value, "пятьюдесятью");
            Assert.AreEqual(table[24]["id"].Value, "шестьюдесятью");
            Assert.AreEqual(table[25]["id"].Value, "семьюдесятью");
            Assert.AreEqual(table[26]["id"].Value, "восьмьюдесятью");
            Assert.AreEqual(table[27]["id"].Value, "девяноста");
            Assert.AreEqual(table[28]["id"].Value, "ста");
            Assert.AreEqual(table[29]["id"].Value, "двумястами");
            Assert.AreEqual(table[30]["id"].Value, "тремястами");
            Assert.AreEqual(table[31]["id"].Value, "четырьмястами");
            Assert.AreEqual(table[32]["id"].Value, "пятьюстами");
            Assert.AreEqual(table[33]["id"].Value, "шестьюстами");
            Assert.AreEqual(table[34]["id"].Value, "семьюстами");
            Assert.AreEqual(table[35]["id"].Value, "восьмьюстами");
            Assert.AreEqual(table[36]["id"].Value, "девятьюстами");
            Assert.AreEqual(table[37]["id"].Value, "одной тысячей");
            Assert.AreEqual(table[38]["id"].Value, "двумя тысячами");
            Assert.AreEqual(table[39]["id"].Value, "тремя тысячами");
            Assert.AreEqual(table[40]["id"].Value, "четыремя тысячами");
            Assert.AreEqual(table[41]["id"].Value, "пятью тысячами");
            Assert.AreEqual(table[42]["id"].Value, "шестью тысячами");
            Assert.AreEqual(table[43]["id"].Value, "семью тысячами");
            Assert.AreEqual(table[44]["id"].Value, "восьмью тысячами");
            Assert.AreEqual(table[45]["id"].Value, "девятью тысячами");
            Assert.AreEqual(table[46]["id"].Value, "десятью тысячами");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатью тысячами");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатью тысячами");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатью тысячами");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатью тысячами");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатью тысячами");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатью тысячами");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатью тысячами");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатью тысячами");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатью тысячами");
            Assert.AreEqual(table[56]["id"].Value, "одним миллионом");
            Assert.AreEqual(table[57]["id"].Value, "двумя миллионами");
            Assert.AreEqual(table[58]["id"].Value, "тремя миллионами");
            Assert.AreEqual(table[59]["id"].Value, "четыремя миллионами");
            Assert.AreEqual(table[60]["id"].Value, "пятью миллионами");
            Assert.AreEqual(table[61]["id"].Value, "шестью миллионами");
            Assert.AreEqual(table[62]["id"].Value, "семью миллионами");
            Assert.AreEqual(table[63]["id"].Value, "восьмью миллионами");
            Assert.AreEqual(table[64]["id"].Value, "девятью миллионами");
            Assert.AreEqual(table[65]["id"].Value, "десятью миллионами");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатью миллионами");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатью миллионами");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатью миллионами");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатью миллионами");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатью миллионами");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатью миллионами");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатью миллионами");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатью миллионами");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатью миллионами");
            Assert.AreEqual(table[75]["id"].Value, "одним миллиардом");
            Assert.AreEqual(table[76]["id"].Value, "двумя миллиардами");
            Assert.AreEqual(table[77]["id"].Value, "тремя миллиардами");
            Assert.AreEqual(table[78]["id"].Value, "четыремя миллиардами");
            Assert.AreEqual(table[79]["id"].Value, "пятью миллиардами");
            Assert.AreEqual(table[80]["id"].Value, "шестью миллиардами");
            Assert.AreEqual(table[81]["id"].Value, "семью миллиардами");
            Assert.AreEqual(table[82]["id"].Value, "восьмью миллиардами");
            Assert.AreEqual(table[83]["id"].Value, "девятью миллиардами");
            Assert.AreEqual(table[84]["id"].Value, "десятью миллиардами");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатью миллиардами");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатью миллиардами");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатью миллиардами");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатью миллиардами");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатью миллиардами");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатью миллиардами");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатью миллиардами");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатью миллиардами");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатью миллиардами");
            Assert.AreEqual(table[94]["id"].Value, "одним триллионом");
            Assert.AreEqual(table[95]["id"].Value, "двумя триллионами");
            Assert.AreEqual(table[96]["id"].Value, "тремя триллионами");
            Assert.AreEqual(table[97]["id"].Value, "четыремя триллионами");
            Assert.AreEqual(table[98]["id"].Value, "пятью триллионами");
            Assert.AreEqual(table[99]["id"].Value, "шестью триллионами");
            Assert.AreEqual(table[100]["id"].Value, "семью триллионами");
            Assert.AreEqual(table[101]["id"].Value, "восьмью триллионами");
            Assert.AreEqual(table[102]["id"].Value, "девятью триллионами");
            Assert.AreEqual(table[103]["id"].Value, "десятью триллионами");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатью триллионами");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатью триллионами");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатью триллионами");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатью триллионами");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатью триллионами");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатью триллионами");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатью триллионами");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатью триллионами");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатью триллионами");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest5()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Prepositional, Sex.Male, false, false, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "ноле");
            Assert.AreEqual(table[1]["id"].Value, "одном");
            Assert.AreEqual(table[2]["id"].Value, "двух");
            Assert.AreEqual(table[3]["id"].Value, "трех");
            Assert.AreEqual(table[4]["id"].Value, "четырех");
            Assert.AreEqual(table[5]["id"].Value, "пяти");
            Assert.AreEqual(table[6]["id"].Value, "шести");
            Assert.AreEqual(table[7]["id"].Value, "семи");
            Assert.AreEqual(table[8]["id"].Value, "восьми");
            Assert.AreEqual(table[9]["id"].Value, "девяти");
            Assert.AreEqual(table[10]["id"].Value, "десяти");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцати");
            Assert.AreEqual(table[12]["id"].Value, "двенадцати");
            Assert.AreEqual(table[13]["id"].Value, "тринадцати");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцати");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцати");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцати");
            Assert.AreEqual(table[17]["id"].Value, "семнадцати");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцати");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцати");
            Assert.AreEqual(table[20]["id"].Value, "двадцати");
            Assert.AreEqual(table[21]["id"].Value, "тридцати");
            Assert.AreEqual(table[22]["id"].Value, "сорока");
            Assert.AreEqual(table[23]["id"].Value, "пятидесяти");
            Assert.AreEqual(table[24]["id"].Value, "шестидесяти");
            Assert.AreEqual(table[25]["id"].Value, "семидесяти");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесяти");
            Assert.AreEqual(table[27]["id"].Value, "девяноста");
            Assert.AreEqual(table[28]["id"].Value, "ста");
            Assert.AreEqual(table[29]["id"].Value, "двухстах");
            Assert.AreEqual(table[30]["id"].Value, "трехстах");
            Assert.AreEqual(table[31]["id"].Value, "четырехстах");
            Assert.AreEqual(table[32]["id"].Value, "пятистах");
            Assert.AreEqual(table[33]["id"].Value, "шестистах");
            Assert.AreEqual(table[34]["id"].Value, "семистах");
            Assert.AreEqual(table[35]["id"].Value, "восьмистах");
            Assert.AreEqual(table[36]["id"].Value, "девятистах");
            Assert.AreEqual(table[37]["id"].Value, "одной тысяче");
            Assert.AreEqual(table[38]["id"].Value, "двух тысячах");
            Assert.AreEqual(table[39]["id"].Value, "трех тысячах");
            Assert.AreEqual(table[40]["id"].Value, "четырех тысячах");
            Assert.AreEqual(table[41]["id"].Value, "пяти тысячах");
            Assert.AreEqual(table[42]["id"].Value, "шести тысячах");
            Assert.AreEqual(table[43]["id"].Value, "семи тысячах");
            Assert.AreEqual(table[44]["id"].Value, "восьми тысячах");
            Assert.AreEqual(table[45]["id"].Value, "девяти тысячах");
            Assert.AreEqual(table[46]["id"].Value, "десяти тысячах");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцати тысячах");
            Assert.AreEqual(table[48]["id"].Value, "двенадцати тысячах");
            Assert.AreEqual(table[49]["id"].Value, "тринадцати тысячах");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцати тысячах");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцати тысячах");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцати тысячах");
            Assert.AreEqual(table[53]["id"].Value, "семнадцати тысячах");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцати тысячах");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцати тысячах");
            Assert.AreEqual(table[56]["id"].Value, "одном миллионе");
            Assert.AreEqual(table[57]["id"].Value, "двух миллионах");
            Assert.AreEqual(table[58]["id"].Value, "трех миллионах");
            Assert.AreEqual(table[59]["id"].Value, "четырех миллионах");
            Assert.AreEqual(table[60]["id"].Value, "пяти миллионах");
            Assert.AreEqual(table[61]["id"].Value, "шести миллионах");
            Assert.AreEqual(table[62]["id"].Value, "семи миллионах");
            Assert.AreEqual(table[63]["id"].Value, "восьми миллионах");
            Assert.AreEqual(table[64]["id"].Value, "девяти миллионах");
            Assert.AreEqual(table[65]["id"].Value, "десяти миллионах");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцати миллионах");
            Assert.AreEqual(table[67]["id"].Value, "двенадцати миллионах");
            Assert.AreEqual(table[68]["id"].Value, "тринадцати миллионах");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцати миллионах");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцати миллионах");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцати миллионах");
            Assert.AreEqual(table[72]["id"].Value, "семнадцати миллионах");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцати миллионах");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцати миллионах");
            Assert.AreEqual(table[75]["id"].Value, "одном миллиарде");
            Assert.AreEqual(table[76]["id"].Value, "двух миллиардах");
            Assert.AreEqual(table[77]["id"].Value, "трех миллиардах");
            Assert.AreEqual(table[78]["id"].Value, "четырех миллиардах");
            Assert.AreEqual(table[79]["id"].Value, "пяти миллиардах");
            Assert.AreEqual(table[80]["id"].Value, "шести миллиардах");
            Assert.AreEqual(table[81]["id"].Value, "семи миллиардах");
            Assert.AreEqual(table[82]["id"].Value, "восьми миллиардах");
            Assert.AreEqual(table[83]["id"].Value, "девяти миллиардах");
            Assert.AreEqual(table[84]["id"].Value, "десяти миллиардах");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцати миллиардах");
            Assert.AreEqual(table[86]["id"].Value, "двенадцати миллиардах");
            Assert.AreEqual(table[87]["id"].Value, "тринадцати миллиардах");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцати миллиардах");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцати миллиардах");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцати миллиардах");
            Assert.AreEqual(table[91]["id"].Value, "семнадцати миллиардах");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцати миллиардах");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцати миллиардах");
            Assert.AreEqual(table[94]["id"].Value, "одном триллионе");
            Assert.AreEqual(table[95]["id"].Value, "двух триллионах");
            Assert.AreEqual(table[96]["id"].Value, "трех триллионах");
            Assert.AreEqual(table[97]["id"].Value, "четырех триллионах");
            Assert.AreEqual(table[98]["id"].Value, "пяти триллионах");
            Assert.AreEqual(table[99]["id"].Value, "шести триллионах");
            Assert.AreEqual(table[100]["id"].Value, "семи триллионах");
            Assert.AreEqual(table[101]["id"].Value, "восьми триллионах");
            Assert.AreEqual(table[102]["id"].Value, "девяти триллионах");
            Assert.AreEqual(table[103]["id"].Value, "десяти триллионах");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцати триллионах");
            Assert.AreEqual(table[105]["id"].Value, "двенадцати триллионах");
            Assert.AreEqual(table[106]["id"].Value, "тринадцати триллионах");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцати триллионах");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцати триллионах");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцати триллионах");
            Assert.AreEqual(table[110]["id"].Value, "семнадцати триллионах");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцати триллионах");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцати триллионах");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest6()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Nominative, Sex.Male, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевой");
            Assert.AreEqual(table[1]["id"].Value, "первый");
            Assert.AreEqual(table[2]["id"].Value, "второй");
            Assert.AreEqual(table[3]["id"].Value, "третий");
            Assert.AreEqual(table[4]["id"].Value, "четвертый");
            Assert.AreEqual(table[5]["id"].Value, "пятый");
            Assert.AreEqual(table[6]["id"].Value, "шестой");
            Assert.AreEqual(table[7]["id"].Value, "седьмой");
            Assert.AreEqual(table[8]["id"].Value, "восьмой");
            Assert.AreEqual(table[9]["id"].Value, "девятый");
            Assert.AreEqual(table[10]["id"].Value, "десятый");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатый");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатый");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатый");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатый");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатый");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатый");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатый");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатый");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатый");
            Assert.AreEqual(table[20]["id"].Value, "двадцатый");
            Assert.AreEqual(table[21]["id"].Value, "тридцатый");
            Assert.AreEqual(table[22]["id"].Value, "сороковой");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятый");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятый");
            Assert.AreEqual(table[25]["id"].Value, "семидесятый");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятый");
            Assert.AreEqual(table[27]["id"].Value, "девяностый");
            Assert.AreEqual(table[28]["id"].Value, "сотый");
            Assert.AreEqual(table[29]["id"].Value, "двухсотый");
            Assert.AreEqual(table[30]["id"].Value, "трехсотый");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотый");
            Assert.AreEqual(table[32]["id"].Value, "пятисотый");
            Assert.AreEqual(table[33]["id"].Value, "шестисотый");
            Assert.AreEqual(table[34]["id"].Value, "семисотый");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотый");
            Assert.AreEqual(table[36]["id"].Value, "девятисотый");
            Assert.AreEqual(table[37]["id"].Value, "тысячный");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячный");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячный");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячный");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячный");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячный");
            Assert.AreEqual(table[43]["id"].Value, "семитысячный");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячный");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячный");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячный");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячный");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячный");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячный");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячный");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячный");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячный");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячный");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячный");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячный");
            Assert.AreEqual(table[56]["id"].Value, "миллионный");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионный");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионный");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионный");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионный");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионный");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионный");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионный");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионный");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионный");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионный");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионный");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионный");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионный");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионный");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионный");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионный");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионный");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионный");
            Assert.AreEqual(table[75]["id"].Value, "миллиардный");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардный");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардный");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардный");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардный");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардный");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардный");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардный");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардный");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардный");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардный");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардный");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардный");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардный");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардный");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардный");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардный");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардный");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардный");
            Assert.AreEqual(table[94]["id"].Value, "триллионный");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионный");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионный");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионный");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионный");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионный");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионный");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионный");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионный");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионный");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионный");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионный");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионный");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионный");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионный");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионный");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионный");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионный");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионный");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest7()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Genitive, Sex.Male, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевого");
            Assert.AreEqual(table[1]["id"].Value, "первого");
            Assert.AreEqual(table[2]["id"].Value, "второго");
            Assert.AreEqual(table[3]["id"].Value, "третьего");
            Assert.AreEqual(table[4]["id"].Value, "четвертого");
            Assert.AreEqual(table[5]["id"].Value, "пятого");
            Assert.AreEqual(table[6]["id"].Value, "шестого");
            Assert.AreEqual(table[7]["id"].Value, "седьмого");
            Assert.AreEqual(table[8]["id"].Value, "восьмого");
            Assert.AreEqual(table[9]["id"].Value, "девятого");
            Assert.AreEqual(table[10]["id"].Value, "десятого");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатого");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатого");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатого");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатого");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатого");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатого");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатого");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатого");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатого");
            Assert.AreEqual(table[20]["id"].Value, "двадцатого");
            Assert.AreEqual(table[21]["id"].Value, "тридцатого");
            Assert.AreEqual(table[22]["id"].Value, "сорокового");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятого");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятого");
            Assert.AreEqual(table[25]["id"].Value, "семидесятого");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятого");
            Assert.AreEqual(table[27]["id"].Value, "девяностого");
            Assert.AreEqual(table[28]["id"].Value, "сотого");
            Assert.AreEqual(table[29]["id"].Value, "двухсотого");
            Assert.AreEqual(table[30]["id"].Value, "трехсотого");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотого");
            Assert.AreEqual(table[32]["id"].Value, "пятисотого");
            Assert.AreEqual(table[33]["id"].Value, "шестисотого");
            Assert.AreEqual(table[34]["id"].Value, "семисотого");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотого");
            Assert.AreEqual(table[36]["id"].Value, "девятисотого");
            Assert.AreEqual(table[37]["id"].Value, "тысячного");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячного");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячного");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячного");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячного");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячного");
            Assert.AreEqual(table[43]["id"].Value, "семитысячного");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячного");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячного");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячного");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячного");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячного");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячного");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячного");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячного");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячного");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячного");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячного");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячного");
            Assert.AreEqual(table[56]["id"].Value, "миллионного");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионного");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионного");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионного");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионного");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионного");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионного");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионного");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионного");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионного");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионного");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионного");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионного");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионного");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионного");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионного");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионного");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионного");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионного");
            Assert.AreEqual(table[75]["id"].Value, "миллиардного");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардного");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардного");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардного");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардного");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардного");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардного");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардного");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардного");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардного");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардного");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардного");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардного");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардного");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардного");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардного");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардного");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардного");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардного");
            Assert.AreEqual(table[94]["id"].Value, "триллионного");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионного");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионного");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионного");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионного");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионного");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионного");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионного");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионного");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионного");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионного");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионного");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионного");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионного");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионного");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионного");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионного");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионного");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионного");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest8()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Dative, Sex.Male, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевому");
            Assert.AreEqual(table[1]["id"].Value, "первому");
            Assert.AreEqual(table[2]["id"].Value, "второму");
            Assert.AreEqual(table[3]["id"].Value, "третьему");
            Assert.AreEqual(table[4]["id"].Value, "четвертому");
            Assert.AreEqual(table[5]["id"].Value, "пятому");
            Assert.AreEqual(table[6]["id"].Value, "шестому");
            Assert.AreEqual(table[7]["id"].Value, "седьмому");
            Assert.AreEqual(table[8]["id"].Value, "восьмому");
            Assert.AreEqual(table[9]["id"].Value, "девятому");
            Assert.AreEqual(table[10]["id"].Value, "десятому");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатому");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатому");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатому");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатому");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатому");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатому");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатому");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатому");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатому");
            Assert.AreEqual(table[20]["id"].Value, "двадцатому");
            Assert.AreEqual(table[21]["id"].Value, "тридцатому");
            Assert.AreEqual(table[22]["id"].Value, "сороковому");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятому");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятому");
            Assert.AreEqual(table[25]["id"].Value, "семидесятому");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятому");
            Assert.AreEqual(table[27]["id"].Value, "девяностому");
            Assert.AreEqual(table[28]["id"].Value, "сотому");
            Assert.AreEqual(table[29]["id"].Value, "двухсотому");
            Assert.AreEqual(table[30]["id"].Value, "трехсотому");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотому");
            Assert.AreEqual(table[32]["id"].Value, "пятисотому");
            Assert.AreEqual(table[33]["id"].Value, "шестисотому");
            Assert.AreEqual(table[34]["id"].Value, "семисотому");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотому");
            Assert.AreEqual(table[36]["id"].Value, "девятисотому");
            Assert.AreEqual(table[37]["id"].Value, "тысячному");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячному");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячному");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячному");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячному");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячному");
            Assert.AreEqual(table[43]["id"].Value, "семитысячному");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячному");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячному");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячному");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячному");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячному");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячному");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячному");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячному");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячному");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячному");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячному");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячному");
            Assert.AreEqual(table[56]["id"].Value, "миллионному");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионному");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионному");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионному");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионному");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионному");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионному");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионному");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионному");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионному");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионному");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионному");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионному");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионному");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионному");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионному");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионному");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионному");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионному");
            Assert.AreEqual(table[75]["id"].Value, "миллиардному");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардному");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардному");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардному");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардному");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардному");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардному");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардному");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардному");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардному");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардному");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардному");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардному");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардному");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардному");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардному");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардному");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардному");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардному");
            Assert.AreEqual(table[94]["id"].Value, "триллионному");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионному");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионному");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионному");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионному");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионному");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионному");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионному");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионному");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионному");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионному");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионному");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионному");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионному");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионному");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионному");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионному");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионному");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионному");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest9()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Accusative, Sex.Male, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевого");
            Assert.AreEqual(table[1]["id"].Value, "первого");
            Assert.AreEqual(table[2]["id"].Value, "второго");
            Assert.AreEqual(table[3]["id"].Value, "третьего");
            Assert.AreEqual(table[4]["id"].Value, "четвертого");
            Assert.AreEqual(table[5]["id"].Value, "пятого");
            Assert.AreEqual(table[6]["id"].Value, "шестого");
            Assert.AreEqual(table[7]["id"].Value, "седьмого");
            Assert.AreEqual(table[8]["id"].Value, "восьмого");
            Assert.AreEqual(table[9]["id"].Value, "девятого");
            Assert.AreEqual(table[10]["id"].Value, "десятого");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатого");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатого");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатого");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатого");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатого");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатого");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатого");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатого");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатого");
            Assert.AreEqual(table[20]["id"].Value, "двадцатого");
            Assert.AreEqual(table[21]["id"].Value, "тридцатого");
            Assert.AreEqual(table[22]["id"].Value, "сорокового");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятого");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятого");
            Assert.AreEqual(table[25]["id"].Value, "семидесятого");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятого");
            Assert.AreEqual(table[27]["id"].Value, "девяностого");
            Assert.AreEqual(table[28]["id"].Value, "сотого");
            Assert.AreEqual(table[29]["id"].Value, "двухсотого");
            Assert.AreEqual(table[30]["id"].Value, "трехсотого");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотого");
            Assert.AreEqual(table[32]["id"].Value, "пятисотого");
            Assert.AreEqual(table[33]["id"].Value, "шестисотого");
            Assert.AreEqual(table[34]["id"].Value, "семисотого");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотого");
            Assert.AreEqual(table[36]["id"].Value, "девятисотого");
            Assert.AreEqual(table[37]["id"].Value, "тысячного");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячного");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячного");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячного");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячного");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячного");
            Assert.AreEqual(table[43]["id"].Value, "семитысячного");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячного");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячного");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячного");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячного");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячного");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячного");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячного");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячного");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячного");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячного");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячного");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячного");
            Assert.AreEqual(table[56]["id"].Value, "миллионного");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионного");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионного");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионного");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионного");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионного");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионного");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионного");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионного");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионного");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионного");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионного");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионного");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионного");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионного");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионного");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионного");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионного");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионного");
            Assert.AreEqual(table[75]["id"].Value, "миллиардного");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардного");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардного");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардного");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардного");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардного");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардного");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардного");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардного");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардного");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардного");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардного");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардного");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардного");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардного");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардного");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардного");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардного");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардного");
            Assert.AreEqual(table[94]["id"].Value, "триллионного");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионного");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионного");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионного");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионного");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионного");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионного");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионного");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионного");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионного");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионного");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионного");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионного");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионного");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионного");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионного");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионного");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионного");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионного");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest10()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Instrumental, Sex.Male, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевым");
            Assert.AreEqual(table[1]["id"].Value, "первым");
            Assert.AreEqual(table[2]["id"].Value, "вторым");
            Assert.AreEqual(table[3]["id"].Value, "третьим");
            Assert.AreEqual(table[4]["id"].Value, "четвертым");
            Assert.AreEqual(table[5]["id"].Value, "пятым");
            Assert.AreEqual(table[6]["id"].Value, "шестым");
            Assert.AreEqual(table[7]["id"].Value, "седьмым");
            Assert.AreEqual(table[8]["id"].Value, "восьмым");
            Assert.AreEqual(table[9]["id"].Value, "девятым");
            Assert.AreEqual(table[10]["id"].Value, "десятым");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатым");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатым");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатым");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатым");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатым");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатым");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатым");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатым");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатым");
            Assert.AreEqual(table[20]["id"].Value, "двадцатым");
            Assert.AreEqual(table[21]["id"].Value, "тридцатым");
            Assert.AreEqual(table[22]["id"].Value, "сороковым");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятым");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятым");
            Assert.AreEqual(table[25]["id"].Value, "семидесятым");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятым");
            Assert.AreEqual(table[27]["id"].Value, "девяностым");
            Assert.AreEqual(table[28]["id"].Value, "сотым");
            Assert.AreEqual(table[29]["id"].Value, "двухсотым");
            Assert.AreEqual(table[30]["id"].Value, "трехсотым");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотым");
            Assert.AreEqual(table[32]["id"].Value, "пятисотым");
            Assert.AreEqual(table[33]["id"].Value, "шестисотым");
            Assert.AreEqual(table[34]["id"].Value, "семисотым");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотым");
            Assert.AreEqual(table[36]["id"].Value, "девятисотым");
            Assert.AreEqual(table[37]["id"].Value, "тысячным");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячным");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячным");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячным");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячным");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячным");
            Assert.AreEqual(table[43]["id"].Value, "семитысячным");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячным");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячным");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячным");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячным");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячным");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячным");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячным");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячным");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячным");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячным");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячным");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячным");
            Assert.AreEqual(table[56]["id"].Value, "миллионным");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионным");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионным");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионным");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионным");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионным");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионным");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионным");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионным");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионным");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионным");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионным");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионным");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионным");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионным");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионным");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионным");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионным");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионным");
            Assert.AreEqual(table[75]["id"].Value, "миллиардным");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардным");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардным");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардным");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардным");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардным");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардным");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардным");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардным");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардным");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардным");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардным");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардным");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардным");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардным");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардным");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардным");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардным");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардным");
            Assert.AreEqual(table[94]["id"].Value, "триллионным");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионным");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионным");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионным");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионным");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионным");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионным");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионным");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионным");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионным");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионным");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионным");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионным");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионным");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионным");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионным");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионным");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионным");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионным");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest11()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Prepositional, Sex.Male, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевом");
            Assert.AreEqual(table[1]["id"].Value, "первом");
            Assert.AreEqual(table[2]["id"].Value, "втором");
            Assert.AreEqual(table[3]["id"].Value, "третьем");
            Assert.AreEqual(table[4]["id"].Value, "четвертом");
            Assert.AreEqual(table[5]["id"].Value, "пятом");
            Assert.AreEqual(table[6]["id"].Value, "шестом");
            Assert.AreEqual(table[7]["id"].Value, "седьмом");
            Assert.AreEqual(table[8]["id"].Value, "восьмом");
            Assert.AreEqual(table[9]["id"].Value, "девятом");
            Assert.AreEqual(table[10]["id"].Value, "десятом");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатом");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатом");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатом");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатом");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатом");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатом");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатом");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатом");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатом");
            Assert.AreEqual(table[20]["id"].Value, "двадцатом");
            Assert.AreEqual(table[21]["id"].Value, "тридцатом");
            Assert.AreEqual(table[22]["id"].Value, "сороковом");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятом");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятом");
            Assert.AreEqual(table[25]["id"].Value, "семидесятом");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятом");
            Assert.AreEqual(table[27]["id"].Value, "девяностом");
            Assert.AreEqual(table[28]["id"].Value, "сотом");
            Assert.AreEqual(table[29]["id"].Value, "двухсотом");
            Assert.AreEqual(table[30]["id"].Value, "трехсотом");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотом");
            Assert.AreEqual(table[32]["id"].Value, "пятисотом");
            Assert.AreEqual(table[33]["id"].Value, "шестисотом");
            Assert.AreEqual(table[34]["id"].Value, "семисотом");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотом");
            Assert.AreEqual(table[36]["id"].Value, "девятисотом");
            Assert.AreEqual(table[37]["id"].Value, "тысячном");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячном");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячном");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячном");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячном");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячном");
            Assert.AreEqual(table[43]["id"].Value, "семитысячном");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячном");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячном");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячном");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячном");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячном");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячном");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячном");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячном");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячном");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячном");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячном");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячном");
            Assert.AreEqual(table[56]["id"].Value, "миллионном");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионном");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионном");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионном");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионном");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионном");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионном");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионном");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионном");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионном");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионном");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионном");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионном");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионном");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионном");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионном");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионном");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионном");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионном");
            Assert.AreEqual(table[75]["id"].Value, "миллиардном");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардном");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардном");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардном");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардном");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардном");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардном");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардном");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардном");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардном");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардном");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардном");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардном");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардном");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардном");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардном");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардном");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардном");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардном");
            Assert.AreEqual(table[94]["id"].Value, "триллионном");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионном");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионном");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионном");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионном");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионном");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионном");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионном");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионном");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионном");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионном");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионном");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионном");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионном");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионном");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионном");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионном");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионном");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионном");
        }
    
        [TestMethod]
        public void ConvertModuleConvertIntTest12()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Nominative, Sex.Female, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевая");
            Assert.AreEqual(table[1]["id"].Value, "первая");
            Assert.AreEqual(table[2]["id"].Value, "вторая");
            Assert.AreEqual(table[3]["id"].Value, "третья");
            Assert.AreEqual(table[4]["id"].Value, "четвертая");
            Assert.AreEqual(table[5]["id"].Value, "пятая");
            Assert.AreEqual(table[6]["id"].Value, "шестая");
            Assert.AreEqual(table[7]["id"].Value, "седьмая");
            Assert.AreEqual(table[8]["id"].Value, "восьмая");
            Assert.AreEqual(table[9]["id"].Value, "девятая");
            Assert.AreEqual(table[10]["id"].Value, "десятая");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатая");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатая");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатая");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатая");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатая");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатая");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатая");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатая");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатая");
            Assert.AreEqual(table[20]["id"].Value, "двадцатая");
            Assert.AreEqual(table[21]["id"].Value, "тридцатая");
            Assert.AreEqual(table[22]["id"].Value, "сороковая");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятая");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятая");
            Assert.AreEqual(table[25]["id"].Value, "семидесятая");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятая");
            Assert.AreEqual(table[27]["id"].Value, "девяностая");
            Assert.AreEqual(table[28]["id"].Value, "сотая");
            Assert.AreEqual(table[29]["id"].Value, "двухсотая");
            Assert.AreEqual(table[30]["id"].Value, "трехсотая");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотая");
            Assert.AreEqual(table[32]["id"].Value, "пятисотая");
            Assert.AreEqual(table[33]["id"].Value, "шестисотая");
            Assert.AreEqual(table[34]["id"].Value, "семисотая");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотая");
            Assert.AreEqual(table[36]["id"].Value, "девятисотая");
            Assert.AreEqual(table[37]["id"].Value, "тысячная");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячная");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячная");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячная");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячная");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячная");
            Assert.AreEqual(table[43]["id"].Value, "семитысячная");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячная");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячная");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячная");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячная");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячная");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячная");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячная");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячная");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячная");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячная");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячная");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячная");
            Assert.AreEqual(table[56]["id"].Value, "миллионная");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионная");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионная");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионная");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионная");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионная");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионная");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионная");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионная");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионная");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионная");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионная");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионная");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионная");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионная");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионная");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионная");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионная");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионная");
            Assert.AreEqual(table[75]["id"].Value, "миллиардная");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардная");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардная");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардная");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардная");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардная");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардная");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардная");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардная");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардная");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардная");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардная");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардная");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардная");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардная");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардная");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардная");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардная");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардная");
            Assert.AreEqual(table[94]["id"].Value, "триллионная");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионная");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионная");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионная");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионная");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионная");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионная");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионная");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионная");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионная");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионная");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионная");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионная");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионная");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионная");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионная");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионная");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионная");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионная");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest13()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Genitive, Sex.Female, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевой");
            Assert.AreEqual(table[1]["id"].Value, "первой");
            Assert.AreEqual(table[2]["id"].Value, "второй");
            Assert.AreEqual(table[3]["id"].Value, "третьей");
            Assert.AreEqual(table[4]["id"].Value, "четвертой");
            Assert.AreEqual(table[5]["id"].Value, "пятой");
            Assert.AreEqual(table[6]["id"].Value, "шестой");
            Assert.AreEqual(table[7]["id"].Value, "седьмой");
            Assert.AreEqual(table[8]["id"].Value, "восьмой");
            Assert.AreEqual(table[9]["id"].Value, "девятой");
            Assert.AreEqual(table[10]["id"].Value, "десятой");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатой");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатой");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатой");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатой");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатой");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатой");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатой");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатой");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатой");
            Assert.AreEqual(table[20]["id"].Value, "двадцатой");
            Assert.AreEqual(table[21]["id"].Value, "тридцатой");
            Assert.AreEqual(table[22]["id"].Value, "сороковой");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятой");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятой");
            Assert.AreEqual(table[25]["id"].Value, "семидесятой");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятой");
            Assert.AreEqual(table[27]["id"].Value, "девяностой");
            Assert.AreEqual(table[28]["id"].Value, "сотой");
            Assert.AreEqual(table[29]["id"].Value, "двухсотой");
            Assert.AreEqual(table[30]["id"].Value, "трехсотой");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотой");
            Assert.AreEqual(table[32]["id"].Value, "пятисотой");
            Assert.AreEqual(table[33]["id"].Value, "шестисотой");
            Assert.AreEqual(table[34]["id"].Value, "семисотой");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотой");
            Assert.AreEqual(table[36]["id"].Value, "девятисотой");
            Assert.AreEqual(table[37]["id"].Value, "тысячной");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячной");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячной");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячной");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячной");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячной");
            Assert.AreEqual(table[43]["id"].Value, "семитысячной");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячной");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячной");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячной");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячной");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячной");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячной");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячной");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячной");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячной");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячной");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячной");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячной");
            Assert.AreEqual(table[56]["id"].Value, "миллионной");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионной");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионной");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионной");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионной");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионной");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионной");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионной");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионной");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионной");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионной");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионной");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионной");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионной");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионной");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионной");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионной");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионной");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионной");
            Assert.AreEqual(table[75]["id"].Value, "миллиардной");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардной");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардной");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардной");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардной");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардной");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардной");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардной");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардной");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардной");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардной");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардной");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардной");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардной");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардной");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардной");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардной");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардной");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардной");
            Assert.AreEqual(table[94]["id"].Value, "триллионной");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионной");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионной");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионной");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионной");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионной");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионной");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионной");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионной");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионной");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионной");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионной");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионной");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионной");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионной");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионной");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионной");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионной");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионной");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest14()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Dative, Sex.Female, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевой");
            Assert.AreEqual(table[1]["id"].Value, "первой");
            Assert.AreEqual(table[2]["id"].Value, "второй");
            Assert.AreEqual(table[3]["id"].Value, "третьей");
            Assert.AreEqual(table[4]["id"].Value, "четвертой");
            Assert.AreEqual(table[5]["id"].Value, "пятой");
            Assert.AreEqual(table[6]["id"].Value, "шестой");
            Assert.AreEqual(table[7]["id"].Value, "седьмой");
            Assert.AreEqual(table[8]["id"].Value, "восьмой");
            Assert.AreEqual(table[9]["id"].Value, "девятой");
            Assert.AreEqual(table[10]["id"].Value, "десятой");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатой");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатой");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатой");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатой");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатой");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатой");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатой");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатой");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатой");
            Assert.AreEqual(table[20]["id"].Value, "двадцатой");
            Assert.AreEqual(table[21]["id"].Value, "тридцатой");
            Assert.AreEqual(table[22]["id"].Value, "сороковой");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятой");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятой");
            Assert.AreEqual(table[25]["id"].Value, "семидесятой");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятой");
            Assert.AreEqual(table[27]["id"].Value, "девяностой");
            Assert.AreEqual(table[28]["id"].Value, "сотой");
            Assert.AreEqual(table[29]["id"].Value, "двухсотой");
            Assert.AreEqual(table[30]["id"].Value, "трехсотой");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотой");
            Assert.AreEqual(table[32]["id"].Value, "пятисотой");
            Assert.AreEqual(table[33]["id"].Value, "шестисотой");
            Assert.AreEqual(table[34]["id"].Value, "семисотой");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотой");
            Assert.AreEqual(table[36]["id"].Value, "девятисотой");
            Assert.AreEqual(table[37]["id"].Value, "тысячной");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячной");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячной");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячной");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячной");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячной");
            Assert.AreEqual(table[43]["id"].Value, "семитысячной");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячной");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячной");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячной");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячной");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячной");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячной");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячной");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячной");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячной");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячной");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячной");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячной");
            Assert.AreEqual(table[56]["id"].Value, "миллионной");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионной");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионной");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионной");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионной");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионной");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионной");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионной");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионной");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионной");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионной");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионной");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионной");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионной");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионной");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионной");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионной");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионной");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионной");
            Assert.AreEqual(table[75]["id"].Value, "миллиардной");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардной");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардной");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардной");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардной");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардной");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардной");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардной");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардной");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардной");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардной");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардной");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардной");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардной");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардной");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардной");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардной");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардной");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардной");
            Assert.AreEqual(table[94]["id"].Value, "триллионной");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионной");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионной");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионной");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионной");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионной");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионной");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионной");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионной");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионной");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионной");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионной");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионной");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионной");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионной");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионной");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионной");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионной");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионной");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest15()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Accusative, Sex.Female, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевую");
            Assert.AreEqual(table[1]["id"].Value, "первую");
            Assert.AreEqual(table[2]["id"].Value, "вторую");
            Assert.AreEqual(table[3]["id"].Value, "третью");
            Assert.AreEqual(table[4]["id"].Value, "четвертую");
            Assert.AreEqual(table[5]["id"].Value, "пятую");
            Assert.AreEqual(table[6]["id"].Value, "шестую");
            Assert.AreEqual(table[7]["id"].Value, "седьмую");
            Assert.AreEqual(table[8]["id"].Value, "восьмую");
            Assert.AreEqual(table[9]["id"].Value, "девятую");
            Assert.AreEqual(table[10]["id"].Value, "десятую");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатую");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатую");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатую");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатую");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатую");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатую");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатую");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатую");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатую");
            Assert.AreEqual(table[20]["id"].Value, "двадцатую");
            Assert.AreEqual(table[21]["id"].Value, "тридцатую");
            Assert.AreEqual(table[22]["id"].Value, "сороковую");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятую");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятую");
            Assert.AreEqual(table[25]["id"].Value, "семидесятую");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятую");
            Assert.AreEqual(table[27]["id"].Value, "девяностую");
            Assert.AreEqual(table[28]["id"].Value, "сотую");
            Assert.AreEqual(table[29]["id"].Value, "двухсотую");
            Assert.AreEqual(table[30]["id"].Value, "трехсотую");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотую");
            Assert.AreEqual(table[32]["id"].Value, "пятисотую");
            Assert.AreEqual(table[33]["id"].Value, "шестисотую");
            Assert.AreEqual(table[34]["id"].Value, "семисотую");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотую");
            Assert.AreEqual(table[36]["id"].Value, "девятисотую");
            Assert.AreEqual(table[37]["id"].Value, "тысячную");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячную");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячную");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячную");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячную");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячную");
            Assert.AreEqual(table[43]["id"].Value, "семитысячную");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячную");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячную");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячную");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячную");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячную");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячную");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячную");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячную");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячную");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячную");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячную");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячную");
            Assert.AreEqual(table[56]["id"].Value, "миллионную");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионную");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионную");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионную");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионную");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионную");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионную");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионную");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионную");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионную");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионную");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионную");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионную");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионную");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионную");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионную");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионную");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионную");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионную");
            Assert.AreEqual(table[75]["id"].Value, "миллиардную");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардную");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардную");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардную");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардную");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардную");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардную");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардную");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардную");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардную");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардную");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардную");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардную");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардную");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардную");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардную");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардную");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардную");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардную");
            Assert.AreEqual(table[94]["id"].Value, "триллионную");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионную");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионную");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионную");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионную");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионную");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионную");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионную");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионную");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионную");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионную");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионную");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионную");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионную");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионную");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионную");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионную");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионную");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионную");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest16()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Instrumental, Sex.Female, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевой");
            Assert.AreEqual(table[1]["id"].Value, "первой");
            Assert.AreEqual(table[2]["id"].Value, "второй");
            Assert.AreEqual(table[3]["id"].Value, "третьей");
            Assert.AreEqual(table[4]["id"].Value, "четвертой");
            Assert.AreEqual(table[5]["id"].Value, "пятой");
            Assert.AreEqual(table[6]["id"].Value, "шестой");
            Assert.AreEqual(table[7]["id"].Value, "седьмой");
            Assert.AreEqual(table[8]["id"].Value, "восьмой");
            Assert.AreEqual(table[9]["id"].Value, "девятой");
            Assert.AreEqual(table[10]["id"].Value, "десятой");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатой");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатой");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатой");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатой");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатой");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатой");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатой");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатой");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатой");
            Assert.AreEqual(table[20]["id"].Value, "двадцатой");
            Assert.AreEqual(table[21]["id"].Value, "тридцатой");
            Assert.AreEqual(table[22]["id"].Value, "сороковой");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятой");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятой");
            Assert.AreEqual(table[25]["id"].Value, "семидесятой");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятой");
            Assert.AreEqual(table[27]["id"].Value, "девяностой");
            Assert.AreEqual(table[28]["id"].Value, "сотой");
            Assert.AreEqual(table[29]["id"].Value, "двухсотой");
            Assert.AreEqual(table[30]["id"].Value, "трехсотой");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотой");
            Assert.AreEqual(table[32]["id"].Value, "пятисотой");
            Assert.AreEqual(table[33]["id"].Value, "шестисотой");
            Assert.AreEqual(table[34]["id"].Value, "семисотой");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотой");
            Assert.AreEqual(table[36]["id"].Value, "девятисотой");
            Assert.AreEqual(table[37]["id"].Value, "тысячной");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячной");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячной");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячной");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячной");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячной");
            Assert.AreEqual(table[43]["id"].Value, "семитысячной");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячной");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячной");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячной");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячной");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячной");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячной");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячной");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячной");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячной");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячной");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячной");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячной");
            Assert.AreEqual(table[56]["id"].Value, "миллионной");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионной");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионной");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионной");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионной");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионной");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионной");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионной");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионной");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионной");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионной");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионной");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионной");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионной");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионной");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионной");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионной");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионной");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионной");
            Assert.AreEqual(table[75]["id"].Value, "миллиардной");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардной");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардной");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардной");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардной");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардной");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардной");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардной");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардной");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардной");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардной");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардной");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардной");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардной");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардной");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардной");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардной");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардной");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардной");
            Assert.AreEqual(table[94]["id"].Value, "триллионной");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионной");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионной");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионной");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионной");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионной");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионной");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионной");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионной");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионной");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионной");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионной");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионной");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионной");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионной");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионной");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионной");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионной");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионной");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest17()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Prepositional, Sex.Female, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевой");
            Assert.AreEqual(table[1]["id"].Value, "первой");
            Assert.AreEqual(table[2]["id"].Value, "второй");
            Assert.AreEqual(table[3]["id"].Value, "третьей");
            Assert.AreEqual(table[4]["id"].Value, "четвертой");
            Assert.AreEqual(table[5]["id"].Value, "пятой");
            Assert.AreEqual(table[6]["id"].Value, "шестой");
            Assert.AreEqual(table[7]["id"].Value, "седьмой");
            Assert.AreEqual(table[8]["id"].Value, "восьмой");
            Assert.AreEqual(table[9]["id"].Value, "девятой");
            Assert.AreEqual(table[10]["id"].Value, "десятой");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатой");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатой");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатой");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатой");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатой");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатой");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатой");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатой");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатой");
            Assert.AreEqual(table[20]["id"].Value, "двадцатой");
            Assert.AreEqual(table[21]["id"].Value, "тридцатой");
            Assert.AreEqual(table[22]["id"].Value, "сороковой");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятой");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятой");
            Assert.AreEqual(table[25]["id"].Value, "семидесятой");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятой");
            Assert.AreEqual(table[27]["id"].Value, "девяностой");
            Assert.AreEqual(table[28]["id"].Value, "сотой");
            Assert.AreEqual(table[29]["id"].Value, "двухсотой");
            Assert.AreEqual(table[30]["id"].Value, "трехсотой");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотой");
            Assert.AreEqual(table[32]["id"].Value, "пятисотой");
            Assert.AreEqual(table[33]["id"].Value, "шестисотой");
            Assert.AreEqual(table[34]["id"].Value, "семисотой");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотой");
            Assert.AreEqual(table[36]["id"].Value, "девятисотой");
            Assert.AreEqual(table[37]["id"].Value, "тысячной");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячной");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячной");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячной");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячной");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячной");
            Assert.AreEqual(table[43]["id"].Value, "семитысячной");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячной");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячной");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячной");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячной");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячной");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячной");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячной");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячной");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячной");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячной");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячной");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячной");
            Assert.AreEqual(table[56]["id"].Value, "миллионной");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионной");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионной");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионной");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионной");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионной");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионной");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионной");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионной");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионной");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионной");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионной");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионной");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионной");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионной");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионной");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионной");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионной");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионной");
            Assert.AreEqual(table[75]["id"].Value, "миллиардной");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардной");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардной");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардной");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардной");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардной");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардной");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардной");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардной");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардной");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардной");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардной");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардной");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардной");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардной");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардной");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардной");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардной");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардной");
            Assert.AreEqual(table[94]["id"].Value, "триллионной");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионной");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионной");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионной");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионной");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионной");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионной");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионной");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионной");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионной");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионной");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионной");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионной");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионной");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионной");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионной");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионной");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионной");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионной");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest18()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Nominative, Sex.Neuter, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевое");
            Assert.AreEqual(table[1]["id"].Value, "первое");
            Assert.AreEqual(table[2]["id"].Value, "второе");
            Assert.AreEqual(table[3]["id"].Value, "третье");
            Assert.AreEqual(table[4]["id"].Value, "четвертое");
            Assert.AreEqual(table[5]["id"].Value, "пятое");
            Assert.AreEqual(table[6]["id"].Value, "шестое");
            Assert.AreEqual(table[7]["id"].Value, "седьмое");
            Assert.AreEqual(table[8]["id"].Value, "восьмое");
            Assert.AreEqual(table[9]["id"].Value, "девятое");
            Assert.AreEqual(table[10]["id"].Value, "десятое");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатое");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатое");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатое");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатое");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатое");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатое");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатое");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатое");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатое");
            Assert.AreEqual(table[20]["id"].Value, "двадцатое");
            Assert.AreEqual(table[21]["id"].Value, "тридцатое");
            Assert.AreEqual(table[22]["id"].Value, "сороковое");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятое");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятое");
            Assert.AreEqual(table[25]["id"].Value, "семидесятое");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятое");
            Assert.AreEqual(table[27]["id"].Value, "девяностое");
            Assert.AreEqual(table[28]["id"].Value, "сотое");
            Assert.AreEqual(table[29]["id"].Value, "двухсотое");
            Assert.AreEqual(table[30]["id"].Value, "трехсотое");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотое");
            Assert.AreEqual(table[32]["id"].Value, "пятисотое");
            Assert.AreEqual(table[33]["id"].Value, "шестисотое");
            Assert.AreEqual(table[34]["id"].Value, "семисотое");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотое");
            Assert.AreEqual(table[36]["id"].Value, "девятисотое");
            Assert.AreEqual(table[37]["id"].Value, "тысячное");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячное");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячное");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячное");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячное");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячное");
            Assert.AreEqual(table[43]["id"].Value, "семитысячное");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячное");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячное");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячное");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячное");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячное");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячное");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячное");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячное");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячное");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячное");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячное");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячное");
            Assert.AreEqual(table[56]["id"].Value, "миллионное");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионное");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионное");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионное");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионное");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионное");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионное");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионное");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионное");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионное");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионное");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионное");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионное");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионное");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионное");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионное");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионное");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионное");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионное");
            Assert.AreEqual(table[75]["id"].Value, "миллиардное");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардное");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардное");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардное");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардное");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардное");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардное");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардное");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардное");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардное");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардное");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардное");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардное");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардное");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардное");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардное");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардное");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардное");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардное");
            Assert.AreEqual(table[94]["id"].Value, "триллионное");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионное");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионное");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионное");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионное");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионное");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионное");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионное");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионное");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионное");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионное");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионное");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионное");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионное");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионное");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионное");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионное");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионное");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионное");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest19()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Genitive, Sex.Neuter, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевого");
            Assert.AreEqual(table[1]["id"].Value, "первого");
            Assert.AreEqual(table[2]["id"].Value, "второго");
            Assert.AreEqual(table[3]["id"].Value, "третьего");
            Assert.AreEqual(table[4]["id"].Value, "четвертого");
            Assert.AreEqual(table[5]["id"].Value, "пятого");
            Assert.AreEqual(table[6]["id"].Value, "шестого");
            Assert.AreEqual(table[7]["id"].Value, "седьмого");
            Assert.AreEqual(table[8]["id"].Value, "восьмого");
            Assert.AreEqual(table[9]["id"].Value, "девятого");
            Assert.AreEqual(table[10]["id"].Value, "десятого");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатого");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатого");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатого");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатого");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатого");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатого");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатого");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатого");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатого");
            Assert.AreEqual(table[20]["id"].Value, "двадцатого");
            Assert.AreEqual(table[21]["id"].Value, "тридцатого");
            Assert.AreEqual(table[22]["id"].Value, "сорокового");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятого");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятого");
            Assert.AreEqual(table[25]["id"].Value, "семидесятого");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятого");
            Assert.AreEqual(table[27]["id"].Value, "девяностого");
            Assert.AreEqual(table[28]["id"].Value, "сотого");
            Assert.AreEqual(table[29]["id"].Value, "двухсотого");
            Assert.AreEqual(table[30]["id"].Value, "трехсотого");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотого");
            Assert.AreEqual(table[32]["id"].Value, "пятисотого");
            Assert.AreEqual(table[33]["id"].Value, "шестисотого");
            Assert.AreEqual(table[34]["id"].Value, "семисотого");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотого");
            Assert.AreEqual(table[36]["id"].Value, "девятисотого");
            Assert.AreEqual(table[37]["id"].Value, "тысячного");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячного");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячного");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячного");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячного");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячного");
            Assert.AreEqual(table[43]["id"].Value, "семитысячного");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячного");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячного");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячного");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячного");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячного");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячного");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячного");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячного");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячного");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячного");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячного");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячного");
            Assert.AreEqual(table[56]["id"].Value, "миллионного");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионного");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионного");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионного");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионного");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионного");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионного");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионного");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионного");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионного");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионного");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионного");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионного");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионного");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионного");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионного");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионного");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионного");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионного");
            Assert.AreEqual(table[75]["id"].Value, "миллиардного");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардного");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардного");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардного");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардного");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардного");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардного");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардного");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардного");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардного");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардного");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардного");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардного");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардного");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардного");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардного");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардного");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардного");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардного");
            Assert.AreEqual(table[94]["id"].Value, "триллионного");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионного");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионного");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионного");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионного");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионного");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионного");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионного");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионного");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионного");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионного");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионного");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионного");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионного");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионного");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионного");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионного");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионного");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионного");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest20()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Dative, Sex.Neuter, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевому");
            Assert.AreEqual(table[1]["id"].Value, "первому");
            Assert.AreEqual(table[2]["id"].Value, "второму");
            Assert.AreEqual(table[3]["id"].Value, "третьему");
            Assert.AreEqual(table[4]["id"].Value, "четвертому");
            Assert.AreEqual(table[5]["id"].Value, "пятому");
            Assert.AreEqual(table[6]["id"].Value, "шестому");
            Assert.AreEqual(table[7]["id"].Value, "седьмому");
            Assert.AreEqual(table[8]["id"].Value, "восьмому");
            Assert.AreEqual(table[9]["id"].Value, "девятому");
            Assert.AreEqual(table[10]["id"].Value, "десятому");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатому");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатому");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатому");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатому");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатому");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатому");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатому");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатому");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатому");
            Assert.AreEqual(table[20]["id"].Value, "двадцатому");
            Assert.AreEqual(table[21]["id"].Value, "тридцатому");
            Assert.AreEqual(table[22]["id"].Value, "сороковому");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятому");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятому");
            Assert.AreEqual(table[25]["id"].Value, "семидесятому");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятому");
            Assert.AreEqual(table[27]["id"].Value, "девяностому");
            Assert.AreEqual(table[28]["id"].Value, "сотому");
            Assert.AreEqual(table[29]["id"].Value, "двухсотому");
            Assert.AreEqual(table[30]["id"].Value, "трехсотому");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотому");
            Assert.AreEqual(table[32]["id"].Value, "пятисотому");
            Assert.AreEqual(table[33]["id"].Value, "шестисотому");
            Assert.AreEqual(table[34]["id"].Value, "семисотому");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотому");
            Assert.AreEqual(table[36]["id"].Value, "девятисотому");
            Assert.AreEqual(table[37]["id"].Value, "тысячному");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячному");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячному");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячному");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячному");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячному");
            Assert.AreEqual(table[43]["id"].Value, "семитысячному");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячному");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячному");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячному");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячному");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячному");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячному");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячному");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячному");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячному");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячному");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячному");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячному");
            Assert.AreEqual(table[56]["id"].Value, "миллионному");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионному");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионному");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионному");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионному");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионному");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионному");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионному");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионному");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионному");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионному");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионному");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионному");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионному");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионному");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионному");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионному");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионному");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионному");
            Assert.AreEqual(table[75]["id"].Value, "миллиардному");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардному");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардному");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардному");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардному");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардному");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардному");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардному");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардному");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардному");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардному");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардному");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардному");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардному");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардному");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардному");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардному");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардному");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардному");
            Assert.AreEqual(table[94]["id"].Value, "триллионному");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионному");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионному");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионному");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионному");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионному");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионному");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионному");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионному");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионному");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионному");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионному");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионному");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионному");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионному");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионному");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионному");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионному");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионному");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest21()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Accusative, Sex.Neuter, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевое");
            Assert.AreEqual(table[1]["id"].Value, "первое");
            Assert.AreEqual(table[2]["id"].Value, "второе");
            Assert.AreEqual(table[3]["id"].Value, "третье");
            Assert.AreEqual(table[4]["id"].Value, "четвертое");
            Assert.AreEqual(table[5]["id"].Value, "пятое");
            Assert.AreEqual(table[6]["id"].Value, "шестое");
            Assert.AreEqual(table[7]["id"].Value, "седьмое");
            Assert.AreEqual(table[8]["id"].Value, "восьмое");
            Assert.AreEqual(table[9]["id"].Value, "девятое");
            Assert.AreEqual(table[10]["id"].Value, "десятое");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатое");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатое");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатое");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатое");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатое");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатое");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатое");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатое");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатое");
            Assert.AreEqual(table[20]["id"].Value, "двадцатое");
            Assert.AreEqual(table[21]["id"].Value, "тридцатое");
            Assert.AreEqual(table[22]["id"].Value, "сороковое");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятое");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятое");
            Assert.AreEqual(table[25]["id"].Value, "семидесятое");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятое");
            Assert.AreEqual(table[27]["id"].Value, "девяностое");
            Assert.AreEqual(table[28]["id"].Value, "сотое");
            Assert.AreEqual(table[29]["id"].Value, "двухсотое");
            Assert.AreEqual(table[30]["id"].Value, "трехсотое");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотое");
            Assert.AreEqual(table[32]["id"].Value, "пятисотое");
            Assert.AreEqual(table[33]["id"].Value, "шестисотое");
            Assert.AreEqual(table[34]["id"].Value, "семисотое");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотое");
            Assert.AreEqual(table[36]["id"].Value, "девятисотое");
            Assert.AreEqual(table[37]["id"].Value, "тысячное");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячное");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячное");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячное");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячное");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячное");
            Assert.AreEqual(table[43]["id"].Value, "семитысячное");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячное");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячное");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячное");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячное");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячное");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячное");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячное");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячное");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячное");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячное");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячное");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячное");
            Assert.AreEqual(table[56]["id"].Value, "миллионное");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионное");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионное");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионное");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионное");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионное");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионное");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионное");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионное");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионное");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионное");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионное");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионное");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионное");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионное");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионное");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионное");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионное");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионное");
            Assert.AreEqual(table[75]["id"].Value, "миллиардное");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардное");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардное");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардное");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардное");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардное");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардное");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардное");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардное");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардное");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардное");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардное");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардное");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардное");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардное");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардное");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардное");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардное");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардное");
            Assert.AreEqual(table[94]["id"].Value, "триллионное");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионное");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионное");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионное");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионное");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионное");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионное");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионное");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионное");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионное");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионное");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионное");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионное");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионное");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионное");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионное");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионное");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионное");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионное");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest22()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Instrumental, Sex.Neuter, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевым");
            Assert.AreEqual(table[1]["id"].Value, "первым");
            Assert.AreEqual(table[2]["id"].Value, "вторым");
            Assert.AreEqual(table[3]["id"].Value, "третьим");
            Assert.AreEqual(table[4]["id"].Value, "четвертым");
            Assert.AreEqual(table[5]["id"].Value, "пятым");
            Assert.AreEqual(table[6]["id"].Value, "шестым");
            Assert.AreEqual(table[7]["id"].Value, "седьмым");
            Assert.AreEqual(table[8]["id"].Value, "восьмым");
            Assert.AreEqual(table[9]["id"].Value, "девятым");
            Assert.AreEqual(table[10]["id"].Value, "десятым");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатым");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатым");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатым");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатым");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатым");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатым");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатым");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатым");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатым");
            Assert.AreEqual(table[20]["id"].Value, "двадцатым");
            Assert.AreEqual(table[21]["id"].Value, "тридцатым");
            Assert.AreEqual(table[22]["id"].Value, "сороковым");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятым");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятым");
            Assert.AreEqual(table[25]["id"].Value, "семидесятым");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятым");
            Assert.AreEqual(table[27]["id"].Value, "девяностым");
            Assert.AreEqual(table[28]["id"].Value, "сотым");
            Assert.AreEqual(table[29]["id"].Value, "двухсотым");
            Assert.AreEqual(table[30]["id"].Value, "трехсотым");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотым");
            Assert.AreEqual(table[32]["id"].Value, "пятисотым");
            Assert.AreEqual(table[33]["id"].Value, "шестисотым");
            Assert.AreEqual(table[34]["id"].Value, "семисотым");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотым");
            Assert.AreEqual(table[36]["id"].Value, "девятисотым");
            Assert.AreEqual(table[37]["id"].Value, "тысячным");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячным");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячным");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячным");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячным");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячным");
            Assert.AreEqual(table[43]["id"].Value, "семитысячным");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячным");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячным");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячным");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячным");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячным");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячным");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячным");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячным");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячным");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячным");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячным");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячным");
            Assert.AreEqual(table[56]["id"].Value, "миллионным");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионным");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионным");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионным");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионным");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионным");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионным");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионным");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионным");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионным");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионным");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионным");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионным");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионным");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионным");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионным");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионным");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионным");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионным");
            Assert.AreEqual(table[75]["id"].Value, "миллиардным");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардным");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардным");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардным");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардным");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардным");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардным");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардным");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардным");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардным");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардным");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардным");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардным");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардным");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардным");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардным");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардным");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардным");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардным");
            Assert.AreEqual(table[94]["id"].Value, "триллионным");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионным");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионным");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионным");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионным");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионным");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионным");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионным");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионным");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионным");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионным");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионным");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионным");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионным");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионным");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионным");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионным");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионным");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионным");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest23()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 20; i < 100; i += 10)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 100; i < 1000; i += 100)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000; i < 20000; i += 1000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (int i = 1000000; i < 20000000; i += 1000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000; i < 20000000000; i += 1000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            for (long i = 1000000000000; i < 20000000000000; i += 1000000000000)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                table.Add(row);
            }
            plug.ConvertIntColToString(table, "id", TextCase.Prepositional, Sex.Neuter, false, true, out table);
            Assert.AreEqual(table.Count, 113);
            Assert.AreEqual(table.Columns.Count, 1);
            Assert.AreEqual(table[0]["id"].Value, "нулевом");
            Assert.AreEqual(table[1]["id"].Value, "первом");
            Assert.AreEqual(table[2]["id"].Value, "втором");
            Assert.AreEqual(table[3]["id"].Value, "третьем");
            Assert.AreEqual(table[4]["id"].Value, "четвертом");
            Assert.AreEqual(table[5]["id"].Value, "пятом");
            Assert.AreEqual(table[6]["id"].Value, "шестом");
            Assert.AreEqual(table[7]["id"].Value, "седьмом");
            Assert.AreEqual(table[8]["id"].Value, "восьмом");
            Assert.AreEqual(table[9]["id"].Value, "девятом");
            Assert.AreEqual(table[10]["id"].Value, "десятом");
            Assert.AreEqual(table[11]["id"].Value, "одиннадцатом");
            Assert.AreEqual(table[12]["id"].Value, "двенадцатом");
            Assert.AreEqual(table[13]["id"].Value, "тринадцатом");
            Assert.AreEqual(table[14]["id"].Value, "четырнадцатом");
            Assert.AreEqual(table[15]["id"].Value, "пятнадцатом");
            Assert.AreEqual(table[16]["id"].Value, "шестнадцатом");
            Assert.AreEqual(table[17]["id"].Value, "семнадцатом");
            Assert.AreEqual(table[18]["id"].Value, "восемнадцатом");
            Assert.AreEqual(table[19]["id"].Value, "девятнадцатом");
            Assert.AreEqual(table[20]["id"].Value, "двадцатом");
            Assert.AreEqual(table[21]["id"].Value, "тридцатом");
            Assert.AreEqual(table[22]["id"].Value, "сороковом");
            Assert.AreEqual(table[23]["id"].Value, "пятидесятом");
            Assert.AreEqual(table[24]["id"].Value, "шестидесятом");
            Assert.AreEqual(table[25]["id"].Value, "семидесятом");
            Assert.AreEqual(table[26]["id"].Value, "восьмидесятом");
            Assert.AreEqual(table[27]["id"].Value, "девяностом");
            Assert.AreEqual(table[28]["id"].Value, "сотом");
            Assert.AreEqual(table[29]["id"].Value, "двухсотом");
            Assert.AreEqual(table[30]["id"].Value, "трехсотом");
            Assert.AreEqual(table[31]["id"].Value, "четырехсотом");
            Assert.AreEqual(table[32]["id"].Value, "пятисотом");
            Assert.AreEqual(table[33]["id"].Value, "шестисотом");
            Assert.AreEqual(table[34]["id"].Value, "семисотом");
            Assert.AreEqual(table[35]["id"].Value, "восьмисотом");
            Assert.AreEqual(table[36]["id"].Value, "девятисотом");
            Assert.AreEqual(table[37]["id"].Value, "тысячном");
            Assert.AreEqual(table[38]["id"].Value, "двухтысячном");
            Assert.AreEqual(table[39]["id"].Value, "трехтысячном");
            Assert.AreEqual(table[40]["id"].Value, "четырехтысячном");
            Assert.AreEqual(table[41]["id"].Value, "пятитысячном");
            Assert.AreEqual(table[42]["id"].Value, "шеститысячном");
            Assert.AreEqual(table[43]["id"].Value, "семитысячном");
            Assert.AreEqual(table[44]["id"].Value, "восьмитысячном");
            Assert.AreEqual(table[45]["id"].Value, "девятитысячном");
            Assert.AreEqual(table[46]["id"].Value, "десятитысячном");
            Assert.AreEqual(table[47]["id"].Value, "одиннадцатитысячном");
            Assert.AreEqual(table[48]["id"].Value, "двенадцатитысячном");
            Assert.AreEqual(table[49]["id"].Value, "тринадцатитысячном");
            Assert.AreEqual(table[50]["id"].Value, "четырнадцатитысячном");
            Assert.AreEqual(table[51]["id"].Value, "пятнадцатитысячном");
            Assert.AreEqual(table[52]["id"].Value, "шестнадцатитысячном");
            Assert.AreEqual(table[53]["id"].Value, "семнадцатитысячном");
            Assert.AreEqual(table[54]["id"].Value, "восемнадцатитысячном");
            Assert.AreEqual(table[55]["id"].Value, "девятнадцатитысячном");
            Assert.AreEqual(table[56]["id"].Value, "миллионном");
            Assert.AreEqual(table[57]["id"].Value, "двухмиллионном");
            Assert.AreEqual(table[58]["id"].Value, "трехмиллионном");
            Assert.AreEqual(table[59]["id"].Value, "четырехмиллионном");
            Assert.AreEqual(table[60]["id"].Value, "пятимиллионном");
            Assert.AreEqual(table[61]["id"].Value, "шестимиллионном");
            Assert.AreEqual(table[62]["id"].Value, "семимиллионном");
            Assert.AreEqual(table[63]["id"].Value, "восьмимиллионном");
            Assert.AreEqual(table[64]["id"].Value, "девятимиллионном");
            Assert.AreEqual(table[65]["id"].Value, "десятимиллионном");
            Assert.AreEqual(table[66]["id"].Value, "одиннадцатимиллионном");
            Assert.AreEqual(table[67]["id"].Value, "двенадцатимиллионном");
            Assert.AreEqual(table[68]["id"].Value, "тринадцатимиллионном");
            Assert.AreEqual(table[69]["id"].Value, "четырнадцатимиллионном");
            Assert.AreEqual(table[70]["id"].Value, "пятнадцатимиллионном");
            Assert.AreEqual(table[71]["id"].Value, "шестнадцатимиллионном");
            Assert.AreEqual(table[72]["id"].Value, "семнадцатимиллионном");
            Assert.AreEqual(table[73]["id"].Value, "восемнадцатимиллионном");
            Assert.AreEqual(table[74]["id"].Value, "девятнадцатимиллионном");
            Assert.AreEqual(table[75]["id"].Value, "миллиардном");
            Assert.AreEqual(table[76]["id"].Value, "двухмиллиардном");
            Assert.AreEqual(table[77]["id"].Value, "трехмиллиардном");
            Assert.AreEqual(table[78]["id"].Value, "четырехмиллиардном");
            Assert.AreEqual(table[79]["id"].Value, "пятимиллиардном");
            Assert.AreEqual(table[80]["id"].Value, "шестимиллиардном");
            Assert.AreEqual(table[81]["id"].Value, "семимиллиардном");
            Assert.AreEqual(table[82]["id"].Value, "восьмимиллиардном");
            Assert.AreEqual(table[83]["id"].Value, "девятимиллиардном");
            Assert.AreEqual(table[84]["id"].Value, "десятимиллиардном");
            Assert.AreEqual(table[85]["id"].Value, "одиннадцатимиллиардном");
            Assert.AreEqual(table[86]["id"].Value, "двенадцатимиллиардном");
            Assert.AreEqual(table[87]["id"].Value, "тринадцатимиллиардном");
            Assert.AreEqual(table[88]["id"].Value, "четырнадцатимиллиардном");
            Assert.AreEqual(table[89]["id"].Value, "пятнадцатимиллиардном");
            Assert.AreEqual(table[90]["id"].Value, "шестнадцатимиллиардном");
            Assert.AreEqual(table[91]["id"].Value, "семнадцатимиллиардном");
            Assert.AreEqual(table[92]["id"].Value, "восемнадцатимиллиардном");
            Assert.AreEqual(table[93]["id"].Value, "девятнадцатимиллиардном");
            Assert.AreEqual(table[94]["id"].Value, "триллионном");
            Assert.AreEqual(table[95]["id"].Value, "двухтриллионном");
            Assert.AreEqual(table[96]["id"].Value, "трехтриллионном");
            Assert.AreEqual(table[97]["id"].Value, "четырехтриллионном");
            Assert.AreEqual(table[98]["id"].Value, "пятитриллионном");
            Assert.AreEqual(table[99]["id"].Value, "шеститриллионном");
            Assert.AreEqual(table[100]["id"].Value, "семитриллионном");
            Assert.AreEqual(table[101]["id"].Value, "восьмитриллионном");
            Assert.AreEqual(table[102]["id"].Value, "девятитриллионном");
            Assert.AreEqual(table[103]["id"].Value, "десятитриллионном");
            Assert.AreEqual(table[104]["id"].Value, "одиннадцатитриллионном");
            Assert.AreEqual(table[105]["id"].Value, "двенадцатитриллионном");
            Assert.AreEqual(table[106]["id"].Value, "тринадцатитриллионном");
            Assert.AreEqual(table[107]["id"].Value, "четырнадцатитриллионном");
            Assert.AreEqual(table[108]["id"].Value, "пятнадцатитриллионном");
            Assert.AreEqual(table[109]["id"].Value, "шестнадцатитриллионном");
            Assert.AreEqual(table[110]["id"].Value, "семнадцатитриллионном");
            Assert.AreEqual(table[111]["id"].Value, "восемнадцатитриллионном");
            Assert.AreEqual(table[112]["id"].Value, "девятнадцатитриллионном");
        }
    
        [TestMethod]
        public void ConvertModuleConvertIntTest24()
        {
            ReportTable table = "[{id:-1},{id:" + long.MaxValue + "},{id:" + long.MinValue + "},{id:" + 123456789987654 + "},{id:" + 123000789000654 + "}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertIntColToString(table, "id", TextCase.Nominative, Sex.Male, false, false, out table);
            Assert.AreEqual(table[0]["id"].Value, "минус один");
            Assert.AreEqual(table[1]["id"].Value, long.MaxValue.ToString());
            Assert.AreEqual(table[2]["id"].Value, long.MinValue.ToString());
            Assert.AreEqual(table[3]["id"].Value, "сто двадцать три триллиона четыреста пятьдесят шесть миллиардов семьсот восемьдесят девять миллионов девятьсот восемьдесят семь тысяч шестьсот пятьдесят четыре");
            Assert.AreEqual(table[4]["id"].Value, "сто двадцать три триллиона семьсот восемьдесят девять миллионов шестьсот пятьдесят четыре");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest25()
        {
            ReportTable table = "[{id:-1}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertIntColToString(table, "id", TextCase.Nominative, Sex.Male, true, false, out table);
            Assert.AreEqual(table[0]["id"].Value, "Минус один");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTest26()
        {
            ReportTable table = "[{\"id\":\"-1\"},{\"id\":\"" + long.MaxValue + "\"},{\"id\":\"" + long.MinValue + "\"},{\"id\":\"" + 123456789987654 + "\"},{\"id\":\"" + 123000789000654 + "\"}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertIntColToString(table, "id", TextCase.Nominative, Sex.Male, false, true, out table);
            Assert.AreEqual(table[0]["id"].Value, "минус первый");
            Assert.AreEqual(table[1]["id"].Value, long.MaxValue.ToString());
            Assert.AreEqual(table[2]["id"].Value, long.MinValue.ToString());
            Assert.AreEqual(table[3]["id"].Value, "сто двадцать три триллиона четыреста пятьдесят шесть миллиардов семьсот восемьдесят девять миллионов девятьсот восемьдесят семь тысяч шестьсот пятьдесят четвертый");
            Assert.AreEqual(table[4]["id"].Value, "сто двадцать три триллиона семьсот восемьдесят девять миллионов шестьсот пятьдесят четвертый");
        }

        [TestMethod]
        public void ConvertModuleConvertIntTestIncorrectTable()
        {
            ReportTable table = null;
            ConvertPlug plug = new ConvertPlug();
            try
            {
                plug.ConvertIntColToString(table, "id", TextCase.Nominative, Sex.Male, false, true, out table);
                Assert.Fail();
            } catch (ConvertException)
            {
            }
        }

        [TestMethod]
        public void ConvertModuleConvertIntTestIncorrectRow()
        {
            ConvertPlug plug = new ConvertPlug();
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            for (int i = 0; i < 20; i++)
            {
                ReportRow row = null;
                if (i != 3)
                {
                    row = new ReportRow(table);
                    row.Add(new ReportCell(row, i.ToString()));
                }
                table.Add(row);
            }
            try
            {
                plug.ConvertIntColToString(table, "id", TextCase.Nominative, Sex.Male, false, true, out table);
                Assert.Fail();
            }
            catch (ConvertException)
            {
            }
        }

        [TestMethod]
        public void ConvertModuleConvertIntTestIncorrectRowValue()
        {
            ReportTable table = "[{\"id\":\"Hello\"}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertIntColToString(table, "id", TextCase.Nominative, Sex.Male, false, true, out table);
            Assert.AreEqual(table[0]["id"].Value, "Hello");
        }

        [TestMethod]
        public void ConvertModuleConvertFloatTest1()
        {
            ReportTable table = "[{\"id\":\"-1,11\"},{\"id\":\"" + decimal.MaxValue + "\"},{\"id\":\"" + decimal.MinValue + "\"},{\"id\":\"7654,321012345\"},{\"id\":\"123456789000654,00012345\"},{\"id\":\"0,012\"},{\"id\":\"1,001012\"},{\"id\":\"21\"},{\"id\":\"0\"},{\"id\":\"0,00\"}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertFloatColToString(table, "id", TextCase.Nominative, false, out table);
            Assert.AreEqual(table[0]["id"].Value, "минус одна целая одиннадцать сотых");
            Assert.AreEqual(table[1]["id"].Value, decimal.MaxValue.ToString());
            Assert.AreEqual(table[2]["id"].Value, decimal.MinValue.ToString());
            Assert.AreEqual(table[3]["id"].Value, "семь тысяч шестьсот пятьдесят четыре целых триста двадцать один миллион двенадцать тысяч триста сорок пять миллиардных");
            Assert.AreEqual(table[4]["id"].Value, "сто двадцать три триллиона четыреста пятьдесят шесть миллиардов семьсот восемьдесят девять миллионов шестьсот пятьдесят четыре целых двенадцать тысяч триста сорок пять стомиллионных");
            Assert.AreEqual(table[5]["id"].Value, "ноль целых двенадцать тысячных");
            Assert.AreEqual(table[6]["id"].Value, "одна целая одна тысяча двенадцать миллионных");
            Assert.AreEqual(table[7]["id"].Value, "двадцать одна целая ноль десятых");
            Assert.AreEqual(table[8]["id"].Value, "ноль целых ноль десятых");
            Assert.AreEqual(table[9]["id"].Value, "ноль целых ноль десятых");
        }

        [TestMethod]
        public void ConvertModuleConvertFloatTest2()
        {
            ReportTable table = "[{\"id\":\"-1,11\"},{\"id\":\"" + decimal.MaxValue + "\"},{\"id\":\"" + decimal.MinValue + "\"},{\"id\":\"7654,321012345\"},{\"id\":\"123456789000654,00012345\"},{\"id\":\"0,012\"},{\"id\":\"1,001012\"},{\"id\":\"21\"},{\"id\":\"0\"},{\"id\":\"0,00\"}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertFloatColToString(table, "id", TextCase.Genitive, true, out table);
            Assert.AreEqual(table[0]["id"].Value, "Минус одной целой одиннадцати сотых");
            Assert.AreEqual(table[1]["id"].Value, decimal.MaxValue.ToString());
            Assert.AreEqual(table[2]["id"].Value, decimal.MinValue.ToString());
            Assert.AreEqual(table[3]["id"].Value, "Семи тысяч шестисот пятидесяти четырех целых трехсот двадцати одного миллиона двенадцати тысяч трехсот сорока пяти миллиардных");
            Assert.AreEqual(table[4]["id"].Value, "Ста двадцати трех триллионов четырехсот пятидесяти шести миллиардов семисот восьмидесяти девяти миллионов шестисот пятидесяти четырех целых двенадцати тысяч трехсот сорока пяти стомиллионных");
            Assert.AreEqual(table[5]["id"].Value, "Ноля целых двенадцати тысячных");
            Assert.AreEqual(table[6]["id"].Value, "Одной целой одной тысячи двенадцати миллионных");
            Assert.AreEqual(table[7]["id"].Value, "Двадцати одной целой ноля десятых");
            Assert.AreEqual(table[8]["id"].Value, "Ноля целых ноля десятых");
            Assert.AreEqual(table[9]["id"].Value, "Ноля целых ноля десятых");
        }

        [TestMethod]
        public void ConvertModuleConvertFloatTest3()
        {
            ReportTable table = "[{\"id\":\"-1,11\"},{\"id\":\"" + decimal.MaxValue + "\"},{\"id\":\"" + decimal.MinValue + "\"},{\"id\":\"7654,321012345\"},{\"id\":\"123456789000654,00012345\"},{\"id\":\"0,012\"},{\"id\":\"1,001012\"},{\"id\":\"21\"},{\"id\":\"0\"},{\"id\":\"0,00\"}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertFloatColToString(table, "id", TextCase.Accusative, false, out table);
            Assert.AreEqual(table[0]["id"].Value, "минус одну целую одиннадцать сотых");
            Assert.AreEqual(table[1]["id"].Value, decimal.MaxValue.ToString());
            Assert.AreEqual(table[2]["id"].Value, decimal.MinValue.ToString());
            Assert.AreEqual(table[3]["id"].Value, "семь тысяч шестьсот пятьдесят четыре целых триста двадцать один миллион двенадцать тысяч триста сорок пять миллиардных");
            Assert.AreEqual(table[4]["id"].Value, "сто двадцать три триллиона четыреста пятьдесят шесть миллиардов семьсот восемьдесят девять миллионов шестьсот пятьдесят четыре целых двенадцать тысяч триста сорок пять стомиллионных");
            Assert.AreEqual(table[5]["id"].Value, "ноль целых двенадцать тысячных");
            Assert.AreEqual(table[6]["id"].Value, "одну целую одну тысячу двенадцать миллионных");
            Assert.AreEqual(table[7]["id"].Value, "двадцать одну целую ноль десятых");
            Assert.AreEqual(table[8]["id"].Value, "ноль целых ноль десятых");
            Assert.AreEqual(table[9]["id"].Value, "ноль целых ноль десятых");
        }

        [TestMethod]
        public void ConvertModuleConvertFloatTest4()
        {
            ReportTable table = "[{\"id\":\"-1,11\"},{\"id\":\"" + decimal.MaxValue + "\"},{\"id\":\"" + decimal.MinValue + "\"},{\"id\":\"7654,321012345\"},{\"id\":\"123456789000654,00012345\"},{\"id\":\"0,012\"},{\"id\":\"1,001012\"},{\"id\":\"21\"},{\"id\":\"0\"},{\"id\":\"0,00\"}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertFloatColToString(table, "id", TextCase.Dative, false, out table);
            Assert.AreEqual(table[0]["id"].Value, "минус одной целой одиннадцати сотым");
            Assert.AreEqual(table[1]["id"].Value, decimal.MaxValue.ToString());
            Assert.AreEqual(table[2]["id"].Value, decimal.MinValue.ToString());
            Assert.AreEqual(table[3]["id"].Value, "семи тысячам шестистам пятидесяти четырем целым тремстам двадцати одному миллиону двенадцати тысячам тремстам сорока пяти миллиардным");
            Assert.AreEqual(table[4]["id"].Value, "ста двадцати трем триллионам четыремстам пятидесяти шести миллиардам семистам восьмидесяти девяти миллионам шестистам пятидесяти четырем целым двенадцати тысячам тремстам сорока пяти стомиллионным");
            Assert.AreEqual(table[5]["id"].Value, "нолю целых двенадцати тысячным");
            Assert.AreEqual(table[6]["id"].Value, "одной целой одной тысяче двенадцати миллионным");
            Assert.AreEqual(table[7]["id"].Value, "двадцати одной целой нолю десятых");
            Assert.AreEqual(table[8]["id"].Value, "нолю целых нолю десятых");
            Assert.AreEqual(table[9]["id"].Value, "нолю целых нолю десятых");
        }

        [TestMethod]
        public void ConvertModuleConvertFloatTest5()
        {
            ReportTable table = "[{\"id\":\"-1,11\"},{\"id\":\"" + decimal.MaxValue + "\"},{\"id\":\"" + decimal.MinValue + "\"},{\"id\":\"7654,321012345\"},{\"id\":\"123456789000654,00012345\"},{\"id\":\"0,012\"},{\"id\":\"1,001012\"},{\"id\":\"21\"},{\"id\":\"0\"},{\"id\":\"0,00\"}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertFloatColToString(table, "id", TextCase.Prepositional, false, out table);
            Assert.AreEqual(table[0]["id"].Value, "минус одной целой одиннадцати сотых");
            Assert.AreEqual(table[1]["id"].Value, decimal.MaxValue.ToString());
            Assert.AreEqual(table[2]["id"].Value, decimal.MinValue.ToString());
            Assert.AreEqual(table[3]["id"].Value, "семи тысячах шестистах пятидесяти четырех целых трехстах двадцати одном миллионе двенадцати тысячах трехстах сорока пяти миллиардных");
            Assert.AreEqual(table[4]["id"].Value, "ста двадцати трех триллионах четырехстах пятидесяти шести миллиардах семистах восьмидесяти девяти миллионах шестистах пятидесяти четырех целых двенадцати тысячах трехстах сорока пяти стомиллионных");
            Assert.AreEqual(table[5]["id"].Value, "ноле целых двенадцати тысячных");
            Assert.AreEqual(table[6]["id"].Value, "одной целой одной тысяче двенадцати миллионных");
            Assert.AreEqual(table[7]["id"].Value, "двадцати одной целой ноле десятых");
            Assert.AreEqual(table[8]["id"].Value, "ноле целых ноле десятых");
            Assert.AreEqual(table[9]["id"].Value, "ноле целых ноле десятых");
        }

        [TestMethod]
        public void ConvertModuleConvertFloatTest6()
        {
            ReportTable table = "[{\"id\":\"-1,11\"},{\"id\":\"" + decimal.MaxValue + "\"},{\"id\":\"" + decimal.MinValue + "\"},{\"id\":\"7654,321012345\"},{\"id\":\"123456789000654,00012345\"},{\"id\":\"0,012\"},{\"id\":\"1,001012\"},{\"id\":\"21\"},{\"id\":\"0\"},{\"id\":\"0,00\"}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertFloatColToString(table, "id", TextCase.Instrumental, false, out table);
            Assert.AreEqual(table[0]["id"].Value, "минус одной целой одиннадцатью сотыми");
            Assert.AreEqual(table[1]["id"].Value, decimal.MaxValue.ToString());
            Assert.AreEqual(table[2]["id"].Value, decimal.MinValue.ToString());
            Assert.AreEqual(table[3]["id"].Value, "семью тысячами шестьюстами пятьюдесятью четыремя целыми тремястами двадцатью одним миллионом двенадцатью тысячами тремястами сорока пятью миллиардными");
            Assert.AreEqual(table[4]["id"].Value, "ста двадцатью тремя триллионами четырьмястами пятьюдесятью шестью миллиардами семьюстами восьмьюдесятью девятью миллионами шестьюстами пятьюдесятью четыремя целыми двенадцатью тысячами тремястами сорока пятью стомиллионными");
            Assert.AreEqual(table[5]["id"].Value, "нолем целых двенадцатью тысячными");
            Assert.AreEqual(table[6]["id"].Value, "одной целой одной тысячей двенадцатью миллионными");
            Assert.AreEqual(table[7]["id"].Value, "двадцатью одной целой нолем десятых");
            Assert.AreEqual(table[8]["id"].Value, "нолем целых нолем десятых");
            Assert.AreEqual(table[9]["id"].Value, "нолем целых нолем десятых");
        }

        [TestMethod]
        public void ConvertModuleConvertCurrencyTest1()
        {
            ReportTable table = "[{\"id\":\"-1,11\"},{\"id\":\"" + decimal.MaxValue + "\"},{\"id\":\"" + decimal.MinValue + "\"},{\"id\":\"7654,321012345\"},{\"id\":\"123456789000654,00012345\"},{\"id\":\"0,012\"},{\"id\":\"1,001012\"},{\"id\":\"21\"},{\"id\":\"0\"},{\"id\":\"0,00\"}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertCurrencyColToString(table, "id", CurrencyType.Dollar, "nii,ff (nniin rn ffn kn)", " ", true, false, out table);
            Assert.AreEqual(table[0]["id"].Value, "-1,11 (минус один доллар одиннадцать центов)");
            Assert.AreEqual(table[1]["id"].Value, decimal.MaxValue.ToString());
            Assert.AreEqual(table[2]["id"].Value, decimal.MinValue.ToString());
            Assert.AreEqual(table[3]["id"].Value, "7 654,32 (семь тысяч шестьсот пятьдесят четыре доллара тридцать два цента)");
            Assert.AreEqual(table[4]["id"].Value, "123 456 789 000 654,00 (сто двадцать три триллиона четыреста пятьдесят шесть миллиардов семьсот восемьдесят девять миллионов шестьсот пятьдесят четыре доллара ноль центов)");
            Assert.AreEqual(table[5]["id"].Value, "0,01 (ноль долларов один цент)");
            Assert.AreEqual(table[6]["id"].Value, "1,00 (один доллар ноль центов)");
            Assert.AreEqual(table[7]["id"].Value, "21,00 (двадцать один доллар ноль центов)");
            Assert.AreEqual(table[8]["id"].Value, "0,00 (ноль долларов ноль центов)");
            Assert.AreEqual(table[9]["id"].Value, "0,00 (ноль долларов ноль центов)");
        }

        [TestMethod]
        public void ConvertModuleConvertCurrencyTest2()
        {
            ReportTable table = "[{\"id\":\"-1,11\"},{\"id\":\"" + decimal.MaxValue + "\"},{\"id\":\"" + decimal.MinValue + "\"},{\"id\":\"7654,321012345\"},{\"id\":\"123456789000654,00012345\"},{\"id\":\"0,012\"},{\"id\":\"1,001012\"},{\"id\":\"21\"},{\"id\":\"0\"},{\"id\":\"0,00\"}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertCurrencyColToString(table, "id", CurrencyType.Ruble, "nii,ff (nniin rn ffn kn)", " ", true, false, out table);
            Assert.AreEqual(table[0]["id"].Value, "-1,11 (минус один рубль одиннадцать копеек)");
            Assert.AreEqual(table[1]["id"].Value, decimal.MaxValue.ToString());
            Assert.AreEqual(table[2]["id"].Value, decimal.MinValue.ToString());
            Assert.AreEqual(table[3]["id"].Value, "7 654,32 (семь тысяч шестьсот пятьдесят четыре рубля тридцать две копейки)");
            Assert.AreEqual(table[4]["id"].Value, "123 456 789 000 654,00 (сто двадцать три триллиона четыреста пятьдесят шесть миллиардов семьсот восемьдесят девять миллионов шестьсот пятьдесят четыре рубля ноль копеек)");
            Assert.AreEqual(table[5]["id"].Value, "0,01 (ноль рублей одна копейка)");
            Assert.AreEqual(table[6]["id"].Value, "1,00 (один рубль ноль копеек)");
            Assert.AreEqual(table[7]["id"].Value, "21,00 (двадцать один рубль ноль копеек)");
            Assert.AreEqual(table[8]["id"].Value, "0,00 (ноль рублей ноль копеек)");
            Assert.AreEqual(table[9]["id"].Value, "0,00 (ноль рублей ноль копеек)");
        }

        [TestMethod]
        public void ConvertModuleConvertCurrencyTest3()
        {
            ReportTable table = "[{\"id\":\"-1,11\"},{\"id\":\"" + decimal.MaxValue + "\"},{\"id\":\"" + decimal.MinValue + "\"},{\"id\":\"7654,321012345\"},{\"id\":\"123456789000654,00012345\"},{\"id\":\"0,012\"},{\"id\":\"1,001012\"},{\"id\":\"21\"},{\"id\":\"0\"},{\"id\":\"0,00\"}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertCurrencyColToString(table, "id", CurrencyType.Euro, "nii,ff (nniin rn ffn kn)", " ", true, false, out table);
            Assert.AreEqual(table[0]["id"].Value, "-1,11 (минус один евро одиннадцать центов)");
            Assert.AreEqual(table[1]["id"].Value, decimal.MaxValue.ToString());
            Assert.AreEqual(table[2]["id"].Value, decimal.MinValue.ToString());
            Assert.AreEqual(table[3]["id"].Value, "7 654,32 (семь тысяч шестьсот пятьдесят четыре евро тридцать два цента)");
            Assert.AreEqual(table[4]["id"].Value, "123 456 789 000 654,00 (сто двадцать три триллиона четыреста пятьдесят шесть миллиардов семьсот восемьдесят девять миллионов шестьсот пятьдесят четыре евро ноль центов)");
            Assert.AreEqual(table[5]["id"].Value, "0,01 (ноль евро один цент)");
            Assert.AreEqual(table[6]["id"].Value, "1,00 (один евро ноль центов)");
            Assert.AreEqual(table[7]["id"].Value, "21,00 (двадцать один евро ноль центов)");
            Assert.AreEqual(table[8]["id"].Value, "0,00 (ноль евро ноль центов)");
            Assert.AreEqual(table[9]["id"].Value, "0,00 (ноль евро ноль центов)");
        }

        [TestMethod]
        public void ConvertModuleConvertDateTimeTest1()
        {
            ReportTable table = "[{\"id\":\"" + DateTime.MaxValue + "\"},{\"id\":\"" + DateTime.MinValue + "\"},{\"id\":\"26.06.1988 13:47:56\"},{\"id\":\"1988-06-26 13:47:56\"},{\"id\":\"1988-06-26\"},{\"id\":\"26.06.1988\"},{\"id\":\"кирпич\"}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertDateTimeColToString(table, "id", "dd.MM.yyyy (yy yyn) - HH:mm:ss - ddn MMg yyyyg HHn mmn ssn hhn", true, out table);
            Assert.AreEqual(table[0]["id"].Value, "31.12.9999 (99 девяносто девятый год) - 23:59:59 - тридцать первое декабря девять тысяч девятьсот девяносто девятого года двадцать три часа пятьдесят девять минут пятьдесят девять секунд одиннадцать часов");
            Assert.AreEqual(table[1]["id"].Value, "01.01.0001 (01 первый год) - 00:00:00 - первое января первого года ноль часов ноль минут ноль секунд двенадцать часов");
            Assert.AreEqual(table[2]["id"].Value, "26.06.1988 (88 восемьдесят восьмой год) - 13:47:56 - двадцать шестое июня одна тысяча девятьсот восемьдесят восьмого года тринадцать часов сорок семь минут пятьдесят шесть секунд один час");
            Assert.AreEqual(table[3]["id"].Value, "26.06.1988 (88 восемьдесят восьмой год) - 13:47:56 - двадцать шестое июня одна тысяча девятьсот восемьдесят восьмого года тринадцать часов сорок семь минут пятьдесят шесть секунд один час");
            Assert.AreEqual(table[4]["id"].Value, "26.06.1988 (88 восемьдесят восьмой год) - 00:00:00 - двадцать шестое июня одна тысяча девятьсот восемьдесят восьмого года ноль часов ноль минут ноль секунд двенадцать часов");
            Assert.AreEqual(table[5]["id"].Value, "26.06.1988 (88 восемьдесят восьмой год) - 00:00:00 - двадцать шестое июня одна тысяча девятьсот восемьдесят восьмого года ноль часов ноль минут ноль секунд двенадцать часов");
            Assert.AreEqual(table[6]["id"].Value, "кирпич");
        }

        [TestMethod]
        public void ConvertModuleConvertNameTest1()
        {
            ReportTable table = "[{\"id\":\"Азаров Илья Сергеевич\"},{\"id\":\"Алехин Владимир Сергеевич\"},{\"id\":\"Бабайцев Алексей Сергеевич\"},{\"id\":\"Вощечков Александр Александрович\"},{\"id\":\"Гаина Виктор Олегович\"},{\"id\":\"Эльтеков Сергей Валерьевич\"},{\"id\":\"Игнатов Василий Васильевич\"},{\"id\":\"кирпич\"}]";
            ConvertPlug plug = new ConvertPlug();
            plug.ConvertNameColToCase(table, "id", "ss nn pp s n p", TextCase.Genitive, out table);
            Assert.AreEqual(table[0]["id"].Value, "Азарова Ильи Сергеевича А И С");
            Assert.AreEqual(table[1]["id"].Value, "Алехина Владимира Сергеевича А В С");
            Assert.AreEqual(table[2]["id"].Value, "Бабайцева Алексея Сергеевича Б А С");
            Assert.AreEqual(table[3]["id"].Value, "Вощечкова Александра Александровича В А А");
            Assert.AreEqual(table[4]["id"].Value, "Гаины Виктора Олеговича Г В О");
            Assert.AreEqual(table[5]["id"].Value, "Эльтекова Сергея Валерьевича Э С В");
            Assert.AreEqual(table[6]["id"].Value, "Игнатова Василия Васильевича И В В");
            Assert.AreEqual(table[7]["id"].Value, "кирпич");
        }

        [TestMethod]
        public void ConvertModuleConcatTest1()
        {
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            table.Columns.Add("name");
            table.Columns.Add("id2");
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, null));
                row.Add(new ReportCell(row, (10-i).ToString()));
                table.Add(row);
            }
            ConvertPlug plug = new ConvertPlug();
            string result;
            plug.TableConcat(table, "|", ";", out result);
            Assert.AreEqual(result,"0;;10|1;;9|2;;8|3;;7|4;;6|5;;5|6;;4|7;;3|8;;2|9;;1");
        }

        [TestMethod]
        public void ConvertModuleConcatTest2()
        {
            ReportRow row = new ReportRow(null);
            row.Add(new ReportCell(row, "Hell".ToString()));
            row.Add(new ReportCell(row, null));
            row.Add(new ReportCell(row, "o".ToString()));
            ConvertPlug plug = new ConvertPlug();
            string result;
            plug.RowConcat(row, "", out result);
            Assert.AreEqual(result, "Hello");
        }

        [TestMethod]
        public void ConvertModuleConcatTest3()
        {
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            table.Columns.Add("name");
            table.Columns.Add("id2");
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, null));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                table.Add(row);
            }
            ConvertPlug plug = new ConvertPlug();
            string result;
            plug.ColumnConcat(table, "id", ";", out result);
            Assert.AreEqual(result, "0;1;2;3;4;5;6;7;8;9");
        }

        [TestMethod]
        public void ConvertModuleGetTest1()
        {
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            table.Columns.Add("name");
            table.Columns.Add("id2");
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, null));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                table.Add(row);
            }
            ConvertPlug plug = new ConvertPlug();
            object result;
            plug.GetCell(table, 0, "name", out result);
            Assert.AreEqual(result, null);
            plug.GetCell(table, 1, "id2", out result);
            Assert.AreEqual(result, "9");
        }

        [TestMethod]
        public void ConvertModuleGetTest2()
        {
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            table.Columns.Add("name");
            table.Columns.Add("id2");
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, null));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                table.Add(row);
            }
            ConvertPlug plug = new ConvertPlug();
            ReportRow result;
            plug.GetRow(table, 1, out result);
            Assert.AreEqual(result, table[1]);
        }

        [TestMethod]
        public void ConvertModuleGetTest3()
        {
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            table.Columns.Add("name");
            table.Columns.Add("id2");
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, null));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                table.Add(row);
            }
            ConvertPlug plug = new ConvertPlug();
            ReportRow result;
            try
            {
                plug.GetRow(table, 10, out result);
                Assert.Fail();
            } catch (ConvertException)
            {
            }
        }

        [TestMethod]
        public void ConvertModuleGetTest4()
        {
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            table.Columns.Add("name");
            table.Columns.Add("id2");
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, null));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                table.Add(row);
            }
            ConvertPlug plug = new ConvertPlug();
            object result;
            try
            {
                plug.GetCell(table, 80, "name", out result);
                Assert.Fail();
            } catch (ConvertException)
            {
            }
        }

        [TestMethod]
        public void ConvertModuleGetTes5()
        {
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            table.Columns.Add("name");
            table.Columns.Add("id2");
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, null));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                table.Add(row);
            }
            ConvertPlug plug = new ConvertPlug();
            object result;
            try
            {
                plug.GetCell(table, 1, "name2", out result);
                Assert.Fail();
            }
            catch (ConvertException)
            {
            }
        }

        [TestMethod]
        public void ConvertModuleGetTes6()
        {
            ReportTable table = new ReportTable();
            table.Columns.Add("id");
            table.Columns.Add("name");
            table.Columns.Add("id");
            for (int i = 0; i < 10; i++)
            {
                ReportRow row = new ReportRow(table);
                row.Add(new ReportCell(row, i.ToString()));
                row.Add(new ReportCell(row, null));
                row.Add(new ReportCell(row, (10 - i).ToString()));
                table.Add(row);
            }
            ConvertPlug plug = new ConvertPlug();
            object result;
            plug.GetCell(table, 1, "id", out result);
            Assert.AreEqual(result, "1");
        }
    }
}
