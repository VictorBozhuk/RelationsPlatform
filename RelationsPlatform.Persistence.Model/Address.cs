using System;

namespace RelationsPlatform.Persistence.Model
{
    public class Address
    {
        public Guid Id { get; set; }
        public Guid ContactId { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string NumberOfHouse { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
