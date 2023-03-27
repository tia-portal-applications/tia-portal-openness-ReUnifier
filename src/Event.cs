using Siemens.Engineering;
using Siemens.Engineering.Hmi.Screen;
using Siemens.Engineering.HmiUnified.UI.Events;
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
    public delegate void EventTaskDelegate(string str);
    public partial class Event : Form
    {
        public event EventTaskDelegate TaskEvent;
        public Event()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Event_Load(object sender, EventArgs e)
        {
            this.EventType.Items.Clear();
            this.EventType.Text = "";
            if (Program.EventList.Count>0)
            {
                this.EventType.Items.AddRange(Program.EventList.ToArray());
            }
        }

        private void EventType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.EventType.Text !="")
            {
                if(Program.OptionStr.EndsWith("ScreenProperties"))
                {
                    foreach (var screen in Program.GetScreens())
                    {
                        foreach (HmiScreenEventHandler eveHandler in screen.EventHandlers)
                        {                           
                            string eveType = eveHandler.GetAttribute("EventType").ToString();
                            if (this.EventType.Text == eveType)
                            {
                                IEngineeringObject script = eveHandler.GetAttribute("Script") as IEngineeringObject;
                                this.ScriptTextBox.Text = script.GetType().GetProperty("ScriptCode").GetValue(script).ToString();
                                return;
                            }

                        }
                    }
                }
                else
                {
                    foreach (var screenItem in Program.ItemList)
                    {
                        IEngineeringObject itemConcreto = screenItem;
                        foreach (IEngineeringObject eveHandItem in itemConcreto.GetComposition("EventHandlers") as IEngineeringComposition)
                        {
                            string eveType = eveHandItem.GetAttribute("EventType").ToString();
                            if (this.EventType.Text == eveType)
                            {
                                IEngineeringObject script = eveHandItem.GetAttribute("Script") as IEngineeringObject;
                                this.ScriptTextBox.Text = script.GetType().GetProperty("ScriptCode").GetValue(script).ToString();
                                return;
                            }
                        }
                    }
                }
                
            }
            
        }

        private void EventUpdate_Click(object sender, EventArgs e)
        {
            string EventCodeStr = this.EventType.Text + " &&& " + this.ScriptTextBox.Text;           
            TaskEvent(EventCodeStr);
            this.Close();
        }

        private void EventDelete_Click(object sender, EventArgs e)
        {
            string EventCodeStr = this.EventType.Text + " &&& " + "Event_Delete";
            TaskEvent(EventCodeStr);
            this.Close();
        }
    }
}
