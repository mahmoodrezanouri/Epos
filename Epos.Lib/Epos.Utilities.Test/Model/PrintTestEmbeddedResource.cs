using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Xunit.Sdk;

namespace Epos.Utilities.Test.Model
{
    public sealed class PrintTestEmbeddedResource : DataAttribute
    {
        private readonly string[] _args;

        public PrintTestEmbeddedResource(params string[] args)
        {
            _args = args;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var result = new object[_args.Length];
            for (var index = 0; index < _args.Length; index++)
            {
                result[index] = ReadManifestData(_args[index], testMethod.Name);
            }
            return new[] { result };
        }

        public static IEnumerable<PrintTestModel> ReadManifestData(string resourceName, string methodName)
        {

            var dataFolder = Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName, resourceName);
            string text = File.ReadAllText(dataFolder);

            var allCases = JsonSerializer.Deserialize<List<PrintTestModel>>(text);

            return allCases;


        }
    }
}
