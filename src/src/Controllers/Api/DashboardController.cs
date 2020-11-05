﻿using System;
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
    [Route("api/Dashboard")]
    //[Authorize]
    public class SecurityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;



        public SecurityController(ApplicationDbContext context,
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

        // GET: api/Dashboard/GetDashboard
        //[HttpGet("{organizationId}")]
        //public IActionResult GetDashboard([FromRoute]Guid organizationId)
        //{
        //    var subject = _context.Subject.ToList();
        //    return Json(new { data = subject });
        //}

        // POST: api/Dashboard/GetDashboard
        //[HttpPost]
        //public async Task<IActionResult> PostDashboard([FromBody] JObject model)
        //{
        //    int id = 0;
        //    var info = await _userManager.GetUserAsync(User);
        //    id = Convert.ToInt32(model["Id"].ToString());
        //    Subject subject = new Subject
        //    {
        //        SubjectCode = model["SubjectCode"].ToString(),
        //        Name = model["Name"].ToString(),
        //        Description = model["Description"].ToString(),
        //        //Public = Convert.ToBoolean(model["Public"].ToString()),
        //        AvailableForCourses = model["AvailableForCourses"].ToString(),
        //        //Active = Convert.ToBoolean(model["Active"].ToString())
        //    };

        //    if (id == 0)
        //    {
        //        //subject.RequesterName = info.FullName;
        //        _context.Subject.Add(subject);
        //    }
        //    else
        //    {
        //        subject.Id = id;
        //        //repair.RequesterName = info.FullName;
        //        _context.Subject.Update(subject);
        //    }
        //    await _context.SaveChangesAsync();
        //    return Json(new { success = true, message = "Successfully Saved!" });
        //}

        // POST: api/Dashboard/SubmitSubjects
        //[HttpPost("SubmitSubjects")]
        //public async Task<IActionResult> SubmitSubjects(int id)
        //{
        //    //int id = 0;
        //    var info = await _userManager.GetUserAsync(User);
        //    //id = Convert.ToInt32(model["Id"].ToString());
        //    Subject subject = new Subject
        //    {
        //        Id = Convert.ToInt32(id)
        //    };

        //    _context.Subject.Add(subject);
        //    //if (id == 0)
        //    //{
        //    //    //subject.RequesterName = info.FullName;
        //    //    _context.Subject.Add(subject);
        //    //}
        //    //else
        //    //{
        //    //    subject.Id = id;
        //    //    //repair.RequesterName = info.FullName;
        //    //    _context.Subject.Update(subject);
        //    //}
        //    await _context.SaveChangesAsync();
        //    return Json(new { success = true, message = "Successfully Saved!" });
        //}

        // DELETE: api/Security/
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSecurityRepairCheck([FromRoute] int id)
        //{
        //    Subject subject = _context.Subject.Where(x => x.Id == id).FirstOrDefault();
        //    _context.Remove(subject);
        //    await _context.SaveChangesAsync();
        //    return Json(new { success = true, message = "Delete success." });
        //}

    }
}