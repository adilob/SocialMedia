﻿namespace SocialMedia.Core.Repositories;

public interface IRepository<T>
{
	Task<T> GetByIdAsync(Guid id);
	Task<IEnumerable<T>> GetAllAsync();
	Task<T> AddAsync(T entity);
	Task UpdateAsync(T entity);
	Task DeleteAsync(T entity);
}
