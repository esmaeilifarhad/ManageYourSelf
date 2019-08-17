using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.Utility
{
    public class PersianDatePrevNext
    {

        public string NextDay(string OldDate)
        {
            string NewDate = string.Empty;
            NewDate = ConvertDateToSqlFormat(OldDate);
            string Day = NewDate.Substring(0, 2);
            string Mounth = NewDate.Substring(2, 2);
            string Year = NewDate.Substring(4, 2);


            string NewDay = string.Empty;
            string NewMounth = string.Empty;
            string NewYear = string.Empty;

            #region Kabise
            if (isKabise(NewDate))
            {
                if (int.Parse(Mounth) <= 6 && int.Parse(Day) == 31)
                {

                    NewDay = "01";
                    NewMounth = (int.Parse(Mounth) + 1).ToString();
                    NewDate = Year + NewMounth + NewDay;
                }
                else if (int.Parse(Mounth) > 6 && int.Parse(Mounth) < 12 && int.Parse(Day) == 30)
                {

                    NewDay = "01";
                    NewMounth = (int.Parse(Mounth) + 1).ToString();
                    NewDate = Year + NewMounth + NewDay;
                }
                else if (int.Parse(Mounth) == 12 && int.Parse(Day) == 30)
                {

                    NewDay = "01";
                    NewMounth = "01";
                    NewYear = (int.Parse(Year) + 1).ToString();
                    NewDate = NewYear + NewMounth + NewDay;
                }
                else
                {
                    NewDate = (int.Parse(NewDate) + 1).ToString();
                }
                return NewDate;
            }
            #endregion 
            else
            {
                if (int.Parse(Mounth) <= 6 && int.Parse(Day) == 31)
                {

                    NewDay = "01";
                    NewMounth = (int.Parse(Mounth) + 1).ToString();
                    NewDate = Year + NewMounth + NewDay;
                }
                else if (int.Parse(Mounth) > 6 && int.Parse(Mounth) < 12 && int.Parse(Day) == 30)
                {

                    NewDay = "01";
                    NewMounth = (int.Parse(Mounth) + 1).ToString();
                    NewDate = Year + NewMounth + NewDay;
                }
                else if (int.Parse(Mounth) == 12 && int.Parse(Day) == 29)
                {

                    NewDay = "01";
                    NewMounth = "01";
                    NewYear = (int.Parse(Year) + 1).ToString();
                    NewDate = NewYear + NewMounth + NewDay;
                }
                else
                {
                    NewDate = (int.Parse(NewDate) + 1).ToString();
                }
                return NewDate;
            }
        }
        private bool IsEndMounth(string Date)
        {
            bool res = false;
            Date = ConvertDateToSqlFormat(Date);
            int Day = int.Parse(Date.Substring(0, 2));
            int Mounth = int.Parse(Date.Substring(2, 2));
            int Year = int.Parse(Date.Substring(4, 2));

            if (isKabise(Date))
            {
                if (Day == 31)
                {
                    res = true;
                }
                if (Mounth > 6 && Day == 30)
                {
                    res = true;
                }
            }
            else
            {
                if (Day == 31)
                {
                    res = true;
                }
                if (Mounth > 6 && Mounth < 12 && Day == 30)
                {
                    res = true;
                }
                if (Mounth == 12 && Day == 29)
                {
                    res = true;
                }
            }
            return res;
        }
        private string ConvertDateToSqlFormat(string DateSlash)
        {
            string strNew = DateSlash;
            if (DateSlash.Length == 8)
            {
                strNew = DateSlash.Replace("/", string.Empty);
            }
            if (DateSlash.Length == 10)
            {
                strNew = DateSlash.Replace("/", string.Empty);
                string Year = strNew.Substring(2, 2);
                string Month = strNew.Substring(4, 2);
                string Day = strNew.Substring(6, 2);
                strNew = Year + Month + Day;
            }
            return strNew;
        }
        private bool isKabise(string Date)
        {
            bool result = false;
            string Mydate = ConvertDateToSqlFormat(Date);

            string Day = Mydate.Substring(0, 2);
            string Mounth = Mydate.Substring(2, 2);
            string Year = Mydate.Substring(4, 2);

            if (int.Parse(Year) % 4 == 3)
            {
                result = true;
            }

            return result;
        }
        public string PrevDayn(string Date, int n, string NextPrev)
        {
            Date = ConvertDateToSqlFormat(Date);

            int Year = int.Parse(Date.Substring(0, 2));
            int Mounth = int.Parse(Date.Substring(2, 2));
            int Day = int.Parse(Date.Substring(4, 2));

            //-----
            if (NextPrev == "Prev")

                for (int i = 0; i < n; i++)
                {
                    //----------2 3 4 5 6 7
                    if (Day == 1 && Mounth > 1 && Mounth < 8)
                    {
                        Day = 31;
                        Mounth = Mounth - 1;
                        i = i + 1;
                    }
                    else if (Day == 1 && Mounth > 7 && Mounth <= 12)
                    {
                        Day = 30;
                        Mounth = Mounth - 1;
                        i = i + 1;
                    }
                    else if (Day == 1 && Mounth == 1)
                    {
                        Year = Year - 1;
                        //-------سال کبیسه است
                        if (Year % 4 == 3)
                        {
                            Day = 30;
                            Mounth = 12;
                        }
                        else
                        {
                            Day = 29;
                            Mounth = 12;
                        }
                        i = i + 1;
                    }
                    else
                    {
                        Day = Day - 1;
                    }
                }
            string NewDay = Day.ToString(); ;
            string NewMounth = Mounth.ToString();
            if (Day <= 9)
                NewDay = "0" + Day.ToString();
            if (Mounth <= 9)
                NewMounth = "0" + Mounth.ToString();
            string NewDate = Year.ToString() + NewMounth + NewDay;
            return NewDate;
        }
    }
}