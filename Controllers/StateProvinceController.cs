
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
using System.Runtime.ConstrainedExecution;
using PersoneManagement.Web.Models;


namespace PersoneManagement.Web.Controllers
{
    public class StateProvinceController : Controller
    {
        private IStateProvinceRepository _stateProvinceRepository;
        private IExcelService _exportExcelService;

        public StateProvinceController() : this(new StateProvinceRepository(), new ExcelService()) { }

        public StateProvinceController(IStateProvinceRepository stateProvinceRepository, 
            IExcelService exportExcelService) 
        {
            _stateProvinceRepository = stateProvinceRepository;
            _exportExcelService = exportExcelService;
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

            var workBook = _exportExcelService.StateProvinceExportToExcel();

            //Save Workbook
            var stream = new System.IO.MemoryStream();

            workBook.Save(stream, SaveFormat.Xlsx);

            //return
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StateProvince.xlsx");

        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StateProvinceImportToExcel(HttpPostedFileBase file)
        {
            if(file != null && file.ContentLength >0)
            {
                Workbook workbook = new Workbook(file.InputStream);
                Worksheet worksheet = workbook.Worksheets[0];

                for (int i = 1; i < worksheet.Cells.MaxDataRow; i++)
                {
                    int StateProvinceId = ConvertStatePRovince(worksheet,i);
                    string Name = worksheet.Cells[i, 1].StringValue;
                    string Code = worksheet.Cells[i, 2].StringValue;
                    string TerritorryRegion = worksheet.Cells[i, 3].StringValue;
                    bool Flag = ConvertFlag(worksheet.Cells[i, 4].StringValue);

                    var parts = TerritorryRegion.Split(new[] { " - " }, StringSplitOptions.None);
                    string territoryName = parts[0];
                    string regionname = parts[1];

                    var territoryId = _stateProvinceRepository.GetTerritoriesIdByName(territoryName);
                    var regionCode = _stateProvinceRepository.GetCountryRegionIdByName(regionname);

                    Guid oldGuid = Guid.Empty;
                    if (_stateProvinceRepository.GetStateProvinceById(StateProvinceId) != null)
                    {
                        oldGuid = _stateProvinceRepository.GetStateProvinceById(StateProvinceId).rowguid;
                    }

                    
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


                    _stateProvinceRepository.ImportFromExcel(stateProvince);
                }

                TempData["SuccessMessage"] = "Data imported Succesfully";

                return RedirectToAction("Index");

            }
            return View();
        }

        private bool ConvertFlag(string flag)
        {
            if (flag == "Yes") return true;

            return false;

        }

        private int ConvertStatePRovince(Worksheet worksheet, int i)
        {
            if(worksheet.Cells[i, 0].Value != null)
            {
                return worksheet.Cells[i, 0].IntValue;
            }

            return 0;
        }



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


    }
}
