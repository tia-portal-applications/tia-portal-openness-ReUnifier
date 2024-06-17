namespace ReUnifier
{
    partial class Event
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
            this.EventType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ScriptTextBox = new System.Windows.Forms.RichTextBox();
            this.EventUpdate = new System.Windows.Forms.Button();
            this.EventDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EventType
            // 
            this.EventType.Font = new System.Drawing.Font("Siemens Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EventType.FormattingEnabled = true;
            this.EventType.Items.AddRange(new object[] {
            "Tapped",
            "ContextTapped",
            "Loaded",
            "Unloaded",
            "None"});
            this.EventType.Location = new System.Drawing.Point(70, 25);
            this.EventType.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.EventType.Name = "EventType";
            this.EventType.Size = new System.Drawing.Size(184, 23);
            this.EventType.TabIndex = 45;
            this.EventType.SelectedIndexChanged += new System.EventHandler(this.EventType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Siemens Sans", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 23);
            this.label3.TabIndex = 44;
            this.label3.Text = "Type:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Siemens Sans", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 23);
            this.label1.TabIndex = 47;
            this.label1.Text = "Script:";
            // 
            // ScriptTextBox
            // 
            this.ScriptTextBox.Font = new System.Drawing.Font("Siemens Sans", 9F);
            this.ScriptTextBox.Location = new System.Drawing.Point(12, 107);
            this.ScriptTextBox.Name = "ScriptTextBox";
            this.ScriptTextBox.Size = new System.Drawing.Size(582, 363);
            this.ScriptTextBox.TabIndex = 46;
            this.ScriptTextBox.Text = "";
            // 
            // EventUpdate
            // 
            this.EventUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.EventUpdate.BackColor = System.Drawing.Color.LightSkyBlue;
            this.EventUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EventUpdate.Font = new System.Drawing.Font("Siemens Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EventUpdate.ForeColor = System.Drawing.Color.White;
            this.EventUpdate.Location = new System.Drawing.Point(521, 484);
            this.EventUpdate.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.EventUpdate.Name = "EventUpdate";
            this.EventUpdate.Size = new System.Drawing.Size(78, 25);
            this.EventUpdate.TabIndex = 49;
            this.EventUpdate.Text = "Update";
            this.EventUpdate.UseVisualStyleBackColor = false;
            this.EventUpdate.Click += new System.EventHandler(this.EventUpdate_Click);
            // 
            // EventDelete
            // 
            this.EventDelete.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.EventDelete.BackColor = System.Drawing.Color.LightSkyBlue;
            this.EventDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EventDelete.Font = new System.Drawing.Font("Siemens Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EventDelete.ForeColor = System.Drawing.Color.White;
            this.EventDelete.Location = new System.Drawing.Point(437, 484);
            this.EventDelete.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.EventDelete.Name = "EventDelete";
            this.EventDelete.Size = new System.Drawing.Size(78, 25);
            this.EventDelete.TabIndex = 48;
            this.EventDelete.Text = "Delete";
            this.EventDelete.UseVisualStyleBackColor = false;
            this.EventDelete.Click += new System.EventHandler(this.EventDelete_Click);
            // 
            // Event
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::ReUnifier.Properties.Resources.Bg_1x;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(611, 519);
            this.Controls.Add(this.EventUpdate);
            this.Controls.Add(this.EventDelete);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ScriptTextBox);
            this.Controls.Add(this.EventType);
            this.Controls.Add(this.label3);
            this.DoubleBuffered = true;
            this.Name = "Event";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Event Dynamization";
            this.Load += new System.EventHandler(this.Event_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox EventType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox ScriptTextBox;
        private System.Windows.Forms.Button EventUpdate;
        private System.Windows.Forms.Button EventDelete;
    }
}