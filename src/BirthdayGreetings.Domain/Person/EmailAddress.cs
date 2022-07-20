namespace BirthdayGreetings.Domain
{
    public class EmailAddress
    {
        public EmailAddress(string emailAddress)
        {
            Address = emailAddress;
        }

        public string Address { get; }

        public override bool Equals(object? obj)
        {
            return obj is EmailAddress address &&
                   Address == address.Address;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Address);
        }
    }
}
