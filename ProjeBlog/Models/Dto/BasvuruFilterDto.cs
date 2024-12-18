using System.ComponentModel.DataAnnotations;
using System;

namespace ProjeBlog.Models.Dto
{
    public class BasvuruFilterDto
    {
        public string FirstName { get; set; }  
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int ItemsPerPage {  get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
