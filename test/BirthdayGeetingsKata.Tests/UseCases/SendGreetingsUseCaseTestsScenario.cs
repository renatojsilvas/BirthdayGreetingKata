using BirthdayGreetings.Domain;

namespace BirthdayGeetingsKata.Tests.UseCases
{
    public class SendGreetingsUseCaseBasicPathScenario : ISendGreetingsUseCaseTestsScenario
    {
        private List<Person> _birthdayPersons = new()
        {
            new Person("John", "Doe", new DateTime(1982, 10, 08), new EmailAddress("john.doe@foobar.com")),
            new Person("Mary", "Ann", new DateTime(1975, 09, 11), new EmailAddress("mary.ann@foobar.com")),
        };

        private string _messageContent = @"
Subject: Happy birthday!

Happy birthday, dear {0}!
";

        private List<Message> _expectedMessageByPerson = new()
        {
           new Message(new Person("John", "Doe", new DateTime(1982, 10, 08), new EmailAddress("john.doe@foobar.com")),
               "Happy Birthday!",
                "Happy birthday, dear John!"),

           new Message(new Person("Mary", "Ann", new DateTime(1982, 10, 11), new EmailAddress("mary.ann@foobar.com")),
               "Happy Birthday!",                
                "Happy birthday, dear Mary!"),
        };

        public DateTime BirthdayDate => new(1982, 10, 15);
        public DateTime TodayDate => new(1982, 10, 01);
        public string MessageTemplate => _messageContent;
        public List<Person> GetBirthdayPersons() => _birthdayPersons;
        public Person GetPerson(string firstName) => _birthdayPersons.Find(person => person.FirstName == firstName);
        public List<Message> GetExpectedMessages() => _expectedMessageByPerson;
    }
}
