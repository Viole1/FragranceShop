using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS_Parfum.Domain.Enum;

namespace GS_Parfum.Domain.Entity.User
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[Required]
        public Role Role { get; set; }

        //[Required]
        public string Name { get; set; }
        //[Required]
        public string Surname { get; set; }
        //[Required]
        [StringLength(20)]
        public string Username { get; set; }
        //[Required]
        // [StringLength(20)]
        public string Password { get; set; }

        //[Required]
        [StringLength(50)]
        //[RegularExpression(@"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}\b")] // \b [A-Z0-9._%+-] + @[A-Z0-9.-] + \.[A-Z]{2,} \b
        public string Email { get; set; }
        //[Required]
        //[Phone]
        public string PhoneNumber { get; set; }

        // при регистрации заполняются эти поля обязательно
        //[Required]
        [StringLength(20)]
        public string DeliveryName { get; set; }
        //[Required]
        [StringLength(20)]
        public string DeliveryCity { get; set; }
        //[Required]
        [StringLength(20)]
        public string DeliveryStreet { get; set; }
        //[Required]
        [StringLength(20)]
        public string DeliveryHomeNumber { get; set; }
    }
}
