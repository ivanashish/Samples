using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BankApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
using BankApplication.BusinessLogic;
using BankApplication.Models;

namespace BankApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountsController : Controller
    {
        private IAccountManager accountManager;
        private IBranchManager branchManager;
        private ICustomerManager customerManager;
        private IUnitOfWork unitOfWork;

        public AccountsController(IAccountManager accountManager, IBranchManager branchManager, ICustomerManager customerManager, IUnitOfWork unitOfWork)
        {
            this.accountManager = accountManager;
            this.branchManager = branchManager;
            this.customerManager = customerManager;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var accounts = await this.accountManager.GetAccounts();
            return View(accounts);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await this.accountManager.GetAccountByID(id.Value);

            return View(account);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["BranchId"] = new SelectList(await this.branchManager.GetBranches(), "Id", "Name");
            ViewData["CustomerId"] = new SelectList(await this.customerManager.GetCustomers(), "Id", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,BranchId,Type,Balance,MinimumBalance,Id")] AccountEntity account)
        {
            if (ModelState.IsValid)
            {
                await this.accountManager.InsertAccount(account);
                await this.unitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(await this.branchManager.GetBranches(), "Id", "Name", account.BranchId);
            ViewData["CustomerId"] = new SelectList(await this.customerManager.GetCustomers(), "Id", "FirstName", account.CustomerId);
            return View(account);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await this.accountManager.GetAccountByID(id.Value);

            ViewData["BranchId"] = new SelectList(await this.branchManager.GetBranches(), "Id", "Name", account.BranchId);
            ViewData["CustomerId"] = new SelectList(await this.customerManager.GetCustomers(), "Id", "FirstName", account.CustomerId);
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,BranchId,Type,Balance,MinimumBalance,Id")] AccountEntity account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.accountManager.UpdateAccount(account);
                await this.unitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BranchId"] = new SelectList(await this.branchManager.GetBranches(), "Id", "Name", account.BranchId);
            ViewData["CustomerId"] = new SelectList(await this.customerManager.GetCustomers(), "Id", "FirstName", account.CustomerId);
            return View(account);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await this.accountManager.GetAccountByID(id.Value);
            
            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.accountManager.DeleteAccount(id);
            await this.unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetAccountsByBranchAndCustomerIds(int branchId, int customerId)
        {
            var accounts = await this.accountManager.GetAccountsByBranchAndCustomerIds(branchId, customerId);
            return Json(new SelectList(accounts, "Id", "Id"));
        }
    }
}
