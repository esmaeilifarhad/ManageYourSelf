//----------------------******************Job**************************************************************
//Create Get
$("div .Job").on("click", "input[name='CreateJob']", function () {
    CreateJobGet();
});
//Delete
$("div .Job").on("click", ".fa-remove", function () {
    var res = confirm("آیا حذف انجام شود؟");
    if (res == true) {
        var JobId = $(this).attr("JobId");
        DeleteJob(JobId);
    }
});
//Edit
$("div .Job").on("click", ".fa-edit", function () {
    var JobId = $(this).attr("JobId");
    EditJob(JobId);
});
function CreateJobGet() {
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/Job/CreateJob",
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
function CreateJobPost() {

    var JobName = $("#MasterModal textarea[name='JobName'").val();
    var CategoryId = $("#MasterModal option:selected").val();
    var Mohasebe = $("#MasterModal input[name=Moh]:checked").val();
    var Show = $("#MasterModal input[name=Show]:checked").val();
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Job/CreateJob",
           data: JSON.stringify({ Name: JobName, Mohasebe: Mohasebe, GridShow: Show, CategoryId: CategoryId }),
           success: function (result) {
               if (result == true) {
                   RefreshExecute();
               }
               else {
                   alert("خطا در ثبت");
               }
           }
       });
}
function DeleteJob(JobId) {
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Job/DeleteJob",

           data: JSON.stringify({ JobId: JobId }),
           success: function (result) {
               if (result == true) {
                   //  alert("حذف انجام شد");
                   // RefreshExecute();
                   ListJob()
               }
               else {
                   alert("خطا در ثبت");
               }
           }
       });
}
function EditJob(JobId) {
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/Job/EditJob?JobId=" + JobId,
           success: function (result) {
               $(".BodyModal").html(result);
               $("#MasterModal").modal();

           },
           error: function (error) {
               console.log(error);
           }
       });
}
function UpdateJob(JobId) {

    var JobName = $("#MasterModal input[name='JobName'").val();
    var CategoryId = $("#MasterModal option:selected").val();
    var Mohasebe = $("#MasterModal input[name=Moh]:checked").val();
    var Show = $("#MasterModal input[name=Show]:checked").val();
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Job/UpdateJob",
           data: JSON.stringify({ Name: JobName, Mohasebe: Mohasebe, GridShow: Show, CategoryId: CategoryId, JobId: JobId }),
           success: function (result) {
               if (result == true) {
                   RefreshExecute();
               }
               else {
                   alert("خطا در ثبت");
               }
           }
       });
}
function ListJob() {
    var urll = "/Job/ListJob";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {

            $(".Job").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function RefreshExecute() {
    ListCategory();
    ListJob();
    ListPercentJob();
    ShowKarkadPivot();
    ListKarkard();
    ListKarkardNew();
    SumMounthKarkard();
    SumKarkardPerJob();
    SumMounthKarkardPerJob();
}