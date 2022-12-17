namespace CsvToModelTester
{
    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsCurrentEmployee { get; set; }

        public override string ToString()
        {
            return $"#{Id}: {FirstName} {LastName}. Age: {Age}. Email: {Email}. Phone Number: {PhoneNumber}. Is Current Employee? {IsCurrentEmployee}.";
        }
    }
}
