using PersoneManagement.Web.AddressService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PersoneManagement.Web.Models.Interfaces
{
    public interface IAddressRepository
    {

        IEnumerable<AddressDTO> GetAddressesByBusinessId(int businessEntityId);

        AddressDTO GetAddressById(int id, int businessEntityID);

        IEnumerable<AddressTypeDTO> GetAddressTypes();

        void AddAddress(DTO.AddressDTO addressDTO, int businessEntityID);
        
        void UpdateAddress(DTO.AddressDTO addressDTO, int businessEntityID, Guid oldGuid);
        
        void DeleteAddress(int id, int businessEntityID);
        int GetAddressTypeIdByName(string addressTypeName);
    }
}
