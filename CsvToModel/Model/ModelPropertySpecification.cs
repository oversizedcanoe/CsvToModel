namespace CsvToModel.Model
{
    /// <summary>
    /// This class contains pairs of model properties with their associated CSV column title.
    /// </summary>
    public class ModelPropertySpecification
    {
        /// <summary>
        /// The model's property name.
        /// </summary>
        public string ModelPropertyName { get; set; }
    
        /// <summary>
        /// The title of the column associated to ModelPropertyName in the CSV.
        /// </summary>
        public string CsvColumnTitle { get; set; }

        /// <summary>
        /// Creates a ModelPropertySpecification.
        /// </summary>
        /// <param name="modelPropertyName">The model's property name.</param>
        /// <param name="csvColumnTitle"> The title of the column associated to ModelPropertyName in the CSV. </param>
        public ModelPropertySpecification(string modelPropertyName, string csvColumnTitle)
        {
            this.ModelPropertyName = modelPropertyName;
            this.CsvColumnTitle = csvColumnTitle;
        }

        public override string ToString()
        {
            return $"{nameof(CsvColumnTitle)}={CsvColumnTitle}, {nameof(ModelPropertyName)}={ModelPropertyName}";
        }
    }
}
