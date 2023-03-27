using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Diagnostics;

namespace Loggingtext
{
    static public class OnlyOneStartUp
    {
        static public string UseLogPath = AppDomain.CurrentDomain.BaseDirectory; //LOG文件目录
        static public string UseConfigPath = AppDomain.CurrentDomain.BaseDirectory + "/Config";
    }

    static public class LogAndXmlOP
    {
        /// <summary>输出信息至LOG文件
        /// 输出信息至LOG文件
        /// </summary>
        /// <param name="FileName">文件名称</param>
        /// <param name="LogMsg">输出内容</param>
        static public void LogOut(string FileName, string LogMsg, int path)
        {
            try
            {
                DateTime dt = DateTime.Now;
                string AllFileName = "";
                string Wvalue = "";
                string FilePath = "";
                if (path == 0)
                {
                    FilePath = OnlyOneStartUp.UseLogPath + "/Log";
                    //Console.Write(FilePath+"\n");
                    AllFileName = string.Format("{0}/{1}_{2:0000}{3:00}{4:00}.txt",
                 FilePath, FileName, dt.Year, dt.Month, dt.Day);
                    Wvalue = string.Format("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}.{6:000}({7:0000000000}) {8}\r\n",
                                    dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, Environment.TickCount, LogMsg);                  
                }
                else if (path == 1)
                {
                    FilePath = OnlyOneStartUp.UseLogPath + "/DataBase";
                    //Console.Write(FilePath + "\n");
                    AllFileName = string.Format("{0}/{1}_{2:0000}{3:00}{4:00}_{5:00}{6:00}{5:00}{6:000}{7:0000000000}.txt",
                      FilePath, FileName, dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, Environment.TickCount);
                    Wvalue = string.Format("{0}\r\n", LogMsg);
                }
                else if (path == 2)
                {
                    FilePath = OnlyOneStartUp.UseLogPath + "/AbsLog";
                    //Console.Write(FilePath + "\n");
                    AllFileName = string.Format("{0}/{1}_{2:0000}{3:00}{4:00}_{5:00}{6:00}{5:00}{6:000}{7:0000000000}.txt",
                      FilePath, FileName, dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, Environment.TickCount);
                    Wvalue = string.Format("{0}\r\n", LogMsg);
                }
                if (!Directory.Exists(FilePath))
                    Directory.CreateDirectory(FilePath);
                StreamWriter fwr = new StreamWriter(AllFileName, true);
                fwr.Write(Wvalue);
                fwr.Close();
                fwr.Dispose();
            }
            catch (Exception e)
            {
                //  string sError = string.Format("Log输出Error:{0}", e.Message);
            }
        }
        static public void LogHex(string LogFile, string Mark, byte[] buffer)
        {
            string Wvalue = Mark + ":";
            for (int i = 0; i < buffer.Length; i++)
                Wvalue += string.Format(" {0:X2}", buffer[i]);
            LogOut(LogFile, Wvalue, 0);
        }

