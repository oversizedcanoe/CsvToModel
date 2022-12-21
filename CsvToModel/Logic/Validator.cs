using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CsvToModel.Logic
{
    public static class Validator
    {
        // TODO set proper other extensions
        private static string[] validExtensions = new string[] { ".csv", ".xslx" };

        public static void ValidateInputFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException($"Parameter '{nameof(fileName)}' is required.");
            }

            FileInfo fileInfo;

            try
            {
                // Try this on a file that does not exist.
                fileInfo = new FileInfo(fileName);
            }
            catch (SecurityException ex)
            {
                throw new SecurityException($"{nameof(CsvToModel)} does not have permission to file '{fileName}'.", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new SecurityException($"{nameof(CsvToModel)} does not have access to file '{fileName}' Please ensure the file is not readonly or currently in use.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An unknown exception has occurred while trying to access '{fileName}'. The exception is: {ex.Message}.", ex);
            }

            if (validExtensions.Contains(fileInfo.Extension) == false)
            {
                throw new ArgumentException($"File '{fileInfo.Name}' has an invalid extension Accepted extensions: {string.Join(", ", validExtensions)}.");
            }

        }
    }
}
