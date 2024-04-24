using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Domain.Entity.Cart
{
    public class CartItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int VolumePriceId { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
    }
}
