namespace ImagedComboBox
{
    partial class Form1
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
            this.imagedComboBox1 = new ImagedComboBox();
            this.SuspendLayout();
            // 
            // imagedComboBox1
            // 
            this.imagedComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.imagedComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imagedComboBox1.FormattingEnabled = true;
            this.imagedComboBox1.Location = new System.Drawing.Point(76, 107);
            this.imagedComboBox1.Name = "imagedComboBox1";
            this.imagedComboBox1.Size = new System.Drawing.Size(121, 21);
            this.imagedComboBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.imagedComboBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ImagedComboBox imagedComboBox1;
    }
}