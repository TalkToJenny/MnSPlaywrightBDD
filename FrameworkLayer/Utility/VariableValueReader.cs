using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_SPlaywrightBDD.FrameworkLayer.Utility
{
    public class VariableValueReader
    {
        public static string ReadVariableValue(string PathToVariableFile, string variableAttributeName)
        {
            var fileContent = File.ReadAllText(PathToVariableFile);
            JObject jObjt = JObject.Parse(fileContent);
            return jObjt[variableAttributeName].ToString();// a j token object.
        }
    }
}
