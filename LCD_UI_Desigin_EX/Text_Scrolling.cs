using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LCD_UI_Desigin_EX
{
    public partial class Text_Scrolling : Label
    {
        public Text_Scrolling()
        {
            InitializeComponent();
            UseCompatibleTextRendering = true;
            this.BackColor = Color.White;
            timer1.Start();
        }

        float position, speed;

        public float Set_Speed { get { return speed; } set { speed = value; Invalidate(); } }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(position, 7);
            base.OnPaint(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (position < -Width)
            {
                position = Width;
            }
            position -= speed;
            Invalidate();
        }
    }
}