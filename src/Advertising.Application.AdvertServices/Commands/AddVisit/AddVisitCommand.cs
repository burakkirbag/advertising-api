using Advertising.Application.Commands;
using System;

namespace Advertising.Adverts.Commands.AddVisit
{
    public class AddVisitCommand : CommandBase
    {
        public int AdvertId { get; }
        public string VisitorIpAddress { get; }
        public DateTime Date { get; }

        public AddVisitCommand(int advertId, string visitorIpAddress, DateTime date)
        {
            AdvertId = advertId;
            VisitorIpAddress = visitorIpAddress;
            Date = date;
        }
    }
}
