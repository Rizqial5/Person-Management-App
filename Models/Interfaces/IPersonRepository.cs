using PersoneManagement.Web.PersonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersoneManagement.Web.Models.Interfaces
{
    public interface IPersonRepository
    {
        IEnumerable<PersonDTO> GetPersonLists();

        PersonDTO GetPerson(int businessEntityId);

        PersonDTO GetAddressById(int businessEntityId);

        string GetFullName(int businessEntityId);

        void CreatePerson(DTO.PersonDTO personDTO);
        void UpdatePerson(DTO.PersonDTO personDTO, Guid oldGuild);
        void DeletePerson(int businessEntityId);
    }
}
