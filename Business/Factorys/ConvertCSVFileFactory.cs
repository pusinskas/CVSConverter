using CSVConverter.Business.Models;
using CSVConverter.Business.Interfaces;
using CSVConverter.Business.Adapter;

namespace CSVConverter.Business.Factorys
{
    public class ConvertCSVFileFactory
    {
        public ICSVToFIleConvert getConverter(eFileType from, eFileType to)
        {
            switch (from)
            {
                case eFileType.CSV:

                    switch (to)
                    {
                        case eFileType.JSON : return new CSVToJsonAdapter();
                        case eFileType.XML : return new CSVToXmlAdapter();

                        default: throw new System.NotSupportedException("File to " + to.ToString() + "is not supported") ;
                    }

                default: throw new System.NotSupportedException("File from must to be CSV");
            }

        }
    }
}