using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayGreetings.Infrastructure
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string message)
            : base(message) { }
    }
}
