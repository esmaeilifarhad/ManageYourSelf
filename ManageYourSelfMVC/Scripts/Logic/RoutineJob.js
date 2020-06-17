//--Execute All List when click Tab
$("ul li a[href='#RoutineJob']").on("click", function () {
    Refresh();
    //RoutineJobCreate();
});
//***************************************************RoutineJob
function List() {
    $.LoadingOverlay("show");
    var urll = "/RoutineJob/List";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListRoutineJob").html(data);
            $.LoadingOverlay("hide");
        },
        error: function (error) {
            console.log(error);
            $.LoadingOverlay("hide");
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
            console.log(error);
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
            console.log("RoutineJobListMasterPage : ");
            console.log( error);
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
            console.log(error);
        }
    })
}
function CreateRoutineJobPost(RoutineJobId) {
    $(".modal-footer table").remove();
    $.LoadingOverlay("show");
    var Job = $("#MasterModal input[name='Job']").val();
    var Order = $("#MasterModal input[name='Order']").val();
    var Rate = $("#MasterModal input[name='Rate']").val();
    var RoozDaily = "1,2,3,4,5,6,7";
  
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/RoutineJob/Create",
           data: JSON.stringify({ Job: Job, RoozDaily: RoozDaily, Order: Order, Rate: Rate, RoutineJobId: RoutineJobId }),
           success: function (result) {
               
               if (result == true) {
                   Refresh();
                   closeModal()
               }
               else {
                   alert("خطا در ثبت");
               }
               $.LoadingOverlay("hide");
           },
           error: function (error) {
               console.log(error);
               $.LoadingOverlay("hide");
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
                var tablebutt = "<table class='table' style='font-size: 9px;'>"
                tablebutt += "<tr>" +
                    "<td><input type='button' style='background-color:green' value='ذخیره' onclick='CreateRoutineJobPost()'/> | " +
                    "<input type='button' value='بستن' onclick='closeModal()'/></td>" +
                    "</tr>"
                tablebutt += "</table>"
                $(".modal-footer").empty();
                $(".modal-footer").append(tablebutt);

               $(".BodyModal").html(result);
               $("#MasterModal").modal();
           },
           error: function (error) {
               console.log(error);
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
               console.log(error);
           }
       });
}
function EditRoutineJob(RoutineJobId) {
    $.LoadingOverlay("show");
        $.ajax(
           {
               type: 'POST',
               contentType: "application/json;charset=utf-8",
               dataType: "json",
               url: "/RoutineJob/EditRoutineJob",
               data: JSON.stringify({ RoutineJobId: RoutineJobId }),
               success: function (result) {

                   var table = "<table class='table' style='font-size: 9px;'>"
                   table += "<tr>" +
                       "<td>عنوان</td><td><input name='Job' type='text' value='" + result.Job + "'/></td>" +
                       "</tr><tr>"+
                         "<td>Rate</td><td><input name='Rate' type='number' value='" + result.Rate + "'/></td>" +
                         "</tr><tr>"+
                          "<td>Order</td><td><input name='Order' type='number' value='" + result.Order + "'/></td>" +
                       "</tr>"
                   table += "</table>"

                   var tablebutt = "<table class='table' style='font-size: 9px;'>"
                   tablebutt += "<tr>" +
                       "<td><input type='button' style='background-color:green' value='ذخیره' onclick='CreateRoutineJobPost(" + RoutineJobId + ")'/> | " +
                        "<input type='button' value='بستن' onclick='closeModal()'/></td>" +
                       "</tr>"
                   tablebutt += "</table>"

                 //  $(".ListTaskAnjamShode table").remove()
                   // $(".ListTaskAnjamShode span").remove()  modal-footer
                   $(".BodyModal").empty();
                   $(".modal-footer").empty();
                   $(".BodyModal").append(table);
                   $(".modal-footer").append(tablebutt);
                   $("#MasterModal").modal();
                   $.LoadingOverlay("hide");
               },
               error: function (error) {
                   
                  // $("html body").remove();
                   alert(error.responseText)
                   console.log(error.responseText)
                   $.LoadingOverlay("hide");
                  
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
               console.log(error);
           }
       });
}
function Refresh() {
    List();
    ShowPivot();
    RoutineJobCreate();
    RoutineJobListMasterPage();
    ListTaskAnjamShode();
   // RoutineJobCreate();
}
function closeModal() {
    $(".modal-footer table").remove();
    $('#MasterModal').modal('toggle');
}
//--------------------------Events
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
          console.log(error);
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
          console.log(error);
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
            console.log(error);
        }
    });
});


