using BirthdayGreetings.Domain;
using System.Globalization;

namespace BirthdayGreetings.Infrastructure
{
    public class PersonRepositoryFile : IPersonRepository
    {
        private readonly string _filePath;

        public PersonRepositoryFile(string filePath)
        {
            _filePath = filePath;
        }
        public async Task<IList<Person>> GetBirthdaysByMonth(int year, int month)
        {
            var birthdays = new List<Person>();
            try
            {
                var lines = await File.ReadAllLinesAsync(_filePath);                
                foreach (var line in lines)
                {
                    if (line.Equals("last_name, first_name, date_of_birth, email"))
                        continue;

                    var items = line.Split(',');
                    var lastName = items[0].Trim();
                    var firstName = items[1].Trim();
                    var birthdayDate = DateTime.Parse(items[2].Trim(), new CultureInfo("en-US"));
                    var emailAddress = new EmailAddress(items[3].Trim());

                    if (birthdayDate.Year != year || birthdayDate.Month != month)
                        continue;

                    var person = new Person(firstName, lastName, birthdayDate, emailAddress);

                    birthdays.Add(person);
                }
            }
            catch (FileNotFoundException)
            {
                throw new InfrastructureException($"{_filePath} file does not exist");
            }
            catch (Exception)
            {
                throw new InfrastructureException($"{_filePath} either is not a csv file or is malformed");
            }

            return birthdays;
        }
    }
}
