using System.Windows.Controls;

namespace SchadLucas.Wpf.EzMvvm.Tests.Sections
{
    internal interface IFakeView : IView { }

    internal class FakeView : UserControl, IFakeView
    {
        public FakeView(int children = 1)
        {
            var grid = new Grid();

            for (var i = 0; i < children; i++)
            {
                grid.Children.Add(new TextBlock {Text = i.ToString()});
            }

            Content = grid;
        }
    }
}