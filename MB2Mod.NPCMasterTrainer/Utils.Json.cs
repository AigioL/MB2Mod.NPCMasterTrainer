using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        private static readonly StringEnumConverter stringEnumConverter = new StringEnumConverter();

        public static string ToJsonString(this object obj, bool writeIndented = true)
        {
            return JsonConvert.SerializeObject(obj, writeIndented ? Formatting.Indented : Formatting.None, stringEnumConverter);
        }

        public static bool TryDeserialize<T>(string jsonStr, out T obj)
        {
            try
            {
                obj = JsonConvert.DeserializeObject<T>(jsonStr, stringEnumConverter);
                return true;
            }
            catch
            {
                obj = default;
                return false;
            }
        }
    }
}