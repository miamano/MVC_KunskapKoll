using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestMedAjax.Models;

namespace TestMedAjax.Repository
{
    public class AddressBookRepository
    {
        public static AddressBookRepository repo = null;
        private List<AddressBook> addressBookList;

        public AddressBookRepository()
        {
            addressBookList = new List<AddressBook>();
        }

        public static AddressBookRepository getRepo()
        {
            if (repo == null)
            {
                repo = new AddressBookRepository();
                repo.GetAllBookMockData();
            }
            return repo;
        }

        private void GetAllBookMockData()
        {
            addressBookList = new List<AddressBook>() {
                new AddressBook() { AddressBookId = Guid.NewGuid(), Name = "Krisztina Barta", Address = "Mellanvångsvägen 2C Lund 22358", Tnr = "0725107873", LastUpdate = DateTime.Now },
                new AddressBook() { AddressBookId = Guid.NewGuid(), Name = "Magnus Andersson", Address = "Mellanvångsvägen 2C Lund 22358", Tnr = "0712345678", LastUpdate = DateTime.Now }
            };
        }

        public List<AddressBook> GetAddressBooks()
        {
            return addressBookList;
        }

        public AddressBook GetItemByID(Guid id)
        {
            var item = addressBookList.Where(a => a.AddressBookId == id).FirstOrDefault();
            return item;
        }

        public void AddNewItem(AddressBook ab)
        {
            addressBookList.Add(ab);
        }

        public void EditItem(AddressBook ab)
        {
            var item = addressBookList.Where(a => a.AddressBookId == ab.AddressBookId).FirstOrDefault();
            item.Name = ab.Name;
            item.Tnr = ab.Tnr;
            item.LastUpdate = ab.LastUpdate;
            item.Address = ab.Address;

        }

        public void DeleteItem(Guid id)
        {
            var item = addressBookList.Where(a => a.AddressBookId == id).FirstOrDefault();
            if (item != null)
            {
                addressBookList.Remove(item);
            }
        }
    }
}