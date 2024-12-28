using HW13_InternetShop._Contract.QueryModels;

namespace HW13_InternetShop.Services
{
    public interface IShopServices<T>
    {
        T Add(T obj);
        bool Delete(int id);
        void DeleteAll();
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Update(int id, T updated);
        bool IsExist(int id);
    }
}