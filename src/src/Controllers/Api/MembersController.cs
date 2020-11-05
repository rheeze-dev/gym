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
    [Route("api/Members")]
    //[Authorize]
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;



        public MembersController(ApplicationDbContext context,
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

        //POST: api/Dashboard/PostExtend
        [HttpPost("PostExtend")]
        public async Task<IActionResult> PostExtend(int id)
        {
            var extend = _context.Members.Where(x => x.Id == id).FirstOrDefault();
            if (extend.Status == "Active")
            {
                extend.ExpireDate = extend.ExpireDate.AddMonths(1);

            }
            else
            {
                extend.ExpireDate = DateTime.Now.AddMonths(1);
            }
            _context.Members.Update(extend);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Successfully Saved!" });
        }
        //POST: api/Dashboard/PostMembers
        [HttpPost]
        public async Task<IActionResult> PostMembers([FromBody] JObject model)
        {
            int id = 0;
            var info = await _userManager.GetUserAsync(User);
            id = Convert.ToInt32(model["Id"].ToString());
            var daily = _context.DailyCollection.OrderByDescending(x => x.Date).FirstOrDefault();
            var monthly = _context.MonthlyCollection.OrderByDescending(x => x.Date).FirstOrDefault();

            Members members = new Members
            {
                FullName = model["FullName"].ToString(),
                Address = model["Address"].ToString(),
                Contact = model["Contact"].ToString(),
                MedicalCondition = model["MedicalCondition"].ToString(),
                School = model["School"].ToString(),
                StartDate = DateTime.Now,
                Age = Convert.ToInt32(model["Age"].ToString()),
                Remarks = model["Remarks"].ToString(),
                LockerNumber = Convert.ToInt32(model["LockerNumber"].ToString()),
                Plan = Convert.ToInt32(model["Plan"].ToString())

            };

            members.ExpireDate = members.StartDate.AddMonths(1);
            if (model["IsStudent"].ToString() == "Student")
            {
                members.IsStudent = true;
                if (members.Plan == 1)
                {
                    members.AmountPaid = 400;
                }
                else if (members.Plan == 2)
                {
                    members.AmountPaid = 370;
                }
                else if (members.Plan == 3)
                {
                    members.AmountPaid = 350;
                }
                else
                {
                    members.AmountPaid = 300;
                }
            }
            else
            {
                members.IsStudent = false;
                if (members.Plan == 1)
                {
                    members.AmountPaid = 500;
                }
                else if (members.Plan == 2)
                {
                    members.AmountPaid = 470;
                }
                else if (members.Plan == 3)
                {
                    members.AmountPaid = 450;
                }
                else
                {
                    members.AmountPaid = 400;
                }
            }
            if (DateTime.Now >= members.ExpireDate)
            {
                members.Status = "Expired";
            }
            else
            {
                members.Status = "Active";

            }

            if (id == 0)
            {
                //subject.RequesterName = info.FullName;
                _context.Members.Add(members);
            }
            else
            {
                members.Id = id;
                //repair.RequesterName = info.FullName;
                _context.Members.Update(members);
            }
            var Daily = new DailyCollection
            {
                Origin = "Membership of " + members.FullName,
                Date = DateTime.Now,
                Amount = members.AmountPaid,
                Remarks = members.Remarks
            };
            if (daily == null)
            {
                Daily.Total = Daily.Amount;
                _context.DailyCollection.Add(Daily);
            }
            else if (Daily.Date.Day == daily.Date.Day)
            {
                Daily.Total = daily.Total + Daily.Amount;
            }
            else
            {
                Daily.Total = Daily.Amount;
            }
            _context.DailyCollection.Add(Daily);

            var Monthly = new MonthlyCollection
            {
                Origin = "Membership of " + members.FullName,
                Date = DateTime.Now,
                Amount = members.AmountPaid,
                Remarks = members.Remarks
            };
            if (monthly == null)
            {
                Monthly.Total = Monthly.Amount;
                _context.MonthlyCollection.Add(Monthly);
            }
            else if (Monthly.Date.Month == monthly.Date.Month)
            {
                Monthly.Total = monthly.Total + Monthly.Amount;
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
        public async Task<IActionResult> DeleteMembers([FromRoute] int id)
        {
            Members members = _context.Members.Where(mem => mem.Id == id).FirstOrDefault();
            _context.Remove(members);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete success." });
        }

    }
}