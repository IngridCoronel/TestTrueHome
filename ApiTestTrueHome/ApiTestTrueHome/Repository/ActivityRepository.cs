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
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        public ActivityRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        //private readonly ApplicationDbContext _db;
        //public ActivityRepository(ApplicationDbContext db)
        //{
        //    _db = db;
        //    _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //}
    //    public bool CreateActivity(Activity activity)
    //    {
    //        _db.Activity.Add(activity);
    //        return SaveActivity();
    //    }

    //    public bool DeleteActivity(Activity activity)
    //    {
    //        _db.Activity.Remove(activity);
    //        return SaveActivity();
    //    }

    //    public bool ExistActivity(int activityId)
    //    {
    //        return _db.Activity.Any(c => c.Id == activityId);
    //    }

    //    public ICollection<Activity> GetActivities(int propertyId)
    //    {
    //       return _db.Activity.Include(p => p.Property).Where(p => p.Property_Id == propertyId).ToList();
    //    }

    //    public Activity GetActivity(int activityId)
    //    {
    //        //return _db.Activity.Get();
    //        return _db.Activity.Include(p => p.Property,).Where(p => p.Property_Id == propertyId).ToList();

    //        //return _db.Activity.FirstOrDefault(c => c.Id == activityId);
    //    }

    //    public bool SaveActivity()
    //    {
    //        return _db.SaveChanges() >= 0 ? true : false;
    //    }

    //    public bool UpdateActivity(Activity activity)
    //    {
    //        _db.Activity.Update(activity);
    //        return SaveActivity();
    //    }
    }
}
