﻿using MagicalLifeAPI.Networking;
using MagicalLifeAPI.Networking.Messages;
using MagicalLifeAPI.Networking.Serialization;
using MagicalLifeAPI.Networking.World;

namespace MagicalLifeClient.Message
{
    public class WorldTransferHeaderMessageHandler : MessageHandler
    {
        public WorldTransferHeaderMessageHandler() : base(NetMessageID.WorldTransferHeaderMessage)
        {
        }

        public override void HandleMessage(BaseMessage message)
        {
            WorldTransferHeaderMessage msg = (WorldTransferHeaderMessage)message;
            NetWorldReceiver.Receive(msg);
        }
    }
}