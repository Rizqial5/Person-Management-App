
using Microsoft.Ajax.Utilities;
using PersoneManagement.Web.Models;
using PersoneManagement.Web.Models.DTO;
using PersoneManagement.Web.Models.Interfaces;
using PersoneManagement.Web.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersoneManagement.Web.Controllers
{
    public class AddressController : Controller
    {
        private IAddressRepository _addressRepository;
        private IPersonRepository _personRepository;
        private IStateProvinceRepository _stateProvinceRepository;

        public AddressController() : this(new AddressRepository(), new PersonRepository(), new StateProvinceRepository()) { }
        public AddressController(IAddressRepository addressRepository, IPersonRepository personRepository, IStateProvinceRepository stateProvinceRepository)
        {
            _addressRepository = addressRepository;
            _personRepository = personRepository;
            _stateProvinceRepository = stateProvinceRepository;
        }

        // GET: Address
        public ActionResult Index(int personID)
        {
            ViewBag.PersonID = personID;

       

            ViewBag.Name = _personRepository.GetFullName(personID);

            return View();
        }

        public JsonResult GetAddress(int businessEntityId)
        {
            var addressData = _addressRepository.GetAddressesByBusinessId(businessEntityId);

            var json = Json(new { data = addressData }, JsonRequestBehavior.AllowGet);

            json.MaxJsonLength = int.MaxValue;

            return json;
        }


        // GET: Address/Create
        public ActionResult Create(int personID)
        {

            ShowProvinceList();
            ShowTypesList(); 

            return View();
        }

        // POST: Address/Create
        [HttpPost]
        public ActionResult Create(AddressDTO addressDTO, int personID)
        {
            try
            {

                // TODO: Add insert logic here
                _addressRepository.AddAddress(addressDTO, personID);


                TempData["SuccessMessage"] = "Customer created succesfully";
                return RedirectToAction("Index", new {personID = personID});
            }
            catch (Exception ex) 
            {
                ShowProvinceList();
                ShowTypesList();
                ModelState.AddModelError("", "Error creating person: " + ex.Message);
                return View();
            }
        }

        // GET: Address/Edit/5
        public ActionResult Edit(int id, int personId)
        {

            var address = _addressRepository.GetAddressById(id, personId);

            if (address == null) return HttpNotFound();

            ShowProvinceList();
            ShowTypesList();

            var result = Mapping.Mapper.Map<AddressDTO>(address);

            return View(result);
        }

        // POST: Address/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AddressDTO addressDTO, int personId)
        {
            try
            {
                // TODO: Add update logic here
                var existingData = _addressRepository.GetAddressById(id, personId);

                var existingRowGuid = existingData.rowguid;

                if (existingData == null)
                {
                    return HttpNotFound();
                }

                if (ModelState.IsValid)
                {
                    _addressRepository.UpdateAddress(addressDTO, personId, existingRowGuid);
                    TempData["SuccessMessage"] = "Address Updated succesfully";
                }

                return RedirectToAction("Index", new { personID = personId });
            }
            catch(Exception ex) 
            {
                ShowProvinceList();
                ShowTypesList();
                ModelState.AddModelError("", "Unable to update due to " + ex.Message);
                return View();
            }
        }

        // GET: Address/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Address/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, AddressDTO addressDTO, int personID)
        {
            try
            {
                // TODO: Add delete logic here

                var existingData = _addressRepository.GetAddressById(id, personID);

                if (existingData == null)
                {
                    return HttpNotFound();
                }

                _addressRepository.DeleteAddress(id, personID);

                return Json(new
                {
                    success = true
                }
                );
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", "Unable to delete due to " + ex.Message);
                return Json(new
                {
                    success = false
                }
                );
            }
        }

        public void ShowTypesList()
        {
            var dropDownType = _addressRepository.GetAddressTypes();

            ViewBag.typeList = new SelectList(dropDownType, "AddressTypeID", "Name");
        }
  

        public void ShowProvinceList()
        {
            var dropDownsProvince = _stateProvinceRepository.GetAll();

            ViewBag.provinceList = new SelectList(dropDownsProvince, "StateProvinceID", "Name");
        }
    }
}
