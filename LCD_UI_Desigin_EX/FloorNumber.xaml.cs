using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LCD_UI_Desigin_EX
{
    public partial class FloorNumber : UserControl
    {
        private double _floorHeight; // 각 층의 높이
        public List<TextBlock> _floors;

        public FloorNumber()
        {
            InitializeComponent();
            _floors = new List<TextBlock>();
        }

        private void UpdateFloorHeight()
        {
            if (_floors.Any())
            {
                _floorHeight = _floors.Average(f => f.ActualHeight + f.Margin.Top + f.Margin.Bottom);
            }
        }

        public void AddFloor(string floorName)
        {
            var floorTextBlock = new TextBlock
            {
                Text = floorName,
                FontSize = 48,
                Margin = new Thickness(0, 42, 0, 42),
                //Margin = new Thickness(0, 29, 0, 27),
                TextAlignment = TextAlignment.Center
            };
            _floors.Add(floorTextBlock);
            FloorStackPanel.Children.Add(floorTextBlock);

            FloorStackPanel.UpdateLayout();
            UpdateFloorHeight();
        }


        private int _prevFloorIndex = 0; // 이전 층 번호를 추적하기 위한 변수

        public void MoveToFloorByIndex(int floorIndex)
        {
            if (floorIndex >= 0 && floorIndex < _floors.Count)
            {
                // 모든 플로어를 일반체로 설정
                foreach (var floor in _floors)
                {
                    floor.FontWeight = FontWeights.Normal;
                    floor.Foreground = new SolidColorBrush(Colors.SteelBlue);
                    floor.FontSize = 48;
                    floor.FontFamily = new FontFamily("나눔 고딕");
                }

                // 새로 선택된 플로어를 볼드체로 설정
                _floors[floorIndex].FontWeight = FontWeights.Bold;
                _floors[floorIndex].Foreground = new SolidColorBrush(Colors.MidnightBlue);
                _floors[floorIndex].FontSize = 51;

                UpdateFloorHeight();

                double halfHeightOfScrollViewer = FloorScrollViewer.ActualHeight / 2;
                double newYOffset = halfHeightOfScrollViewer - ((floorIndex + 0.5) * _floorHeight);

                if (floorIndex > _prevFloorIndex)
                {
                    // 아래로 이동할 때: 아래쪽 화살표가 깜박이는 애니메이션 호출
                    FlashArrow(DownArrow);
                }
                else if (floorIndex < _prevFloorIndex)
                {
                    // 위로 이동할 때: 위쪽 화살표가 깜박이는 애니메이션 호출
                    FlashArrow(UpArrow);
                }

                // 이전 층 번호 업데이트
                _prevFloorIndex = floorIndex;

                var animation = new DoubleAnimation
                {
                    To = newYOffset,
                    Duration = TimeSpan.FromSeconds(0.8),
                    EasingFunction = new QuadraticEase()
                };

                // 애니메이션 완료 이벤트 핸들러 추가
                animation.Completed += (s, e) =>
                {
                    // 층 이동 후 UpArrow 및 DownArrow의 색상 업데이트
                    UpdateArrowColors(floorIndex);

                    // 깜빡이는 동작을 중단한다.
                    _flashing = false; // 깜빡이는 플래그를 중단 상태로 설정
                };

                FloorTranslateTransform.BeginAnimation(TranslateTransform.YProperty, animation);
            }
        }

        private bool _flashing = false;

        public async Task FlashArrow(TextBlock arrow)
        {
            if (!_flashing)
            {
                arrow.Foreground = new SolidColorBrush(Colors.MidnightBlue);

                _flashing = true;
                int flashCount = 10; // 깜박이는 횟수
                double originalOpacity = arrow.Opacity;

                for (int i = 0; i < flashCount; i++)
                {
                    if (_flashing)
                    {
                        arrow.Opacity = 0; // 화살표를 숨김
                        await Task.Delay(100); // 깜박이는 시간 (130ms)
                        arrow.Opacity = originalOpacity; // 원래 불투명도로 복원
                        await Task.Delay(150); // 깜박이는 시간 (150ms)
                    }
                }

                arrow.Opacity = originalOpacity; // 원래 불투명도로 복원
                _flashing = false;
            }
        }

        private void UpdateArrowColors(int currentFloorIndex)
        {
            // 첫 번째 층에 도달한 경우 UpArrow 색상 변경
            if (currentFloorIndex == 0)
            {
                UpArrow.Text = "▲";
                UpArrow.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
            else
            {
                UpArrow.Text = "▲";
                UpArrow.Foreground = new SolidColorBrush(Colors.SteelBlue);
            }

            // 마지막 층에 도달한 경우 DownArrow 색상 변경
            if (currentFloorIndex == _floors.Count - 1)
            {
                DownArrow.Text = "▼";
                DownArrow.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
            else
            {
                DownArrow.Text = "▼";
                DownArrow.Foreground = new SolidColorBrush(Colors.SteelBlue);
            }
        }

        private void FloorScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // 이벤트를 처리했음을 설정하여 마우스 스크롤이 ScrollViewer에 영향을 주지 않도록 합니다.
            e.Handled = true;
        }

    }
}
