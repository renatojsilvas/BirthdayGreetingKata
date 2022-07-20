using BirthdayGreetings.Domain;

namespace BirthdayGeetingsKata.Tests.UseCases
{
    public interface ISendGreetingsUseCaseTestsScenario
    {
        DateTime BirthdayDate { get; }
        DateTime TodayDate { get; }
        string MessageTemplate { get; }
        List<Person> GetBirthdayPersons();
        List<Message> GetExpectedMessages();
        Person GetPerson(string firstName);
    }
}