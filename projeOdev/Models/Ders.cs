using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeOdev.Models
{
    public class Ders
    {
        [Key]
        public int DersId { get; set; }
        public string DersAdi { get; set; }
        public int? Kredisi { get; set; }
        public int YonetimId { get; set; }
        public virtual Yonetim Yonetim { get; set; }
    }
}