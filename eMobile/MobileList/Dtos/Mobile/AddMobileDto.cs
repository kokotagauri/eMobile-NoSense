using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MobileList.Dtos.Mobile
{
    public class AddMobileDto
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public decimal Weight { get; set; }
        public string Resolution { get; set; }
        public string Processor { get; set; }
        public string Memory { get; set; }
        public string OperatingSystem { get; set; }
        public decimal Price { get; set; }
        public int Manufacturer { get; set; }
        public IFormFile ThumbNail { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<IFormFile> Videos { get; set; }
    }
}
