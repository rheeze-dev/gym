using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using src.Data;
using src.Models;
using src.Services;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Promos")]
    //[Authorize]
    public class PromosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;



        public PromosController(ApplicationDbContext context,
            IDotnetdesk dotnetdesk,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _dotnetdesk = dotnetdesk;
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }

        //GET: api/Dashboard/GetPromos
        [HttpGet("{organizationId}")]
        public IActionResult GetPromos([FromRoute]Guid organizationId)
        {
            var promos = _context.Promos.OrderByDescending(x => x.Id).ToList();
            return Json(new { data = promos });
        }

        //POST: api/Dashboard/PostPromos
        [HttpPost]
        public async Task<IActionResult> PostMembers([FromBody] JObject model)
        {
            int id = 0;
            var info = await _userManager.GetUserAsync(User);
            id = Convert.ToInt32(model["Id"].ToString());
            Promos promos = new Promos
            {
                Name = model["Name"].ToString(),
                Price = Convert.ToInt32(model["Price"].ToString()),
                Remarks = model["Remarks"].ToString()
     
            };

            if (id == 0)
            {
                //subject.RequesterName = info.FullName;
                _context.Promos.Add(promos);
            }
            else
            {
                promos.Id = id;
                //repair.RequesterName = info.FullName;
                _context.Promos.Update(promos);
            }
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Successfully Saved!" });
        }

       
        //DELETE: api/Security/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromos([FromRoute] int id)
        {
            Promos promos = _context.Promos.Where(mem => mem.Id == id).FirstOrDefault();
            _context.Remove(promos);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete success." });
        }

    }
}