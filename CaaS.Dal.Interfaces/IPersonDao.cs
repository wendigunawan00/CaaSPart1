using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaaS.Domain;

namespace CaaS.Dal.Interface;

public interface IPersonDao
{
    Task<IEnumerable<Person>> FindAllAsync(string table);
    Task<Person?> FindByIdAsync(string id,string table);

    Task<bool> UpdateAsync(Person person, string table);
    Task<bool> DeleteByIdAsync(string id, string table);
    Task<bool> StoreAsync(Person person, string table);

    
}
