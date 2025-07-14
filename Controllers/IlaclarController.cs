using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_Backend.Data;
using Pharmacy_Backend.DTOs;
using Pharmacy_Backend.Models;
using Pharmacy_Backend.Repositories;

namespace Pharmacy_Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IlaclarController : ControllerBase
    {
        private readonly IIlacRepositories _ilacRepositories;

        public IlaclarController(IIlacRepositories ilacRepositories)
        {
            _ilacRepositories = ilacRepositories;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IlaclarModelResponse> GetIlaclar()
        {
            var response = new IlaclarModelResponse();

            var ilaclar = await _ilacRepositories.GetListAsync(i=>i.Status==false);
            if(!ilaclar.Any())
            {
                response.Code = "400";
                response.Errors.Add("İlaç Bulunamadı");
                return response;
            }

            foreach (var ilac in ilaclar)
            {
                var model = new IlaclarModel();
                model.Id = ilac.Id;
                model.Name = ilac.Name;
                model.Description = ilac.Description;
                model.Barcode = ilac.Barcode;

                response.ilaclarModels.Add(model);
            }

            response.Code = "200";
            response.Message = "İlaç Listeleme başarılı";
            
            return response;
        }

        
    }
}
