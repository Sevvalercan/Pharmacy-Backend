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


        //tüm ilaçları listeleme
        [HttpGet ("ilaç listeleme")]
        [AllowAnonymous]
        public async Task<IlaclarModelResponse> GetIlaclar()
        {
            var response = new IlaclarModelResponse();

            var ilaclar = await _ilacRepositories.GetListAsync(i => i.Status == true);
            if (!ilaclar.Any())
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


        //id ye göre ilaç getirme
        [HttpGet("{id} göre ilaç getirme")]
        [AllowAnonymous]
        public async Task<IActionResult> GetIlac(long id)
        {
            var ilac = await _ilacRepositories.GetAsync(i => i.Id == id && i.Status == true);
            if (ilac == null)
                return NotFound("İlaç bulunamadı.");

            var model = new IlaclarModel
            {
                Id = ilac.Id,
                Name = ilac.Name,
                Description = ilac.Description,
                Barcode = ilac.Barcode
            };

            return Ok(model);
        }



        //desc na göre listeleme

        [HttpPost("desc göre ilaç filtreleme")]
        [AllowAnonymous]
        public async Task<IlaclarModelResponse> FiltreliIlacGetir([FromBody] IlacFiltreRequest request)
        {
            var response = new IlaclarModelResponse();

            if (string.IsNullOrWhiteSpace(request.DescriptionKeyword))
            {
                response.Code = "400";
                response.Errors.Add("Anahtar kelime boş olamaz.");
                return response;
            }

            string NormalizeText(string text)
            {
                if (string.IsNullOrEmpty(text)) return string.Empty;
                var normalized = text.ToLower().Trim();
                var charsToRemove = new char[] { '.', ',', ';', ':', '-', '_', '\n', '\r' };
                foreach (var c in charsToRemove)
                    normalized = normalized.Replace(c.ToString(), "");
                return normalized;
            }

            var normalizedKeywords = NormalizeText(request.DescriptionKeyword)
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Burada Status = true olanlar aranacak
            var ilaclarAll = await _ilacRepositories.GetListAsync(i => i.Status == true && i.Description != null);

            var ilaclar = ilaclarAll
                .Where(i => normalizedKeywords.Any(k => NormalizeText(i.Description).Contains(k)))
                .ToList();

            if (!ilaclar.Any())
            {
                response.Code = "404";
                response.Errors.Add("Eşleşen ilaç bulunamadı.");
                return response;
            }

            foreach (var ilac in ilaclar)
            {
                response.ilaclarModels.Add(new IlaclarModel
                {
                    Id = ilac.Id,
                    Name = ilac.Name,
                    Description = ilac.Description,
                    Barcode = ilac.Barcode
                });
            }

            response.Code = "200";
            response.Message = "Eşleşen ilaçlar başarıyla listelendi.";
            return response;
        }




        //ilaç düzenleme
        [HttpPut("ilaç düzenleme")]
        [AllowAnonymous]
        // Gerekirse AllowAnonymous yapılabilir
        public async Task<BaseApiResponse> UpdateIlac([FromBody] IlacUpdateModel model)
        {
            var response = new BaseApiResponse();

            // 1. İlaç var mı kontrol et
            var ilac = await _ilacRepositories.GetAsync(i => i.Id == model.Id && i.Status == true);
            if (ilac == null)
            {
                response.Code = "404";
                response.Errors.Add("İlaç bulunamadı.");
                return response;
            }

            // 2. Alanları güncelle (sadece gelenleri)
            if (!string.IsNullOrWhiteSpace(model.Name))
                ilac.Name = model.Name;

            if (!string.IsNullOrWhiteSpace(model.Description))
                ilac.Description = model.Description;

            if (!string.IsNullOrWhiteSpace(model.Barcode))
                ilac.Barcode = model.Barcode;

            if (model.Stock.HasValue)
                ilac.Stock = model.Stock.Value;

            ilac.ModifiedDate = DateTime.UtcNow;

            // 3. Güncelle ve kaydet
            await _ilacRepositories.UpdateAsync(ilac);

            response.Code = "200";
            response.Message = "İlaç başarıyla güncellendi.";
            return response;
        }


        // ilaç silme(soft delete)

        [HttpDelete("{id} göre ilaç silme")]
        [AllowAnonymous]
        public async Task<BaseApiResponse> DeleteIlac(long id)
        {
            var response = new BaseApiResponse();

            // 1. İlaç var mı kontrol et
            var ilac = await _ilacRepositories.GetAsync(i => i.Id == id && i.Status == true);
            if (ilac == null)
            {
                response.Code = "404";
                response.Errors.Add("Silinecek ilaç bulunamadı.");
                return response;
            }

            // 2. Soft delete işlemleri
            ilac.Status = false; // pasif hâle getir
            ilac.DeletedDate = DateTime.UtcNow;
            ilac.ModifiedDate = DateTime.UtcNow;

            // 3. Veritabanına güncelle
            await _ilacRepositories.UpdateAsync(ilac);

            response.Code = "200";
            response.Message = "İlaç başarıyla silindi (soft delete).";
            return response;
        }



        //ilaç ekleme
        [HttpPost]
        [AllowAnonymous]
        public async Task<BaseApiResponse> AddIlac([FromBody] IlacCreateRequest model)
        {
            var response = new BaseApiResponse();

            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Description))
            {
                response.Code = "400";
                response.Errors.Add("İlaç adı ve açıklaması boş olamaz.");
                return response;
            }

            var ilac = new Ilac
            {
                Name = model.Name,
                Description = model.Description,
                Barcode = model.Barcode,
                Stock = model.Stock,
                Status = true,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                DeletedDate = DateTime.MinValue // ← silinmemiş demek
            };

            await _ilacRepositories.AddAsync(ilac);

            response.Code = "201";
            response.Message = "İlaç başarıyla eklendi.";
            return response;
        }


    }
}
