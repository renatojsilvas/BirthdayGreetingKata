using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayGreetings.Domain.UseCases
{
    public class SendGreetingsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
