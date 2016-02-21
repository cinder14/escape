using System;
using System.Collections.Generic;

namespace Escape
{
    public class DashboardViewModel : BaseViewModel
    {
        public DashboardViewModel(IViewModelView view)
            : base(view, "DashboardViewModel")
        {
        }

        public List<Rescue> Rescues
        {
            get
            {
                return this.EscapeApp.RescueGetAll();
            }
        }

    }
}

