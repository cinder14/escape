using System;

namespace Escape
{
    public partial interface IViewPlatform
    {
        string ShortName { get; }
        string VersionNumber { get; }
    }
}

