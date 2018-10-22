using System;
using System.Collections.Generic;

namespace SchadLucas.Utilities.Tests
{
    internal class TreeTraversalTestObject
    {
        public TreeTraversalTestObject(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
        public IEnumerable<TreeTraversalTestObject> Children { get; set; } = new TreeTraversalTestObject[0];

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}