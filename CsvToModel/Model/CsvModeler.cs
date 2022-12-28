using CsvToModel.Interface;
using CsvToModel.Logic;
using System.Reflection;

namespace CsvToModel.Model
{
    // MISC TODO
    // Method/class summaries
    // What to do when a cell is empty or I can't cast to the specified type?
    // Potentially add 2nd implementation where 'where T : class, new()' is used -- should test speed
    public class CsvModeler
    {
        private ICSVModelerOptions options;

        private Dictionary<int, PropertyInfo> propertyIndices = new Dictionary<int, PropertyInfo>();

        // TODO implement an interface
        public CsvModeler()
        {
            this.options = new CSVModelerOptions();
        }

        public CsvModeler(ICSVModelerOptions options)
        {
            this.options = options;
        }

        public List<T> ParseCsv<T>(string fileName) where T : class
        {
            // Validate input fileName/file.
            Validator.ValidateInputFile(fileName);

            // Get the first line of the CSV, which is the property names.
            using FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
            using var reader = new StreamReader(fileStream);

            string? headerLine = reader.ReadLine();

            if (string.IsNullOrWhiteSpace(headerLine))
            {
                throw new ArgumentException($"File '{fileName}' has no content.");
            }

            // Get the type and properties for T.
            Type modelType = typeof(T);
            PropertyInfo[] propertyInfos = modelType.GetProperties();

            // Get the headers of the CSV, and loop through them to find the matching PropertyInfo. 
            // This will be put into a Dictionary for quick access to the property given the current column index.
            List<string> headers = headerLine.Split(this.options.Delimeter).ToList();

            for (int i = 0; i < headers.Count; i++)
            {
                string propertyName = headers[i];
                PropertyInfo propertyInfo = this.GetProperty(propertyName, propertyInfos);

                this.propertyIndices.Add(i, propertyInfo);
            }

            // Initialize property value array which we will set to the current line.
            string[] propertyValues = new string[headers.Count];

            // This will be our return list.
            List<T> result = new List<T>();

            if (options.SkipSecondRow)
            {
                reader.ReadLine();
            }

            // Iterate each line, setting the found property values to the current T.
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                propertyValues = line.Split(this.options.Delimeter);

                // Other option: instantiate as object? then (T)object at the end?
                // Or require that T has a public parameter-free constructor, and update the method call to be "where T: class, new()"
                T newModelInstance = (T)Activator.CreateInstance(modelType);

                // Loop through properties of this object. Set each property in the new object to the value read on this line.
                for (int i = 0; i < this.propertyIndices.Count; i++)
                {
                    var propertyToSet = this.propertyIndices[i];
                    var propertyType = propertyToSet.PropertyType;

                    propertyToSet.SetValue(newModelInstance, Convert.ChangeType(propertyValues[i], propertyType));
                }

                result.Add(newModelInstance);
            }

            return result;
        }

        private PropertyInfo GetProperty(string csvColumnName, PropertyInfo[] propertyInfos)
        {
            string propertyName = csvColumnName;

            var propertySpecification = this.options.PropertySpecifications.FirstOrDefault(ps=>ps.CsvColumnTitle == csvColumnName);

            if (propertySpecification != null)
            {
                propertyName = propertySpecification.ModelPropertyName;
            }
            
            PropertyInfo? propertyInfo = propertyInfos.Where(pi => pi.Name == propertyName).FirstOrDefault();

            if (propertyInfo == null)
            {
                string additionalContext = string.Empty;
                
                if(propertySpecification != null)
                {
                    additionalContext = $" using PropertySpecification {propertySpecification}";
                }
                
                throw new Exception($"Unable to find property for CSV column '{csvColumnName}'{additionalContext}.");
            }

            return propertyInfo;
        }
    }
}