using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static LCD_UI_Desigin_EX.WaterUseage;

namespace LCD_UI_Desigin_EX
{
    public partial class Form1 : Form
    {

        private FloorNumber elevatorControl_F;
        private FloorText elevatorControl_T;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            elevatorControl_F = floorNumber1;
            elevatorControl_T = floorText1;

            UpdateFloorInfo();

            elevatorControl_F.MoveToFloorByIndex(0);
            elevatorControl_T.MoveToFloorByIndex(0);
        }

        private void UpdateFloorInfo()
        {
            elevatorControl_F.AddFloor("3F");
            elevatorControl_T.AddFloor("전망대", "Observatory");
            elevatorControl_F.AddFloor("2F");
            elevatorControl_T.AddFloor("국제선 출발층", "International Departure Floor");
            elevatorControl_F.AddFloor("1F");
            elevatorControl_T.AddFloor("입국장", "Arrivals");
            elevatorControl_F.AddFloor("B1");
            elevatorControl_T.AddFloor("정부종합행정센터", "General Government Center");
            elevatorControl_F.AddFloor("B2");
            elevatorControl_T.AddFloor("항공사 사무실", "Airline offices");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int currentFloorIndex = elevatorControl_F._floors.IndexOf(elevatorControl_F._floors.FirstOrDefault(f => f.FontWeight == FontWeights.Bold));

            if (e.KeyCode == Keys.PageUp && currentFloorIndex > 0)
            {
                elevatorControl_F.MoveToFloorByIndex(currentFloorIndex - 1);
                elevatorControl_T.MoveToFloorByIndex(currentFloorIndex - 1);
            }
            else if (e.KeyCode == Keys.PageDown && currentFloorIndex < elevatorControl_F._floors.Count - 1)
            {
                elevatorControl_F.MoveToFloorByIndex(currentFloorIndex + 1);
                elevatorControl_T.MoveToFloorByIndex(currentFloorIndex + 1);
            }
            else if (e.KeyCode == Keys.ShiftKey)
            {
                if (time_Weather_Info1.Visible)
                {
                    time_Weather_Info1.Visible = false;
                    time_Weather_Info2.Visible = true;
                    time_Weather_Info3.Visible = false;
                    elementHost1.BackColor = Color.FromArgb(117, 159, 197);
                    elementHost2.BackColor = Color.FromArgb(117, 159, 197);
                    elementHost3.BackColor = Color.FromArgb(47, 94, 120);
                    elementHost3.BackgroundImage = null;

                    panel3.BackColor = Color.FromArgb(47, 94, 120);
                    panel3.BackgroundImage = null;

                    label2.BackColor = Color.FromArgb(47, 94, 120);
                    label9.BackColor = Color.FromArgb(47, 94, 120);

                    panel1.BackColor = Color.DarkBlue;
                    pictureBox3.BackColor = Color.DarkBlue;
                    label1.BackColor = Color.DarkBlue;
                    text_Scrolling1.BackColor = Color.DarkBlue;
                }
                else if (time_Weather_Info2.Visible)
                {
                    time_Weather_Info1.Visible = false;
                    time_Weather_Info2.Visible = false;
                    time_Weather_Info3.Visible = true;
                    //elementHost1.BackColor = Color.FromArgb(156, 158, 157);
                    //elementHost2.BackColor = Color.FromArgb(156, 158, 157);

                    elementHost1.BackColor = Color.FromArgb(164, 185, 232);
                    elementHost2.BackColor = Color.FromArgb(164, 185, 232);
                    elementHost3.BackgroundImage = Properties.Resources.LCDSettop_Gray2;

                    panel3.BackColor = Color.FromArgb(156, 158, 157);
                    panel3.BackgroundImage = Properties.Resources.LCDSettop_Gray2;

                    label2.BackColor = Color.Gray;
                    label9.BackColor = Color.Gray;

                    panel1.BackColor = Color.FromArgb(64, 64, 64);
                    pictureBox3.BackColor = Color.FromArgb(64, 64, 64);
                    label1.BackColor = Color.FromArgb(64, 64, 64);
                    text_Scrolling1.BackColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    time_Weather_Info1.Visible = true;
                    time_Weather_Info2.Visible = false;
                    time_Weather_Info3.Visible = false;
                    elementHost1.BackColor = Color.FromArgb(164, 185, 232);
                    elementHost2.BackColor = Color.FromArgb(164, 185, 232);
                    elementHost3.BackColor = Color.FromArgb(164, 185, 232);
                    elementHost3.BackgroundImage = null;

                    panel3.BackColor = Color.FromArgb(164, 185, 232);
                    panel3.BackgroundImage = null;

                    label2.BackColor = Color.FromArgb(164, 185, 232);
                    label9.BackColor = Color.FromArgb(164, 185, 232);

                    panel1.BackColor = Color.MidnightBlue;
                    pictureBox3.BackColor = Color.MidnightBlue;
                    label1.BackColor = Color.MidnightBlue;
                    text_Scrolling1.BackColor = Color.MidnightBlue;
                }
            }
            else if (e.KeyCode == Keys.CapsLock)
            {
                if (elementHost3.Visible)
                {
                    elementHost3.Visible = false;
                    tableLayoutPanel2.Visible = true;
                    tableLayoutPanel3.Visible = false;
                }
                else if (tableLayoutPanel2.Visible)
                {
                    elementHost3.Visible = false;
                    tableLayoutPanel2.Visible = false;
                    tableLayoutPanel3.Visible = true;
                }
                else
                {
                    elementHost3.Visible = true;
                    tableLayoutPanel2.Visible = false;
                    tableLayoutPanel3.Visible = false;
                }
            }
        }

    }
}