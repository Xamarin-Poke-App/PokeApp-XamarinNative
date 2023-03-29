using System;
using Xamarin.Essentials;
using SharedCode.Interfaces;

namespace SharedCode.Util
{
 
    public class NetworkConnection : INetworkConnection
    {
        public NetworkConnection(EventHandler<NetworkChangedEventArgs> handler)
        {
            NetworkHasChanged += handler;
            Connectivity.ConnectivityChanged += OnNetworkChanged;
        }

        public EventHandler<NetworkChangedEventArgs> NetworkHasChanged { get; set; }

        private void OnNetworkChanged(object sender, ConnectivityChangedEventArgs args)
        {
            EventHandler<NetworkChangedEventArgs> handler = NetworkHasChanged;

            if (handler == null) return;

            var access = args.NetworkAccess;
            var profiles = args.ConnectionProfiles;
            var customArgs = new NetworkChangedEventArgs();

            if (access == NetworkAccess.Internet)
            {
                customArgs.IsConnected = true;
                this.OnNetworkChanged(customArgs);
                return;
            }

            customArgs.IsConnected = false;
            this.OnNetworkChanged(customArgs);
        }

        public void OnNetworkChanged(NetworkChangedEventArgs args)
        {
            NetworkHasChanged.Invoke(this, args);
        }
    }
}
