using TestGetechnologiesMx.Repository.Repositories;


namespace TestGetechnologiesMx.Logic
{
    public interface IBaseLogic<TModel> : IDisposable where TModel : class, new()
    {
        string LastError { get; }
        IQueryable<TModel> Get();
        TModel Get(object id);
        bool Add(TModel model);
        bool Edit(TModel model);
        bool Delete(int id);
    }
    public class BaseLogic<TModel> : IBaseLogic<TModel> where TModel : class, new()
    {
        protected readonly IBaseRepository<TModel> _repository;

        public string LastError { get; protected set; }

        public BaseLogic(IBaseRepository<TModel> repository)
        {
            _repository = repository;
        }

        public virtual IQueryable<TModel> Get()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                LastError = "Problem retrieving data";
                return Enumerable.Empty<TModel>().AsQueryable();
            }
        }

        public virtual TModel Get(object id)
        {
            try
            {
                return _repository.Find(id);
            }
            catch (Exception ex)
            {
                LastError = "Problem retrieving data";
                return null;
            }
        }

        public virtual bool Add(TModel model)
        {
            try
            {
                _repository.Add(model);
                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                LastError = "Problem saving data";
                return false;
            }
        }

        public virtual bool Edit(TModel model)
        {
            try
            {
                _repository.Edit(model);
                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                LastError = "Problem saving data";
                return false;
            }
        }

        public virtual bool Delete(int id)
        {
            try
            {
                var model = _repository.Find(id);
                _repository.Delete(model);
                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                LastError = "Problem saving data";
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            _repository.Dispose();
        }
    }
}
