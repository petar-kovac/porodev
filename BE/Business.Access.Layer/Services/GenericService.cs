using Business.Access.Layer.Models;
using Business.Access.Layer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Access.Layer.Services
{
    public class GenericService<TemplateEntity> : IGenericService<TemplateEntity> where TemplateEntity : class, IBusinessEntityModel
    {
        public Task<Guid?> Create(TemplateEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TemplateEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TemplateEntity> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TemplateEntity> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<TemplateEntity> Update(Guid id, TemplateEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
