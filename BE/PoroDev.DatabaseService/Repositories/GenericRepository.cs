using Microsoft.EntityFrameworkCore;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UnitOfWorkResponse;
using PoroDev.DatabaseService.Repositories.Contracts;
using System.Linq.Expressions;
using static PoroDev.DatabaseService.Constants.Constants;

namespace PoroDev.DatabaseService.Repositories
{
    public class GenericRepository<TemplateEntity, TemplateDatabaseContext> :
        IGenericRepository<TemplateEntity> where TemplateEntity : class, new()
        where TemplateDatabaseContext : DbContext
    {
        protected readonly TemplateDatabaseContext _context;

        public GenericRepository(TemplateDatabaseContext context)
        {
            _context = context;
        }

        public async Task<UnitOfWorkResponseModel<TemplateEntity>> CreateAsync(TemplateEntity entity)
        {
            TemplateEntity createdEntity;
            try
            {
                var entityDto = await _context.Set<TemplateEntity>().AddAsync(entity);
                createdEntity = entityDto.Entity;

                UnitOfWorkResponseModel<TemplateEntity> response = new()
                {
                    Entity = createdEntity,
                    ExceptionName = null,
                    HumanReadableMessage = null
                };

                return response;
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
        }

        public async Task<UnitOfWorkResponseModel<TemplateEntity>> Delete(TemplateEntity entity)
        {
            TemplateEntity? returnEntity = null;
            UnitOfWorkResponseModel<TemplateEntity> response = new UnitOfWorkResponseModel<TemplateEntity>();
            try
            {
                _context.Set<TemplateEntity>().Remove(entity);
                returnEntity = entity;
            }
            catch (Exception)
            {
                response.Entity = null;
                response.ExceptionName = nameof(DatabaseException);
                response.HumanReadableMessage = InternalDatabaseError;
                return response;
            }

            if (returnEntity != null)
            {
                response.Entity = returnEntity;
                response.ExceptionName = null;
                response.HumanReadableMessage = null;
            }
            else
            {
                response.Entity = null;
                response.ExceptionName = nameof(UserNotFoundException);
                response.HumanReadableMessage = UserNotFoundExceptionMessage;
            }
            return response;
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
            UnitOfWorkResponseModel<TemplateEntity> response = new UnitOfWorkResponseModel<TemplateEntity>();

            try
            {
                entity = await _context.Set<TemplateEntity>().FirstOrDefaultAsync(filter);
            }
            catch (Exception)
            {
                response.Entity = null;
                response.ExceptionName = nameof(DatabaseException);
                response.HumanReadableMessage = InternalDatabaseError;
                return response;
            }
            if (entity != null)
            {
                response.Entity = entity;
                response.ExceptionName = null;
                response.HumanReadableMessage = null;
            }
            else
            {
                response.Entity = null;
                response.ExceptionName = nameof(UserNotFoundException);
                response.HumanReadableMessage = UserNotFoundExceptionMessage;
            }
            return response;
        }

        public async Task<ICollection<TemplateEntity>?> FindAllAsync(Expression<Func<TemplateEntity, bool>> filter)
        {
            return await _context.Set<TemplateEntity>().Where(filter).ToListAsync();
        }

        public async Task<UnitOfWorkResponseModel<TemplateEntity>> UpdateAsync(TemplateEntity entity, Guid id)
        {
            TemplateEntity exist = await _context.Set<TemplateEntity>().FindAsync(id);
            UnitOfWorkResponseModel<TemplateEntity> response = new UnitOfWorkResponseModel<TemplateEntity>();

            try
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                UnitOfWorkResponseModel<TemplateEntity> responseDataBaseError = new UnitOfWorkResponseModel<TemplateEntity>();
                response.Entity = null;
                response.ExceptionName = nameof(DatabaseException);
                response.HumanReadableMessage = InternalDatabaseError;
                return response;
            }

            if (entity != null)
            {
                response.Entity = entity;
                response.ExceptionName = null;
                response.HumanReadableMessage = null;
                return response;
            }
            else
            {
                response.Entity = null;
                response.ExceptionName = nameof(UserNotFoundException);
                response.HumanReadableMessage = UserNotFoundExceptionMessage;
                return response;
            }
        }
    }
}