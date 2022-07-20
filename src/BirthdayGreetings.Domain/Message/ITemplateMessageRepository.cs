namespace BirthdayGreetings.Domain
{
    public interface IMessageTemplateRepository

    {
        Task<string> GetMessageTemplate();
    }
}
