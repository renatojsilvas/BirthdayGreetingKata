namespace BirthdayGreetings.Domain
{
    public interface IMessageBuilderService
    {
        Message BuildMessage(string template, string subject, Person recipient);
    }
}