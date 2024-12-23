﻿using E_Learning.Models;
using E_Learning.Repository.IReposatories;

namespace E_Learning.Repositories.IReposatories
{
	public interface ISocialMediaRepository : IRepository<SocialMedia>
	{
		Task<SocialMedia?> GetByNameAsync(string name);
	}

}
