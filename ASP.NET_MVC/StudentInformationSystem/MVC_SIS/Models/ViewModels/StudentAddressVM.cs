using Exercises.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercises.Models.ViewModels
{
    public class StudentAddressVM : IValidatableObject
    {
        public Student Student { get; set; }
        public List<SelectListItem> CourseItems { get; set; }
        public List<SelectListItem> MajorItems { get; set; }
        public List<SelectListItem> StateItems { get; set; }
        public List<int> SelectedCourseIds { get; set; }

        public StudentAddressVM()
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

            // Student info validation
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
            if (Student.GPA < 0 || Student.GPA > 4)
            {
                errors.Add(new ValidationResult("GPA must be between 0.0 and 4.0", new[] { "Student.GPA" }));
            }
            if (SelectedCourseIds.Count == 0)
            {
                errors.Add(new ValidationResult("Please select at least one course", new[] { "SelectedCourseIds" }));
            }

            // Student Address validation
            if (string.IsNullOrEmpty(Student.Address.PostalCode))
            {
                errors.Add(new ValidationResult("Please enter the postal code", new[] { "Student.Address.PostalCode" }));
            }           
            else if (Student.Address.PostalCode.Length != 5)
            {
                errors.Add(new ValidationResult("Postal code must be 5 numeric digits", new[] { "Student.Address.PostalCode" }));
            }            
            else foreach (char c in Student.Address.PostalCode)
            {
                if (Char.IsLetter(c))
                {
                    errors.Add(new ValidationResult("Postal code cannot contain letters", new[] { "Student.Address.PostalCode" }));
                }
            }
            
            if (string.IsNullOrEmpty(Student.Address.Street1))
            {
                errors.Add(new ValidationResult("Please enter an address", new[] { "Student.Address.Street1" }));
            }
            if (string.IsNullOrEmpty(Student.Address.City))
            {
                errors.Add(new ValidationResult("Please enter a city", new[] { "Student.Address.City" }));
            }
            if(Student.Address.State.StateAbbreviation == null)
            {
                errors.Add(new ValidationResult("Please select a state", new[] { "Student.Address.State.StateAbbreviation" }));
            }

            return errors;
        }
    }
}