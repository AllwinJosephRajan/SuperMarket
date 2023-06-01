using Microsoft.AspNetCore.Mvc;
using SuperMarket.Data;
using SuperMarket.Models;

namespace SuperMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMarketController : ControllerBase
    {
        // Declaring a private readonly field named _dbContext of type ApplicationDbContext.
        private readonly ApplicationDbContext _dbContext;

        // Injecting instance of the ApplicationDbContext into the SMarketController class. 
        public SMarketController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<SMarketModel>> GetAll()
        {
            return _dbContext.SMarket.ToList();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<SMarketModel> GetById(int id)
        {
            var smarketmodel = _dbContext.SMarket.Find(id);
            if (smarketmodel == null)
            {
                return NoContent();
            }
            return Ok(smarketmodel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<SMarketModel> Create([FromBody] SMarketModel smarketmodel)
        {
            // To avoid duplicate records getting stored in database 
            var result = _dbContext.SMarket.AsQueryable().Where(x => x.Categories.ToLower().Trim() == smarketmodel.Categories.ToLower().Trim()).Any();
            if (result)
            {
                return Conflict("Data already exists in Database");
            }

            _dbContext.SMarket.Add(smarketmodel);
            _dbContext.SaveChanges();

            // The created data will be displayed as responce 
            return CreatedAtAction("GetById", new { id = smarketmodel.Id },smarketmodel);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<SMarketModel> Update(int id, [FromBody] SMarketModel smarketmodel)
        {
            if(smarketmodel == null || id != smarketmodel.Id)
            {
                return BadRequest();
            }
            var smarketFromDb = _dbContext.SMarket.Find(id);
            if (smarketFromDb == null)
            {
                return NotFound();
            }
            smarketFromDb.Categories = smarketmodel.Categories;
            smarketFromDb.PricingAndDiscounts = smarketmodel.PricingAndDiscounts;
            smarketFromDb.AvailableOffers = smarketmodel.AvailableOffers;
            smarketFromDb.ReviewAndRatings = smarketmodel.ReviewAndRatings;
            
            _dbContext.SMarket.Update(smarketFromDb);
            _dbContext.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var smarket = _dbContext.SMarket.Find(id);
            if (smarket == null)
            {
                return NotFound();  
            }
            _dbContext.SMarket.Remove(smarket);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
