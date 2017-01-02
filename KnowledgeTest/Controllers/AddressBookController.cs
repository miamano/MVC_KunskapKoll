using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnowledgeTest.Repository;
using KnowledgeTest.Models;

namespace KnowledgeTest.Controllers
{
    public class AddressBookController : Controller
    {

        public static AddressBookRepository Repo = new AddressBookRepository();
        private static bool initiated = false;

        [HttpGet]
        public ActionResult Index()
        {
            if (!initiated)
            {
                Repo.InitiateRepos();
                initiated = true;
            }
            return View(Repo.GetAddressBooks());
        }

        [HttpGet]
        public ActionResult CreateNewItem()
        {
            return View();
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

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            Repo.DeleteItem(id);
            return RedirectToAction("Index");
        }
    }
}