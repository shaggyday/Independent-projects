using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using blood_bankblood_bank_app;
using System.Windows.Forms;

namespace blood_bank_app
{
    class UserInterface
    {
        public String[] DonerData { get; }

        public UserInterface(String firstName, String lastName, String bloodType, String RhFactor, String address, String phone)
        {
            DonerData = new string[6]{ lastName, firstName, bloodType, RhFactor, address, phone };
            database.storeData(DonerData);
        }
    }

}
