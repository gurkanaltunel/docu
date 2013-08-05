using System.Collections.Generic;

namespace DocumentServices.Models
{
    public class FolderInformation
    {
        public int FolderId { get; set; }

        public IList<Folder> Folders { get; set; }

        public IList<File> Files { get; set; }

        public int ParentFolder { get; set; }

        public string FolderName { get; set; }

        public string CurrentPath { get; set; }
    }
}