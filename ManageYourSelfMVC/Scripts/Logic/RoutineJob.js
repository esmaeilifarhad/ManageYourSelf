//--Execute All List when click Tab
$("ul li a[href='#RoutineJob']").on("click", function () {
    Refresh();
    //RoutineJobCreate();
});
//***************************************************RoutineJob
function List() {
    var urll = "/RoutineJob/List";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListRoutineJob").html(data);
        },
        error: function (error) {
            alert(error);
        }
    })
}
function RoutineJobCreate() {
    var sss = $("#bd-root-PersianDatePicker input[type=text]").val();
    var MyData = sss
    var urll = "/RoutineJob/RoutineJobCreate";
    $.ajax({
        data: JSON.stringify({ MyData: MyData }),
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $("#RoutineJobCreate").html(data);
        },
        error: function (error) {
            alert(error);
        }
    })
}
function RoutineJobListMasterPage()
{
    var urll = "/RoutineJob/RoutineJobListMasterPage";
    $.ajax({
       // data: JSON.stringify({ MyData: MyData }),
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {

            $(".RoutineJobListMasterPage").html(data);
        },
        error: function (error) {
            alert(error);
        }
    })
}
function ShowPivot() {
    var urll = "/RoutineJob/ShowPivot";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListRoutineJobHa").html(data);
        },
        error: function (error) {
            alert(error);
        }
    })
}
function CreateRoutineJobPost() {
   
    var Job = $("#MasterModal input[name='Job']").val();
    var RoozDaily = "1,2,3,4,5,6,7";
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/RoutineJob/Create",
           data: JSON.stringify({ Job: Job,RoozDaily:RoozDaily }),
           success: function (result) {
               if (result == true) {
                   Refresh();
               }
               else {
                   alert("خطا در ثبت");
               }
           },
           error: function (error) {
               alert(error);
           }
       });
}
function CreateRoutineJobGet() {
 
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/RoutineJob/Create",
           success: function (result) {
               $(".BodyModal").html(result);
               $("#MasterModal").modal();
           },
           error: function (error) {
               alert(error);
           }
       }
       );
}
function EditHolyDay(HolyDayId) {
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/HolyDay/EditHolyDay?HolyDayId=" + HolyDayId,
           success: function (result) {
               $(".BodyModal").html(result);
               $("#MasterModal").modal();
           },
           error: function (error) {
               alert(error);
           }
       });
}
function UpdateHolyDay() {
 
    var MojoodyName = $("#MasterModal input[name='MojoodyName']").val();
    var HolyDayId = $("#MasterModal div[name='EditHolyDay'] table").attr("HolyDayId");
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/HolyDay/UpdateHolyDay",
           data: JSON.stringify({ MojoodyName: MojoodyName, HolyDayId: HolyDayId }),
           success: function (result) {
                   RefreshListHolyDay();
           }
       });
}
function Delete(RoutineJobId) {
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/RoutineJob/Delete",

           data: JSON.stringify({ RoutineJobId: RoutineJobId }),
           success: function (result) {
               if (result == true) {
                   Refresh();
               }
               else {
                   alert("خطا در ثبت");
               }
           }
       });
}
function SaveRoutineJob() {
        var MyArray = [];
        $(".ListRoutineJob div table tbody tr").each(function () {
            var RoutineJobId = $(this).find(".fa-save").attr("RoutineJobId");
            var RoozDaily = '';
            $(".ListRoutineJob div table tbody tr td input[name=" + RoutineJobId + "]:checked").each(function () {
                RoozDaily += $(this).val() + ","
            });
            MyArray.push(RoutineJobId + "_" + RoozDaily);
        });
    $.ajax(
       {
           type: 'post',
           data: JSON.stringify({MyData: MyArray}),
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/RoutineJob/SaveRoutineJob",
           success: function (result) {
               if (result == true) {
                 //  alert("ذخیره شد");
                   Refresh();
               }
           },
           error: function (error) {
               alert(error);
           }
       });
}
function Refresh() {
    List();
    ShowPivot();
    RoutineJobCreate();
    RoutineJobListMasterPage();
   // RoutineJobCreate();
}
//--------------------------Events
//--Create Get
$(".ListRoutineJob").on("click", "input[name='CreateRoutineJob']", function () {
    CreateRoutineJobGet();
});
//--Delete
$(".ListRoutineJob").on("click", ".fa-remove", function () {
    var res = confirm("آیا حذف انجام شود؟");
    if (res == true) {
        var RoutineJobId = $(this).attr("RoutineJobId");
        Delete(RoutineJobId);
    }
});
//--Edit
$(".ListHolyDay").on("click", ".fa-edit", function () {
    var HolyDayId = $(this).attr("HolyDayId");
    EditHolyDay(HolyDayId);
});
//--Save
$("#RoutineJob .ListRoutineJob").on("click", ".fa-save", function () {
    SaveRoutineJob();
});
//--Save In RoutineJobHa
$("#RoutineJobCreate").on("click", "input[type=checkbox]", function () {
    var RoutineJobId = $(this).val();
    SaveCheckedRoutineJobHa(RoutineJobId, this)
   /*
    var s = $(this).val();
    var ss = $(this).is(":checked");
    var sss = $("#bd-root-PersianDatePicker input[type=text]").val();
    var MyData = s + "_" + ss + "_" + sss
    $.ajax(
  {
      type: 'post',
      data: JSON.stringify({ MyData: MyData }),
      contentType: "application/json;charset=utf-8",
      dataType: "json",
      url: "/RoutineJob/SaveRoutineJobHa",
      success: function (result) {
          if (result == true) {
              //  alert("ذخیره شد");
              Refresh();
              //RoutineJobCreate();
          }
      },
      error: function (error) {
          alert(error);
      }
  });
    */
});
function SaveCheckedRoutineJobHa(RoutineJobId,thiss)
{
   
   // var s =RoutineJobId;
   // var IsCheck = $(".RoutineJobListMasterPage input[type=checkbox]").is(":checked");
    var IsCheck = $(thiss).is(":checked")
    var Date = $("#bd-root-PersianDatePicker input[type=text]").val();
   // var MyData = s + "_" + ss + "_" + sss
    $.ajax(
  {
      type: 'post',
      data: JSON.stringify({ RoutineJobId: RoutineJobId, IsCheck: IsCheck, Date: Date }),
      contentType: "application/json;charset=utf-8",
      dataType: "json",
      url: "/RoutineJob/SaveRoutineJobHa",
      success: function (result) {
          if (result == true) {
              //  alert("ذخیره شد");
              Refresh();
              //RoutineJobCreate();
          }
      },
      error: function (error) {
          alert(error);
      }
  });
}
$("#RoutineJobCreate").on("click", ".btn-danger", function () {
    var MyData = $("#bd-root-PersianDatePicker input[type=text]").val();
    var urll = "/RoutineJob/RoutineJobCreate?MyData="+ MyData;
    $.ajax({
        type: "Get",
       // data: JSON.stringify({ MyData: MyData }),
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $("#RoutineJobCreate").html(data);
        },
        error: function (error) {
            alert(error);
        }
    });
});

//jQuery('#menu4 #RoutineJobCreate #bd-root-PersianDatePicker').on('input', function () {
//    alert("1");
//});
////...which is nice and clean, but may be extended further to:

//jQuery("#RoutineJobCreate #bd-root-PersianDatePicker").on('input propertychange paste', function() {
//    alert("2");
//});
//$('#RoutineJobCreate').bind('input', function () {
//    alert();
//    /* This will be fired every time, when textbox's value changes. */
//});


