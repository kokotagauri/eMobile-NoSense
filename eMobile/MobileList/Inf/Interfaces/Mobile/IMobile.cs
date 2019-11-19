using MobileList.Dtos.Manufacturer;
using MobileList.Dtos.Mobile;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileList.Inf.Interfaces.Mobile
{
    public interface IMobile
    {
        #region Mobile

        Task<MobilesGenericDto> GetFilteredMobiles(string name, int manufacturer, decimal minPrice, decimal maxPrice, int page, int pageSize);
        Task<FullMobileDto> GetMobile(int id);
        Task<string> AddMobile(AddMobileDto model);

        #endregion

        #region Manufacturer

        Task<List<GetManufacturerDto>> GetManufacturers();

        #endregion
    }
}
