using System.Reflection;
using System.Runtime.CompilerServices;
using MB2Mod.NPCMasterTrainer;

[assembly: SuppressIldasm]
[assembly: AssemblyTitle(Utils.AssemblyTitle)]
[assembly: AssemblyVersion(Utils.Version)]
[assembly: AssemblyFileVersion(Utils.FileVersion)]
[assembly: InternalsVisibleTo("MB2Mod.NPCMasterTrainer.UnitTest")]
[assembly: InternalsVisibleTo("MB2Mod.NPCMasterTrainer.Launcher")]

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        internal const string FileVersion = "1.0.3";
        internal const string Version = FileVersion + ".*";
        internal const string AssemblyTitle = "M&BII Mod NPC Master Trainer";
    }
}