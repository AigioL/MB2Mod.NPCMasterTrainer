using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading.Tasks;
using utils = MB2Mod.NPCMasterTrainer.Utils;

namespace MB2Mod.NPCMasterTrainer.Launcher
{
    internal static class Utils
    {
        public static bool IsDevelopment => utils.IsDevelopment;

        private const string DefGamePath = @"C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord";

        private const string SteamInstallRegistryPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 261550";

        public static string GetGamePath()
        {
            string gamePath = default;
            try
            {
                var registryKey = Registry.LocalMachine;
                foreach (var item in SteamInstallRegistryPath.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    registryKey = registryKey.OpenSubKey(item, RegistryRights.ReadKey);
                }
                gamePath = registryKey.GetValue("InstallLocation")?.ToString();
            }
            catch
            {
            }
            return string.IsNullOrWhiteSpace(gamePath) ? DefGamePath : gamePath;
        }

        public static readonly string[] GameProcessNames = new[] { "TaleWorlds.MountAndBlade.Launcher", "Bannerlord", "Bannerlord.Native", "Bannerlord_BE" };

        public static Process[] GetGameProcesses()
        {
            var processes = GameProcessNames.Select(x => Process.GetProcessesByName(x)).SelectMany(x => x).ToArray();
            return processes;
        }

        public static async Task ExitGameAsync()
        {
            var gameProcesses = GetGameProcesses();
            if (gameProcesses != null && gameProcesses.Any())
            {
                Console.WriteLine($"Kill Game Processes({gameProcesses.Length})");
                Array.ForEach(gameProcesses, p => p.Kill(true));
                await Task.Delay(500);
                int i = 0;
                do
                {
                    gameProcesses = GetGameProcesses();
                    await Task.Delay(750);
                    i++;
                } while (gameProcesses != null && gameProcesses.Any() && i < 1000);
            }
        }

        public static string GetProjectPath(string currentPath)
        {
            var currentDirInfo = new DirectoryInfo(currentPath);
            string modProjPath = null;
            while (currentDirInfo.Parent != null)
            {
                currentDirInfo = currentDirInfo.Parent;
                if (currentDirInfo.GetFiles("*.csproj").Any())
                {
                    modProjPath = Path.Combine(currentDirInfo.Parent.FullName, "MB2Mod.NPCMasterTrainer");
                    break;
                }
            }
            return modProjPath;
        }

        public static string SetVersion(string xml, Version version)
        {
            if (xml != null)
            {
                xml = xml.Replace("<Version value=\"v1.0.0.0\"/>", $"<Version value=\"v{version.ToFullString()}\"/>");
            }
            return xml;
        }

        private const string SubModule_XML = "SubModule.xml";

        private const string mod_dll_file_prefix = "MB2Mod.";

        private const string mod_dll_file_search_pattern = mod_dll_file_prefix + "*.dll";

        public static string SearchModDllFile(string dirPath)
        {
            return Directory.GetFiles(dirPath, mod_dll_file_search_pattern).OrderByDescending(x => File.GetCreationTime(x)).FirstOrDefault();
        }

