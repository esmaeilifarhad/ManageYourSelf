using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.Models.Repository
{
    public class KarkardsRepository
    {
        private Models.DomainModels.ManageYourSelfEntities DB = null;
        public KarkardsRepository()
        {
            DB = new Models.DomainModels.ManageYourSelfEntities();
        }
        public bool Add(Models.DomainModels.KarKard entity, bool autoSave = true)
        {
            try
            {
                DB.KarKards.Add(entity);
                if (autoSave)
                    return Convert.ToBoolean(DB.SaveChanges());
                else return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("خطا در ثبت " + ex.Message);

            }
        }
        public bool Update(Models.DomainModels.KarKard entity, bool autoSave = true)
        {
            try
            {
                DB.KarKards.Attach(entity);
                DB.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                if (autoSave)
                    return Convert.ToBoolean(DB.SaveChanges());
                else return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("خطا در ویرایش " + ex.Message);

            }
        }
        public IQueryable<Models.DomainModels.KarKard> Select()
        {
            try
            {
                return DB.KarKards.AsQueryable();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Models.DomainModels.KarKard, TResult>> Selector)
        {
            try
            {
                // DB.KarKards.Select(q => new { name = q.KarKardName });
                return DB.KarKards.Select(Selector);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Models.DomainModels.KarKard Find(int id)
        {
            try
            {
                var Res = DB.KarKards.SingleOrDefault(q => q.KarkardId == id);
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
                var entity = DB.KarKards.Find(id);
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
        public bool Delete(Models.DomainModels.KarKard entity, bool autoSave = true)
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
        public IQueryable<Models.DomainModels.KarKard> where(System.Linq.Expressions.Expression<Func<Models.DomainModels.KarKard, bool>> predicate)
        {
            try
            {
                return DB.KarKards.Where(predicate);
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
                if (DB.KarKards.Any())
                    return DB.KarKards.OrderByDescending(q => q.KarkardId).First().KarkardId;
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
        ~KarkardsRepository()
        {
            Dispose(false);
        }
    }
}