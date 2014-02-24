namespace AmEditor
{
    partial class FormSqlValue
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.scintillaEditor = new ScintillaNET.Scintilla();
            this.contextMenuStripSQLEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.отменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.повторитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.вырезатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.буферОбменаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.путьДоФайлаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.путьДоПапкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxValues = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.folderBrowserDialogInsert = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialogInsert = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.scintillaEditor)).BeginInit();
            this.contextMenuStripSQLEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // scintillaEditor
            // 
            this.scintillaEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scintillaEditor.AutoComplete.IsCaseSensitive = false;
            this.scintillaEditor.AutoComplete.ListString = "AND AS BEGIN BY CREATE CROSS END FROM FULL FUNCTION GROUP HAVING INNER JOIN LIMIT" +
                " OR ORDER OUTER PROCEDURE SELECT SET TABLE TOP TRIGGER VIEW WHERE";
            this.scintillaEditor.ConfigurationManager.Language = "mssql";
            this.scintillaEditor.ContextMenuStrip = this.contextMenuStripSQLEditor;
            this.scintillaEditor.LineWrapping.Mode = ScintillaNET.LineWrappingMode.Word;
            this.scintillaEditor.Location = new System.Drawing.Point(12, 5);
            this.scintillaEditor.Margins.Margin0.Width = 20;
            this.scintillaEditor.Name = "scintillaEditor";
            this.scintillaEditor.Size = new System.Drawing.Size(590, 246);
            this.scintillaEditor.TabIndex = 0;
            // 
            // contextMenuStripSQLEditor
            // 
            this.contextMenuStripSQLEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отменитьToolStripMenuItem,
            this.повторитьToolStripMenuItem,
            this.toolStripMenuItem1,
            this.вырезатьToolStripMenuItem,
            this.копироватьToolStripMenuItem1,
            this.вставитьToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.contextMenuStripSQLEditor.Name = "contextMenuStripSQLEditor";
            this.contextMenuStripSQLEditor.Size = new System.Drawing.Size(182, 142);
            // 
            // отменитьToolStripMenuItem
            // 
            this.отменитьToolStripMenuItem.Name = "отменитьToolStripMenuItem";
            this.отменитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.отменитьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.отменитьToolStripMenuItem.Text = "Отменить";
            this.отменитьToolStripMenuItem.Click += new System.EventHandler(this.отменитьToolStripMenuItem_Click);
            // 
            // повторитьToolStripMenuItem
            // 
            this.повторитьToolStripMenuItem.Name = "повторитьToolStripMenuItem";
            this.повторитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.повторитьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.повторитьToolStripMenuItem.Text = "Повторить";
            this.повторитьToolStripMenuItem.Click += new System.EventHandler(this.повторитьToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(178, 6);
            // 
            // вырезатьToolStripMenuItem
            // 
            this.вырезатьToolStripMenuItem.Name = "вырезатьToolStripMenuItem";
            this.вырезатьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.вырезатьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.вырезатьToolStripMenuItem.Text = "Вырезать";
            this.вырезатьToolStripMenuItem.Click += new System.EventHandler(this.вырезатьToolStripMenuItem_Click);
            // 
            // копироватьToolStripMenuItem1
            // 
            this.копироватьToolStripMenuItem1.Name = "копироватьToolStripMenuItem1";
            this.копироватьToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.копироватьToolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
            this.копироватьToolStripMenuItem1.Text = "Копировать";
            this.копироватьToolStripMenuItem1.Click += new System.EventHandler(this.копироватьToolStripMenuItem1_Click);
            // 
            // вставитьToolStripMenuItem
            // 
            this.вставитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.буферОбменаToolStripMenuItem,
            this.путьДоФайлаToolStripMenuItem,
            this.путьДоПапкиToolStripMenuItem});
            this.вставитьToolStripMenuItem.Name = "вставитьToolStripMenuItem";
            this.вставитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.вставитьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.вставитьToolStripMenuItem.Text = "Вставить";
            // 
            // буферОбменаToolStripMenuItem
            // 
            this.буферОбменаToolStripMenuItem.Name = "буферОбменаToolStripMenuItem";
            this.буферОбменаToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.буферОбменаToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.буферОбменаToolStripMenuItem.Text = "Буфер обмена";
            this.буферОбменаToolStripMenuItem.Click += new System.EventHandler(this.буферОбменаToolStripMenuItem_Click);
            // 
            // путьДоФайлаToolStripMenuItem
            // 
            this.путьДоФайлаToolStripMenuItem.Name = "путьДоФайлаToolStripMenuItem";
            this.путьДоФайлаToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.I)));
            this.путьДоФайлаToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.путьДоФайлаToolStripMenuItem.Text = "Путь до файла";
            this.путьДоФайлаToolStripMenuItem.Click += new System.EventHandler(this.путьДоФайлаToolStripMenuItem_Click);
            // 
            // путьДоПапкиToolStripMenuItem
            // 
            this.путьДоПапкиToolStripMenuItem.Name = "путьДоПапкиToolStripMenuItem";
            this.путьДоПапкиToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.I)));
            this.путьДоПапкиToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.путьДоПапкиToolStripMenuItem.Text = "Путь до папки";
            this.путьДоПапкиToolStripMenuItem.Click += new System.EventHandler(this.путьДоПапкиToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(93, 257);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Отменить";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(12, 257);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // comboBoxValues
            // 
            this.comboBoxValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxValues.FormattingEnabled = true;
            this.comboBoxValues.Location = new System.Drawing.Point(174, 257);
            this.comboBoxValues.Name = "comboBoxValues";
            this.comboBoxValues.Size = new System.Drawing.Size(347, 21);
            this.comboBoxValues.TabIndex = 4;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(527, 257);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Вставить";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // openFileDialogInsert
            // 
            this.openFileDialogInsert.FileName = "openFileDialog1";
            // 
            // FormSqlValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 287);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBoxValues);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.scintillaEditor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(630, 321);
            this.Name = "FormSqlValue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Задать значение";
            ((System.ComponentModel.ISupportInitialize)(this.scintillaEditor)).EndInit();
            this.contextMenuStripSQLEditor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ScintillaNET.Scintilla scintillaEditor;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxValues;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSQLEditor;
        private System.Windows.Forms.ToolStripMenuItem отменитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem повторитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem вырезатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem буферОбменаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem путьДоФайлаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem путьДоПапкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogInsert;
        private System.Windows.Forms.OpenFileDialog openFileDialogInsert;
    }
}