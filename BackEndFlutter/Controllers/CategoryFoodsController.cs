using BackEndFlutter.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFlutter.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryFoodsController : ControllerBase
    {
        private readonly ProjectflutterContext _db;

        public CategoryFoodsController(ProjectflutterContext db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<ActionResult<List<CategoryFood>>> GetCategoryFoods()
        {
            var cate = await _db.CategoryFood.Include(p=>p.Foods).ToListAsync();
            return cate;
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CategoryFood data)
        {
            try
            {
                await _db.CategoryFood.AddAsync(data);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(Create), new { statusCode = "1", mes = "เพิ่มข้อมูลสำเร็จ", data });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Create), e.Message.ToString());
            }
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromForm] CategoryFood data)
        {
          
            try
            {
                _db.CategoryFood.Update(data);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(Edit), new { statusCode = "1", mes = "แก้ไขข้อมูลสำเร็จ", data });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Edit), e.Message.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryFood>> Delete(int id)
        {
            var cate = await _db.CategoryFood.FirstOrDefaultAsync(x=> x.CategoryFoodId == id);

            try
            {
                _db.CategoryFood.Remove(cate);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(Delete), new { statusCode = "1", mes = "ลบข้อมูลสำเร้จ", cate });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Delete), e.Message.ToString());
            }

        }
    }
}
