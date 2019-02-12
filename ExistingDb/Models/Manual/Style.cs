﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExistingDb.Models.Manual
{
    [Table("Colors")]
    public class Style
    {
        [Key]
        [Column("Id")]
        public long UniqueIdent { get; set; }
        [Column("name")]
        public string StyleName { get; set; }
        public string MainColor { get; set; }
        public string HighlightColor { get; set; }
        [InverseProperty(nameof(Shoe.Style))]
        public IEnumerable<Shoe> Products { get; set; }
    }
}
