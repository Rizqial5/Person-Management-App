using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersoneManagement.Web.Models.Interfaces;
using PersoneManagement.Web.StateProvinceService;
//using PersoneManagement.Web.Models.DTO;


namespace PersoneManagement.Web.Models.Repositories
{
    public class StateProvinceRepository : IStateProvinceRepository
    {
        private StateProvinceServiceClient _stateProvinceClient;

        public StateProvinceRepository()
        {
            _stateProvinceClient = new StateProvinceServiceClient();
        }

        public void DeleteStateProvince(int id)
        {
            _stateProvinceClient.DeleteStateProvince(id);
        }

        public IEnumerable<StateProvinceDTO> GetAll()
        {
            var data = _stateProvinceClient.GetAll();

            return data;
        }

        public IEnumerable<CountryRegionDTO> GetCountryRegions()
        {
            var data = _stateProvinceClient.GetCountryRegions();

            return data;
        }

        public IEnumerable<SalesTerritorryDTO> GetTerritoriesByRegionCode(string regionCode)
        {
            var data = _stateProvinceClient.GetTerritoriesByRegionCode(regionCode);

            return data;
        }

        public StateProvinceDTO GetStateProvinceById(int id)
        {
            var data = _stateProvinceClient.GetStateProvinceById(id);

            return data;
        }



        public void InsertStateProvince(DTO.StateProvinceDTO stateProvinceDTO)
        {
            var transform = new StateProvinceDTO
            {
                StateProvinceID = stateProvinceDTO.StateProvinceID,
                StateProvinceCode = stateProvinceDTO.StateProvinceCode,
                CountryRegionCode = stateProvinceDTO.CountryRegionCode,
                IsOnlyStateProvinceFlag = stateProvinceDTO.IsOnlyStateProvinceFlag,
                Name = stateProvinceDTO.Name,
                TerritoryID = stateProvinceDTO.TerritoryID,

            };

            _stateProvinceClient.InsertStateProvince(transform);
        }

        public void UpdateStateProvince(DTO.StateProvinceDTO stateProvinceDTO, Guid oldGuid)
        {

            var transform = new StateProvinceService.StateProvinceDTO
            {
                StateProvinceID = stateProvinceDTO.StateProvinceID,
                StateProvinceCode = stateProvinceDTO.StateProvinceCode,
                CountryRegionCode = stateProvinceDTO.CountryRegionCode,
                IsOnlyStateProvinceFlag = stateProvinceDTO.IsOnlyStateProvinceFlag,
                Name = stateProvinceDTO.Name,
                TerritoryID = stateProvinceDTO.TerritoryID,

            }; 

            _stateProvinceClient.UpdateStateProvince(transform, oldGuid);
        }

        public IEnumerable<SalesTerritorryDTO> GetAllTerritories()
        {
            var data = _stateProvinceClient.GetAllTerritories();

            return data;
        }

        public string GetCountryRegionIdByName(string regionName)
        {
            var data = _stateProvinceClient.GetCountryRegionIdByName(regionName);

            return data;
        }

        public int GetTerritoriesIdByName(string territoriesName)
        {
            var data = _stateProvinceClient.GetTerritoriesIdByName(territoriesName);

            return data;
        }

        public void ImportFromExcel(DTO.StateProvinceDTO stateProvinceDTO)
        {
            var result = Mapping.Mapper.Map<StateProvinceDTO>(stateProvinceDTO);

            _stateProvinceClient.ImportFromExcel(result);
        }

        public int GetStateProvinceIdByName(string stateProvinceName)
        {
            var data = _stateProvinceClient.GetStateProvinceIdByName(stateProvinceName);
            return data;
        }
    }
}