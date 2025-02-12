using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PersoneManagement.Web.Models.DTO
{
    public class PersonDTO
    {
        [Key]
        public int BusinessEntityID { get; set; }

        [Required(ErrorMessage = "Type is Required")]
        [DisplayName("Type")]
        public string PersonType { get; set; }

        [Required(ErrorMessage = "Name Style is Required")]
        [DisplayName("Name Style")]
        public bool NameStyle { get; set; }

        public string Title { get; set; }

        [Required(ErrorMessage = "First name is Required")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Suffix { get; set; }

        [DisplayName("Email Promotion")]
        [Required(ErrorMessage = "Email Promotion is Required")]
        [Range(0, 2, ErrorMessage = "Range is over limit")]
        public int EmailPromotion { get; set; }

        public System.Guid rowguid { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm:ss tt}")]
        [DisplayName("Modified Date")]
        public System.DateTime ModifiedDate { get; set; }

        //public IEnumerable<SelectListItem> BusinessEntity { get; set; }

        public string FullName { get; set; }
    }
}