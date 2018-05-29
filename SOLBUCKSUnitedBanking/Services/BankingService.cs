using SOLBUCKSUnitedBanking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOLBUCKSUnitedBanking.Services
{
    public class BankingService : IBankingService
    {
        private ApplicationDbContext _context;

        public BankingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Account> GetAccountsAvailableForTransfer(int currentAccountId)
        {
            return _context.Accounts.Where(a => a.Id != currentAccountId && a.Active).ToList() ?? new List<Account>();
        }

        public List<Account> GetAccountsForUser(string userid)
        {
            return _context.Accounts.Where(a => a.UserId == userid && a.Active).ToList() ?? new List<Account>();
        }


        public void Deposit(decimal amount, int accountId)
        {
            Account account = _context.Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account != null)
            {
                if (amount < 1)
                {
                    throw new Exception($"The amount to deposit must be at least 1 dollar.");
                }
                if (amount > 1000000)
                {
                    throw new Exception($"The amount to deposit must be between 1 and 1,000,000 dollars.");
                }
                account.Amount += amount;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Cannot find user account with an id of {accountId}.");
            }
        }

        public void Withdraw(decimal amount, int accountId)
        {
            Account account = _context.Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account != null)
            {
                if (account.Amount < amount)
                {
                    throw new Exception($"The amount of {amount} dollars exceeds the available account balance.");
                }
                if (amount < 1)
                {
                    throw new Exception($"The amount to withdraw must be at least 1 dollar.");
                }
                if (amount > 1000000)
                {
                    throw new Exception($"The amount to withdraw must be between 1 and 1,000,000 dollars.");
                }
                account.Amount -= amount;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Cannot find user account with an id of {accountId}.");
            }
        }

        public void Transfer(int accountId, int transferToAccountId, decimal amount)
        {
            Account account = _context.Accounts.FirstOrDefault(a => a.Id == accountId);
            Account transferAccount = _context.Accounts.FirstOrDefault(a => a.Id == transferToAccountId);
            if (account != null)
            {
                if (transferAccount != null)
                {
                    if (account.Amount < amount)
                    {
                        throw new Exception($"The amount of {amount} dollars exceeds the available account balance.");
                    }
                    if (amount < 1)
            {
                throw new Exception($"The amount to transfer must be at least 1 dollar.");
            }
                    if (amount > 1000000)
                    {
                        throw new Exception($"The amount to transfer must be between 1 and 1,000,000 dollars.");
                    }
                    account.Amount -= amount;
            transferAccount.Amount += amount;
            _context.SaveChanges();
                }
                else
                {
                    throw new Exception($"Cannot find user account with an id of {transferToAccountId}.");
                }
            }
            else
            {
                throw new Exception($"Cannot find user account with an id of {accountId}.");
            }
        }
    }
}
