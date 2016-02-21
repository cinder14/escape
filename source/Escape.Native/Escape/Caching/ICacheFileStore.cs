using System;
using System.IO;

namespace Escape
{
    public interface ICacheFileStore
    {
        bool Exists(string filePath);
        bool FolderExists(string folderPath);
        bool TryReadTextFile(string filePath, out string contents);
        void EnsureFolderExists(string folderPath);
        void WriteFile(string filePath, string contents);
        void DeleteFile(string filePath);
        void DeleteFolder(string folderPath, bool recursive);
        Stream OpenRead(string path);
        Stream OpenWrite(string path);
    }
}

