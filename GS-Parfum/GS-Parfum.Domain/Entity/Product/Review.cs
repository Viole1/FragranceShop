﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS_Parfum.Domain.Entity;

namespace GS_Parfum.Domain.Entity.Product
{
    public class Review
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }

        [Range(1, 5)]
        public int Rate { get; set; }
        [Range(1, 5)]
        public int LongevetyRate { get; set; }
        [Range(1, 5)]
        public int SillageRate { get; set; }

        public string Content { get; set; }
        public DateTime Date { get; set; }
        public virtual Product Product { get; set; }
        public virtual User.User User { get; set; }
    }
}
