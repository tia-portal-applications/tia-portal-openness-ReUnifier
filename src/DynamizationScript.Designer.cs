namespace ReUnifier
{
    partial class DynamizationScript
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DynamizationScript));
            this.DynamizationDelete = new System.Windows.Forms.Button();
            this.DynamizationUpdate = new System.Windows.Forms.Button();
            this.Editor = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TagSelect = new System.Windows.Forms.ComboBox();
            this.ReadOnly = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.DynamizationScriptTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.triggerType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.triggerTag = new System.Windows.Forms.ComboBox();
            this.TagNameList = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.TriggerPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.TriggerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // DynamizationDelete
            // 
            this.DynamizationDelete.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DynamizationDelete.BackColor = System.Drawing.Color.LightSkyBlue;
            this.DynamizationDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DynamizationDelete.Font = new System.Drawing.Font("Siemens Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DynamizationDelete.ForeColor = System.Drawing.Color.White;
            this.DynamizationDelete.Location = new System.Drawing.Point(437, 484);
            this.DynamizationDelete.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.DynamizationDelete.Name = "DynamizationDelete";
            this.DynamizationDelete.Size = new System.Drawing.Size(78, 25);
            this.DynamizationDelete.TabIndex = 40;
            this.DynamizationDelete.Text = "Delete";
            this.DynamizationDelete.UseVisualStyleBackColor = false;
            this.DynamizationDelete.Click += new System.EventHandler(this.DynamizationDelete_Click);
            // 
            // DynamizationUpdate
            // 
            this.DynamizationUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DynamizationUpdate.BackColor = System.Drawing.Color.LightSkyBlue;
            this.DynamizationUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DynamizationUpdate.Font = new System.Drawing.Font("Siemens Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DynamizationUpdate.ForeColor = System.Drawing.Color.White;
            this.DynamizationUpdate.Location = new System.Drawing.Point(521, 484);
            this.DynamizationUpdate.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.DynamizationUpdate.Name = "DynamizationUpdate";
            this.DynamizationUpdate.Size = new System.Drawing.Size(78, 25);
            this.DynamizationUpdate.TabIndex = 41;
            this.DynamizationUpdate.Text = "Update";
            this.DynamizationUpdate.UseVisualStyleBackColor = false;
            this.DynamizationUpdate.Click += new System.EventHandler(this.DynamizationUpdate_Click);
            // 
            // Editor
            // 
            this.Editor.BackColor = System.Drawing.Color.Transparent;
            this.Editor.Font = new System.Drawing.Font("Siemens Sans", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Editor.ForeColor = System.Drawing.Color.White;
            this.Editor.Location = new System.Drawing.Point(16, 10);
            this.Editor.Name = "Editor";
            this.Editor.Size = new System.Drawing.Size(94, 23);
            this.Editor.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Siemens Sans", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(13, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 23);
            this.label2.TabIndex = 42;
            this.label2.Text = "Tag:";
            // 
            // TagSelect
            // 
            this.TagSelect.Font = new System.Drawing.Font("Siemens Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TagSelect.FormattingEnabled = true;
            this.TagSelect.Location = new System.Drawing.Point(69, 68);
            this.TagSelect.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.TagSelect.Name = "TagSelect";
            this.TagSelect.Size = new System.Drawing.Size(184, 23);
            this.TagSelect.TabIndex = 43;
            // 
            // ReadOnly
            // 
            this.ReadOnly.AutoSize = true;
            this.ReadOnly.BackColor = System.Drawing.Color.Transparent;
            this.ReadOnly.Font = new System.Drawing.Font("Siemens Sans", 11F, System.Drawing.FontStyle.Bold);
            this.ReadOnly.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ReadOnly.Location = new System.Drawing.Point(291, 70);
            this.ReadOnly.Name = "ReadOnly";
            this.ReadOnly.Size = new System.Drawing.Size(92, 21);
            this.ReadOnly.TabIndex = 44;
            this.ReadOnly.TabStop = true;
            this.ReadOnly.Text = "ReadOnly";
            this.ReadOnly.UseVisualStyleBackColor = false;
            this.ReadOnly.Click += new System.EventHandler(this.ReadOnly_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Siemens Sans", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 23);
            this.label1.TabIndex = 42;
            this.label1.Text = "Script:";
            // 
            // DynamizationScriptTextBox
            // 
            this.DynamizationScriptTextBox.Font = new System.Drawing.Font("Siemens Sans", 9F);
            this.DynamizationScriptTextBox.Location = new System.Drawing.Point(133, 133);
            this.DynamizationScriptTextBox.Name = "DynamizationScriptTextBox";
            this.DynamizationScriptTextBox.Size = new System.Drawing.Size(461, 337);
            this.DynamizationScriptTextBox.TabIndex = 39;
            this.DynamizationScriptTextBox.Text = "";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Siemens Sans", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 23);
            this.label3.TabIndex = 42;
            this.label3.Text = "Type:";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Siemens Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Tag Dynamization",
            "ScriptCode Dynamization"});
            this.comboBox1.Location = new System.Drawing.Point(69, 19);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(184, 23);
            this.comboBox1.TabIndex = 43;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Siemens Sans", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(12, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 23);
            this.label4.TabIndex = 42;
            this.label4.Text = "Trigger Type:";
            // 
            // triggerType
            // 
            this.triggerType.Font = new System.Drawing.Font("Siemens Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.triggerType.FormattingEnabled = true;
            this.triggerType.Items.AddRange(new object[] {
            "Disabled",
            "T100ms",
            "T250ms",
            "T500ms",
            "T1s",
            "T2s",
            "T5s",
            "T10s",
            "Tags",
            "Tags-automatic"});
            this.triggerType.Location = new System.Drawing.Point(15, 210);
            this.triggerType.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.triggerType.Name = "triggerType";
            this.triggerType.Size = new System.Drawing.Size(95, 23);
            this.triggerType.TabIndex = 43;
            this.triggerType.SelectedIndexChanged += new System.EventHandler(this.triggerType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Siemens Sans", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 23);
            this.label5.TabIndex = 42;
            this.label5.Text = "Tags:";
            // 
            // triggerTag
            // 
            this.triggerTag.Font = new System.Drawing.Font("Siemens Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.triggerTag.FormattingEnabled = true;
            this.triggerTag.Location = new System.Drawing.Point(7, 24);
            this.triggerTag.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.triggerTag.Name = "triggerTag";
            this.triggerTag.Size = new System.Drawing.Size(95, 23);
            this.triggerTag.TabIndex = 43;
            // 
            // TagNameList
            // 
            this.TagNameList.Font = new System.Drawing.Font("Siemens Sans", 9.75F);
            this.TagNameList.FormattingEnabled = true;
            this.TagNameList.ItemHeight = 15;
            this.TagNameList.Location = new System.Drawing.Point(8, 80);
            this.TagNameList.Name = "TagNameList";
            this.TagNameList.Size = new System.Drawing.Size(93, 109);
            this.TagNameList.TabIndex = 45;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Siemens Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(28, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 24);
            this.button1.TabIndex = 46;
            this.button1.Text = "ADD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.White;
            this.pictureBox6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox6.Image = global::ReUnifier.Properties.Resources.trash;
            this.pictureBox6.Location = new System.Drawing.Point(8, 162);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(26, 27);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 47;
            this.pictureBox6.TabStop = false;
            this.pictureBox6.Click += new System.EventHandler(this.pictureBox6_Click);
            // 
            // TriggerPanel
            // 
            this.TriggerPanel.Controls.Add(this.label5);
            this.TriggerPanel.Controls.Add(this.pictureBox6);
            this.TriggerPanel.Controls.Add(this.button1);
            this.TriggerPanel.Controls.Add(this.TagNameList);
            this.TriggerPanel.Controls.Add(this.triggerTag);
            this.TriggerPanel.Location = new System.Drawing.Point(8, 274);
            this.TriggerPanel.Name = "TriggerPanel";
            this.TriggerPanel.Size = new System.Drawing.Size(118, 195);
            this.TriggerPanel.TabIndex = 48;
            // 
            // DynamizationScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ReUnifier.Properties.Resources.Bg_1x;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(611, 519);
            this.Controls.Add(this.TriggerPanel);
            this.Controls.Add(this.ReadOnly);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.triggerType);
            this.Controls.Add(this.TagSelect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DynamizationUpdate);
            this.Controls.Add(this.DynamizationDelete);
            this.Controls.Add(this.DynamizationScriptTextBox);
            this.Controls.Add(this.Editor);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DynamizationScript";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Property Dynamization";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DynamizationScript_FormClosed);
            this.Load += new System.EventHandler(this.DynamizationScript_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.TriggerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button DynamizationDelete;
        private System.Windows.Forms.Button DynamizationUpdate;
        private System.Windows.Forms.Label Editor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox TagSelect;
        private System.Windows.Forms.RadioButton ReadOnly;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox DynamizationScriptTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox triggerType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox triggerTag;
        private System.Windows.Forms.ListBox TagNameList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Panel TriggerPanel;
    }
}