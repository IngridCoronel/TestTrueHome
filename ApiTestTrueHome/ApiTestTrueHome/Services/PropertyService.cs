using ApiTestTrueHome.Data.IData;
using ApiTestTrueHome.Models;
using ApiTestTrueHome.Repository.IRepository;
using ApiTestTrueHome.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public PropertyService(IPropertyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public bool CreateProperty(Property property)
        {
            _repository.Insert(property);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteProperty(Property property)
        {

            _repository.Update(property);
            return Convert.ToBoolean(_unitOfWork.Save());

        }

        public bool ExistProperty(string title)
        {
            bool value = _repository.Get(c => c.Title.ToLower().Trim() == title.ToLower().Trim()).Result.Any();
            return value;
        }

        public bool ExistProperty(int propertyId)
        {
            return _repository.GetById(propertyId)!=null;
        }

        public async Task<ICollection<Property>> GetProperties()
        {
            return (await _repository.Get()).OrderBy(c => c.Title).ToList();
        }

        public Property GetProperty(int propertyId)
        {
            return _repository.Get(c => c.Id == propertyId).Result.FirstOrDefault();
        }

        public async Task<int> UpdateProperty(Property property)
        {
            await _repository.Update(property);
            return await _unitOfWork.SaveAsync();
        }
    }

}