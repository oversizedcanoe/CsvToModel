using CsvToModel.Interface;

namespace CsvToModel.Model
{
    public class CSVModelerOptions : ICSVModelerOptions
    {
        public string Delimeter { get; set; } = ",";

        public bool UseNewConstraint { get; set; } = false;

        public bool SkipSecondRow { get; set; } = false;

        public List<ModelPropertySpecification> PropertySpecifications { get; set; } = new List<ModelPropertySpecification>();

        public bool IgnoreUnmatchedCsvColumns { get; set; } = false;

        public List<string> CsvColumnsToSkip { get; set; } = new List<string>();
    }
}
