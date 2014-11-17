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
    internal partial class FormLanguage : Form
    {
        public string config_language { 
            get { return comboBoxConfigLanguage.Text; }
            set { comboBoxConfigLanguage.Text = value; }
        }

        public string interface_language
        {
            get { return comboBoxInterfaceLanguage.Text; }
            set { comboBoxInterfaceLanguage.Text = value; }
        }

        public List<string> languages {
            set 
            {
                foreach (string item in value)
                {
                    comboBoxConfigLanguage.Items.Add(item);
                    comboBoxInterfaceLanguage.Items.Add(item);
                }
            }
        }

        private Language language;

        public FormLanguage(Language language)
        {
            InitializeComponent();
            this.language = language;
            Text = this.language.Translate(Text);
            label1.Text = this.language.Translate(label1.Text);
            label2.Text = this.language.Translate(label2.Text);
            button1.Text = this.language.Translate(button1.Text);
            button2.Text = this.language.Translate(button2.Text);
        }
    }
}
