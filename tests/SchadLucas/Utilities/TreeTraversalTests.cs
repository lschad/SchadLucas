using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;

namespace SchadLucas.Utilities.Tests
{
    [TestClass]
    [TestCategory("Utilities")]
    public class TreeTraversalTests : EzTest
    {
        [TestMethod]
        public void DepthFirstTest()
        {
            var ids = new[]
            {
                Guid.Parse("4d3f5693-f2e2-42b1-a42f-ce8131e48189"),
                Guid.Parse("d0d037dc-8e50-4a08-b054-c5b053808d6a"),
                Guid.Parse("6058319a-f4ad-4cc8-b1f6-95da6262eaa2"),
                Guid.Parse("b867bf62-4cdb-4c38-80ca-b7fd9d725fcf"),
                Guid.Parse("4ade37c5-08e4-4e11-b388-4693ffc3563c"),
                Guid.Parse("d4538fb3-ac8d-4bbf-a181-f5b06538dc22"),
                Guid.Parse("f880cdf1-5d1c-4143-9c73-5df194a950f5"),
                Guid.Parse("bea17e1e-377f-42d7-9ad6-b07e4f0d74db"),
                Guid.Parse("e6651a05-76a8-44c5-8be9-eb518eb385a0"),
                Guid.Parse("ed420f30-5376-4ea2-b519-ba2edc561113"),
                Guid.Parse("37f49c00-405a-421b-86cf-f147b0f5e0c0"),
                Guid.Parse("d443afb9-079a-4968-9b61-c2511d66f265"),
                Guid.Parse("bb739f82-e7a9-431e-9928-46034a4d10ef")
            };

            var root = new TreeTraversalTestObject(ids[0])
            {
                Children = new[]
                {
                    new TreeTraversalTestObject(ids[1])
                    {
                        Children = new[]
                        {
                            new TreeTraversalTestObject(ids[2])
                            {
                                Children = new[]
                                {
                                    new TreeTraversalTestObject(ids[3]),
                                    new TreeTraversalTestObject(ids[4]),
                                    new TreeTraversalTestObject(ids[5]),
                                    new TreeTraversalTestObject(ids[6])
                                }
                            }
                        }
                    },
                    new TreeTraversalTestObject(ids[7])
                    {
                        Children = new[]
                        {
                            new TreeTraversalTestObject(ids[8])
                            {
                                Children = new[]
                                {
                                    new TreeTraversalTestObject(ids[9])
                                    {
                                        Children = new[]
                                        {
                                            new TreeTraversalTestObject(ids[10])
                                            {
                                                Children = new[]
                                                {
                                                    new TreeTraversalTestObject(ids[11])
                                                    {
                                                        Children = new[]
                                                        {
                                                            new TreeTraversalTestObject(ids[12])
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var result = TreeTraversal.DepthFirst(root, x => x.Children).ToList();

            result.ForEach(r => CollectionAssert.Contains(ids, r.Id));
            Assert.AreEqual(ids[0], result[0].Id);
            Assert.AreEqual(ids[7], result[1].Id);
            Assert.AreEqual(ids[8], result[2].Id);
            Assert.AreEqual(ids[9], result[3].Id);
            Assert.AreEqual(ids[10], result[4].Id);
            Assert.AreEqual(ids[11], result[5].Id);
            Assert.AreEqual(ids[12], result[6].Id);
            Assert.AreEqual(ids[1], result[7].Id);
            Assert.AreEqual(ids[2], result[8].Id);
            Assert.AreEqual(ids[6], result[9].Id);
            Assert.AreEqual(ids[5], result[10].Id);
            Assert.AreEqual(ids[4], result[11].Id);
            Assert.AreEqual(ids[3], result[12].Id);
        }
    }
}