using System;
using System.IO;

namespace Escape.iOS
{
    public class IOSFileStore : BaseFileStore
    {
        public IOSFileStore()
        {
        }

        public override string NativePath(string filePath)
        {
            if (filePath.StartsWith("res:"))
            {
                return filePath.Substring("res:".Length);
            }
            if (filePath.StartsWith("file://"))
            {
                return filePath.Substring("file://".Length);
            }
            if (filePath.StartsWith("assets-library:/"))
            {
                return filePath;
            }
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), filePath);
        }
    }
}

