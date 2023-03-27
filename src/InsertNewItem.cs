using Siemens.Engineering;
using Siemens.Engineering.HmiUnified.HmiTags;
using Siemens.Engineering.HmiUnified.UI.Screens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Loggingtext;
using Siemens.Engineering.HmiUnified.UI.Base;
using System.Reflection;
using Siemens.Engineering.SW.Blocks;
using Siemens.Engineering.Hmi.Screen;
using Siemens.Engineering.HmiUnified.UI.Dynamization;
using Siemens.Engineering.HmiUnified.UI.Dynamization.Script;
using System.Net;
using Siemens.Engineering.HmiUnified.UI.Parts;
using Siemens.Engineering.HmiUnified.UI.Events;

namespace ReUnifier
{
    class InsertNewItem
    {

        public void InsertTags(string type)//insert a new tag, or logging tag, or AlarmTag. If the setting Information is ont only one item, after insert, it should jump to the update fuction.
        {
            String newName = Program.SetStatements["Name"];
            int tagNum = 0;
            int loggingTagNum = 0;
            int discreteAlarmTagNum = 0;
            int analogAlarmTagNum = 0;
            if (Program.ScreenName == "HmiTags" && Program.HmiSoftware.Tags.Find(newName) == null)
            {
                if (System.Windows.Forms.MessageBox.Show(@" Create a new Tag ?", @" Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Program.HmiSoftware.Tags.Create(newName);
                    LogAndXmlOP.LogOut("Log", "create a new Tag : " + newName + " \n", 0);
                    Program.StrShow += "create a new Tag : " + newName + " \n";
                    tagNum++;
                    if (Program.SetStatements.Count > 1)
                    {
                        Program.WhereConditionsStr = "Name &$&= &$&" + newName;
                        Program.SetStatements.Remove("Name");
                        HandleHmiTags(type);//jump to the update function
                    }
                }
                else
                {
                    LogAndXmlOP.LogOut("Log", "Alarming : " + " Tag creation failed, it already exists " + " \n", 0);
                    Program.StrShow += "Alarming : " + " Tag creation failed, it already exists " + " \n";
                }
            }
            else if (Program.ScreenName == "HmiLoggingTags")
            {
                foreach (var tag in Program.HmiSoftware.Tags)
                {
                    foreach (var relevantTag in RecursiveTags(tag, tag.Name))
                    {
                        if (relevantTag.LoggingTags.Find(newName) == null)//First search whether there is this logtag. If not, create a new one
                        {
                            if (System.Windows.Forms.MessageBox.Show(@"create a new LoggingTags?", @"Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            {
                                relevantTag.LoggingTags.Create(newName);//Insert a record variable. The archive file stored in the record variable is not set. Manual is required.
                                LogAndXmlOP.LogOut("Log", "create a new LoggingTag : " + newName + " \n", 0);
                                Program.StrShow += "create a new LoggingTag : " + newName + " \n";
                                loggingTagNum++;
                                if (Program.SetStatements.Count > 1)
                                {
                                    Program.SetStatements.Remove("Name");
                                    HandleHmiTags(type);
                                }
                            }
                        }
                        else
                        {
                            LogAndXmlOP.LogOut("Log", "Alarming : " + " LoggingTag creation failed, it already exists " + " \n", 0);
                            Program.StrShow = Program.StrShow + "Alarming : " + " LoggingTag creation failed, it already exists " + " \n";
                        }
                    }
                }
            }
            else if (Program.ScreenName == "HmiDiscreteAlarmTags")
            {
                if (Program.HmiSoftware.DiscreteAlarms.Find(newName) == null)//First search whether there is this logtag. If not, create a new one
                {
                    if (System.Windows.Forms.MessageBox.Show(@"create a new  DiscreteAlarmTag?", @"Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        Program.HmiSoftware.DiscreteAlarms.Create(newName);//Insert a record variable. The archive file stored in the record variable is not set. Manual is required.
                        LogAndXmlOP.LogOut("Log", "create a new  DiscreteAlarmTag : " + newName + " \n", 0);
                        Program.StrShow += "create a new  DiscreteAlarmTag : " + newName + " \n";
                        discreteAlarmTagNum++;
                        if (Program.SetStatements.Count > 1)
                        {
                            Program.WhereConditionsStr = "Name &$&= &$&" + newName;
                            Program.SetStatements.Remove("Name");
                            HandleHmiTags(type);
                        }
                    }
                }
                else
                {
                    LogAndXmlOP.LogOut("Log", "Alarming : " + "  DiscreteAlarmTag creation failed, it already exists " + " \n", 0);
                    Program.StrShow = Program.StrShow + "Alarming : " + "  DiscreteAlarmTag creation failed, it already exists " + " \n";
                }
            }
            else
            {
                if (Program.HmiSoftware.AnalogAlarms.Find(newName) == null)//First search whether there is this logtag. If not, create a new one
                {
                    if (System.Windows.Forms.MessageBox.Show(@"create a new   AnalogAlarmTag?", @"Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        Program.HmiSoftware.AnalogAlarms.Create(newName);//Insert a record variable. The archive file stored in the record variable is not set. Manual is required.
                        LogAndXmlOP.LogOut("Log", "create a new   AnalogAlarmTag : " + newName + " \n", 0);
                        Program.StrShow += "create a new   AnalogAlarmTag : " + newName + " \n";
                        analogAlarmTagNum++;
                        if (Program.SetStatements.Count > 1)
                        {
                            Program.WhereConditionsStr = "Name &$&= &$&" + newName;
                            Program.SetStatements.Remove("Name");
                            HandleHmiTags(type);
                        }
                    }
                }
                else
                {
                    LogAndXmlOP.LogOut("Log", "Alarming : " + "   AnalogAlarmTag creation failed, it already exists " + " \n", 0);
                    Program.StrShow = Program.StrShow + "Alarming : " + "   AnalogAlarmTag creation failed, it already exists " + " \n";
                }
                analogAlarmTagNum++;
            }
            if (tagNum > 0)
            {
                LogAndXmlOP.LogOut("Log", "create new Tags Num : " + tagNum + " \n", 0);
                Program.StrShow = Program.StrShow + "create new Tags Num : " + tagNum + " \n";
            }
            if (loggingTagNum > 0)
            {
                LogAndXmlOP.LogOut("Log", "create new LoggingTags Num : " + loggingTagNum + " \n", 0);
                Program.StrShow = Program.StrShow + "create new LoggingTags Num : " + loggingTagNum + " \n";
            }
            if (discreteAlarmTagNum > 0)
            {
                LogAndXmlOP.LogOut("Log", "create new DiscreteAlarmTags Num : " + discreteAlarmTagNum + " \n", 0);
                Program.StrShow = Program.StrShow + "create new DiscreteAlarmTags Num : " + discreteAlarmTagNum + " \n";
            }
            if (discreteAlarmTagNum > 0)
            {
                LogAndXmlOP.LogOut("Log", "create new AnalogAlarmTags Num : " + analogAlarmTagNum + " \n", 0);
                Program.StrShow = Program.StrShow + "create new AnalogAlarmTags Num : " + analogAlarmTagNum + " \n";
            }
        }
       
        public int HandleHmiTags(string type)// update some properties, only for tags and logging tags.
        {
            int tagNum = 0;
            int loggingTagNum = 0;
            int conditionNum = 0;
            foreach (var tag in Program.HmiSoftware.Tags)
            {
                foreach (var relevantTag in RecursiveTags(tag, tag.Name))
                {
                    conditionNum++;
                    if (type == "executed")
                    {
                        foreach (var setStatement in Program.SetStatements)
                        {
                            string testStr ;
                            if (setStatement.Value.Contains("'"))
                            {
                                testStr = relevantTag.GetType().GetProperty(setStatement.Key)?.GetValue(relevantTag, null).ToString();
                                string replaceStrOld = setStatement.Value.Split('\'')[0];
                                string replaceStrNew = setStatement.Value.Split('\'')[1];
                                testStr = testStr?.Replace(replaceStrOld, replaceStrNew);
                            }
                            else
                            {
                                testStr = setStatement.Value;
                            }
                            if (Program.ScreenName == "HmiTags")
                            {
                                SetPropertyRecursive(setStatement.Key, testStr, relevantTag);
                                LogAndXmlOP.LogOut("Log", "Update the property of the tag : " + relevantTag.Name + " \n", 0);
                                Program.StrShow = Program.StrShow + "Update the property of the tag : " + relevantTag.Name + " \n";
                                tagNum++;
                            }
                            else if (Program.ScreenName == "HmiLoggingTags")//If it is a logtag，it should though foreach again
                            {
                                foreach (var loggingTag in relevantTag.LoggingTags)
                                {
                                    SetPropertyRecursive(setStatement.Key, testStr, loggingTag);
                                    LogAndXmlOP.LogOut("Log", "Update the property of the logging Tag : " + loggingTag.Name + " \n", 0);
                                    Program.StrShow = Program.StrShow + "Update the property of the logging Tag : " + loggingTag.Name + " \n";
                                    loggingTagNum++;
                                }
                            }
                        }
                    }
                }
            }
            if (type == "condition")
            {
                return conditionNum;//show the num in setting step
            }
            if ((loggingTagNum + tagNum) == 0)
            {
                LogAndXmlOP.LogOut("Log", "Alarming : " + " No changes. Conditions do not match" + " \n", 0);
                Program.StrShow = Program.StrShow + "Alarming : " + " No changes. Conditions do not match" + " \n";
            }
            if (tagNum > 0)
            {
                LogAndXmlOP.LogOut("Log", "Update the properties in tags : " + tagNum + " \n", 0);
                Program.StrShow = Program.StrShow + "Update the properties in tags : " + tagNum + " \n";
            }
            if (loggingTagNum > 0)
            {
                LogAndXmlOP.LogOut("Log", "The Num of updated properties in logging tags : " + loggingTagNum + " \n", 0);
                Program.StrShow = Program.StrShow + "The Num of updated properties in logging tags : " + loggingTagNum + " \n";
            }
            return 0;
        }
        public int HandleHmiAlarmTags(string type)//update function, only for alarm tags.
        {
            dynamic hmiAlarmTag;
            if (Program.ScreenName == "HmiDiscreteAlarmTags")
            {
                hmiAlarmTag = Program.HmiSoftware.DiscreteAlarms;
            }
            else
            {
                hmiAlarmTag = Program.HmiSoftware.AnalogAlarms;
            }
            int changeNum = 0;
            int conditionNum = 0;
            Conditionstolist();
            foreach (var tag in hmiAlarmTag)
            {
                if (Compare_tag(tag, tag.Name))/////////////////compare tag
                {
                    conditionNum++;
                    if (type == "executed")
                    {
                        foreach (var setStatement in Program.SetStatements)
                        {
                            string testStr;
                            if (setStatement.Value.Contains("'"))
                            {
                                testStr = tag.GetType().GetProperty(setStatement.Key).GetValue(tag, null).ToString();
                                string replaceStrOld = setStatement.Value.Split('\'')[0];
                                string replaceStrNew = setStatement.Value.Split('\'')[1];
                                testStr = testStr?.Replace(replaceStrOld, replaceStrNew);
                            }
                            else
                            {
                                testStr = setStatement.Value;
                            }
                            SetPropertyRecursive(setStatement.Key, testStr, tag);
                            LogAndXmlOP.LogOut("Log", "Update the property of the AlarmingTag : " + tag.Name + " \n", 0);
                            Program.StrShow = Program.StrShow + "Update the property of the AlarmingTag : " + tag.Name + " \n";
                            changeNum++;
                        }
                    }
                }
            }
            if (type == "condition")
            {
                return conditionNum;
            }
            if (changeNum == 0)
            {
                LogAndXmlOP.LogOut("Log", "Alarming : " + " No changes. Conditions do not match" + " \n", 0);
                Program.StrShow += "Alarming : " + " No changes. Conditions do not match" + " \n";
            }
            else
            {
                LogAndXmlOP.LogOut("Log", "The Num of updated properties in AlarmingTags: " + changeNum + " \n", 0);
                Program.StrShow += "The Num of updated properties in AlarmingTags: " + changeNum + " \n";
            }
            return 0;
        }
        public static void SetPropertyRecursive(string key, string value, IEngineeringObject relevantTag)//setting new properties
        {
            if (key.Contains("."))
            {
                List<string> keySplit = key.Split('.').ToList();
                IEngineeringObject deeperObj = relevantTag.GetAttribute(keySplit[0]) as IEngineeringObject;
                // Todo: GetComposition
                keySplit.RemoveAt(0);
                string deeperKey = string.Join(".", keySplit);
                SetPropertyRecursive(deeperKey, value, deeperObj);
            }
            else
            {
                SetMyAttributesSimpleTypes(key, value, relevantTag);
            }
        }
        public static bool IsNumericType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        private static void SetMyAttributesSimpleTypes(string keyToSet, object valueToSet, IEngineeringObject obj)
        {
            Type type = obj.GetType().GetProperty(keyToSet)?.PropertyType;

            object attrVal = null;
            if (type != null && type.BaseType == typeof(Enum))
            {
                attrVal = Enum.Parse(type, valueToSet.ToString());
            }
            else if (type != null && type.Name == "Color")
            {
                //var hexColor = new ColorConverter();               
                //attrVal = (Color)hexColor.ConvertFromString(valueToSet.ToString().ToUpper());
                attrVal = ColorTranslator.FromHtml(Program.StrColorSet);
            }
            else if (obj.GetType().Name == "MultilingualText")
            {
                obj = (obj as MultilingualText)?.Items.FirstOrDefault(item => item.Language.Culture.Name == keyToSet);

                if (obj == null)
                {
                    Console.WriteLine(@"Language " + keyToSet + @" does not exist in this Runtime!");
                    return;
                }
                keyToSet = "Text";
                attrVal = valueToSet.ToString();
            }
            else if (keyToSet == "Tags")
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                attrVal = (valueToSet as List<object>).Select(i => i.ToString()).ToList();
            }
            else
            {
                if (type != null)
                {
                    Program.ScreenItemUpdate = "";
                    if (IsNumericType(type) && (valueToSet.ToString().StartsWith("+") || valueToSet.ToString().StartsWith("-")))
                    {
                        attrVal = Convert.ChangeType(valueToSet, type);
                        attrVal = (dynamic)obj.GetAttribute(keyToSet.ToString()) + (dynamic)attrVal;
                        Program.ScreenItemUpdate = " " + keyToSet.ToString() + " = " + attrVal;
                    }
                    else if (IsNumericType(type) && valueToSet.ToString().StartsWith("*"))
                    {
                        string svalueToSet = valueToSet.ToString().TrimStart('*');
                        double calculatedValue = (dynamic)obj.GetAttribute(keyToSet.ToString()) * Convert.ToDouble(svalueToSet);
                        attrVal = Convert.ChangeType(calculatedValue, type);
                        Program.ScreenItemUpdate = " " + keyToSet.ToString() + " = " + attrVal;
                    }
                    else if (IsNumericType(type) && valueToSet.ToString().StartsWith("/"))
                    {
                        string svalueToSet = valueToSet.ToString().TrimStart('/');
                        double calculatedValue = (dynamic)obj.GetAttribute(keyToSet.ToString()) / Convert.ToDouble(svalueToSet);
                        attrVal = Convert.ChangeType(calculatedValue, type);
                        Program.ScreenItemUpdate = " " + keyToSet.ToString() + " = " + attrVal;
                    }else
                    {
                        attrVal = Convert.ChangeType(valueToSet, type);
                    }

                   // attrVal = Convert.ChangeType(valueToSet, type);
                    // if a numeric type begins with + or -, it should be added to the current value
                    /*
                    if (IsNumericType(type) && (valueToSet.ToString().StartsWith("+") || valueToSet.ToString().StartsWith("-")))
                    {
                        attrVal = (dynamic)obj.GetAttribute(keyToSet) + (dynamic)attrVal;
                    }
                    */
                }
            }
            try
            {
                obj.SetAttribute(keyToSet, attrVal);
            }
            catch { /*Console.WriteLine(ex.Message);*/ }
        }
        public int UpdateScreen(string type)//update screen items. include delete option
        {
            Regex rx = new Regex(Program.ScreenName);

            int changeNum = 0;
            int conditionNum = 0;
            Program.ItemList.Clear();
            Conditionstolist();
            foreach (var screen in Program.GetScreens())
            {
                if (Program.OptionStr.EndsWith("ScreenProperties"))
                {
                    if (Compare_tag(screen, screen.Name))
                    {
                        if (type == "executed")
                        {
                            changeNum += UpdateScreenOption(null, screen);//only when we execute the option , it should jump to another function.
                        }
                        conditionNum++;
                    }
                }
                
                else
                {                   
                    if (!rx.Match(screen.Name).Success)
                    {
                        continue;
                    }
                    for (int i = 0; i < screen.ScreenItems.Count; i++)
                    {
                        HmiScreenItemBase screenItem = screen.ScreenItems[i];

                        if (Compare_screen(screenItem.Name, screenItem.GetType().Name))
                        {
                            if (type == "executed")
                            {
                                int newNum = UpdateScreenOption(screenItem, screen);
                                if (newNum > 0 && Program.OptionStr.StartsWith("DELETE"))
                                {
                                    i--;
                                }
                                changeNum += newNum;
                            }
                            Program.ItemList.Add(screenItem);
                            conditionNum++;
                        }
                    }
                }
            }
            if (type == "condition")
            {
                return conditionNum;
            }
            if (Program.OptionStr.EndsWith("ScreenProperties"))
            {
                if (changeNum == 0)
                {
                    LogAndXmlOP.LogOut("Log", "Alarming : " + " Not changing any Property in any screen. " + " \n", 0);
                    Program.StrShow = Program.StrShow + "Alarming : " + " Not changing any Property in any screen. " + " \n";
                }
                else
                {
                    LogAndXmlOP.LogOut("Log", "Modification times from the Screens is : " + changeNum + " \n", 0);
                    Program.StrShow = Program.StrShow + "Modification times from the Screens is : " + changeNum + " \n";
                }
            }
            else  //after change item's properties from a screen, change the text in the log file and the text information on the program screen
            {
                if (changeNum == 0)
                {
                    LogAndXmlOP.LogOut("Log", "Alarming : " + " Not changing any item in any screen. " + " \n", 0);
                    Program.StrShow = Program.StrShow + "Alarming : " + " Not changing any item in any screen. " + " \n";
                }
                else
                {
                    LogAndXmlOP.LogOut("Log", "Modification times from the Screen : " + Program.ScreenName + " is : " + changeNum + " \n", 0);
                    Program.StrShow = Program.StrShow + "Modification times from the Screen : " + Program.ScreenName + " is : " + changeNum + " \n";
                }
            }

            return 0;
        }

        public int UpdateScreenOption(HmiScreenItemBase screenItem, HmiScreen hmiScreen)  //execute, only for screen items
        {
            int changeNum = 0;
            if (Program.OptionStr.StartsWith("DELETE"))
            {
                if (Program.OptionStr.EndsWith("ScreenProperties"))
                {

                }
                else
                {
                    string itemNameStr = screenItem.Name;
                    IEngineeringObject hmiEngObj = screenItem;
                    if (hmiEngObj != null)
                    {
                        hmiEngObj.Invoke("Delete", null);
                        changeNum++;

                        LogAndXmlOP.LogOut("Log", "DELETE the ScreenItem : " + itemNameStr + " from the Screen : " + hmiScreen.Name + " \n", 0);
                        Program.StrShow += "DELETE the ScreenItem : " + itemNameStr + " from the Screen : " + hmiScreen.Name + " \n";
                    }
                }
            }
            else
            {
                foreach (var setStatement in Program.SetStatements)
                {
                    string testStr ;
                    string loggingStr ;
                    string dynamizationType = "";
                    string triggerType = "";
                    List<string> triggerTags = new List<string>(); 
                    if (setStatement.Value.Contains("&&&"))
                    {
                        dynamizationType = Regex.Split(setStatement.Value, "&&&", RegexOptions.IgnoreCase)[0].Trim();
                        if(dynamizationType == "ScriptCode")
                        {
                            triggerType = Regex.Split(setStatement.Value, "&&&", RegexOptions.IgnoreCase)[1].Trim();
                            if (Regex.Split(setStatement.Value, "&&&", RegexOptions.IgnoreCase).Count() > 3)
                            {
                                string tagsNameStr = Regex.Split(setStatement.Value, "&&&", RegexOptions.IgnoreCase)[2].Trim();
                                string[] tagNameArr = tagsNameStr.Split('#');
                                triggerTags = tagNameArr.ToList();
                                testStr = Regex.Split(setStatement.Value, "&&&", RegexOptions.IgnoreCase)[3];
                            }
                            else
                            {
                                testStr = Regex.Split(setStatement.Value, "&&&", RegexOptions.IgnoreCase)[2];
                            }
                        }
                        else
                        {
                            testStr = Regex.Split(setStatement.Value, "&&&", RegexOptions.IgnoreCase)[1];
                        }
                        
                    }
                    else
                    {
                        if (setStatement.Value.Contains("'"))
                        {
                            try
                            {
                                testStr = Program.OptionStr.EndsWith("ScreenProperties") ? hmiScreen.GetType().GetProperty(setStatement.Key).GetValue(hmiScreen, null).ToString() : screenItem.GetType().GetProperty(setStatement.Key).GetValue(screenItem, null).ToString();
                            }
                            catch
                            {
                                continue;
                            }
                            string replaceStrOld = setStatement.Value.Split('\'')[0];
                            string replaceStrNew = setStatement.Value.Split('\'')[1];
                            testStr = testStr?.Replace(replaceStrOld, replaceStrNew);
                        }
                        else
                        {
                            testStr = setStatement.Value;
                        }
                    }
                    
                    try
                    {
                        if (setStatement.Key == "Font")
                        {
                            string[] split = testStr.Split(new[] { "," }, StringSplitOptions.None);
                            if (split[0].Trim().Length > 0)
                            {
                                SetPropertyRecursive(setStatement.Key + ".Name", split[0], screenItem);
                            }
                            if (split[1].Trim().Length > 0)
                            {
                                string SizeStr = "";
                                HmiFontPart fontPart = (HmiFontPart)screenItem.GetAttribute("Font");
                                if (split[1].ToString().StartsWith("+"))
                                {
                                    string svalueToSet = split[1].ToString().TrimStart('+');
                                    double calculatedValue = (dynamic)fontPart.Size + Convert.ToDouble(svalueToSet);
                                    SizeStr = calculatedValue.ToString();
                                }
                                else if (split[1].ToString().StartsWith("-"))
                                {
                                    string svalueToSet = split[1].ToString().TrimStart('-');
                                    double calculatedValue = (dynamic)fontPart.Size - Convert.ToDouble(svalueToSet);
                                    if (calculatedValue < 8)
                                    {
                                        calculatedValue = 8;
                                    }
                                    SizeStr = calculatedValue.ToString();
                                }
                                else if (split[1].ToString().StartsWith("*"))
                                {
                                    string svalueToSet = split[1].ToString().TrimStart('*');
                                    double calculatedValue = (dynamic)fontPart.Size * Convert.ToDouble(svalueToSet);
                                    SizeStr = calculatedValue.ToString();
                                }
                                else if (split[1].ToString().StartsWith("/"))
                                {
                                    string svalueToSet = split[1].ToString().TrimStart('/');
                                    double calculatedValue = (dynamic)fontPart.Size / Convert.ToDouble(svalueToSet);
                                    SizeStr = calculatedValue.ToString();
                                }
                                else
                                {
                                    SizeStr = split[1];
                                }

                                SetPropertyRecursive(setStatement.Key + ".Size", SizeStr, screenItem);

                            }
                            if (split[2].Trim().Length > 0)
                            {
                                SetPropertyRecursive(setStatement.Key + ".Weight", split[2], screenItem);
                            }
                            if (split[3].Trim().Length > 0)
                            {
                                SetPropertyRecursive(setStatement.Key + ".Italic", split[3], screenItem);
                            }
                            if (split[4].Trim().Length > 0)
                            {
                                SetPropertyRecursive(setStatement.Key + ".Underline", split[4], screenItem);
                            }
                            if (split[5].Trim().Length > 0)
                            {
                                SetPropertyRecursive(setStatement.Key + ".StrikeOut", split[5], screenItem);
                            }
                            loggingStr = "Update the property : " + setStatement.Key + " Of the Item : " + screenItem.Name + " from the Screen : " + hmiScreen.Name + " \n";
                        }
                        else if (setStatement.Key.Contains("Font."))//just for change font dynamization
                        {
                            List<string> keySplit = setStatement.Key.Split('.').ToList();
                            IEngineeringObject deeperObj = screenItem.GetAttribute(keySplit[0]) as IEngineeringObject;
                            string deeperKey = keySplit[1];
                            if (deeperObj is Siemens.Engineering.HmiUnified.UI.Parts.HmiFontPart)
                            {
                                Siemens.Engineering.HmiUnified.UI.Parts.HmiFontPart hmiFont = (Siemens.Engineering.HmiUnified.UI.Parts.HmiFontPart)deeperObj;

                                foreach (var itemProperties in hmiFont.GetType().GetProperties())
                                {
                                    Regex PropertyName = new Regex(itemProperties.Name.ToString());
                                    if (!PropertyName.Match(deeperKey).Success)
                                    {
                                        continue;
                                    }
                                    if (testStr == "Dynamization_Delete")
                                    {
                                        DynamizationBaseComposition dyns = hmiFont.Dynamizations;
                                        DynamizationBase dynamization = dyns.Find(deeperKey);
                                        dynamization.Delete();
                                        break;
                                    }
                                    else if (dynamizationType == "ScriptCode")
                                    {

                                        testStr = "\n" + testStr;
                                        DynamizationBaseComposition dyns = hmiFont.Dynamizations;
                                        ScriptDynamization scriptDynamic = dyns.Create<ScriptDynamization>(deeperKey);
                                        scriptDynamic.GetType().InvokeMember("ScriptCode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, scriptDynamic, new object[] { testStr });
                                        if(triggerType=="Disabled")
                                        {
                                            scriptDynamic.Trigger.Type = TriggerType.Disabled;
                                        }
                                        else if(triggerType == "Tags")
                                        {
                                            scriptDynamic.Trigger.Type = TriggerType.Tags;
                                            scriptDynamic.Trigger.Tags = triggerTags;
                                        }
                                        else
                                        {
                                            scriptDynamic.Trigger.Type = TriggerType.CustomCycle;
                                            scriptDynamic.Trigger.CustomDuration = triggerType;
                                        }

                                        break;
                                    }
                                    else if (dynamizationType == "Tag")
                                    {
                                        DynamizationBaseComposition dyns = hmiFont.Dynamizations;
                                        TagDynamization tagDynamic = dyns.Create<TagDynamization>(deeperKey);
                                        tagDynamic.Tag = testStr.Split('-')[0].Trim();
                                        tagDynamic.ReadOnly = bool.Parse(testStr.Split('-')[1].Trim());
                                        break;
                                    }
                                }
                            }
                            loggingStr = "Update the property : " + setStatement.Key + " Of the Item : " + screenItem.Name + Program.ScreenItemUpdate + " from the Screen : " + hmiScreen.Name + " \n";
                            Program.ScreenItemUpdate = "";
                        }
                        else if(setStatement.Key=="Events")
                        {
                            if(Program.OptionStr.EndsWith("ScreenProperties"))
                            {
                                foreach (var screen in Program.GetScreens())
                                {
                                    foreach (HmiScreenEventHandler eveHandler in screen.EventHandlers)
                                    {
                                        string eveType = eveHandler.GetAttribute("EventType").ToString();
                                        IEngineeringObject script = eveHandler.GetAttribute("Script") as IEngineeringObject;
                                        Regex PropertyName = new Regex(eveType);
                                        if (!PropertyName.Match(dynamizationType).Success)
                                        {
                                            continue;
                                        }
                                        if (testStr.EndsWith("Event_Delete"))
                                        {
                                            script.SetAttribute("ScriptCode", "");
                                        }
                                        else
                                        {
                                            if(testStr.StartsWith("\n")==false)
                                            {
                                                testStr = "\n" + testStr;
                                            }
                                            
                                            script.SetAttribute("ScriptCode", testStr);
                                        }

                                        break;

                                    }
                                }
                            }
                            else
                            {
                                IEngineeringObject itemConcreto = screenItem;

                                foreach (IEngineeringObject eveHandItem in itemConcreto.GetComposition("EventHandlers") as IEngineeringComposition)
                                {

                                    string eveType = eveHandItem.GetAttribute("EventType").ToString();
                                    IEngineeringObject script = eveHandItem.GetAttribute("Script") as IEngineeringObject;
                                    Regex PropertyName = new Regex(eveType);
                                    if (!PropertyName.Match(dynamizationType).Success)
                                    {
                                        continue;
                                    }
                                    if (testStr.EndsWith("Event_Delete"))
                                    {
                                        script.SetAttribute("ScriptCode", "");
                                    }
                                    else
                                    {
                                        testStr = "\n" + testStr;
                                        script.SetAttribute("ScriptCode", testStr);
                                    }

                                    break;
                                }
                            }
                            
                            loggingStr = "Update the property : " + setStatement.Key + "  from the Screen : " + hmiScreen.Name + " \n";

                        }
                        else
                        {
                            
                            if (Program.OptionStr.EndsWith("ScreenProperties"))
                            {
                                if (testStr == "Dynamization_Delete")
                                {
                                    foreach (var itemProperties in hmiScreen.GetType().GetProperties())
                                    {
                                        Regex PropertyName = new Regex(itemProperties.Name.ToString());
                                        if (!PropertyName.Match(setStatement.Key).Success)
                                        {
                                            continue;
                                        }
                                        DynamizationBaseComposition dyns = hmiScreen.Dynamizations;
                                        DynamizationBase dynamization = dyns.Find(setStatement.Key);
                                        dynamization.Delete();
                                        break;
                                    }
                                }
                                else if (dynamizationType == "ScriptCode")
                                {
                                    foreach (var itemProperties in hmiScreen.GetType().GetProperties())
                                    {
                                        Regex PropertyName = new Regex(itemProperties.Name.ToString());
                                        if (!PropertyName.Match(setStatement.Key).Success)
                                        {
                                            continue;
                                        }
                                        testStr = "\n" + testStr;
                                        DynamizationBaseComposition dyns = hmiScreen.Dynamizations;
                                        ScriptDynamization scriptDynamic = dyns.Create<ScriptDynamization>(setStatement.Key);
                                        scriptDynamic.GetType().InvokeMember("ScriptCode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, scriptDynamic, new object[] { testStr });
                                        if (triggerType == "Disabled")
                                        {
                                            scriptDynamic.Trigger.Type = TriggerType.Disabled;
                                        }
                                        else if (triggerType == "Tags")
                                        {
                                            scriptDynamic.Trigger.Type = TriggerType.Tags;
                                            scriptDynamic.Trigger.Tags = triggerTags;
                                        }
                                        else
                                        {
                                            scriptDynamic.Trigger.Type = TriggerType.CustomCycle;
                                            scriptDynamic.Trigger.CustomDuration = triggerType;
                                        }
                                        break;
                                    }
                                }
                                else if (dynamizationType == "Tag")
                                {
                                    foreach (var itemProperties in hmiScreen.GetType().GetProperties())
                                    {
                                        Regex PropertyName = new Regex(itemProperties.Name.ToString());
                                        if (!PropertyName.Match(setStatement.Key).Success)
                                        {
                                            continue;
                                        }
                                        DynamizationBaseComposition dyns = hmiScreen.Dynamizations;
                                        TagDynamization tagDynamic = dyns.Create<TagDynamization>(setStatement.Key);
                                        tagDynamic.Tag = testStr.Split('-')[0].Trim();
                                        tagDynamic.ReadOnly = bool.Parse(testStr.Split('-')[1].Trim());
                                        break;
                                    }
                                }
                                else
                                {
                                    SetPropertyRecursive(setStatement.Key, testStr, hmiScreen);
                                }

                                loggingStr = "Update the property : " + setStatement.Key + " from the Screen : " + hmiScreen.Name + " \n";
                            }                           
                            else 
                            {
                                if (testStr == "Dynamization_Delete")
                                {
                                    foreach (var itemProperties in screenItem.GetType().GetProperties())
                                    {
                                        Regex PropertyName = new Regex(itemProperties.Name.ToString());
                                        if (!PropertyName.Match(setStatement.Key).Success)
                                        {
                                            continue;
                                        }
                                        DynamizationBaseComposition dyns = screenItem.Dynamizations;
                                        DynamizationBase dynamization = dyns.Find(setStatement.Key);
                                        dynamization.Delete();
                                        break;
                                    }
                                }
                                else if (dynamizationType == "ScriptCode")
                                {
                                    foreach (var itemProperties in screenItem.GetType().GetProperties())
                                    {
                                        Regex PropertyName = new Regex(itemProperties.Name.ToString());
                                        if (!PropertyName.Match(setStatement.Key).Success)
                                        {
                                            continue;
                                        }
                                        testStr = "\n" + testStr;
                                        DynamizationBaseComposition dyns = screenItem.Dynamizations;
                                        ScriptDynamization scriptDynamic = dyns.Create<ScriptDynamization>(setStatement.Key);
                                        scriptDynamic.GetType().InvokeMember("ScriptCode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, scriptDynamic, new object[] { testStr });
                                        if (triggerType == "Disabled")
                                        {
                                            scriptDynamic.Trigger.Type = TriggerType.Disabled;
                                        }
                                        else if (triggerType == "Tags")
                                        {
                                            scriptDynamic.Trigger.Type = TriggerType.Tags;
                                            scriptDynamic.Trigger.Tags = triggerTags;
                                        }
                                        else
                                        {
                                            scriptDynamic.Trigger.Type = TriggerType.CustomCycle;
                                            scriptDynamic.Trigger.CustomDuration = triggerType;
                                        }
                                        break;
                                    }
                                }
                                else if (dynamizationType == "Tag")
                                {
                                    foreach (var itemProperties in screenItem.GetType().GetProperties())
                                    {
                                        Regex PropertyName = new Regex(itemProperties.Name.ToString());
                                        if (!PropertyName.Match(setStatement.Key).Success)
                                        {
                                            continue;
                                        }
                                        DynamizationBaseComposition dyns = screenItem.Dynamizations;
                                        TagDynamization tagDynamic = dyns.Create<TagDynamization>(setStatement.Key);
                                        tagDynamic.Tag = testStr.Split('-')[0].Trim();
                                        tagDynamic.ReadOnly = bool.Parse(testStr.Split('-')[1].Trim());
                                        break;
                                    }
                                }
                                else
                                {
                                    SetPropertyRecursive(setStatement.Key, testStr, screenItem);
                                }
                                loggingStr = "Update the property : " + setStatement.Key + " Of the Item : " + screenItem.Name + Program.ScreenItemUpdate + " from the Screen : " + hmiScreen.Name + " \n";                                
                            }
                            Program.ScreenItemUpdate = "";
                        }
                        changeNum++;
                    }
                    catch (Exception ex)
                    {
                        loggingStr = ex.Message;
                    }
                    LogAndXmlOP.LogOut("Log", loggingStr, 0);
                    Program.StrShow += loggingStr;
                }
            }
            return changeNum;
        }

        public IEnumerable<HmiTag> RecursiveTags(HmiTag parent, string parentFullTagName)
        {
            IEnumerable<HmiTag> tags = new List<HmiTag>();
            Conditionstolist();
            if (Compare_tag(parent, parentFullTagName))/////////////////compare tag
            {
                tags = tags.Concat(new[] { parent });
            }
            /*
            if (parent.Members.Count == 0)
            {
                Conditionstolist();
                if (Compare_tag(parent, parentFullTagName))/////////////////compare tag
                {
                    tags = tags.Concat(new[] { parent });
                }
            }
            else
            {
                foreach (var childTag in parent.Members)
                {
                    tags = tags.Concat(RecursiveTags(childTag, parentFullTagName + "." + childTag.Name));
                }
            }
            */
            return tags;
        }
        // gibt alle Geräte mit WinCC Unified in einer Liste zurück
        public bool IsRelevant(dynamic tag, string fullTagName, List<string> whereConditionsListNew)//only for all kinds tags
        {
            bool resultBool = false;
            foreach (var whereCondition in whereConditionsListNew)
            {
                if (whereCondition.Trim().Length == 0)
                {
                    continue;
                }
                string[] split = whereCondition.Trim().Split(new[] { "&$&" }, StringSplitOptions.None);
                if (split[0].Trim() != "And" && split[0].Trim() != "Or")
                {
                    if (split[1].Trim() == "=")
                    {
                        string rx = split[2].Trim();
                        string property = split[0].Trim() == "Name" ? fullTagName : tag.GetAttributes(new List<string> { split[0] })[0].ToString();
                        resultBool = (rx == property);
                    }
                    else
                    {
                        Regex rx = new Regex(split[2].Trim().Replace(".", "\\."), RegexOptions.Compiled);//^And $are the start and end characters. However, this is not applicable to fuzzy query criteria.
                        string property = split[0].Trim() == "Name" ? fullTagName : tag.GetAttributes(new List<string> { split[0] })[0].ToString();
                        resultBool = rx.Match(property).Success;
                    }
                }
                else
                {
                    bool itemResultBool;
                    if (split[2].Trim() == "=")
                    {
                        string rx = split[3].Trim();
                        string property = split[1].Trim() == "Name" ? fullTagName : tag.GetAttributes(new List<string> { split[1] })[0].ToString();
                        itemResultBool = (rx == property);
                    }
                    else
                    {
                        Regex rx = new Regex(split[3].Trim().Replace(".", "\\."), RegexOptions.Compiled);//^And $are the start and end characters. However, this is not applicable to fuzzy query criteria.
                        string property = split[1].Trim() == "Name" ? fullTagName : tag.GetAttributes(new List<string> { split[1] })[1].ToString();
                        itemResultBool = rx.Match(property).Success;
                    }
                    if (split[0].Trim() == "And")
                    {
                        resultBool = resultBool && itemResultBool;
                    }
                    else
                    {
                        resultBool = resultBool || itemResultBool;
                    }
                }
            }
            return resultBool;
        }

        public bool isRelevant_screen(string nameStr, string typeStr, List<string> whereConditionsListNew)//only for screen items
        {
            bool resultBool = false;
            foreach (var whereCondition in whereConditionsListNew)
            {
                if (whereCondition.Length == 0)
                {
                    continue;
                }
                string[] split = whereCondition.Trim().Split(new[] { "&$&" }, StringSplitOptions.None);
                if (split[0].Trim() != "And" && split[0].Trim() != "Or")
                {
                    if (split[1].Trim() == "=")
                    {
                        string rx = split[2].Trim();
                        string property = split[0].Trim() == "Name" ? nameStr : typeStr;
                        resultBool = (rx == property);
                    }
                    else
                    {
                        Regex rx = new Regex(split[2].Trim().Replace(".", "\\."), RegexOptions.Compiled);//^And $are the start and end characters. However, this is not applicable to fuzzy query criteria.
                        string property = split[0].Trim() == "Name" ? nameStr : typeStr;
                        resultBool = rx.Match(property).Success;
                    }
                }
                else
                {
                    bool itemResultBool;
                    if (split[2].Trim() == "=")
                    {
                        string rx = split[3].Trim();
                        string property = split[1].Trim() == "Name" ? nameStr : typeStr;
                        itemResultBool = (rx == property);
                    }
                    else
                    {
                        Regex rx = new Regex(split[3].Trim().Replace(".", "\\."), RegexOptions.Compiled);//^And $are the start and end characters. However, this is not applicable to fuzzy query criteria.
                        string property = split[1].Trim() == "Name" ? nameStr : typeStr;
                        itemResultBool = rx.Match(property).Success;
                    }
                    if (split[0].Trim() == "And")
                    {
                        resultBool = resultBool && itemResultBool;
                    }
                    else
                    {
                        resultBool = resultBool || itemResultBool;
                    }
                }
            }
            return resultBool;
        }

        public void Conditionstolist()//Collating conditional statements,change it from string to a list
        {

            Program.WhereConditionsList.Clear();
            string groupLogic = "";
            if (Program.WhereConditionsStr.Contains("&$&( &$&"))
            {
                string[] split = Program.WhereConditionsStr.Split(new[] { " &$&( &$& " }, StringSplitOptions.None);
                foreach (string str in split)
                {
                    string strNew = str.Trim();
                    if (str.Contains("&$&  )"))
                    {
                        string[] split1 = str.Split(new[] { "&$&  )" }, StringSplitOptions.None);//split1[0] is the group,split1[1] is another part
                        string[] strAdd = { groupLogic, split1[0] };
                        Program.WhereConditionsList.Add(strAdd);

                        if (split1[1].Trim().EndsWith("Or"))
                        {
                            groupLogic = "Or";
                        }
                        else if (split1[1].Trim().EndsWith("And"))
                        {
                            groupLogic = "And";
                        }
                        if (split1[1].Trim().Contains("&$& Or"))
                        {
                            string[] split2 = split1[1].Trim().Split(new[] { "&$& Or" }, StringSplitOptions.None);
                            foreach (string str1 in split2)
                            {
                                if (str1.Trim().Contains("&$& And"))
                                {
                                    string[] split3 = str1.Split(new[] { "&$& And" }, StringSplitOptions.None);
                                    int i = 0;
                                    foreach (string str2 in split3)
                                    {
                                        if (i == 0)
                                        {
                                            string[] strAdd1 = { "Or", str2 };
                                            Program.WhereConditionsList.Add(strAdd1);
                                        }
                                        else
                                        {
                                            string[] strAdd1 = { "And", str2 };
                                            Program.WhereConditionsList.Add(strAdd1);
                                        }
                                    }

                                }
                                else if (str1.Length > 0)
                                {
                                    string[] strAdd1 = { "Or", str1 };
                                    Program.WhereConditionsList.Add(strAdd1);
                                }
                            }
                        }
                        else if (split1[1].Trim().Contains("&$& And"))
                        {
                            string[] split3 = Program.WhereConditionsStr.Split(new[] { "&$& And" }, StringSplitOptions.None);
                            foreach (string str2 in split3)
                            {
                                string[] strAdd1 = { "And", str2 };
                                Program.WhereConditionsList.Add(strAdd1);
                            }
                        }
                    }
                    else
                    {
                        if (strNew.Trim().EndsWith("Or"))
                        {
                            if (groupLogic == "")
                            {
                                string[] strAdd = { "", strNew.Substring(0, (strNew.Length - 2)) };
                                Program.WhereConditionsList.Add(strAdd);
                            }
                            else
                            {
                                string[] strAdd = { groupLogic, strNew.Substring(0, (strNew.Length - 2)) };
                                Program.WhereConditionsList.Add(strAdd);
                            }
                            groupLogic = "Or";
                        }
                        else if (strNew.Trim().EndsWith("And"))
                        {
                            if (groupLogic == "")
                            {
                                string[] strAdd = { "", strNew.Substring(0, (strNew.Length - 3)) };
                                Program.WhereConditionsList.Add(strAdd);
                            }
                            else
                            {
                                string[] strAdd = { groupLogic, strNew.Substring(0, (strNew.Length - 3)) };
                                Program.WhereConditionsList.Add(strAdd);
                            }
                            groupLogic = "And";
                        }
                    }
                }
            }
            else
            {
                string[] strAdd = { "", Program.WhereConditionsStr };
                Program.WhereConditionsList.Add(strAdd);
            }
        }

        public bool Compare_tag(dynamic tag, string fullTagName)
        {
            bool resultBool = false;
            bool resultItem;
            foreach (string[] str in Program.WhereConditionsList)
            {
                List<string> whereConditionslistnew = new List<string>();
                if (str[1].Contains("&$& And&$&") || str[1].Contains("&$& Or&$&"))
                {
                    string[] split = str[1].Split(new[] { " &$& " }, StringSplitOptions.None);
                    foreach (string str1 in split)
                    {
                        whereConditionslistnew.Add(str1);
                    }
                }
                else
                {
                    whereConditionslistnew.Add(str[1]);
                }
                resultItem = IsRelevant(tag, fullTagName, whereConditionslistnew);
                if (str[0] == "And")
                {
                    resultBool = resultBool && resultItem;
                }
                else if (str[0] == "Or")
                {
                    resultBool = resultBool || resultItem;
                }
                else
                {
                    resultBool = resultItem;
                }
            }
            return resultBool;
        }
        public bool Compare_screen(string namestr, string typestr)
        {
            bool resultBool = false;
            bool resultItem;
            foreach (string[] str in Program.WhereConditionsList)
            {
                if (str[1].Trim().StartsWith("&$&"))
                {
                    str[1] = str[1].Trim().Substring(3);
                }
                List<string> whereConditionsListNew = new List<string>();
                if (str[1].Contains("&$& And&$&") || str[1].Contains("&$& Or&$&"))
                {
                    string[] split = str[1].Split(new[] { " &$& " }, StringSplitOptions.None);
                    foreach (string str1 in split)
                    {
                        whereConditionsListNew.Add(str1);
                    }
                }
                else
                {
                    whereConditionsListNew.Add(str[1]);
                }
                resultItem = isRelevant_screen(namestr, typestr, whereConditionsListNew);
                if (str[0] == "And")
                {
                    resultBool = resultBool && resultItem;
                }
                else if (str[0] == "Or")
                {
                    resultBool = resultBool || resultItem;
                }
                else
                {
                    resultBool = resultItem;
                }
            }
            return resultBool;
        }
    }
}
