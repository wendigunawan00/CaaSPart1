using CaaS.Dal.Interface;
using CaaS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CaaS.Client;

internal class DalPersonTester
{
    // null prüfen
    private readonly IPersonDao personDao;
    private readonly string table ="Mandants";

    public DalPersonTester(IPersonDao personDao)
    {
        this.personDao = personDao;
    }

    public async Task TestFindAllAsync()
    {
        (await personDao.FindAllAsync(table))
            .ToList()
            .ForEach(p => Console.WriteLine($"{p.Id,5} | {p.FirstName,-10} | {p.LastName,-15} | {p.DateOfBirth,10:yyyy-MM-dd}"));
    }

    public async Task TestFindByIdAsync()
    {
        Person? person1 = await personDao.FindByIdAsync("mandant-1",table);
        Console.WriteLine($"FindById(1) -> {person1?.ToString() ?? "<null>"}");

        Person? person2 = await personDao.FindByIdAsync("mandant-2",table);
        Console.WriteLine($"FindById(99) -> {person2?.ToString() ?? "<null>"}");
    }

    public async Task TestUpdateAsync()
    {
        Person? person = await personDao.FindByIdAsync("mandant-1",table);
        Console.WriteLine($"before update: person -> {person?.ToString() ?? "<null>"}");
        if (person is null)
        {
            return;
        }

        person.DateOfBirth = person.DateOfBirth.AddYears(-1);
        await personDao.UpdateAsync(person,table);

        person = await personDao.FindByIdAsync("mandant-1",table);
        Console.WriteLine($"after update:  person -> {person?.ToString() ?? "<null>"}");
    }


    public async Task TestDeleteByIdAsync()
    {
        Person? person = await personDao.FindByIdAsync("mandant-2", table);
        Console.WriteLine($"before update: person -> {person?.ToString() ?? "<null>"}");
        if (person is null)
        {
            return;
        }

        await personDao.DeleteByIdAsync(person.Id, table);
        person = await personDao.FindByIdAsync("mandant-2", table);
        Console.WriteLine($"after update:  person -> {person?.ToString() ?? "<null>"}");
    }

    public async Task TestStoreAsync()
    {
        Person? person = await personDao.FindByIdAsync("mandant-2", table);
        Console.WriteLine($"before update: person -> {person?.ToString() ?? "<null>"}");
       
        person = new Person("mandant-2", "ryo", "kimura", new DateTime(1971,12,7), "ryokimura@example.com", "addr-m2", "mandant");        
        await personDao.StoreAsync(person, table);
        person = await personDao.FindByIdAsync("mandant-2", table);
        Console.WriteLine($"after update:  person -> {person?.ToString() ?? "<null>"}");
    }

    public async Task TestTransactionsAsync()
    {
        Person? person1 = await personDao.FindByIdAsync("mandant-1",table);
        Person? person2 = await personDao.FindByIdAsync("mandant-2",table);
        if (person1 is null || person2 is null)
        {
            Console.WriteLine("Cannot perfom test because persons with id 1 and 2 are required");
            return;
        }

        DateTime oldDate1 = person1.DateOfBirth;
        DateTime oldDate2 = person2.DateOfBirth;
        DateTime newDate1 = DateTime.MinValue;
        DateTime newDate2 = DateTime.MinValue;

        try
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                person1.DateOfBirth = newDate1 = oldDate1.AddDays(1);
                person2.DateOfBirth = newDate2 = oldDate2.AddDays(1);
                await personDao.UpdateAsync(person1,"Mandants");
                //throw new ArgumentException(); // comment this out to rollback transaction
                await personDao.UpdateAsync(person2,"Mandants");
                scope.Complete();
            }
        }
        catch
        {
        }

        person1 = await personDao.FindByIdAsync("mandant-1",table);
        person2 = await personDao.FindByIdAsync("mandant-2",table);
        if (person1 is null || person2 is null)
        {
            Console.WriteLine("Cannot perfom test because persons with id 1 and 2 are required");
            return;
        }

        if (oldDate1 == person1.DateOfBirth && oldDate2 == person2.DateOfBirth)
            Console.WriteLine("Transaction was ROLLED BACK.");
        else if (newDate1 == person1.DateOfBirth && newDate2 == person2.DateOfBirth)
            Console.WriteLine("Transaction was COMMITTED.");
        else
            Console.WriteLine("No Transaction.");
    }
}
