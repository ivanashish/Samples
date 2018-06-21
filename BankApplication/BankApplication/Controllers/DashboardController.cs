using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApplication.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Controllers
{
    public class DashboardController : Controller
    {
        private IBranchManager branchManager;
        private ICustomerManager customerManager;
        private IAccountManager accountManager;
        private ITransactionManager transactionManager;

        public DashboardController(IBranchManager branchManager, ICustomerManager customerManager, IAccountManager accountManager, ITransactionManager transactionManager)
        {
            this.branchManager = branchManager;
            this.accountManager = accountManager;
            this.customerManager = customerManager;
            this.transactionManager = transactionManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Branches = await this.branchManager.GetBranches();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsByAccountId(int accountId)
        {
            var transactions = await this.transactionManager.GetTransactionsByAccountId(accountId);
            return Json(transactions);
        }
    }
}