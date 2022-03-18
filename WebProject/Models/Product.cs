using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(5)]
        [Column(TypeName = ("char"))]
        [Required]
        public string Code { get; set; }
        
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(16,2)")]
        public decimal Price { get; set; }
        
        [StringLength(100)]
        public string UrlImg { get; set; }

        public Guid? CommodityId { get; set; }

        public virtual Commodity Commodity { get; set; }
    }
}
