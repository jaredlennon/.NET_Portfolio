﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystem.Models.Responses
{
    public class UpdateOrderResponse : Response
    {
        public List<Order> Orders { get; set; }
    }
}
