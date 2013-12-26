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
    internal partial class FormStringValue : Form
    {
        public string Value
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public FormStringValue(Language language)
        {
            InitializeComponent();
            button1.Text = language.Translate(button1.Text);
            button2.Text = language.Translate(button2.Text);
            label1.Text = language.Translate(label1.Text);
        }
    }
}
