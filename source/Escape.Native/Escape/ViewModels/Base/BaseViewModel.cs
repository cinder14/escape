using System;

namespace Escape
{
    public abstract class BaseViewModel : BaseViewModel<IViewModelView>
    {
        public BaseViewModel(IViewModelView view, string trackPrefix)
            : base(view, trackPrefix)
        {
        }
    }
    public abstract class BaseViewModel<TViewModelView> : BaseClass
    {
        public BaseViewModel(TViewModelView view, string trackPrefix)
            : base(trackPrefix)
        {
            this.View = view;
        }

        public virtual bool HasStarted { get; set; }
        public virtual bool HasAppeared { get; set; }
        public virtual bool SeemsVisible { get; set; }

        public virtual TViewModelView View { get; set; }
        public virtual IEscapeApp EscapeApp
        {
            get
            {
                return Container.EscapeApp;
            }
        }

        public virtual void OnAppear()
        {
            this.HasAppeared = true;
            this.SeemsVisible = true;
        }
        public virtual void OnDisappear()
        {
            this.SeemsVisible = false;
        }

        public virtual void Start()
        {
            this.HasStarted = true;
        }




    }
}

