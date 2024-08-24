﻿using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.Services
{
    public class PartnerDto
    {
        public string Name { get; set; }

        public int NumberIssuedPromoCodes { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<PartnerPromoCodeLimit> PartnerLimits { get; set; }
    }
}
