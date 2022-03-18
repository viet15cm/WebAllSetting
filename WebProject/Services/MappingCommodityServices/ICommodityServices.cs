using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Services.MappingCommodityServices
{
    public interface ICommodityServices
    {
        Task<IEnumerable<Commodity>> GetAll();

        Task<IdentityResult> Editor(Commodity commodity);

        Task<IdentityResult> Create(Commodity commodity);


        Task<IdentityResult> Delete(Guid? Id);

        Task<Commodity> GetCommodity(Guid? Id);

       

       
    }
}
