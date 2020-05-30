//public static string? Init()
//{
//    if (GetCultureByMBTextManager(out var culture, out var currentLanguageId) && culture != default)
//    {
//        Resources.Culture = culture;
//        return currentLanguageId;
//    }
//    return default;
//}

//[Obsolete]
//public static bool GetCultureByMBTextManager(out CultureInfo? culture, out string? currentLanguageId)
//{
//    culture = default;
//    currentLanguageId = default;
//    try
//    {
//        currentLanguageId = GetCurrentLanguageIdByMBTextManager();
//        if (currentLanguageId != null)
//        {
//            if (mapping_language_id__culture_name.TryGetValue(currentLanguageId, out var value))
//            {
//                culture = new CultureInfo(value);
//            }
//        }
//    }
//    catch
//    {

//    }
//    return culture != default;
//}

//[DllImport("kernel32.dll")]
//internal static extern ushort GetUserDefaultUILanguage();

//static readonly CultureInfo SimplifiedChineseCultureInfo = new CultureInfo(SimplifiedChinese);
//static readonly CultureInfo TraditionalChineseCultureInfo = new CultureInfo(TraditionalChinese);

//public static string GetLanguage()
//{
//    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
//    {
//        var lang = GetUserDefaultUILanguage();
//        var culture = new CultureInfo(lang);
//        if (culture.Match(SimplifiedChineseCultureInfo)) return SimplifiedChinese;
//        if (culture.Match(TraditionalChineseCultureInfo)) return TraditionalChinese;
//    }
//    return English;
//}

//static bool Match(this CultureInfo culture, CultureInfo parent)
//{
//    int i = 0;
//    while (parent != null && parent != CultureInfo.InvariantCulture)
//    {
//        if (i > 10) break;
//        if (culture == parent) return true;
//        parent = parent.Parent;
//        i++;
//    }
//    return false;
//}

//public static string GetLanguageByWin32Registry()
//{
//    var typeRegistry = Type.GetType("Microsoft.Win32.Registry");
//    if (typeRegistry != null)
//    {
//        var typeString = typeof(string);
//        var typeRegistryKey = Type.GetType("Microsoft.Win32.RegistryKey");
//        var typeRegistryRights = Type.GetType("System.Security.AccessControl.RegistryRights");
//        var readKeyEnumByRegistryRights = Enum.Parse(typeRegistryRights, "ReadKey");
//        var methodOpenSubKey = typeRegistryKey.GetMethod("OpenSubKey", new[] { typeString, typeRegistryRights });
//        var methodGetValue = typeRegistryKey.GetMethod("GetValue", new[] { typeString });
//        var path = @"SYSTEM\ControlSet001\Control\Nls\Language";
//        var registry = typeRegistry.GetField("LocalMachine", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField).GetValue(null);
//        foreach (var item in path.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries))
//        {
//            registry = methodOpenSubKey.Invoke(registry, new[] { item, readKeyEnumByRegistryRights });
//        }
//        var value = methodGetValue.Invoke(registry, new[] { "Default" })?.ToString();
//        switch (value)
//        {
//            case "0804":
//                return SimplifiedChinese;
//            case "0404":
//                return TraditionalChinese;
//        }
//        // 0411 Japanese
//    }
//    else
//    {
//        if (IsDevelopment)
//        {
//            Console.WriteLine("Type Not Found Microsoft.Win32.Registry." + Environment.NewLine);
//        }
//    }
//    return English; // 0409
//}

//public static bool UseGameLanguage { get; private set; }

//public static string GetLanguage()
//{
//    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
//    {
//        try
//        {
//            return GetLanguageByWin32Registry();
//        }
//        catch (Exception ex)
//        {
//            if (IsDevelopment)
//            {
//                Console.WriteLine("GetLanguageByWin32Registry catch." + Environment.NewLine + ex + Environment.NewLine);
//            }
//            UseGameLanguage = true;
//        }
//    }
//    return English;
//}