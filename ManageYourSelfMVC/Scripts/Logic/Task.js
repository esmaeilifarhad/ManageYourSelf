//************************************Task*****************************************************
//--Execute All List when click Tab
$("ul li a[href='#menuTiming']").on("click", function () {

    // var s = $('input[name="chkTomarrow"]:checked');
    //ListTiming(0);

    if ($("input[name='chkTomarrow']").prop('checked') == true) {
        ListTiming(1);
    }
    else {
        ListTiming(0);
    }

});
//--------------------
$("ul li a[href='#menuTaskGeneral']").on("click", function () {
    ListTaskGeneral();
});
$("ul li a[href='#menuTaskFuture']").on("click", function () {
    ListTaskFuture();
    ListTaskFutureChk();
});
function ListTaskFutureChkPost(MyArray) {
    $.ajax(
     {
         type: 'Post',
         data: JSON.stringify({ MyData: MyArray }),
         contentType: "application/json;charset=utf-8",
         dataType: "html",
         url: "/Task/ListTaskFutureChkPost",
         success: function (result) {
             
             if (result.result == false) {
                 alert(result.message)
             }
             else {
                 $(".ListTaskFuture").html(result);
             }

         },
         error: function (error) {
             $(".ListTaskFuture").html("<p>موردی برای مشاهده وجود ندارد</p>")
         }
     });
}
function ListTaskFuture() {
    var urll = "/Task/ListTaskFuture";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (result) {

            if (result.result == false) {
                alert(result.message)
            }
            else {
                $(".ListTaskFuture").html(result);
            }

        },
        error: function (error) {
            
            console.log(error);
        }
    })

}
function ListTaskFutureChk() {
    var urll = "/Task/ListTaskFutureChk";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (result) {

            if (result.result == false) {
                alert(result.message)
            }
            else {
                $(".ListTaskFutureChk").html(result);
            }

        },
        error: function (error) {
            
            console.log(error);
        }
    })

}

//----------------------

$("#menuTiming").on("click", "input[name='chkTomarrow']", function () {

    if ($("input[name='chkTomarrow']").prop('checked') == true) {
        ListTiming(1);
    }
    else {
        ListTiming(0);
    }

});
//-----Edit Task Event
$(".ListTask").on("click", ".TblTask .fa-edit", function () {
    var TaskId = $(this).attr("Data_id");
    // alert(TaskId)
    EditTask(TaskId);
});

$(".ListTiming").on("click", "div table tbody tr td  .Ti", function () {
    var TaskId = $(this).attr("Data_id");
    TimingTask(TaskId);
})
$("Body").on("click", ".calendarTask", function () {
    var TaskId = $(this).attr("Data_id");
    TimingTask(TaskId);
});

