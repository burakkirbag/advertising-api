using Advertising.Domain.Entities;
using System;

namespace Advertising.Adverts
{
    public class Advert : EntityBase
    {
        public int MemberId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int TownId { get; set; }
        public string TownName { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int KM { get; set; }
        public string Color { get; set; }
        public string Gear { get; set; }
        public string Fuel { get; set; }
        public string FirstPhoto { get; set; }
        public string SecondPhoto { get; set; }
        public string UserInfo { get; set; }
        public string UserPhone { get; set; }
        public string Text { get; set; }
    }
}
