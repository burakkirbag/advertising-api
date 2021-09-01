namespace Advertising.Adverts.Dto
{
    public class AdvertDetailDto : AdvertDto
    {
        public string MemberId { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string TownId { get; set; }
        public string ModelId { get; set; }
        public string CategoryId { get; set; }
        public string SecondPhoto { get; set; }
        public string UserInfo { get; set; }
        public string UserPhone { get; set; }
        public string Text { get; set; }
    }
}
