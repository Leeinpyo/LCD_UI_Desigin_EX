using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LCD_UI_Desigin_EX.WaterUseage;

public class WaterUsageViewModel : INotifyPropertyChanged
{
    private WaterUsage _waterUsage;

    public WaterUsageViewModel()
    {
        // 여기서 데이터를 로드하거나 초기화합니다.
        _waterUsage = new WaterUsage { DailyUsage = 200000, MonthlyUsage = 6000000, YearlyUsage = 72000000 };
    }

    public double DailyUsage
    {
        get => _waterUsage.DailyUsage;
        set
        {
            _waterUsage.DailyUsage = value;
            OnPropertyChanged(nameof(DailyUsage));
        }
    }

    public double MonthlyUsage
    {
        get => _waterUsage.MonthlyUsage;
        set
        {
            _waterUsage.MonthlyUsage = value;
            OnPropertyChanged(nameof(MonthlyUsage));
        }
    }

    public double YearlyUsage
    {
        get => _waterUsage.YearlyUsage;
        set
        {
            _waterUsage.YearlyUsage = value;
            OnPropertyChanged(nameof(YearlyUsage));
        }
    }

    public string DailyUsageDisplay
    {
        get => FormatValue(DailyUsage);
    }

    public string MonthlyUsageDisplay
    {
        get => FormatValue(MonthlyUsage);
    }

    public string YearlyUsageDisplay
    {
        get => FormatValue(YearlyUsage);
    }

    public double DailyMax => _waterUsage.DailyMax;
    public double MonthlyMax => _waterUsage.MonthlyMax;
    public double YearlyMax => _waterUsage.YearlyMax;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public static string FormatValue(double value)
    {
        if (value >= 1_000_000_000) // 십억 이상
            return (value / 1_000_000_000).ToString("0.##B");
        if (value >= 1_000_000) // 백만 이상
            return (value / 1_000_000).ToString("0.##M");
        if (value >= 1_000) // 천 이상
            return (value / 1_000).ToString("0.##K");
        return value.ToString();
    }

}

