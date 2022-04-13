using System;
using System.Linq;
using System.Xml.Linq;

namespace CSVConverter.Business.Adapter
{
    public class CSVToXmlAdapter : FileAdapterBase
    {
        public override string Convert(string content)
        {
            string[] csvContentLines = content.Split(
                                new[] { Environment.NewLine },
                                StringSplitOptions.None
                            );


            XDocument xDocument = GetXml(
                        header: csvContentLines.First() ,
                        csvLines: csvContentLines.Skip(1).ToArray() 
            );

            return xDocument.ToString();

        }

        private static XDocument GetXml(string header, string[] csvLines, char delimiter = ',', char objectDelimiter = '_')
        {
            if (!csvLines.Any())
            {
                return new XDocument();
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

            var xDocument = new XDocument();
            var xPrincipalRoot = new XElement("root");

            foreach (var line in csvLines)
            {
                var csvParts = line.Split(delimiter);

                if (csvParts.Count() != headerParts.Count())
                {
                    throw new InvalidOperationException($"The columns in CSV line '{line}' does not match the header '{header}'");
                }

                xPrincipalRoot.Add(GetXmlElement(headerParts, csvParts));
            }

            xDocument.Add(xPrincipalRoot);

            return xDocument;
        }

        private static XElement GetXmlElement(string[] headerParts, string[] rowParts, char objectDelimiter = '_')
        {
            var xElementRoot = new XElement("element");

            for (var i = 0; i < headerParts.Count(); i++)
            {
                if (headerParts[i].Contains(objectDelimiter))
                {
                    string nextObjectName = headerParts[i].Split(objectDelimiter)[0];

                    var elementNextObject = new XElement(nextObjectName);

                    for (int n = i; n < headerParts.Count(); n++)
                    {
                        /*Verify if is still have an object*/
                        if  (!headerParts[n].Contains(objectDelimiter)) 
                            break;

                        /*Verify if is the same object*/
                        if  (!nextObjectName.Equals(headerParts[n].Split(objectDelimiter)[0])) 
                            break;

                        /*Add the property value for the nexted object*/
                        elementNextObject.Add(
                                new XElement(headerParts[n].Split(objectDelimiter)[1], 
                                             rowParts[n])
                            );

                        /*Setting the counting i to last position*/
                        i=n;
                    }

                    /*Adding the next object to principal object*/
                    xElementRoot.Add(new XElement(nextObjectName, elementNextObject));

                }
                else
                {
                    xElementRoot.Add(new XElement(headerParts[i], rowParts[i]));
                }
            }

           return xElementRoot;
        }


    }

}