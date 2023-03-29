using System;

namespace SharedCode.Interfaces
{
    public interface INetworkConnection
    {
        EventHandler<NetworkChangedEventArgs> NetworkHasChanged { set; get; }
        void OnNetworkChanged(NetworkChangedEventArgs args);
    }

    public class NetworkChangedEventArgs : EventArgs
    {
        public bool IsConnected { get; set; }
    }
}
