using System.Windows;
using System.Windows.Controls;

namespace Presentation.Behaviors
{
    public static class CanvasSizeBehavior
    {
        public static readonly DependencyProperty ObserveSizeProperty =
            DependencyProperty.RegisterAttached(
                "ObserveSize",
                typeof(bool),
                typeof(CanvasSizeBehavior),
                new PropertyMetadata(false, OnObserveSizeChanged));

        public static void SetObserveSize(DependencyObject element, bool value)
            => element.SetValue(ObserveSizeProperty, value);

        public static bool GetObserveSize(DependencyObject element)
            => (bool)element.GetValue(ObserveSizeProperty);

        private static void OnObserveSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element && (bool)e.NewValue)
            {
                void UpdateSize()
                {
                    if (element.DataContext is IHaveCanvasSize vm)
                        vm.SetCanvasSize(element.ActualWidth, element.ActualHeight);
                }

                element.Loaded += (_, __) => UpdateSize();
                element.SizeChanged += (_, __) => UpdateSize();
            }
        }
    }
}
