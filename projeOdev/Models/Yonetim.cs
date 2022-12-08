using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace projeOdev.Models
{
    public class Yonetim
    {
        [Key]
        public int YonetimId { get; set; }
        public string AdSoyad { get; set; }
        public string Gorevi { get; set; }
        public string YonetimTip { get; set; }
    }
}