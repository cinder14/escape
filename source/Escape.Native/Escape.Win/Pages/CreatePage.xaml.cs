using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Escape.Win.Pages
{
    public sealed partial class CreatePage : Page, ICreateView
    {
        public CreatePage()
        {
            this.InitializeComponent();
        }

        #region Properties
        public CreateViewModel ViewModel { get; set; }

        #endregion

        #region Overrides

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.ViewModel = new CreateViewModel(this);

            txtWhen.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm tt");
            this.ViewModel.Start();
        }

        #endregion

        #region View Methods

        public void EnsureNotificationPermission()
        {
            
        }

        private void Create()
        {
            try
            {
                DateTime parsed = default(DateTime);
                if (!DateTime.TryParse(txtWhen.Text, out parsed) || parsed == default(DateTime))
                {
                    throw new Exception("You must provide a valid date");
                }
                Rescue rescue = new Rescue()
                {
                    rescue_id = Guid.NewGuid(),
                    stamp_utc = parsed,
                    title = txtTitle.Text,
                    body = txtBody.Text
                };

                string jsonString = JsonConvert.SerializeObject(rescue);

                // create notification here
                XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
                var strings = toastXml.GetElementsByTagName("text");
                strings[0].AppendChild(toastXml.CreateTextNode(rescue.title));
                strings[1].AppendChild(toastXml.CreateTextNode(rescue.body));
                XmlElement toastElement = ((XmlElement)toastXml.SelectSingleNode("/toast"));
                toastElement.SetAttribute("launch", "#/Pages/Notification.xaml?rescue=" + jsonString);

                ScheduledToastNotification toast = new ScheduledToastNotification(toastXml, rescue.stamp_utc);
                toast.Id = rescue.ToString();

                Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);



                this.ViewModel.AddRescue(rescue);

                //TODO:Could:Notify success first.

                this.Frame.GoBack();
            }
            catch (Exception ex)
            {
                new MessageDialog(ex.Message, "Error").ShowAsync();
            }

        }

        #endregion

        #region Event Handlers
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            this.Create();
        }

        #endregion
    }
}
