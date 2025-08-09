using NPOI.OpenXmlFormats.Dml.Diagram;

namespace Tracking_Folder_Tool.Services
{
    public class CsvReaderService
    {
        public class CsvReader
        {
            /// <summary>
            /// Reads a CSV file and returns a list of objects.
            /// </summary>
            /// <param name="filePath">Path to the CSV file.</param>
            /// <returns>List of mapped objects.</returns>
            public List<CsvDataModel> ReadCsvFile(string filePath)
            {
                var result = new List<CsvDataModel>();

                try
                {
                    // Validate file existence
                    if (!File.Exists(filePath))
                    {
                        throw new FileNotFoundException("CSV file not found.", filePath);
                    }

                    // Read all lines from the CSV file
                    var lines = File.ReadAllLines(filePath);

                    if (lines.Length == 0)
                    {
                        throw new InvalidOperationException("CSV file is empty.");
                    }

                    // Assuming first line is the header
                    var headers = lines[0].Split(',').Select(h => h.Trim()).ToArray();

                    // Process each data row
                    for (int i = 1; i < lines.Length; i++)
                    {
                        var values = lines[i].Split(',').Select(v => v.Trim()).ToArray();

                        // Ensure the row has the expected number of columns
                        if (values.Length != headers.Length)
                        {
                            throw new InvalidOperationException($"Invalid data format at row {i + 1}. Expected {headers.Length} columns, found {values.Length}.");
                        }

                        // Map the row to the model
                        var model = new CsvDataModel
                        {
                            Id = int.Parse(values[0]), // Adjust based on your CSV structure
                            Name = values[1],
                            Age = int.Parse(values[2])
                        };

                        result.Add(model);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error reading CSV file.", ex);
                }
            }

            /// <summary>
            /// Reads a CSV file dynamically (when the structure is unknown).
            /// </summary>
            /// <param name="filePath">Path to the CSV file.</param>
            /// <returns>List of dictionaries where each dictionary represents a row.</returns>
            public List<Dictionary<string, string>> ReadCsvFileDynamic(string filePath)
            {
                var result = new List<Dictionary<string, string>>();

                try
                {
                    // Validate file existence
                    if (!File.Exists(filePath))
                    {
                        throw new FileNotFoundException("CSV file not found.", filePath);
                    }

                    // Read all lines from the CSV file
                    var lines = File.ReadAllLines(filePath);

                    if (lines.Length == 0)
                    {
                        throw new InvalidOperationException("CSV file is empty.");
                    }

                    // Get headers from the first line
                    var headers = lines[0].Split(',').Select(h => h.Trim()).ToArray();

                    // Process each data row
                    for (int i = 1; i < lines.Length; i++)
                    {
                        var values = lines[i].Split(',').Select(v => v.Trim()).ToArray();

                        // Ensure the row has the expected number of columns
                        if (values.Length != headers.Length)
                        {
                            throw new InvalidOperationException($"Invalid data format at row {i + 1}. Expected {headers.Length} columns, found {values.Length}.");
                        }

                        // Create a dictionary for the row
                        var row = new Dictionary<string, string>();
                        for (int j = 0; j < headers.Length; j++)
                        {
                            row[headers[j]] = values[j];
                        }

                        result.Add(row);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error reading CSV file dynamically.", ex);
                }
            }
        }

        public class CsvDataModel
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int Age { get; set; }
        }
    }
}
