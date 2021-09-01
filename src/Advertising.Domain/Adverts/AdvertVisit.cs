using Advertising.Domain.Entities;
using System;

namespace Advertising.Adverts
{
    public class AdvertVisit : EntityBase
    {
        public int AdvertId { get; set; }
        public string IpAddress { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
