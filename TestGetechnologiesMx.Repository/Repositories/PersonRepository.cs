using TestGetechnologiesMx.Repository.Entities;

namespace TestGetechnologiesMx.Repository.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
    }

    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(ProjectModel context) : base(context)
        {
        }
    }
}
