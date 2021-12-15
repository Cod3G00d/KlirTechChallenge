using System.Linq;
using System.Collections.Generic;
using KlirTechChallenge.Domain.Payments;
using KlirTechChallenge.Infrastructure.Database.Context;
using System;
using System.Threading;
using System.Threading.Tasks;
using KlirTechChallenge.Domain.SeedWork;
using KlirTechChallenge.Domain.Promotions;
using Microsoft.EntityFrameworkCore;
using KlirTechChallenge.Domain.Customers;

namespace KlirTechChallenge.Infrastructure.Domain.Customers
{
    public class Customers : ICustomers
    {
        private readonly KlirTechChallengeContext _dbContext;

        public Customers(KlirTechChallengeContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Add(Customer customer, CancellationToken cancellationToken = default)
        {
            await _dbContext.Customers.AddAsync(customer, cancellationToken);
        }

        public async Task<Customer> GetById(CustomerId id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Customer> GetByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers
                .Where(c => c.Email == email)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}