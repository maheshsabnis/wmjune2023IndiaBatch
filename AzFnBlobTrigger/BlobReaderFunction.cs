using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;


namespace AzFnBlobTrigger
{
    public class BlobReaderFunction
    {
        [FunctionName("CsvReader")]                                          // UseDevelopmentStorage=true 
        public async Task Run([BlobTrigger("mycontainer/{name}", Connection = "blobconnstring")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            // 1. Check the Length of the blob more than 9 butes

            if (myBlob.Length > 0)
            {
                using (var reader = new StreamReader(myBlob))
                {
                    // 1. STart Reading the FIrts Line, this will be a header column 
                    var headerRow = await reader.ReadLineAsync();
                    // 2. set the Start line number
                    var startLineNumber = 1;

                    // 3. Start from the Next Line
                    var currentLine = await reader.ReadLineAsync();
                    while (currentLine != null) 
                    {
                        // 3.a.Continue reading each line
                        currentLine = await reader.ReadLineAsync();
                        // 3.b. Add each record in the table  
                        await InsertRecordToTable(currentLine, log);
                        // Move to Next Line
                        startLineNumber++;
                    }
                    log.LogInformation($"Data is Written into the Database");
                }
            }
            else
            {
                log.LogInformation($"The file size if 0 bytes");
            }
        }

        private async Task InsertRecordToTable(string curLine, ILogger log)
        {
            // make sute that the current Line is not a white space or null
            if (string.IsNullOrWhiteSpace(curLine))
            {
                log.LogInformation($"The Data is EMpty");
                return;
            }
            // Spilt the Current line by , this will return an array
            var column = curLine.Split(',');

            SqlConnection connection = new SqlConnection("Server=tcp:wmjune2023.database.windows.net,1433;Initial Catalog=MyCompany;Persist Security Info=False;User ID=MaheshAdmin;Password= P@ssw0rd_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "Insert into PeopleInfo (Name, City) Values (@Name,@City)";
            command.Parameters.AddWithValue("@Name", column[0]); // Name
            command.Parameters.AddWithValue("@City", column[1]); // City

            // execute the query
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
