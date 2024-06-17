using Siemens.Engineering;
using Siemens.Engineering.HmiUnified;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Text.RegularExpressions;
using Loggingtext;
using Siemens.Engineering.HmiUnified.UI.Shapes;
using Siemens.Engineering.HmiUnified.UI.Events;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design;
using Siemens.Engineering.Hmi.Screen;
using Siemens.Engineering.SW.Blocks;
using Siemens.Engineering.HmiUnified.UI.Base;
using Siemens.Engineering.HmiUnified.UI.Dynamization.Script;
using Siemens.Engineering.HmiUnified.UI.Enum;
using Siemens.Engineering.HmiUnified.UI.Screens;
using Siemens.Engineering.Hmi.Tag;
using Siemens.Engineering.HmiUnified.HmiTags;
using Siemens.Engineering.HW;

namespace ReUnifier
{
    public partial class Form2 : Form
    {
        public List<string> ListStatementsProperty = new List<string>();
        public List<string> ListNewStatementsProperty = new List<string>();
        public List<string> ListStatementsValue = new List<string>();
        public List<string> ListNewStatementsValue = new List<string>();
        public List<string> ListConditionProperty = new List<string>();
        public List<string> ListNewConditionProperty = new List<string>();
        public List<string> ListNewConditionValue = new List<string>();
        public List<string[]> ListCondition = new List<string[]>();
        public List<string> ListDeviceSelect = new List<string>();
        public IList<HmiSoftware> HmiSoftwareList;
        public PropertyInfo[] Properties = new PropertyInfo[] { };
        public string XmlPath = "C:\\ReUnifier\\HmiReUnifier.xml";
        private readonly InsertNewItem _insertNewItem = new InsertNewItem();
        public Project TiaPortalProject;
        public TiaPortal MyTiaPortal;
        public Image ConnectImage;
        public List<string> GroupName = new List<string>();
        public int ConditionPageNum;


        public Form2()
        {
            InitializeComponent();
        }
        public int ConditionNum;//no conditions
        void StatementValueStr(string str)
        {
            ScriptStr.Text = str;
            ScriptStr.Visible = true;
        }
        public string TxtStatusBoxSelect
        {
            get => this.comboBox1.Text;
            set => comboBox1.Text = value;
        }
        public string PicNameStr
        {
            get => this.OptionName.Text;
            set => OptionName.Text = value;
        }
        public string StatusBoxText
        {
            get => this.OptionName.Text;
            set => OptionName.Text = value;
        }
        public void GetXmlInformation(string xmlFilePath , string nodePath)
        {
            try
            {
                //Initialize an XML instance
                XmlDocument myXmlDoc = new XmlDocument();
                //Load XML file (the parameter is the path of XML file)
                myXmlDoc.Load(xmlFilePath);
                //Get the first node with matching name (selectsinglenode): the root node of this XML file
                //XmlNode rootNode = myXmlDoc.SelectSingleNode("HmiTags");
                XmlNode node = myXmlDoc.SelectSingleNode(nodePath   );
                if (node != null)
                {
                    String xmlDate = node.InnerText;
                    List<string> xmlDateSplit = xmlDate.Split(',').ToList();

                    StatementsValue.Text = "";
                    //Clear Combobox
                    StatementsValue.Items.Clear();
                    StatementsValue.Items.AddRange(xmlDateSplit.ToArray());
                }
            }
            catch
            {
                // ignored
            }
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OptionName.Text = "";
            OptionName.Enabled = true;
            StatementsProperty.Text = "";
            StatementsValue.Text = "";
            ConditionProperty.Text = "";
            ConditionProperty2.Text = "";
            ConditionValue.Text = "";
            OptionName.Items.Clear();
            OptionName.Items.Add(item: "HmiTags");
            OptionName.Items.Add(item: "HmiLoggingTags");
            OptionName.Items.Add(item: "HmiDiscreteAlarmTags");
            OptionName.Items.Add(item: "HmiAnalogAlarmTags");
            OptionName.Items.Add(item: "ScreenItems");
            OptionName.Items.Add(item: "ScreenProperties");

            if (TxtStatusBoxSelect == "DELETE")
            {
                OptionName.Text = @"ScreenItems";
                OptionName.Enabled = false;
                pictureBox3.Enabled = false;
            }
            else if (TxtStatusBoxSelect == "INSERT")
            {
                OptionName.Items.Remove(value: "ScreenItems");
                OptionName.Items.Remove(value: "ScreenProperties");
                pictureBox2.Enabled = false;
            }
            ComboBox2_SelectedIndexChanged(sender: sender, e: e);
        }
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comboboxTextStr = comboBox1.Text;
            string deviceTextStr = deviceSelect.Text;
            string optionTextStr = OptionName.Text;
            ParaReset();
            deviceSelect.Text = deviceTextStr;
            comboBox1.Text = comboboxTextStr;
            OptionName.Text = optionTextStr;
            StatementsProperty.Text = "";
            StatementsValue.Text = "";
            ConditionProperty.Text = "";
            ConditionProperty2.Text = "";
            ConditionValue.Text = "";
            StatementsProperty.Items.Clear();
            StatementsValue.Items.Clear();
            ConditionProperty.Items.Clear();
            ConditionValue.Items.Clear();
            label8.Visible = false;
            PicName.Visible = false;
            Properties = null;
            try
            {
                if (StatusBoxText == "ScreenItems")
                {

                    PicName.Show();
                    label8.Show();
                    PicName.Items.Clear();
                    PicName.Text = "";
                    pictureBox2.Enabled = true;
                    foreach (var screen in Program.GetScreens())
                    {
                        PicName.Items.Add(screen.Name);
                    }                    
                }
                else if (TxtStatusBoxSelect == "INSERT" && (StatusBoxText == "HmiTags" || StatusBoxText == "HmiDiscreteAlarmTags" || StatusBoxText == "HmiAnalogAlarmTags"))
                {
                    pictureBox2.Enabled = false;
                }
                else
                {
                    pictureBox2.Enabled = true;
                }
                if (StatusBoxText == "HmiTags")
                {
                    Properties = Program.HmiSoftware.Tags.First().GetType().GetProperties();
                }
                else if (StatusBoxText == "HmiDiscreteAlarmTags")
                {
                    Properties = Program.HmiSoftware.DiscreteAlarms.First().GetType().GetProperties();
                }
                else if (StatusBoxText == "HmiAnalogAlarmTags")
                {
                    Properties = Program.HmiSoftware.AnalogAlarms.First().GetType().GetProperties();
                }
                else if (StatusBoxText == "HmiLoggingTags")
                {
                    foreach (var tag in Program.HmiSoftware.Tags)
                    {
                        if (tag.LoggingTags.Count != 0)
                        {
                            Properties = tag.LoggingTags.First().GetType().GetProperties();
                            break;
                        }
                    }
                }
                else if (StatusBoxText == "ScreenProperties")
                {
                    Properties = Program.GetScreens().First().GetType().GetProperties();
                }
               if(OptionName.Text.Length>0)
                {
                    ScanScreenItemProperties();
                    ListConditionProperty.Clear();
                    BindConditionProperty();
                }
            }
            catch
            {
                // ignored
            }
        }


        

        private void testfunction()
        {

            Siemens.Engineering.HmiUnified.UI.Screens.HmiScreen screen = ((HmiSoftware)Program.HmiSoftware).Screens.Create("MyScreen");
            HmiCircle hmiCircle = screen.ScreenItems.Create<HmiCircle>("MYCircle");
            //Event Creation 
            PropertyEventHandler propertyEvent = hmiCircle.PropertyEventHandlers.Create("ToolTipText",PropertyEventType.Change);

        }
        private void StatementsProperty_TextUpdate(object sender, EventArgs e)//When the input box text changes, automatically match the names of attributes that may be suitable and add them to the drop-down list for selection.
        {
            StatementsValue.Text = "";
            //Clear Combobox
            this.StatementsProperty.Items.Clear();
            this.StatementsValue.Items.Clear();
            // Clear listNewStatementsProperty
            ListNewStatementsProperty.Clear();
            ListNewStatementsValue.Clear();
            //Foreach all data
            foreach (var item in ListStatementsProperty)
            {
                if (item.Contains(this.StatementsProperty.Text))
                {
                    //Match，Add to ListNewStatementsProperty
                    ListNewStatementsProperty.Add(item);
                }
            }
            //Add keywords that have been found to Combobox 
            this.StatementsProperty.Items.AddRange(ListNewStatementsProperty.ToArray());
            //Set cursor position
            this.StatementsProperty.SelectionStart = this.StatementsProperty.Text.Length;

            Cursor = Cursors.Default;

            this.StatementsProperty.DroppedDown = true;
        }
        private void BindConditionProperty()
        {
            if (OptionName.Text == @"ScreenItems")
            {
                //StatementsProperty.Items.Clear();
                ListConditionProperty.Add("Name");
                ListConditionProperty.Add("TYPE");
            }
            else if (OptionName.Text == @"ScreenProperties" && Properties.Count() > 0)
            {
                //StatementsProperty.Items.Clear();
                foreach (PropertyInfo p in Properties)
                {   // ConditionProperty.Items.Add(p.Name)
                    if (p.CanRead)
                    {
                        ListConditionProperty.Add(p.Name);
                    }
                }
            }
            else if (Properties.Count() > 0)
            {
                foreach (PropertyInfo p in Properties)
                {   // ConditionProperty.Items.Add(p.Name)
                    if (p.CanRead)
                    {
                        ListConditionProperty.Add(p.Name);
                    }
                }
            }
            if(OptionName.Text == @"HmiLoggingTags")
            {
                if(TxtStatusBoxSelect == "INSERT")
                {
                    ListConditionProperty.Clear();
                }
                ListConditionProperty.Add("Process tag");
            }
            ConditionProperty.Items.Clear();
            this.ConditionProperty.Items.AddRange(ListConditionProperty.ToArray());

        }

