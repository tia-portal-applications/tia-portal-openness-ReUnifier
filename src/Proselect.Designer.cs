
namespace ReUnifier
{
    partial class Proselect
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
            this.comboBoxProName = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Siemens Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select the Project to Inport from TIA Portal";
            // 
            // comboBoxProName
            // 
            this.comboBoxProName.FormattingEnabled = true;
            this.comboBoxProName.Location = new System.Drawing.Point(16, 52);
            this.comboBoxProName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxProName.Name = "comboBoxProName";
            this.comboBoxProName.Size = new System.Drawing.Size(569, 23);
            this.comboBoxProName.TabIndex = 2;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonCancel.Font = new System.Drawing.Font("Siemens Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.buttonCancel.Location = new System.Drawing.Point(420, 102);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 45);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonApply.Font = new System.Drawing.Font("Siemens Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonApply.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonApply.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonApply.Location = new System.Drawing.Point(508, 102);
            this.buttonApply.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(80, 45);
            this.buttonApply.TabIndex = 17;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = false;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // Proselect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(604, 167);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.comboBoxProName);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Proselect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proselect";
            this.Load += new System.EventHandler(this.Proselect_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxProName;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonApply;
    }
}