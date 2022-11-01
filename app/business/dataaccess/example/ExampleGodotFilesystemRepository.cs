using app.business.dataaccess.common.repository;
using app.business.dataaccess.example.dto;

namespace app.business.dataaccess.example
{
    internal class ExampleGodotFilesystemRepository : AbstractReadWriteRepository<ExampleSerializable>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mock">If true, a mocked data source will be used. Otherwise a production data source will be used. </param>
        public ExampleGodotFilesystemRepository(bool mock = false)
        {
            if (mock)
                this.dataSource = new ExampleGodotFilesystemMockDataSource();
            else
                this.dataSource = new ExampleGodotFilesystemDataSource();
        }
    }
}
