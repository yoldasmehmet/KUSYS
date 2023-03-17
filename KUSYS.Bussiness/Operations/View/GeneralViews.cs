using KUSYS.Bussiness.Containers;
using KUSYS.Common.Entities;
using Library.Common.Entities;
using MethodBoundaryAspect.Fody.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Bussiness.Operations.View
{
    public  class GeneralViews
    {
        [Logged]
        public static  MethodExecutionBase ListAllStudent(KUSYSDBContext kUSYSDBContext)
        {
            
            var studentList = kUSYSDBContext.Students.ToList();
            return new BaseResponse { Data = studentList };
        }
        [Logged]
        public static  MethodExecutionBase ListAllStudentWithCourses(List<string> courseIdList, ClaimsPrincipal User,KUSYSDBContext kUSYSDBContext)
        {

            object match = null;
            var actor = User.Identities.SelectMany(x => x.Claims).Where(x => x.Type == ClaimTypes.Actor).Select(x => x.Value).FirstOrDefault();
            switch (actor)
            {
                case "user":
                    {
                        match = kUSYSDBContext.StudentCourses.Include(x => x.Student).Include(x => x.Course).Where(x => x.Student.UserName == User.Identity.Name).ToList();
                    }
                    break;
                case "admin":
                    {
                        match = kUSYSDBContext.StudentCourses.Include(x => x.Student).Include(x => x.Course).ToList();
                    }
                    break;
                default:
                    break;
            }
            // throw new Exception("DENEME2");//Burdaki comment kaldırılsa program hataya düşecektir. Aşağıdaki örnekte düşmeyecektir.
            return new BaseResponse { Data = match };
        }

        [Logged(flowBehavior: FlowBehavior.Continue)]
        public static  MethodExecutionBase MatchStudentWithCourses(KUSYSDBContext kUSYSDBContext, ClaimsPrincipal User)
        {
        
            var result = kUSYSDBContext.Courses.Where(x => x.StudentCourses.Any(a=>a.Student.UserName==User.Identity.Name) ).AsNoTracking().ToList();
            //throw new Exception("DENEME"); //Burdaki comment kaldırılsa bile program hatayı api tarafında vermeyecek status=false değeri dönecektir; fakat loglarda hata verdiği görünecektir.
            return new BaseResponse { Data = result };
        }

    }
}
