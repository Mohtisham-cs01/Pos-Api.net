
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using taskApi.Data;
using taskApi.DataModels;
using taskApi.Models;

namespace taskApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAny")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("done");
        }

        [HttpPost]
        public IActionResult Post(SalesTransection transaction)
        {
            try
            {
                ValidateTransaction(transaction);

                // Create SalesMaster
                var invoiceAmount = CalculateTotalCost(transaction.ItemIds);
                var salesMaster = new SalesMaster
                {
                    CustomerId = transaction.CustomerId,
                    InvoiceDate = DateTime.Now,
                    InvoiceAmount = invoiceAmount
                };

                _context.SalesMasters.Add(salesMaster);
                _context.SaveChanges();

                // Get the saved SalesMaster ID
                var savedMasterId = salesMaster.InvoiceId;

                // Create SalesDetails for each item
                foreach (var itemId in transaction.ItemIds)
                {
                    var item = _context.Items.Find(itemId);

                    // Validate that the item with the given itemId exists
                    if (item == null)
                    {
                        // Rollback the transaction and return an error response
                        _context.SalesMasters.Remove(salesMaster);
                        _context.SaveChanges();
                        return BadRequest($"Item with ID {itemId} not found.");
                    }

                    var cost = item.ItemCost;

                    var salesDetails = new SalesDetails
                    {
                        InvoiceId = savedMasterId,
                        Item = itemId.ToString(),
                        Cost = cost
                    };

                    _context.SalesDetails.Add(salesDetails);
                }

                _context.SaveChanges();

                return Ok("Sales record created successfully");
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Route("DeleteMaster/{id}")]
        public IActionResult DeleteSalesInfo(int id)
        {
            Console.WriteLine($"Received id: {id}");
            try
            {
                // Retrieve SalesMaster with the specified ID
                var salesMaster = _context.SalesMasters.FirstOrDefault(sm => sm.InvoiceId == id);

                if (salesMaster == null)
                {
                    return NotFound($"SalesMaster with InvoiceId {id} not found.");
                }

                // Retrieve and delete associated SalesDetails where MasterInvoiceId matches the provided ID
                var salesDetailsToDelete = _context.SalesDetails.Where(sd => sd.InvoiceId == id).ToList();
                _context.SalesDetails.RemoveRange(salesDetailsToDelete);

                // Delete SalesMaster
                _context.SalesMasters.Remove(salesMaster);

                // Save changes to the database
                _context.SaveChanges();

                return Ok($"SalesMaster with InvoiceId {id} and associated SalesDetails deleted successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting sales information.");
            }
        }



        [HttpGet]
        [Route("GetAllSalesInfo")]
        public IActionResult GetAllSalesInfo()
        {
            try
            {
                // Retrieve all SalesMasters
                var salesMasters = _context.SalesMasters.ToList();

                // Retrieve all SalesDetails
                var salesDetails = _context.SalesDetails.ToList();

                // Combine SalesMasters and SalesDetails based on InvoiceId
                var salesInfo = salesMasters.Select(salesMaster => new
                {
                    SalesMaster = salesMaster,
                    SalesDetails = salesDetails.Where(sd => sd.InvoiceId == salesMaster.InvoiceId).ToList()
                }).ToList();

                return Ok(salesInfo);
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching sales information.");
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetSalesInfo(int id)
        {
            try
            {
                // Retrieve SalesMaster
                var salesMaster = _context.SalesMasters
                    .FirstOrDefault(sm => sm.InvoiceId == id);

                if (salesMaster == null)
                {
                    return NotFound($"SalesMaster with InvoiceId {id} not found.");
                }

                // Retrieve associated SalesDetails
                var salesDetails = _context.SalesDetails
                    .Where(sd => sd.InvoiceId == id)
                    .ToList();

                // Combine SalesMaster and SalesDetails in the response
                var salesInfo = new
                {
                    SalesMaster = salesMaster,
                    SalesDetails = salesDetails
                };

                return Ok(salesInfo);
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching sales information.");
            }
        }
        private List<int> GetInvalidItemIds(List<int> itemIds)
        {
            // Validate that there is at least one item in the list
            if (itemIds == null || itemIds.Count == 0)
            {
                throw new ArgumentException("At least one item ID is required for calculation.");
            }

            // Find invalid item IDs
            var invalidItemIds = itemIds.Except(_context.Items.Select(i => i.ItemId)).ToList();

            return invalidItemIds;
        }

        private decimal CalculateTotalCost(List<int> itemIds)
        {
            var invalidItemIds = GetInvalidItemIds(itemIds);

            if (invalidItemIds.Any())
            {
                throw new ArgumentException($"The following item IDs do not correspond to any items: {string.Join(", ", invalidItemIds)}");
            }

            // Calculate total cost based on valid itemIds
            var totalCost = _context.Items
                .Where(i => itemIds.Contains(i.ItemId))
                .Sum(i => i.ItemCost);

            return totalCost;
        }


        private void ValidateTransaction(SalesTransection transaction)
        {
            // Validate that the transaction object is not null
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction), "Sales transaction object cannot be null.");
            }

            // Validate that there is at least one item in the transaction
            if (transaction.ItemIds == null || transaction.ItemIds.Count == 0)
            {
                throw new ArgumentException("At least one item is required in the sales transaction.");
            }
        }
    }
}

