using TestGetechnologiesMx.Repository.Entities;
using TestGetechnologiesMx.Repository.Repositories;

namespace TestGetechnologiesMx.Logic
{
    public interface IInvoiceLogic : IBaseLogic<Invoice>
    {
    }

    public class InvoiceLogic : BaseLogic<Invoice>, IInvoiceLogic
    {
        public InvoiceLogic(IInvoiceRepository repository) : base(repository)
        {
        }
    }
}
