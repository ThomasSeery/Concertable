﻿using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class ListingApplicationPurchaseResponse : PurchaseResponse
    {
        public int ApplicationId { get; set; }
        public EventDto Event { get; set; }
    }
}
