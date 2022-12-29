using CsvToModel.Model;

namespace CsvToModel.Interface
{
    /// <summary>
    /// This class provides configuration options for the CsvModeler functionality.
    /// </summary>
    public interface ICSVModelerOptions
    {
        /// <summary>
        /// Specifies the delimeter to use when splitting rows. Default is a comma (',').
        /// </summary>
        public string Delimeter { get; set; }

        /// <summary>
        /// Indicates whether to use "new T()" when instantiating a new instance of the Model to create.
        /// This is only possible of T contains a public parameterless constructor. If this is false,
        /// (T)Activator.CreateInstance(typeof(T)) is used instead. Setting this to true can bring performance
        /// improvements (YET TO BE TESTED)
        /// Default value is false.
        /// </summary>
        public bool UseNewConstraint { get; set; }

        /// <summary>
        /// Indicates whether to skip the second row in a CSV. Default value is false.
        /// </summary>
        public bool SkipSecondRow { get; set; }

        /// <summary>
        /// Used to clarify which property in the model should be set via which column in the CSV.
        /// For example: PropertySpecifications.Add(new("PhoneNumber", "PhoneNum")),
        /// where "PhoneNumber" is the property name and "PhoneNum" is the CSV column title.
        /// </summary>
        public List<ModelPropertySpecification> PropertySpecifications { get; set; }

        /// <summary>
        /// Indicates whether to ignore CSV columns which do not have a corresponding property.
        /// To ignore specific columns, use the 'CsvColumnsToSkip' option.
        /// Default value is false.
        /// </summary>
        public bool IgnoreUnmatchedCsvColumns { get; set; }

        /// <summary>
        /// Contains all columns in the supplied CSV to not attempt to match to a property of the model.
        /// </summary>
        public List<string> CsvColumnsToSkip { get; set; }
    }
}
