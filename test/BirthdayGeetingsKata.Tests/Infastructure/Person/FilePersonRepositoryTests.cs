using BirthdayGreetings.Domain;
using BirthdayGreetings.Infrastructure;
using FluentAssertions;

namespace BirthdayGeetingsKata.Tests.Infastructure
{
    public class FilePersonRepositoryTests
    {
        [Fact]
        public async void Should_return_a_list_of_two_persons_from_the_csv_file()
        {
            // Arrange
            var filePath = @"Data\birthdayDates.csv";
            var repository = new PersonRepositoryFile(filePath);

            // Act
            var birthdayPersons = await repository.GetBirthdaysByMonth(1982, 10);

            // Assert
            birthdayPersons.Should().HaveCount(2);
        }

        [Fact]
        public async void Should_return_a_list_of_persons_from_the_csv_file()
        {
            // Arrange
            var filePath = @"Data\birthdayDates.csv";
            var repository = new PersonRepositoryFile(filePath);
            var expectedList = new List<Person>()
            {
                 new Person("John", "Doe", new DateTime(1982, 10, 08), new EmailAddress("john.doe@foobar.com")),
                 new Person("Mary", "Ann", new DateTime(1982, 10, 11), new EmailAddress("mary.ann@foobar.com")),
            };

            // Act
            var birthdayPersons = await repository.GetBirthdaysByMonth(1982, 10);

            // Assert
            birthdayPersons.Should().BeEquivalentTo(expectedList);
        }

        [Fact]
        public async void Should_throw_an_infrastucture_exception_when_file_does_not_exist()
        {
            // Arrange
            var filePath = @"Data\unknown.csv";
            var repository = new PersonRepositoryFile(filePath);

            // Act
            Func<Task> action = () => repository.GetBirthdaysByMonth(1982, 10);

            // Assert
            (await action.Should().ThrowAsync<InfrastructureException>())
                         .WithMessage(@"Data\unknown.csv file does not exist");
        }

        [Fact]
        public async void Should_throw_an_infrastucture_exception_when_file_is_malformed()
        {
            // Arrange
            var filePath = @"Data\malformed.csv";
            var repository = new PersonRepositoryFile(filePath);

            // Act
            Func<Task> action = () => repository.GetBirthdaysByMonth(1982, 10);

            // Assert
            (await action.Should().ThrowAsync<InfrastructureException>())
                         .WithMessage(@"Data\malformed.csv either is not a csv file or is malformed");
        }
    }
}
