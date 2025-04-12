﻿using System.ComponentModel.DataAnnotations;

namespace Light_Shop.API.DTOs.Requests
{
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }

    }
}
