using TestGetechnologiesMx.Repository.Entities;

namespace TestGetechnologiesMx.Repository.Repositories
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
    }

    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ProjectModel context) : base(context)
        {
        }
    }
}
