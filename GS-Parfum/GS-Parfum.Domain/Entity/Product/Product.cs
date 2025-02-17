﻿using GS_Parfum.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS_Parfum.Domain.Entity;

namespace GS_Parfum.Domain.Entity.Product
{
    public class Product
    {
        public int Id { get; set; }

        public byte[] Photo { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public ICollection<ProductVolumePrice> VolumePrices { get; set; }

        // замени на аккорд и добавь типы едп, едт
        public ProductType Type { get; set; } // chords


        public ICollection<Chord> Chords { get; set; }
        public ICollection<Note> TopNotes { get; set; }
        public ICollection<Note> MiddleNotes { get; set; }
        public ICollection<Note> BaseNotes { get; set; }

        // [Required]
        public SexType Sex { get; set; }

        // десятичное число и флакончик со звездочкой
        public int Rating { get; set; }
        public int NumberOfRatings { get; set; }


        public string Description { get; set; }

        // будут просто полоски с указанием количества голос за тот или иной варик
        public List<LongevityRating> LongevityRatings { get; set; }
        public List<SillageRating> SillageRatings { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
