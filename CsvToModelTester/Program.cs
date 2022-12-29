using CsvToModel.Interface;
using CsvToModel.Model;
using System.Reflection;

namespace CsvToModelTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string csvFileName = "people.csv";

            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string csvFilePath = Path.Combine(filePath, csvFileName);

            CsvModeler csvModeler = new CsvModeler(new CSVModelerOptions()
            {
                PropertySpecifications = new List<ModelPropertySpecification> { new ModelPropertySpecification("PhoneNumber", "Phone") },
                IgnoreUnmatchedCsvColumns = true
            });

            List<Person> people = csvModeler.ParseCsv<Person>(csvFilePath);

            foreach (var person in people)
            {
                Console.WriteLine(person.ToString());
            }

            Console.ReadKey();
        }
    }
}