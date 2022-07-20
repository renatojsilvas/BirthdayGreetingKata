using BirthdayGreetings.Domain;

namespace BirthdayGreetings.Infrastructure
{
    public class FileMessageTemplateRepository : IMessageTemplateRepository
    {
        private readonly string _filePath;

        public FileMessageTemplateRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<string> GetMessageTemplate()
        {
            string? message;

            try
            {
                message = await File.ReadAllTextAsync(_filePath);

                if (!message.Contains("{0}"))
                    throw new InfrastructureException($"{_filePath} is malformed");
            }
            catch (FileNotFoundException)
            {
                throw new InfrastructureException($"{_filePath} file does not exist");
            }

            return message;
        }
    }
}
