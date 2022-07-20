namespace BirthdayGreetings.Domain
{
    public interface IMessageService
    {
        Task SendMessage(Message message);
    }
}
