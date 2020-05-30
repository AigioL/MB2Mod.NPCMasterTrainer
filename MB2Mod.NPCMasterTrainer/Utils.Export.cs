using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MB2Mod.NPCMasterTrainer.Properties;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public interface IExportData
        {
            string ToRowString();
        }

        public abstract partial class ExportData : IExportData
        {
            public const string Separator = ",";

            static string StringToCSVCell(string str)
            {
                if (str == default) return string.Empty;
                var mustQuote = str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n");
                if (mustQuote)
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("\"");
                    foreach (char nextChar in str)
                    {
                        stringBuilder.Append(nextChar);
                        if (nextChar == '"') stringBuilder.Append("\"");
                    }
                    stringBuilder.Append("\"");
                    return stringBuilder.ToString();
                }
                return str;
            }

            public static string Join(IEnumerable<string> strings) => string.Join(Separator, strings.Select(StringToCSVCell));

            public abstract string ToRowString();
        }

        public abstract class ExportData<TExportData> : ExportData where TExportData : ExportData<TExportData>
        {
            public static readonly Lazy<PropertyInfo[]> lazy_properties = new Lazy<PropertyInfo[]>(() =>
            {
                var properties = typeof(TExportData).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty);
                var query = from p in properties let attr = p.GetCustomAttribute<Int32Attribute>() where attr != null orderby attr.Value select p;
                return query.ToArray();
            });

            protected IEnumerable<string> Values
            {
                get
                {
                    var properties = lazy_properties.Value;
                    var strings = properties.Select(x => x.GetValue(this)?.ToString());
                    return strings;
                }
            }

            public override string ToRowString()
            {
                var strings = Values;
                return Join(strings);
            }

            protected static IEnumerable<string> TableHeaders
            {
                get
                {
                    var properties = lazy_properties.Value;
                    var strings = properties.Select(x => Resources.GetString(x.Name));
                    return strings;
                }
            }

            public static string TableHeader => Join(TableHeaders);
        }

        static void WriteFile(string contents, string fileNamePrefix)
        {
            if (string.IsNullOrWhiteSpace(contents)) return;
            var path = ExportDirectory;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var filePath = Path.Combine(path, $"{fileNamePrefix} {DateTime.Now:yyyy-MM-dd HH.mm.ss.fffffff}.csv");
            var message = $"Export, filePath: {filePath}";
            DisplayMessage(message);
            if (File.Exists(filePath)) File.Delete(filePath);
            File.WriteAllText(filePath, contents, new UTF8Encoding(true)); // csv utf-8 with BOM
        }

        static bool? Export<T, TExportData>(IEnumerable<T> source, Func<T, TExportData> convert, string tableHeader, string fileNamePrefix,
            Func<IEnumerable<TExportData>, string[]> getDynamicHeaders = null,
            Func<string, TExportData, string> getDynamicValue = null)
            where TExportData : IExportData
        {
            try
            {
                var hasItems = source != null && source.Any();
                TExportData[] items = hasItems ? source.Select(convert).Where(x => x != null).ToArray() : null;
                if (items != null && items.Any())
                {
                    var hasDynamicHeaders = getDynamicHeaders != default && getDynamicValue != default;
                    var stringBuilder = new StringBuilder(tableHeader);
                    string[] dynamicHeaders = null;
                    if (hasDynamicHeaders)
                    {
                        dynamicHeaders = getDynamicHeaders(items);
                        if (dynamicHeaders != null && dynamicHeaders.Any())
                        {
                            stringBuilder.Append(ExportData.Separator);
                            stringBuilder.Append(ExportData.Join(dynamicHeaders));
                        }
                        else
                        {
                            hasDynamicHeaders = false;
                        }
                    }
                    stringBuilder.AppendLine();
                    foreach (var item in items)
                    {
                        var str = item.ToRowString();
                        stringBuilder.Append(str);
                        if (hasDynamicHeaders)
                        {
                            var dynamicValues = dynamicHeaders.Select(x => getDynamicValue(x, item)).ToArray();
                            str = ExportData.Join(dynamicValues);
                            if (!string.IsNullOrEmpty(str))
                            {
                                stringBuilder.Append(ExportData.Separator);
                                stringBuilder.Append(str);
                            }
                        }
                        stringBuilder.AppendLine();
                    }
                    var contents = stringBuilder.ToString();
                    WriteFile(contents, fileNamePrefix);
                }
                else
                {
                    if (Config.Instance.HasWin32Console())
                    {
                        Console.WriteLine($"Export Fail, fileNamePrefix: {fileNamePrefix} hasItems:{hasItems}");
                    }
                }
                return hasItems;
            }
            catch (Exception e)
            {
                DisplayMessage(e);
                return null;
            }
        }

        static void Print<T>(this IEnumerable<T> items, string tag, string name, Func<T, object> print)
        {
            Console.WriteLine($"----- Print {name}({tag}) -----");
            if (items != null && items.Any())
            {
                var arr = items.Select(print);
                Console.WriteLine(ToJsonString(arr));
            }
            else
            {
                Console.WriteLine($"print {name} fail, is empty or null.");
            }
            Console.WriteLine();
        }
    }
}
