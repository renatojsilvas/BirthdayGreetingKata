namespace BirthdayGreetings.Domain.UseCases
{
    public class SendGreetingsUseCase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMessageTemplateRepository _templateMessageRepository;
        private readonly IMessageBuilderService _messageBuilderService;
        private readonly IMessageService _messageService;

        public SendGreetingsUseCase(IPersonRepository personRepository,
            IMessageTemplateRepository templateMessageRepository,
            IMessageBuilderService messageBuilderService,
            IMessageService messageService)
        {
            _personRepository = personRepository;
            _templateMessageRepository = templateMessageRepository;
            _messageBuilderService = messageBuilderService;
            _messageService = messageService;
        }

        public async Task<SendGreetingsResponse> SendGreetings(SendGreetingsRequest request)
        {
            var birthdaysOfTheMonth = await _personRepository.GetBirthdaysByMonth(request.TodayDate.Year,
                                                                                  request.TodayDate.Month);

            if (birthdaysOfTheMonth.Count == 0)
            {
                return new SendGreetingsResponse()
                {
                    Success = false,
                    Message = "There are no birthdays in the month"
                };
            }

            var messageTemplate = await _templateMessageRepository.GetMessageTemplate();

            foreach (var birthday in birthdaysOfTheMonth)
            {
                var message = _messageBuilderService.BuildMessage(messageTemplate, "Happy Birthday!", birthday);
                await _messageService.SendMessage(message);
            }

            return new SendGreetingsResponse()
            {
                Success = true,
                Message = "Greetings has sent succesfully to the birthdays of the month!"
            };
        }
    }
}
