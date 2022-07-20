using BirthdayGreetings.Domain;
using BirthdayGreetings.Domain.UseCases;
using BirthdayGreetings.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayGreetings
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScoped<IPersonRepository>(x => new PersonRepositoryFile(@"Data\birthdayDates.csv"))
                    .AddScoped<IMessageTemplateRepository>(x => new FileMessageTemplateRepository(@"Data\template.txt"))
                    .AddScoped<IMessageBuilderService, MessageBuilderService>()
                    .AddScoped<IMessageService, EmailService>()
                    .AddScoped<SendGreetingsUseCase>();

            var serviceProvider = services.BuildServiceProvider();
            var sendGreetings = serviceProvider.GetRequiredService<SendGreetingsUseCase>();

            MainAsync(sendGreetings).Wait();
        }

        private static async Task MainAsync(SendGreetingsUseCase sendGreetings)
        {

            var request = new SendGreetingsRequest()
            {
                TodayDate = new DateTime(1982, 10, 01),
            };

            var response = await sendGreetings.SendGreetings(request);

            Console.WriteLine(response.Message);
        }
    }
}