using app.business.dataaccess.common.datasource;
using app.business.dataaccess.example.dto;
using app.business.dataaccess.util;

namespace app.business.dataaccess.example
{
    internal class ExampleGodotFileSystemDataSource : GodotFileSystemDataSource<ExampleSerializable>
    {
        public ExampleGodotFileSystemDataSource()
            : base(PathUtility.USER_DIR_PATH_PREFIX + "example.json")
        {
        }
    }
}
