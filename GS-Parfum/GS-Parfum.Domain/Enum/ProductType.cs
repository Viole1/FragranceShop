using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Domain.Enum
{
    public enum ProductType
    {
        [Display(Name = "Душистая вода")]
        EauFraiche = 0,
        [Display(Name = "Одеколон")]
        Cologne = 1,
        [Display(Name = "Туалетная вода")]
        EauDeToilette = 2,
        [Display(Name = "Парфюмерная вода")]
        EauDeParfum = 3,
        [Display(Name = "Парфюм")]
        Parfum = 4
    }
}
