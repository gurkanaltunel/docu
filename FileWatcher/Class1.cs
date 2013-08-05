using System;
using System.Drawing;
using SharpShell.Interop;
using SharpShell.SharpIconOverlayHandler;

namespace FileWatcher
{
    public abstract class IconOverlayHandlerBase : SharpIconOverlayHandler
    {
        protected override int GetPriority()
        {
            return 10;
        }

        protected override bool CanShowOverlay(string path, FILE_ATTRIBUTE attributes)
        {
            try
            {
                var info = GetSyncInformation(path, attributes);
                return CanShowOverlay(info);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static SyncInformation GetSyncInformation(string path, FILE_ATTRIBUTE attributes)
        {
            var builder = InformationBuilderFactory.GetInformationBuilder(path, attributes);
            return builder.BuildInformation(path, attributes);
        }

        protected abstract bool CanShowOverlay(SyncInformation info);

        protected abstract override Icon GetOverlayIcon();
    }
}