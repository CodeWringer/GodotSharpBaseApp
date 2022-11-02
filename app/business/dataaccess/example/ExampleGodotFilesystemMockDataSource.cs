using app.business.dataaccess.common.datasource;
using app.business.dataaccess.example.dto;
using app.business.model;
using System.Collections.Generic;

namespace app.business.dataaccess.example
{
    internal class ExampleGodotFileSystemMockDataSource : AbstractReadWriteDataSource<ExampleSerializable>
    {
        private ExampleSerializable data = new ExampleSerializable()
        {
            items = new List<AnItem>()
            {
                new AnItem("Abc1"),
                new AnItem("Def2"),
                new AnItem("Ghi3"),
            }
        };

        public override ExampleSerializable Read()
        {
            return data;
        }

        public override void Write(ExampleSerializable toWrite)
        {
            data = toWrite;
        }
    }
}
