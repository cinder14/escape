// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace Escape.iOS
{
	public partial class MainController : UIViewController, IViewModelView
	{
        #region Constructor

		public MainController (IntPtr handle) : base (handle)
		{
		}

        #endregion

        #region Properties

        public const string IDENTIFIER = "MainController";

        public DashboardViewModel ViewModel { get; set; }

        #endregion

        #region Overrides

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ViewModel = new DashboardViewModel(this);

            tblData.TableFooterView = new UIView();
            tblData.Source = new DashboardTableViewSource(this);

            this.ViewModel.Start();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.NavigationController.NavigationBarHidden = true;

            tblData.ReloadData();
        }

        #endregion

        #region Nested Classes

        public class DashboardTableViewSource : UITableViewSource
        {
            public DashboardTableViewSource(MainController controller)
            {
                this.Controller = controller;
            }

            public MainController Controller { get; set; }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                Rescue data = this.Controller.ViewModel.Rescues[indexPath.Row];
                UITableViewCell cell = tableView.DequeueReusableCell("CellRescue");
                cell.TextLabel.Text = data.title;
                return cell;
            }
            public override nint RowsInSection(UITableView tableView, nint section)
            {
                return this.Controller.ViewModel.Rescues.Count;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                tableView.DeselectRow(indexPath, true);
            }

        }

        #endregion
	}
}
