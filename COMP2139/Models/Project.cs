﻿using System;
using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Models
{
	public class Project

	{
		public int ProjectId { get; set; }


		//[Required]

		public /*required*/ string Name { get; set; }



		public string? Description { get; set; } // ? this means the variable can be nullable


        [DataType(DataType.Date)] 

        public DateTime StartDate { get; set; }


        [DataType(DataType.Date)] 

        public DateTime EndDate { get; set; }



		public string? Status { get; set; } // ? this means the variable can be nullable


	}
}
