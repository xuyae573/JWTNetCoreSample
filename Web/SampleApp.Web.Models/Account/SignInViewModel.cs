using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleCommerce.Web.Models
{
    public class SignInViewModel
    {
        [Required]
        [Display(Name = "UserId")]
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
