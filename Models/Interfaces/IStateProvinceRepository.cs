//using PersoneManagement.Web.Models.DTO;
using PersoneManagement.Web.StateProvinceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PersoneManagement.Web.Models.Interfaces
{
    public interface IStateProvinceRepository
    {
        IEnumerable<StateProvinceDTO> GetAll();
        IEnumerable<CountryRegionDTO> GetCountryRegions();
        IEnumerable<SalesTerritorryDTO> GetTerritoriesByRegionCode(string regionCode);
        IEnumerable<SalesTerritorryDTO> GetAllTerritories();

        StateProvinceDTO GetStateProvinceById(int id);

        void InsertStateProvince(DTO.StateProvinceDTO stateProvince);

        void UpdateStateProvince(DTO.StateProvinceDTO stateProvince, Guid oldGuid);

        void DeleteStateProvince(int id);

        string GetCountryRegionIdByName(string regionName);

        int GetTerritoriesIdByName(string territoriesName);

        void ImportFromExcel(DTO.StateProvinceDTO stateProvinceDTO);

    }
}
