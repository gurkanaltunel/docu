using SharpShell.Interop;

namespace FileWatcher
{
    internal abstract class SyncInformationBuilder
    {
        private readonly string _path;
        private readonly FILE_ATTRIBUTE _attributes;

        protected SyncInformationBuilder(string path, FILE_ATTRIBUTE attributes)
        {
            _path = path;
            _attributes = attributes;
        }

        public abstract SyncInformation BuildInformation(string path, FILE_ATTRIBUTE attributes);
    }
}