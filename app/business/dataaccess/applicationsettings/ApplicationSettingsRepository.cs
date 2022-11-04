using app.business.dataaccess.common.datasource;
using app.business.dataaccess.common.repository;
using app.business.state;

namespace app.business.dataaccess.applicationsettings
{
    internal class ApplicationSettingsRepository : AbstractReadWriteRepository<ApplicationSettings>
    {
        public ApplicationSettingsRepository()
        {
            // TODO: Proper data source selection
            this.dataSource = new GodotFileSystemDataSource<ApplicationSettings>(ApplicationSettings.PATH_APP_SETTINGS);
        }
    }
}
