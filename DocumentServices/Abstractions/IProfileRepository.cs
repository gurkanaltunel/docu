using System.Collections.Generic;
using DocumentServices.Models;

namespace DocumentServices.Abstractions
{
    public interface IProfileRepository
    {
        IList<Profile> GetProfileForUser(int id);
    }
}