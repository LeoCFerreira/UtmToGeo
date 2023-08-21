using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinatorConversorLib.Models.Points
{
    public interface IUtmPoint
    {
        decimal X { get; set; }

        decimal Y { get; set; }

        object Clone();
    }
}
