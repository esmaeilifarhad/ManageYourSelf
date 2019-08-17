using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.Utility
{
    public class PersianDateMethod
    {
        public PersianDateMethod(int d = 1, int m = 1, int y = 1397)
        {
            Month = m;
            Day = d;
            Year = y;
        }
        public PersianDateMethod(PersianDateMethod x)
        {
            Month = x.Month;
            Day = x.Day;
            Year = x.Year;
        }
        private int _day;
        private int _month;
        private int _Year;
       // private static int[] MonthDays = { 0, 31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29 };
        private int MounthDays(int Mounth)
        {
            int RoozInMounth = 0;
            if (1 <= Mounth && Mounth <= 6)
                RoozInMounth = 31;
            if (7 <= Mounth && Mounth <= 11)
                RoozInMounth = 30;
            if (Year % 4 == 3 && Mounth==12)
            {
                RoozInMounth = 30;
            }
            if (Year % 4 != 3 && Mounth == 12)
            {
                RoozInMounth = 29;
            }
            else
            {
                throw new ArgumentException("ماه باید بین 1 تا 12 باشد");
            }
            return RoozInMounth;
        }
        public int Day
        {
            set
            {
                if (value >= 0 && value <= MounthDays(Month))
                    _day = value;
                else
                    throw new ArgumentException("روز مناسب نمی باشد");
            }
            get
            {
                return _day;
            }
        }
        public int Month
        {
            set
            {
                if (value >= 1 && value <= 12)
                    _month = value;
                else
                    throw new ArgumentException("خطا در ماه بین 1 تا 12");
            }
            get
            {
                return _month;
            }
        }
        public int Year
        {
            set
            {
                if (1000 < value && value < 9999)
                    _Year = value;
                else
                   throw new ArgumentException("سال باید 4 رقم باشد");
            }
            get
            {
                return _Year;
            }
        }
       
        public override string ToString()
        {
            return Year.ToString() + "/" + (Month < 10 ? "0" : "") + Month.ToString() + "/" + (Day < 10 ? "0" : "") + Day.ToString();
        }
        private bool IslastDayOfMonth()
        {
            //if (Day == MonthDays[Month])
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return Day == MounthDays(Month) ? true : false;
        }
        private void NextDay()
        {
            if (IslastDayOfMonth() && Month == 12)
            {
                Year++;
                Month = 1;
                Day = 1;
            }
            else if (IslastDayOfMonth())
            {

                Month++;
                Day = 1;
            }
            else
                Day++;
        }
        public PersianDateMethod NDayNext(int N)
        {
            for (int i = 0; i < N; i++)
            {
                NextDay();
            }
            return this;
        }
        // در این متد دیگر باشی جاری کاری ندارد
        public PersianDateMethod NDayNextOtherOBJ(int N)
        {
            PersianDateMethod M = new PersianDateMethod(this);
            for (int i = 0; i < N; i++)
            {
                M.NextDay();
            }
            return M;
        }
        public int NDayBetweenTwoDate(PersianDateMethod Date1,PersianDateMethod Date2)
        {
            int Days = 0;
            //while (Date1<=Date2)
            //{

            //}
            return Days;
        }
    }
}