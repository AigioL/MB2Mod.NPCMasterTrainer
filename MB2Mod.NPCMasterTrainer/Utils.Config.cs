using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        internal static string[] BinaryPath { get; } = new string[] { "bin", "Win64_Shipping_Client" };

        static readonly Lazy<string> lazy_CurrentDirectory = new Lazy<string>(() =>
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

        static readonly Lazy<string> lazy_CurrentModDirectory = new Lazy<string>(() =>
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

            static readonly Lazy<Config> lazy_instance = new Lazy<Config>(() =>
            {
                Config config = null;
                var path = ConfigPath;
                if (File.Exists(path))
                {
                    string jsonConfig;
                    try
                    {
                        jsonConfig = File.ReadAllText(path);
                    }
                    catch (Exception ex_read)
                    {
                        jsonConfig = null;
                        DisplayMessage(ex_read);
                    }
                    if (!string.IsNullOrWhiteSpace(jsonConfig) && TryDeserialize<Config>(jsonConfig, out var obj)) config = obj;
                }
                if (config == null)
                {
                    config = new Config();
                    try
                    {
                        File.WriteAllText(path, config.ToJsonString());
                    }
                    catch (Exception ex_write)
                    {
                        DisplayMessage(ex_write);
                    }
                }
                return config;
            });

            public static Config Instance => lazy_instance.Value;
        }
    }
}
