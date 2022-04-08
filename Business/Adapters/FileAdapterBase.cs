using System;
using CSVConverter.Business.Interfaces;

namespace CSVConverter.Business.Adapter
{
    public abstract class FileAdapterBase : ICSVToFIleConvert
    {
        public abstract string Convert(string csvContent);
        
    }

}