        public static bool Deploy(string currentPath, string projPath, string gamePath, bool build_package = false)
        {
            if (string.IsNullOrWhiteSpace(projPath))
            {
                Console.WriteLine("Deploy Fail, projPath IsNullOrWhiteSpace.");
                return false;
            }
            var readmeFiles = Directory.GetParent(projPath).GetFiles("README*.md")
                .ToDictionary(k => k, v => File.ReadAllText(v.FullName));
            var xmlString = File.ReadAllText(Path.Combine(projPath, SubModule_XML));
            var outPath = Path.Combine(projPath, "bin", IsDevelopment ? "Debug" : "Release");
            var dllFilePathSource = SearchModDllFile(outPath);
            if (dllFilePathSource == default)
            {
                var dirs = Directory.GetDirectories(outPath).OrderByDescending(x => Directory.GetCreationTime(x)).ToArray();
                foreach (var dir in dirs)
                {
                    dllFilePathSource = SearchModDllFile(dir);
                    if (dllFilePathSource != default) break;
                }
            }
            if (dllFilePathSource == default)
            {
                Console.WriteLine("Deploy Fail, Mod Dll File Not Found.");
                return false;
            }
            Version version;
            try
            {
                var assembly = Assembly.LoadFile(dllFilePathSource);
                var file_version = new Version(assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version);
                version = assembly.GetName().Version;
                xmlString = SetVersion(xmlString, file_version);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deploy Fail, Read Assembly Catch.");
                Console.WriteLine(e);
                return false;
            }
            var dllFileNameWithoutExtension = Path.GetFileNameWithoutExtension(dllFilePathSource);
            var modDirName = dllFileNameWithoutExtension;
            if (modDirName.StartsWith(mod_dll_file_prefix, StringComparison.OrdinalIgnoreCase))
                modDirName = modDirName[mod_dll_file_prefix.Length..];
            var modDirPath = Path.Combine(gamePath, "Modules", modDirName);
            var modDirPathExists = Directory.Exists(modDirPath);

            var xmlPath = Path.Combine(modDirPath, SubModule_XML);
            var xmlPathWrite = true;

            var readmePaths = readmeFiles.Keys
                .ToDictionary(k => k, v => Path.Combine(modDirPath, v.Name));
            var readmePathsWrite = readmeFiles.Keys.ToDictionary(k => k, v => true);

            if (!modDirPathExists)
            {
                Directory.CreateDirectory(modDirPath);
            }
            else
            {
                if (File.Exists(xmlPath))
                {
                    if (File.ReadAllText(xmlPath) != xmlString)
                    {
                        File.Delete(xmlPath);
                    }
                    else
                    {
                        xmlPathWrite = false;
                    }
                }

                foreach (var item in readmePaths)
                {
                    if (File.Exists(item.Value))
                    {
                        if (File.ReadAllText(item.Value) != readmeFiles[item.Key])
                        {
                            File.Delete(item.Value);
                        }
                        else
                        {
                            readmePathsWrite[item.Key] = false;
                        }
                    }
                }
            }
            if (xmlPathWrite) File.WriteAllText(xmlPath, xmlString);
            foreach (var item in readmePathsWrite)
            {
                var readmeWrite = item.Value;
                if (readmeWrite) File.WriteAllText(readmePaths[item.Key], readmeFiles[item.Key]);
            }
            var array_modDllDirPaths = new string[utils.BinaryPath.Length + 1];
            utils.BinaryPath.CopyTo(array_modDllDirPaths, 1);
            array_modDllDirPaths[0] = modDirPath;
            var modDllDirPath = Path.Combine(array_modDllDirPaths);
            var modDllDirPathExists = Directory.Exists(modDllDirPath);
            var dllFileWrite = true;
            var dllFilePathDest = Path.Combine(modDllDirPath, Path.GetFileName(dllFilePathSource));
            if (!modDllDirPathExists)
            {
                Directory.CreateDirectory(modDllDirPath);
            }
            else
            {
                if (File.Exists(dllFilePathDest) && !FileEquals(dllFilePathDest, dllFilePathSource))
                {
                    File.Delete(dllFilePathDest);
                }
                else
                {
                    dllFileWrite = false;
                }
            }
            if (dllFileWrite) File.Copy(dllFilePathSource, dllFilePathDest);
            if (build_package)
            {
                string zipFileNamePrefix =
                    dllFileNameWithoutExtension.StartsWith(mod_dll_file_prefix, StringComparison.OrdinalIgnoreCase) ?
                    dllFileNameWithoutExtension[mod_dll_file_prefix.Length..] : dllFileNameWithoutExtension;
                void ClearPackages()
                {
                    var files = Directory.GetFiles(currentPath, $"{zipFileNamePrefix}_v*.zip");
                    foreach (var file in files)
                    {
                        Console.WriteLine("ClearPackages Del: " + file);
                        File.Delete(file);
                    }
                }
                string BuildPackage(CompressionLevel level = CompressionLevel.Optimal)
                {
                    var zipFilePath = Path.Combine(currentPath,
                        $"{zipFileNamePrefix}_v{version.ToFullString()}.zip");
                    using var fileStream = new FileStream(zipFilePath, FileMode.CreateNew);
                    using var archive = new ZipArchive(fileStream, ZipArchiveMode.Create, true);
                    archive.CreateEntryFromFile(xmlPath, Path.Combine(modDirName, SubModule_XML), level);
                    foreach (var readmePath in readmePaths)
                    {
                        archive.CreateEntryFromFile(readmePath.Key.FullName, Path.Combine(modDirName, readmePath.Key.Name), level);
                    }
                    var binPath = Path.Combine(utils.BinaryPath);
                    archive.CreateEntryFromFile(dllFilePathSource, Path.Combine(modDirName, binPath, dllFileNameWithoutExtension + ".dll"), level);
                    fileStream.Flush();
                    Console.WriteLine($"BuildPackage Path: {zipFilePath}");
                    return zipFilePath;
                }
                ClearPackages();
                var zipFilePath = BuildPackage();
                TestZipFile(zipFilePath);
            }
            return true;
        }

        private static bool FileEquals(string leftFilePath, string rightFilePath)
        {
            using var leftFileStream = File.OpenRead(leftFilePath);
            using var rightFileStream = File.OpenRead(rightFilePath);
            var left = Hashs.SHA384_String(leftFileStream);
            var right = Hashs.SHA384_String(rightFileStream);
            return left == right;
        }

        private static readonly string[] WinRAR = new[] { "WinRAR", "WinRAR.exe" };

        private static readonly string[] _7_Zip = new[] { "7-Zip", "7z.exe" };

        private static void TestZipFile(string zipFilePath)
        {
            var progra = new[] {
                Environment.SpecialFolder.ProgramFiles,
                Environment.SpecialFolder.ProgramFilesX86
            }.Select(Environment.GetFolderPath).Distinct().ToArray();
            foreach (var programFiles in progra)
            {
                var winrar = Path.Combine(new[] { programFiles }.Concat(WinRAR).ToArray());
                if (File.Exists(winrar))
                {
                    Process.Start(winrar, $"t -r \"{zipFilePath}\"");
                }
                var _7zip = Path.Combine(new[] { programFiles }.Concat(_7_Zip).ToArray());
                if (File.Exists(_7zip))
                {
                    Process.Start(_7zip, $"t \"{zipFilePath}\" -r");
                }
            }
        }
    }
}