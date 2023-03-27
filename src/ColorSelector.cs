using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using ColorHelper;

namespace ReUnifier
{
    public partial class ColorSelector : UserControl
    {
        public String XmlPath = "C:\\ReUnifier\\HmiReUnifier.xml";
        public string ToFavorites;
        public bool WriteXml;
        public List<string> XmlDateCompose = new List<string>{ "","","","","","","","" };
        public void GetXmlInformation(string xmlFilePath)
        {
            try
            {
                //Initialize an XML instance
                XmlDocument myXmlDoc = new XmlDocument();
                //Load XML file (the parameter is the path of XML file)
                myXmlDoc.Load(xmlFilePath);
                //Get the first node with matching name (selectsinglenode): the root node of this XML file
                //XmlNode rootNode = myXmlDoc.SelectSingleNode("HmiTags");
                XmlNode node = myXmlDoc.SelectSingleNode("//Objects/Screens/ColorFavorites");
                if (node != null)
                {
                    String xmlDate = node.InnerText;

                    if (WriteXml)
                    {
                        node.InnerText = ToFavorites;
                        myXmlDoc.Save(XmlPath);

                    }
                    else
                    {
                        List<string> xmlDateSplit = xmlDate.Split(',').ToList();

                        for (int j = 49; j < 57; j++)
                        {
                            ColorBox cb2 = (ColorBox)Controls.Find("colorBox" + j.ToString(), true)[0];
                            cb2.BackColor = ColorTranslator.FromHtml(xmlDateSplit[j - 49]);
                            Console.WriteLine(xmlDateSplit[j - 49]);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public Control PaintControl = new Control();
        public ColorSelector()
        {
            InitializeComponent();
            LoadColorBox();
            LoadSystemColors();
        }

        //Selected color
        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set => _selectedColor = value;
        }

        //Default color used
        public Color DefaultColor { get; set; }

        private void LoadColorBox()
        {
            //Default used color sets common colors
            string[] sColors=new string[]{"&HFFFFFF","&HC0C0FF","&HC0E0FF","&HC0FFFF","&HC0FFC0","&HFFFFC0","&HFFC0C0","&HFFC0FF" ,
                          "&HE0E0E0","&H8080FF","&H80C0FF","&H80FFFF","&H80FF80","&HFFFF80","&HFF8080","&HFF80FF",
                          "&HC0C0C0","&H0000FF","&H0080FF","&H00FFFF","&H00FF00","&HFFFF00","&HFF0000","&HFF00FF",
                          "&H808080","&H0000C0","&H0040C0","&H00C0C0","&H00C000","&HC0C000","&HC00000","&HC000C0",
                          "&H404040","&H000080","&H004080","&H008080","&H008000","&H808000","&H800000","&H800080",
                          "&H000000","&H000040","&H404080","&H004040","&H004000","&H404000","&H400000","&H400040"};
            for (int i = 1; i < 49; i++)
            {
               ColorBox cb= (ColorBox)Controls.Find("colorBox"+i.ToString(),true)[0];
               cb.BackColor = ColorTranslator.FromHtml(sColors[i-1]);
            }
            GetXmlInformation(XmlPath);
            ColorBox cbTest = (ColorBox)Controls.Find("colorBox8", true)[0];
            //testColor = cbTest.BackColor;
            //ReUnifier.Program.StrColorSet = ColorTranslator.ToHtml(testColor);

            //Add event handling
            for (int i = 1; i < 57; i++)
            {
                ColorBox cb = (ColorBox)Controls.Find("colorBox" + i.ToString(), true)[0];
                cb.MouseClick += cb_MouseClick;                    
            }
        }

        private void cb_MouseClick(object sender, MouseEventArgs e)
        {
            ColorBox cb=(ColorBox)sender;
            cb.BorderColor =  Color.FromArgb(65, 173, 255);
            _selectedColor = cb.BackColor;
            this.Visible = false;
            PaintControl.BackColor = _selectedColor;
            PaintControl.Invalidate();
        }

        private void LoadSystemColors()
        {
            lbSysColors.Items.Clear();
            Array allColors = Enum.GetValues(typeof(KnownColor)); //Get system color set                                                     
            foreach (KnownColor var in allColors)
            {
                lbSysColors.Items.Add(var.ToString());  //Load the children of the option box
            }
            lbSysColors.SelectedIndex = 0;
        }

        //Draw color drop-down box
        private void lbSysColors_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                //Draw selected effect
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    SolidBrush brushItem = new SolidBrush(SystemColors.MenuHighlight); //paint brush
                    Rectangle rectItem = e.Bounds;
                    e.Graphics.FillRectangle(brushItem, rectItem);
                }
                else
                {
                    SolidBrush brushItem = new SolidBrush(SystemColors.Window); //paint brush
                    Rectangle rectItem = e.Bounds;
                    e.Graphics.FillRectangle(brushItem, rectItem);
                }

                string colorName = lbSysColors.Items[e.Index].ToString();  //The color name of the child
                SolidBrush brush = new SolidBrush(Color.FromName(colorName)); //paint brush
                Font font = new Font("Arial", 9);                                                       //Font style               
                Brush brushs = Brushes.Black;
                Rectangle rect = e.Bounds;                                                               //Get the area to redraw
                rect.Inflate(-2, -2);                                                                             //Zoom to a certain size

                Rectangle rectColor = new Rectangle(rect.Location, new Size(20, rect.Height));
                e.Graphics.FillRectangle(brush, rectColor);                                                  // fill color   
                e.Graphics.DrawRectangle(Pens.Black, rectColor);                                       // bound box   

                //Draw text
                e.Graphics.DrawString(colorName, font, brushs, (rect.X + 22), rect.Y);
            }
        }


        int _index = 49;
        //Enter more color settings
        private void lblMore_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            cd.AnyColor = true;
            
            if (cd.ShowDialog() == DialogResult.OK)
            {
                if (_index > 56)//Cyclic assignment
                    _index = 49;
                ColorBox cb = (ColorBox)Controls.Find("colorBox" + _index.ToString(), true)[0];
                cb.BackColor = cd.Color;
                _index++;
            }
            for (int j = 49; j < 57; j++)
            {
                ColorBox cb3 = (ColorBox)Controls.Find("colorBox" + j.ToString(), true)[0];
                XmlDateCompose[j - 49] = ColorTranslator.ToHtml(cb3.BackColor);

                // cb2.BackColor = ColorTranslator.FromHtml(xmlDateSplit[j - 49]);
                //Console.WriteLine(xmlDateSplit[j - 49]);
            }
            ToFavorites = string.Join(",", XmlDateCompose);
            
            WriteXml = true;
            GetXmlInformation(XmlPath);
            WriteXml = false;
        }

        private void lbSysColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            string colorName = lbSysColors.SelectedItem.ToString();
            _selectedColor = Color.FromName(colorName);
            this.Visible = false;
            PaintControl.BackColor = _selectedColor;
            PaintControl.Invalidate();
        }
    }
}
