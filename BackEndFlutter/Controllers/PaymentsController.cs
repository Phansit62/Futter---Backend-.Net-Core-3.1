using BackEndFlutter.Models.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFlutter.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ProjectflutterContext _db;
        private IWebHostEnvironment _environment;

        public PaymentsController(ProjectflutterContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [HttpGet]
        public async Task<ActionResult<List<Payments>>> GetPayments()
        {
            var pay = await _db.Payments.Include(x=>x.Order).ThenInclude(x => x.OderDetail).ToListAsync();
            return pay;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] int id, [FromForm] IFormFile UpFile)
        {
            Payments pay = new Payments();
            pay.OrderId = id;
            pay.DateIn = DateTime.Now;
            pay.Status = false;
            try
            {
                #region ImageManageMent
                var path = _environment.WebRootPath + Helpers.Constants.DirectoryPayments;
                if (UpFile != null)
                {

                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    //ตัดเอาเฉพาะชื่อไฟล์
                    var fileName =Helpers.Constants.PaymentsImage;
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
                        pay.Image = Helpers.Constants.DirectoryPayments + fileName;
                    }
                    await _db.Payments.AddAsync(pay);
                    await _db.SaveChangesAsync();


                }
                #endregion
                return CreatedAtAction(nameof(Create), new { statusCode = "1", mes = "เพิ่มข้อมูลสำเร็จ"});
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Create), e.Message.ToString());
            }
        }
        [Route("PaymentDelivery")]
        [HttpPost]
        public async Task<ActionResult> PaymentDelivery([FromForm] int id, [FromForm] IFormFile UpFile)
        {
            Payments pay = new Payments();
            pay.OrderDeliveryId = id;
            pay.DateIn = DateTime.Now;
            pay.Status = false;
            try
            {
                #region ImageManageMent
                var path = _environment.WebRootPath + Helpers.Constants.DirectoryPayments;
                if (UpFile != null)
                {

                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    //ตัดเอาเฉพาะชื่อไฟล์
                    var fileName = Helpers.Constants.PaymentsImage;
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
                        pay.Image = Helpers.Constants.DirectoryPayments + fileName;
                    }
                    await _db.Payments.AddAsync(pay);
                    await _db.SaveChangesAsync();


                }
                #endregion
                return CreatedAtAction(nameof(PaymentDelivery), new { statusCode = "1", mes = "เพิ่มข้อมูลสำเร็จ" });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(PaymentDelivery), e.Message.ToString());
            }
        }
    }
}
