using Business.Access.Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Access.Layer.Services.Contracts
{
    public interface IGenericService<TemplateEntity> where TemplateEntity : class, IBusinessEntityModel
    {
        Task<Guid?> Create(TemplateEntity model);

        Task<TemplateEntity> GetById(Guid id);

        Task<IEnumerable<TemplateEntity>> GetAll();

        Task<TemplateEntity> GetByName(string name);

        Task<TemplateEntity> Update(Guid id, TemplateEntity model);

        Task<bool> Delete(Guid id);
    }
}
