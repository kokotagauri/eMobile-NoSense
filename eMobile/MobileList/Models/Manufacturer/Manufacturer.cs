using System.Collections.Generic;

namespace MobileList.Models.Manufacturer
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Mobile.Mobile> Mobiles { get; set; }
    }
}
