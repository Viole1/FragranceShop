using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Domain.Enum
{
    public enum SexType
    {
        [Display(Name = "Мужской")]
        MALE = 0,
        [Display(Name = "Женский")]
        FEMALE = 1,
        [Display(Name = "Унисекс")]
        UNISEX = 2
    }
}
