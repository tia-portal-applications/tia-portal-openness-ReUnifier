using Siemens.Engineering.HmiUnified.UI.Dynamization.Script;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReUnifier
{
    public delegate void TaskDelegate(string str);
    public partial class DynamizationScript : Form
    {
        public event TaskDelegate TaskEvent;
        private bool rvCheck = true;
        private bool changeSign = true;
        public DynamizationScript()
        {
            InitializeComponent();
        }
        //private readonly Form2 _form2 = new Form2();
        private void DynamizationScript_Load(object sender, EventArgs e)
        {
            DynamizationScriptTextBox.Enabled = false;
            TagSelect.Enabled = false;
            ReadOnly.Enabled = false;
            TriggerPanel.Visible = false;
            triggerType.Enabled = false;

            foreach (var tag in Program.HmiSoftware.Tags)
            {
                string valuesStr = tag.GetType().GetProperty("Name")?.GetValue(tag, null).ToString();
                if (TagSelect.Items.Contains(valuesStr) == false)
                {
                    TagSelect.Items.Add(valuesStr);
                    triggerTag.Items.Add(valuesStr);
                }
            }
            TagSelect.Text = "";
            comboBox1.Text = Program.DynamizationType+ " Dynamization";

            if (Program.DynamizationType =="Tag")
            {
                TagSelect.Text = Program.DynamizationStr.Split('-')[0];
                ReadOnly.Checked = bool.Parse(Program.DynamizationStr.Split('-')[1]);
                DynamizationScriptTextBox.Text = "";
                TagSelect.Enabled = true;
                ReadOnly.Enabled = true;
            }
            if(Program.DynamizationType == "ScriptCode")
            {
                DynamizationScriptTextBox.Text = Program.DynamizationStr;
                TagSelect.Text = "";
                ReadOnly.Checked = false;
                DynamizationScriptTextBox.Enabled = true;
                triggerType.Text = Program.DynamizationTrigger.Type.ToString();
                triggerType.Enabled = true;
                if (Program.DynamizationTrigger.Type.ToString()=="Tags")
                {
                    TriggerPanel.Visible = true;                    
                    List<string> triggerTags = (List<string>)Program.DynamizationTrigger.Tags;
                    TagNameList.Items.AddRange(triggerTags.ToArray());
                }
            }
            rvCheck = ReadOnly.Checked;
        }

        private void DynamizationUpdate_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "ScriptCode Dynamization")
            {
                Program.DynamizationType = "ScriptCode";
                Program.DynamizationStr = DynamizationScriptTextBox.Text;
                if(triggerType.Text=="Disabled" || triggerType.Text == "Tags-automatic")
                {
                    Program.DynamizationStr = "Disabled" + " &&& " + Program.DynamizationStr;
                }
                else if(triggerType.Text == "Tags")
                {
                    string tagName = "";
                    foreach(var s in TagNameList.Items)
                    {
                        if(tagName!="")
                        {
                            tagName = tagName + "#" + s.ToString();
                        }
                        else
                        {
                            tagName = s.ToString();
                        }
                    }
                    Program.DynamizationStr = "Tags" + " &&& " + tagName + " &&& " + Program.DynamizationStr;
                }
                else
                {
                    Program.DynamizationStr = triggerType.Text + " &&& " + Program.DynamizationStr;
                }
                TagSelect.Enabled = false;
                ReadOnly.Enabled = false;
            }
            else if (comboBox1.Text == "Tag Dynamization")
            {
                Program.DynamizationType = "Tag";
                Program.DynamizationStr = TagSelect.Text + "-" + ReadOnly.Checked.ToString();
            }
            else if (comboBox1.Text == "")
            {
                Program.DynamizationType = "";
                Program.DynamizationStr = "Dynamization_Delete";
            }
            else
            {
                MessageBox.Show("Unsupported type");
                return;
            }
            TaskEvent(Program.DynamizationStr);
            changeSign = false;
            this.Close();
        }
        
        private void ReadOnly_Click(object sender, EventArgs e)
        {

            if (rvCheck)
            {
                ReadOnly.Checked = false;
                rvCheck = false;
            }
            else
            {
                ReadOnly.Checked = true;
                rvCheck = true;
            }
        }

        private void DynamizationDelete_Click(object sender, EventArgs e)
        {
            Program.DynamizationType = "";
            Program.DynamizationStr = "Dynamization_Delete";
            TaskEvent(Program.DynamizationStr);
            changeSign = false;
            this.Close();
        }

        private void DynamizationScript_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(changeSign==true)
            {
                Program.setFontType = "";
                Program.DynamizationStr = "";
                Program.DynamizationType = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            triggerType.Enabled = false;
            TriggerPanel.Visible = false;
            if (comboBox1.Text == "Tag Dynamization")
            {
                DynamizationScriptTextBox.Enabled = false;
                TagSelect.Enabled = true;
                ReadOnly.Enabled = true;

            }
            else if (comboBox1.Text == "ScriptCode Dynamization")
            {
                DynamizationScriptTextBox.Enabled = true;
                TagSelect.Enabled = false;
                ReadOnly.Enabled = false;
                triggerType.Enabled = true;
                if(triggerType.Text == "Tags")
                {
                    TriggerPanel.Visible = true;
                }
            }
            else if((comboBox1.Text == ""))
            {
                DynamizationScriptTextBox.Enabled = false;
                TagSelect.Enabled = false;
                ReadOnly.Enabled = false;
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            int statementsCount = TagNameList.SelectedItems.Count;
            if (statementsCount > 0)
            {
                List<string> itemValues = new List<string>();
                for (int i = 0; i < statementsCount; i++)
                {
                    itemValues.Add(TagNameList.SelectedItems[i].ToString());
                }
                foreach (string item in itemValues)
                {
                    TagNameList.Items.Remove(item);
                }
            }
            else
            {
                TagNameList.Items.Clear();
            }
        }

        private void triggerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(triggerType.Text=="Tags")
            {
                TriggerPanel.Visible = true;
            }
            else
            {
                TriggerPanel.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tagNameStr = triggerTag.Text;
            if(TagNameList.Items.Contains(tagNameStr)==false)
            {
                TagNameList.Items.Add(tagNameStr);
            }
        }
    }
}
