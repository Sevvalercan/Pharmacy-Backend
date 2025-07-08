using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_Backend.Data;
using Pharmacy_Backend.Models;

namespace Pharmacy_Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IlaclarController : ControllerBase
    {
        //private readonly ContextDb _context;

        //public IlaclarController(ContextDb context)
        //{
        //    _context = context;
        //}

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetIlaclar()
        {
            //var ilaclar = _context.Ilaclar.ToList();
            return Ok();
        }

        //[HttpPost]
        //public IActionResult AddIlac(Ilac ilac)
        //{
        //    _context.Ilaclar.Add(ilac);
        //    _context.SaveChanges();
        //    return Ok(ilac);
        //}
    }
}
