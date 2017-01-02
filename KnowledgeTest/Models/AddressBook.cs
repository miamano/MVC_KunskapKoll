using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowledgeTest.Models
{
    public class AddressBook
    {
        public Guid AddressBookId { get; set; }
        public string Name { get; set; }
        public string Tnr { get; set; }
        public string Address { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}