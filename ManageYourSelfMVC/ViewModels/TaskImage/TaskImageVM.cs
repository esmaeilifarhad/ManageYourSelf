using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels.TaskImage
{
    public class TaskImageVM
    {
      
        public int ImageId { get; set; }
        public int TaskId { get; set; }
        public string ImageName { get; set; }
        public HttpPostedFileWrapper ImageFile { get; set; }
        public byte[] imgByte { get; set; }

    }
}