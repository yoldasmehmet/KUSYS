using KUSYS.Bussiness.Containers;
using KUSYS.Common.Entities;
using MethodBoundaryAspect.Fody.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Bussiness.Operations.Crud
{
    public class GeneralCruds
    {
        [Logged(flowBehavior: FlowBehavior.Continue)]// Programın hata vermeden devam etmesini sağlayan özellik. Try catch bloguna alır kodu ve hatayı yakalamaz sadece loglar.
                                                     // Aşagıdaki commentli hata fırlatma kodunu açıp test edebilirsiniz.
        public static MethodExecutionBase CreateStudent(StudentModel student, KUSYSDBContext kUSYSDBContext)
        {

            kUSYSDBContext.Students.Add(new Student { FirstName = student.FirstName, UserName = student.UserName, LastName = student.LastName, BirthDate = student.BirthDate });
            kUSYSDBContext.SaveChanges();
            // throw new Exception("DENEME2");//Burdaki comment kaldırılsa api hataya düşmeyecek değer dönderecektir statusu false olan.
            return new BaseResponse { Data = student };
        }
        [Logged]
        public static MethodExecutionBase DeleteStudent(string userName, KUSYSDBContext kUSYSDBContext)
        {
            var user = kUSYSDBContext.Students.FirstOrDefault(x => x.UserName == userName);
            if (user != null)
            {
                kUSYSDBContext.Students.Remove(user);
                kUSYSDBContext.SaveChanges();
            }

            return new BaseResponse { Data = user };
        }
        [Logged]
        public static MethodExecutionBase UpdateStudent(StudentModel student, KUSYSDBContext kUSYSDBContext)
        {
            var updatedStudent = kUSYSDBContext.Students.Where(x => x.UserName == student.UserName).FirstOrDefault();
            if (updatedStudent != null)
            {
                updatedStudent.FirstName = student.FirstName;
                updatedStudent.LastName = student.LastName;
                updatedStudent.BirthDate = student.BirthDate;
                kUSYSDBContext.SaveChanges();
            }
            return new BaseResponse { Data = student };
        }
    }

}
