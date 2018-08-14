﻿using MagicalLifeAPI.Entity.AI.Job;
using MagicalLifeAPI.Networking.Serialization;
using ProtoBuf;

namespace MagicalLifeAPI.Networking.Messages
{
    /// <summary>
    /// Used to tell the server that a new job has been created.
    /// </summary>
    [ProtoContract]
    public class JobCreatedMessage : BaseMessage
    {
        [ProtoMember(1)]
        public Job Job { get; set; }

        public JobCreatedMessage(Job job) : base(NetMessageID.JobCreatedMessage)
        {
            this.Job = job;
        }

        public JobCreatedMessage() : base(NetMessageID.JobCreatedMessage)
        {
        }
    }
}