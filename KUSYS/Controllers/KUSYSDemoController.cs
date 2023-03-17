using KUSYS.Bussiness.Containers;
using KUSYS.Bussiness.Operations.Crud;
using KUSYS.Bussiness.Operations.View;
using KUSYS.Common.Entities;
using Library.Security;
using Library.Security.Models;
using MethodBoundaryAspect.Fody.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace KUSYS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KUSYSDemoController : ControllerBase
    {


        private readonly ILogger<KUSYSDemoController> _logger;
        private readonly KUSYSDBContext _kUSYSDBContext;
        public KUSYSDemoController(ILogger<KUSYSDemoController> logger, KUSYSDBContext kUSYSDBContext)
        {
            _kUSYSDBContext = kUSYSDBContext;

            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "US-1")]
        public MethodExecutionBase CreateStudent(StudentModel student)
        {

            return GeneralCruds.CreateStudent(student, _kUSYSDBContext);
        }
        [HttpPost]
        [Authorize(Roles = "US-1")]
        public MethodExecutionBase DeleteStudent(string userName)
        {
            return GeneralCruds.DeleteStudent(userName, _kUSYSDBContext);
        }
        [HttpPost]
        [Authorize(Roles = "US-1")]
        public MethodExecutionBase UpdateStudent(StudentModel student)
        {
            return GeneralCruds.UpdateStudent(student, _kUSYSDBContext);
        }
        [HttpPost]
        [Authorize(Roles = "US-2")]
        public MethodExecutionBase ListAllStudent()
        {
            return GeneralViews.ListAllStudent(_kUSYSDBContext);
        }
        [HttpPost]
        [Authorize(Roles = "US-4")]
        public MethodExecutionBase ListAllStudentWithCourses(List<string> courseIdList)
        {

            return GeneralViews.ListAllStudentWithCourses(courseIdList, User, _kUSYSDBContext);
        }
        [HttpPost]
        [Authorize(Roles = "US-3")]
        public MethodExecutionBase MatchStudentWithCourses()
        {

            return GeneralViews.MatchStudentWithCourses(_kUSYSDBContext, User);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserModel userModel)
        {
            userModel.Password = userModel.Password.ToMd5();
            var isvalid = await SecurityContext.Login(this, userModel);
            if (isvalid != null)
            {
                return RedirectToAction("MainWindow");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        [Authorize(Roles = "US-1,US-2,US-3,US-4")]
        [HttpGet]
        public string MainWindow()
        {
            string token = HttpContext.Session.GetString("Token");
            return token;
        }
    }
}