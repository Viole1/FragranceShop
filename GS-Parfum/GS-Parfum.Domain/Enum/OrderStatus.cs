using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Domain.Enum
{
    public enum OrderStatus
    {
        IN_PROCESS = 0,
        SHIPPED = 1,
        DELIVERED = 2,
        CANCELED = 3
    }
}