$("body").on("click", ".Ta", function () {
    var TaskId = $(this).attr("Data_id");
    // alert(TaskId)
    EditTask(TaskId);
})
//------ListTaskAnjamNashode
$("#menu4 input[name='task']").on("click", function () {
    //console.log($("input[type='radio'][name='task']:checked").val())
    var ValOfRadio = $("input[type='radio'][name='task']:checked").val();
    var typeTask;
    switch (ValOfRadio) {
        case "anjamshode":
            typeTask = "anjamshode";
            break;
        case "anjamnashode":
            typeTask = "anjamnashode";
            break;
        case "gheirefal":
            typeTask = "gheirefal";
            break;
        default:
            typeTask = "";
            break;
    }
    ListTask(typeTask)
});
//ShowCreateTask
$("Body").on("click", ".CreateNewTask", function () {
    var CatId = $(this).attr("CatId");
    CreateTaskView(CatId);
});
function CreateTaskView(CatId) {


    var urll = "/Task/CreateTaskView?CatId=" + CatId;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (result) {
            // var obj = data;
            //var obj = JSON.parse(data);
            $(".BodyModal").html(result);
            $("#MasterModal").modal();
        }
    })
}
//-----------------------------
//Delete Timing
$("Body").on("click", ".DeleteTiming", function () {
    DeleteTiming();
});
function DeleteTiming() {
    $.ajax(
          {
              type: 'POST',
              contentType: "application/json;charset=utf-8",
              dataType: "json",
              url: "/Task/DeleteTiming",
              // data: JSON.stringify({ DateEnd: DateEnd, CatId: CatId }),
              success: function (result) {
                  ListTaskGeneral();
                  if (result.result == false) {
                      alert(result.message)
                  }
              },
              error: function (error) {
                  // alert(result.message);
                  // alert(result.result)
                  console.log(error.responseText);
              }
          });
}
//Delete Timeing Task
$("Body").on("click", ".removeTimeTask", function () {
    var TaskId = $(this).attr("Data_id");
    removeTimeTask(TaskId);
});
//Level Up Down
$("Body").on("click", ".TaskUpLevel", function () {
    var TaskId = $(this).attr("Data_id");
    TaskUpLevel(TaskId);
});
function TaskUpLevel(TaskId) {
    
    $.LoadingOverlay("show");
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Task/TaskUpLevel",
           data: JSON.stringify({ TaskId: TaskId }),
           success: function (result) {
               if (result == true) {
                   //  $("#ShowMessage").text('ثبت شد');
                   ListTask("anjamnashode");
                   RefreshTask();
                   if ($("input[name='chkTomarrow']").prop('checked') == true) {
                       ListTiming(1);
                   }
                   else {
                       ListTiming(0);
                   }
               }
               else {
                   $("#ShowMessage").text('خطا در ثبت');
               }
               $.LoadingOverlay("hide");
           },
           error: function (error) {
               console.log(error);
           }
       });
}
function ListTaslLevelHigh() {
    var urll = "/Task/ListTaslLevelHigh";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (result) {
            $(".footer-contact").html(result);
            //--------
        },
        error: function (error) {
            console.log("******start******")
            console.log("ListTaslLevelHigh : ");
            console.log(error)
            console.log("******end******")

        }
    })
}
$("Body").on("click", ".TaskDownLevel", function () {
    var TaskId = $(this).attr("Data_id");
    TaskDownLevel(TaskId);
});
function TaskDownLevel(TaskId) {
    $.LoadingOverlay("show");
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Task/TaskDownLevel",
           data: JSON.stringify({ TaskId: TaskId }),
           success: function (result) {
               if (result == true) {
                   //  $("#ShowMessage").text('ثبت شد');
                   ListTask("anjamnashode");
                   RefreshTask();
                   if ($("input[name='chkTomarrow']").prop('checked') == true) {
                       ListTiming(1);
                   }
                   else {
                       ListTiming(0);
                   }
               }
               else {
                   $("#ShowMessage").text('خطا در ثبت');
               }
               $.LoadingOverlay("hide");
           },
           error: function (error) {
               console.log("******start******")
               console.log("TaskDownLevel : ");
               console.log(error)
               console.log("******end******")

           }
       });
}
function removeTimeTask(TaskId) {
    $.ajax(
          {

              type: 'POST',
              contentType: "application/json;charset=utf-8",
              dataType: "json",
              url: "/Task/removeTimeTask?TaskId=" + TaskId,
              // data: JSON.stringify({ DateEnd: DateEnd, CatId: CatId }),
              success: function (result) {

                  if ($("input[name='chkTomarrow']").prop('checked') == true) {
                      ListTiming(1);
                  }
                  else {
                      ListTiming(0);
                  }

                  if (result.result == false) {
                      alert(result.message)
                  }
              },
              error: function (error) {
                  console.log("******start******")
                  console.log("removeTimeTask : ");
                  console.log(error.responseText)
                  console.log("******end******")
              }
          });
}
function EditTask(TaskId) {
    $.LoadingOverlay("show");
    var urll = "/Task/EditTask?TaskId=" + TaskId;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (result) {
            // var obj = data;
            //var obj = JSON.parse(data);
            $(".BodyModal").html(result);
            $("#MasterModal").modal();
            $.LoadingOverlay("hide");
            //--------
        },
        error: function (error) {
            console.log(error);
            $.LoadingOverlay("hide");
        }
    })
}
$("Body").on("click", ".ChangeTodayTask", function () {
    var CatId = $(this).attr("CatId");
    ChangeTodayTask(CatId);
});
function ChangeTodayTask(CatId) {
    var urll = "/Task/ChangeTodayTask?CatId=" + CatId;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (result) {
            // var obj = data;
            //var obj = JSON.parse(data);
            $(".BodyModal").html(result);
            $("#MasterModal").modal();

            //--------
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function ChangeTodayTaskPost() {
    var DateEnd = $("#MasterModal table input[name='DateEnd']").val()
    var CatId = $("#MasterModal div[name='ChangeTodayTask'] table").attr("CatId");
    var chkIsTransfer = $("#MasterModal div[name='ChangeTodayTask'] table input[name='chkIsTransfer']").prop('checked');
    $.ajax(
          {

              type: 'POST',
              contentType: "application/json;charset=utf-8",
              dataType: "json",
              url: "/Task/ChangeTodayTask?CatId=" + CatId + "&&DateEnd=" + DateEnd + "&&chkIsTransfer=" + chkIsTransfer,
              // data: JSON.stringify({ DateEnd: DateEnd, CatId: CatId }),
              success: function (result) {
                  ListTaskGeneral();
                  RefreshTask();
                  if (result.result == false) {
                      alert("ChangeTodayTaskPost() : " + result.message)
                  }
              },
              error: function (error) {
                  // alert(result.message);
                  // alert(result.result)
                  alert("ChangeTodayTaskPost() : " + error.responseText);
              }
          });
}
function CreateTask() {
    // 
    var _CatId = $("#MasterModal table .MYSelect option:selected").val();
    var _Name = $("#MasterModal table textarea[name='Name']").val()
    //var _Name = $("#MasterModal table input[name='Name']").val()
    var _Olaviat = $("#MasterModal table input[name='Olaviat']").val()
    var _DateEnd = $("#MasterModal table input[name='DateEnd']").val()
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Task/CreateTask",

           data: JSON.stringify({ Name: _Name, DateEnd: _DateEnd, Olaviat: _Olaviat, CatId: _CatId }),
           success: function (result) {
               if (result == true) {
                   $("#ShowMessage").text('ثبت شد');
                   ListTask("anjamnashode");
                   RefreshTask();
               }
               else {
                   $("#ShowMessage").text('خطا در ثبت');
               }
           }
       });
}
function UpdateTask() {
    var CatId = $("#MasterModal table .MYSelect option:selected").val();
    var Name = $("#MasterModal table textarea[name='Name']").val()
    var Olaviat = $("#MasterModal table input[name='Olaviat']").val()
    var DarsadPishraft = $("#MasterModal table input[name='DarsadPishraft']").val()
    var DateEnd = $("#MasterModal table input[name='DateEnd']").val()
    var DateStart = $("#MasterModal table input[name='DateStart']").val()
    var IsActive = $("input[name='TaskIsActive']").prop('checked')
    var IsCheck = $("input[name='TaskIsCheck']").prop('checked')
    var TaskId = $("#MasterModal table").attr("TaskId")
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Task/UpdateTask",
           data: JSON.stringify({ TaskId: TaskId, DateStart: DateStart, DateEnd: DateEnd, IsActive: IsActive, IsCheck: IsCheck, DarsadPishraft: DarsadPishraft, Name: Name, Olaviat: Olaviat, CatId: CatId }),
           success: function (result) {
               if (result == true) {
                   $("#ShowMessage").text('ثبت شد');
                   ListTask("anjamnashode");
                   RefreshTask()

                   if ($("input[name='chkTomarrow']").prop('checked') == true) {
                       ListTiming(1);
                   }
                   else {
                       ListTiming(0);
                   }
               }
               else {
                   $("#ShowMessage").text('خطا در ثبت');
               }
           },
           error: function (error) {
               console.log(error);
           }
       });
}

