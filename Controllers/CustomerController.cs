using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetCore_Dapper.Models;
using NetCore_Dapper.Repository;

namespace NetCore_Dapper.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerRepository customerRepository;
        public CustomerController(IConfiguration configuration)
        {
            customerRepository = new CustomerRepository(configuration);
        }
        public IActionResult Index()
        {
            return View(customerRepository.FindAll());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if(ModelState.IsValid)
            {
                customerRepository.Add(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        public IActionResult Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            Customer obj = customerRepository.FindByID(id.Value);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Customer obj)
        {
            if(ModelState.IsValid)
            {
                customerRepository.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if(id == null)
                return NotFound();
            customerRepository.Remove(id.Value);
            return RedirectToAction("Index");
        }
        
    }
}
