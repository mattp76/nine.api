using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nine.Test.Helpers
{
    public class PathHelper
    {
        public static string GetBinPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static string GetProjectPath()
        {
            string appRoot = GetBinPath();
            var dir = new DirectoryInfo(appRoot).Parent.Parent.Parent;
            var name = dir.Name;
            return dir.FullName + @"\" + name + @"\";
        }

        public static string GetTestProjectPath()
        {
            string appRoot = GetBinPath();
            var dir = new DirectoryInfo(appRoot).Parent.Parent;
            return dir.FullName + @"\";
        }

        public static string GetMainProjectPath()
        {
            string testProjectPath = GetTestProjectPath();
            // Just hope it ends in the standard .Tests, lop it off, done.
            string path = testProjectPath.Substring(0, testProjectPath.Length - 7) + @"\";
            return path;
        }
    }
}
