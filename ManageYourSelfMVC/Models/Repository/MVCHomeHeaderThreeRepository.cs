using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.Models.Repository
{
    //public class MVCHomeHeaderThreeRepository
    //{
    //}
    public class MVCHomeHeaderThreeRepository : IDisposable
    {
        private Models.DomainModels.ManageYourSelfEntities DB = null;
        public MVCHomeHeaderThreeRepository()
        {
            DB = new Models.DomainModels.ManageYourSelfEntities();
        }
        public bool Add(Models.DomainModels.MVCHomeHeaderThree entity, bool autoSave = true)
        {
            try
            {
                DB.MVCHomeHeaderThrees.Add(entity);
                if (autoSave)
                    return Convert.ToBoolean(DB.SaveChanges());
                else return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("خطا در ثبت " + ex.Message);

            }
        }
        public bool Update(Models.DomainModels.MVCHomeHeaderThree entity, bool autoSave = true)
        {
            try
            {
                DB.MVCHomeHeaderThrees.Attach(entity);
                DB.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                // DB.MVCHomeHeaderThrees.Add(entity);
                if (autoSave)
                    return Convert.ToBoolean(DB.SaveChanges());
                else return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("خطا در ویرایش " + ex.Message);

            }
        }
        public IQueryable<Models.DomainModels.MVCHomeHeaderThree> Select()
        {
            try
            {
                return DB.MVCHomeHeaderThrees.AsQueryable();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Models.DomainModels.MVCHomeHeaderThree, TResult>> Selector)
        {
            try
            {
                // DB.MVCHomeHeaderThrees.Select(q => new { name = q.MVCHomeHeaderThreeName });
                return DB.MVCHomeHeaderThrees.Select(Selector);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Models.DomainModels.MVCHomeHeaderThree Find(int id)
        {
            try
            {
                var Res = DB.MVCHomeHeaderThrees.SingleOrDefault(q => q.MVCHomeHeaderThreeId == id);
                return Res;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }
        public bool Delete(int id, bool autoSave = true)
        {
            try
            {
                var entity = DB.MVCHomeHeaderThrees.Find(id);
                DB.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                {
                    return Convert.ToBoolean(DB.SaveChanges());
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }
        public bool Delete(Models.DomainModels.MVCHomeHeaderThree entity, bool autoSave = true)
        {
            try
            {
                DB.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                {
                    return Convert.ToBoolean(DB.SaveChanges());
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<Models.DomainModels.MVCHomeHeaderThree> where(System.Linq.Expressions.Expression<Func<Models.DomainModels.MVCHomeHeaderThree, bool>> predicate)
        {
            try
            {
                return DB.MVCHomeHeaderThrees.Where(predicate);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int GetLastIdentity()
        {
            try
            {
                if (DB.MVCHomeHeaderThrees.Any())
                    return DB.MVCHomeHeaderThrees.OrderByDescending(q => q.MVCHomeHeaderThreeId).First().MVCHomeHeaderThreeId;
                else
                    return 0;
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.DB != null)
                {
                    this.DB.Dispose();
                    this.DB = null;
                }
            }
            //Dispose(true);
            //GC.SuppressFinalize(this);
        }
        ~MVCHomeHeaderThreeRepository()
        {
            Dispose(false);
        }
    }
}