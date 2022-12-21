using System.Reflection;

namespace CsvToModel.Model
{
    // MISC TODO
    // Have a ICsvModelerConfigurationOptions class to pass options into.
    // Method/class summaries
    // Allow user to specify differences between column names and property names--ie "Property PhoneNumber is in the CSV as 'Phone'"
    // What to do when a cell is empty or I can't cast to the specified type?
    // Potentially add 2nd implementation where 'where T : class, new()' is used -- should test speed

    public class CsvModeler
    {
        // TODO implement an interface
        public CsvModeler()
        {

        }

        public List<T> ParseCsv<T>(string fileName) where T : class
        {
            
            // TODO validate read access and use an object locker when reading file

            // Loop through the first line of the CSV. This will get us the property names.
            using StreamReader reader = fileInfo.OpenText();

            string? headerLine = reader.ReadLine();

            if (string.IsNullOrWhiteSpace(headerLine))
            {
                throw new ArgumentException($"File '{fileInfo.Name}' has no content.");
            }

            List<T> result = new List<T>();

            Type modelType = typeof(T);

            PropertyInfo[] propertyInfos = modelType.GetProperties();

            List<string> headers = headerLine.Split(',').ToList();
            
            string[] propertyValues = new string[headers.Count];

            string? line;
            
            while ((line = reader.ReadLine()) != null)
            {
                propertyValues = line.Split(',');

                // Other option: instantiate as object? then (T)object at the end?
                // Or require that T has a public parameter-free constructor, and update the method call to be "where T: class, new()"
                T newModelInstance = (T)Activator.CreateInstance(modelType);

                // Loop through properties of this object. Set each property in the new object to the value read on this line.
                // TODO can this looping/index finding be done outside of the loop somehow?
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    var currentProperty = propertyInfos[i];
                    // TODO is this case insensitive? It should be. Or configuration?
                    int propertyIndexInCsv = headers.IndexOf(currentProperty.Name);
                    var currentPropertyType = currentProperty.PropertyType;
                    currentProperty.SetValue(newModelInstance, Convert.ChangeType(propertyValues[i], currentPropertyType));
                }

                result.Add(newModelInstance);
            }

            return result;
        }
    }
}