﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.Models
{
    public class RefreshTokenRequest
    {
        public required string Username { get; set; }
        public required string RefreshToken { get; set; }
    }
}
