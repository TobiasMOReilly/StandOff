using System;
using System.Net;

namespace Mirror.Discovery
{
    public class ServerResponse : MessageBase
    {
        // The server that sent this
        // this is a property so that it is not serialized,  but the
        // client fills this up after we receive it
        public IPEndPoint EndPoint { get; set; }

        public Uri uri;

        //FAR ADDED
        public string name;
        public int maxPlayers;
        public int currentPlayers;
        //END FAR ADDED
        // Prevent duplicate server appearance when a connection can be made via LAN on multiple NICs
        public long serverId;
    }
}