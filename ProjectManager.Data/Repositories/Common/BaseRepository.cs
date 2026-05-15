using Microsoft.EntityFrameworkCore;
using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Interfaces;
using ProjectManager.Core.Models.Interfaces.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ProjectManager.Data.Repositories.Common
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public T? Get(int id)
        {
            return _dbSet.FirstOrDefault(e => e.Id == id);
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public Result<T> Update(T entity)
        {
            try
            {
                _context.Update(entity);
                _context.SaveChanges();
                return Result<T>.Ok(entity!);
            }
            catch (DbException ex)
            {
                return Result<T>.Fail($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Result<T>.Fail($"An error occurred: {ex.Message}");
            }
            
        }
        public Result<bool> Delete(int id)
        {
            try
            {
                var existingEntity = _dbSet.FirstOrDefault(e => e.Id == id);
                if (existingEntity != null || existingEntity != default)
                {
                    _dbSet.Remove(existingEntity);
                    _context.SaveChanges();
                    return Result<bool>.Ok(true);

                }
                else
                {
                    return Result<bool>.Fail($"Entity with id {id} not found.");
                }
            }
            catch (DbException ex)
            {
                return Result<bool>.Fail($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"An error occurred: {ex.Message}");
            }
        }
        public Result<T> Create(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
                return Result<T>.Ok(entity);
            }
            catch (DbException ex)
            {
                return Result<T>.Fail($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Result<T>.Fail($"An error occurred: {ex.Message}");
            }
        }
    }
}
