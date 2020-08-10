using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        internal static string[] BinaryPath { get; } = new string[] { "bin", "Win64_Shipping_Client" };

        private static readonly Lazy<string> lazy_CurrentDirectory = new Lazy<string>(() =>
           {
               var assembly = typeof(Utils).Assembly;
               if (!assembly.IsDynamic)
               {
                   try
                   {
                       return Path.GetDirectoryName(assembly.Location);
                   }
                   catch
                   {
                   }
               }
               return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
           });

        public static string CurrentDirectory => lazy_CurrentDirectory.Value;

        private static readonly Lazy<string> lazy_CurrentModDirectory = new Lazy<string>(() =>
           {
               var current = CurrentDirectory;
               var directory = new DirectoryInfo(current);
               foreach (var item in BinaryPath.Reverse())
               {
                   if (!string.Equals(directory.Name, item, StringComparison.OrdinalIgnoreCase))
                   {
                       return current;
                   }
                   directory = directory.Parent;
               }
               return directory.FullName;
           });

        public static string CurrentModDirectory => lazy_CurrentModDirectory.Value;

        public static string ExportDirectory => Path.Combine(CurrentModDirectory, "Export");

        public static string ConfigPath => Path.Combine(CurrentModDirectory, "Config.json");

        public sealed partial class Config
        {
            public string ModConfigVersion { get; set; }

            /// <summary>
            /// 启用开发者控制台
            /// </summary>
            public bool EnableDeveloperConsole { get; set; } = true;

            /// <summary>
            /// 是否显示Win32控制台
            /// </summary>
            public bool ShowWin32Console { get; set; }

            /// <summary>
            /// 清空物品的熟练度要求
            /// </summary>
            public bool ClearItemDifficulty { get; set; } = true;

            /// <summary>
            /// 解锁长弓在马背上使用
            /// </summary>
            public bool UnlockLongBowForUseOnHorseBack { get; set; } = true;

            /// <summary>
            /// 解锁平民装扮
            /// </summary>
            public bool UnlockItemCivilian { get; set; } = true;

            /// <summary>
            /// 增加弹药量-箭
            /// </summary>
            public ushort AddAmmoByArrow { get; set; } = (ushort)(IsDevelopment ? 63 : 11);

            /// <summary>
            /// 增加弹药量-弩箭
            /// </summary>
            public ushort AddAmmoByBolt { get; set; } = 6;

            /// <summary>
            /// 增加弹药量-飞斧
            /// </summary>
            public ushort AddAmmoByThrowingAxe { get; set; } = 2;

            /// <summary>
            /// 增加弹药量-飞刀
            /// </summary>
            public ushort AddAmmoByThrowingKnife { get; set; } = 13;

            /// <summary>
            /// 增加弹药量-标枪
            /// </summary>
            public ushort AddAmmoByJavelin { get; set; } = 1;

            /// <summary>
            /// 增加弹药量-标枪(次要武器即需要按X切换成标枪的长杆武器)
            /// </summary>
            public ushort AddAmmoByJavelinSecondary { get; set; }

            /// <summary>
            /// 修复目前游戏中从剪贴板粘贴的中文文字出现乱码
            /// </summary>
            public bool FixGetClipboardText { get; set; } = true;

            private static bool IsOldConfigFile(string ver)
            {
                try
                {
                    var version = new Version(ver);
                    var current_version = new Version(FileVersion);
                    return current_version > version;
                }
                catch
                {
                    return ver != FileVersion;
                }
            }

            private static readonly StringBuilder lazy_instance_sb = IsDevelopment ? new StringBuilder() : null;

            private static readonly Lazy<Config> lazy_instance = new Lazy<Config>(() =>
               {
                   Config config = null;
                   var path = ConfigPath;
                   var exists = File.Exists(path);
                   if (exists)
                   {
                       string jsonConfig;
                       try
                       {
                           jsonConfig = File.ReadAllText(path);
                       }
                       catch (Exception ex_read)
                       {
                           jsonConfig = null;
                           if (IsDevelopment) lazy_instance_sb.AppendLine(ex_read.ToString());
                       }
                       if (!string.IsNullOrWhiteSpace(jsonConfig) && TryDeserialize<Config>(jsonConfig, out var obj))
                       {
                           config = obj;
                       }
                       else
                       {
                           if (IsDevelopment)
                           {
                               Console.WriteLine("Read Config File Fail.");
                           }
                       }
                   }
                   bool isWrite;
                   if (config == null)
                   {
                       if (IsDevelopment) lazy_instance_sb.AppendLine("new Config And Write.");
                       config = GetConfig();
                       isWrite = true;
                   }
                   else if (IsOldConfigFile(config.ModConfigVersion))
                   {
                       if (IsDevelopment) lazy_instance_sb.AppendLine($"IsOldConfigFile And Write, MCV: {config.ModConfigVersion}, CMCV: {GetModConfigVersion()}.");
                       config.ModConfigVersion = GetModConfigVersion();
                       isWrite = true;
                   }
                   else
                   {
                       if (IsDevelopment) lazy_instance_sb.AppendLine("No Write.");
                       isWrite = false;
                   }
                   if (isWrite)
                   {
                       try
                       {
                           if (exists)
                           {
                               var bakPath = path + ".bak";
                               if (File.Exists(bakPath)) File.Delete(bakPath);
                               File.Move(path, bakPath);
                               File.Delete(path);
                           }
                           File.WriteAllText(path, config.ToJsonString());
                       }
                       catch (Exception ex_write)
                       {
                           if (IsDevelopment) lazy_instance_sb.AppendLine(ex_write.ToString());
                       }
                   }
                   return config;
               });

            public static Config Instance => lazy_instance.Value;

            public static void PrintConfigInstanceLog()
            {
                var str = lazy_instance_sb?.ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    Console.WriteLine(str);
                }
            }

            private static string GetModConfigVersion() => ToFullString(new Version(FileVersion));

            public static Config GetConfig() => new Config { ModConfigVersion = GetModConfigVersion() };
        }

        public static string ToFullString(this Version version)
        {
            return $"{GetInt(version.Major)}.{GetInt(version.Minor)}.{GetInt(version.Build)}.{GetInt(version.Revision)}";
            static int GetInt(int i) => i < 0 ? 0 : i;
        }
    }
}