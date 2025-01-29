using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepair
{
    public class SparePartsService
    {
        private readonly CarRepairEntities6 _context;

        public SparePartsService(CarRepairEntities6 context)
        {
            _context = context;
        }

        public List<string> CheckSparePartsStock()
        {
            var lowStockParts = _context.SpareParts
                .Where(sp => sp.QuantityInStock < 20)
                .Select(sp => sp.NameSparePart)
                .ToList();

            var messages = new List<string>();

            if (lowStockParts.Any())
            {
                foreach (var part in lowStockParts)
                {
                    messages.Add($"Опасно мало деталей: {part}");
                }
            }
            else
            {
                messages.Add("Запас деталей в норме.");
            }

            return messages;
        }
    }
}
