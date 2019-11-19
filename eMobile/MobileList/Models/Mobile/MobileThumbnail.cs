namespace MobileList.Models.Mobile
{
    public class MobileThumbnail
    {
        public int Id { get; set; }
        public string Src { get; set; }

        public int MobileId { get; set; }
        public Mobile Mobile { get; set; }
    }
}
