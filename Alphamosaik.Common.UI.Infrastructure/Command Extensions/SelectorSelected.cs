using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Alphamosaik.Common.UI.Infrastructure
{
    public static class SelectorSelected
    {
        private static readonly DependencyProperty SelectedCommandBehaviorProperty = DependencyProperty.RegisterAttached(
            "SelectedCommandBehavior",
            typeof(SelectorSelectedCommandBehavior),
            typeof(SelectorSelected),
            null);

        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(SelectorSelected),
            new PropertyMetadata(OnSetCommandCallback));

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Only works for selector")]
        public static void SetCommand(Selector selector, ICommand command)
        {
            selector.SetValue(CommandProperty, command);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Only works for selector")]
        public static ICommand GetCommand(Selector selector)
        {
            return selector.GetValue(CommandProperty) as ICommand;
        }

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var selector = dependencyObject as Selector;
            if (selector != null)
                GetOrCreateBehavior(selector).Command = e.NewValue as ICommand;
        }

        private static SelectorSelectedCommandBehavior GetOrCreateBehavior(Selector selector)
        {
            var behavior = selector.GetValue(SelectedCommandBehaviorProperty) as SelectorSelectedCommandBehavior;
            if (behavior == null)
            {
                behavior = new SelectorSelectedCommandBehavior(selector);
                selector.SetValue(SelectedCommandBehaviorProperty, behavior);
            }

            return behavior;
        }
    }
}