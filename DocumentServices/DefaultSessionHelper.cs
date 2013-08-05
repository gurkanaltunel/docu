using System;
using System.Web;
using DocumentServices.Abstractions;
using DocumentServices.Models;

namespace DocumentServices
{
    public class DefaultSessionHelper : ISessionHelper
    {
        private File _currentFile;

        public UserContext CurrentUser
        {
            get { return Get<UserContext>("context"); }
            private set { Set("context", value); }
        }

        public FolderInformation CurrentFolder
        {
            get { return Get<FolderInformation>("currentfolder"); }
            set { Set("currentfolder", value); }
        }

        public string CurrentFolderPath { get; set; }

        public File CurrentFile
        {
            get { return Get<File>("currentfile"); }
            set { Set("currentfile", value); }
        }

        public void CreateLogin(UserContext context)
        {
            context.LoginTime = DateTime.Now;
            CurrentUser = context;
        }

        private static TObjectType Get<TObjectType>(string key)
        {
            return (TObjectType)HttpContext.Current.Session[key];
        }

        private static void Set(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }
}