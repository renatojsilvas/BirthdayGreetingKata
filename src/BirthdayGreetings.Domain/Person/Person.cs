namespace BirthdayGreetings.Domain
{
    public class Person
    {
        public Person(string firstName, string lastName, DateTime birthdayDate, EmailAddress emailAddress) 
        {
            FirstName = firstName;
            LastName = lastName;
            BirthdayDate = birthdayDate;
            EmailAddress = emailAddress;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public DateTime BirthdayDate { get; }
        public EmailAddress EmailAddress { get; }

        public override bool Equals(object? obj)
        {
            return obj is Person person &&
                   FirstName == person.FirstName &&
                   LastName == person.LastName &&
                   BirthdayDate == person.BirthdayDate &&
                   EqualityComparer<EmailAddress>.Default.Equals(EmailAddress, person.EmailAddress);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, BirthdayDate, EmailAddress);
        }
    }
}
