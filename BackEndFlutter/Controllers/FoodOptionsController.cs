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
    public class FoodOptionsController : ControllerBase
    {

        private readonly ProjectflutterContext _db;

        public FoodOptionsController(ProjectflutterContext db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<ActionResult<List<FoodOptions>>> GetCategoryFoods()
        {
            var fo = await _db.FoodOptions.Include(p => p.Food).Include(x => x.Options).ThenInclude(m => m.OptionsDetail).ToListAsync();
            return fo;
        }

    }
}
