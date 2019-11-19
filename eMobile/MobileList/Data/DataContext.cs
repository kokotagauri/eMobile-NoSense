using Microsoft.EntityFrameworkCore;
using MobileList.Models.Manufacturer;
using MobileList.Models.Mobile;

namespace MobileList.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }

        #region Mobile

        public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<MobileThumbnail> MobileThumbnail { get; set; }
        public DbSet<MobileImage> MobileImages { get; set; }
        public DbSet<MobileVideo> MobileVideos { get; set; }

        #endregion

        #region Manufacturer

        public DbSet<Manufacturer> Manufacturers { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mobile>()
                .HasMany(m => m.MobileImages)
                .WithOne(mi => mi.Mobile);

            modelBuilder.Entity<Mobile>()
                .HasOne(m => m.MobileThumbnail)
                .WithOne(mt => mt.Mobile)
                .HasForeignKey<MobileThumbnail>(mt => mt.MobileId);

            modelBuilder.Entity<Mobile>()
                .HasMany(m => m.MobileVideos)
                .WithOne(mv => mv.Mobile);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(m => m.Mobiles)
                .WithOne(m => m.Manufacturer);
        }
    }
}
