using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp.BusinessLogic;
using WebApp.Models;
using WebApp.Repository;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BranchesController : Controller
    {
        private IBranchManager branchManager;
        private IUnitOfWork unitOfWork;

        public BranchesController(IBranchManager branchManager, IUnitOfWork unitOfWork)
        {
            this.branchManager = branchManager;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.branchManager.GetBranches());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await this.branchManager.GetBranchByID(id.Value);

            return View(branch);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Code,IFSC,Address,Id")] BranchEntity branch)
        {
            if (ModelState.IsValid)
            {
                this.branchManager.InsertBranch(branch);
                await this.unitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await this.branchManager.GetBranchByID(id.Value);

            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Code,IFSC,Address,Id")] BranchEntity branch)
        {
            if (id != branch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.branchManager.UpdateBranch(branch);
                await this.unitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await this.branchManager.GetBranchByID(id.Value);

            return View(branch);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.branchManager.DeleteBranch(id);
            await this.unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }
    }
}
