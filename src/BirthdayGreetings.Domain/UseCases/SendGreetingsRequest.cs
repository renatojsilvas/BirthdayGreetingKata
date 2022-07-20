using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayGreetings.Domain.UseCases
{
    public class SendGreetingsRequest
    {
        public DateTime TodayDate { get; set; }
    }
}
