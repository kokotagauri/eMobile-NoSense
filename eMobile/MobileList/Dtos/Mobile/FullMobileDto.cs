using MobileList.Dtos.Common;
using System.Collections.Generic;

namespace MobileList.Dtos.Mobile
{
    public class FullMobileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public decimal Weight { get; set; }
        public string Resolution { get; set; }
        public string Processor { get; set; }
        public string Memory { get; set; }
        public string OperatingSystem { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public List<MediaDto> ImagesAndVideos { get; set; }
    }
}
