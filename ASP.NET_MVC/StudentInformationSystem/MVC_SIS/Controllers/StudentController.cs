using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercises.Models.Data;
using Exercises.Models.ViewModels;

namespace Exercises.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = StudentRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new StudentVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            viewModel.SetStateItems(StateRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(StudentVM studentVM)
        {
            if(ModelState.IsValid)
            {
                studentVM.Student.Courses = new List<Course>();

                foreach (var id in studentVM.SelectedCourseIds)
                    studentVM.Student.Courses.Add(CourseRepository.Get(id));
                studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);
                

                StudentRepository.Add(studentVM.Student);
                return RedirectToAction("List");
            }
            else
            {
                
                studentVM.SetMajorItems(MajorRepository.GetAll());
                studentVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.SetStateItems(StateRepository.GetAll());

                return View("Add", studentVM);
            }            
        }

        [HttpGet]
        public ActionResult Edit(int studentId)
        {
            var student = StudentRepository.Get(studentId);
            var viewModel = new StudentAddressVM();
            viewModel.Student = student;
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            viewModel.SetStateItems(StateRepository.GetAll());

            if(student.Courses != null)
            {
                viewModel.SelectedCourseIds = (from c in student.Courses
                                               select c.CourseId).ToList();
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(StudentAddressVM studentAddressVM)
        {
            if(ModelState.IsValid)
            {
                studentAddressVM.Student.Courses = new List<Course>();

                foreach (var id in studentAddressVM.SelectedCourseIds)
                {
                    studentAddressVM.Student.Courses.Add(CourseRepository.Get(id));
                }
                studentAddressVM.Student.Major = MajorRepository.Get(studentAddressVM.Student.Major.MajorId);
                studentAddressVM.Student.Address.State = StateRepository.Get(studentAddressVM.Student.Address.State.StateAbbreviation);
                StudentRepository.SaveAddress(studentAddressVM.Student.StudentId, studentAddressVM.Student.Address);
                StudentRepository.Edit(studentAddressVM.Student);

                return RedirectToAction("List");
            }
            else
            {
                studentAddressVM.SetCourseItems(CourseRepository.GetAll());
                studentAddressVM.SetMajorItems(MajorRepository.GetAll());
                studentAddressVM.SetStateItems(StateRepository.GetAll());

                return View("Edit", studentAddressVM);
            }
        }

        [HttpGet]
        public ActionResult Delete(int studentId)
        {
            var student = StudentRepository.Get(studentId);
            var viewModel = new StudentDeleteVM();
            viewModel.Student = student;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(StudentDeleteVM studentDeleteVM)
        {
            StudentRepository.Delete(studentDeleteVM.Student.StudentId);
            return RedirectToAction("List");
        }
    }
}