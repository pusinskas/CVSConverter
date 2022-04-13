using System;
using CSVConverter.Business.Factorys;
using CSVConverter.Business.Interfaces;
using CSVConverter.Business.Models;

namespace CSVConverter.UI.App
{
    class Program
    {
        static void Main(string[] args)
        {
            string csvContent = "name,address_line1,address_line2,employee_name,employee_address,age";
            csvContent += Environment.NewLine;
            csvContent += "Dave,Street,Town,Company,Company Street,30";
            csvContent += Environment.NewLine;
            csvContent += "John,Street 2,Town 2,Company 1,Company Street 2, 35";


            var convertCSVFileFactory = new ConvertCSVFileFactory();

            ICSVConverter fileConvertToJson =  convertCSVFileFactory.getConverter(eFileType.CSV, eFileType.JSON );

            Console.Write(fileConvertToJson.Convert(csvContent));

            
            ICSVConverter fileConvertToXml =  convertCSVFileFactory.getConverter(eFileType.CSV, eFileType.XML );

            Console.Write(fileConvertToXml.Convert(csvContent));


            /*string json = fileConvertToJson.Convert(csvContent);

            Console.Write(json);

            ICSVConverter fileConvertJsonToCSV =  convertCSVFileFactory.getConverter(eFileType.JSON, eFileType.CSV );

            Console.Write(fileConvertJsonToCSV.Convert(json));*/


            
        }
    }
    
}
