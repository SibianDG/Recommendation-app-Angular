using System.Linq;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbSet<Customer> _customers;
        private readonly ApplicationDbContext _context;


        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _customers = dbContext.Customers;
        }

        public Customer GetBy(string email)
        {
            return _customers.SingleOrDefault(c => c.Email == email);
        }

        public void Add(Customer customer)
        {
            _customers.Add(customer);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}