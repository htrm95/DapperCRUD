using DapperCRUD.Models;
using DapperCRUD.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DapperCRUD.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _iCustomerRepository;
        private readonly IAddressRepository _addressRepository;

        public CustomerController(ICustomerRepository iCustomerRepository, IAddressRepository addressRepository)
        {
            _iCustomerRepository = iCustomerRepository;
            _addressRepository = addressRepository;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _iCustomerRepository.GetAllAsync();
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _iCustomerRepository.GetByIdAsync(id);
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {

                await _iCustomerRepository.Create(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var _iCustomer = await _iCustomerRepository.GetByIdAsync(id);
            return View(_iCustomer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    customer.Id = id;

                    await _iCustomerRepository.Update(customer);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var _iCustomer = await _iCustomerRepository.GetByIdAsync(id);
            return View(_iCustomer);
        }

        [HttpDelete, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _iCustomerRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Address(int id)
        {
            var result = await _iCustomerRepository.GetAddressesAsync(id);
            return View(result);
        }

        public async Task<IActionResult> DetailsAddress(int id)
        {
            var result = await _addressRepository.GetByAddres(id);
            return View(result);
        }
        public IActionResult CreateAddress()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAddress(Address address)
        {
            if (ModelState.IsValid)
            {

                await _addressRepository.Create(address);
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

    }
}
