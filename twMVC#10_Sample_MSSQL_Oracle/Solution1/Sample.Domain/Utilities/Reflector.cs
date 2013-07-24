using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Utilities
{
    public class Reflector
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="pathOrAssemblyName">Name of the path or assembly.</param>
        /// <param name="classFullName">Full name of the class.</param>
        /// <returns></returns>
        public static Type GetType(string pathOrAssemblyName, string classFullName)
        {
            try
            {
                if (!pathOrAssemblyName.Contains(Path.DirectorySeparatorChar.ToString()))
                {
                    string assemblyName = AbstractAssemblyName(pathOrAssemblyName);
                    if (!classFullName.Contains(assemblyName))
                    {
                        classFullName = String.Concat(assemblyName, ".", classFullName);
                    }
                    Assembly assembly = Assembly.Load(assemblyName);
                    return assembly.GetType(classFullName);
                }

                Assembly asm = Assembly.LoadFrom(pathOrAssemblyName);
                if (null == asm) return null;

                Type type = asm.GetType(classFullName);

                if (null == type)
                {
                    foreach (Type one in asm.GetTypes())
                    {
                        if (one.Name == classFullName)
                        {
                            type = one;
                            break;
                        }
                    }
                }
                return type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Abstracts the name of the assembly.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns></returns>
        private static string AbstractAssemblyName(string assemblyName)
        {
            string prefix = ".\\";
            string suffix = ".dll";

            if (assemblyName.StartsWith(prefix))
            {
                assemblyName = assemblyName.Substring(prefix.Length);
            }
            if (assemblyName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
            {
                assemblyName = assemblyName.Substring(0, assemblyName.Length - suffix.Length);
            }
            return assemblyName;
        }
    }
}
