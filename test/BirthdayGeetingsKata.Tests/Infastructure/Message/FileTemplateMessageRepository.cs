
using BirthdayGreetings.Infrastructure;
using FluentAssertions;

namespace BirthdayGeetingsKata.Tests.Infastructure.Message
{
    public class FileTemplateMessageRepository
    {
        [Fact]
        public async void Should_return_the_message_template_from_csv_file()
        {
            // Arrange
            var filePath = @"Data\template.txt";
            var repository = new FileMessageTemplateRepository(filePath);

            // Act
            var template = await repository.GetMessageTemplate();

            // Assert
            template.Should().Contain("{0}");
        }

        [Fact]
        public async void Should_throw_an_infrastucture_exception_when_file_does_not_exist()
        {
            // Arrange
            var filePath = @"Data\unknown.txt";
            var repository = new FileMessageTemplateRepository(filePath);

            // Act
            Func<Task> action = () => repository.GetMessageTemplate();

            // Assert
            (await action.Should().ThrowAsync<InfrastructureException>())
                         .WithMessage(@"Data\unknown.txt file does not exist");
        }

        [Fact]
        public async void Should_throw_an_infrastucture_exception_when_file_is_malformed()
        {
            // Arrange
            var filePath = @"Data\templateMalFormed.txt";
            var repository = new FileMessageTemplateRepository(filePath);

            // Act
            Func<Task> action = () => repository.GetMessageTemplate();

            // Assert
            (await action.Should().ThrowAsync<InfrastructureException>())
                         .WithMessage(@"Data\templateMalFormed.txt is malformed");
        }
    }
}
