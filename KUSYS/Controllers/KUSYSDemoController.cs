//using KUSYS.Bussiness.Containers;
//using KUSYS.Common.Entities;
//using Library.Security;
//using Library.Security.Models;
//using MethodBoundaryAspect.Fody.Attributes;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;
//using System.Text;
//using System.Text.Json;

//namespace KUSYS.Controllers
//{
//    [ApiController]
//    [Route("[controller]/[action]")]
//    public class KUSYSDemoController : ControllerBase
//    {


//        private readonly ILogger<KUSYSDemoController> _logger;
//        private readonly KUSYSDBContext _kUSYSDBContext;
//        public KUSYSDemoController(ILogger<KUSYSDemoController> logger, KUSYSDBContext kUSYSDBContext)
//        {
//            _kUSYSDBContext = kUSYSDBContext;

//            _logger = logger;
//        }

//        [HttpPost]
//        [Authorize(Roles = "US-1")]
//        [Logged(flowBehavior:FlowBehavior.Continue)]// Programın hata vermeden devam etmesini sağlayan özellik. Try catch bloguna alır kodu ve hatayı yakalamaz sadece loglar.
//                                                    // Aşagıdaki commentli hata fırlatma kodunu açıp test edebilirsiniz.
//        public MethodExecutionBase CreateStudent(StudentModel student)
//        {

//            _kUSYSDBContext.Students.Add(new Student { FirstName = student.FirstName, UserName = student.UserName, LastName = student.LastName, BirthDate = student.BirthDate });
//            _kUSYSDBContext.SaveChanges();
//           //throw new Exception("DENE"); //Burdaki comment kaldırılsa bile program hata vermeyecek fakat loglarda hata verdiği görünecektir. 
//            return new BaseResponse { Data = student };
//        }
//        [HttpPost]
//        [Authorize(Roles = "US-1")]
//        [Logged]
//        public MethodExecutionBase DeleteStudent( string userName)
//        {
//            var deletedRecord = _kUSYSDBContext.Students.Single(x => x.UserName == userName);
//            var s = _kUSYSDBContext.Students.Remove(deletedRecord);
//            _kUSYSDBContext.SaveChanges();
//            return new BaseResponse { Data = deletedRecord };
//        }
//        [HttpPost]
//        [Authorize(Roles = "US-1")]
//        [Logged]
//        public MethodExecutionBase UpdateStudent(StudentModel student)
//        {
//            var updatedStudent = _kUSYSDBContext.Students.Where(x => x.UserName == student.UserName).FirstOrDefault();
//            updatedStudent.FirstName = student.FirstName;
//            updatedStudent.LastName = student.LastName;
//            updatedStudent.BirthDate = student.BirthDate;
//            _kUSYSDBContext.SaveChanges();
//            return new BaseResponse { Data = student};
//        }
//        [HttpPost]
//        [Authorize(Roles = "US-2")]
//        [Logged]
//        public MethodExecutionBase ListAllStudent()
//        {
//            var studentList = _kUSYSDBContext.Students.ToList();
//            return new BaseResponse { Data = studentList};
//        }
//        [HttpPost]
//        [Authorize(Roles = "US-4")]
//        [Logged]
//        public MethodExecutionBase ListAllStudentWithCourses(List<string> courseIdList)
//        {

//            object match = null;
//            var actor = User.Identities.SelectMany(x => x.Claims).Where(x => x.Type == ClaimTypes.Actor).Select(x => x.Value).FirstOrDefault();
//            switch (actor)
//            {
//                case "user":
//                    {
//                        match = _kUSYSDBContext.StudentCourses.Include(x => x.Student).Include(x => x.Course).Where(x => x.Student.UserName == User.Identity.Name).ToList();
//                    }
//                    break;
//                case "admin":
//                    {
//                        match = _kUSYSDBContext.StudentCourses.Include(x => x.Student).Include(x => x.Course).ToList();
//                    }
//                    break;
//                default:
//                    break;
//            }
//           // throw new Exception("DENEME2");//Burdaki comment kaldırılsa program hataya düşecektir. Aşağıdaki örnekte düşmeyecektir.
//            return new BaseResponse { Data = match};
//        }
//        [HttpPost]
//        [Authorize(Roles = "US-3")]
//        [Logged(flowBehavior:FlowBehavior.Continue)]
//        public MethodExecutionBase MatchStudentWithCourses(StudentModel student)
//        {

//            var result = _kUSYSDBContext.StudentCourses.Include(x => x.Student).Include(x => x.Course).Where(x => x.Student.UserName == User.Identity.Name).ToList();
//            //throw new Exception("DENEME"); //Burdaki comment kaldırılsa bile program hata vermeyecek fakat loglarda hata verdiği görünecektir.
//            return new BaseResponse { Data = result };
//        }

//        [AllowAnonymous]
//        [HttpPost]
//        [Logged]
//        public async Task<IActionResult> Login(UserModel userModel)
//        {
//            userModel.Password = userModel.Password.ToMd5();
//            var isvalid = await SecurityContext.Login(this, userModel);
//            if (isvalid != null)
//            {
//                return RedirectToAction("MainWindow");
//            }
//            else
//            {
//                return RedirectToAction("Error");
//            }
//        }


//        [Authorize(Roles = "US-1,US-2,US-3,US-4")]
//        [HttpGet]
//        public string MainWindow()
//        {
//            string token = HttpContext.Session.GetString("Token");
//            return token;
//        }
//    }
//}


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