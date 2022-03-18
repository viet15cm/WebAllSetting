using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebProject.ModelValidations;

namespace WebProject.Areas.ProductManager.Models
{
    public class CommodityData
    {
       
        public Guid Id { get; set; }

        [Display(Name ="Mã")]
        [CodeValidation]
        [StringLength(5)]
        public string Code { get; set; }

        [Display(Name="Tên")]
        [NameValidation]
        [StringLength(30)]        
        public string Name { get; set; }

        public bool Option { get; set; }
    }
}
