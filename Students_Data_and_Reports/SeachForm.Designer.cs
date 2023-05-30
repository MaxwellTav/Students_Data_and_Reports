namespace Students_Data_and_Reports
{
    partial class SeachForm
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
            this.CancelButton = new System.Windows.Forms.Button();
            this.SelectionSearchComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SearchValueTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.Location = new System.Drawing.Point(212, 100);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(98, 44);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancelar";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SelectionSearchComboBox
            // 
            this.SelectionSearchComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectionSearchComboBox.Enabled = false;
            this.SelectionSearchComboBox.FormattingEnabled = true;
            this.SelectionSearchComboBox.Items.AddRange(new object[] {
            "ID",
            "Nombre",
            "Apellido",
            "Teléfono",
            "Nacionalidad",
            "Residencia",
            "Correo"});
            this.SelectionSearchComboBox.Location = new System.Drawing.Point(133, 34);
            this.SelectionSearchComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.SelectionSearchComboBox.Name = "SelectionSearchComboBox";
            this.SelectionSearchComboBox.Size = new System.Drawing.Size(160, 27);
            this.SelectionSearchComboBox.TabIndex = 1;
            this.SelectionSearchComboBox.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "¿Qué desea busar?";
            // 
            // SearchValueTextBox
            // 
            this.SearchValueTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchValueTextBox.Location = new System.Drawing.Point(12, 64);
            this.SearchValueTextBox.Name = "SearchValueTextBox";
            this.SearchValueTextBox.Size = new System.Drawing.Size(282, 28);
            this.SearchValueTextBox.TabIndex = 0;
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton.Location = new System.Drawing.Point(-1, 100);
            this.OkButton.Margin = new System.Windows.Forms.Padding(4);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(218, 44);
            this.OkButton.TabIndex = 4;
            this.OkButton.Text = "Buscar";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Datos disponibles";
            this.label2.Visible = false;
            // 
            // SeachForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 143);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.SearchValueTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SelectionSearchComboBox);
            this.Controls.Add(this.CancelButton);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SeachForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "¿Qué desea busar?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.ComboBox SelectionSearchComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SearchValueTextBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label label2;
    }
}