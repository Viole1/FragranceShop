using GS_Parfum.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Domain.Response
{
    public class BaseResponse<T> : IBaseRepsonse<T>
    {
        public string Description { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        public T Data { get; set; }
    }
}
