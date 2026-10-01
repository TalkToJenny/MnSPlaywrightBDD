using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_SPlaywrightBDD.FrameworkLayer.Utility
{
    public class PathHelper
    {
        public static string GetPathToFile(string fileName, string folderName = "")
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            //var fileLocation = $"/{folderName}/{fileName}";
            var fileLocation = "";
            if (string.IsNullOrEmpty(folderName))
            {
                fileLocation = fileName;
            }
            else fileLocation = Path.Combine(folderName, fileName);

            var fullPath = Path.Combine(currentDirectory, fileLocation);

            var directoryPath = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directoryPath))
            {
                // If not, create the directory
                Directory.CreateDirectory(directoryPath);
            }
            return fullPath;
        }
    }
}
