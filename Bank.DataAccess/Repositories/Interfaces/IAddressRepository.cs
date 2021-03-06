﻿using Bank.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DataAccess.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        IEnumerable getAddressList();
        Address getAddressById(int id);
        void addNewAddress(Address address);
    }
}
