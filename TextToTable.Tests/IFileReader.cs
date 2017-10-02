using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToTable.Tests
{
    public interface IFileReader
    {
        StreamReader GetStream();
    }
}
