using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Domain.Entity.Cart
{
    public class Cart
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
