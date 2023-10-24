using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LCD_UI_Desigin_EX
{
    /// <summary>
    /// WaterUseage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WaterUseage : UserControl
    {
        public class WaterUsage
        {
            public double DailyUsage { get; set; }
            public double MonthlyUsage { get; set; }
            public double YearlyUsage { get; set; }
            public double DailyMax { get; set; } = 500000; // 예시값
            public double MonthlyMax { get; set; } = 10000000; // 예시값
            public double YearlyMax { get; set; } = 150000000; // 예시값
        }

        public WaterUseage()
        {
            InitializeComponent();
            var viewModel = new WaterUsageViewModel();
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            this.DataContext = viewModel;
            UpdateGauge(DayArc, ((WaterUsageViewModel)this.DataContext).DailyUsage);
            UpdateGauge(MonthArc, ((WaterUsageViewModel)this.DataContext).MonthlyUsage);
            UpdateGauge(YearArc, ((WaterUsageViewModel)this.DataContext).YearlyUsage);
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(WaterUsageViewModel.DailyUsage):
                    UpdateGauge(DayArc, ((WaterUsageViewModel)this.DataContext).DailyUsage);
                    break;
                case nameof(WaterUsageViewModel.MonthlyUsage):
                    UpdateGauge(MonthArc, ((WaterUsageViewModel)this.DataContext).MonthlyUsage);
                    break;
                case nameof(WaterUsageViewModel.YearlyUsage):
                    UpdateGauge(YearArc, ((WaterUsageViewModel)this.DataContext).YearlyUsage);
                    break;
            }
        }

        private void UpdateGauge(Path path, double value)
        {
            var percentage = value / GetMaxValue(path); // GetMaxValue는 해당 게이지의 최대값을 반환하는 메서드입니다.
            SetArcPath(path, percentage);
        }

        private void SetArcPath(Path path, double percentage)
        {
            const double radius = 18; // 반경
            double angle = percentage * 360.0; // 0 to 100% maps to 0 to 360 degrees
            double x = 20 + radius * Math.Sin(angle * Math.PI / 180.0);
            double y = 20 - radius * Math.Cos(angle * Math.PI / 180.0);

            bool largeArc = angle > 180.0;

            path.Data = Geometry.Parse($"M 20,20 L 20,2 A {radius},{radius} 0 {(largeArc ? 1 : 0)} 1 {x},{y} Z");
        }

        private double GetMaxValue(Path path)
        {
            var viewModel = this.DataContext as WaterUsageViewModel;
            if (viewModel == null) return 1.0; // 기본값

            if (path == DayArc) return viewModel.DailyMax;
            if (path == MonthArc) return viewModel.MonthlyMax;
            if (path == YearArc) return viewModel.YearlyMax;

            return 1.0; // 기본값
        }

    }
}
