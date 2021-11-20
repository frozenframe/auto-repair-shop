﻿using System;

namespace AutoRepairShop
{
	public class Client
	{
		public Int32 Id { get; }
		public String Lastname { get; set; }
		public String Name { get; set; }
		public String Surname { get; set; }
		public String Phone { get; set; }
		public String Comment { get; set; }

		public Client(Int32 id, String lastname, String name, String surname, String phone, String comment)
		{
			this.Id = id;
			this.Lastname = lastname;
			this.Name = name;
			this.Surname = surname;
			this.Phone = phone;
			this.Comment = comment;
		}

		public Client(String lastname, String name, String surname, String phone, String comment) :
			this(null, lastname, name, surname, phone, comment)
		{
		}
	}
}