using System;

namespace Escape
{
    public interface ICreateView : IViewModelView
    {
        void EnsureNotificationPermission();
    }
}

