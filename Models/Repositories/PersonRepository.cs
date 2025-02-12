//using PersoneManagement.Web.Models.DTO;
using Microsoft.Ajax.Utilities;
using PersoneManagement.Web.Models.Interfaces;
using PersoneManagement.Web.PersonService;
using PersoneManagement.Web.StateProvinceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersoneManagement.Web.Models.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private PersonServiceClient _personClient;

        public PersonRepository()
        {
            _personClient = new PersonServiceClient();
        }

        public void CreatePerson(DTO.PersonDTO personDTO)
        {

            var data = Mapping.Mapper.Map<PersonService.PersonDTO>(personDTO);

            _personClient.CreatePerson(data);
        }

        public void DeletePerson(int businessEntityId)
        {
            _personClient.DeletePerson(businessEntityId);
        }

        public PersonDTO GetAddressById(int businessEntityId)
        {
            throw new NotImplementedException();
        }

        public string GetFullName(int businessEntityId)
        {
            var fullName = _personClient.GetFullName(businessEntityId);

            return fullName;
        }



        public PersonDTO GetPerson(int businessEntityId)
        {
            var personData = _personClient.GetPerson(businessEntityId);

            return personData;
        }

        public IEnumerable<PersonDTO> GetPersonLists()
        {
            var data = _personClient.GetPersonLists();

            return data;
        }

        public void UpdatePerson(DTO.PersonDTO personDTO, Guid oldGuild)
        {
            var data = Mapping.Mapper.Map<PersonService.PersonDTO>(personDTO);

            _personClient.UpdatePerson(data, oldGuild);
        }
    }
}