using CSVConverter.Business.Models;
using CSVConverter.Business.Interfaces;
using CSVConverter.Business.Adapter;

namespace CSVConverter.Business.Factorys
{
    public class ConvertCSVFileFactory
    {
        public ICSVConverter getConverter(eFileType from, eFileType to)
        {
            switch (from)
            {
                case eFileType.CSV:

                    switch (to)
                    {
                        case eFileType.JSON : return new CSVToJsonAdapter();
                        case eFileType.XML : return new CSVToXmlAdapter();

                        default: throw new System.NotSupportedException("File from CSV to " + to.ToString() + "is not supported") ;
                    }

                /*case eFileType.JSON:
                    switch(to)
                    {
                        case eFileType.CSV : return new JsonToCSVAdapter();

                        default: throw new System.NotSupportedException("File from JSON to " + to.ToString() + "is not supported") ;
                    }
                */
                default: throw new System.NotSupportedException("File from" + from.ToString() + "is not supported");
            }

        }
    }
}