﻿namespace Application.DTOs
{
	public class UserDTO
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string PasswordHash { get; set; }

	}
}