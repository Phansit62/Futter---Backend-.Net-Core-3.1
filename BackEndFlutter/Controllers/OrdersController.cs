using BackEndFlutter.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFlutter.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ProjectflutterContext _db;
        public OrdersController(ProjectflutterContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Orders>>> GetOrders()
        {
            var order = await _db.Orders.Include(o=>o.Status).Include(od=>od.OderDetail).ThenInclude(f=>f.Food).Include(od=>od.OderDetail).ThenInclude(x => x.DetailsNavigation).ThenInclude(od=>od.OptionsDetail).ThenInclude(o=>o.Options).Include(d=>d.Payments).ToListAsync();
            return order;
        }


        [HttpGet("GetOrderDelivery")]
        public async Task<ActionResult<List<OrdersDelivery>>> GetOrderDelivery()
        {
            var order = await _db.OrdersDelivery.Include(c=>c.Cus).
                Include(o => o.Statusdelivery).Include(od => od.OderDetail).ThenInclude(f => f.Food).Include(od => od.OderDetail).ThenInclude(x => x.DetailsNavigation).ThenInclude(od => od.OptionsDetail).ThenInclude(o => o.Options).Include(d => d.Payments).ToListAsync();
            return order;
        }


        [HttpGet("OpenBill/{id}")]
        public async Task<ActionResult<List<Orders>>> OpenBill(string id, int total)
        {
            Orders orders = new Orders();
            orders.TableId = Convert.ToInt32(id);
            orders.StatusId = 1;
            orders.DateIn = DateTime.Now;
            orders.Total = total;
            _db.Orders.Add(orders);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(OpenBill), new { statusCode = "1", data = orders, id = orders.OrderId });


        }
        [HttpGet("OpenBillDelivery/{id}")]
        public async Task<ActionResult<List<OrdersDelivery>>> OpenBillDelivery(string id, int total,string lag,string longt,string detail)
        {
            try
            {
                OrdersDelivery orders = new OrdersDelivery();
                orders.CusId = Convert.ToInt32(id);
                orders.DateIn = DateTime.Now;
                orders.StatusdeliveryId = 1;
                orders.Details = detail;
                orders.Lat = lag;
                orders.Long = longt;
                orders.Total = total;
                await _db.OrdersDelivery.AddAsync(orders);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(OpenBillDelivery), new { statusCode = "1", data = orders, id = orders.OrderDeliveryId });
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Create), e.Message.ToString());
            }
        }

        [HttpGet("ConfirmOrder/{id}")]
        public async Task<ActionResult<List<Payments>>> ConfirmOrder(string id)
        {
            var pay = await _db.Payments.Include(p => p.Order).FirstOrDefaultAsync(x => x.PaymentId == Convert.ToInt32(id));
            pay.Status = true;
            pay.Order.StatusId = 2;

            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(ConfirmOrder), new { statusCode = "1", data = pay});


        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] OderDetail data, [FromForm] string option,[FromForm] string id,[FromForm] int total)
        {
            try
            {
                var chktable = await _db.Orders.FirstOrDefaultAsync(x => x.TableId == Convert.ToInt32(id) && x.StatusId == 1);
                if (chktable == null)
                {
                    Orders orders = new Orders();
                    orders.TableId = Convert.ToInt32(id);
                    orders.StatusId = 1;
                    orders.DateIn = DateTime.Now;
                    orders.Total = total;
                    _db.Orders.Add(orders);
                    data.OrderId = orders.OrderId;
                    _db.OderDetail.Add(data);
                }
                else {
                    data.OrderId = chktable.OrderId;
                    _db.OderDetail.Add(data);
                }
              
                JArray jsonResponse = JArray.Parse(option);
                List<Details> de = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Details>>(option.ToString());
                    if (de != null)
                    {
                        foreach (var item in de)
                        {
                            Details details = new Details();
                            details.OptionsDetailId = item.OptionsDetailId;
                            details.OptionsId = item.OptionsId;
                            details.OrderDetailId = item.OrderDetailId;
                            _db.Details.Add(details);

                        }
                        await _db.SaveChangesAsync();
                }
                return CreatedAtAction(nameof(Create), new { statusCode = "1", msg = "ทำรายการสำเร็จ"});
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(Create), e.Message.ToString());
            }
        }


        [HttpGet("GetOrdersForTable/{id}")]
        public async Task<ActionResult<List<Orders>>> GetOrdersForTable(int id)
        {
            var order = await _db.Orders.Where(x=>x.TableId == id).Include(od => od.OderDetail).ThenInclude(x => x.DetailsNavigation).ThenInclude(od => od.OptionsDetail).ThenInclude(o => o.Options).Include(d => d.Payments).ToListAsync();
            return order;
        }

        [HttpGet("GetOrdersForUser/{id}")]
        public async Task<ActionResult<List<OrdersDelivery>>> GetOrdersForUser(int id)
        {
            var order = await _db.OrdersDelivery.Where(x => x.CusId == id).Include(od => od.OderDetail).ThenInclude(x => x.DetailsNavigation).ThenInclude(od => od.OptionsDetail).ThenInclude(o => o.Options).Include(d => d.Payments).ToListAsync();
            return order;
        }
    }
}
