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
    [Route("api/Others")]
    //[Authorize]
    public class OthersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;



        public OthersController(ApplicationDbContext context,
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

        //GET: api/Dashboard/GetOthers
        [HttpGet("{organizationId}")]
        public IActionResult GetOthers([FromRoute]Guid organizationId)
        {
            var others = _context.Others.OrderByDescending(x => x.Id).ToList();
            return Json(new { data = others });

        }
        //GET: api/Dashboard/GetDaily
        [HttpGet("GetDaily")]
        public IActionResult GetDaily([FromRoute]Guid organizationId)
        {
            var daily = _context.DailyCollection.OrderByDescending(x => x.Date).ToList();
            return Json(new { data = daily });
        }
        //GET: api/Dashboard/GetMonthly
        [HttpGet("GetMonthly")]
        public IActionResult GetMonthly([FromRoute]Guid organizationId)
        {
            var monthly = _context.MonthlyCollection.OrderByDescending(x => x.Date).ToList();
            return Json(new { data = monthly });
        }

        //POST: api/Dashboard/PostOthers
        [HttpPost]
        public async Task<IActionResult> PostOthers([FromBody] JObject model)
        {
            int id = 0;
            var info = await _userManager.GetUserAsync(User);
            id = Convert.ToInt32(model["Id"].ToString());
            Others others = new Others
            {
                Items = model["Items"].ToString(),
                Price = Convert.ToInt32(model["Price"].ToString()),
                Date = DateTime.Now,
                Remarks = model["Remarks"].ToString()
     
            };

            if (id == 0)
            {
                //subject.RequesterName = info.FullName;
                _context.Others.Add(others);
            }
            else
            {
                others.Id = id;
                //repair.RequesterName = info.FullName;
                _context.Others.Update(others);
            }
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Successfully Saved!" });
        }
        //POST: api/Dashboard/PostBuy
        [HttpPost("PostBuy")]
        public async Task<IActionResult> PostBuy(int id)
        {
            var info = await _userManager.GetUserAsync(User);
            var others = _context.Others.Where(x => x.Id == id).FirstOrDefault();
            var latestDaily = _context.DailyCollection.OrderByDescending(x => x.Date).FirstOrDefault();
            var latestMonthly = _context.MonthlyCollection.OrderByDescending(x => x.Date).FirstOrDefault();
            //var now = 
            var Daily = new DailyCollection {
                Origin = others.Items,
                Date = DateTime.Now,
                Amount = others.Price,
                Remarks = others.Remarks
            };
            if (latestDaily == null)
            {
                Daily.Total = Daily.Amount;
                _context.DailyCollection.Add(Daily);
            }
            else if (Daily.Date.Day == latestDaily.Date.Day)
            {
                Daily.Total = latestDaily.Total + Daily.Amount;
            }
            else
            {
                Daily.Total = Daily.Amount;
            }
            _context.DailyCollection.Add(Daily);

            var Monthly = new MonthlyCollection
            {
                Origin = others.Items,
                Date = DateTime.Now,
                Amount = others.Price,
                Remarks = others.Remarks
            };
            if (latestMonthly == null)
            {
                Monthly.Total = Monthly.Amount;
                _context.MonthlyCollection.Add(Monthly);
            }
            else if (Monthly.Date.Month == latestMonthly.Date.Month)
            {
                Monthly.Total = latestMonthly.Total + Monthly.Amount;
            }
            else
            {
                Monthly.Total = Monthly.Amount;
            }
            _context.MonthlyCollection.Add(Monthly);

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