using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using AMClasses;

namespace AmEditor
{
    internal partial class FormCommandLineParams : Form
    {
        private Language language { get; set; }

        public Dictionary<string, string> command_line_params { get {
            Dictionary<string, string> cl_params = new Dictionary<string, string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
                if ((!row.IsNewRow) && (row.Cells[0].Value != null) && (row.Cells[0].Value.ToString().Trim() != ""))
                    cl_params.Add(row.Cells[0].Value.ToString().Trim(), row.Cells[1].Value == null ? "" : row.Cells[1].Value.ToString().Trim());
            return cl_params;
        }
            set {
                foreach (string key in value.Keys)
                    dataGridView1.Rows.Add(new object[] { key, value[key] });
            }
        }

        public FormCommandLineParams(Language language)
        {
            InitializeComponent();
            this.language = language;
            Text = this.language.Translate(Text);
            button1.Text = this.language.Translate(button1.Text);
            button2.Text = this.language.Translate(button2.Text);
            dataGridView1.Columns[0].HeaderText = this.language.Translate(dataGridView1.Columns[0].HeaderText);
            dataGridView1.Columns[1].HeaderText = this.language.Translate(dataGridView1.Columns[1].HeaderText);
        }

        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (!dataGridView1.Rows[e.RowIndex].IsNewRow))
            {
                if (!Regex.IsMatch(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), "^[a-zA-Z_][a-zA-Z0-9_]*$"))
                {
                    MessageBox.Show(language.Translate("Имя параметра командной строки может содержать только цифры, буквы и символ подчеркивания. Первым символом имени не должна быть цифра"),
                        language.Translate("Ошибка"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
                bool duplicate_name = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    if (!row.IsNewRow && (row.Index != e.RowIndex) && (row.Cells[0].Value.ToString().Trim() ==
                        dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Trim()))
                        duplicate_name = true;
                if (duplicate_name)
                {
                    MessageBox.Show(language.Translate("Нельзя задавать одинаковые имена для нескольких параметров"),
                        language.Translate("Ошибка"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Trim() == "config")
                {
                    MessageBox.Show(language.Translate("Имя параметра config является зарезервированным"),
                        language.Translate("Ошибка"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                if (!row.IsNewRow && row.Selected)
                    dataGridView1.Rows.Remove(row);
        }
    }
}
