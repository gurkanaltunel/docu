using System.Collections.Generic;
using DocumentServices.Models;

namespace Web.Models
{
    public class AddFileModel
    {
        public IList<Profile> Profiles { get; set; }

        public AddFileModel()
        {
            Profiles = new List<Profile>();
        }
    }
}