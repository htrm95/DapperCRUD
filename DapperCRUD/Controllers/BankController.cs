using DapperCRUD.Models;
using DapperCRUD.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperCRUD.Controllers
{

    public class BankController : Controller
    {

        private IBankRepository _bankRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BankController(IBankRepository bankRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bankRepository = bankRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IFormFile Photo { get; set; }

        public async Task<IActionResult> GetAllBanks()
        {
            var res = await _bankRepository.GetAllAsync();
            return View(res);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _bankRepository.GetByAddres(id);
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bank bank, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (bank.Logo != null)
                    {
                        string FileFolder =
                        Path.Combine(webHostEnvironment.WebRootPath, "css", bank.Logo);
                        System.IO.File.Delete(FileFolder);

                    }
                    bank.Logo = ProcessUploadFile();
                }
                await _bankRepository.Create(bank);
                return RedirectToAction(nameof(Index));
            }
            return View(bank);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var _Branch = await _bankRepository.GetByAddres(id);
            return View(_Branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bank bank)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bank.Id = id;

                    await _bankRepository.Update(bank);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bank);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var _Branch = await _bankRepository.GetByAddres(id);
            return View(_Branch);
        }

        [HttpDelete, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bankRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private string ProcessUploadFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string UploadsFolder =
                    Path.Combine(webHostEnvironment.WebRootPath, "css");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(UploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
