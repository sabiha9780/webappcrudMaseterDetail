using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppCurdMD.Models;

namespace WebAppCurdMD.ViewModel
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public System.DateTime JoinDate { get; set; }
        public string Gender { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public List<Experience> Experiences { get; set; } = new List<Experience>();

        public string Operation { get; set; }


        public Employee ToEmployee()
        {
            return new Employee()
            {
                Id = this.Id,
                Name = this.Name,
                Designation = this.Designation,
                JoinDate = this.JoinDate,
                Gender = this.Gender,
                Salary = this.Salary,
                IsActive = this.IsActive,
                Experiences = this.Experiences
            };
        }
        public EmployeeVM()
        {

        }
        public EmployeeVM(Employee employee)
        {
            this.Id = employee.Id;
            this.Name = employee.Name;
            this.Designation = employee.Designation;
            this.JoinDate = employee.JoinDate;
            this.Gender = employee.Gender;
            this.Salary = employee.Salary;
            this.IsActive = employee.IsActive;
            this.Experiences =employee.Experiences.ToList();

        }


    }
}