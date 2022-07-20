namespace BirthdayGreetings.Domain
{
    public class Message
    {
        public Message(Person recipient, string subject, string body)
        {            
            Recipient = recipient;            
            Subject = subject;
            Body = body;
        }
        
        public Person Recipient { get; }
        public string Subject { get; }
        public string Body { get; }

        public override bool Equals(object? obj)
        {
            return obj is Message message &&
                   EqualityComparer<Person>.Default.Equals(Recipient, message.Recipient) &&
                   Subject == message.Subject &&
                   Body == message.Body;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Recipient, Subject, Body);
        }
    }
}
