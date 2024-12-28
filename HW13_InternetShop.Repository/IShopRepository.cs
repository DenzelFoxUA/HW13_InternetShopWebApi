

namespace HW13_InternetShop.Repository
{
    public interface IShopRepository<T>
    {
        public IEnumerable<T> GetAll(); //read all

        public void DeleteAll(); //delete all

        public T Add(T entity); //create

        public T GetById(int id); // read

        public T Update(int id, T entity); // update

        public void Delete(int id); //delete
    }
}