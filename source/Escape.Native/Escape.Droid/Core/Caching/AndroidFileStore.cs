using System;
using Android.Content;
using System.IO;

namespace Escape.Droid
{
    public class AndroidFileStore : BaseFileStore
    {
        public AndroidFileStore(Context context)
        {
            this.Context = context;
        }

        private Context Context { get; set; }

        public override string NativePath(string path)
        {
            return Path.Combine(Context.FilesDir.Path, path);
        }
    }
}

