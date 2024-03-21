using Scripts.Data.InputOutput;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data.Player
{
    //==========================
    //=====JSON
    //==========================

    [Serializable]
    public class Contact
    {
        public int id;
        public string first_name;
        public string last_name;
        public string email;
        public string gender;
        public string ip_address;
        public bool favourite;
    }

    public class ContactsData : PlayerData<Contact>
    {
        //==========================
        //=====CONSTRUCTOR
        //==========================

        public ContactsData(string file, string pregen) : base(file, pregen) { }
    }
}
