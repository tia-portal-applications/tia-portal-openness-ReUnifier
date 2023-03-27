using Siemens.Engineering;
using Siemens.Engineering.HmiUnified;
using Siemens.Engineering.HmiUnified.UI.Base;
using Siemens.Engineering.HmiUnified.UI.Dynamization.Script;
using Siemens.Engineering.HmiUnified.UI.ScreenGroup;
using Siemens.Engineering.HmiUnified.UI.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReUnifier
{
    static class Program
    {
        public static HmiSoftware HmiSoftware;
        public static string ScreenName { set; get; }
        public static List<string[]> WhereConditionsList = new List<string[]>();
        public static string WhereConditionsStr = "";//collect the select conditions,which will be use in different cs.
        public static string ConditionStr = "";
        public static Dictionary<string, TiaPortalProcess> ProOpened = new Dictionary<string, TiaPortalProcess>();
        public static Dictionary<string, string> SetStatements = new Dictionary<string, string>();//collect the setting informations,which will be use in different cs.
        public static string StatementStr = "";
        public static string StrColorSet;//shows in the setting information page after choosing the color.
        public static string StrShow = "";//shows in the checklistbox after executing.
        public static int ProState = 0;
        //ProState:0:have not connect any projects. 1:just connect a project. 2:finished choosing the option. 3:finished choosing the selected conditions.4:finished choosing the setting informations. 5:finished executing. 
        public static string OperationStr = "";   // shows the option result in every steps.
        public static string OptionStr = "";//help us make sure if the option is delete or insert or other options    
        public static string ScreenItemUpdate = "";
        public static List<HmiScreenItemBase> ItemList = new List<HmiScreenItemBase>();
        public static string DynamizationStr = "";
        public static string DynamizationType = "";
        public static Trigger DynamizationTrigger ;
        public static string setFontType = "";
        public static List<string> EventList = new List<string>();
        public static string EventTypeName = "";
        public static string EventCodeStr = "";

        /// <summary>
        /// Main。
        /// </summary>
        [STAThread]
       
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }

        
        // Get all of screens which is contaned by the Unified Panel
        // 
        public static IEnumerable<HmiScreen> GetScreens()
        {
            var allScreens = HmiSoftware.Screens.ToList();
            allScreens.AddRange(ParseGroups(HmiSoftware.ScreenGroups));
            return allScreens;
        }

        private static IEnumerable<HmiScreen> ParseGroups(HmiScreenGroupComposition parentGroups)
        {
            foreach (var group in parentGroups)
            {
                foreach (var screen in group.Screens)
                {
                    yield return screen;
                }
                foreach (var screen in ParseGroups(group.Groups))
                {
                    yield return screen;
                }
            }
        }

        }
}
