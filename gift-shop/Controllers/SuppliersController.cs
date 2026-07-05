using gift_shop.Models;
using gift_shop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace gift_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // =========================
        // GET: api/suppliers
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        // =========================
        // GET: api/suppliers/{id}
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);

            if (supplier == null)
                return NotFound(new { message = "Supplier not found" });

            return Ok(supplier);
        }

        // =========================
        // POST: api/suppliers
        // =========================
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdSupplier = await _supplierService.CreateSupplierAsync(supplier);

            return Ok(new
            {
                message = "Supplier created successfully",
                data = createdSupplier
            });
        }

        // =========================
        // PUT: api/suppliers/{id}
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] Supplier supplier)
        {
            if (id != supplier.supplier_id)
                return BadRequest(new { message = "Supplier ID mismatch" });

            var updated = await _supplierService.UpdateSupplierAsync(supplier);

            if (!updated)
                return NotFound(new { message = "Supplier not found" });

            return Ok(new { message = "Supplier updated successfully" });
        }

        // =========================
        // DELETE: api/suppliers/{id}
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var deleted = await _supplierService.DeleteSupplierAsync(id);

            if (!deleted)
                return NotFound(new { message = "Supplier not found" });

            return Ok(new { message = "Supplier deleted successfully" });
        }
    }
}
