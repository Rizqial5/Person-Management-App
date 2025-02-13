using PersoneManagement.Web.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aspose.Cells;
using PersoneManagement.Web.StateProvinceService;
using PersoneManagement.Web.AddressService;
using PersoneManagement.Web.PersonService;

namespace PersoneManagement.Web.Models.Repositories
{
    public class ExcelService : IExcelService
    {
        private StateProvinceServiceClient _stateProvinceClient;
        private AddressServiceClient _addressServiceClient;
        private PersonServiceClient _personServiceClient;

        public ExcelService()
        {
            _stateProvinceClient = new StateProvinceServiceClient();
            _addressServiceClient = new AddressServiceClient();
            _personServiceClient = new PersonServiceClient();
        }

        #region Import Region

        public void ImportStateProvinceExcelToDatabase(HttpPostedFileBase file)
        {
            Workbook workbook = new Workbook(file.InputStream);
            Worksheet worksheet = workbook.Worksheets[0];

            //Check and input data from excel to database
            for (int i = 1; i < worksheet.Cells.MaxDataRow; i++)
            {

                //initialisasi dan input data berdasaarkan row[i] excel
                int StateProvinceId = ConvertStatePRovince(worksheet, i);
                string Name = worksheet.Cells[i, 1].StringValue;
                string Code = worksheet.Cells[i, 2].StringValue;
                string TerritorryRegion = worksheet.Cells[i, 3].StringValue;
                bool Flag = ConvertFlag(worksheet.Cells[i, 4].StringValue);

                var parts = TerritorryRegion.Split(new[] { " - " }, StringSplitOptions.None);
                string territoryName = parts[0];
                string regionname = parts[1];

                var territoryId = _stateProvinceClient.GetTerritoriesIdByName(territoryName);
                var regionCode = _stateProvinceClient.GetCountryRegionIdByName(regionname);

                Guid oldGuid = Guid.Empty;

                if (_stateProvinceClient.GetStateProvinceById(StateProvinceId) != null)
                {
                    oldGuid = _stateProvinceClient.GetStateProvinceById(StateProvinceId).rowguid;
                }


                // masukkan data ke new StateProvinceDTO
                var stateProvince = new StateProvinceDTO
                {
                    StateProvinceID = StateProvinceId,
                    Name = Name,
                    StateProvinceCode = Code,
                    TerritoryID = territoryId,
                    CountryRegionCode = regionCode,
                    IsOnlyStateProvinceFlag = Flag,
                    rowguid = oldGuid
                };


                _stateProvinceClient.ImportFromExcel(stateProvince);
            }
        }

        private bool ConvertFlag(string flag)
        {
            if (flag == "Yes") return true;

            return false;

        }

        private int ConvertStatePRovince(Worksheet worksheet, int i)
        {
            if (worksheet.Cells[i, 0].Value != null)
            {
                return worksheet.Cells[i, 0].IntValue;
            }

            return 0;
        }

        public void ImportAddressExcelToDatabase(HttpPostedFileBase file, int businessEntityId)
        {
            var workbook = new Workbook(file.InputStream);
            var worksheet = workbook.Worksheets[0];

            //Check and input data from excel to database
            for (int i = 1; i <= worksheet.Cells.MaxDataRow; i++)
            {
                //initialisasi dan input data berdasaarkan row[i] excel
                int AddressID = ConvertAddress(worksheet, i);
                string Type = worksheet.Cells[i, 1].StringValue;
                string AddressLine1 = worksheet.Cells[i, 2].StringValue;
                string AddressLine2 = worksheet.Cells[i, 3].StringValue;
                string City = worksheet.Cells[i, 4].StringValue;
                string StateProvince = worksheet.Cells[i, 5].StringValue;
                string PostalCode = worksheet.Cells[i, 6].StringValue;

                var stateProvinceId = _stateProvinceClient.GetStateProvinceIdByName(StateProvince);
                var addressTypeId = _addressServiceClient.GetAddressTypeIdByName(Type).ToString();


                var oldGuid = Guid.Empty;

                if (_addressServiceClient.GetAddressById(AddressID, businessEntityId) != null)
                {
                    oldGuid = _addressServiceClient.GetAddressById(AddressID, businessEntityId).rowguid;
                }
                // masukkan data ke new AddressDTO
                var address = new AddressDTO
                {
                    AddressID = AddressID,
                    AddressLine1 = AddressLine1,
                    AddressLine2 = AddressLine2,
                    City = City,
                    StateProvinceID = stateProvinceId,
                    PostalCode = PostalCode,
                    BusinessEntityID = businessEntityId,
                    rowguid = oldGuid,
                    AddressesTypeName = addressTypeId
                };

                _addressServiceClient.ImportFromExcel(address);
            }
        }

