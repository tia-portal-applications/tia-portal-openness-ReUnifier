using Siemens.Engineering;
using Siemens.Engineering.HmiUnified;
using Siemens.Engineering.HW;
using Siemens.Engineering.HW.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReUnifier
{
    public partial class Proselect : Form
    {
        public Proselect()
        {
            InitializeComponent();
        }
        InsertNewItem insertNewItem = new InsertNewItem();
        public Project MyProject;
        public TiaPortal MyTiaPortal;
        TiaPortal tiaPortal = null;
        private void Proselect_Load(object sender, EventArgs e)
        {
            Program.ProOpened.Clear();
            TiaPortalDiagnostics();          
            comboBoxProName.Text = Program.StrShow;          
        }
        public void TiaPortalDiagnostics()
        {
            IList<TiaPortalProcess> tiaPortalProcesses = TiaPortal.GetProcesses();
            foreach (TiaPortalProcess tiaPortalProcess in tiaPortalProcesses)
            {
                if (tiaPortalProcess.ProjectPath != null)
                {
                    string ProNameStr = tiaPortalProcess.ProjectPath.ToString();
                    comboBoxProName.Items.Add(ProNameStr);
                    Program.ProOpened.Add(ProNameStr, tiaPortalProcess);
                }
            }
        }
        private void buttonApply_Click(object sender, EventArgs e)
        {
            String ProjectPath = comboBoxProName.Text;
            try
            {
                if(Program.ProPathStr != ProjectPath && Program.ProPathStr!=null)
                {
                    TiaPortalProcess tiaPortalProcessOld = Program.ProOpened[Program.ProPathStr];
                    foreach (TiaPortalSession session in tiaPortalProcessOld.AttachedSessions)
                    {
                        session.Dispose();
                    }
                }
                if (Program.ProOpened.ContainsKey(ProjectPath))
                {
                    TiaPortalProcess tiaPortalProcess = Program.ProOpened[ProjectPath];
                    Program.hmiSoftware = GetHmiSoftwares(ref tiaPortal, tiaPortalProcess).FirstOrDefault();
                }
                Program.ProPathStr = ProjectPath;
            }
            catch (Exception ex)
            {
                Program.StrShow = ProjectPath + " Error while opening project" + ex.Message;
                comboBoxProName.Text = Program.StrShow;
            }
            Program.ProPathStrOpen = comboBoxProName.Text;
            Form2 form2 = new Form2();
            form2.Show();
            this.Close();
        }
        
        public IEnumerable<HmiSoftware> GetHmiSoftwares(ref TiaPortal tiaPortal, TiaPortalProcess tiaPortalProcess)
        {
            
            tiaPortal = tiaPortalProcess.Attach();
            Project tiaPortalProject = tiaPortal.Projects[0];
            return
            from device in tiaPortalProject.Devices
            from deviceItem in device.DeviceItems
            let softwareContainer = deviceItem.GetService<Siemens.Engineering.HW.Features.SoftwareContainer>()
            where softwareContainer?.Software is HmiSoftware
            select softwareContainer.Software as HmiSoftware;

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Close();
        }
    }
}
