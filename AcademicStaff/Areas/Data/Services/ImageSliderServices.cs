using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AcademicStaff.Areas.Data.IServices;
using System.Threading.Tasks;
using AcademicStaff.Models.Entities;
using System.IO;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AcademicStaff.Models;

namespace AcademicStaff.Areas.Data.Services
{
    public class ImageSliderServices:IimageSliderServices
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task New(ImageSlider models, HttpPostedFileBase upload)
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

            db.ImageSlider.Add(models);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            ImageSlider models = await db.ImageSlider.FindAsync(id);
           
            db.ImageSlider.Remove(models);
            await db.SaveChangesAsync();
        }

        public async Task<List<ImageSlider>> List()
        {
            var slider = db.ImageSlider;
            return await slider.ToListAsync();
        }


        public async Task AddToSlider(ImageSlider models)
        {
            if (models.CurrentSlider == false)
            {
                models.CurrentSlider = true;
                db.Entry(models).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            else
            {
                models.CurrentSlider = false;
                db.Entry(models).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

        }


      
    }
}