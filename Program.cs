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


            var convertCSVFileFactory = new ConvertCSVFileFactory();

            ICSVToFIleConvert fileConvertToJson =  convertCSVFileFactory.getConverter(eFileType.CSV, eFileType.JSON );

            Console.Write(fileConvertToJson.Convert(csvContent));

            
            ICSVToFIleConvert fileConvertToXml =  convertCSVFileFactory.getConverter(eFileType.CSV, eFileType.XML );

            Console.Write(fileConvertToXml.Convert(csvContent));

            
        }
    }
    
}
