using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LCD_UI_Desigin_EX
{
    public partial class FloorText : UserControl
    {
        private double _floorHeight = 100.0;
        public List<StackPanel> _floors;

        public FloorText()
        {
            InitializeComponent();
            _floors = new List<StackPanel>();
        }

        public void AddFloor(string floor, string elevatorInfo)
        {
            var floorInfo = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            var floorTextBlock = new TextBlock
            {
                Text = floor,
                FontSize = 32,
                Margin = new Thickness(0, 35, 0, 5),
                //Margin = new Thickness(0, 22, 0, 3),
                FontFamily = new FontFamily("나눔 고딕")
            };

            var elevatorInfoTextBlock = new TextBlock
            {
                Text = elevatorInfo,
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 5, 0, 35),
                //Margin = new Thickness(0, 3, 0, 22),
                FontFamily = new FontFamily("나눔 고딕")
            };

            floorInfo.Children.Add(floorTextBlock);
            floorInfo.Children.Add(elevatorInfoTextBlock);

            _floors.Add(floorInfo);
            FloorStackPanel.Children.Add(floorInfo);

            FloorStackPanel.UpdateLayout();
            _floorHeight = floorInfo.ActualHeight;
        }

        public void MoveToFloorByIndex(int floorIndex)
        {
            if (floorIndex >= 0 && floorIndex < _floors.Count)
            {
                foreach (var floor in _floors)
                {
                    var textBlock = floor.Children[0] as TextBlock;
                    var elevatorInfoTextBlock = floor.Children[1] as TextBlock;
                    if (textBlock != null)
                    {
                        textBlock.FontWeight = FontWeights.Normal;
                        textBlock.Foreground = new SolidColorBrush(Colors.SteelBlue);
                        textBlock.FontSize = 32;

                        elevatorInfoTextBlock.FontWeight = FontWeights.Normal;
                    }
                }

                var selectedFloorTextBlock = _floors[floorIndex].Children[0] as TextBlock;
                var selectedElevatorInfoTextBlock = _floors[floorIndex].Children[1] as TextBlock;
                if (selectedFloorTextBlock != null)
                {
                    selectedFloorTextBlock.FontWeight = FontWeights.Bold;
                    selectedFloorTextBlock.Foreground = new SolidColorBrush(Colors.MidnightBlue);
                    selectedFloorTextBlock.FontSize = 34;

                    selectedElevatorInfoTextBlock.FontWeight = FontWeights.Bold;
                }

                // 수정된 부분
                double halfHeightOfContainer = this.ActualHeight / 2;
                double newYOffset = halfHeightOfContainer - ((floorIndex + 0.5) * _floorHeight);

                var animation = new DoubleAnimation
                {
                    To = newYOffset,
                    Duration = TimeSpan.FromSeconds(0.8),
                    EasingFunction = new QuadraticEase()
                };

                TranslateTransform transform;
                if (FloorStackPanel.RenderTransform is TranslateTransform)
                {
                    transform = FloorStackPanel.RenderTransform as TranslateTransform;
                }
                else
                {
                    transform = new TranslateTransform();
                    FloorStackPanel.RenderTransform = transform;
                }

                transform.BeginAnimation(TranslateTransform.YProperty, animation);
            }
        }
        private void FloorScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // 이벤트를 처리했음을 설정하여 마우스 스크롤이 ScrollViewer에 영향을 주지 않도록 합니다.
            e.Handled = true;
        }
    }
}
