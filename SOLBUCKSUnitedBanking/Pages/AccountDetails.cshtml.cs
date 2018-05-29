using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOLBUCKSUnitedBanking.Data;
using SOLBUCKSUnitedBanking.Services;

namespace SOLBUCKSUnitedBanking.Pages
{
    public class AccountDetailsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBankingService _bankingService;

        public AccountDetailsModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _bankingService = new BankingService(context);
            Accounts = new List<AccountModel>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<AccountModel> Accounts { get; set; }

        public class AccountModel
        {
            public string Amount { get; set; }

            public int AccountId { get; set; }

            public string AccountNumber { get; set; }
        }

        public void OnGet()
        {
            List<Data.Account> accounts = _bankingService.GetAccountsForUser(_userManager.GetUserId(User));
            foreach (Data.Account account in accounts)
            {
                Accounts.Add(new AccountModel() { AccountId = account.Id, AccountNumber = account.AccountNumber, Amount = account.Amount.HasValue ? account.Amount.Value.ToString("0.00") : "0.00" });
                FirstName = string.IsNullOrEmpty(FirstName) ? account.FirstName : string.Empty;
                LastName = string.IsNullOrEmpty(LastName) ? account.LastName : string.Empty;
            }
        }
    }
}
