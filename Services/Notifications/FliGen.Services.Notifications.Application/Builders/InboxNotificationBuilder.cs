using FliGen.Services.Notifications.Application.Commands;

namespace FliGen.Services.Notifications.Application.Builders
{
    public class InboxNotificationBuilder : IMessageBuilder<SendInboxNotification>
    {
        private readonly SendInboxNotification _notification;

        private InboxNotificationBuilder()
        {
            _notification = new SendInboxNotification();
        }

        public static IMessageBuilder<SendInboxNotification> Create()
        {
            return new InboxNotificationBuilder();
        }

        public IMessageBuilder<SendInboxNotification> WithSender(string sender)
        {
            _notification.Sender = sender;
            return this;
        }

        public IMessageBuilder<SendInboxNotification> WithReceiver(object receiver)
        {
            _notification.PlayerIds = (int[])receiver;
            return this;
        }

        public IMessageBuilder<SendInboxNotification> WithTopic(string topic)
        {
            _notification.Topic = topic;
            return this;
        }

        public IMessageBuilder<SendInboxNotification> WithBody(string body)
        {
            _notification.Body = body;
            return this;
        }

        public SendInboxNotification Build()
        {
            return _notification;
        }
    }
}