function ListTask(typeTask) {
    $.LoadingOverlay("show");
    var s = typeTask
    var urll = "/Task/ListTaskAnjamnashode?typeTask=" + typeTask;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListTask").html(data);
            eachColorTask();
            $.LoadingOverlay("hide");
        },
        error: function (error) {
            $(".ListTask").html("<p>دسترسی ندارید</p>");
            // console.log(error);
        }
    })
}
function TimingTask(TaskId) {
    var urll = "/Task/TimingTask?TaskId=" + TaskId;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (result) {
            // var obj = data;
            //var obj = JSON.parse(data);
            $(".BodyModal").html(result);
            $("#MasterModal").modal();

            //--------
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function ListTiming(x) {

    if (x == 0) {
        var urll = "/Task/ListTiming?x=" + x;
        $.ajax({
            type: "Get",
            contentType: "application/json;charset=utf-8",
            dataType: "html",
            url: urll,
            success: function (result) {
                if (result.result == false) {
                    alert(result.message)
                    console.log(result.message)
                }
                else {
                    $(".ListTiming").html(result);
                    timeOff();
                    ListTaskGeneral();
                }
                //--------
            },
            error: function (error) {
                console.log("ListTiming : " + error.message);
            }
        })
    }
    if (x == 1) {
        var urll = "/Task/ListTiming?x=" + x;
        $.ajax({
            type: "Get",
            contentType: "application/json;charset=utf-8",
            dataType: "html",
            url: urll,
            success: function (result) {
                // var obj = data;
                //var obj = JSON.parse(data);
                $(".ListTiming").html(result);
                timeOff();

                //--------
            },
            error: function (error) {
                console.log(error);
            }
        })
    }
}
function ListTaskGeneral() {
    var urll = "/Task/ListTaskGeneral";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (result) {
            if (result.result == false) {
                alert(result.message)
            }
            else {
                $(".ListTaskGeneral").html(result);
            }

        },
        error: function (error) {
            console.log(error);
        }
    })

}
$("body").on("click", ".AllCatChecked", function () {
    $(".ListTaskFutureChk").each(function () {
        $("input").attr("checked", true);
    });
});
$(".ListTaskFutureChk").on("click", "input", function () {

    var MyArray = [];
    var lvl = '';
    $(".ListTaskFutureChk .Categories  input:checked").each(function () {
        //  console.log($(this).val())
        lvl += $(this).val() + ",";

    });
    MyArray.push(lvl);
    ListTaskFutureChkPost(MyArray);
});
function RefreshChk() {
    var MyArray = [];
    var lvl = '';
    $(".ListTaskFutureChk .Categories input:checked").each(function () {
        //  console.log($(this).val())
        lvl += $(this).val() + ",";

    });
    MyArray.push(lvl);
    ListTaskFutureChkPost(MyArray);
}
function TimingPost() {

    var ManageTimeId = $("#MasterModal .MYSelect option:selected").val();
    var TaskId = $("#MasterModal div[name='UpdateTiming'] table").attr("TaskId");
    
    $.ajax(
          {
              type: 'POST',
              contentType: "application/json;charset=utf-8",
              dataType: "json",
              url: "/Task/CreateTiming",
              data: JSON.stringify({ ManageTimeId: ManageTimeId, TaskId: TaskId }),
              success: function (result) {
                  
                  if (result == true) {
                      if ($("input[name='chkTomarrow']").prop('checked') == true) {
                          ListTiming(1);
                      }
                      else {
                          ListTiming(0);
                      }
                      ListTaskGeneral();
                      ListTask("anjamnashode");

                      timeOff();
                      RefreshTask();
                  }
                  else {
                      alert("خطا در ثبت");
                  }
              },
              error: function (error) {
                  console.log(error);
              }
          });
}
function eachColorTask() {
    $(".TblTask tr td:nth-child(2)").each(function () {
        //اگر مانده روز بیشتر از صفر بود
        var mandeRooz=$(this).parent().find("td").eq(10).text()
        if(mandeRooz==0)
        {

            var valuee = ($(this).text());
            if (valuee < 0) {
                $(this).parent().css({ "color": "gray" });
            }
            if (valuee == 0) {
                $(this).parent().css({ "color": "gray" });
            }
            if (valuee == 1) {
                $(this).parent().css({ "color": "red" });
            }
            if (valuee == 2) {
                $(this).parent().css({ "color": "orange" });
            }
            if (valuee == 3) {
                $(this).parent().css({ "color": "#5a49e0" });
            }
            if (valuee == 4) {
                $(this).parent().css({ "color": "#26d826" });
            }
            if (valuee == 5) {
                $(this).parent().css({ "color": "darkgreen" });
            }
        }
        else
        {
            if(mandeRooz%2==0)
                $(this).parent().css({ "color": "#b5b7b9" });
            else
                $(this).parent().css({ "color": "black" });
        }
        

    });
}
function timeOff() {
    $("#menuTiming .ListTiming  table tr td:nth-child(1)").each(function () {
        var valuee = ($(this).text());
        var d = new Date();
        var n = d.getHours();
        // var time = d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds();
        var TMinut = d.getMinutes();
        var TSec = d.getSeconds();
        //alert(TMinut);
        //alert(TSec);

        if (TMinut < 2 && valuee == n) {
            //alert("okk");
            // alert($(this).next().next().text());
        }

        if (valuee < n) {
            $(this).parent().css({ "color": "red" });
        }
        if (valuee == n) {
            $(this).parent().css({ "color": "green" });
        }
    });
}
async function changeToAnjamShode() {
    $.LoadingOverlay("show");
    var obj={}
    var count= $(".TblTask tr td .AnjamShode").length
    var i=0;
    $(".TblTask tr td .AnjamShode").each(async function () {
        i+=1;
        if ($(this).is(':checked')) {
            // checked
            obj.IsCheck = true
            obj.TaskId = $(this).attr("Data_id")

         var x=await   UpdateTask2(obj)
            
        }
        else {
            // unchecked

        }

        if(count==i)
        {
            ListTask("anjamnashode");
            $.LoadingOverlay("hide");
       // resolve("finish")
        }

    })
    
}
function UpdateTask2(obj) {

    return new Promise(resolve => {
        $.ajax(
           {
               type: 'POST',
               contentType: "application/json;charset=utf-8",
               dataType: "json",
               url: "/Task/UpdateTask",
               data: JSON.stringify({ TaskId: obj.TaskId, IsCheck: obj.IsCheck }),
               success: function (result) {
                   resolve(result)

               },
               error: function (error) {
                   resolve(error)
               }
           });
    })
}
function RefreshTask() {
    RefreshChk();
    ListTaskGeneral();
    ListTaslLevelHigh();
    ListTiming(0);
}
