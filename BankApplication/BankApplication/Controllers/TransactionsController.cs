using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankApplication.Repositories;
using BankApplication.Repositories.DbModels;
using Microsoft.AspNetCore.Authorization;
using BankApplication.BusinessLogic;
using BankApplication.Models;

namespace BankApplication.Controllers
{
    public class TransactionsController : Controller
    {
        private IAccountManager accountManager;
        private ITransactionManager transactionManager;
        private IUnitOfWork unitOfWork;

        public TransactionsController(IAccountManager accountManager, ITransactionManager transactionManager, IUnitOfWork unitOfWork)
        {
            this.accountManager = accountManager;
            this.transactionManager = transactionManager;
            this.unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Index()
        {
            return View(await this.transactionManager.GetTransactions());
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await this.transactionManager.GetTransactionByID(id.Value);

            return View(transaction);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create()
        {
            ViewData["AccountId"] = new SelectList(await this.accountManager.GetAccounts(), "Id", "Id");
            return View();
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,Type,Amount,Id")] TransactionEntity transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.TransDate = DateTime.UtcNow;
                var account = await this.accountManager.GetAccountByID(transaction.AccountId);

                if(transaction.Type == "Deposit")
                {
                    transaction.Balance = account.Balance + transaction.Amount;
                    account.Balance += transaction.Amount;
                }
                else if (transaction.Type == "Withdrawl")
                {
                    if (account.Balance - transaction.Amount < 0)
                    {
                        throw new Exception("Insufficient balance");
                    }
                    else
                    {
                        transaction.Balance = account.Balance - transaction.Amount;
                        account.Balance -= transaction.Amount;
                    }
                }

                await this.transactionManager.InsertTransaction(transaction);
                await this.unitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(await this.accountManager.GetAccounts(), "Id", "Id", transaction.AccountId);
            return View(transaction);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await this.transactionManager.GetTransactionByID(id.Value);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(await this.accountManager.GetAccounts(), "Id", "Id", transaction.AccountId);
            return View(transaction);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,Type,Amount,Id")] TransactionEntity transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.transactionManager.UpdateTransaction(transaction);
                await this.unitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(await this.accountManager.GetAccounts(), "Id", "Id", transaction.AccountId);
            return View(transaction);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await this.transactionManager.GetTransactionByID(id.Value);

            return View(transaction);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.transactionManager.DeleteTransaction(id);
            await this.unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GetTransactionsByAccountId(int accountId)
        {
            return Json(new List<TransactionEntity> { new TransactionEntity { Id = 1 }, new TransactionEntity { Id = 2 } });
        }
    }
}
