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
    internal partial class FormComboBoxValue : Form
    {
        public string value { get { return comboBoxValue.Text; }
            set { comboBoxValue.Text = value; }
        }

        public FormComboBoxValue(List<string> autocomplete_values, Language language)
        {
            InitializeComponent();
            foreach (string autocomplete_value in autocomplete_values)
                comboBoxValue.Items.Add(autocomplete_value);
            button1.Text = language.Translate(button1.Text);
            button2.Text = language.Translate(button2.Text);
            label2.Text = language.Translate(label2.Text);
        }

        private void comboBoxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DialogResult = DialogResult.OK;
        }

    }
}
