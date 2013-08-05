using System.Collections.Generic;
using DocumentServices.Models;

namespace Web.Models
{
    public class MainViewModel
    {
        public IList<Folder> Folders { get; set; }

        public FolderInformation CurrentFolder { get; set; }
    }
}