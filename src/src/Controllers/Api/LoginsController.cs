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
    [Route("api/Logins")]
    //[Authorize]
    public class LoginsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;



        public LoginsController(ApplicationDbContext context,
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

        //GET: api/Dashboard/GetMembers
        [HttpGet("{organizationId}")]
        public IActionResult GetMembers([FromRoute]Guid organizationId)
        {
            var members = _context.Members.OrderByDescending(x => x.Id).ToList();
            return Json(new { data = members });
        }

        //POST: api/Dashboard/PostTimein
        [HttpPost("PostTimein")]
        public async Task<IActionResult> PostTimein(int id)
        {
            var info = await _userManager.GetUserAsync(User);
            var members = _context.Members.Where(x => x.Id == id).FirstOrDefault();
            var logins = new Logins
            {
                MembersId = members.Id,
                FullName = members.FullName,
                Timein = DateTime.Now
            };
            members.IsTimeout = true;
            _context.Members.Update(members);

            //subject.RequesterName = info.FullName;
            _context.Logins.Add(logins);

            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Successfully Saved!" });
        }
        //POST: api/Dashboard/PostTimeout
        [HttpPost("PostTimeout")]
        public async Task<IActionResult> PostTimeout(int id)
        {
            var info = await _userManager.GetUserAsync(User);
            var timeOutChecker = _context.Members.Where(x => x.Id == id).FirstOrDefault();
            timeOutChecker.IsTimeout = false;
            var logins = _context.Logins.Where(x => x.MembersId == id).OrderByDescending(x=> x.Timein).FirstOrDefault();
            logins.Timeout = DateTime.Now;
            //subject.RequesterName = info.FullName;
            _context.Members.Update(timeOutChecker);
            _context.Logins.Update(logins);

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