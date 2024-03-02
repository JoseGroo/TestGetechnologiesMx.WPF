using Microsoft.AspNetCore.Mvc;
using TestGetechnologiesMx.Logic;
using TestGetechnologiesMx.Repository.Entities;

namespace TestGetechnologiesMx.NetCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceLogic _invoiceLogic;

        public InvoiceController(IInvoiceLogic invoiceLogic)
        {
            _invoiceLogic = invoiceLogic;
        }

        [Route("Get")]
        public IActionResult Get([FromQuery]int personId)
        {
            try
            {
                var listPeople = _invoiceLogic.Get().Where(r => r.PersonId == personId);
                return Ok(listPeople);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrio un error al intentar obtener el listado de facturas.");
            }
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] Invoice invoice)
        {
            try
            {
                bool saved = _invoiceLogic.Add(invoice);
                if (saved)
                    return Ok();

                return BadRequest(_invoiceLogic.LastError);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrio un error al intentar guardar la factura.");
            }
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromQuery] int invoiceId)
        {
            try
            {
                bool saved = _invoiceLogic.Delete(invoiceId);
                if (saved)
                    return Ok();

                return BadRequest(_invoiceLogic.LastError);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrio un error al intentar eliminar la factura.");
            }
        }
    }
}