        private void ConditionProperty_TextUpdate(object sender, EventArgs e)
        {
            ConditionValue.Text = "";
            //Clear Combobox
            this.ConditionProperty.Items.Clear();
            this.ConditionValue.Items.Clear();
            // Clear listNewStatementsProperty
            ListNewConditionProperty.Clear();
            ListNewConditionValue.Clear();
            //Foreach all data
            foreach (var item in ListConditionProperty)
            {
                if (item.Contains(this.ConditionProperty.Text))
                {
                    //Match，Add to ListNewStatementsProperty
                    ListNewConditionProperty.Add(item);
                }
            }
            //Add keywords that have been found to Combobox 
            this.ConditionProperty.Items.AddRange(ListNewConditionProperty.ToArray());
            //Set cursor position
            this.ConditionProperty.SelectionStart = this.ConditionProperty.Text.Length;

            Cursor = Cursors.Default;

            this.ConditionProperty.DroppedDown = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBoxProName.Enabled = false;
            ConnectButton.Enabled = false;
            buttonExecute.BackColor = Color.FromArgb(0, 161, 209);
            ButtonPrevious.BackColor = Color.FromArgb(0, 161, 209);
            LogAndXmlOP.LogOut("Log", "Form Load" + " \n", 0);
            Form2_Load_Clear();
        }
        private void Form2_Load_Clear()
        {
            Program.ProOpened.Clear();
            Program.ProState = 0;
            OptionName.Items.Clear();
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

            buttonExecute.Visible = false;
            ButtonPrevious.Visible = false;

            ParaReset();
        }
        private void Project_Search(object sender, EventArgs e)
        {
            if (Program.HmiSoftware == null)
            {
                comboBoxProName.Text = @"Scanning TIA projects";
                int ProNum = 0;
                ProNum = TiaPortalDiagnostics();
                if (ProNum > 0)
                {
                    comboBoxProName.Text = @"Select a project from TIA";
                    comboBoxProName.Enabled = true;
                    ConnectImage = ReUnifier.Properties.Resources.Connect;
                    ConnectButton.Image = ReUnifier.Properties.Resources.Connect;
                    ConnectButton.Enabled = true;
                }
                else
                {
                    comboBoxProName.Text = @"No items retrieved";
                    comboBoxProName.Enabled = false;
                    ConnectButton.Enabled = false;
                    Program.HmiSoftware = null;
                }
                ParaReset();
            }else
            {
                MessageBox.Show("Please disconnect first!");
            }
           
        }
        private void ParaReset() //Relevant parameter reset
        {
            ConditionNum = 0;
            Program.OperationStr = "";
            Program.ConditionStr = "";
            Program.StatementStr = "";
            checklistkeep.Text = "";
            listBoxCheck.Text = "";
            checklistkeep.Visible = false;
            listBoxCheck.Visible = false;
            PicName.Text = "";
            PicName.Hide();
            label8.Hide();
            OptionName.Text = "";
            comboBox1.Text = "";
            StatementsBox.Items.Clear();
            PicboxReset();
            ScriptStr.Text = "";
            ScriptStr.Visible=false;
            Program.setFontType = "";
            if (Program.ProState > 0)
            {
                pictureBox1.Image = ReUnifier.Properties.Resources.inProgress;
                label2.Font = new Font(label2.Font, label2.Font.Style | FontStyle.Bold);
                pictureBox2.Enabled = true;
                pictureBox3.Enabled = true;
                pictureBox4.Enabled = true;
                Program.ProState = 1;
            }

            ConditionPageNum = 0;
            ListCondition.Clear();
            this.ConditionProperty.Items.Clear();
            this.ConditionValue.Items.Clear();
            ListNewConditionProperty.Clear();
            ListNewConditionValue.Clear();
            GroupName.Clear();
            CheckboxReset();
        }
        private void PicboxReset()
        {
            pictureBox1.Image = ReUnifier.Properties.Resources.Pending;
            pictureBox2.Image = ReUnifier.Properties.Resources.Pending;
            pictureBox3.Image = ReUnifier.Properties.Resources.Pending;
            pictureBox4.Image = ReUnifier.Properties.Resources.Pending;
            pictureBox5.Image = ReUnifier.Properties.Resources.Pending;

            pictureBoxline1.Image = ReUnifier.Properties.Resources.GrayLine;
            pictureBoxline2.Image = ReUnifier.Properties.Resources.GrayLine;
            pictureBoxline3.Image = ReUnifier.Properties.Resources.GrayLine;
            pictureBoxline4.Image = ReUnifier.Properties.Resources.GrayLine;
        }
        private void FontStyleReset()
        {
            label2.Font = new Font(label2.Font, label2.Font.Style & ~FontStyle.Bold);
            label10.Font = new Font(label10.Font, label10.Font.Style & ~FontStyle.Bold);
            label7.Font = new Font(label7.Font, label7.Font.Style & ~FontStyle.Bold);
            label11.Font = new Font(label11.Font, label11.Font.Style & ~FontStyle.Bold);
            label12.Font = new Font(label12.Font, label12.Font.Style & ~FontStyle.Bold);
        }
        public int TiaPortalDiagnostics()//add all the opened projects to the listbox
        {
            MyTiaPortal = new TiaPortal(TiaPortalMode.WithoutUserInterface);
            IList<TiaPortalProcess> tiaPortalProcesses = TiaPortal.GetProcesses();
            int ProNum = 0;
            foreach (TiaPortalProcess tiaPortalProcess in tiaPortalProcesses)
            {
                if (tiaPortalProcess.ProjectPath != null)
                {
                    string proNameStr = tiaPortalProcess.ProjectPath.ToString();
                    if(comboBoxProName.Items.Contains(proNameStr)==false)
                    {
                        comboBoxProName.Items.Add(proNameStr);
                        Program.ProOpened.Add(proNameStr, tiaPortalProcess);
                    }                   
                    ProNum++;
                }
            }
            return ProNum;
        }

        private int Execute(string type)//Return0:complete executing,but unsuccessfully. Return1:have not complete. Return2:complete successfully
        {
            int resultBool = 0;
            Program.SetStatements.Clear();
            /*
            foreach (string item in StatementsBox.Items)
            {
                string[] StatementsBoxArr = Regex.Split(item, "&&=&&", RegexOptions.IgnoreCase);
                Program.SetStatements.Add(StatementsBoxArr[0].Trim(), StatementsBoxArr[1].Trim());
            }
            */
            if(listBoxCheck.Text.Contains(" SET "))
            {
                string statementList = "" ;
                int indexSet = listBoxCheck.Text.IndexOf(" SET ", StringComparison.Ordinal) + 5;
                int strLength = listBoxCheck.Text.Length;
                if (listBoxCheck.Text.Contains(" WHERE "))
                {
                    int indexWhere = listBoxCheck.Text.IndexOf(" WHERE ", StringComparison.Ordinal);
                    statementList = listBoxCheck.Text.Substring(indexSet, indexWhere - indexSet);
                }
                else
                {
                    statementList = listBoxCheck.Text.Substring(indexSet);
                }
                string[] statementListArr = statementList.Split(new[] { "&$&" }, StringSplitOptions.None);
                foreach (string item in statementListArr)
                {
                    string[] StatementsBoxArr = item.Split('=');
                    Program.SetStatements.Add(StatementsBoxArr[0].Trim(), StatementsBoxArr[1].Trim());
                }
            }
            if (type == "executed" && comboBox1.Text == @"INSERT" && (Program.SetStatements.ContainsKey("Name") != true))
            {              
                MessageBox.Show(@"Missing name row in set list!");
                return 1;
            }
            if(Program.ScreenName=="")
            {
                Program.ScreenName = PicName.Text == "*" ? ".*" : PicName.Text;
            }
            else
            {
                Program.ScreenName = Program.ScreenName == "*" ? ".*" : Program.ScreenName;
            }
            
            Program.WhereConditionsStr = "";
            if (listBoxCheck.Text.Contains(" WHERE "))
            {
                int indexWhere = listBoxCheck.Text.IndexOf(" WHERE ", StringComparison.Ordinal);
                Program.WhereConditionsStr = listBoxCheck.Text.Substring((indexWhere + 7));
            }

            if (type == "executed")
            {            
                LogAndXmlOP.LogOut("Log", "Execute : " + listBoxCheck.Text + " \n", 0);
                Program.StrShow = Program.StrShow + "Execute : " + listBoxCheck.Text + " \n";
            }
            int conditionNum = 0;
            try
            {
                if (Program.HmiSoftware == null)
                {
                    throw new Exception("No WinCC Unified software found. Please add a WinCC Unified device and run this app again!");
                }
                else if (Program.OptionStr.StartsWith(@"INSERT") && Program.ScreenName.Contains("Screen") == false)
                {
                    _insertNewItem.InsertTags(type);
                }
                else if (Program.ScreenName == @"HmiLoggingTags")
                {
                    conditionNum = _insertNewItem.HandleHmiLoggingTags(type);
                }
                else if (Program.ScreenName == @"HmiTags")
                {
                    conditionNum = _insertNewItem.HandleHmiTags(type);
                }
                else if (Program.ScreenName == @"HmiDiscreteAlarmTags" || Program.ScreenName == @"HmiAnalogAlarmTags")
                {
                    conditionNum = _insertNewItem.HandleHmiAlarmTags(type);
                }
                else if (!string.IsNullOrWhiteSpace(Program.ScreenName))
                {
                    if (Program.ScreenName == "ScreenItems")
                    {
                        Program.ScreenName = Program.OptionStr.Split(new[] { "&$&" }, StringSplitOptions.None)[2];
                    }
                    conditionNum = _insertNewItem.UpdateScreen(type);
                }
                if (type == "executed")
                {
                    Program.StrShow = $"{Program.StrShow} Finished,\n";
                    resultBool = 2;
                }
            }
            catch (Exception ex)
            {
                Program.StrShow = $"{Program.StrShow}Failed:,{ ex.Message},\n";
                Program.StrShow = $"{Program.StrShow}Failed:,\n";
            }
            if (type == "executed")
            {
                listBoxCheck.Text = Program.StrShow;
            }
            else if (comboBox1.Text != @"INSERT")
            {
                label14.Text = @"The number of matching conditions is: " + conditionNum;
                if (comboBox1.Text == @"DELETE")
                {
                    listBoxCheck.Text = listBoxCheck.Text+ "\n"+"\n"+ label14.Text;
                }
            }
            return resultBool;
        }

