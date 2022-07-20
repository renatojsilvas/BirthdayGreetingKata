namespace BirthdayGreetings.Domain
{
    public interface IPersonRepository
    {
        Task<IList<Person>> GetBirthdaysByMonth(int year, int month);
    }
}