        private int ConvertAddress(Worksheet worksheet, int i)
        {
            if(worksheet.Cells[i, 0].Value != null)
            {
                return worksheet.Cells[i, 0].IntValue;
            }
            else
            {
                return 0;
            }
        }

        public void ImportPersonExcelToDatabase(HttpPostedFileBase file)
        {
            throw new NotImplementedException();
        }


        #endregion


        #region Export Region
        public Workbook StateProvinceExportToExcel()
        {
            var data = _stateProvinceClient.GetAll().Select(sp => new
            {
                StateProvinceID = sp.StateProvinceID,
                Name = sp.Name,
                Code = sp.StateProvinceCode,
                TerritoryRegion = sp.TerritorryNames + " - " + sp.CountryRegionNames,
                Flag = sp.IsOnlyStateProvinceFlag,
                RowGUID = sp.rowguid
            });


            //dropdown
            var territoryRegion = data.Select(d => d.TerritoryRegion).ToList(); // Ganti ngambilnya

            // new Workbook

            Workbook workBook = new Workbook();
            Worksheet workSheet = workBook.Worksheets[0];

            int[] maxColumnWidths = new int[5];

            workSheet.Name = "State Provinces";


            //Add Header
            workSheet.Cells[0, 0].PutValue("StateProvinceId");
            workSheet.Cells[0, 1].PutValue("Name");
            workSheet.Cells[0, 2].PutValue("State Province Code");
            workSheet.Cells[0, 3].PutValue("Territorry - Region");
            workSheet.Cells[0, 4].PutValue("Flag");
            workSheet.Cells[0, 5].PutValue("RowGuid");

            

            //Set for customize column width            
            maxColumnWidths[1] = "Name".Length;
            maxColumnWidths[2] = "State Province Code".Length;
            maxColumnWidths[3] = "Territorry - Region".Length;
            maxColumnWidths[4] = "Flag".Length;

            //Set Hide Column ID and RowGuid
            workSheet.Cells.HideColumn(0);
            workSheet.Cells.HideColumn(5);
          


            // add unlock style

            Style unlockedStyle = workSheet.Workbook.CreateStyle();
            unlockedStyle.HorizontalAlignment = TextAlignmentType.Center;
            unlockedStyle.IsLocked = false;
            workSheet.Cells.ApplyStyle(unlockedStyle, new StyleFlag() { Locked = true });


            // add lock style
            Style style = workSheet.Workbook.CreateStyle();
            StyleFlag styleFlag = new StyleFlag();
            style.IsLocked = true;
            styleFlag.Locked = true;
            workSheet.Cells.Columns[0].ApplyStyle(style, styleFlag); // StateProvinceID
            workSheet.Cells.Columns[5].ApplyStyle(style, styleFlag); // RowGUID

            workSheet.Protect(ProtectionType.All);


            // Populate Data
            for (int i = 0; i < data.Count(); i++)
            {
                workSheet.Cells[i + 1, 0].PutValue(data.ToList()[i].StateProvinceID);
                workSheet.Cells[i + 1, 1].PutValue(data.ToList()[i].Name);
                workSheet.Cells[i + 1, 2].PutValue(data.ToList()[i].Code);
                workSheet.Cells[i + 1, 3].PutValue(data.ToList()[i].TerritoryRegion);
                workSheet.Cells[i + 1, 4].PutValue(SetBoolFlag(data.ToList()[i].Flag));
                workSheet.Cells[i + 1, 5].PutValue(data.ToList()[i].RowGUID);


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

            // Populate Territory Region to master sheet
            for (int i = 0; i < territoryRegion.Count; i++)
            {
                masterSheet.Cells[i, 0].PutValue(territoryRegion[i]);
            }

            // Set master sheet to very hidden
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

            //Data Validation State PRovince Code
            int stateProvinceCode = 2; // Index kolom state Province Code
            CellArea stateProvinceArea = CellArea.CreateCellArea(1, stateProvinceCode, 1000, stateProvinceCode);
            Validation stateProvinceValidation = workSheet.Validations[workSheet.Validations.Add(stateProvinceArea)];
            stateProvinceValidation.Type = ValidationType.TextLength;
            stateProvinceValidation.Operator = OperatorType.Between;
            stateProvinceValidation.Formula1 = "1"; // min length
            stateProvinceValidation.Formula2 = "3"; // max length
            stateProvinceValidation.ShowError = true;
            stateProvinceValidation.AlertStyle = ValidationAlertType.Stop;
            stateProvinceValidation.ErrorTitle = "Invalid input";
            stateProvinceValidation.ErrorMessage = "The text length max is 3 characters.";

            //Data Validation Name 50 length kolom index 1
            int nameColumnIndex = 1;
            CellArea nameCellArea = CellArea.CreateCellArea(1, nameColumnIndex, 1000, nameColumnIndex);
            Validation nameValidation = workSheet.Validations[workSheet.Validations.Add(nameCellArea)];
            nameValidation.Type = ValidationType.TextLength;
            nameValidation.Operator = OperatorType.Between;
            nameValidation.Formula1 = "1";
            nameValidation.Formula2 = "50";
            nameValidation.ShowError = true;
            nameValidation.AlertStyle = ValidationAlertType.Stop;
            nameValidation.ErrorTitle = "Invalid Input";
            nameValidation.ErrorMessage = "The text length max is 50 characters.";




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

        public Workbook AddressExportToExcel(int businessEntityId)
        {
            var data = _addressServiceClient.GetAddressesByBusinessId(businessEntityId)
                .Select(a => new
                {
                    AddressID = a.AddressID,
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    City = a.City,
                    PostalCode = a.PostalCode,
                    StatesProvinceName = a.StatesProvinceName,
                    AddressesTypeName = a.AddressesTypeName
                });

            var stateProvinceDD = _stateProvinceClient.GetAll().Select(sp=> sp.Name).ToList();
            var addressTypeDD = _addressServiceClient.GetAddressTypes().Select(a=> a.Name).ToList();

            Workbook workBook = new Workbook();
            Worksheet workSheet = workBook.Worksheets[0];

            int[] maxColumnWidths = new int[7];

            workSheet.Name = "Address";

            //Add Header
            workSheet.Cells[0, 0].PutValue("AddressID");
            workSheet.Cells[0, 1].PutValue("Type");
            workSheet.Cells[0, 2].PutValue("Address Line 1");
            workSheet.Cells[0, 3].PutValue("Address Line 2");
            workSheet.Cells[0, 4].PutValue("City");
            workSheet.Cells[0, 5].PutValue("State Province");
            workSheet.Cells[0, 6].PutValue("Postal Code");

            //set for customize column width
            maxColumnWidths[1] = "Type".Length;
            maxColumnWidths[2] = "Address Line 1".Length;
            maxColumnWidths[3] = "Address Line 2".Length;
            maxColumnWidths[4] = "City".Length;
            maxColumnWidths[5] = "State Province".Length;
            maxColumnWidths[6] = "Postal Code".Length;

            //Set Hide Column ID
            workSheet.Cells.HideColumn(0);

            // add unlock style
            Style unlockedStyle = workSheet.Workbook.CreateStyle();
            unlockedStyle.HorizontalAlignment = TextAlignmentType.Center;
            unlockedStyle.IsLocked = false;
            workSheet.Cells.ApplyStyle(unlockedStyle, new StyleFlag() { Locked = true });

            // add lock style
            Style style = workSheet.Workbook.CreateStyle();
            StyleFlag styleFlag = new StyleFlag();
            style.IsLocked = true;
            styleFlag.Locked = true;
            workSheet.Cells.Columns[0].ApplyStyle(style, styleFlag); // AddressID

            workSheet.Protect(ProtectionType.All);

            // Populate Data
            for (int i = 0; i < data.Count(); i++)
            {
                workSheet.Cells[i + 1, 0].PutValue(data.ToList()[i].AddressID);
                workSheet.Cells[i + 1, 1].PutValue(data.ToList()[i].AddressesTypeName);
                workSheet.Cells[i + 1, 2].PutValue(data.ToList()[i].AddressLine1);
                workSheet.Cells[i + 1, 3].PutValue(data.ToList()[i].AddressLine2);
                workSheet.Cells[i + 1, 4].PutValue(data.ToList()[i].City);
                workSheet.Cells[i + 1, 5].PutValue(data.ToList()[i].StatesProvinceName);
                workSheet.Cells[i + 1, 6].PutValue(data.ToList()[i].PostalCode);

                maxColumnWidths[1] = Math.Max(maxColumnWidths[1], data.ToList()[i].AddressesTypeName.Length);
                maxColumnWidths[2] = Math.Max(maxColumnWidths[2], data.ToList()[i].AddressLine1.Length);
                maxColumnWidths[3] = Math.Max(maxColumnWidths[3], data.ToList()[i].AddressLine2.Length);
                maxColumnWidths[4] = Math.Max(maxColumnWidths[4], data.ToList()[i].City.Length);
                maxColumnWidths[5] = Math.Max(maxColumnWidths[5], data.ToList()[i].StatesProvinceName.Length);
                maxColumnWidths[6] = Math.Max(maxColumnWidths[6], data.ToList()[i].PostalCode.Length);
            }

            for (int col = 1; col < maxColumnWidths.Length; col++)
            {
                // Tambahkan padding agar ada ruang di sekitar teks
                workSheet.Cells.SetColumnWidth(col, maxColumnWidths[col] + 2);
            }

            // Master Sheet
            Worksheet masterSheet = workBook.Worksheets.Add("Master");

            //populate State Province to master sheet
            for (int i = 0; i < stateProvinceDD.Count(); i++)
            {
                masterSheet.Cells[i, 0].PutValue(stateProvinceDD[i]);
                
            }

            for (int i = 0; i < addressTypeDD.Count(); i++)
            {
                masterSheet.Cells[i, 1].PutValue(addressTypeDD[i]);
            }

            // Set master sheet to very hidden
            masterSheet.VisibilityType = VisibilityType.VeryHidden;

            //Data Validation State Province
            int stateProvinceColumnIndex = 5;
            CellArea stateProvinceArea = CellArea.CreateCellArea(1, stateProvinceColumnIndex, 1000, stateProvinceColumnIndex);
            Validation stateProvinceValidation = workSheet.Validations[workSheet.Validations.Add(stateProvinceArea)];
            stateProvinceValidation.Type = ValidationType.List;
            stateProvinceValidation.Formula1 = "=Master!$A$1:$A$" + stateProvinceDD.Count();

            //Data Validation Address Type
            int addressTypeColumnIndex = 1;
            CellArea addressTypeArea = CellArea.CreateCellArea(1, addressTypeColumnIndex, 1000, addressTypeColumnIndex);
            Validation addressTypeValidation = workSheet.Validations[workSheet.Validations.Add(addressTypeArea)];
            addressTypeValidation.Type = ValidationType.List;
            addressTypeValidation.Formula1 = "=Master!$B$1:$B$" + addressTypeDD.Count();

            //Data Validation Address Line 1
            int addressLine1ColumnIndex = 2;
            CellArea addressLine1Area = CellArea.CreateCellArea(1, addressLine1ColumnIndex, 1000, addressLine1ColumnIndex);
            Validation addressLine1Validation = workSheet.Validations[workSheet.Validations.Add(addressLine1Area)];
            addressLine1Validation.Type = ValidationType.TextLength;
            addressLine1Validation.Operator = OperatorType.Between;
            addressLine1Validation.Formula1 = "1";
            addressLine1Validation.Formula2 = "60";
            addressLine1Validation.ShowError = true;
            addressLine1Validation.AlertStyle = ValidationAlertType.Stop;
            addressLine1Validation.ErrorTitle = "Invalid Input";
            addressLine1Validation.ErrorMessage = "The text length max is 60 characters.";

            //Data Validation Address Line 2
            int addressLine2ColumnIndex = 3;
            CellArea addressLine2Area = CellArea.CreateCellArea(1, addressLine2ColumnIndex, 1000, addressLine2ColumnIndex);
            Validation addressLine2Validation = workSheet.Validations[workSheet.Validations.Add(addressLine2Area)];
            addressLine2Validation.Type = ValidationType.TextLength;
            addressLine2Validation.Operator = OperatorType.Between;
            addressLine2Validation.Formula1 = "1";
            addressLine2Validation.Formula2 = "60";
            addressLine2Validation.ShowError = true;
            addressLine2Validation.AlertStyle = ValidationAlertType.Stop;
            addressLine2Validation.ErrorTitle = "Invalid Input";
            addressLine2Validation.ErrorMessage = "The text length max is 60 characters.";

            //Data Validation City
            int cityColumnIndex = 4;
            CellArea cityArea = CellArea.CreateCellArea(1, cityColumnIndex, 1000, cityColumnIndex);
            Validation cityValidation = workSheet.Validations[workSheet.Validations.Add(cityArea)];
            cityValidation.Type = ValidationType.TextLength;
            cityValidation.Operator = OperatorType.Between;
            cityValidation.Formula1 = "1";
            cityValidation.Formula2 = "30";
            cityValidation.ShowError = true;
            cityValidation.AlertStyle = ValidationAlertType.Stop;
            cityValidation.ErrorTitle = "Invalid Input";
            cityValidation.ErrorMessage = "The text length max is 30 characters.";

            //Data Validation Postal Code
            int postalCodeColumnIndex = 6;
            CellArea postalCodeArea = CellArea.CreateCellArea(1, postalCodeColumnIndex, 1000, postalCodeColumnIndex);
            Validation postalCodeValidation = workSheet.Validations[workSheet.Validations.Add(postalCodeArea)];
            postalCodeValidation.Type = ValidationType.TextLength;
            postalCodeValidation.Operator = OperatorType.Between;
            postalCodeValidation.Formula1 = "1";
            postalCodeValidation.Formula2 = "15";
            postalCodeValidation.ShowError = true;
            postalCodeValidation.AlertStyle = ValidationAlertType.Stop;
            postalCodeValidation.ErrorTitle = "Invalid Input";
            postalCodeValidation.ErrorMessage = "The text length max is 15 characters.";


            return workBook;




        }

        public Workbook PersonExportToExcel()
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}