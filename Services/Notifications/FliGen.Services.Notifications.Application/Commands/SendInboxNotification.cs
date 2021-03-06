﻿using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Notifications.Application.Commands
{
    [MessageNamespace("players")]
    public class InboxNotification : ICommand
    {
        public int[] PlayerIds { get; set; }
        public string Sender { get; set; }
        public string Topic { get; set; }
        public string Body { get; set; }

        public InboxNotification(){}

        [JsonConstructor]
        public InboxNotification(int[] playerIds, string sender, string topic, string body)
        {
            PlayerIds = playerIds;
            Sender = sender;
            Topic = topic;
            Body = body;
        }
    }
}