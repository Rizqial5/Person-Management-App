using PersoneManagement.Web.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aspose.Cells;
using PersoneManagement.Web.StateProvinceService;

namespace PersoneManagement.Web.Models.Repositories
{
    public class ExcelService : IExcelService
    {
        private StateProvinceServiceClient _stateProvinceClient;

        public ExcelService()
        {
            _stateProvinceClient = new StateProvinceServiceClient();
        }


        public Workbook StateProvinceExportToExcel()
        {
            var data = _stateProvinceClient.GetAll().Select(sp => new
            {
                StateProvinceID = sp.StateProvinceID,
                Name = sp.Name,
                Code = sp.StateProvinceCode,
                TerritoryRegion = sp.TerritorryNames + " - " + sp.CountryRegionNames,
                Flag = sp.IsOnlyStateProvinceFlag
            });


            var territoryRegion = data.Select(d => d.TerritoryRegion).ToList();

            // new Workbook

            Workbook workBook = new Workbook();
            Worksheet workSheet = workBook.Worksheets[0];

            int[] maxColumnWidths = new int[5];



            workSheet.Name = "State Provinces";


            //style settings



            //Add Header

            workSheet.Cells[0, 0].PutValue("StateProvinceId");
            workSheet.Cells.HideColumn(0);
            //workSheet.Cells[0, 0].;
            workSheet.Cells[0, 1].PutValue("Name");
            workSheet.Cells[0, 2].PutValue("State Province Code");
            workSheet.Cells[0, 3].PutValue("Territorry - Region");
            workSheet.Cells[0, 4].PutValue("Flag");

            maxColumnWidths[1] = "Name".Length;
            maxColumnWidths[2] = "State Province Code".Length;
            maxColumnWidths[3] = "Territorry - Region".Length;
            maxColumnWidths[4] = "Flag".Length;



            // add lock style

            Style unlockedStyle = workSheet.Workbook.CreateStyle();
            unlockedStyle.HorizontalAlignment = TextAlignmentType.Center;
            unlockedStyle.IsLocked = false;
            workSheet.Cells.ApplyStyle(unlockedStyle, new StyleFlag() { Locked = true });


            Style style = workSheet.Workbook.CreateStyle();
            StyleFlag styleFlag = new StyleFlag();
            style.IsLocked = true;
            styleFlag.Locked = true;
            workSheet.Cells.Columns[0].ApplyStyle(style, styleFlag);

            workSheet.Protect(ProtectionType.All);



            for (int i = 0; i < data.Count(); i++)
            {
                workSheet.Cells[i + 1, 0].PutValue(data.ToList()[i].StateProvinceID);
                workSheet.Cells[i + 1, 1].PutValue(data.ToList()[i].Name);
                workSheet.Cells[i + 1, 2].PutValue(data.ToList()[i].Code);
                workSheet.Cells[i + 1, 3].PutValue(data.ToList()[i].TerritoryRegion);
                workSheet.Cells[i + 1, 4].PutValue(SetBoolFlag(data.ToList()[i].Flag));


                maxColumnWidths[1] = Math.Max(maxColumnWidths[1], data.ToList()[i].Name.Length);
                maxColumnWidths[2] = Math.Max(maxColumnWidths[2], data.ToList()[i].Code.Length);
                maxColumnWidths[3] = Math.Max(maxColumnWidths[3], data.ToList()[i].TerritoryRegion.Length);
                maxColumnWidths[4] = Math.Max(maxColumnWidths[4], data.ToList()[i].Flag.ToString().Length);


            }

            for (int col = 1; col < maxColumnWidths.Length; col++)
            {
                // Tambahkan padding agar ada ruang di sekitar teks
                workSheet.Cells.SetColumnWidth(col, maxColumnWidths[col] + 5);
            }

            // Master Sheet
            Worksheet masterSheet = workBook.Worksheets.Add("Master");

            for (int i = 0; i < territoryRegion.Count; i++)
            {
                masterSheet.Cells[i, 0].PutValue(territoryRegion[i]);
            }

            masterSheet.VisibilityType = VisibilityType.VeryHidden;




            //data validation untuk tipe integer /number

            //Data validation territoryRegion
            int territoryRegionColumnIndex = 3;
            CellArea territoryRegionArea = CellArea.CreateCellArea(1, territoryRegionColumnIndex, 1000, territoryRegionColumnIndex);
            Validation territoryValidation = workSheet.Validations[workSheet.Validations.Add(territoryRegionArea)];
            territoryValidation.Type = ValidationType.List;
            territoryValidation.Formula1 = "=Master!$A$1:$A$" + territoryRegion.Count;

            //Data Validation flag boolean
            int flagColumnIndex = 4;
            CellArea flagArea = CellArea.CreateCellArea(1, flagColumnIndex, 1000, flagColumnIndex);
            Validation flagValidation = workSheet.Validations[workSheet.Validations.Add(flagArea)];
            flagValidation.Type = ValidationType.List;
            flagValidation.Formula1 = "Yes, No";

            return workBook;
        }

        private string SetBoolFlag(bool flag)
        {
            if (flag)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }

        }
    }
}