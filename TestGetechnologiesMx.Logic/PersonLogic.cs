using TestGetechnologiesMx.Repository.Entities;
using TestGetechnologiesMx.Repository.Repositories;

namespace TestGetechnologiesMx.Logic
{
    public interface IPersonLogic : IBaseLogic<Person>
    {
        bool Save(Person person);
    }

    public class PersonLogic : BaseLogic<Person>, IPersonLogic
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public PersonLogic(IPersonRepository repository, IInvoiceRepository invoiceRepository) : base(repository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public bool Save(Person person)
        {
            try
            {
                bool existIdentification = _repository.GetAll().Where(r => r.Identification == person.Identification && r.Id != person.Id).Any();
                if (existIdentification)
                {
                    LastError = "El identificador que intanta registrar ya existe en otra persona.";
                    return false;
                }

                if (person.Id > 0)
                    _repository.Edit(person);
                else
                    _repository.Add(person);

                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                LastError = "Ocurrio un error inesperado al intentar guardar la persona.";
                return false;
            }
        }

        public override bool Delete(int id)
        {
            var invoices = _invoiceRepository.GetAll().Where(r => r.PersonId == id);

            if (invoices.Any())
            {
                foreach(var invoice in invoices)
                {
                    _invoiceRepository.Delete(invoice);
                }

                _invoiceRepository.Save();
            }

            return base.Delete(id);
        }
    }
}
