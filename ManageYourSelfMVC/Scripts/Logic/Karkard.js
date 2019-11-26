//*********************Karkard********************************************************
$("#TimeForJob input[name='List']").on("click", function () {
    ListKarkard();
});
//Create Get
$("#TimeForJob").on("click", "input[name='Create']", function () {
    CreateKarkardGet();
});
//Create Get
$("#TimeForJob").on("click", "input[name='UploadDataCard']", function () {
    UploadDataCard();
});
//Delete
$("#TimeForJob").on("click", "table .fa-remove", function () {

    var res = confirm("آیا حذف انجام شود؟");
    if (res == true) {
        var KarkardId = $(this).attr("KarkardId");
        DeleteKarkard(KarkardId)
    }
});
function ListKarkard() {
    var urll = "/Karkard/List";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {

            $(".DivKarkard").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function ShowKarkadPivot() {
    var urll = "/Karkard/ShowKarkadPivot";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {

            $(".ShowKarkadPivot").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function CreateKarkardGet() {
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/Karkard/Create",
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
function CreateKarkardPost() {
    var SpendTimeMinute = $("#MasterModal input[name='SpendTimeMinute'").val();
    var DayDate = $("#MasterModal input[name='DayDate'").val();
    var JobId = $("#MasterModal option:selected").attr("JobId");
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Karkard/Create",
           data: JSON.stringify({ JobId: JobId, SpendTimeMinute: SpendTimeMinute, DayDate: DayDate }),
           success: function (result) {
               if (result == true) {
                   RefreshExecute();
                   ListKarkard();
               }
               else {
                   alert("خطا در ثبت");
               }
           }
       });
}
function DeleteKarkard(KarkardId) {

    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Karkard/DeleteKarkard",

           data: JSON.stringify({ KarkardId: KarkardId }),
           success: function (result) {
               if (result == true) {
                   ListKarkard();
                   RefreshExecute();
               }
               else {
                   alert("خطا در ثبت");
               }
           }
       });
}
