using System;

namespace Escape
{
    public interface ITrack
    {
        void LogError(Exception ex, string tag = "");
        void LogError(string message, string tag = "");
        void LogTrace(string message, string tag = "");
        void LogWarning(string message, string tag = "");
    }
}

