using KindleManager.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace KindleManager
{


    public partial class MyClippingForm : Form
    {

        List<ClippingInfo> Items;
        readonly string titleIdentifier = "_____Title:";

        public MyClippingForm()
        {
            InitializeComponent();
        }

        private void MyClippingForm_Load(object sender, EventArgs e)
        {

            var filePath = Utils.USBDevice.GetMyClippingPath();
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return;

            int counter = 0;
            string line;

            // Read the file and display it line by line.
            StreamReader file =
                new StreamReader(filePath);

            StringBuilder strBuilder = null;

            Items = new List<ClippingInfo>();

            var documentTitle = "";


            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("==========") && strBuilder != null)
                {

                    if (strBuilder != null)
                    {
                        var val = strBuilder.ToString();
                        if (!string.IsNullOrEmpty(val))
                        {
                            Items.Add(new ClippingInfo
                            {
                                Content = val,
                                Title = documentTitle
                            });
                        }
                    }
                    strBuilder = null;
                    documentTitle = "";
                }

                if (strBuilder != null && !string.IsNullOrEmpty(line))
                {
                    strBuilder.Append(line);
                }

                if (line.StartsWith("- Your Note at location") ||
                    line.StartsWith("- Your Highlight at location") ||
                    line.StartsWith("- Your Bookmark at location"))
                {
                    strBuilder = new StringBuilder();
                }

                if (strBuilder == null)
                    documentTitle = line;

                System.Console.WriteLine(line);
                counter++;
            }

            Items.Reverse();

            foreach (var item in Items)
            {
                listBox1.Items.Add(titleIdentifier + item.Title);
                listBox1.Items.Add(item.Content);
            }

            file.Close();
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index > -1)
            {
                string text = (string)listBox1.Items[e.Index];

                bool isTitle = text.StartsWith(titleIdentifier);

                text = text.Replace(titleIdentifier, "");

                /*Normal items*/
                if ((e.State & DrawItemState.Focus) == 0)
                {
                    if (isTitle)
                    {
                        e.Graphics.FillRectangle(
                       new SolidBrush(SystemColors.ControlLight),
                       e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(
                             new SolidBrush(SystemColors.Window),
                             e.Bounds);
                    }
                }
                else /*Selected item, needs highlighting*/
                {
                    e.Graphics.FillRectangle(
                        new SolidBrush(SystemColors.Highlight),
                        e.Bounds);
                }
                if (isTitle)
                {

                    e.Graphics.DrawString(text, Font,
                        new SolidBrush(SystemColors.WindowText),
                        new Point(0, e.Bounds.Y + 5));
                }
                else
                {

                    e.Graphics.DrawString(text, Font,
                        new SolidBrush(SystemColors.WindowText),
                        e.Bounds);
                }
            }

        }

        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {

            string text = (string)listBox1.Items[e.Index];
            text = text.Replace(titleIdentifier, "");
            SizeF sf = e.Graphics.MeasureString(text, Font, Width);
            int htex = (e.Index == 0) ? 15 : 10;
            e.ItemHeight = (int)sf.Height + htex;
            e.ItemWidth = Width;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var text = (string)listBox1.Items[listBox1.SelectedIndex];
            text = text.Replace(titleIdentifier, "");
            System.Diagnostics.Process.Start("https://www.google.com/search?q=" + text);
        }
    }
}
