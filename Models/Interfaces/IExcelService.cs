using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PersoneManagement.Web.Models.Interfaces
{
    public interface IExcelService
    {
        Workbook StateProvinceExportToExcel();
        Workbook PersonExportToExcel();
        Workbook AddressExportToExcel(int businessEntityId);
        void ImportStateProvinceExcelToDatabase(HttpPostedFileBase file);
        void ImportPersonExcelToDatabase(HttpPostedFileBase file);
        void ImportAddressExcelToDatabase(HttpPostedFileBase file, int businessEntityId);



    }
}
