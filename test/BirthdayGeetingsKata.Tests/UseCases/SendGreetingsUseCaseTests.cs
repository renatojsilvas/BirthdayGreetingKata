using BirthdayGreetings.Domain;
using BirthdayGreetings.Domain.UseCases;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace BirthdayGeetingsKata.Tests.UseCases
{
    public class SendGreetingsUseCaseTests
    {
        private readonly SendGreetingsUseCase _useCase;

        private readonly Mock<IPersonRepository> _personRepositoryFake;
        private readonly Mock<IMessageTemplateRepository> _messageTemplateRepositoryFake;
        private readonly Mock<IMessageBuilderService> _messageBuilderServiceFake;
        private readonly Mock<IMessageService> _emailServiceFake;

        private readonly ISendGreetingsUseCaseTestsScenario _testScenario;

        private readonly AutoMocker _autoMocker = new();

        public SendGreetingsUseCaseTests()
        {
            _personRepositoryFake = new Mock<IPersonRepository>();
            _messageTemplateRepositoryFake = new Mock<IMessageTemplateRepository>();
            _messageBuilderServiceFake = new Mock<IMessageBuilderService>();
            _emailServiceFake = new Mock<IMessageService>();

            _testScenario = new SendGreetingsUseCaseBasicPathScenario();

            _personRepositoryFake.Setup(x =>
                    x.GetBirthdaysByMonth(It.Is<int>(x => x == _testScenario.BirthdayDate.Year),
                                          It.Is<int>(x => x == _testScenario.BirthdayDate.Month)))
                     .ReturnsAsync(_testScenario.GetBirthdayPersons());
            _personRepositoryFake.Setup(x =>
                    x.GetBirthdaysByMonth(It.Is<int>(x => x == DateTime.Now.Year),
                                          It.Is<int>(x => x == DateTime.Now.Month)))
                     .ReturnsAsync(new List<Person>());

            _messageTemplateRepositoryFake.Setup(x => x.GetMessageTemplate())
                                          .ReturnsAsync(_testScenario.MessageTemplate);


            List<Message> _expectedMessages = _testScenario.GetExpectedMessages();
            foreach (var message in _expectedMessages)
            {
                _messageBuilderServiceFake.Setup(x => x.BuildMessage(It.Is<string>(x => x == _testScenario.MessageTemplate),
                                                                 It.Is<string>(x => x == "Happy Birthday!"),
                                                                 It.Is<Person>(x => x.FirstName == message.Recipient.FirstName)))
                                      .Returns(message);
            }

            _autoMocker.Use(_personRepositoryFake);
            _autoMocker.Use(_messageTemplateRepositoryFake);
            _autoMocker.Use(_messageBuilderServiceFake);
            _autoMocker.Use(_emailServiceFake);

            _useCase = _autoMocker.CreateInstance<SendGreetingsUseCase>();
        }

        [Fact]
        public async void Should_send_greetings_by_email_succesfully_given_a_date_which_contains_a_birthday_person()
        {
            // Arrange
            var request = new SendGreetingsRequest()
            {
                TodayDate = _testScenario.TodayDate,
            };

            // Act
            var response = await _useCase.SendGreetings(request);

            // Assert
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Greetings has sent succesfully to the birthdays of the month!");

            foreach (var expectedMessage in _testScenario.GetExpectedMessages())
            {
                _emailServiceFake.Verify(x => x.SendMessage(
                                            It.Is<Message>(message => message.Equals(expectedMessage))),
                                            Times.Exactly(1));
            }
        }

        [Fact]
        public async void Should_not_send_greetings_by_email_given_a_date_which_not_contains_a_birthday_person()
        {
            // Arrange
            var request = new SendGreetingsRequest()
            {
                TodayDate = DateTime.Today,
            };

            // Act
            var response = await _useCase.SendGreetings(request);

            // Assert
            response.Success.Should().BeFalse();
            response.Message.Should().Be("There are no birthdays in the month");

            _emailServiceFake.Verify(x => x.SendMessage(
                It.IsAny<Message>()),
                Times.Never);
        }
    }
}