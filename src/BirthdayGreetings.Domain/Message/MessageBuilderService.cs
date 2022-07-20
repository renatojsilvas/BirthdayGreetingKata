namespace BirthdayGreetings.Domain
{
    public class MessageBuilderService : IMessageBuilderService
    {
        public Message BuildMessage(string template, string subject, Person recipient)
        {
            return new Message(recipient, subject,
                template.Replace("{0}", recipient.FirstName));
        }
    }
}
