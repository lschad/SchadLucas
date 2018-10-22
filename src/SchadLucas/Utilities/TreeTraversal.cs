using System;
using System.Collections.Generic;

namespace SchadLucas.Utilities
{
    public static class TreeTraversal
    {
        public static IEnumerable<T> DepthFirst<T>(T root, Func<T, IEnumerable<T>> children)
        {
            var stack = new Stack<T>(new[] {root});

            while (stack.Count != 0)
            {
                var current = stack.Pop();

                foreach (var child in children(current))
                {
                    stack.Push(child);
                }

                yield return current;
            }
        }
    }
}