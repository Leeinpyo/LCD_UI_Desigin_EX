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
    public partial class Time_Weather_Info : UserControl
    {
        public Time_Weather_Info()
        {
            InitializeComponent();
            // 컨트롤 깜박임 최소화
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
        }
    }
}
