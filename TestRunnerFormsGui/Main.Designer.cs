namespace TestRunnerFormsGui
{
    partial class Main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.FileLabel = new System.Windows.Forms.Label();
            this.FileButton = new System.Windows.Forms.Button();
            this.FileDialog = new System.Windows.Forms.OpenFileDialog();
            this.HorizontalSplit = new System.Windows.Forms.SplitContainer();
            this.VerticalSplit = new System.Windows.Forms.SplitContainer();
            this.Grid = new System.Windows.Forms.DataGridView();
            this.ColumnResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridColumnSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestInfoTextBox = new System.Windows.Forms.TextBox();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.ReloadButton = new System.Windows.Forms.Button();
            this.Tip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.HorizontalSplit)).BeginInit();
            this.HorizontalSplit.Panel1.SuspendLayout();
            this.HorizontalSplit.Panel2.SuspendLayout();
            this.HorizontalSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalSplit)).BeginInit();
            this.VerticalSplit.Panel1.SuspendLayout();
            this.VerticalSplit.Panel2.SuspendLayout();
            this.VerticalSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // FileLabel
            // 
            this.FileLabel.AutoSize = true;
            this.FileLabel.Location = new System.Drawing.Point(96, 12);
            this.FileLabel.Name = "FileLabel";
            this.FileLabel.Size = new System.Drawing.Size(103, 13);
            this.FileLabel.TabIndex = 0;
            this.FileLabel.Text = "C:\\UnrealSctipt\\Log";
            // 
            // FileButton
            // 
            this.FileButton.Location = new System.Drawing.Point(42, 7);
            this.FileButton.Name = "FileButton";
            this.FileButton.Size = new System.Drawing.Size(48, 23);
            this.FileButton.TabIndex = 1;
            this.FileButton.Text = "File...";
            this.Tip.SetToolTip(this.FileButton, "Select file to load");
            this.FileButton.UseVisualStyleBackColor = true;
            this.FileButton.Click += new System.EventHandler(this.FileButton_Click);
            // 
            // FileDialog
            // 
            this.FileDialog.FileName = "openFileDialog1";
            this.FileDialog.Filter = "UDK log|*.log|All files|*.*";
            // 
            // HorizontalSplit
            // 
            this.HorizontalSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HorizontalSplit.Location = new System.Drawing.Point(2, 36);
            this.HorizontalSplit.Name = "HorizontalSplit";
            this.HorizontalSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // HorizontalSplit.Panel1
            // 
            this.HorizontalSplit.Panel1.Controls.Add(this.VerticalSplit);
            // 
            // HorizontalSplit.Panel2
            // 
            this.HorizontalSplit.Panel2.Controls.Add(this.OutputTextBox);
            this.HorizontalSplit.Size = new System.Drawing.Size(954, 378);
            this.HorizontalSplit.SplitterDistance = 236;
            this.HorizontalSplit.TabIndex = 2;
            // 
            // VerticalSplit
            // 
            this.VerticalSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VerticalSplit.Location = new System.Drawing.Point(0, 0);
            this.VerticalSplit.Name = "VerticalSplit";
            // 
            // VerticalSplit.Panel1
            // 
            this.VerticalSplit.Panel1.Controls.Add(this.Grid);
            // 
            // VerticalSplit.Panel2
            // 
            this.VerticalSplit.Panel2.Controls.Add(this.TestInfoTextBox);
            this.VerticalSplit.Size = new System.Drawing.Size(954, 236);
            this.VerticalSplit.SplitterDistance = 632;
            this.VerticalSplit.TabIndex = 0;
            // 
            // Grid
            // 
            this.Grid.AllowUserToAddRows = false;
            this.Grid.AllowUserToDeleteRows = false;
            this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnResult,
            this.GridColumnSource,
            this.ColumnMessage,
            this.ColumnTime});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grid.DefaultCellStyle = dataGridViewCellStyle1;
            this.Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid.Location = new System.Drawing.Point(0, 0);
            this.Grid.Name = "Grid";
            this.Grid.ReadOnly = true;
            this.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grid.Size = new System.Drawing.Size(632, 236);
            this.Grid.TabIndex = 0;
            this.Grid.SelectionChanged += new System.EventHandler(this.Grid_SelectionChanged);
            // 
            // ColumnResult
            // 
            this.ColumnResult.HeaderText = "Result";
            this.ColumnResult.Name = "ColumnResult";
            this.ColumnResult.ReadOnly = true;
            // 
            // GridColumnSource
            // 
            this.GridColumnSource.HeaderText = "Source";
            this.GridColumnSource.Name = "GridColumnSource";
            this.GridColumnSource.ReadOnly = true;
            this.GridColumnSource.Width = 200;
            // 
            // ColumnMessage
            // 
            this.ColumnMessage.HeaderText = "Message";
            this.ColumnMessage.Name = "ColumnMessage";
            this.ColumnMessage.ReadOnly = true;
            this.ColumnMessage.Width = 300;
            // 
            // ColumnTime
            // 
            this.ColumnTime.HeaderText = "Time";
            this.ColumnTime.Name = "ColumnTime";
            this.ColumnTime.ReadOnly = true;
            // 
            // TestInfoTextBox
            // 
            this.TestInfoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestInfoTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TestInfoTextBox.Location = new System.Drawing.Point(0, 0);
            this.TestInfoTextBox.Multiline = true;
            this.TestInfoTextBox.Name = "TestInfoTextBox";
            this.TestInfoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TestInfoTextBox.Size = new System.Drawing.Size(318, 236);
            this.TestInfoTextBox.TabIndex = 0;
            this.TestInfoTextBox.WordWrap = false;
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputTextBox.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OutputTextBox.Location = new System.Drawing.Point(0, 0);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutputTextBox.Size = new System.Drawing.Size(954, 138);
            this.OutputTextBox.TabIndex = 0;
            // 
            // ReloadButton
            // 
            this.ReloadButton.Enabled = false;
            this.ReloadButton.Image = ((System.Drawing.Image)(resources.GetObject("ReloadButton.Image")));
            this.ReloadButton.Location = new System.Drawing.Point(2, 7);
            this.ReloadButton.Name = "ReloadButton";
            this.ReloadButton.Size = new System.Drawing.Size(34, 23);
            this.ReloadButton.TabIndex = 3;
            this.Tip.SetToolTip(this.ReloadButton, "Reprocess current file");
            this.ReloadButton.UseVisualStyleBackColor = true;
            this.ReloadButton.Click += new System.EventHandler(this.ReloadButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 415);
            this.Controls.Add(this.ReloadButton);
            this.Controls.Add(this.HorizontalSplit);
            this.Controls.Add(this.FileButton);
            this.Controls.Add(this.FileLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Unreal Script Simple Test GUI";
            this.HorizontalSplit.Panel1.ResumeLayout(false);
            this.HorizontalSplit.Panel2.ResumeLayout(false);
            this.HorizontalSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HorizontalSplit)).EndInit();
            this.HorizontalSplit.ResumeLayout(false);
            this.VerticalSplit.Panel1.ResumeLayout(false);
            this.VerticalSplit.Panel2.ResumeLayout(false);
            this.VerticalSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalSplit)).EndInit();
            this.VerticalSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FileLabel;
        private System.Windows.Forms.Button FileButton;
        private System.Windows.Forms.OpenFileDialog FileDialog;
        private System.Windows.Forms.SplitContainer HorizontalSplit;
        private System.Windows.Forms.SplitContainer VerticalSplit;
        private System.Windows.Forms.Button ReloadButton;
        private System.Windows.Forms.DataGridView Grid;
        private System.Windows.Forms.ToolTip Tip;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.TextBox TestInfoTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridColumnSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTime;
    }
}

