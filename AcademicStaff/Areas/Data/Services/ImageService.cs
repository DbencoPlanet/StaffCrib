using AcademicStaff.Areas.Data.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AcademicStaff.Models.Entities;
using System.Threading.Tasks;
using AcademicStaff.Models;
using System.Drawing;
using System.Web.Helpers;
using System.IO;
using System.Drawing.Imaging;

namespace AcademicStaff.Areas.Data.Services
{
    public class ImageService : IImageService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //public HttpPostedFileBase ResizeBitmap(HttpPostedFileBase b, int nWidth, int nHeight)
        //{
        //    HttpPostedFileBase result = new HttpPostedFileBase(nWidth, nHeight);
        //    using (Graphics g = Graphics.FromImage((Image)result))
        //        g.DrawImage(b, 0, 0, nWidth, nHeight);
        //    return result;
        //}
        public async Task<int> Create(HttpPostedFileBase upload)
        {
            ImageModel model = new ImageModel();
            if (upload != null && upload.ContentLength > 0)
            {


                // Find its length and convert it to byte array
                int ContentLength = upload.ContentLength;

                // Create Byte Array
                byte[] bytImg = new byte[ContentLength];

                // Read Uploaded file in Byte Array
                upload.InputStream.Read(bytImg, 0, ContentLength);

                model.ImageContent = bytImg;
                model.ContentType = upload.ContentType;
                model.FileName = upload.FileName;

            }

            db.ImageModel.Add(model);
            await db.SaveChangesAsync();
            return model.Id;
        }

        public async Task Delete(int? id)
        {
            var img = await db.ImageModel.FirstOrDefaultAsync(x => x.Id == id);
            if (img != null)
            {
                db.ImageModel.Remove(img);
                await db.SaveChangesAsync();
            }


        }

        public async Task Edit(int id, HttpPostedFileBase upload)
        {
            var img = await db.ImageModel.FirstOrDefaultAsync(x => x.Id == id);
            if (img != null)
            {
                if (upload != null && upload.ContentLength > 0)
                {


                    // Find its length and convert it to byte array
                    int ContentLength = upload.ContentLength;

                    // Create Byte Array
                    byte[] bytImg = new byte[ContentLength];

                    // Read Uploaded file in Byte Array
                    upload.InputStream.Read(bytImg, 0, ContentLength);

                    img.ImageContent = bytImg;
                    img.ContentType = upload.ContentType;
                    img.FileName = upload.FileName;
                }
                db.Entry(img).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task<ImageModel> Get(int? id)
        {
            var img = await db.ImageModel.FirstOrDefaultAsync(x => x.Id == id);
            return img;
        }

        //public async Task PostImageCreate(List<HttpPostedFileBase> upload, int PostId)
        //{
        //    PostImage model = new PostImage();
        //    if (upload.Count() > 0)
        //    {
        //        foreach (var image in upload)
        //        {
        //            // Find its length and convert it to byte array
        //            int ContentLength = image.ContentLength;

        //            // Create Byte Array
        //            byte[] bytImg = new byte[ContentLength];

        //            // Read Uploaded file in Byte Array
        //            image.InputStream.Read(bytImg, 0, ContentLength);

        //            model.ImageContent = bytImg;
        //            model.ContentType = image.ContentType;
        //            model.FileName = image.FileName;
        //            model.PostId = PostId;
        //            db.PostImages.Add(model);
        //            await db.SaveChangesAsync();
        //        }



        //    }

        

        //        public async Task PostImageDelete(int? PostId)
        //        {
        //            var img = await db.PostImages.Where(x => x.PostId == PostId).ToListAsync();
        //            if(img.Count() > 0)
        //            {
        //                foreach(var my in img)
        //                {
        //                    db.PostImages.Remove(my);

        //                }
        //                await db.SaveChangesAsync();
        //            }
        //        }

        //        public async Task<List<PostImage>> PostImageGet(int? PostId)
        //        {
        //            var img = await db.PostImages.Where(x => x.PostId == PostId).ToListAsync();
        //            return img;
        //        }
        //    }
    }
}