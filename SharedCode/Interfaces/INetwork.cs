using System;

namespace SharedCode.Interfaces
{
    public interface INetwork
    {
        EventHandler<NetworkChangedEventArgs> NetworkHasChanged { set; get; }
        void OnNetworkChanged(NetworkChangedEventArgs args);
    }

    public class NetworkChangedEventArgs : EventArgs
    {
        public bool IsConnected { get; set; }
    }
}
