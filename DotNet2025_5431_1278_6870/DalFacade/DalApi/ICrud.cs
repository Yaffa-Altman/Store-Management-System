

namespace DalApi
{
    public interface ICrud<T>
    {
        int Create(T item); //Creates new entity object in DAL => Return id 
        T? Read(int id); //Reads entity object by this ID => Return Object by id 
        T? Read(Func<T, bool> filter);  //Reads entity object function filter => Return Object
        List<T?> ReadAll(Func<T, bool>? filter = null); //stage 1 only, Reads all entity objects => Return a list of all the Objects
        void Update(T item); //Updates entity object
        void Delete(int id); //Deletes an object by this Id
    }
}
