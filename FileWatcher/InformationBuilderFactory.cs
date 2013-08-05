using System;
using SharpShell.Interop;

namespace FileWatcher
{
    internal class InformationBuilderFactory
    {
        public static SyncInformationBuilder GetInformationBuilder(string path, FILE_ATTRIBUTE attributes)
        {
            switch (attributes)
            {
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_ARCHIVE:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_COMPRESSED:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_DEVICE:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_ENCRYPTED:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_HIDDEN:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_INTEGRITY_STREAM:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_NORMAL:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_NOT_CONTENT_INDEXED:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_NO_SCRUB_DATA:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_OFFLINE:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_READONLY:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_REPARSE_POINT:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_SPARSE_FILE:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_SYSTEM:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_TEMPORARY:
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_VIRTUAL:
                    return new FileSyncInformationBuilder(path, attributes);
                case FILE_ATTRIBUTE.FILE_ATTRIBUTE_DIRECTORY:
                    return new FolderSyncInformationBuilder(path, attributes);
                default:
                    throw new ArgumentOutOfRangeException("attributes");
            }
        }
    }
}