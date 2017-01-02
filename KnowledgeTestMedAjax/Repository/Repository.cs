using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KnowledgeTestMedAjax.Models;

namespace KnowledgeTestMedAjax.Repository
{
    public class AddressBookRepository
    {
        public static List<AddressBook> AddressBookList { get; private set; } = new List<AddressBook>();

        public AddressBookRepository()
        {
            //addressBookList = new List<AddressBook>();
            InitiateRepos();
        }
            
        //TODO - kolla om det behövs som public??
        private void InitiateRepos()
        {
            if (!AddressBookList.Any())
            {
                GetAllBookMockData();
            }
        }

        private void GetAllBookMockData()
        {
            AddressBookList = new List<AddressBook>() {
                new AddressBook() { AddressBookId = Guid.NewGuid(), Name = "Krisztina Barta", Address = "Mellanvångsvägen 2C Lund 22358", Tnr = "0725107873", LastUpdate = DateTime.Now },
                new AddressBook() { AddressBookId = Guid.NewGuid(), Name = "Magnus Andersson", Address = "Mellanvångsvägen 2C Lund 22358", Tnr = "0712345678", LastUpdate = DateTime.Now }
            };
        }

        public List<AddressBook> GetAddressBooks()
        {
            return (from a in AddressBookList select a).ToList();
        }

        public AddressBook GetItemByID(Guid id)
        {
            var item = AddressBookList.Where(a => a.AddressBookId == id).FirstOrDefault();
            return item;
        }

        public void AddNewItem(AddressBook ab)
        {
            AddressBookList.Add(ab);
        }

        public void EditItem(AddressBook ab)
        {
            var item = AddressBookList.Where(a => a.AddressBookId == ab.AddressBookId).FirstOrDefault();
            item.Name = ab.Name;
            item.Tnr = ab.Tnr;
            item.LastUpdate = ab.LastUpdate;
            item.Address = item.Address;

        }

        public void DeleteItem(Guid id)
        {
            var item = AddressBookList.Where(a => a.AddressBookId == id).FirstOrDefault();
            if (item != null)
            {
                AddressBookList.Remove(item);
            }
        }
    }
}