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
    public class CustomersController : ControllerBase

    {
        private readonly ProjectflutterContext _db;

        public CustomersController(ProjectflutterContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customers>>> GetCustomers()
        {
            var customers = await _db.Customers.ToListAsync();
            return customers;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> CreateCustomer([FromForm] Customers data)
        {
            try
            {
                var chk = await _db.Customers.FirstOrDefaultAsync(c => c.Username.Equals(data.Username));
                if (chk != null)
                {
                    return CreatedAtAction(nameof(CreateCustomer), new { statusCode = "0", msg = "ไม่สามารถสมัครสมาชิกได้เนื่องจากชื่อผู้ใช้ซ้ำ" });
                }
                _db.Customers.Add(data);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(CreateCustomer), new { statusCode = "1", msg = "สมัครสมาชิกสำเร็จ", data });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(CreateCustomer), e.Message.ToString());
            }
        }
        [HttpPut]
        public async Task<ActionResult> EditCustomer([FromForm] Customers data)
        {
            var result = await _db.Customers.AsNoTracking().FirstOrDefaultAsync(p => p.CusId == data.CusId);

            _db.Customers.Update(data);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(EditCustomer), new { statusCode = "1", msg = "แก้ไขข้อมูลสำเร็จ", data });
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromForm] Customers data)
        {
            var result = await _db.Customers
                .FirstOrDefaultAsync(user => user.Username.Equals(data.Username) && user.Password.Equals(data.Password));

            var admin = await _db.Admin.FirstOrDefaultAsync(a => a.Username.Equals(data.Username) && a.Password.Equals(data.Password));
            if(result == null && admin != null)
            {
                return CreatedAtAction(nameof(Login), new { statusCode = '2', msg = "เข้าสุ่ระบบสำเร็จ" });
            }
            if (result == null)
                return CreatedAtAction(nameof(Login), new { statusCode = '0', msg = "ไม่พบผู้ใช้งาน" });
            return CreatedAtAction(nameof(Login), new { statusCode = '1', msg = "เข้าสุ่ระบบสำเร็จ", id=result.CusId });
        }
    }
}
