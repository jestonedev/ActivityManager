using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using am_classes;

namespace am_editor
{
    public partial class FormSqlValue : Form
    {
        public string value
        {
            get { return scintilla1.Text; }
            set { scintilla1.Text = value; }
        }

        public FormSqlValue(List<string> global_variables, Language language)
        {
            InitializeComponent();
            foreach (string global_variable in global_variables)
                comboBoxValues.Items.Add(global_variable);
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
