﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class RentalsFormViewModel
    {
        public Customer Customer { get; set; }
        public Books Book { get; set; }

        public Rental Rental { get; set; }
    }
}