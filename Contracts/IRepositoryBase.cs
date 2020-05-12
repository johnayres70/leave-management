using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{

    // create a generic interface to accomodate CRUD operations of any class
   public  interface IRepositoryBase<T> where T: class
    {
        ICollection<T> FindAll(); // A collection of any class or Type

        T FindById(int Id);  // Find a particular recort of any type

        bool isExists(int id);
        bool Create(T Entity);  // Create a record of any type
        bool Update(T Entity);  // Update a record of any type
        bool Delete(T Entity);  // Delete a record of any type
        bool Save();

    }

}
