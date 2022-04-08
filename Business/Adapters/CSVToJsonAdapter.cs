using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace CSVConverter.Business.Adapter
{
    public class CSVToJsonAdapter : FileAdapterBase
    {
        public override string Convert(string csvContent)
        {
            string[] csvContentLines = csvContent.Split(
                                new[] { Environment.NewLine },
                                StringSplitOptions.None
                            );

            JArray jsonArray = GetJsonArray(
                        header: csvContentLines.First() ,
                        csvLines: csvContentLines.Skip(1).ToArray() 
            );

            return jsonArray.ToString();
        }

        private static JArray GetJsonArray(string header, string[] csvLines, char delimiter = ',', char objectDelimiter = '_')
        {
            if (!csvLines.Any())
            {
                return new JArray();
            }

            if (string.IsNullOrEmpty(header))
            {
                throw new ArgumentException(paramName: nameof(header), message: "Cannot be null or empty.");
            }

            var headerParts = header.Split(delimiter);

            if (headerParts.GroupBy(part => part).Where(partGroup => partGroup.Count() > 1).Any())
            {
                throw new InvalidOperationException($"There are repeating headers in '{header}'");
            }

            var jsonArray = new JArray();

            foreach (var line in csvLines)
            {
                var csvParts = line.Split(delimiter);

                if (csvParts.Count() != headerParts.Count())
                {
                    throw new InvalidOperationException($"The columns in CSV line '{line}' does not match the header '{header}'");
                }

                jsonArray.Add(GetJObject(headerParts, csvParts));
            }

            return jsonArray;
        }

        private static JObject GetJObject(string[] headerParts, string[] rowParts, char objectDelimiter = '_')
        {
            var jsonObject = new JObject();

            for (var i = 0; i < headerParts.Count(); i++)
            {
                if (headerParts[i].Contains(objectDelimiter))
                {
                    string nextObjectName = headerParts[i].Split(objectDelimiter)[0];

                    var jsonNextObject = new JObject();

                    for (int n = i; n < headerParts.Count(); n++)
                    {
                        /*Verify if is still have an object*/
                        if  (!headerParts[n].Contains(objectDelimiter)) 
                            break;

                        /*Verify if is the same object*/
                        if  (!nextObjectName.Equals(headerParts[n].Split(objectDelimiter)[0])) 
                            break;

                        /*Add the property value for the nexted object*/
                        jsonNextObject.Add(headerParts[n].Split(objectDelimiter)[1], rowParts[n]);

                        /*Setting the counting i to last position*/
                        i=n;
                    }

                    /*Adding the next object to principal object*/
                    jsonObject.Add(new JProperty(nextObjectName, jsonNextObject));

                }
                else
                {
                    jsonObject.Add(headerParts[i], rowParts[i]);
                }
            }

           return jsonObject;
        }



    }

}