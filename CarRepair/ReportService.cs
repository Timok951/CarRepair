using CarRepair;
using System.Collections.Generic;
using System.Linq;

public class ReportService
{
    private readonly CarRepairEntities6 _context;

    public ReportService(CarRepairEntities6 context)
    {
        _context = context;
    }

    public List<ProfitReport> GetTotalProfitByAddress()
    {
        var totalProfit = _context.STOes
            .GroupBy(sto => new { sto.AddressSTO, sto.AmountPlaces })
            .Select(g => new ProfitReport
            {
                Address = g.Key.AddressSTO,
                TotalProfit = g.Sum(sto => sto.ProfitSTO),
                TotalPlaces = g.Key.AmountPlaces
            })
            .ToList();

        return totalProfit;
    }
}
