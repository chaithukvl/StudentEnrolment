using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using StudentEnrolmentWeb.Models;
using System.Diagnostics.Metrics;

namespace StudentEnrolmentWeb.Controllers
{
    public class StudentDetailsController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<StudentDetailsMVC> studentList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("/Student").Result;
            studentList = response.Content.ReadAsAsync<IEnumerable<StudentDetailsMVC>>().Result;
            ViewBag.StudentDetails = studentList.ToList();
            return View(studentList);
        }
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new StudentDetailsMVC());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("/Student/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<StudentDetailsMVC>().Result);
            }
 

        }

        [HttpPost]
        public ActionResult AddOrEdit(StudentDetailsMVC student)
        {
            if (student.StudentId==0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("/Student", student).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("/Student/" + student.StudentId, student).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult AddOrEditEnrolment(StudentDetailsMVC student, CourseEnrolment courseEnrolment)
        {
            IEnumerable<StudentDetailsMVC> studentList;
            if (student.StudentId == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("/Student", student).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("/api/Student/" + student.StudentId, courseEnrolment).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int studentId)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("/Student/" + studentId.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
