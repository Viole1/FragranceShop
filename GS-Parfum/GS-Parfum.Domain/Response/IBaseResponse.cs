using GS_Parfum.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Domain.Response
{
    public interface IBaseRepsonse<T>
    {
        T Data { get; set; }
        ResponseStatus ResponseStatus { get; set; }
        string Description { get; set; }

    }
}
