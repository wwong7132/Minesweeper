namespace Minesweeper
{
    partial class Setting
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DefaultButton = new System.Windows.Forms.Button();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.minesNumberBox = new System.Windows.Forms.NumericUpDown();
            this.columnsNumberBox = new System.Windows.Forms.NumericUpDown();
            this.rowsNumberBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.minesNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnsNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowsNumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Rows";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Columns";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Mines";
            // 
            // DefaultButton
            // 
            this.DefaultButton.Location = new System.Drawing.Point(12, 89);
            this.DefaultButton.Name = "DefaultButton";
            this.DefaultButton.Size = new System.Drawing.Size(65, 22);
            this.DefaultButton.TabIndex = 6;
            this.DefaultButton.Text = "Default";
            this.DefaultButton.UseVisualStyleBackColor = true;
            this.DefaultButton.Click += new System.EventHandler(this.DefaultButton_Click);
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.ConfirmButton.Location = new System.Drawing.Point(87, 89);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(65, 22);
            this.ConfirmButton.TabIndex = 10;
            this.ConfirmButton.Text = "Confirm";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // minesNumberBox
            // 
            this.minesNumberBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Minesweeper.Properties.Settings.Default, "mines", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.minesNumberBox.Location = new System.Drawing.Point(72, 63);
            this.minesNumberBox.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.minesNumberBox.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.minesNumberBox.Name = "minesNumberBox";
            this.minesNumberBox.Size = new System.Drawing.Size(80, 20);
            this.minesNumberBox.TabIndex = 9;
            this.minesNumberBox.Value = global::Minesweeper.Properties.Settings.Default.mines;
            this.minesNumberBox.ValueChanged += new System.EventHandler(this.minesNumberBox_ValueChanged);
            // 
            // columnsNumberBox
            // 
            this.columnsNumberBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Minesweeper.Properties.Settings.Default, "cols", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.columnsNumberBox.Location = new System.Drawing.Point(72, 38);
            this.columnsNumberBox.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.columnsNumberBox.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.columnsNumberBox.Name = "columnsNumberBox";
            this.columnsNumberBox.Size = new System.Drawing.Size(80, 20);
            this.columnsNumberBox.TabIndex = 8;
            this.columnsNumberBox.Value = global::Minesweeper.Properties.Settings.Default.cols;
            this.columnsNumberBox.ValueChanged += new System.EventHandler(this.columnsNumberBox_ValueChanged);
            // 
            // rowsNumberBox
            // 
            this.rowsNumberBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Minesweeper.Properties.Settings.Default, "rows", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rowsNumberBox.Location = new System.Drawing.Point(72, 14);
            this.rowsNumberBox.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.rowsNumberBox.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.rowsNumberBox.Name = "rowsNumberBox";
            this.rowsNumberBox.Size = new System.Drawing.Size(80, 20);
            this.rowsNumberBox.TabIndex = 7;
            this.rowsNumberBox.Value = global::Minesweeper.Properties.Settings.Default.rows;
            this.rowsNumberBox.ValueChanged += new System.EventHandler(this.rowsNumberBox_ValueChanged);
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(164, 121);
            this.Controls.Add(this.ConfirmButton);
            this.Controls.Add(this.minesNumberBox);
            this.Controls.Add(this.columnsNumberBox);
            this.Controls.Add(this.rowsNumberBox);
            this.Controls.Add(this.DefaultButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Setting";
            this.Text = "Setting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Setting_FormClosing);
            this.Load += new System.EventHandler(this.Setting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.minesNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnsNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowsNumberBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button DefaultButton;
        private System.Windows.Forms.NumericUpDown rowsNumberBox;
        private System.Windows.Forms.NumericUpDown columnsNumberBox;
        private System.Windows.Forms.NumericUpDown minesNumberBox;
        private System.Windows.Forms.Button ConfirmButton;
    }
}