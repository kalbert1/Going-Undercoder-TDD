using SOLBUCKSUnitedBanking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOLBUCKSUnitedBanking.Services
{
    public interface IBankingService
    {
        
        List<Account> GetAccountsAvailableForTransfer(int currentAccountId);

        List<Account> GetAccountsForUser(string userid);

        void Deposit(decimal amount, int accountId);

        void Withdraw(decimal amount, int accountId);

        void Transfer(int accountId, int transferToAccountId, decimal amount);
    }
}
