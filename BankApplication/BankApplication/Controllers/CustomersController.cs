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
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private ICustomerManager customerManager;
        private IUnitOfWork unitOfWork;

        public CustomersController(ICustomerManager customerManager, IUnitOfWork unitOfWork)
        {
            this.customerManager = customerManager;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.customerManager.GetCustomers());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await this.customerManager.GetCustomerByID(id.Value);

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,MiddleName,LastName,DOB,Sex,PAN,ContactNumber,Address,Id")] CustomerEntity customer)
        {
            if (ModelState.IsValid)
            {
                await this.customerManager.InsertCustomer(customer);
                await this.unitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await this.customerManager.GetCustomerByID(id.Value);

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,MiddleName,LastName,DOB,Sex,PAN,ContactNumber,Address,Id")] CustomerEntity customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.customerManager.UpdateCustomer(customer);
                await this.unitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await this.customerManager.GetCustomerByID(id.Value);

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.customerManager.DeleteCustomer(id);
            await this.unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetCustomersByBranchId(int id)
        {
            var customers = await this.customerManager.GetCustomersByBranchId(id);
            return Json(new SelectList(customers, "Id", "FirstName"));
        }
    }
}
