namespace MobileList.Models.Mobile
{
    public class MobileVideo
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public string ThumbnailSrc { get; set; }

        public int MobileId { get; set; }
        public Mobile Mobile { get; set; }
    }
}
