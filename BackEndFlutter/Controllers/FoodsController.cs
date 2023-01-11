using BackEndFlutter.Models.Data;
using BackEndFlutter.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFlutter.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly ProjectflutterContext _db;
        private IWebHostEnvironment _environment;

        public FoodsController(ProjectflutterContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [HttpGet]
        public async Task<ActionResult<List<Foods>>> GetFoods()
        {
            var food = await _db.Foods.Include(x=>x.Catefood).Include(p=>p.FoodOptions).ThenInclude(od=>od.Options).ThenInclude(o=>o.OptionsDetail).Include(i=>i.ImageFood).ToListAsync();
            return food;
        }
        [HttpGet("ClasstifyFoods/{id}")]
        public async Task<ActionResult<List<Foods>>> ClasstifyFoods(int id)
        {
            var food = await _db.Foods.Include(x => x.Catefood).Include(p => p.FoodOptions).ThenInclude(od => od.Options).ThenInclude(o => o.OptionsDetail).Include(i => i.ImageFood).ToListAsync();

            if (id != 0)
            {
                food = await _db.Foods.Where(e => e.CatefoodId == id).Include(x => x.Catefood).Include(p => p.FoodOptions).ThenInclude(od => od.Options).ThenInclude(o => o.OptionsDetail).Include(i => i.ImageFood).ToListAsync();

            }
            return food;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] Foods data, [FromForm] IFormFileCollection UpFile)
        {
            var chk = _db.Foods.Where(a => a.Name == data.Name).FirstOrDefault();

            if (chk != null)
                return CreatedAtAction(nameof(Create), new { statusCode = "0", mes = "พบข้อมูลซ้ำ", data });
            try
            {
                await _db.Foods.AddAsync(data);
                await _db.SaveChangesAsync();
                #region ImageManageMent
                var path = _environment.WebRootPath + Helpers.Constants.DirectoryFood;
                if (UpFile?.Count > 0)
                {

                    foreach (var file in UpFile)
                    {
                        var img = new ImageFood();

                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                        //ตัดเอาเฉพาะชื่อไฟล์
                        var fileName = data.FoodId + Helpers.Constants.FoodsImage;
                        if (file.FileName != null)
                        {
                            fileName +=
                            file.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                        }
                        using (FileStream filestream =
                        System.IO.File.Create(path + fileName))
                        {
                            file.CopyTo(filestream);
                            filestream.Flush();
                            img.Path = Helpers.Constants.DirectoryFood + fileName;
                        }
                        img.FoodId = data.FoodId;
                        await _db.ImageFood.AddAsync(img);
                        await _db.SaveChangesAsync();
                    }



                }
                #endregion
                return CreatedAtAction(nameof(Create), new { statusCode = "1", mes = "เพิ่มข้อมูลสำเร็จ", data });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Create), e.Message.ToString());
            }
        }
        [HttpPut]
        public async Task<ActionResult> Edit([FromForm] Foods data, [FromForm] IFormFileCollection UpFile)
        {
            var find = await _db.Foods.AsNoTracking()
                .Include(p => p.ImageFood)
                .FirstOrDefaultAsync(x => x.FoodId == data.FoodId);
            try
            {
                _db.Foods.Update(data);
                await _db.SaveChangesAsync();
                #region ImageManageMent
                var path = _environment.WebRootPath + Helpers.Constants.DirectoryFood;
                if (UpFile?.Count > 0)
                {

                    foreach (var file in UpFile)
                    {
                        var img = new ImageFood();

                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                        //ตัดเอาเฉพาะชื่อไฟล์
                        var fileName = data.FoodId + Helpers.Constants.FoodsImage;
                        if (file.FileName != null)
                        {
                            fileName +=
                            file.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                        }
                        using (FileStream filestream =
                        System.IO.File.Create(path + fileName))
                        {
                            file.CopyTo(filestream);
                            filestream.Flush();
                            img.Path = Helpers.Constants.DirectoryFood + fileName;
                        }
                        img.FoodId = data.FoodId;
                        await _db.ImageFood.AddAsync(img);
                        await _db.SaveChangesAsync();
                    }



                }
                #endregion
                return CreatedAtAction(nameof(Edit), new { statusCode = "1", mes = "แก้ไขข้อมูลสำเร็จ", data });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Edit), e.Message.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Foods>> Delete(int id)
        {
            var cate = await _db.Foods
                .Include(c => c.ImageFood)
                .FirstOrDefaultAsync(x => x.FoodId == id);


            try
            {
                _db.Foods.Remove(cate);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(Delete), new { statusCode = "1", mes = "ลบข้อมูลสำเร้จ", cate });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Delete), e.Message.ToString());
            }

        }


        [HttpPut("UpdateImage")]
        public async Task<ActionResult> UpdateImage([FromForm] ImageFood data, [FromForm] IFormFile UpFile)
        {
            
            try
            {
                #region ImageManageMent
                var path = _environment.WebRootPath + Helpers.Constants.DirectoryFood;
                if (UpFile != null)
                {
                  
                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                        //ตัดเอาเฉพาะชื่อไฟล์
                        var fileName = data.FoodId + Helpers.Constants.FoodsImage;
                        if (UpFile.FileName != null)
                        {
                            fileName +=
                            UpFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                        }
                        using (FileStream filestream =
                        System.IO.File.Create(path + fileName))
                        {
                        UpFile.CopyTo(filestream);
                            filestream.Flush();
                            data.Path = Helpers.Constants.DirectoryFood + fileName;
                        }
                        _db.ImageFood.Update(data);
                        await _db.SaveChangesAsync();
                    



                }
                #endregion
                return CreatedAtAction(nameof(Create), new { statusCode = "1", mes = "เพิ่มข้อมูลสำเร็จ", data });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Create), e.Message.ToString());
            }
        }

        [HttpDelete("DeleteImage/{id}")]
        public async Task<ActionResult<Foods>> DeleteImage(int id)
        {
            var img = await _db.ImageFood.FirstOrDefaultAsync(x => x.ImageId == id);


            try
            {
                _db.ImageFood.Remove(img);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(Delete), new { statusCode = "1", mes = "ลบข้อมูลสำเร้จ" });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Delete), e.Message.ToString());
            }

        }

    }
}
