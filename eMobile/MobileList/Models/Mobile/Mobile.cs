using System.Collections.Generic;

namespace MobileList.Models.Mobile
{
    public class Mobile
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

        public ICollection<MobileImage> MobileImages { get; set; }
        public MobileThumbnail MobileThumbnail { get; set; }
        public ICollection<MobileVideo> MobileVideos { get; set; }
        public Manufacturer.Manufacturer Manufacturer { get; set; }
    }
}
