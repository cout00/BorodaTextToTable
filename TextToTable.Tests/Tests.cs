using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using System.IO;
using TextToXelopesTable;

namespace TextToTable.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ParserTest()
        {
            var filereader = MockRepository.GenerateStub<IFileReader>();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"81.txt");
            filereader.Expect(r => r.GetStream()).Return(new StreamReader(path));
            StringParser sp = new StringParser(filereader);
        }
    }
}
