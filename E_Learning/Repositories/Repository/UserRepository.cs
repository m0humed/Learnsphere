namespace E_Learning.Repositories.Repository
{
	using E_Learning.Models;
	using E_Learning.Repositories.IReposatories;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class UserRepository : IUserRepository
	{
		private readonly UserManager<User> _userManager;

		public UserRepository(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<User> GetByIdAsync(string userId)
		{
			return await _userManager.FindByIdAsync(userId);
		}

		public async Task<User> GetByEmailAsync(string email)
		{
			return await _userManager.FindByEmailAsync(email);
		}

		public async Task<IEnumerable<User>> GetAllAsync()
		{
			// You might want to retrieve users from the database context directly for performance,
			// but the UserManager can also be used depending on your requirements.
			var users = await _userManager.Users.ToListAsync();
			return users;
		}

		public async Task AddAsync(User user)
		{
			await _userManager.CreateAsync(user);
		}

		public async Task DeleteAsync(string userId)
		{
			var user = await GetByIdAsync(userId);
			if (user != null)
			{
				await _userManager.DeleteAsync(user);
			}
		}

		public async Task UpdateAsync(User newUser, User oldUser)
		{
			// Check if the user exists
			if (oldUser == null) throw new ArgumentNullException(nameof(oldUser));

			// Update the properties
			oldUser.FName = newUser.FName;
			oldUser.LName = newUser.LName;
			oldUser.Email = newUser.Email;
			oldUser.Image = newUser.Image;
			oldUser.DateJoined = newUser.DateJoined; // Optional; update only if needed
			oldUser.LastLogin = newUser.LastLogin; // Optional; update only if needed
			oldUser.Enrollment = newUser.Enrollment; // Optional; update only if needed
			oldUser.DataForInstructor = newUser.DataForInstructor; // Optional; update only if needed

			// Update the user in the database
			await _userManager.UpdateAsync(oldUser);
		}
	}

}
