using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace projeOdev.Models
{
    public class Atama
    {
        [Key]
        public int OgrenciDersId { get; set; }
        public int DersId { get; set; }
        public int OgrenciId { get; set; }
        public virtual Ders Ders { get; set; }
        public virtual Ogrenci Ogrenci { get; set; }
    }
}