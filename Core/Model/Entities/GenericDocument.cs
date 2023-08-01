using System;

namespace Core.Model.Entities
{
    public class GenericDocument
    {
        public String Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime DeleteTime { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
// Search by title bu kısma alınabilir gibi