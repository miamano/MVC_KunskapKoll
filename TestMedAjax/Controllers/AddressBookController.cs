using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMedAjax.Repository;
using TestMedAjax.Models;

namespace TestMedAjax.Controllers
{
    public class AddressBookController : Controller
    {
        public static AddressBookRepository Repo;

        [HttpGet]
        public ActionResult Index()
        {
            Repo = AddressBookRepository.getRepo();
            return View(Repo.GetAddressBooks());
        }

        [HttpGet]
        public ActionResult CreateNewItem()
        {
            return PartialView("CreateNewItem");
        }

        [HttpPost]
        public ActionResult AjaxCreateNewItem(string name, string address, string tnr)
        {
            // TODO: Validera tnr

            Guid id = Guid.NewGuid();
            AddressBook postAB = new AddressBook() { AddressBookId = id , Name = name, Address = address, Tnr = tnr, LastUpdate = DateTime.Now };
            Repo.AddNewItem(postAB);

            //Guid id = Repo.AjaxAddNewAlbum(name, comment);
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewItem(AddressBook ab)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(ab.Name) || string.IsNullOrWhiteSpace(ab.Tnr))
                {
                    ModelState.AddModelError("error", "Error: Name or telephone number is empty!");
                    return View(ab);
                }

                AddressBook postAB = new AddressBook() { AddressBookId = Guid.NewGuid(), Name = ab.Name, Address = ab.Address, Tnr = ab.Tnr, LastUpdate = DateTime.Now };
                Repo.AddNewItem(postAB);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            return View(Repo.GetItemByID(id));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddressBook ab)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(ab.Name) || string.IsNullOrWhiteSpace(ab.Tnr))
                {
                    ModelState.AddModelError("error", "Error: Name or telephone number is empty!");
                    return View(ab);
                }

                ab.LastUpdate = DateTime.Now;
                Repo.EditItem(ab);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            return View(Repo.GetItemByID(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(AddressBook ab)
        {
            if (ModelState.IsValid)
            {
                Repo.EditItem(ab);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]  //OBS! AJAX: HttpPost Server: [HttpGet]
        public ActionResult Delete(Guid id)
        {
            Repo.DeleteItem(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult List()
        {
            return PartialView(Repo.GetAddressBooks());
        }
    }
}