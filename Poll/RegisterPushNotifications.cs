using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;


namespace Poll
{
    internal class Channel
    {
        public int Id { get; set; }
        public string ChannelUri { get; set; }
        public string InstallationId { get; set; }
    }
    
    /// <summary>
    /// A class that has boilerplate code that configures push notifications by registering the channel of the device with the service.
    /// In Visual Studio 2013, this class is provided for you automatically.
    /// </summary>
    internal class RegisterPushNotifications
    {
        public static event Windows.Foundation.TypedEventHandler<PushNotificationChannel, PushNotificationReceivedEventArgs> PushNotificationReceived;

        /// <summary>
        /// Register the push notification channel
        /// </summary>
        /// <param name="mobileServiceClient">Reference to your MobileServiceClient</param>
        public async static void UploadChannel(MobileServiceClient mobileServiceClient)
        {
            var channel = await Windows.Networking.PushNotifications.PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            var token = Windows.System.Profile.HardwareIdentification.GetPackageSpecificToken(null);
            string installationId = Windows.Security.Cryptography.CryptographicBuffer.EncodeToBase64String(token.Id);


            channel.PushNotificationReceived += channel_PushNotificationReceived;
            var ch = new Channel();
            ch.ChannelUri = channel.Uri;
            ch.InstallationId = installationId;

            try
            {
                await mobileServiceClient.GetTable<Channel>().InsertAsync(ch);
            }
            catch (Exception exception)
            {
                HandleInsertChannelException(exception);
            }
        }

        /// <summary>
        /// Propagate the event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        static void channel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            PushNotificationReceived(sender, args);
        }

        private static void HandleInsertChannelException(Exception exception)
        {

        }
    }
}
