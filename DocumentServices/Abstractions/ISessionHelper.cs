using DocumentServices.Models;

namespace DocumentServices.Abstractions
{
    public interface ISessionHelper
    {
        UserContext CurrentUser { get; }
        FolderInformation CurrentFolder { get; set; }
        string CurrentFolderPath { get; set; }
        File CurrentFile { get; set; }

        void CreateLogin(UserContext context);
    }
}