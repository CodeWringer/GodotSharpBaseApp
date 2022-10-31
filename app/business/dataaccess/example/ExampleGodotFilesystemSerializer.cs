namespace app.business.dataaccess.example
{
    internal class ExampleGodotFilesystemSerializer : GodotFilesystemDataSource<ExampleSerializable>
    {
        private string _filePath = USER_PREFIX + "example.json";
        public override string FilePath { get => _filePath; protected set => _filePath = value; }
    }
}
