using System;

namespace INTS_DATASET.Utils
{
    public static class PathUtils
    {
        public static string GetProjectDirectoryPath()
        {
            return AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
        }
    }
}
