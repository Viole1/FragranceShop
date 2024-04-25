using GS_Parfum.Domain.Entity.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS_Parfum.Domain.Enum;

namespace GS_Parfum.Domain.Entity.Order
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public OrderStatus Status { get; set; }

        public string DeliveryName { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryStreet { get; set; }
        public string DeliveryHomeNumber { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
