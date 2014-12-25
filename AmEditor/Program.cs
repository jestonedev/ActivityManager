using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AmEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string fileName = null;
            for (int i = 0; i < args.Length; i++)
            {
                if (File.Exists(args[i]))
                {
                    FileInfo fi = new FileInfo(args[i]);
                    if (fi.Extension.ToUpper(CultureInfo.CurrentCulture) == ".XML")
                        fileName = args[i];
                }
            }
            Application.Run(new Editor(fileName));
        }
    }
}