        public void ButtonStatementAdd_Click(object sender, EventArgs e)
        {
            string statementsPropertyStr = StatementsProperty.Text;
            string statementsValueStr = StatementsValue.Text;
            if (Program.DynamizationType=="Tag" || Program.DynamizationType == "ScriptCode")
            {
                statementsValueStr = Program.DynamizationType + " &&& " + ScriptStr.Text;
            }
            if(Program.DynamizationStr== "Dynamization_Delete")
            {
                statementsValueStr = Program.DynamizationStr;
            }
            if(this.StatementsProperty.Text=="Events")
            {
                statementsValueStr = ScriptStr.Text;
            }
            if(Program.setFontType!="")
            {
                statementsPropertyStr = Program.setFontType;
            }
            if (StatementsBox.Items.Contains(statementsPropertyStr + " &&=&& " + statementsValueStr) == false)
            {
                foreach (string item in StatementsBox.Items)
                {
                    if (item.Contains(statementsPropertyStr))
                    {
                        MessageBox.Show(@"Property settings already exist! Please delete the existing property settings first.");
                        return;
                    }
                }
                StatementsBox.Items.Add(statementsPropertyStr + " &&=&& " + statementsValueStr);
            }
            ScriptStr.Text = "";
            ScriptStr.Visible = false;
        }
        private void buttonStatementsClear_Click(object sender, EventArgs e)
        {
            int statementsCount = StatementsBox.SelectedItems.Count;
            if (statementsCount > 0)
            {
                List<string> itemValues = new List<string>();
                for (int i = 0; i < statementsCount; i++)
                {
                    itemValues.Add(StatementsBox.SelectedItems[i].ToString());
                }
                foreach (string item in itemValues)
                {
                    StatementsBox.Items.Remove(item);
                }
            }
            else
            {
                StatementsBox.Items.Clear();
            }
        }
        private void StatementsProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            StatementsValue.Text = "";
            StatementsValue.Items.Clear();
            panel4.Visible = false;
            colorSelector1.Visible = false;
            button2.Visible = false;
            panelfont.Visible = false;
            ScriptStr.Visible = false;
            ScriptStr.Text = "";
            Program.DynamizationStr = "";
            Program.DynamizationType = "";
            if (StatementsProperty.Text == @"DataType")
            {
                GetXmlInformation(XmlPath, "//Objects/HmiTags/DataType");
            }
            else if (StatementsProperty.Text == @"LoggingMode")
            {
                GetXmlInformation(XmlPath, "//Objects/LoggingTags/LoggingMode");
            }
            else if (StatementsProperty.Text.Contains("Color"))
            {
                panel4.Visible = true;
                if (StatusBoxText == "ScreenItems" || StatusBoxText == "ScreenProperties")
                {
                    this.StatementsValue.Items.Add("Dynamization");
                }
            }
            else if (StatementsProperty.Text.Contains("Font"))
            {
                button2.Visible = true;
                Font_Size.Items.Clear();
                this.Font_Size.Items.Add("12");
                this.Font_Size.Items.Add("Dynamization");
            }
            else if (StatementsProperty.Text == "Events")
            {
                Program.EventList.Clear();
                if (StatusBoxText == "ScreenItems")
                {
                    foreach (var screenItem in Program.ItemList)
                    {
                        IEngineeringObject itemConcreto = screenItem;
                        foreach (IEngineeringObject eveHandItem in itemConcreto.GetComposition("EventHandlers") as IEngineeringComposition)
                        {

                            // IEngineeringObject script = eveHandItem.GetAttribute("Script") as IEngineeringObject;
                            string eveType = eveHandItem.GetAttribute("EventType").ToString();
                            if (Program.EventList.Contains(eveType) == false)
                            {
                                Program.EventList.Add(eveType);
                            }
                        }
                    }
                }
                if (StatusBoxText == "ScreenProperties")
                {
                    // string screenNameStr = PicName.Text.Trim();
                    foreach (var screen in Program.conditionScreenList)
                    {
                        foreach (HmiScreenEventHandler eveHandler in screen.EventHandlers)
                        {
                            IEngineeringObject script = eveHandler.GetAttribute("Script") as IEngineeringObject;
                            string eveType = eveHandler.GetAttribute("EventType").ToString();
                            if (Program.EventList.Contains(eveType) == false)
                            {
                                Program.EventList.Add(eveType);
                            }
                        }
                    }
                }
                Event eventForm = new Event();
                eventForm.TaskEvent += new EventTaskDelegate(StatementValueStr);
                eventForm.ShowDialog();
            }           
            else
            {
                if (StatusBoxText == "ScreenItems")
                {
                    if (StatementsProperty.Text == "HiddenInput" || StatementsProperty.Text == "AcceptOnDeactivated")
                    {
                        this.StatementsValue.Items.Add("True");
                        this.StatementsValue.Items.Add("False");
                    }
                    Regex rx = new Regex(PicName.Text);
                    foreach (var screen in Program.GetScreens())
                    {
                        if (!rx.Match(screen.Name).Success)
                        {
                            continue;
                        }
                        foreach (var screenItem in screen.ScreenItems)
                        {
                            if (ContainProperty(screenItem, StatementsProperty.Text))//Read the value of the selected attribute from all eligible items of the specified screen
                            {
                                var valuesStr="";
                                
                                valuesStr = screenItem.GetType().GetProperty(StatementsProperty.Text)?.GetValue(screenItem, null).ToString();
                                if (this.StatementsValue.Items.Contains(valuesStr) == false && valuesStr != "")
                                {
                                    if (valuesStr != null) this.StatementsValue.Items.Add(valuesStr);
                                }
                            }
                        }
                    }
                }
                else
                {
                    List<string> listValue = PropertyItemsChoose(StatementsProperty.Text);
                    this.StatementsValue.Items.AddRange(listValue.ToArray());
                }
                if (StatusBoxText == "ScreenItems" || StatusBoxText == "ScreenProperties")
                {
                    this.StatementsValue.Items.Add("Dynamization");
                }
            }
        }
        private void StatementsValue_TextUpdate(object sender, EventArgs e)
        {
            //Clear Combobox
            this.StatementsValue.Items.Clear();
            // Clear listNewStatementsValue
            ListNewStatementsValue.Clear();
            //Foreach all data
            foreach (var item in ListStatementsValue)
            {
                if (item.Contains(this.StatementsValue.Text))
                {
                    //Match，Add to ListNewStatementsValue
                    ListNewStatementsValue.Add(item);
                }
            }
            //Add keywords that have been found to Combobox 
            this.StatementsValue.Items.AddRange(items: ListNewStatementsValue.ToArray());
            //Set cursor position
            this.StatementsValue.SelectionStart = this.StatementsValue.Text.Length;

            Cursor = Cursors.Default;

            this.StatementsValue.DroppedDown = true;
        }

        private void ConditionProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConditionProperty2.Text = "";
            ConditionProperty2.Enabled = false;
            ConditionValue.Text = "";
            ConditionValue.Items.Clear();

            List<string> listValue = PropertyItemsChoose(ConditionProperty.Text);
            this.ConditionValue.Items.AddRange(listValue.ToArray());
            if ((OptionName.Text == @"ScreenItems" && ConditionProperty.Text == @"TYPE") || ConditionProperty.Text.Contains("Color"))
            {
                ConditionProperty2.Text = @"="; //Fuzzy query not allowed
            }
            else
            {
                ConditionProperty2.Enabled = true;
            }
        }
        public List<string> PropertyItemsChoose(string propertyName)
        {
            List<string> propertyNameList = new List<string>();
            string valuesStr ;
            if (StatusBoxText == "ScreenItems")
            {
                Regex rx = new Regex(PicName.Text);
                foreach (var screen in Program.GetScreens())
                {
                    if (!rx.Match(screen.Name).Success)
                    {
                        continue;
                    }
                    foreach (var screenItem in screen.ScreenItems)
                    {
                        if (ConditionProperty.Text == @"TYPE")
                        {
                            string itemType = screenItem.ToString().Split('.').Last();
                            if (propertyNameList.Contains(itemType) == false)
                            {
                                propertyNameList.Add(itemType);
                            }
                        }
                        else
                        {
                            if (ContainProperty(screenItem, propertyName))
                            {
                                valuesStr = screenItem.GetType().GetProperty(propertyName)?.GetValue(screenItem, null).ToString();
                                if (propertyNameList.Contains(valuesStr) == false)
                                {
                                    propertyNameList.Add(valuesStr);
                                }
                            }
                        }
                    }
                }

            }
            else if (StatusBoxText == "ScreenProperties")
            {
                foreach (var screen in Program.GetScreens())
                {
                    valuesStr = screen.GetType().GetProperty(propertyName)?.GetValue(screen, null).ToString();
                    if (propertyNameList.Contains(valuesStr) == false)
                    {
                        propertyNameList.Add(valuesStr);
                    }
                }
            }
            else if (StatusBoxText == "HmiTags")
            {
                foreach (var tag in Program.HmiSoftware.Tags)
                {
                    valuesStr = tag.GetType().GetProperty(propertyName)?.GetValue(tag, null).ToString();
                    if (propertyNameList.Contains(valuesStr) == false)
                    {
                        propertyNameList.Add(valuesStr);
                    }
                }
            }
            else if (StatusBoxText == "HmiDiscreteAlarmTags")
            {
                foreach (var tag in Program.HmiSoftware.DiscreteAlarms)
                {
                    valuesStr = tag.GetType().GetProperty(propertyName)?.GetValue(tag, null).ToString();
                    if (propertyNameList.Contains(valuesStr) == false)
                    {
                        propertyNameList.Add(valuesStr);
                    }
                }
            }
            else if (StatusBoxText == "HmiAnalogAlarmTags")
            {
                foreach (var tag in Program.HmiSoftware.AnalogAlarms)
                {
                    valuesStr = tag.GetType().GetProperty(propertyName)?.GetValue(tag, null).ToString();
                    if (propertyNameList.Contains(valuesStr) == false)
                    {
                        propertyNameList.Add(valuesStr);
                    }
                }
            }
            else if (StatusBoxText == "HmiLoggingTags")
            {
                foreach (var tag in Program.HmiSoftware.Tags)
                {
                    if(propertyName == "Process tag")
                    {
                        valuesStr = tag.Name.ToString();
                        if (propertyNameList.Contains(valuesStr) == false)
                        {
                            propertyNameList.Add(valuesStr);
                        }
                    }
                    else
                    {
                        foreach (var logTag in tag.LoggingTags)
                        {
                            valuesStr = logTag.GetType().GetProperty(propertyName)?.GetValue(logTag, null).ToString();
                            if (propertyNameList.Contains(valuesStr) == false)
                            {
                                propertyNameList.Add(valuesStr);
                            }
                        }
                    }
                    
                }
            }
            return propertyNameList;
        }
        private bool ContainProperty(object instance, string propertyName)
        {
            if (instance != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo foundPropertyInfo = instance.GetType().GetProperty(propertyName);
                return (foundPropertyInfo != null);
            }
            return false;
        }

        public IEnumerable<HmiSoftware> GetHmiSoftware(ref TiaPortal tiaPortal, TiaPortalProcess tiaPortalProcess)
        {
            if (tiaPortal == null) throw new ArgumentNullException(nameof(tiaPortal));
            tiaPortal = tiaPortalProcess.Attach();
            TiaPortalProject = tiaPortal.Projects[0];
            var allDevice = GetDevice(TiaPortalProject);

            return
          //  from device in TiaPortalProject.Devices
            from device in allDevice
            from deviceItem in device.DeviceItems       
            let softwareContainer = deviceItem.GetService<Siemens.Engineering.HW.Features.SoftwareContainer>()
            where softwareContainer?.Software is HmiSoftware
            select softwareContainer.Software as HmiSoftware;
        }

        public IEnumerable<Device> GetDevice(Project project)
        {
            var allDevice = project.Devices.ToList();
            if(project.DeviceGroups.Count>0)
            {
                foreach (var devicegroup in project.DeviceGroups)
                {
                    if(devicegroup.Devices.Count>0)
                    {
                        foreach (var device in devicegroup.Devices)
                        {
                            allDevice.Add(device);
                        }
                    }
                }
            }
            return allDevice;
        }

        private void StatementsBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (StatementsBox.IndexFromPoint(e.X, e.Y) == -1) StatementsBox.ClearSelected();//click the empty
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void PicName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Regex rx = new Regex(PicName.Text);
            StatementsProperty.Items.Clear();
            ConditionProperty.Items.Clear();
            Program.ItemList.Clear();
            foreach (var screen in Program.GetScreens())
            {
                if (!rx.Match(screen.Name).Success)
                {
                    continue;
                }
                foreach (var screenItem in screen.ScreenItems)
                {
                    Properties = screenItem.GetType().GetProperties();
                    if (Properties.Length > 0)
                    {                      
                        ListConditionProperty.Clear();
                        BindConditionProperty();
                    }
                }
            }
            if(checklistkeep.Text.Contains(" WHERE "))
            {
                Execute("condition");
                ScanScreenItemProperties();
            }
        }
        private void ConditionValue_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ColorPanel_Click(object sender, EventArgs e)
        {
            colorSelector1.PaintControl = panel4;
            if (colorSelector1.Visible)
            {
                colorSelector1.Visible = false;
            }
            else
            {
                colorSelector1.Visible = true;
            }
        }

        private void BackColorChanged_Click(object sender, EventArgs e)
        {
            var testColor = panel4.BackColor;
            Program.StrColorSet = ColorTranslator.ToHtml(testColor);
            StatementsValue.Text = Program.StrColorSet;
        }

        private void StatementsBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                StatementsBox.Focus();
            }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E && e.Control)
            {
                e.Handled = true;
                Execute("executed");
            }
        }
        private void Step_Click(object sender, EventArgs e)//After connected the project, erery steps will jump to this function first
        {
            if (Program.ProState >= 1) //if the project is disconnected ,we can not go any step
            {
                Control button = (Control)sender;
                string bName = button.Name;

                checklistkeep.Visible = false;
                buttonExecute.Visible = true;
                buttonExecute.Text = @"Next";
                buttonExecute.Enabled = true;
                listBoxCheck.Visible = false;
                ButtonPrevious.Visible = true;
                StepExecute();
                panel1.Visible = false;
                panel2.Visible = false;
                colorSelector1.Visible = false;
                panel3.Visible = false;
                PicboxReset();

                if (bName == "buttonExecute" && label12.Font.Bold) // if we new a tast, we should reset parameters first
                {
                    ParaReset();
                }
                if (Program.ProState > 4)                  //change every picbox as the project statement
                {
                    pictureBox1.Image = ReUnifier.Properties.Resources.Done;
                    pictureBox2.Image = ReUnifier.Properties.Resources.Done;
                    pictureBox3.Image = ReUnifier.Properties.Resources.Done;
                    pictureBox4.Image = ReUnifier.Properties.Resources.Done;
                    pictureBoxline1.Image = ReUnifier.Properties.Resources.GreenLine;
                    pictureBoxline2.Image = ReUnifier.Properties.Resources.GreenLine;
                    pictureBoxline3.Image = ReUnifier.Properties.Resources.GreenLine;
                    pictureBoxline4.Image = ReUnifier.Properties.Resources.GreenLine;
                }
                else if (Program.ProState > 3)
                {
                    pictureBox1.Image = ReUnifier.Properties.Resources.Done;
                    pictureBox2.Image = ReUnifier.Properties.Resources.Done;
                    pictureBox3.Image = ReUnifier.Properties.Resources.Done;
                    pictureBoxline1.Image = ReUnifier.Properties.Resources.GreenLine;
                    pictureBoxline2.Image = ReUnifier.Properties.Resources.GreenLine;
                    pictureBoxline3.Image = ReUnifier.Properties.Resources.GreenLine;
                }
                else if (Program.ProState > 2)
                {
                    pictureBox1.Image = ReUnifier.Properties.Resources.Done;
                    pictureBox2.Image = ReUnifier.Properties.Resources.Done;
                    pictureBoxline1.Image = ReUnifier.Properties.Resources.GreenLine;
                    pictureBoxline2.Image = ReUnifier.Properties.Resources.GreenLine;
                }
                else if (Program.ProState > 1)
                {
                    pictureBox1.Image = ReUnifier.Properties.Resources.Done;
                    pictureBoxline1.Image = ReUnifier.Properties.Resources.GreenLine;
                }

                if (bName == "pictureBox1" || (bName == "ButtonPrevious" && label7.Font.Bold) || bName == "ConnectButton" || (bName == "buttonExecute" && label12.Font.Bold))
                {
                    FontStyleReset();
                    checklistkeep.Visible = false;
                    panel1.Visible = true;
                    ButtonPrevious.Visible = false;
                    pictureBox1.Image = ReUnifier.Properties.Resources.inProgress;
                    label2.Font = new Font(label2.Font, label2.Font.Style | FontStyle.Bold);
                }
                else if (bName == "pictureBox2" || (bName == "buttonExecute" && label2.Font.Bold) || (bName == "ButtonPrevious" && label10.Font.Bold))
                {                   
                    if (checklistkeep.Text.Trim().Contains("INSERT") && (StatusBoxText == "HmiTags" || StatusBoxText == "HmiDiscreteAlarmTags" || StatusBoxText == "HmiAnalogAlarmTags") && (bName == "buttonExecute" && label2.Font.Bold))
                    {
                        Step_Click(pictureBox3, e);
                        return;
                    }
                    else if (checklistkeep.Text.Trim().Contains("INSERT") && (StatusBoxText == "HmiTags" || StatusBoxText == "HmiDiscreteAlarmTags" || StatusBoxText == "HmiAnalogAlarmTags") && (bName == "ButtonPrevious" && label10.Font.Bold))
                    {
                        Step_Click(pictureBox1, e);
                        return;
                    }
                    FontStyleReset();
                    checklistkeep.Visible = true;
                    panel3.Visible = true;
                    panel3.Enabled = false;
                    label7.Font = new Font(label7.Font, label7.Font.Style | FontStyle.Bold);
                    pictureBox2.Image = ReUnifier.Properties.Resources.inProgress;
                    if (Program.ProState > 1)
                    {
                        panel3.Enabled = true;
                    }
                    ConditionPageNum = 0;
                    ConditionRefresh("");
                    PictureChange();
                }
                else if (bName == "pictureBox3" || (bName == "buttonExecute" && label7.Font.Bold) || (bName == "ButtonPrevious" && label11.Font.Bold))
                {                    
                    if (checklistkeep.Text.Trim().StartsWith("DELETE") && (bName == "buttonExecute" && label7.Font.Bold))
                    {
                        Step_Click(pictureBox4, e);
                        Execute("condition");
                        return;
                    }
                    else if (checklistkeep.Text.Trim().StartsWith("DELETE") && (bName == "ButtonPrevious" && label11.Font.Bold))
                    {
                        Step_Click(pictureBox2, e);
                        return;
                    }
                    FontStyleReset();
                    panel2.Visible = true;
                    panel2.Enabled = false;
                    panelfont.Visible = false;
                    button2.Visible = false;
                    label10.Font = new Font(label10.Font, label10.Font.Style | FontStyle.Bold);
                    pictureBox3.Image = ReUnifier.Properties.Resources.inProgress;
                    checklistkeep.Visible = true;
                    if (Program.ProState > 2)
                    {
                        panel2.Enabled = true;
                    }
                }
                else if (bName == "pictureBox4" || (bName == "buttonExecute" && label10.Font.Bold) || (bName == "ButtonPrevious" && label12.Font.Bold))
                {
                    FontStyleReset();
                    checklistkeep.Visible = false;
                    buttonExecute.Text = @"Execute";
                    listBoxCheck.Visible = true;
                    buttonExecute.Enabled = false;
                    label11.Font = new Font(label11.Font, label11.Font.Style | FontStyle.Bold);
                    pictureBox4.Image = ReUnifier.Properties.Resources.inProgress;
                    /*
                    if (Program.ProState > 3)
                    {
                        buttonExecute.Enabled = true;
                    }
                    */
                    buttonExecute.Enabled = true;
                }
                else if (bName == "buttonExecute" && label11.Font.Bold)
                {
                    FontStyleReset();
                    DeviceSelect_SelectedIndexChanged(sender, e);
                    if (Execute("executed") == 1)
                    {
                        return;
                    }
                    checklistkeep.Visible = false;
                    buttonExecute.Text = @"New";
                    ButtonPrevious.Visible = false;
                    listBoxCheck.Visible = true;
                    label12.Font = new Font(label12.Font, label12.Font.Style | FontStyle.Bold);
                    pictureBox5.Image = ReUnifier.Properties.Resources.inProgress;
                    if (Program.ProState == 4)
                    {
                        Program.ProState = 5; 
                        pictureBox1.Image = ReUnifier.Properties.Resources.Done;
                        pictureBox2.Image = ReUnifier.Properties.Resources.Done;
                        pictureBox3.Image = ReUnifier.Properties.Resources.Done;
                        pictureBox4.Image = ReUnifier.Properties.Resources.Done;
                        pictureBoxline1.Image = ReUnifier.Properties.Resources.GreenLine;
                        pictureBoxline2.Image = ReUnifier.Properties.Resources.GreenLine;
                        pictureBoxline3.Image = ReUnifier.Properties.Resources.GreenLine;
                        pictureBoxline4.Image = ReUnifier.Properties.Resources.GreenLine;
                    }
                }

            }
        }
        private void StepExecute()// Put in the option informations; put in the conditions; put in the set informations
        {
            string unitStr = "";
            if (panel1.Visible && panel2.Visible == false && panel3.Visible == false) //Put in the option informations
            {
                if (comboBox1.Text.Length > 0 && OptionName.Text.Length > 0)
                {
                    if (OptionName.Text != @"ScreenItems")
                    {
                        unitStr = comboBox1.Text + " &$& " + OptionName.Text;
                    }
                    else
                    {
                        if (PicName.Text.Length == 0)
                        {
                            return;
                        }
                        unitStr = comboBox1.Text + " &$& " + OptionName.Text + " &$& " + PicName.Text;
                    }
                    if (Program.ProState <= 2)
                    {
                        if (TxtStatusBoxSelect == "INSERT" && (StatusBoxText == "HmiTags" || StatusBoxText == "HmiDiscreteAlarmTags" || StatusBoxText == "HmiAnalogAlarmTags"))
                        {
                            Program.ProState = 3;
                        }
                        else
                        {
                            Program.ProState = 2;
                        }
                    }

                    Program.OperationStr = "DEVICE " + deviceSelect.Text + "&$&" + unitStr;
                    checklistkeep.Text = Program.OperationStr;
                    if (Program.StatementStr.Length > 0)
                    {
                        checklistkeep.Text = Program.OperationStr + @" SET " + Program.StatementStr;
                    }
                    if (Program.ConditionStr.Length > 0)
                    {
                        checklistkeep.Text = Program.OperationStr + @" WHERE " + Program.ConditionStr;
                    }

                    listBoxCheck.Text = checklistkeep.Text;
                }
                Program.OptionStr = comboBox1.Text + " " + OptionName.Text;
            }
            else if (panel1.Visible == false && panel2.Visible && panel2.Enabled && panel3.Visible == false)//put in the set informations
            {
                Program.SetStatements.Clear();
                if (StatementsBox.Items.Count > 0)
                {
                    foreach (string item in StatementsBox.Items)
                    {
                        string[] StatementsBoxArr = Regex.Split(item, "&&=&&", RegexOptions.IgnoreCase);
                        Program.SetStatements.Add(StatementsBoxArr[0].Trim(), StatementsBoxArr[1].Trim());
                        if (unitStr == "")
                        {
                            unitStr = item.Replace("&&=&&", " = ");
                        }
                        else
                        {
                            unitStr = unitStr + "&$&" + item.Replace("&&=&&", " = ");
                        }
                    }
                    Program.StatementStr = unitStr;
                    checklistkeep.Text = Program.OperationStr + @" SET " + Program.StatementStr;
                    if (Program.ConditionStr.Length > 0)
                    {
                        checklistkeep.Text = Program.OperationStr + @" SET " + Program.StatementStr + @" WHERE " + Program.ConditionStr;
                    }

                    if (Program.ProState < 4)
                    {
                        Program.ProState = 4;
                    }

                    listBoxCheck.Text = checklistkeep.Text;

                }
            }
            else if (panel1.Visible == false && panel2.Visible == false && panel3.Visible && panel3.Enabled )//put in the conditions
            {
                if (ConditionNum > 0)
                {
                    if (ConditionNum == 1)
                    {
                        unitStr = checkboxtext1.Text;
                    }
                    else
                    {
                        string groupNameOne = "";

                        foreach (string[] stringArr in ListCondition)
                        {
                            string logicStr = "";
                            if (stringArr[1].Length > 0)
                            {
                                string conditionUnit;
                                if (stringArr[1].Contains("And&$&"))
                                {
                                    logicStr = "And";
                                    conditionUnit = stringArr[1].Replace("And&$&", "");
                                }
                                else if (stringArr[1].Contains("Or&$&"))
                                {
                                    logicStr = "Or";
                                    conditionUnit = stringArr[1].Replace("Or&$&", "");
                                }
                                else
                                {
                                    conditionUnit = stringArr[1];
                                }
                                if (stringArr[0] == "0")//0:no grouped;1:grouped,index0:if grouped;index1:condition;index2:groupname
                                {
                                    if (groupNameOne == "")
                                    {
                                        unitStr = unitStr + stringArr[1] + " &$& ";
                                    }
                                    else
                                    {
                                        unitStr = unitStr + " ) &$& " + stringArr[1] + " &$& ";
                                        groupNameOne = "";
                                    }

                                }
                                else
                                {
                                    if (groupNameOne == "")
                                    {
                                        groupNameOne = stringArr[2];
                                        unitStr = unitStr + logicStr + " &$&( &$& " + conditionUnit + " &$& ";
                                    }
                                    else
                                    {
                                        if (groupNameOne == stringArr[2])
                                        {
                                            unitStr = unitStr + stringArr[1] + " &$& ";
                                            if (ListCondition.IndexOf(stringArr) == (ListCondition.Count - 1))
                                            {
                                                unitStr = unitStr + " ) &$& ";
                                            }
                                        }
                                        else
                                        {
                                            unitStr = unitStr + " ) &$& " + logicStr + " &$& ( &$& " + conditionUnit + " &$& ";
                                            groupNameOne = stringArr[2];
                                        }
                                    }

                                }
                            }
                        }
                    }
                    if (Program.ProState <= 3)
                    {
                        if (!checklistkeep.Text.Trim().StartsWith("DELETE"))
                        {
                            Program.ProState = 3;
                        }
                        else
                        {
                            Program.ProState = 4;
                        }
                    }
                    Program.ConditionStr = unitStr;
                    checklistkeep.Text = $@"{Program.OperationStr} WHERE {Program.ConditionStr}";
                    if (Program.StatementStr.Length > 0)
                    {
                        checklistkeep.Text = $@"{Program.OperationStr} SET {Program.StatementStr} WHERE {Program.ConditionStr}";
                    }
                    listBoxCheck.Text = checklistkeep.Text;
                    Execute("condition");
                    ScanScreenItemProperties();
                }
            }
        }
        
        private void ScanScreenItemProperties()
        {
            StatementsProperty.Items.Clear();
            ListStatementsProperty.Clear();
            
            if(StatusBoxText == "ScreenItems" && Program.ItemList.Count>0)
            {
                ListStatementsProperty.Add("Events");
                foreach (var screenItem in Program.ItemList)
                {
                    Properties = screenItem.GetType().GetProperties();
                    if (Properties.Length > 0)
                    {
                        BindStatementsProperty();
                    }
                }
            }
            else
            {
                BindStatementsProperty();
            } 
            if(StatusBoxText == "ScreenProperties")
            {
                ListStatementsProperty.Add("Events");
            }
            this.StatementsProperty.Items.AddRange(ListStatementsProperty.ToArray());
        }
        private void BindStatementsProperty()
        {
            if (Properties.Count() > 0)
            {
                foreach (PropertyInfo p in Properties)
                {
                    // ConditionProperty.Items.Add(p.Name);
                    if (p.Name == "Font" && ListStatementsProperty.Contains(p.Name) == false)
                    {
                        ListStatementsProperty.Add("Font");
                    }
                    else if(p.DeclaringType.Name=="HmiTextBox" && ListStatementsProperty.Contains("Text") == false)
                    {
                        ListStatementsProperty.Add("Text");
                    }
                    else if(p.DeclaringType.Name == "HmiIOField")
                    {
                        if(ListStatementsProperty.Contains("HiddenInput")== false)
                        {
                            ListStatementsProperty.Add("HiddenInput");
                        }
                        if(ListStatementsProperty.Contains("AcceptOnDeactivated") == false)
                        {
                            ListStatementsProperty.Add("AcceptOnDeactivated");
                        }
                        
                    }
                    else if (p.CanWrite && ListStatementsProperty.Contains(p.Name) == false)
                    {
                        ListStatementsProperty.Add(p.Name);
                    }
                } 
            }
        }
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            string projectPath = comboBoxProName.Text;
            ListDeviceSelect.Clear();
            this.deviceSelect.Items.Clear();

            if (Program.HmiSoftware == null) // if the running software have not connect a project, we should connect a project first
            {
                try
                {
                    TiaPortalProcess tiaPortalProcessOld = Program.ProOpened[projectPath];
                    foreach (TiaPortalSession session in tiaPortalProcessOld.AttachedSessions)
                    {
                        session.Dispose();
                    }
                    if (Program.ProOpened.ContainsKey(projectPath))
                    {
                        TiaPortalProcess tiaPortalProcess = Program.ProOpened[projectPath];
                        Program.HmiSoftware = GetHmiSoftware(ref MyTiaPortal, tiaPortalProcess).FirstOrDefault();
                        LogAndXmlOP.LogOut($"Log", $"Project select : {projectPath} \n", 0);
                        this.deviceSelect.Text = Program.HmiSoftware?.Name;
                        HmiSoftwareList = GetHmiSoftware(ref MyTiaPortal, tiaPortalProcess).ToArray();
                        
                        if (HmiSoftwareList.Count > 0)
                        {
                            foreach (HmiSoftware hmiSoftwareSelect in HmiSoftwareList)
                            {
                                ListDeviceSelect.Add(hmiSoftwareSelect.Name);
                            }
                        }
                    }
                    Program.ProState = 1;
                    Step_Click(sender, e);
                }
                catch (Exception ex)
                {
                    // ReSharper disable once LocalizableElement
                    comboBoxProName.Text = $"{projectPath} Error while opening project" + ex.Message;
                    LogAndXmlOP.LogOut("Log", $"{projectPath} Error while opening project {ex.Message}\n", 0);
                    return;
                }
                comboBoxProName.Enabled = false;
                ConnectButton.Image = ReUnifier.Properties.Resources.Connected;
                ConnectImage = ReUnifier.Properties.Resources.Connected;
                Program.ProState = 1;

                this.deviceSelect.Items.AddRange(ListDeviceSelect.ToArray());
            }
            else          //If the running software is connecting a project, we should disconnect
            {
                comboBoxProName.Enabled = true;
                Program.HmiSoftware = null;
                comboBoxProName.Items.Clear();
                comboBoxProName.SelectedItem = null;
                Form2_Load_Clear();
            }
        }

        private void ConnectButton_MouseEnter(object sender, EventArgs e)
        {
            if (Program.HmiSoftware != null)
            {
                // ReSharper disable once RedundantNameQualifier
                ConnectButton.Image = ReUnifier.Properties.Resources.hover_Disconnect;
            }
        }

        private void ConnectButton_MouseLeave(object sender, EventArgs e)
        {
            if (Program.HmiSoftware != null)
            {
                ConnectButton.Image = ConnectImage;
            }else
            {
                ConnectButton.Image = ReUnifier.Properties.Resources.Connect;
            }
        }
        private void CheckboxReset()  //reset the checkboxs in the condition panel
        {
            checkboxtext1.Text = "";
            checkboxtext1.Visible = false;
            Group1.Text = "";
            Group1.Visible = false;
            checkBox1.Checked = false;
            checkBox1.Visible = false;

            checkboxtext2.Text = "";
            checkboxtext2.Visible = false;
            Group2.Text = "";
            Group2.Visible = false;
            checkBox2.Checked = false;
            checkBox2.Visible = false;

            checkboxtext3.Text = "";
            checkboxtext3.Visible = false;
            Group3.Text = "";
            Group3.Visible = false;
            checkBox3.Checked = false;
            checkBox3.Visible = false;

            checkboxtext4.Text = "";
            checkboxtext4.Visible = false;
            Group4.Text = "";
            Group4.Visible = false;
            checkBox4.Checked = false;
            checkBox4.Visible = false;

            checkboxtext5.Text = "";
            checkboxtext5.Visible = false;
            Group5.Text = "";
            Group5.Visible = false;
            checkBox5.Checked = false;
            checkBox5.Visible = false;
        }
        private void PictureBoxDelete_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ConditionDelete(checkboxtext1.Text, "delete");
                Group1.Text = "";
                checkboxtext1.Text = "";
                checkboxtext1.Visible = false;
                Group1.Visible = false;
                checkBox1.Checked = false;
                checkBox1.Visible = false;
                ConditionNum = ConditionNum - 1;
            }
            if (checkBox2.Checked)
            {
                ConditionDelete(checkboxtext2.Text, "delete");
                Group2.Text = "";
                checkboxtext2.Text = "";
                checkboxtext2.Visible = false;
                Group2.Visible = false;
                checkBox2.Checked = false;
                checkBox2.Visible = false;
                ConditionNum = ConditionNum - 1;

            }
            if (checkBox3.Checked)
            {
                ConditionDelete(checkboxtext3.Text, "delete");
                Group3.Text = "";
                checkboxtext3.Text = "";
                checkboxtext3.Visible = false;
                Group3.Visible = false;
                checkBox3.Checked = false;
                checkBox3.Visible = false;
                ConditionNum = ConditionNum - 1;
            }
            if (checkBox4.Checked)
            {
                ConditionDelete(checkboxtext4.Text, "delete");
                Group4.Text = "";
                checkboxtext4.Text = "";
                checkboxtext4.Visible = false;
                Group4.Visible = false;
                checkBox4.Checked = false;
                checkBox4.Visible = false;
                ConditionNum = ConditionNum - 1;
            }
            if (checkBox5.Checked)
            {
                ConditionDelete(checkboxtext5.Text, "delete");
                Group5.Text = "";
                checkboxtext5.Text = "";
                checkboxtext5.Visible = false;
                Group5.Visible = false;
                checkBox5.Checked = false;
                checkBox5.Visible = false;
                ConditionNum = ConditionNum - 1;
            }
            if (ConditionNum < 0)
            {
                ConditionNum = 0;
            }
            CheckboxReset();
            ConditionRefresh("");
        }
        private void ConditionDelete(string conditionStr, string typeStr)   //delete a condition from the conditionlist 
        {
            foreach (string[] stringArr in ListCondition)
            {
                if (stringArr[1] == conditionStr && typeStr == "delete")
                {
                    ListCondition.Remove(stringArr);
                    break;
                }
            }
        }
        private void ConditionUpdate(string newStr, string conditionStr, string changeStr)
        {
            foreach (string[] stringArr in ListCondition)
            {
                var i = ListCondition.IndexOf(stringArr);

                if (ListCondition[i][1] == conditionStr)
                {
                    if (changeStr == "group")
                    {
                        ListCondition[i][0] = "1";
                        ListCondition[i][2] = newStr;
                    }
                }
            }
        }
        private void ConditionAnd_Click(object sender, EventArgs e)
        {
            if (ConditionAnd.BackColor == Color.Transparent)
            {
                ConditionAnd.BackColor = Color.LightGreen;
                ConditionOr.BackColor = Color.Transparent;

            }
            else
            {
                ConditionAnd.BackColor = Color.Transparent;
            }
        }

        private void ConditionOr_Click(object sender, EventArgs e)
        {
            if (ConditionOr.BackColor == Color.Transparent)
            {
                ConditionOr.BackColor = Color.LightGreen;
                ConditionAnd.BackColor = Color.Transparent;
            }
            else
            {
                ConditionOr.BackColor = Color.Transparent;
            }
        }

        private void AddTo_Click(object sender, EventArgs e)
        {
            if (ConditionNum == 0)
            {
                ConditionRefresh("Add");
                PictureChange();
                return;
            }
            if (ConditionOr.BackColor == Color.Transparent && ConditionAnd.BackColor == Color.LightGreen)
            {
                ConditionRefresh("Add");
                PictureChange();
                ConditionAnd.BackColor = Color.Transparent;
            }
            else if (ConditionOr.BackColor == Color.LightGreen && ConditionAnd.BackColor == Color.Transparent)
            {
                ConditionRefresh("Add");
                PictureChange();
                ConditionOr.BackColor = Color.Transparent;
            }
            else if (ConditionOr.BackColor == Color.LightGreen && ConditionAnd.BackColor == Color.LightGreen)
            {
                MessageBox.Show(@"'And' and 'Or' can't exist at the same time!");
            }
            else if (ConditionOr.BackColor == Color.Transparent && ConditionAnd.BackColor == Color.Transparent)
            {
                MessageBox.Show(@"Please select 'And' or 'Or'!");
            }
        }
        private void ConditionRefresh(string type)
        {
            if (type == "Add")
            {
                if (ConditionProperty2.Text != @"=")
                {
                    ConditionProperty2.Text = @"Like";
                }
                string conditionUnit = ConditionProperty.Text + "&$&" + ConditionProperty2.Text + "&$&" + ConditionValue.Text;
                if (ConditionNum > 0)
                {
                    if (ConditionAnd.BackColor == Color.LightGreen)
                    {
                        conditionUnit = "And&$&" + conditionUnit;
                    }
                    else
                    {
                        conditionUnit = "Or&$&" + conditionUnit;
                    }
                }
                string[] stringList = { "0", conditionUnit, "" };
                // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
                foreach (string[] stringArr in ListCondition)
                {
                    // ReSharper disable once InvertIf
                    if (stringArr[1] == conditionUnit)
                    {
                        // ReSharper disable once LocalizableElement
                        MessageBox.Show($@"Condition can not be repeated!");
                        return;
                    }
                }
                ListCondition.Add(stringList);
                ConditionNum = ConditionNum + 1;
            }

            if ((ConditionNum - (ConditionPageNum * 5 + 1)) >= 0)
            {
                checkboxtext1.Text = ListCondition[ConditionPageNum * 5][1];
                Group1.Text = ListCondition[ConditionPageNum * 5][2];
                checkBox1.Visible = true;
                checkboxtext1.Visible = true;
                Group1.Visible = true;
            }
            if ((ConditionNum - (ConditionPageNum * 5 + 2)) >= 0)
            {
                checkboxtext2.Text = ListCondition[ConditionPageNum * 5 + 1][1];
                Group2.Text = ListCondition[ConditionPageNum * 5 + 1][2];
                checkBox2.Visible = true;
                checkboxtext2.Visible = true;
                Group2.Visible = true;
            }
            if ((ConditionNum - (ConditionPageNum * 5 + 3)) >= 0)
            {
                checkboxtext3.Text = ListCondition[ConditionPageNum * 5 + 2][1];
                Group3.Text = ListCondition[ConditionPageNum * 5 + 2][2];
                checkBox3.Visible = true;
                checkboxtext3.Visible = true;
                Group3.Visible = true;
            }
            if ((ConditionNum - (ConditionPageNum * 5 + 4)) >= 0)
            {
                checkboxtext4.Text = ListCondition[ConditionPageNum * 5 + 3][1];
                Group4.Text = ListCondition[ConditionPageNum * 5 + 3][2];
                checkBox4.Visible = true;
                checkboxtext4.Visible = true;
                Group4.Visible = true;
            }
            // ReSharper disable once InvertIf
            if ((ConditionNum - (ConditionPageNum * 5 + 5)) >= 0)
            {
                checkboxtext5.Text = ListCondition[ConditionPageNum * 5 + 4][1];
                Group5.Text = ListCondition[ConditionPageNum * 5 + 4][2];
                checkBox5.Visible = true;
                checkboxtext5.Visible = true;
                Group5.Visible = true;
            }
        }

        private void pictureBoxGroup_Click(object sender, EventArgs e)
        {
            int checkState = 0;
            int checkedNum = 0;
            bool cWrong = GroupWrong();
            foreach (Control c in panelCondition.Controls)
            {
                if (c is CheckBox)
                {
                    if (((CheckBox)c).Checked)
                    {
                        checkedNum++;
                    }
                }
            }

            if (checkBox5.Checked)
            {
                checkBox5.Checked = false;
                if (Group5.Text.Length > 0)
                {
                    ConditionDelete(checkboxtext5.Text, "ungroup");
                    ConditionRefresh("");
                    if (checkedNum == 1)
                    {
                        return;
                    }
                }
                else
                {
                    if (cWrong)
                    {
                        Group5.Text = GroupNameNew();
                        ConditionUpdate(Group5.Text, checkboxtext5.Text, "group");
                        checkState = checkState + 1;
                    }
                }
            }
            if (checkBox4.Checked)
            {
                checkBox4.Checked = false;
                if (Group4.Text.Length > 0)
                {
                    ConditionDelete(checkboxtext4.Text, "ungroup");
                    ConditionRefresh("");
                    if (checkedNum == 1)
                    {
                        return;
                    }
                }
                else
                {
                    if (cWrong)
                    {
                        if (checkState > 0)
                        {
                            Group4.Text = Group5.Text;
                        }
                        else
                        {
                            Group4.Text = GroupNameNew();
                        }
                        ConditionUpdate(Group4.Text, checkboxtext4.Text, "group");
                        checkState = checkState + 1;
                    }
                }

            }
            if (checkBox3.Checked)
            {
                checkBox3.Checked = false;
                if (Group3.Text.Length > 0)
                {
                    ConditionDelete(checkboxtext3.Text, "ungroup");
                    ConditionRefresh("");
                    if (checkedNum == 1)
                    {
                        return;
                    }
                }
                else
                {
                    if (cWrong)
                    {
                        if (checkState > 0)
                        {
                            Group3.Text = Group4.Text;
                        }
                        else
                        {
                            Group3.Text = GroupNameNew();
                        }
                        ConditionUpdate(Group3.Text, checkboxtext3.Text, "group");
                        checkState = checkState + 1;
                    }
                }
            }
            if (checkBox2.Checked)
            {
                checkBox2.Checked = false;
                if (Group2.Text.Length > 0)
                {
                    ConditionDelete(checkboxtext2.Text, "ungroup");
                    ConditionRefresh("");
                    if (checkedNum == 1)
                    {
                        return;
                    }
                }
                else
                {
                    if (cWrong)
                    {
                        Group2.Text = checkState > 0 ? Group3.Text : GroupNameNew();
                        ConditionUpdate(Group2.Text, checkboxtext2.Text, "group");
                    }
                }
            }
            if (checkBox1.Checked)
            {
                checkBox1.Checked = false;
                if (Group1.Text.Length > 0)
                {
                    ConditionDelete(checkboxtext1.Text, "ungroup");
                    ConditionRefresh("");
                }
                else
                {
                    if (checkedNum == 1)
                    {
                        return;
                    }
                    if (cWrong)
                    {
                        Group1.Text = Group2.Text;
                    }
                    ConditionUpdate(Group1.Text, checkboxtext1.Text, "group");
                }
            }
        }
        public bool GroupWrong()
        {
            int grouped = 0;
            int ungrouped = 0;
            int checkIn = 0;
            if (checkBox5.Checked)
            {
                checkIn++;
                if (Group5.Text.Length > 0)
                {
                    grouped++;
                }
                else
                {
                    ungrouped++;
                }
            }
            if (checkBox4.Checked)
            {
                checkIn++;
                if (Group4.Text.Length > 0)
                {
                    grouped++;
                }
                else
                {
                    ungrouped++;
                }
            }
            if (checkBox3.Checked)
            {
                checkIn++;
                if (Group3.Text.Length > 0)
                {
                    grouped++;
                }
                else
                {
                    ungrouped++;
                }
            }
            if (checkBox2.Checked)
            {
                checkIn++;
                if (Group2.Text.Length > 0)
                {
                    grouped++;
                }
                else
                {
                    ungrouped++;
                }
            }
            if (checkBox1.Checked)
            {
                checkIn++;
                if (Group1.Text.Length > 0)
                {
                    grouped++;
                }
                else
                {
                    ungrouped++;
                }
            }
            if (checkIn == grouped || checkIn == ungrouped)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string GroupNameNew()//shows the group's name in condition box. Names are automatically generated and sorted by numbers.
        {
            for (int i = 1; i > 0; i++)
            {
                string groupNameStr = $"G{i}";
                if (GroupName.Contains(groupNameStr)==false)
                {
                    GroupName.Add(groupNameStr);
                    return (groupNameStr);
                }
            }
            return ("");
        }

        private void pictureBoxRight_Click(object sender, EventArgs e)
        {
            ConditionPageNum++;

            CheckboxReset();
            PictureChange();
            ConditionRefresh("");
        }

        private void pictureBoxLeft_Click(object sender, EventArgs e)
        {
            ConditionPageNum--;

            CheckboxReset();
            PictureChange();
            ConditionRefresh("");
        }
        private void PictureChange()
        {
            if (ConditionNum > ((ConditionPageNum + 1) * 5))
            {
                pictureBoxright.Visible = true;
            }
            else
            {
                pictureBoxright.Visible = false;
            }
            if (ConditionPageNum == 0)
            {
                pictureBoxleft.Visible = false;
            }
            else
            {
                pictureBoxleft.Visible = true;
            }
        }

        private void DeviceSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control button = (Control)sender;
            string bName = button.Name;
            var deviceSel = deviceSelect.Text;
            Program.ScreenName = "";
            if (bName== "buttonExecute" && listBoxCheck.Text.Length>15)
            {
                int deviceStrLength = listBoxCheck.Text.IndexOf("&$&") -7;
                deviceSel = listBoxCheck.Text.Substring(7, deviceStrLength).Trim();
                int typeStrLength = listBoxCheck.Text.IndexOf("SET") - listBoxCheck.Text.IndexOf("&$&") - 3;
                Program.OptionStr = listBoxCheck.Text.Substring(listBoxCheck.Text.IndexOf("&$&") + 3, typeStrLength).Trim();
                Program.ScreenName = Program.OptionStr.Split(new[] { "&$&" }, StringSplitOptions.None)[1].Trim();
                
            }
            else
            {
                ParaReset();
            }           
            //deviceSelect.Text = deviceSel;
            if (HmiSoftwareList.Count > 0)
            {
                foreach (HmiSoftware hmiSoftwareSelect in HmiSoftwareList)
                {
                    if (hmiSoftwareSelect.Name == deviceSel)

                        Program.HmiSoftware = hmiSoftwareSelect;

                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Font_Name.Text = "";
            Font_Size.Text = "";
            Font_Weight.Text = "";
            Font_Italic.Text = "";
            Font_Underline.Text = "";
            Font_StrikeOut.Text = "";
            if (panelfont.Visible)
            {
                panelfont.Visible = false;
            }
            else
            {
                panelfont.Visible = true;
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            StatementsValue.Text = Font_Name.Text + @"," + Font_Size.Text + @"," + Font_Weight.Text + @"," + Font_Italic.Text + @"," + Font_Underline.Text + @"," + Font_StrikeOut.Text;
            panelfont.Visible = false;
        }

        private void StatementsValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(StatementsValue.Text== "Dynamization")
            {
                colorSelector1.Visible = false;
                panel4.Visible = false;
                if (Program.OptionStr.EndsWith("ScreenProperties"))
                {
                    foreach (var screen in Program.conditionScreenList)
                    {
                        if(screen.Dynamizations.Count>0)
                        {
                            foreach (var itemDynamization in screen.Dynamizations)
                            {
                                Regex PropertyName = new Regex(itemDynamization.PropertyName.ToString());
                                if (!PropertyName.Match(StatementsProperty.Text).Success)
                                {
                                    continue;
                                }
                                if (itemDynamization.DynamizationType == Siemens.Engineering.HmiUnified.UI.Dynamization.DynamizationType.Script)
                                {
                                    // Get current Script Dynamization
                                    Program.DynamizationStr = itemDynamization.GetType().GetProperty("ScriptCode").GetValue(itemDynamization).ToString();
                                    Program.DynamizationTrigger = (Trigger)itemDynamization.GetType().GetProperty("Trigger").GetValue(itemDynamization);
                                    /*
                                    string triggerType = trigger.Type.ToString();
                                    if(triggerType=="Tags")
                                    {
                                       List<string> triggerTags = (List<string>)trigger.Tags;
                                    }
                                    if (trigger.CustomDuration != "")
                                    {

                                    }
                                    */
                                    Program.DynamizationType = "ScriptCode";
                                    break;
                                    // Write changed Script Dynamization
                                    // itemDynamization.GetType().InvokeMember("ScriptCode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, itemDynamization, new object[] { s });
                                }
                                if (itemDynamization.DynamizationType == Siemens.Engineering.HmiUnified.UI.Dynamization.DynamizationType.Tag)
                                {
                                    // Get current Script Dynamization
                                    Program.DynamizationStr = itemDynamization.GetType().GetProperty("Tag").GetValue(itemDynamization).ToString();
                                    Program.DynamizationStr = Program.DynamizationStr + "-" + itemDynamization.GetType().GetProperty("ReadOnly").GetValue(itemDynamization).ToString();
                                    Program.DynamizationType = "Tag";
                                    break;
                                    // Write changed Script Dynamization
                                    // itemDynamization.GetType().InvokeMember("ScriptCode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, itemDynamization, new object[] { s });
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var screenItem in Program.ItemList)
                    {
                        string StatementsPropertyMarchText = StatementsProperty.Text;

                        if (StatementsProperty.Text == "HiddenInput" || StatementsProperty.Text == "AcceptOnDeactivated")
                        {
                            StatementsPropertyMarchText = "InputBehavior";
                            Siemens.Engineering.HmiUnified.UI.Parts.HmiInputBehaviorPart propertiesObject = (Siemens.Engineering.HmiUnified.UI.Parts.HmiInputBehaviorPart)screenItem.GetType().GetProperty("InputBehavior").GetValue(screenItem);
                            if (propertiesObject.Dynamizations.Count > 0)
                            {
                                foreach (var itemDynamization in propertiesObject.Dynamizations)
                                {
                                    Regex PropertyName = new Regex(itemDynamization.PropertyName.ToString());

                                    if (!PropertyName.Match(StatementsProperty.Text).Success)
                                    {
                                        continue;
                                    }
                                    if (itemDynamization.DynamizationType == Siemens.Engineering.HmiUnified.UI.Dynamization.DynamizationType.Script)
                                    {
                                        // Get current Script Dynamization
                                        Program.DynamizationStr = itemDynamization.GetType().GetProperty("ScriptCode").GetValue(itemDynamization).ToString();
                                        Program.DynamizationTrigger = (Trigger)itemDynamization.GetType().GetProperty("Trigger").GetValue(itemDynamization);
                                        Program.DynamizationType = "ScriptCode";
                                        break;
                                    }
                                    if (itemDynamization.DynamizationType == Siemens.Engineering.HmiUnified.UI.Dynamization.DynamizationType.Tag)
                                    {
                                        // Get current Script Dynamization
                                        Program.DynamizationStr = itemDynamization.GetType().GetProperty("Tag").GetValue(itemDynamization).ToString();
                                        Program.DynamizationStr = Program.DynamizationStr + "-" + itemDynamization.GetType().GetProperty("ReadOnly").GetValue(itemDynamization).ToString();
                                        Program.DynamizationType = "Tag";
                                        break;
                                    }
                                }
                            }
                        }
                        else if(screenItem.Dynamizations.Count > 0)
                        {
                            foreach (var itemDynamization in screenItem.Dynamizations)
                            {
                                
                                Regex PropertyName = new Regex(itemDynamization.PropertyName.ToString());
                                
                                if (!PropertyName.Match(StatementsPropertyMarchText).Success)
                                {
                                    continue;
                                }
                                if (itemDynamization.DynamizationType == Siemens.Engineering.HmiUnified.UI.Dynamization.DynamizationType.Script)
                                {
                                    // Get current Script Dynamization
                                    Program.DynamizationStr = itemDynamization.GetType().GetProperty("ScriptCode").GetValue(itemDynamization).ToString();
                                    Program.DynamizationTrigger = (Trigger)itemDynamization.GetType().GetProperty("Trigger").GetValue(itemDynamization);
                                    /*
                                    string triggerType = trigger.Type.ToString();
                                    if(triggerType=="Tags")
                                    {
                                       List<string> triggerTags = (List<string>)trigger.Tags;
                                    }
                                    if (trigger.CustomDuration != "")
                                    {

                                    }
                                    */
                                    Program.DynamizationType = "ScriptCode";
                                    break;
                                    // Write changed Script Dynamization
                                    // itemDynamization.GetType().InvokeMember("ScriptCode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, itemDynamization, new object[] { s });
                                }
                                if (itemDynamization.DynamizationType == Siemens.Engineering.HmiUnified.UI.Dynamization.DynamizationType.Tag)
                                {
                                    // Get current Script Dynamization
                                    Program.DynamizationStr = itemDynamization.GetType().GetProperty("Tag").GetValue(itemDynamization).ToString();
                                    Program.DynamizationStr = Program.DynamizationStr + "-" + itemDynamization.GetType().GetProperty("ReadOnly").GetValue(itemDynamization).ToString();
                                    Program.DynamizationType = "Tag";
                                    break;
                                    // Write changed Script Dynamization
                                    // itemDynamization.GetType().InvokeMember("ScriptCode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, itemDynamization, new object[] { s });
                                }
                            }
                        }
                    }
                }
                   
                DynamizationScript dynamizationScriptForm = new DynamizationScript();
                dynamizationScriptForm.TaskEvent += new TaskDelegate(StatementValueStr);
                dynamizationScriptForm.ShowDialog();               
            }
            else
            {
                Program.DynamizationStr = "";
                Program.DynamizationType = "";
            }
        }

        private void comboBoxFont_SelectedIndexChanged(object sender, EventArgs e)//just for set font use
        {
            Control button = (Control)sender;
            
            if (button.Text == "Dynamization")
            {
                string propertyNameStr = button.Name.Replace("_", ".");
             
                foreach (var screenItem in Program.ItemList)
                {
                    List<string> keySplit = propertyNameStr.Split('.').ToList();
                    IEngineeringObject deeperObj = screenItem.GetAttribute(keySplit[0]) as IEngineeringObject;
                    // Todo: GetComposition
                    string deeperKey = string.Join(".", keySplit);
                    if (deeperObj is Siemens.Engineering.HmiUnified.UI.Parts.HmiFontPart)
                    {
                        Siemens.Engineering.HmiUnified.UI.Parts.HmiFontPart hmiFont = (Siemens.Engineering.HmiUnified.UI.Parts.HmiFontPart)deeperObj;                       
                        if (hmiFont.Dynamizations.Count > 0)
                        {
                            foreach (var itemDynamization in hmiFont.Dynamizations)
                            {
                                Regex PropertyName = new Regex(itemDynamization.PropertyName.ToString());
                                if (!PropertyName.Match(deeperKey).Success)
                                {
                                    continue;
                                }
                                if (itemDynamization.DynamizationType == Siemens.Engineering.HmiUnified.UI.Dynamization.DynamizationType.Script)
                                {
                                    // Get current Script Dynamization
                                    Program.DynamizationStr = itemDynamization.GetType().GetProperty("ScriptCode").GetValue(itemDynamization).ToString();
                                    Program.DynamizationType = "ScriptCode";
                                    break;
                                }
                                if (itemDynamization.DynamizationType == Siemens.Engineering.HmiUnified.UI.Dynamization.DynamizationType.Tag)
                                {
                                    // Get current Script Dynamization
                                    Program.DynamizationStr = itemDynamization.GetType().GetProperty("Tag").GetValue(itemDynamization).ToString();
                                    Program.DynamizationStr = Program.DynamizationStr + "-" + itemDynamization.GetType().GetProperty("ReadOnly").GetValue(itemDynamization).ToString();
                                    Program.DynamizationType = "Tag";
                                    break;
                                }
                            }
                        }
                    }       
                }
                Program.setFontType = propertyNameStr;
                DynamizationScript dynamizationScriptForm = new DynamizationScript();
                dynamizationScriptForm.TaskEvent += new TaskDelegate(StatementValueStr);
                dynamizationScriptForm.ShowDialog();
                panelfont.Visible = false;                
            }
            else
            {
                Program.DynamizationStr = "";
                Program.DynamizationType = "";
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxProName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
