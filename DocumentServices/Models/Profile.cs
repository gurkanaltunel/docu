using ServiceStack.DataAnnotations;

namespace DocumentServices.Models
{
    public class Profile
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}