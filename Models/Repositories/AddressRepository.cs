using PersoneManagement.Web.AddressService;
using PersoneManagement.Web.Models.Interfaces;
using PersoneManagement.Web.PersonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersoneManagement.Web.Models.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private AddressServiceClient _addressClient;

        public AddressRepository()
        {
            _addressClient = new AddressServiceClient();
        }

        public IEnumerable<AddressTypeDTO> GetAddressTypes()
        {
            var adrresTypeLists = _addressClient.GetAddressTypes();

            return adrresTypeLists;
        }

        public IEnumerable<AddressDTO> GetAddressesByBusinessId(int businessEntityId)
        {
            var adrressLists = _addressClient.GetAddressesByBusinessId(businessEntityId);

            return adrressLists;
        }


        public AddressDTO GetAddressById(int id, int businessEntityID)
        {
            var selectedAddress = _addressClient.GetAddressById(id, businessEntityID);

            return selectedAddress;
        }

        public void AddAddress(DTO.AddressDTO addressDTO, int businessEntityID)
        {
            var result = Mapping.Mapper.Map<AddressDTO>(addressDTO);

            _addressClient.AddAddress(result, businessEntityID);   
        }

        public void DeleteAddress(int id, int businessEntityID)
        {
            _addressClient.DeleteAddress(id, businessEntityID);
        }

        public void UpdateAddress(DTO.AddressDTO addressDTO, int businessEntityID, Guid oldGuid)
        {
            var address = Mapping.Mapper.Map<AddressDTO>(addressDTO);

            _addressClient.UpdateAddress(address, businessEntityID, oldGuid);
        }

        public int GetAddressTypeIdByName(string addressTypeName)
        {
            var addressType = _addressClient.GetAddressTypeIdByName(addressTypeName);

            return addressType;
        }
    }
}