        static public bool OpenLog(string FileName)
        {
            try
            {
                DateTime dt = DateTime.Now;
                string FilePath = OnlyOneStartUp.UseLogPath;
                string AllFileName = string.Format("{0}\\{1}_{2:0000}{3:00}{4:00}.log",
                      FilePath, FileName, dt.Year, dt.Month, dt.Day);
                Process.Start(AllFileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary> 写入XML配置文件
        /// 写入XML配置文件
        /// </summary>
        /// <param name="FileName">XML配置文件名称</param>
        /// <param name="KeyName">键名</param>
        /// <param name="Value">值</param>
        static public void WirteConfig(string FileName, string Group, string KeyName, string Value)
        {
            WirteConfig(FileName, string.Empty, Group, KeyName, Value);
        }     //推荐使用
        static public void WirteConfig(string FileName, string KeyName, string Value)
        {
            WirteConfig(FileName, string.Empty, string.Empty, KeyName, Value);
        }
        static public void WirteConfig(string FileName, string Section, string Group, string KeyName, string Value)
        {
            //<?xml version="1.0" standalone="yes"?>
            //<配置名>
            //  <组名>
            //    <标签名KeyName>Value</标签名KeyName>
            //  </组名>
            //</配置名>

            try
            {
                string FilePath = OnlyOneStartUp.UseConfigPath;
                string AllFileName = FilePath + "\\" + FileName + ".xml";
                if (!Directory.Exists(FilePath))
                    Directory.CreateDirectory(FilePath);

                DataSet ds; //配置名
                if (string.IsNullOrEmpty(Section))
                {
                    ds = new DataSet("NewConfig");
                }
                else
                {
                    ds = new DataSet(Section);

                }

                if (File.Exists(AllFileName))
                    ds.ReadXml(AllFileName);

                if (ds.Tables.IndexOf(Group) < 0)
                    ds.Tables.Add(Group);

                if (ds.Tables[Group].Rows.Count < 1)
                    ds.Tables[Group].Rows.Add();

                if (!ds.Tables[Group].Columns.Contains(KeyName))
                    ds.Tables[Group].Columns.Add(KeyName);
                ds.Tables[Group].Rows[0][KeyName] = Value;

                ds.WriteXml(AllFileName);
            }


            catch (Exception e)
            {
                string sError = string.Format("写入配置信息Error:{0}", e.Message);
                //MessageBox.Show(sError);
                //ErrorOut(MethodInfo.GetCurrentMethod().Name, sError);
            }
        }

        /// <summary> 读出XML配置文件
        /// 读出XML配置文件
        /// </summary>
        /// <param name="FileName">XML配置文件名称</param>
        /// <param name="KeyName">键名</param>
        /// <param name="Value">返回的值</param>
        static public bool ReadConfig(string FileName, string KeyName, ref string Value)
        {
            return ReadConfig(FileName, string.Empty, KeyName, ref Value);
        } //推荐使用
        static public bool ReadConfig(string FileName, string Group, string KeyName, ref string Value)
        {

            try
            {
                string FilePath = OnlyOneStartUp.UseConfigPath;
                string AllFileName = FilePath + "\\" + FileName + ".xml";
                if (!Directory.Exists(FilePath))
                    return false;

                DataSet ds = new DataSet();
                if (File.Exists(AllFileName))
                    ds.ReadXml(AllFileName);
                else return false;

                if (ds.Tables.Count < 1)
                    return false;

                DataTable dt;
                if (string.IsNullOrEmpty(Group))
                {
                    dt = ds.Tables[0];
                }
                else
                {
                    dt = ds.Tables[Group];
                }

                if (dt.Rows.Count < 1)
                    return false;

                if (!dt.Columns.Contains(KeyName))
                    return false;

                Value = Convert.ToString(dt.Rows[0][KeyName]);
                return true;
            }
            catch (Exception e)
            {
                string sError = string.Format("读取配置信息Error:{0}", e.Message);
                //MessageBox.Show(sError);
                //ErrorOut(MethodInfo.GetCurrentMethod().Name, sError);
                return false;
            }
        }

        static public void ReadConfigEx(string FileName, string KeyName, ref string Value)
        {
            if (!ReadConfig(FileName, KeyName, ref Value))
                WirteConfig(FileName, KeyName, Value);
        }


    }
}

//使用的例子
//private void butWriteLog_Click(object sender, EventArgs e)
//       {
//           LogOut("1212", tbLog.Text);
//       }
//       private void button1_Click(object sender, EventArgs e)
//       {
//           if (!OpenLog("1212"))
//           {
//               MessageBox.Show("未找到今天的日志");
//           }

//       }

//       private void btnWriteXML_Click(object sender, EventArgs e)
//       {
//           //WirteConfig("aa", "hhl2", tbXML.Text);
//           WirteConfig("aa", "hhl77ww", "hhl2", tbXML.Text);
//       }

//       private void btnReadXML_Click(object sender, EventArgs e)
//       {
//           string aaa = "";
//           //ReadConfig("aa", "hhl2", ref aaa);
//           ReadConfig("aa", "hhl77ww", "hhl2", ref aaa);
//           labXML.Text = aaa;
//       }