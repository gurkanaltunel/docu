using System.Collections.Generic;
using System.Linq;
using DocumentServices.Models;

namespace Web.Models
{
    public class OpenFileViewModel
    {
        public File File { get; set; }

        public IList<FileVersion> FileVersions { get; set; }

        public FileVersion LastFileVersion
        {
            get { return FileVersions.OrderBy(version => version.VersionNumber).Last(); }
        }
    }
}