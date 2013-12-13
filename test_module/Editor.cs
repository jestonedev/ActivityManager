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
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
