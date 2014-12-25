namespace AmEditor
{
    partial class Editor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.dataGridViewSteps = new System.Windows.Forms.DataGridView();
            this.NumStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StepLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StepName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewParams = new System.Windows.Forms.DataGridView();
            this.ParamNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParamType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParamDirection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBoxDescription = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.actionName_comboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pluginName_comboBox = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.повторенийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.меткаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.примечаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.плагиныToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.языкToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.плагиныToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.языкToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.конфигурацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.копироватьСтрокуЗапускаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выполнитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.плагиныToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыКоманднойСтрокиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.языкToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSteps)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParams)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonDown);
            this.splitContainer1.Panel1.Controls.Add(this.buttonUp);
            this.splitContainer1.Panel1.Controls.Add(this.buttonDel);
            this.splitContainer1.Panel1.Controls.Add(this.buttonAdd);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewSteps);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.actionName_comboBox);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.pluginName_comboBox);
            this.splitContainer1.Size = new System.Drawing.Size(779, 484);
            this.splitContainer1.SplitterDistance = 343;
            this.splitContainer1.TabIndex = 1;
            // 
            // buttonDown
            // 
            this.buttonDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDown.Image = ((System.Drawing.Image)(resources.GetObject("buttonDown.Image")));
            this.buttonDown.Location = new System.Drawing.Point(288, 442);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(44, 39);
            this.buttonDown.TabIndex = 6;
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUp.Image = ((System.Drawing.Image)(resources.GetObject("buttonUp.Image")));
            this.buttonUp.Location = new System.Drawing.Point(238, 442);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(44, 39);
            this.buttonUp.TabIndex = 5;
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDel.Image = ((System.Drawing.Image)(resources.GetObject("buttonDel.Image")));
            this.buttonDel.Location = new System.Drawing.Point(60, 442);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(44, 39);
            this.buttonDel.TabIndex = 3;
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.Image")));
            this.buttonAdd.Location = new System.Drawing.Point(10, 442);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(44, 39);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridViewSteps
            // 
            this.dataGridViewSteps.AllowDrop = true;
            this.dataGridViewSteps.AllowUserToAddRows = false;
            this.dataGridViewSteps.AllowUserToDeleteRows = false;
            this.dataGridViewSteps.AllowUserToResizeColumns = false;
            this.dataGridViewSteps.AllowUserToResizeRows = false;
            this.dataGridViewSteps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSteps.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSteps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSteps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumStep,
            this.StepLabel,
            this.StepName});
            this.dataGridViewSteps.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewSteps.MultiSelect = false;
            this.dataGridViewSteps.Name = "dataGridViewSteps";
            this.dataGridViewSteps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSteps.Size = new System.Drawing.Size(337, 433);
            this.dataGridViewSteps.TabIndex = 0;
            this.dataGridViewSteps.SelectionChanged += new System.EventHandler(this.dataGridViewSteps_SelectionChanged);
            this.dataGridViewSteps.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridViewSteps_DragDrop);
            this.dataGridViewSteps.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridViewSteps_DragEnter);
            this.dataGridViewSteps.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewSteps_MouseDown);
            this.dataGridViewSteps.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGridViewSteps_MouseMove);
            this.dataGridViewSteps.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewSteps_MouseUp);
            // 
            // NumStep
            // 
            this.NumStep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.NumStep.DefaultCellStyle = dataGridViewCellStyle2;
            this.NumStep.FillWeight = 50F;
            this.NumStep.HeaderText = "№";
            this.NumStep.MinimumWidth = 35;
            this.NumStep.Name = "NumStep";
            this.NumStep.ReadOnly = true;
            this.NumStep.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NumStep.Width = 35;
            // 
            // StepLabel
            // 
            this.StepLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.StepLabel.DefaultCellStyle = dataGridViewCellStyle3;
            this.StepLabel.HeaderText = "Метка";
            this.StepLabel.MinimumWidth = 50;
            this.StepLabel.Name = "StepLabel";
            this.StepLabel.ReadOnly = true;
            this.StepLabel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StepLabel.Visible = false;
            // 
            // StepName
            // 
            this.StepName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StepName.FillWeight = 200F;
            this.StepName.HeaderText = "Шаг";
            this.StepName.Name = "StepName";
            this.StepName.ReadOnly = true;
            this.StepName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(3, 83);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridViewParams);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.richTextBoxDescription);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Size = new System.Drawing.Size(429, 398);
            this.splitContainer2.SplitterDistance = 301;
            this.splitContainer2.TabIndex = 8;
            // 
            // dataGridViewParams
            // 
            this.dataGridViewParams.AllowUserToAddRows = false;
            this.dataGridViewParams.AllowUserToDeleteRows = false;
            this.dataGridViewParams.AllowUserToResizeRows = false;
            this.dataGridViewParams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewParams.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParamNumber,
            this.ParamName,
            this.ParamType,
            this.ParamDirection,
            this.ParamValue});
            this.dataGridViewParams.Location = new System.Drawing.Point(0, 20);
            this.dataGridViewParams.MultiSelect = false;
            this.dataGridViewParams.Name = "dataGridViewParams";
            this.dataGridViewParams.ReadOnly = true;
            this.dataGridViewParams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewParams.Size = new System.Drawing.Size(423, 278);
            this.dataGridViewParams.TabIndex = 4;
            this.dataGridViewParams.DoubleClick += new System.EventHandler(this.dataGridViewParams_DoubleClick);
            this.dataGridViewParams.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewParams_KeyDown);
            // 
            // ParamNumber
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.ParamNumber.DefaultCellStyle = dataGridViewCellStyle5;
            this.ParamNumber.FillWeight = 50F;
            this.ParamNumber.HeaderText = "№";
            this.ParamNumber.MinimumWidth = 35;
            this.ParamNumber.Name = "ParamNumber";
            this.ParamNumber.ReadOnly = true;
            this.ParamNumber.Width = 35;
            // 
            // ParamName
            // 
            this.ParamName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ParamName.HeaderText = "Имя";
            this.ParamName.MinimumWidth = 100;
            this.ParamName.Name = "ParamName";
            this.ParamName.ReadOnly = true;
            // 
            // ParamType
            // 
            this.ParamType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ParamType.HeaderText = "Тип";
            this.ParamType.MinimumWidth = 100;
            this.ParamType.Name = "ParamType";
            this.ParamType.ReadOnly = true;
            // 
            // ParamDirection
            // 
            this.ParamDirection.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ParamDirection.HeaderText = "Направление";
            this.ParamDirection.MinimumWidth = 100;
            this.ParamDirection.Name = "ParamDirection";
            this.ParamDirection.ReadOnly = true;
            // 
            // ParamValue
            // 
            this.ParamValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ParamValue.HeaderText = "Значение";
            this.ParamValue.MinimumWidth = 100;
            this.ParamValue.Name = "ParamValue";
            this.ParamValue.ReadOnly = true;
            this.ParamValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Параметры";
            // 
            // richTextBoxDescription
            // 
            this.richTextBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxDescription.Location = new System.Drawing.Point(3, 19);
            this.richTextBoxDescription.Name = "richTextBoxDescription";
            this.richTextBoxDescription.ReadOnly = true;
            this.richTextBoxDescription.Size = new System.Drawing.Size(423, 71);
            this.richTextBoxDescription.TabIndex = 8;
            this.richTextBoxDescription.Text = "";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Описание";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Действие";
            // 
            // actionName_comboBox
            // 
            this.actionName_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionName_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.actionName_comboBox.FormattingEnabled = true;
            this.actionName_comboBox.Location = new System.Drawing.Point(3, 59);
            this.actionName_comboBox.Name = "actionName_comboBox";
            this.actionName_comboBox.Size = new System.Drawing.Size(423, 21);
            this.actionName_comboBox.TabIndex = 2;
            this.actionName_comboBox.SelectedIndexChanged += new System.EventHandler(this.actionName_comboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Плагин";
            // 
            // pluginName_comboBox
            // 
            this.pluginName_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginName_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pluginName_comboBox.FormattingEnabled = true;
            this.pluginName_comboBox.Location = new System.Drawing.Point(3, 19);
            this.pluginName_comboBox.Name = "pluginName_comboBox";
            this.pluginName_comboBox.Size = new System.Drawing.Size(423, 21);
            this.pluginName_comboBox.TabIndex = 0;
            this.pluginName_comboBox.SelectedIndexChanged += new System.EventHandler(this.pluginName_comboBox_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.повторенийToolStripMenuItem,
            this.меткаToolStripMenuItem,
            this.примечаниеToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(179, 70);
            // 
            // повторенийToolStripMenuItem
            // 
            this.повторенийToolStripMenuItem.Name = "повторенийToolStripMenuItem";
            this.повторенийToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.повторенийToolStripMenuItem.Text = "Число повторений";
            this.повторенийToolStripMenuItem.Click += new System.EventHandler(this.повторенийToolStripMenuItem_Click);
            // 
            // меткаToolStripMenuItem
            // 
            this.меткаToolStripMenuItem.Name = "меткаToolStripMenuItem";
            this.меткаToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.меткаToolStripMenuItem.Text = "Метка";
            this.меткаToolStripMenuItem.Click += new System.EventHandler(this.меткаToolStripMenuItem_Click);
            // 
            // примечаниеToolStripMenuItem
            // 
            this.примечаниеToolStripMenuItem.Name = "примечаниеToolStripMenuItem";
            this.примечаниеToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.примечаниеToolStripMenuItem.Text = "Примечание";
            this.примечаниеToolStripMenuItem.Click += new System.EventHandler(this.примечаниеToolStripMenuItem_Click);
            // 
            // параметрыToolStripMenuItem
            // 
            this.параметрыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.плагиныToolStripMenuItem,
            this.языкToolStripMenuItem,
            this.toolStripMenuItem1,
            this.выходToolStripMenuItem});
            this.параметрыToolStripMenuItem.Name = "параметрыToolStripMenuItem";
            this.параметрыToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.параметрыToolStripMenuItem.Text = "Параметры";
            // 
            // плагиныToolStripMenuItem
            // 
            this.плагиныToolStripMenuItem.Name = "плагиныToolStripMenuItem";
            this.плагиныToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.плагиныToolStripMenuItem.Text = "Плагины";
            // 
            // языкToolStripMenuItem
            // 
            this.языкToolStripMenuItem.Name = "языкToolStripMenuItem";
            this.языкToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.языкToolStripMenuItem.Text = "Язык";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(121, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // настройкаToolStripMenuItem
            // 
            this.настройкаToolStripMenuItem.Name = "настройкаToolStripMenuItem";
            this.настройкаToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.настройкаToolStripMenuItem.Text = "Настройка";
            // 
            // плагиныToolStripMenuItem1
            // 
            this.плагиныToolStripMenuItem1.Name = "плагиныToolStripMenuItem1";
            this.плагиныToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.плагиныToolStripMenuItem1.Text = "Плагины";
            // 
            // языкToolStripMenuItem1
            // 
            this.языкToolStripMenuItem1.Name = "языкToolStripMenuItem1";
            this.языкToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.языкToolStripMenuItem1.Text = "Язык";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // выходToolStripMenuItem1
            // 
            this.выходToolStripMenuItem1.Name = "выходToolStripMenuItem1";
            this.выходToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.выходToolStripMenuItem1.Text = "Выход";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.конфигурацияToolStripMenuItem,
            this.настройкаToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(797, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // конфигурацияToolStripMenuItem
            // 
            this.конфигурацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem1,
            this.toolStripMenuItem3,
            this.копироватьСтрокуЗапускаToolStripMenuItem,
            this.выполнитьToolStripMenuItem,
            this.toolStripMenuItem4,
            this.выходToolStripMenuItem3});
            this.конфигурацияToolStripMenuItem.Name = "конфигурацияToolStripMenuItem";
            this.конфигурацияToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.конфигурацияToolStripMenuItem.Text = "Конфигурация";
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.создатьToolStripMenuItem.Text = "Создать";
            this.создатьToolStripMenuItem.Click += new System.EventHandler(this.создатьToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // сохранитьКакToolStripMenuItem1
            // 
            this.сохранитьКакToolStripMenuItem1.Name = "сохранитьКакToolStripMenuItem1";
            this.сохранитьКакToolStripMenuItem1.Size = new System.Drawing.Size(251, 22);
            this.сохранитьКакToolStripMenuItem1.Text = "Сохранить как";
            this.сохранитьКакToolStripMenuItem1.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(248, 6);
            // 
            // копироватьСтрокуЗапускаToolStripMenuItem
            // 
            this.копироватьСтрокуЗапускаToolStripMenuItem.Name = "копироватьСтрокуЗапускаToolStripMenuItem";
            this.копироватьСтрокуЗапускаToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.копироватьСтрокуЗапускаToolStripMenuItem.Text = "Копировать строку выполнения";
            this.копироватьСтрокуЗапускаToolStripMenuItem.Click += new System.EventHandler(this.копироватьСтрокуЗапускаToolStripMenuItem_Click);
            // 
            // выполнитьToolStripMenuItem
            // 
            this.выполнитьToolStripMenuItem.Name = "выполнитьToolStripMenuItem";
            this.выполнитьToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.выполнитьToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.выполнитьToolStripMenuItem.Text = "Выполнить";
            this.выполнитьToolStripMenuItem.Click += new System.EventHandler(this.выполнитьToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(248, 6);
            // 
            // выходToolStripMenuItem3
            // 
            this.выходToolStripMenuItem3.Name = "выходToolStripMenuItem3";
            this.выходToolStripMenuItem3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.выходToolStripMenuItem3.Size = new System.Drawing.Size(251, 22);
            this.выходToolStripMenuItem3.Text = "Выход";
            this.выходToolStripMenuItem3.Click += new System.EventHandler(this.выходToolStripMenuItem3_Click);
            // 
            // настройкаToolStripMenuItem1
            // 
            this.настройкаToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.плагиныToolStripMenuItem2,
            this.параметрыКоманднойСтрокиToolStripMenuItem,
            this.языкToolStripMenuItem2});
            this.настройкаToolStripMenuItem1.Name = "настройкаToolStripMenuItem1";
            this.настройкаToolStripMenuItem1.Size = new System.Drawing.Size(78, 20);
            this.настройкаToolStripMenuItem1.Text = "Настройка";
            // 
            // плагиныToolStripMenuItem2
            // 
            this.плагиныToolStripMenuItem2.Name = "плагиныToolStripMenuItem2";
            this.плагиныToolStripMenuItem2.Size = new System.Drawing.Size(244, 22);
            this.плагиныToolStripMenuItem2.Text = "Плагины";
            this.плагиныToolStripMenuItem2.Click += new System.EventHandler(this.плагиныToolStripMenuItem2_Click);
            // 
            // параметрыКоманднойСтрокиToolStripMenuItem
            // 
            this.параметрыКоманднойСтрокиToolStripMenuItem.Name = "параметрыКоманднойСтрокиToolStripMenuItem";
            this.параметрыКоманднойСтрокиToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.параметрыКоманднойСтрокиToolStripMenuItem.Text = "Параметры командной строки";
            this.параметрыКоманднойСтрокиToolStripMenuItem.Click += new System.EventHandler(this.параметрыКоманднойСтрокиToolStripMenuItem_Click);
            // 
            // языкToolStripMenuItem2
            // 
            this.языкToolStripMenuItem2.Name = "языкToolStripMenuItem2";
            this.языкToolStripMenuItem2.Size = new System.Drawing.Size(244, 22);
            this.языкToolStripMenuItem2.Text = "Язык";
            this.языкToolStripMenuItem2.Click += new System.EventHandler(this.языкToolStripMenuItem2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "xml";
            this.openFileDialog1.FileName = "config";
            this.openFileDialog1.Filter = "Config|*.xml";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "xml";
            this.saveFileDialog1.Filter = "Config|*.xml";
            // 
            // Editor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 514);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(813, 552);
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AM-редактор";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Editor_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Editor_DragEnter);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSteps)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParams)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridViewSteps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox actionName_comboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox pluginName_comboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewParams;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem параметрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem плагиныToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem языкToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem плагиныToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem языкToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкаToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem плагиныToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem языкToolStripMenuItem2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamDirection;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamValue;
        private System.Windows.Forms.ToolStripMenuItem конфигурацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выполнитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem1;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox richTextBoxDescription;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem параметрыКоманднойСтрокиToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.ToolStripMenuItem копироватьСтрокуЗапускаToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem повторенийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem меткаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem примечаниеToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn StepLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn StepName;


    }
}

