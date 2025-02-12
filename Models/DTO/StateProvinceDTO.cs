
using PersoneManagement.Web.StateProvinceService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersoneManagement.Web.Models.DTO
{
    public class StateProvinceDTO
    {
        [Key]
        public int StateProvinceID { get; set; }


        [DisplayName("Code")]
        [Required(ErrorMessage = "Code is Required")]
        public string StateProvinceCode { get; set; }

        [DisplayName("Country/Region")]
        [Required(ErrorMessage = "Country Region is Required")]
        public string CountryRegionCode { get; set; }

        [DisplayName("Flag")]
        public bool IsOnlyStateProvinceFlag { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [DisplayName("Territory")]
        [Required(ErrorMessage = "Territorry is Required")]
        public int TerritoryID { get; set; }

        //perlu diperhatikan
        public System.Guid rowguid { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm:ss tt}")]
        [DisplayName("Modified Date")]
        public System.DateTime ModifiedDate { get; set; }


        public IEnumerable<CountryRegion> CountriesRegionsDdl { get; set; }

        public IEnumerable<SelectListItem> SalesTerritorriesDdl { get; set; }

        public string Flag
        {
            get
            {
                return IsOnlyStateProvinceFlag ? "Yes" : "No";
            }
        }

        public string TerritoryName { get; set; }
    }
}