﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Exercises.Models.Data;
using System.ComponentModel.DataAnnotations;

namespace Exercises.Models.ViewModels
{
    public class StudentVM : IValidatableObject
    {
        public Student Student { get; set; }
        public List<SelectListItem> CourseItems { get; set; }
        public List<SelectListItem> MajorItems { get; set; }
        public List<SelectListItem> StateItems { get; set; }
        public List<int> SelectedCourseIds { get; set; }

        public StudentVM()
        {
            CourseItems = new List<SelectListItem>();
            MajorItems = new List<SelectListItem>();
            StateItems = new List<SelectListItem>();
            SelectedCourseIds = new List<int>();
            Student = new Student();
        }

        public void SetCourseItems(IEnumerable<Course> courses)
        {
            foreach (var course in courses)
            {
                CourseItems.Add(new SelectListItem()
                {
                    Value = course.CourseId.ToString(),
                    Text = course.CourseName
                });
            }
        }

        public void SetMajorItems(IEnumerable<Major> majors)
        {
            foreach (var major in majors)
            {
                MajorItems.Add(new SelectListItem()
                {
                    Value = major.MajorId.ToString(),
                    Text = major.MajorName
                });
            }
        }

        public void SetStateItems(IEnumerable<State> states)
        {
            foreach (var state in states)
            {
                StateItems.Add(new SelectListItem()
                {
                    Value = state.StateAbbreviation,
                    Text = state.StateName
                });
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Student.FirstName))
            {
                errors.Add(new ValidationResult("Please enter your first name", new[] { "Student.FirstName" }));
            }
            if (string.IsNullOrEmpty(Student.LastName))
            {
                errors.Add(new ValidationResult("Please enter your last name", new[] { "Student.LastName" }));
            }
            if (Student.Major.MajorId == 0)
            {
                errors.Add(new ValidationResult("Please select a major", new[] { "Student.Major.MajorId" }));
            }           
            if(Student.GPA < 0 || Student.GPA > 4)
            {
                errors.Add(new ValidationResult("GPA must be between 0.0 and 4.0", new[] { "Student.GPA" }));
            }
            if(SelectedCourseIds.Count == 0)
            {
                errors.Add(new ValidationResult("Please select at least one course", new[] { "SelectedCourseIds" }));
            }

            return errors;
        }
    }
}