using System.Windows.Controls;
using System.Windows;

public class ScrollViewerOffsetMediator : FrameworkElement
{
    public ScrollViewer ScrollViewer
    {
        get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
        set { SetValue(ScrollViewerProperty, value); }
    }

    public static readonly DependencyProperty ScrollViewerProperty =
        DependencyProperty.Register("ScrollViewer", typeof(ScrollViewer), typeof(ScrollViewerOffsetMediator), new PropertyMetadata(OnScrollViewerChanged));

    private static void OnScrollViewerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var mediator = (ScrollViewerOffsetMediator)d;
        mediator.UpdateScrollViewerOffset();
    }

    private void UpdateScrollViewerOffset()
    {
        if (ScrollViewer != null)
        {
            ScrollViewer.ScrollToVerticalOffset(VerticalOffset);
        }
    }

    public double VerticalOffset
    {
        get { return (double)GetValue(VerticalOffsetProperty); }
        set { SetValue(VerticalOffsetProperty, value); }
    }

    public static readonly DependencyProperty VerticalOffsetProperty =
        DependencyProperty.Register("VerticalOffset", typeof(double), typeof(ScrollViewerOffsetMediator), new PropertyMetadata(OnVerticalOffsetChanged));

    private static void OnVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var mediator = (ScrollViewerOffsetMediator)d;
        mediator.UpdateScrollViewerOffset();
    }
}
