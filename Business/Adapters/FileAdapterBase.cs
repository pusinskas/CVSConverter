using System;
using CSVConverter.Business.Interfaces;

namespace CSVConverter.Business.Adapter
{
    public abstract class FileAdapterBase : ICSVConverter
    {
        public abstract string Convert(string content);
        
    }

}