using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Escape.Win
{
    public class WindowsFileStore : ICacheFileStore
    {
        public WindowsFileStore()
        {
        }

        public void DeleteFile(string filePath)
        {
            this.PerformDeleteFileSafe(filePath);
        }

        public void DeleteFolder(string folderPath, bool recursive)
        {
            this.PerformDeleteFolderSafe(folderPath);
        }

        public void EnsureFolderExists(string folderPath)
        {
            if (FolderExists(folderPath))
            {
                return;
            }
            string rootFolder = this.NativePath(string.Empty);
            StorageFolder rootStorageFolder = StorageFolder.GetFolderFromPathAsync(rootFolder).Await();
            string relativeFolderPath = this.GetRelativePathToSubFolder(rootStorageFolder.Path, folderPath);
            this.CreateFolderRecursiveAsync(rootStorageFolder, relativeFolderPath).GetAwaiter().GetResult();
        }
        public bool Exists(string filePath)
        {
            try
            {
                filePath = this.NativePath(filePath);
                string fileName = Path.GetFileName(filePath);
                string directory = Path.GetDirectoryName(filePath);
                if (!FolderExists(directory))
                {
                    return false;
                }
                StorageFolder folder = StorageFolder.GetFolderFromPathAsync(directory).Await();
                folder.GetFileAsync(fileName).Await();
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }
        public bool FolderExists(string folderPath)
        {
            try
            {
                folderPath = this.NativePath(folderPath);
                folderPath = folderPath.TrimEnd('\\');

                var thisFolder = StorageFolder.GetFolderFromPathAsync(folderPath).Await();
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }

        public Stream OpenRead(string path)
        {
            try
            {
                StorageFile storageFile = NativeFile(path);
                IRandomAccessStreamWithContentType streamWithContentType = storageFile.OpenReadAsync().Await();
                return streamWithContentType.AsStreamForRead();
            }
            catch
            {
                return null;
            }
        }
        public Stream OpenWrite(string path)
        {
            IRandomAccessStream streamWithContentType = OpenWriteStream(path);
            return streamWithContentType.AsStream();
        }
        public IRandomAccessStream OpenWriteStream(string path)
        {
            StorageFile storageFile = null;
            if (this.Exists(path))
            {
                storageFile = this.NativeFile(path);
            }
            else
            {
                storageFile = PerformCreateFileAsync(path).GetAwaiter().GetResult();
            }
            return storageFile.OpenAsync(FileAccessMode.ReadWrite).Await();
        }

        public bool TryReadTextFile(string filePath, out string contents)
        {
            try
            {
                contents = null;
                StorageFile storageFile = this.NativeFile(filePath);
                IRandomAccessStreamWithContentType accessStream = storageFile.OpenReadAsync().Await();
                using (Stream stream = accessStream.AsStreamForRead())
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        contents = streamReader.ReadToEnd();
                    }
                }
                return true;
            }
            catch
            {
                contents = null;
                return false;
            }
        }
        public void WriteFile(string filePath, string contents)
        {
            this.PerformDeleteFileSafe(filePath);

            StorageFile storageFile = PerformCreateFileAsync(filePath).GetAwaiter().GetResult();

            IRandomAccessStream accessStream = storageFile.OpenAsync(FileAccessMode.ReadWrite).Await();
            using (Stream stream = accessStream.AsStreamForWrite())
            {
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    sw.Write(contents);
                    sw.Flush();
                }
            }
        }


        protected virtual bool PerformDeleteFileSafe(string path)
        {
            try
            {
                StorageFile file = this.NativeFile(path);
                file.DeleteAsync().Await();
                return true;
            }
            catch (FileNotFoundException)
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        protected virtual bool PerformDeleteFolderSafe(string folderPath)
        {
            try
            {
                string path = this.NativePath(folderPath);
                StorageFolder folder = StorageFolder.GetFolderFromPathAsync(path).Await();
                folder.DeleteAsync().Await();
                return true;
            }
            catch (FileNotFoundException)
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected virtual async Task<StorageFile> PerformCreateFileAsync(string path)
        {
            string fullPath = this.NativePath(path);
            string directory = Path.GetDirectoryName(fullPath);
            string fileName = Path.GetFileName(fullPath);
            StorageFolder storageFolder = await StorageFolder.GetFolderFromPathAsync(directory).AsTask().ConfigureAwait(false);
            StorageFile storageFile = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting).AsTask().ConfigureAwait(false);
            return storageFile;
        }
        protected virtual StorageFile NativeFile(string path)
        {
            string fullPath = this.NativePath(path);
            StorageFile storageFile = StorageFile.GetFileFromPathAsync(fullPath).Await();
            return storageFile;
        }
        protected virtual string NativePath(string path)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, path);
        }

        protected virtual string GetRelativePathToSubFolder(string rootPath, string subFolderPath)
        {
            string relativePath = subFolderPath;
            if (subFolderPath.ToLower().Contains(rootPath.ToLower()))
            {
                relativePath = subFolderPath.Substring(rootPath.Length + 1);
            }
            return relativePath;
        }

        protected virtual async Task<StorageFolder> CreateFolderRecursiveAsync(StorageFolder rootFolder, string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                return rootFolder;
            }
            StorageFolder currentFolder = await CreateFolderRecursiveAsync(rootFolder, Path.GetDirectoryName(folderPath)).ConfigureAwait(false);
            return await currentFolder.CreateFolderAsync(Path.GetFileName(folderPath), CreationCollisionOption.OpenIfExists).AsTask().ConfigureAwait(false);
        }
    }
}
