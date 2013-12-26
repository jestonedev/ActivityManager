using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AMClasses;

namespace AmEditor
{
    internal partial class FormSqlValue : Form
    {
        public string Value
        {
            get { return scintilla1.Text; }
            set { scintilla1.Text = value; }
        }

        public FormSqlValue(List<string> globalVariables, Language language)
        {
            InitializeComponent();
            foreach (string GlobalVariable in globalVariables)
                comboBoxValues.Items.Add(GlobalVariable);
            button1.Text = language.Translate(button1.Text);
            button2.Text = language.Translate(button2.Text);
            button4.Text = language.Translate(button4.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            scintilla1.Selection.Text = comboBoxValues.Text;
        }
    }
}
