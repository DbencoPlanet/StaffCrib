using AcademicStaff.Areas.Data.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AcademicStaff.Models.Entities;
using System.Threading.Tasks;
using AcademicStaff.Models;
using System.Data.Entity;

namespace AcademicStaff.Areas.Data.Services
{
    public class ImageGalleryService : IImageGalleryService
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public async Task New(ImageGallery models, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {


                // Find its length and convert it to byte array
                int ContentLength = upload.ContentLength;

                // Create Byte Array
                byte[] bytImg = new byte[ContentLength];

                // Read Uploaded file in Byte Array
                upload.InputStream.Read(bytImg, 0, ContentLength);

                models.Content = bytImg;
                models.ContentType = upload.ContentType;
                models.FileName = upload.FileName;

            }

            db.ImageGallery.Add(models);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            ImageGallery models = await db.ImageGallery.FindAsync(id);

            db.ImageGallery.Remove(models);
            await db.SaveChangesAsync();
        }

        public async Task<List<ImageGallery>> List()
        {
            var slider = db.ImageGallery;
            return await slider.ToListAsync();
        }


        public async Task AddToGallery(ImageGallery models)
        {
            if (models.CurrentGallery == false)
            {
                models.CurrentGallery = true;
                db.Entry(models).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            else
            {
                models.CurrentGallery = false;
                db.Entry(models).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }



    }
}