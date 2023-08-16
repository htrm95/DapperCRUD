using DapperCRUD.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DapperCRUD.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _iCustomerRepository;
        public CustomerController(ICustomerRepository iCustomerRepository)
        {
            _iCustomerRepository = iCustomerRepository;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _iCustomerRepository.GetAllAsync();
            return View(result);
        }

        //[HttpDelete, ActionName("DeleteConfirmed")]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _iCustomerRepository.Delete(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
