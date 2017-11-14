using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToXelopesTable
{
    public class FileReader : IFileReader
    {
        string path;
        public FileReader(string directory)
        {
            path = directory;
        }

        public StreamReader GetStream()
        {
            StreamReader streamReader = new StreamReader(path);
            return streamReader;
        }

        
    }
}
