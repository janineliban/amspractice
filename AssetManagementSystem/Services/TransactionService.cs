using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssetManagementSystem.Models;

namespace AssetManagementSystem.Services
{
    public class TransactionService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TransactionService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void AddTransaction(Transaction transaction)
        {

             transaction.Id = Guid.NewGuid();
            _applicationDbContext.Transactions.Add(transaction);
            _applicationDbContext.SaveChanges();
           
        }
    }
}