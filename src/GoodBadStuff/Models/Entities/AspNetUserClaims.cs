﻿using System;
using System.Collections.Generic;

namespace GoodBadStuff.Models.Entities
{
    public partial class AspNetUserClaims
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
