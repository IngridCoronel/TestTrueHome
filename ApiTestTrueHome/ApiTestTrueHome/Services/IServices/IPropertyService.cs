using ApiTestTrueHome.Data.IData;
using ApiTestTrueHome.Models;
using ApiTestTrueHome.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Services.IServices
{
    public interface IPropertyService
    {
        Task<ICollection<Property>> GetProperties();
        Property GetProperty(int propertyId);
        bool ExistProperty(string title);
        bool ExistProperty(int propertyId);
        bool CreateProperty(Property property);
        Task<int> UpdateProperty(Property property);
        bool DeleteProperty(Property property);

    }
}
