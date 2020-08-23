//--Execute All List when click Tab
$("ul li a[href='#Setting']").on("click", function () {
    
    ListHolyDay();
    ListSetting();
});
//***************************************************HolyDay
function ListHolyDay() {
    
    var urll = "/HolyDay/ListHolyDay";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListHolyDay").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function CreateHolyDayPost() {
   
    var MojoodyName = $("#MasterModal input[name='MojoodyName']").val();
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/HolyDay/CreateHolyDay",
           data: JSON.stringify({ MojoodyName: MojoodyName }),
           success: function (result) {
               if (result == true) {
                   RefreshListHolyDay();
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
function CreateHolyDayGet() {
 
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/HolyDay/CreateHolyDay",
           success: function (result) {
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
function DeleteHolyDay(HolyDayId) {
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/HolyDay/DeleteHolyDay",

           data: JSON.stringify({ HolyDayId: HolyDayId }),
           success: function (result) {
               if (result == true) {
                   RefreshListHolyDay();
               }
               else {
                   alert("خطا در ثبت");
               }
           }
       });
}
function RefreshListHolyDay() {
    ListHolyDay();
    ListTypeHazineh();
    ListDaramd();
}
//--------------------------Events
//--Create Get
$(".ListHolyDay").on("click", "input[name='CreateHolyDay']", function () {
    CreateHolyDayGet();
});
//--Delete
$(".ListHolyDay").on("click", ".fa-remove", function () {
    var res = confirm("آیا حذف انجام شود؟");
    if (res == true) {
        var HolyDayId = $(this).attr("HolyDayId");
        DeleteHolyDay(HolyDayId);
    }
});
//--Edit
$(".ListHolyDay").on("click", ".fa-edit", function () {
    var HolyDayId = $(this).attr("HolyDayId");
    EditHolyDay(HolyDayId);
});
//--------------------
function ListSetting() {
    
    localStorage.setItem("ShowFooterAlert", "true");
}

