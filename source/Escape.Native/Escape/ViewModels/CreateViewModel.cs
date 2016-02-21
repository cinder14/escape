using System;

namespace Escape
{
    public class CreateViewModel : BaseViewModel<ICreateView>
    {
        public CreateViewModel(ICreateView view)
            : base(view, "CreateViewModel")
        {
        }

        public void AddRescue(Rescue rescue)
        {
            //TODO:Should:Perform validation before this
            this.EscapeApp.RescueAdd(rescue);
        }

        public override void OnAppear()
        {
            base.OnAppear();

            this.View.EnsureNotificationPermission();
        }

        public void OnCalendarAccessGranted(bool granted)
        {
            // we might care about this one day
        }
    }
}

