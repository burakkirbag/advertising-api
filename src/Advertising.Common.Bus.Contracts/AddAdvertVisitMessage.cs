using System;

namespace Advertising.Bus.Contracts.Messages
{
    public class AddAdvertVisitMessage : BusMessageBase
    {
        public int AdvertId { get; }
        public string VisitorIpAddress { get; }
        public DateTime Date { get; }

        public AddAdvertVisitMessage(int advertId, string visitorIpAddress, DateTime date)
        {
            AdvertId = advertId;
            VisitorIpAddress = visitorIpAddress;
            Date = date;
        }
    }
}
