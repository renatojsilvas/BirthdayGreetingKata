using BirthdayGreetings.Domain;
using FluentAssertions;

namespace BirthdayGeetingsKata.Tests.Domain
{
    public class MessageBuilderServiceTests
    {
        readonly string _template;
        readonly string _subject;
        readonly string _body;
        readonly Person _recipient;
        readonly Message _expectedMessage;

        public MessageBuilderServiceTests()
        {
            _template = "Teste {0}";
            _recipient = new Person("Recipient", "Teste", DateTime.Today, new EmailAddress("recipient@test.com"));
            _subject = "Subject";
            _body = "Teste Recipient";
            _expectedMessage = new Message(_recipient, _subject, _body);
        }

        [Fact]
        public void Should_return_the_message_when_input_is_valid()
        {
            // Arrange          
            var service = new MessageBuilderService();

            // Act
            var message = service.BuildMessage(_template, _subject, _recipient);

            // Assert
            message.Should().Be(_expectedMessage);
        }
    }
}
