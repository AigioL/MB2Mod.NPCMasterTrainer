using MB2Mod.NPCMasterTrainer.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public interface IExportData
        {
            StringBuilder AppendRowString(StringBuilder stringBuilder);
        }

        public abstract partial class ExportData : IExportData
        {
            public const string Separator = ",";

            static StringBuilder StringToCSVCell(StringBuilder stringBuilder, string str)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    var mustQuote = str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n");
                    if (mustQuote)
                    {
                        stringBuilder.Append("\"");
                        foreach (char nextChar in str)
                        {
                            stringBuilder.Append(nextChar);
                            if (nextChar == '"') stringBuilder.Append("\"");
                        }
                        stringBuilder.Append("\"");
                    }
                    else
                    {
                        stringBuilder.Append(str);
                    }
                }
                return stringBuilder;
            }

            public static StringBuilder Join(StringBuilder stringBuilder, IEnumerable<string> strings)
            {
                if (strings != default && strings.Any())
                {
                    int count = default, i = default;
                    var hasCount = false;
                    if (strings is ICollection<string> strings2)
                    {
                        count = strings2.Count;
                        hasCount = true;
                    }
                    else if (strings is IReadOnlyCollection<string> strings3)
                    {
                        count = strings3.Count;
                        hasCount = true;
                    }
                    foreach (var item in strings)
                    {
                        StringToCSVCell(stringBuilder, item);
                        if (hasCount)
                        {
                            i++;
                            if (i == count) return stringBuilder;
                        }
                        stringBuilder.Append(Separator);
                    }
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
                return stringBuilder;
            }

            public abstract StringBuilder AppendRowString(StringBuilder stringBuilder);
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

            public override StringBuilder AppendRowString(StringBuilder stringBuilder)
            {
                var strings = Values;
                return Join(stringBuilder, strings);
            }

            public static IEnumerable<string> TableHeaders
            {
                get
                {
                    var properties = lazy_properties.Value;
                    var strings = properties.Select(x => Resources.GetString(x.Name));
                    return strings;
                }
            }
        }

        private static void WriteFile(string contents, string fileNamePrefix)
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

        private static bool? Export<T, TExportData>(IEnumerable<T> source, Func<T, TExportData> convert, IEnumerable<string> tableHeaders, string fileNamePrefix,
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
                    var stringBuilder = new StringBuilder();
                    ExportData.Join(stringBuilder, tableHeaders);
                    string[] dynamicHeaders = null;
                    if (hasDynamicHeaders)
                    {
                        dynamicHeaders = getDynamicHeaders(items);
                        if (dynamicHeaders != null && dynamicHeaders.Any())
                        {
                            stringBuilder.Append(ExportData.Separator);
                            ExportData.Join(stringBuilder, dynamicHeaders);
                        }
                        else
                        {
                            hasDynamicHeaders = false;
                        }
                    }
                    stringBuilder.AppendLine();
                    foreach (var item in items)
                    {
                        item.AppendRowString(stringBuilder);
                        if (hasDynamicHeaders)
                        {
                            var dynamicValues = dynamicHeaders.Select(x => getDynamicValue(x, item));
                            stringBuilder.Append(ExportData.Separator);
                            ExportData.Join(stringBuilder, dynamicValues);
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

        private static void Print<T>(this IEnumerable<T> items, string tag, string name, Func<T, object> print)
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