using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.InterFace
{
    public interface ICrud
    {
  
        bool Delete(int id);
        bool Create();
    }
}