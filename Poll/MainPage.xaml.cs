using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.MobileServices;
using Poll.Models;
using System.Threading.Tasks;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Poll
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            await Authenticate();
            await App.pvm.LoadItems();
            PollItemsGridView.ItemsSource = App.pvm.Items;
        }

        /// <summary>
        /// Perform the authentication
        /// </summary>
        /// <returns></returns>
        private async Task Authenticate()
        {
            while (App.CurrentUser == null)
            {
                try
                {
                    // Passing the "singleSignOn" parameter as true will make the app remember your login
                    // But you have to associate it with the store first
                    App.CurrentUser = await App.pvm.MobileService.LoginAsync(MobileServiceAuthenticationProvider.Facebook,true);
                }
                catch (InvalidOperationException) { /* cancelled login */ }
                catch (FileNotFoundException) { /* connectivity issue */ }
            }
        }

        /// <summary>
        /// Add item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddOption_Click(object sender, RoutedEventArgs e)
        {
           await App.pvm.AddItem(new Option() { Name = NameTextBox.Text });
        }

        /// <summary>
        /// Register vote
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Vote_Click(object sender, RoutedEventArgs e)
        {
            await App.pvm.Vote((PollItemsGridView.SelectedItem as Option));
        }

        private async void DeleteVotes_Click(object sender, RoutedEventArgs e)
        {
            await App.pvm.DeleteVotes();
        }

    }
}
