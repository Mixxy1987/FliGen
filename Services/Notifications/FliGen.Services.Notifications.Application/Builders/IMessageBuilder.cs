namespace FliGen.Services.Notifications.Application.Builders
{
    public interface IMessageBuilder<out T>
    {
        IMessageBuilder<T> WithSender(string sender);
        IMessageBuilder<T> WithReceiver(object receiver);
        IMessageBuilder<T> WithTopic(string topic);
        IMessageBuilder<T> WithBody(string body);
        T Build();
    }
}