﻿using Microsoft.EntityFrameworkCore;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UnitOfWorkResponse;
using PoroDev.Database.Repositories.Contracts;
using System.Linq.Expressions;
using static PoroDev.Common.Extensions.CreateResponseExtension;
using static PoroDev.Database.Constants.Constants;

namespace PoroDev.Database.Repositories
{
    public class GenericRepository<TemplateEntity, TemplateDatabaseContext> :
        IGenericRepository<TemplateEntity> where TemplateEntity : class, new()
        where TemplateDatabaseContext : DbContext
    {
        private readonly TemplateDatabaseContext _context;

        public GenericRepository(TemplateDatabaseContext context)
        {
            _context = context;
        }

        public async Task<UnitOfWorkResponseModel<TemplateEntity>> CreateAsync(TemplateEntity entity)
        {
            TemplateEntity createdEntity;
            try
            {
                createdEntity = (await _context.Set<TemplateEntity>().AddAsync(entity)).Entity;
            }
            catch (Exception)
            {
                UnitOfWorkResponseModel<TemplateEntity> responseException = new()
                {
                    Entity = null,
                    ExceptionName = nameof(DatabaseException),
                    HumanReadableMessage = InternalDatabaseError
                };

                return responseException;
            }

            UnitOfWorkResponseModel<TemplateEntity> response = new()
            {
                Entity = createdEntity,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            return response;

        }

        public void Delete(TemplateEntity entity)
        {
            _context.Set<TemplateEntity>().Remove(entity);
        }

        public async Task<ICollection<TemplateEntity>> GetAllAsync()
        {
            return await _context.Set<TemplateEntity>().ToListAsync();
        }

        public async Task<TemplateEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Set<TemplateEntity>().FindAsync(id);
        }

        public async Task<UnitOfWorkResponseModel<TemplateEntity>> FindAsync(Expression<Func<TemplateEntity, bool>> filter)
        {
            TemplateEntity? entity;
            try
            {
                entity = await _context.Set<TemplateEntity>().FirstOrDefaultAsync(filter);
            }
            catch (Exception)
            {
                UnitOfWorkResponseModel<TemplateEntity> response = new UnitOfWorkResponseModel<TemplateEntity>();
                response.Entity = null;
                response.ExceptionName = nameof(DatabaseException);
                response.HumanReadableMessage = InternalDatabaseError;
                return response;
            }
            if (entity != null)
            {
                UnitOfWorkResponseModel<TemplateEntity> response = new UnitOfWorkResponseModel<TemplateEntity>();
                response.Entity = entity;
                response.ExceptionName = null;
                response.HumanReadableMessage = null;
                return response;
            }
            else
            {
                UnitOfWorkResponseModel<TemplateEntity> response = new UnitOfWorkResponseModel<TemplateEntity>();
                response.Entity = null;
                response.ExceptionName = nameof(UserNotFoundException);
                response.HumanReadableMessage = UserNotFoundExceptionMessage;
                return response;
            }
        }

        public async Task<ICollection<TemplateEntity>?> FindAllAsync(Expression<Func<TemplateEntity, bool>> filter)
        {
            return await _context.Set<TemplateEntity>().Where(filter).ToListAsync();
        }

        public async Task<TemplateEntity?> UpdateAsync(TemplateEntity entity, Guid id)
        {
            if (entity == null)
            {
                return null;
            }
            TemplateEntity? exist = await _context.Set<TemplateEntity>().FindAsync(id);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exist;
        }
    }
}