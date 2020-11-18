using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BulkEmailMarketing.Models
{
	public class user_Model
	{
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int isActive { get; set; }


    }


	public class Login_Model
	{
		[Key]
		public int id { get; set; }

		[Required]
		[Display(Name = "UserName")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }


		public string __RequestVerificationToken { get; set; }
		//public string returnUrl { get; set; }
		//public string controller { get; set; }
	}
}