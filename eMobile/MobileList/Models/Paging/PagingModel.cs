using MobileList.Dtos.Manufacturer;
using MobileList.Dtos.Mobile;
using System;
using System.Collections.Generic;

namespace MobileList.Models.Paging
{
    public class PagingModel
    {
        public int Count { get; set; }
        public int PageSize { get; set; } = 6;
        public int CurrentPage { get; set; }

        public string SearchString { get; set; }
        public int Manufacturer { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<GetManufacturerDto> Manufacturers { get; set; }
        public List<SimpleMobileDto> Data { get; set; }
    }
}
