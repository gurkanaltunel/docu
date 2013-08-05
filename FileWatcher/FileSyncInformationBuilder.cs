using SharpShell.Interop;

namespace FileWatcher
{
    internal class FileSyncInformationBuilder : SyncInformationBuilder
    {
        public FileSyncInformationBuilder(string path, FILE_ATTRIBUTE attributes)
            : base(path, attributes)
        { }

        public override SyncInformation BuildInformation(string path, FILE_ATTRIBUTE attributes)
        {
            return new FileSyncInformation
                       {
                           Name = path
                       };
        }
    }

    internal class FileSyncInformation : SyncInformation
    {
        
    }
}