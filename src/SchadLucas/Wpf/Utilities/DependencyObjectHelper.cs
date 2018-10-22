using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SchadLucas.Utilities;

namespace SchadLucas.Wpf.Utilities
{
    public static class DependencyObjectHelper
    {
        public static List<T> GetChildren<T>(DependencyObject root)
        {
            var children = TreeTraversal.DepthFirst(root, TraverseChildren);
            return children.OfType<T>().ToList();
        }

        private static IEnumerable<DependencyObject> TraverseChildren(DependencyObject obj)
        {
            var children = LogicalTreeHelper.GetChildren(obj);
            var dependencyObjects = children.OfType<DependencyObject>();

            return dependencyObjects.Reverse().ToList();
        }
    }
}