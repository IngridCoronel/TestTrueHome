using ApiTestTrueHome.Data;
using ApiTestTrueHome.Data.IData;
using ApiTestTrueHome.Models;
using ApiTestTrueHome.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Repository
{
    public class PropertyRepository : Repository<Property>,IPropertyRepository
    {
        public PropertyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        //public bool DeleteProperty(Property property)
        //{
        //    _db.Property.Remove(property);
        //    return SaveProperty();
        //}
        //private readonly ApplicationDbContext _db;
        //public PropertyRepository(ApplicationDbContext db)
        //{
        //    _db = db;
        //    _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //}
        //public bool CreateProperty(Property property)
        //{
        //    _db.Property.Add(property);
        //    return SaveProperty();
        //}

        //public bool DeleteProperty(Property property)
        //{
        //    _db.Property.Remove(property);
        //    return SaveProperty();
        //}

        //public bool ExistProperty(string title)
        //{
        //    bool value = _db.Property.Any(c => c.Title.ToLower().Trim() == title.ToLower().Trim());
        //    return value;
        //}

        //public bool ExistProperty(int propertyId)
        //{
        //    return _db.Property.Any(c => c.Id == propertyId);
        //}

        //public ICollection<Property> GetProperties()
        //{
        //    return _db.Property.OrderBy(c => c.Title).ToList();
        //}

        //public Property GetProperty(int propertyId)
        //{
        //    return _db.Property.FirstOrDefault(c => c.Id == propertyId);
        //}

        //public bool SaveProperty()
        //{

        //    return _db.SaveChanges() >= 0 ? true : false;
        //}

        //public bool UpdateProperty(Property property)
        //{
        //    _db.Property.Update(property);
        //    return SaveProperty();
        //}
    }
}
