using System;

namespace AkademiPlusSignalR2.DAL.Context
{
    public enum ECity
    {
        İstanbul = 1,
        Bursa = 2,
        Ankara = 3,
        Adana = 4,
        İzmir = 5
    }
    public class Visitor
    {
        public int VisitorID { get; set; }
        public ECity City { get; set; }
        public int CityVisitor { get; set; }
        public DateTime VisitDate { get; set; }

    }
}
