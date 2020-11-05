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
    [Route("api/Equipments")]
    //[Authorize]
    public class EquipmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;



        public EquipmentsController(ApplicationDbContext context,
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

        //GET: api/Dashboard/GetEquipments
        [HttpGet("{organizationId}")]
        public IActionResult GetEquipments([FromRoute]Guid organizationId)
        {
            var equipments = _context.Equipments.OrderByDescending(x => x.Id).ToList();
            return Json(new { data = equipments });
        }

        //POST: api/Dashboard/PostEquipments
        [HttpPost]
        public async Task<IActionResult> PostMembers([FromBody] JObject model)
        {
            int id = 0;
            var info = await _userManager.GetUserAsync(User);
            id = Convert.ToInt32(model["Id"].ToString());
            Equipments equipments = new Equipments
            {
                TotalEquipments = model["TotalEquipments"].ToString(),
                Quantity = Convert.ToInt32(model["Quantity"].ToString()),
                Remarks = model["Remarks"].ToString()
     
            };

            if (id == 0)
            {
                //subject.RequesterName = info.FullName;
                _context.Equipments.Add(equipments);
            }
            else
            {
                equipments.Id = id;
                //repair.RequesterName = info.FullName;
                _context.Equipments.Update(equipments);
            }
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Successfully Saved!" });
        }

       
        //DELETE: api/Security/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipments([FromRoute] int id)
        {
            Equipments equipments = _context.Equipments.Where(mem => mem.Id == id).FirstOrDefault();
            _context.Remove(equipments);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete success." });
        }

    }
}