using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MobileList.Data;
using MobileList.Dtos.Common;
using MobileList.Dtos.Manufacturer;
using MobileList.Dtos.Mobile;
using MobileList.Inf.Interfaces.Mobile;
using MobileList.Models.Mobile;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MobileList.Inf.Repos.Mobile
{
    public class MobileRepo : IMobile
    {
        private readonly DataContext _context;
        private readonly IHostingEnvironment _env;

        public MobileRepo(DataContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        #region Mobile

        public async Task<MobilesGenericDto> GetFilteredMobiles(string name, int manufacturer, decimal minPrice, decimal maxPrice, int page, int pageSize)
        {
            name = name ?? "";

            var mobilesFiltered = await _context.Mobiles.Include(m => m.MobileThumbnail).Include(m => m.Manufacturer)
                .Where(m => m.Name.Contains(name) && m.Price >= minPrice &&
                            m.Price <= maxPrice).ToListAsync();

            if (manufacturer != 0)
                mobilesFiltered = mobilesFiltered.Where(m => m.Manufacturer.Id == manufacturer).ToList();

            var count = mobilesFiltered.Count;

            var mobiles = mobilesFiltered.Skip((page - 1) * pageSize).Take(pageSize).Select(m => new SimpleMobileDto
            {
                Id = m.Id,
                MainImageSrc = m.MobileThumbnail.Src,
                Name = m.Name,
                Price = m.Price
            }).ToList();

            var res = new List<List<SimpleMobileDto>>();

            for (var i = 0; i < mobiles.Count; i += 3)
            {
                if (mobiles.Count - i < 3)
                {
                    res.Add(mobiles.TakeLast(mobiles.Count - i).ToList());
                    break;
                }
                res.Add(mobiles.GetRange(i,3));
            }

            return new MobilesGenericDto
            {
                Mobiles = mobiles,
                Count = count
            };
        }

        public async Task<FullMobileDto> GetMobile(int id)
        {
            var mobile = await _context.Mobiles.Include(m => m.Manufacturer).Include(m => m.MobileImages)
                .Include(m => m.MobileVideos).FirstOrDefaultAsync(m => m.Id == id);

            var res =  new FullMobileDto
            {
                Id = mobile.Id,
                Name = mobile.Name,
                Size = mobile.Size,
                Weight = mobile.Weight,
                Resolution = mobile.Resolution,
                Processor = mobile.Processor,
                Memory = mobile.Memory,
                OperatingSystem = mobile.OperatingSystem,
                Price = mobile.Price,
                Manufacturer = mobile.Manufacturer.Name,
                ImagesAndVideos = new List<MediaDto>()
            };

            res.ImagesAndVideos.AddRange(mobile.MobileVideos.Select(mi => new MediaDto {Src = mi.Src, Type = "video"}));
            res.ImagesAndVideos.AddRange(mobile.MobileImages.Select(mi => new MediaDto { Src = mi.Src, Type = "image" }));

            return res;
        }

        public async Task<string> AddMobile(AddMobileDto model)
        {
            try
            {
                var mobile = new Models.Mobile.Mobile
                {
                    Name = model.Name,
                    Size = model.Size,
                    Weight = model.Weight,
                    Resolution = model.Resolution,
                    Processor = model.Processor,
                    Memory = model.Memory,
                    OperatingSystem = model.OperatingSystem,
                    Price = model.Price,
                    Manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Id == model.Manufacturer)
                };

                await _context.Mobiles.AddAsync(mobile);
                await _context.SaveChangesAsync();

                var path = Path.Combine(_env.WebRootPath, "files", $"{mobile.Id}");
                var pathConst = path;

                if (model.ThumbNail != null)
                {
                    path = Path.Combine(pathConst, "thumbnail");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    using (var stream = new FileStream(Path.Combine(path, model.ThumbNail.FileName.Split('\\').Last()), FileMode.Create))
                    {
                        model.ThumbNail.CopyTo(stream);
                        var mobileThumbnail = new MobileThumbnail
                        {
                            MobileId = mobile.Id,
                            Src = $"/files/{mobile.Id}/thumbnail/{model.ThumbNail.FileName.Split('\\').Last()}"
                        };
                        await _context.MobileThumbnail.AddAsync(mobileThumbnail);
                    }
                }

                if (model.Images != null)
                {
                    foreach (var image in model.Images)
                    {
                        path = Path.Combine(pathConst, "images");
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        using (var stream = new FileStream(Path.Combine(path, image.FileName.Split('\\').Last()),
                            FileMode.Create))
                        {
                            image.CopyTo(stream);
                            var mobileImage = new MobileImage
                            {
                                MobileId = mobile.Id,
                                Src = $"/files/{mobile.Id}/images/{image.FileName.Split('\\').Last()}"
                            };
                            await _context.MobileImages.AddAsync(mobileImage);
                        }
                    }
                }

                if (model.Videos != null)
                {
                    foreach (var video in model.Videos)
                    {
                        path = Path.Combine(pathConst, "videos");
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        using (var stream = new FileStream(Path.Combine(path, video.FileName.Split('\\').Last()),
                            FileMode.Create))
                        {
                            video.CopyTo(stream);
                            var mobileVideo = new MobileVideo
                            {
                                MobileId = mobile.Id,
                                Src = $"/files/{mobile.Id}/videos/{video.FileName.Split('\\').Last()}"
                            };
                            await _context.MobileVideos.AddAsync(mobileVideo);
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return "success";
            }
            catch
            {
                return "fail";
            }
        }

        #endregion

        #region Manufacturer

        public async Task<List<GetManufacturerDto>> GetManufacturers()
        {
            return await _context.Manufacturers.Select(m => new GetManufacturerDto
            {
                Id = m.Id,
                Name = m.Name
            }).ToListAsync();
        }

        #endregion
    }
}
