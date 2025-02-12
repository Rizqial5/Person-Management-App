
using Microsoft.Ajax.Utilities;
using PersoneManagement.Web.Models.DTO;
using PersoneManagement.Web.Models.Interfaces;
using PersoneManagement.Web.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aspose.Cells;
using Aspose.Cells.Drawing;


namespace PersoneManagement.Web.Controllers
{
    public class StateProvinceController : Controller
    {
        private IStateProvinceRepository _stateProvinceRepository;

        public StateProvinceController() : this(new StateProvinceRepository()) { }

        public StateProvinceController(IStateProvinceRepository stateProvinceRepository) 
        {
            _stateProvinceRepository = stateProvinceRepository;
        }

        // GET: StateProvince
        public ActionResult Index()
        {
            //var stateProvincesData = _stateProvinceRepository.GetAll();

            return View();
        }

        public JsonResult GetStates()
        {
            //var stateProvincesData = _stateProvinceRepository.GetStateProvinces();

            var stateProvincesData = _stateProvinceRepository.GetAll();

            return Json(new {data = stateProvincesData}, JsonRequestBehavior.AllowGet);
        }

        // GET: StateProvince/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: StateProvince/Create
        public ActionResult Create()
        {
            ShowRegionsList();

            var stateProvinceDTO = new StateProvinceDTO();

            stateProvinceDTO.SalesTerritorriesDdl = new List<SelectListItem>();

            return View(stateProvinceDTO);
        }

        // POST: StateProvince/Create
        [HttpPost]
        public ActionResult Create(StateProvinceDTO stateProvinceDTO)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _stateProvinceRepository.InsertStateProvince(stateProvinceDTO);
                    TempData["SuccessMessage"] = "Customer created succesfully";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                ShowRegionsList();

                ShowTerritorryListRePop(stateProvinceDTO);
                
                ModelState.AddModelError("", "Unable to Save due to " + ex.Message);
                return View(stateProvinceDTO);
            }
        }

        // GET: StateProvince/Edit/5
        public ActionResult Edit(int id)
        {
            var result = _stateProvinceRepository.GetStateProvinceById(id);

            if (result == null)
            {
                return HttpNotFound();
            }

            StateProvinceDTO transform = RepopulateForm(result);

            return View(transform);
        }

        private StateProvinceDTO RepopulateForm(StateProvinceService.StateProvinceDTO result)
        {
            var transform = new StateProvinceDTO
            {
                StateProvinceID = result.StateProvinceID,
                StateProvinceCode = result.StateProvinceCode,
                CountryRegionCode = result.CountryRegionCode,
                IsOnlyStateProvinceFlag = result.IsOnlyStateProvinceFlag,
                Name = result.Name,
                TerritoryID = result.TerritoryID,
                rowguid = result.rowguid,
                ModifiedDate = DateTime.Now,

            };

            ShowRegionsList();



            transform.SalesTerritorriesDdl = _stateProvinceRepository.GetAllTerritories()
               .Where(t => t.CountryRegionCode == result.CountryRegionCode)
              .Select(t => new SelectListItem
              {
                  Value = t.TerritoryID.ToString(),
                  Text = t.Name,
                  Selected = t.CountryRegionCode == result.CountryRegionCode

              }).ToList();
            return transform;
        }

        // POST: StateProvince/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, StateProvinceDTO stateProvinceDTO)
        {
            var existingData = _stateProvinceRepository.GetStateProvinceById(id);

            try
            {
                // TODO: Add update logic here
                

                if (existingData == null)
                {
                    return HttpNotFound();
                }

                if (ModelState.IsValid)
                {
                    _stateProvinceRepository.UpdateStateProvince(stateProvinceDTO, existingData.rowguid);
                    TempData["SuccessMessage"] = "State Province Updated succesfully";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to update due to " + ex.Message);

                StateProvinceDTO transform = RepopulateForm(existingData);

                return View(transform);
            }
        }

        // GET: StateProvince/Delete/5
        public ActionResult Delete(int id)
        {


            return View();
        }

        // POST: StateProvince/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var existingData = _stateProvinceRepository.GetStateProvinceById(id);

                if (existingData == null)
                {
                    return HttpNotFound();
                }

                _stateProvinceRepository.DeleteStateProvince(id);

                return Json(new
                {
                    success = true
                }
                );


            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", "Unable to update due to " + ex.Message);
                return Json(new
                {
                    success = false
                }
                );
            }
        }

        public ActionResult StateProvinceExportToExcel()
        {
            var data = _stateProvinceRepository.GetAll().Select(sp => new
            {
                StateProvinceID = sp.StateProvinceID,
                Name = sp.Name,
                Code = sp.StateProvinceCode,
                TerritoryRegion = sp.TerritorryNames + " - "+ sp.CountryRegionNames,
                Flag = sp.IsOnlyStateProvinceFlag
            });

            //var countryRegion = data.Select(d => d.CountryName).ToList();
            //var territorryName = data.Select(d => d.Territory).ToList();
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


            

            //Save Workbook
            var stream = new System.IO.MemoryStream();

            workBook.Save(stream, SaveFormat.Xlsx);

            //return
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StateProvince.xlsx");

        }

        //public ActionResult StateProvinceImportToExcel()
        //{
            
        //}



        public JsonResult ShowTerritorryList(string regionCode)
        {
            var dropDownTerriotryList = _stateProvinceRepository.GetTerritoriesByRegionCode(regionCode)
                .Select(t => new SelectListItem
                {
                    Value = t.TerritoryID.ToString(),
                    Text = t.Name
                });

            return Json(dropDownTerriotryList, JsonRequestBehavior.AllowGet);
        }

        public void ShowTerritorryListRePop(StateProvinceDTO stateProvinceDTO)
        {
            stateProvinceDTO.SalesTerritorriesDdl = _stateProvinceRepository.GetAllTerritories()
                           .Where(t => t.CountryRegionCode == stateProvinceDTO.CountryRegionCode)
                          .Select(t => new SelectListItem
                          {
                              Value = t.TerritoryID.ToString(),
                              Text = t.Name,
                              Selected = t.CountryRegionCode == stateProvinceDTO.CountryRegionCode

                          }).ToList();

        }

        public void ShowRegionsList()
        {
            var dropDownRegions = _stateProvinceRepository.GetCountryRegions();
            var dropDownTerritories = _stateProvinceRepository.GetAllTerritories();


            ViewBag.RegionList = new SelectList(dropDownRegions, "CountryRegionCode", "Name");
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
