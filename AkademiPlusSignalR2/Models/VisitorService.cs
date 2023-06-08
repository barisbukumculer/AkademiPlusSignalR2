using AkademiPlusSignalR2.DAL.Context;
using AkademiPlusSignalR2.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AkademiPlusSignalR2.Models
{
    public class VisitorService
    {
        private readonly Context _context;
        private readonly IHubContext<VisitorHub> _hubContext;
        public VisitorService(Context context, IHubContext<VisitorHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task SaveVisitor(Visitor visitor)
        {
            await _context.Visitors.AddAsync(visitor);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveVisitorList", GetVisitorChartList());
        }
        public IQueryable <Visitor> GetList()
        {
            return _context.Visitors.AsQueryable();
        }
        public List<VisitorChart> GetVisitorChartList()
        {
            List<VisitorChart> visitorCharts= new List<VisitorChart>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "Select tarih,[1],[2],[3],[4],[5] from (select[City],CityVisitor,Cast([VisitDate] as Date) as tarih from Visitors) as visitTable Pivot (Sum(CityVisitor) For City in([1],[2],[3],[4],[5])) as pivottable order by tarih asc";
                command.CommandType = CommandType.Text;
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VisitorChart visitorChart= new VisitorChart();
                        visitorChart.VisitDate = reader.GetDateTime(0).ToShortDateString();
                        Enumerable.Range(1, 10).ToList().ForEach(x =>
                        {
                            if (DBNull.Value.Equals(reader[x]))
                            {
                                visitorChart.Counts.Add(0);
                            }
                            else
                            {
                                visitorChart.Counts.Add(reader.GetInt32(x));
                            }
                        });
                       visitorCharts.Add(visitorChart);
                    }
                }
                _context.Database.CloseConnection();
                return visitorCharts;
            }
        }
    }
}
