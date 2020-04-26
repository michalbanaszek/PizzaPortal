﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaPortal.Model.Models
{
	public class EmailMessage
	{
		public EmailMessage()
		{
			ToAddresses = new List<EmailAddress>();
			FromAddresses = new List<EmailAddress>();
		}

		public List<EmailAddress> ToAddresses { get; set; }
		public List<EmailAddress> FromAddresses { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
	}

	public class EmailAddress
	{
		public string Name { get; set; }
		public string Address { get; set; }
	}
}
