using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToXelopesTable
{
    class FileReaderCommon : IFileReader
    {
        List<string> inpList;

        public StreamReader GetStream()
        {
            StreamReader streamReader = null;
            foreach (var path in inpList)
            {
                FileReader fileReader = new FileReader(path);
                streamReader = Merge(streamReader, fileReader.GetStream());
            }
            return streamReader;
        }


        StreamReader Merge(StreamReader streamReader_F, StreamReader streamReader_S)
        {
            if (streamReader_S != null && streamReader_F != null)
            {
                byte[] firstByte = ConvertToByte(streamReader_F);
                byte[] secondByte = ConvertToByte(streamReader_S);

                byte[] result = new byte[firstByte.Length + secondByte.Length];
                firstByte.CopyTo(result, 0);
                secondByte.CopyTo(result, firstByte.Length);
                StreamReader reader = new StreamReader(new MemoryStream(result), System.Text.Encoding.ASCII, true);
                return reader;
            }
            else
            {
                if (streamReader_F != null)
                    return streamReader_F;
                if (streamReader_S != null)
                    return streamReader_S;
            }
            return null;
        }
        byte[] ConvertToByte(StreamReader _streamReader)
        {
            using (var streamReader = new MemoryStream())
            {
                _streamReader.BaseStream.CopyTo(streamReader);
                return streamReader.ToArray();
            }
        }

        public FileReaderCommon(List<string> inpList)
        {
            this.inpList = inpList;
        }
    }
}
