using System.Collections.Generic;
using System.IO;
using System.Text;
using TechJobs.Models;

namespace TechJobs.Data
{
    public class JobDataImporter
    {
        private static bool IsDataLoaded = false;

        /**
         * Load and parse data from job_data.csv
         */
        internal static void LoadData(JobData jobData)
        {

            if (IsDataLoaded)
            {
                return;
            }

            var rows = new List<string[]>();

            using (var reader = File.OpenText("Data/job_data.csv"))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    var rowArray = CSVRowToStringArray(line);
                    if (rowArray.Length > 0)
                    {
                        rows.Add(rowArray);
                    }
                }
            }

            var headers = rows[0];
            rows.Remove(headers);

            /**
             * Parse each row array into a Job object.
             * Assumes CSV column ordering: 
             *      name,employer,location,position type,core competency
             */
            foreach (var row in rows)
            {
                var employer = jobData.Employers.AddUnique(row[1]);
                var location = jobData.Locations.AddUnique(row[2]);
                var positionType = jobData.PositionTypes.AddUnique(row[3]);
                var coreCompetency = jobData.CoreCompetencies.AddUnique(row[4]);

                var newJob = new Job
                {
                    Name = row[0],
                    Employer = employer,
                    Location = location,
                    PositionType = positionType,
                    CoreCompetency = coreCompetency
                };
                jobData.Jobs.Add(newJob);
            }

            IsDataLoaded = true;
        }


        /**
         * Parse a single line of a CSV file into a string array
         */
        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',', char stringSeparator = '\"')
        {
            var isBetweenQuotes = false;
            var valueBuilder = new StringBuilder();
            var rowValues = new List<string>();

            // Loop through the row string one char at a time
            foreach (char c in row.ToCharArray())
            {
                if ((c == fieldSeparator && !isBetweenQuotes))
                {
                    rowValues.Add(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                else
                {
                    if (c == stringSeparator)
                    {
                        isBetweenQuotes = !isBetweenQuotes;
                    }
                    else
                    {
                        valueBuilder.Append(c);
                    }
                }
            }

            // Add the final value
            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();
        }
    }
}
