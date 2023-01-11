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
    public class TablesController : ControllerBase
    {
        private readonly ProjectflutterContext _db;

        public TablesController(ProjectflutterContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Tables>>> GetTables()
        {
            var t = await _db.Tables.ToListAsync();
            return t;
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromForm] Tables data)
        {
            try
            {
                await _db.Tables.AddAsync(data);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(Create), new { statusCode = "1", mes = "เพิ่มข้อมูลสำเร็จ", data });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Create), e.Message.ToString());
            }
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromForm] Tables data)
        {
            var find = await _db.Tables.AsNoTracking().FirstOrDefaultAsync(x => x.TableId == data.TableId);

            try
            {
                _db.Tables.Update(data);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(Edit), new { statusCode = "1", mes = "แก้ไขข้อมูลสำเร็จ", data });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Edit), e.Message.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Tables>> Delete(int id)
        {
            var t = await _db.Tables.FindAsync(id);

            try
            {
                _db.Tables.Remove(t);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(Delete), new { statusCode = "1", mes = "ลบข้อมูลสำเร้จ", t });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Delete), e.Message.ToString());
            }

        }
    }
}
