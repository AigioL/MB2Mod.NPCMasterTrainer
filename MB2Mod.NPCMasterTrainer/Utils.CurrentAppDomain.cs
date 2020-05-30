using System;
using System.Linq;
using System.Reflection;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public static class CurrentAppDomain
        {
            static readonly Lazy<Assembly[]> lazy_CurrentAppDomainAssemblies = new Lazy<Assembly[]>(AppDomain.CurrentDomain.GetAssemblies);

            public static void Print()
            {
                try
                {
                    Console.WriteLine("---- Print Current App Domain Assemblies File Name ----");
                    var assemblies = lazy_CurrentAppDomainAssemblies.Value;
                    foreach (var assembly in assemblies)
                    {
                        Console.WriteLine(assembly.GetName()?.Name);
                        //Console.WriteLine(new
                        //{
                        //    Name = assembly.GetName()?.Name,
                        //    IsDynamic = assembly.TryGetValue(a => a.IsDynamic.ToString()),
                        //    Location = assembly.TryGetValue(a => a.Location),
                        //    CodeBase = assembly.TryGetValue(a => a.CodeBase),
                        //}.ToJsonString());
                    }
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            public static bool Exists(string dllFileNameWithoutExtension)
            {
                var assemblies = lazy_CurrentAppDomainAssemblies.Value;
                return assemblies.Any(x => string.Equals(dllFileNameWithoutExtension, x.GetName()?.Name, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
