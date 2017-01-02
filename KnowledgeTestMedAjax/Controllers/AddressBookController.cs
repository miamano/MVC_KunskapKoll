using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnowledgeTestMedAjax.Repository;
using KnowledgeTestMedAjax.Models;

namespace KnowledgeTest.Controllers
{
    public class AddressBookController : Controller
    {

        public static AddressBookRepository Repo = new AddressBookRepository();

        [HttpGet]
        public ActionResult Index()
        {
            return View(Repo.GetAddressBooks());
        }

        /*
        [HttpGet]
        public ActionResult List(List<AddressBook> abList)
        {
            return PartialView();
        }
        */

        [HttpGet]
        public ActionResult CreateNewItem()
        {
            return PartialView(new AddressBook());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewItem(AddressBook ab)
        {
            if (string.IsNullOrWhiteSpace(ab.Name) || string.IsNullOrWhiteSpace(ab.Tnr))
            {
                ModelState.AddModelError("error", "Error: Name or telephone number is empty!");
                //return View(ab);
                return PartialView("Shared/Error", ab);
            }

            AddressBook postAB = new AddressBook() { AddressBookId = Guid.NewGuid(), Name = ab.Name, Address = ab.Address, Tnr = ab.Tnr, LastUpdate = DateTime.Now };
            Repo.AddNewItem(postAB);

            //return RedirectToAction("List");
            return PartialView("List", Repo.GetAddressBooks());
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            //return View(Repo.GetItemByID(id));
            return PartialView(Repo.GetItemByID(id));
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
            //return RedirectToAction("Index");
            return PartialView("List", Repo.GetAddressBooks());
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            //return View(Repo.GetItemByID(id));
            return PartialView(Repo.GetItemByID(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(AddressBook ab)
        {
            if (ModelState.IsValid)
            {
                Repo.EditItem(ab);
            }
            //return RedirectToAction("Index");
            return PartialView("List", Repo.GetAddressBooks());
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            Repo.DeleteItem(id);
            return PartialView("List", Repo.GetAddressBooks());
        }
    }
}