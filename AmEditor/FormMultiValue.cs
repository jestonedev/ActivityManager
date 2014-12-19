using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AMClasses;
using System.IO;

namespace AmEditor
{
    internal partial class FormMultiValue : Form
    {

        public string Value
        {
            get { return scintillaEditor.Text; }
            set { scintillaEditor.Text = value; }
        }

        public FormMultiValue(List<string> globalVariables, Language language)
        {
            InitializeComponent();
            foreach (string GlobalVariable in globalVariables)
                comboBoxValues.Items.Add(GlobalVariable);
            button1.Text = language.Translate("Сохранить");
            button2.Text = language.Translate("Отменить");
            button4.Text = language.Translate("Вставить");
            безПодсветкиToolStripMenuItem.Text = language.Translate("Без подсветки");
            отменитьToolStripMenuItem.Text = language.Translate("Отменить");
            повторитьToolStripMenuItem.Text = language.Translate("Повторить");
            копироватьToolStripMenuItem1.Text = language.Translate("Копировать");
            вырезатьToolStripMenuItem.Text = language.Translate("Вырезать");
            вставитьToolStripMenuItem.Text = language.Translate("Вставить");
            удалитьToolStripMenuItem.Text = language.Translate("Удалить");
            буферОбменаToolStripMenuItem.Text = language.Translate("Буфер обмена");
            путьДоПапкиToolStripMenuItem.Text = language.Translate("Путь до папки");
            путьДоФайлаToolStripMenuItem.Text = language.Translate("Путь до файла");
            синтаксисToolStripMenuItem.Text = language.Translate("Подсветка синтаксиса");
            setEditorLanguage(mSSQLToolStripMenuItem, "mssql");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            scintillaEditor.Selection.Text = comboBoxValues.Text;
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintillaEditor.UndoRedo.Undo();
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            scintillaEditor.UndoRedo.Redo();
        }

        private void копироватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(scintillaEditor.Selection.Text))
                Clipboard.SetText(scintillaEditor.Selection.Text);
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(scintillaEditor.Selection.Text))
                Clipboard.SetText(scintillaEditor.Selection.Text);
            scintillaEditor.Selection.Text = "";
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintillaEditor.Selection.Text = "";
        }

        private void буферОбменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintillaEditor.Selection.Text = Clipboard.GetText();
        }

        private void путьДоФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogInsert.ShowDialog() == DialogResult.OK)
                scintillaEditor.Selection.Text = openFileDialogInsert.FileName;
        }

        private void путьДоПапкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogInsert.ShowDialog() == DialogResult.OK)
                scintillaEditor.Selection.Text = folderBrowserDialogInsert.SelectedPath;
        }

        private void setEditorLanguage(object sender, string language)
        {
            ToolStripMenuItem current = sender as ToolStripMenuItem;
            ToolStripMenuItem parent = (ToolStripMenuItem)current.OwnerItem;
            foreach (var item in parent.DropDown.Items)
            {
                ToolStripMenuItem menu = item as ToolStripMenuItem;
                if (menu != null)
                    menu.Checked = false;
            }
            scintillaEditor.AutoComplete.ListString = "";
            scintillaEditor.ConfigurationManager.Language = language;
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, language+".synax")))
            {
                using (StreamReader sr = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, language + ".synax")))
                {
                    string autoComplete = sr.ReadToEnd();
                    autoComplete = autoComplete.Replace(Environment.NewLine, " ");
                    scintillaEditor.AutoComplete.ListString = autoComplete;
                }
            }
            current.Checked = true;
        }

        private void mSSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            setEditorLanguage(sender, "mssql");
        }

        private void javaScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setEditorLanguage(sender, "js");
        }

        private void безПодсветкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem current = ((ToolStripMenuItem)sender);
            ToolStripMenuItem parent = (ToolStripMenuItem)current.OwnerItem;
            foreach (var item in parent.DropDown.Items)
            {
                ToolStripMenuItem menu = item as ToolStripMenuItem;
                if (menu != null)
                    menu.Checked = false;
            }
            scintillaEditor.AutoComplete.ListString = "";
            scintillaEditor.ConfigurationManager.Language = "";
            current.Checked = true;
        }
    }
}
