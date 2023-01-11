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
    public class OptionsController : ControllerBase
    {
        private readonly ProjectflutterContext _db;

        public OptionsController(ProjectflutterContext db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<ActionResult<List<Options>>> GetOptions()
        {
            var option = await _db.Options.Include(od=>od.OptionsDetail).ToListAsync();
            return option;
        }

        [HttpPost]

        public async Task<ActionResult> Create([FromForm] String[] optiondetail, [FromForm] Options data)
        {

            await _db.Options.AddAsync(data);
            await _db.SaveChangesAsync();
            try
            {
                foreach (var item in optiondetail)
                {
                    OptionsDetail optionsDetails = new OptionsDetail();
                    optionsDetails.OptionsId = data.OptionsId;
                    optionsDetails.Typename = item;
                    await _db.OptionsDetail.AddAsync(optionsDetails);
                    await _db.SaveChangesAsync();
                }
               
                return CreatedAtAction(nameof(Create), new { statusCode = "1", mes = "เพิ่มข้อมูลสำเร็จ", data });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Create), e.Message.ToString());
            }
        }

        [HttpPut]

        public async Task<ActionResult> Edit([FromForm] String[] od, [FromForm] Options data)
        {
            
            _db.Options.Update(data);
            await _db.SaveChangesAsync();
            try
            {
                foreach (var item in od)
                {
                    OptionsDetail optionsDetails = new OptionsDetail();
                    optionsDetails.OptionsId = data.OptionsId;
                    optionsDetails.Typename = item;
                    await _db.OptionsDetail.AddAsync(optionsDetails);
                    await _db.SaveChangesAsync();
                }

                return CreatedAtAction(nameof(Edit), new { statusCode = "1", mes = "เพิ่มข้อมูลสำเร็จ", data });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Edit), e.Message.ToString());
            }
        }


        [HttpPut("UpdateOptionDetail")]

        public async Task<ActionResult> UpdateOptionDetail([FromForm] OptionsDetail data ) {
            try
            {
                _db.OptionsDetail.Update(data);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(UpdateOptionDetail), new { statusCode = "1", mes = "เพิ่มข้อมูลสำเร็จ", data });
            }
            catch(Exception e)
            {
                return CreatedAtAction(nameof(Edit), e.Message.ToString());
            }
        
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Options>> Delete(int id)
        {
            var options = await _db.Options.Include(x=>x.OptionsDetail).FirstOrDefaultAsync(o=>o.OptionsId == id);

            try
            {
                _db.Options.Remove(options);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(Delete), new { statusCode = "1", mes = "ลบข้อมูลสำเร้จ", options });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Delete), e.Message.ToString());
            }

        }

        [HttpDelete("DeleteeOptionDetail/{id}")]
        public async Task<ActionResult<OptionsDetail>> DeleteeOptionDetail(int id)
        {
            var optionsDetail = await _db.OptionsDetail.FindAsync(id);

            try
            {
                _db.OptionsDetail.Remove(optionsDetail);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(DeleteeOptionDetail), new { statusCode = "1", mes = "ลบข้อมูลสำเร้จ", optionsDetail });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(DeleteeOptionDetail), e.Message.ToString());
            }

        }
    }





}
