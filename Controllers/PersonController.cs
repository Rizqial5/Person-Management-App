

using PersoneManagement.Web.Models;
using PersoneManagement.Web.Models.DTO;
using PersoneManagement.Web.Models.Interfaces;
using PersoneManagement.Web.Models.Repositories;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;


namespace PersoneManagement.Web.Controllers
{
    public class PersonController : Controller, IDisposable
    {

        private IPersonRepository _personRepository;

        public PersonController() : this(new PersonRepository()) { }

        public PersonController(IPersonRepository personRepository)
        {
           _personRepository = personRepository;
        }
        // GET: Person
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPersons()
        {
            var personsData = _personRepository.GetPersonLists();

            var json = Json(new { data = personsData }, JsonRequestBehavior.AllowGet);

            json.MaxJsonLength = int.MaxValue;

            return json;
        }

        // GET: Person/Details/5
        public ActionResult Details(int id)
        {
            var personData = _personRepository.GetPerson(id);

            if(personData == null)
            {
                return HttpNotFound();
            }

            var result = Mapping.Mapper.Map<PersonDTO>(personData);

            return View(result);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(PersonDTO personDTO)
        {
                try
                {
                    // Insert into BusinessEntity
                   
                    _personRepository.CreatePerson(personDTO);

                    // Commit transaction
                    

                    TempData["SuccessMessage"] = "Customer created succesfully";


                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Rollback transaction
                  
                    ModelState.AddModelError("", "Error creating person: " + ex.Message);
                    return View();
                }
            
        }
        

        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {

            var person = _personRepository.GetPerson(id);

            if(person == null)
            {
                return HttpNotFound();
            }

            

            var result = Mapping.Mapper.Map<PersonDTO>(person);

            return View(result);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PersonDTO personDTO)
        {
            try
            {
                // TODO: Add update logic here

                var existingData = _personRepository.GetPerson(personDTO.BusinessEntityID);

                var existingRowGuid = existingData.rowguid;

                if (existingData == null)
                {
                    return HttpNotFound();
                }

                if (ModelState.IsValid)
                {
                    _personRepository.UpdatePerson(personDTO,existingRowGuid);
                    TempData["SuccessMessage"] = "Person Updated succesfully";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to update due to " + ex.Message);
                return View();
            }
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                var existingData = _personRepository.GetPerson(id);

                if (existingData == null)
                {
                    return HttpNotFound();
                }

                _personRepository.DeletePerson(id);
                    
                return Json(new
                {
                    success = true
                }
);
            }
            catch(Exception ex) 
            {
                TempData["ErrorMessage"] = "Person deleted failed " + ex.Message;

                ModelState.AddModelError("", "Unable to update due to " + ex.Message);
                return Json(new
                {
                    success = false,
                    message = "Person deleted failed " + ex.Message
                }
                );
            }
        }
    }
}
