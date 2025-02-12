using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PersoneManagement.Web.Models.DTO
{
    public class AddressDTO
    {
        [Key]
        public int AddressID { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        [DisplayName("Address Line 1")]
        public string AddressLine1 { get; set; }

        [DisplayName("Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "City is Required")]
        public string City { get; set; }

        [DisplayName("State Province")]
        [Required(ErrorMessage = "State Province is Required")]
        public int StateProvinceID { get; set; }

        [DisplayName("Postal Code")]
        [Required(ErrorMessage = "Postal Code is Required")]
        public string PostalCode { get; set; }

        //perlu diperhatikan
        public System.Guid rowguid { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm:ss tt}")]
        [DisplayName("Modified Date")]
        public System.DateTime ModifiedDate { get; set; }

        public string StatesProvinceName { get; set; }

        [DisplayName("Type")]
        public string AddressesTypeName { get; set; }

        public int BusinessEntityID { get; set; }

        public string AddressessFullName
        {
            get; set;
        }
    }
}