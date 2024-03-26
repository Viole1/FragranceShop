using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Domain.Enum
{
    public enum ResponseStatus
    {
        UserNotFound = 0,

        ProductNotFound = 10,

        OK = 200,
        InternalServerError = 500,
        SelectFailed = 600,
    }
}
