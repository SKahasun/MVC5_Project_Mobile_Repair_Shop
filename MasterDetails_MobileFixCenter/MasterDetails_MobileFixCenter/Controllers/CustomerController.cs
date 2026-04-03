using MasterDetails_MobileFixCenter.Models;
using MasterDetails_MobileFixCenter.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterDetails_MobileFixCenter.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly MobileServicingDBContext db = new MobileServicingDBContext();
        // GET: Customer
        [AllowAnonymous]
        public ActionResult Index(int id=1)
        {
            return View(db.Customers.Include(x => x.ServiceEntries.Select(s => s.Service)).OrderByDescending(x => x.CustomerId).ToList().ToPagedList(id, 8));
        }
        public ActionResult AddNewService(int? id)
        {
            ViewBag.service=new SelectList(db.Services.ToList(), "ServiceId", "ServiceName",(id!=null)?id.ToString():"");
            return PartialView("_addNewService");
        }
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(CustomerVM customerVM, int[] serviceId)
        {
            if(ModelState.IsValid)
            {
                Customer customer = new Customer()
                {
                    CustomerName = customerVM.CustomerName,
                    Address = customerVM.Address,
                    Phone = customerVM.Phone,
                    EntryDate = customerVM.EntryDate,
                    IsRegular = customerVM.IsRegular,
                    Problem = customerVM.Problem,
                    DeviceName = customerVM.DeviceName
                };
                HttpPostedFileBase file = customerVM.DevicePictureFile;
                if(file != null)
                {
                    int maxSize = 5 * 1024 * 1024;

                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("PictureFile", "Only JPG and PNG webp files are allowed.");
                        return PartialView("_error");
                    }

                    if (file.ContentLength > maxSize)
                    {
                        ModelState.AddModelError("PictureFile", "Image size must be less than or equal to 5 MB.");
                        return PartialView("_error");
                    }
                    string fileName= Path.Combine("/Image/", DateTime.Now.Ticks.ToString()+Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(fileName));
                    customer.DevicePicture = fileName;
                }
                foreach (var i in serviceId)
                {
                    ServiceEntry serviceEntry = new ServiceEntry()
                    {
                        Customer=customer,
                        CustomerId=customer.CustomerId,
                        ServiceId=i
                    };
                    db.ServiceEntries.Add(serviceEntry);
                }
                db.SaveChanges();
                return PartialView("_success");
            }
            return PartialView("_error");

        }
        public ActionResult Edit(int? id)
        {
            Customer customer=db.Customers.FirstOrDefault(x=>x.CustomerId==id);
            var customerService = db.ServiceEntries.Where(x => x.CustomerId == id).ToList();
            CustomerVM customerVM = new CustomerVM()
            {
                CustomerId= customer.CustomerId,
                CustomerName= customer.CustomerName,
                EntryDate= customer.EntryDate,
                Address= customer.Address,
                Phone= customer.Phone,
                Problem= customer.Problem,
                DeviceName= customer.DeviceName,
                IsRegular= customer.IsRegular,
                DevicePicture= customer.DevicePicture,
            };
            if(customerService.Count()>0)
            {
                foreach (var item in customerService)
                {
                    customerVM.ServiceList.Add(item.ServiceId);
                }
            }
            return View(customerVM);
        }
        [HttpPost]
        public ActionResult Edit(CustomerVM customerVM,int[] serviceId)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer()
                {
                    CustomerId = customerVM.CustomerId,
                    CustomerName= customerVM.CustomerName,
                    EntryDate= customerVM.EntryDate,
                    Address= customerVM.Address,
                    Phone= customerVM.Phone,
                    Problem= customerVM.Problem,
                    DeviceName= customerVM.DeviceName,
                    IsRegular= customerVM.IsRegular

                };
                HttpPostedFileBase file = customerVM.DevicePictureFile;
                if (file != null) 
                {
                    int maxSize = 5 * 1024 * 1024;

                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("PictureFile", "Only JPG and PNG webp files are allowed.");
                        return PartialView("_error");
                    }

                    if (file.ContentLength > maxSize)
                    {
                        ModelState.AddModelError("PictureFile", "Image size must be less than or equal to 5 MB.");
                        return PartialView("_error");
                    }
                    string fileName = Path.Combine("/Image/", DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(fileName));
                    customer.DevicePicture = fileName;
                }
                else
                {
                    customer.DevicePicture = customerVM.DevicePicture;
                }
                var serviceEntry = db.ServiceEntries.Where(x => x.CustomerId == customer.CustomerId).ToList();
                foreach (var service in serviceEntry)
                {
                    db.ServiceEntries.Remove(service);
                }
                foreach (var id in serviceId)
                {
                    ServiceEntry serviceEntry1 = new ServiceEntry()
                    {
                        CustomerId = customer.CustomerId,
                        ServiceId = id
                    };
                    db.ServiceEntries.Add(serviceEntry1);
                }
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_success");
            }
            return PartialView("_error");
        }
        public ActionResult Delete(int? id)
        {
            Customer customer = db.Customers.First(x => x.CustomerId == id);
            var serviceEntries = db.ServiceEntries.Where(x => x.CustomerId == id).ToList();
            CustomerVM customerVM = new CustomerVM()
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                Phone = customer.Phone,
                EntryDate = customer.EntryDate,
                DeviceName = customer.DeviceName,
                Problem = customer.Problem,
                IsRegular = customer.IsRegular,
                Address = customer.Address,
                DevicePicture = customer.DevicePicture
            };
            if (serviceEntries.Count() > 0)
            {
                foreach (var item in serviceEntries)
                {
                    customerVM.ServiceList.Add(item.ServiceId);
                }
            }
            return View(customerVM);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            var serviceEntry = db.ServiceEntries.Where(x => x.CustomerId == customer.CustomerId).ToList();
            db.ServiceEntries.RemoveRange(serviceEntry);
            db.Entry(customer).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}