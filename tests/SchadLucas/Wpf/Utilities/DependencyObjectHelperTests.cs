using System;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;

namespace SchadLucas.Wpf.Utilities.Tests
{
    [TestClass]
    public class DependencyObjectHelperTests : EzTest
    {
        [TestMethod]
        public void GetChildren_FindsAllChildren_WhenFlatOnly()
        {
            const int times = 99;

            var root = new UserControl
            {
                Content = new Grid()
            };

            for (var i = 0; i < times; i++)
            {
                ((Grid) root.Content).Children.Add(new TextBlock {Text = i.ToString()});
            }

            var result = DependencyObjectHelper.GetChildren<TextBlock>(root);
            EzAssert.That(result).CountIs(times);

            for (var i = 0; i < times; i++)
            {
                EzAssert.That(result.FindAll(t => t.Text == i.ToString())).CountIs(1);
            }
        }

        [TestMethod]
        public void GetChildren_FindsAllChildren_WhenNested()
        {
            const int times = 33;

            var root = new UserControl
            {
                Content = new Grid()
            };

            for (var i = 0; i < times; i++)
            {
                var grid = new Grid();
                for (var j = 0; j < times; j++)
                {
                    var grid2 = new Grid();
                    for (var n = 0; n < times; n++)
                    {
                        grid2.Children.Add(new TextBlock {Text = j.ToString()});
                    }
                    grid.Children.Add(grid2);
                }

                ((Grid) root.Content).Children.Add(grid);
            }

            var result = DependencyObjectHelper.GetChildren<TextBlock>(root);
            EzAssert.That(result).CountIs((long) Math.Pow(times, 3));

            for (var i = 0; i < times; i++)
            {
                EzAssert.That(result.FindAll(t => t.Text == i.ToString())).CountIs((long) Math.Pow(times, 2));
            }

        }
    }
}