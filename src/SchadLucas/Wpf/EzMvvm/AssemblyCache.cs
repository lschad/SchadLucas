using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using SchadLucas.Utilities;

namespace SchadLucas.Wpf.EzMvvm
{
    [ExcludeFromCodeCoverage]
    public static class AssemblyCache
    {
        private static Assembly[] _assemblies;

        public static Assembly[] Assemblies
        {
            get
            {
                if (_assemblies == null)
                {
                    var assemblies = new HashSet<Assembly>
                    {
                        Assembly.GetEntryAssembly(),
                        Assembly.GetCallingAssembly(),
                        Assembly.GetExecutingAssembly(),
                        Assembly.GetAssembly(typeof(AssemblyCache))
                    };

                    _assemblies = GetAllAssemblies(assemblies).ToArray();
                }

                return _assemblies;
            }
        }

        private static IEnumerable<Assembly> GetAllAssemblies(ISet<Assembly> assemblies)
        {
            var copy = new List<Assembly>(assemblies);

            foreach (var assembly in assemblies)
            {
                foreach (var foundAssembly in TreeTraversal.DepthFirst(assembly, a =>
                {
                    var list = new HashSet<Assembly>();
                    foreach (var referencedAssembly in a.GetReferencedAssemblies())
                    {
                        var loadedAssembly = Assembly.Load(referencedAssembly);
                        if (!loadedAssembly.GlobalAssemblyCache)
                        {
                            list.Add(loadedAssembly);
                        }
                    }

                    return list;
                }))
                {
                    if (!copy.Contains(foundAssembly))
                    {
                        copy.Add(foundAssembly);
                    }
                }
            }

            return copy;
        }
    }
}