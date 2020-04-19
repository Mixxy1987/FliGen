using FliGen.Services.Notifications.Application.Commands;

namespace FliGen.Services.Notifications.Application.Builders
{
    public class InboxNotificationBuilder : IMessageBuilder<InboxNotification>
    {
        private readonly InboxNotification _notification;

        private InboxNotificationBuilder()
        {
            _notification = new InboxNotification();
        }

        public static IMessageBuilder<InboxNotification> Create()
        {
            return new InboxNotificationBuilder();
        }

        public IMessageBuilder<InboxNotification> WithSender(string sender)
        {
            _notification.Sender = sender;
            return this;
        }

        public IMessageBuilder<InboxNotification> WithReceiver(object receiver)
        {
            _notification.PlayerIds = (int[])receiver;
            return this;
        }

        public IMessageBuilder<InboxNotification> WithTopic(string topic)
        {
            _notification.Topic = topic;
            return this;
        }

        public IMessageBuilder<InboxNotification> WithBody(string body)
        {
            _notification.Body = body;
            return this;
        }

        public InboxNotification Build()
        {
            return _notification;
        }
    }
}