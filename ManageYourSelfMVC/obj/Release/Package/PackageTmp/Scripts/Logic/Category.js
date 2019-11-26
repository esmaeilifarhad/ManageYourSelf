//------------------************************Category*************************************************
//List Job and Category بروز آوری هر دو
$("#Category input[name='List']").on("click", function () {
    ListCategory();
    ListJob();
    ListPercentJob();
});
//Create Get
$("div .Category").on("click", "input[name='CreateCategory']", function () {
    CreateCategoryGet();
});
//Delete
$("div .Category").on("click", ".fa-remove", function () {
    var res = confirm("آیا حذف انجام شود؟");
    if (res == true) {
        var CategoryId = $(this).attr("CategoryId");
        DeleteCategory(CategoryId);
    }
});
//Edit
$("div .Category").on("click", ".fa-edit", function () {
    var CategoryId = $(this).attr("CategoryId");
    EditCategory(CategoryId);
});
function ListCategory() {
    var urll = "/Category/ListCategory";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {

            $(".Category").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function CreateCategoryPost() {
    var CategoryName = $("#MasterModal textarea").val();

    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Category/CreateCategory",
           data: JSON.stringify({ CategoryName: CategoryName }),
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
function CreateCategoryGet() {
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/Category/CreateCategory",
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
function EditCategory(CategoryId) {
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/Category/EditCategory?CategoryId=" + CategoryId,

           success: function (result) {
               $(".BodyModal").html(result);
               $("#MasterModal").modal();

           },
           error: function (error) {
               console.log(error);
           }
       });
}
function UpdateCategory() {
    var CategoryName = $("#MasterModal textarea").val();
    var CategoryId = $("#MasterModal div textarea").attr("CategoryId");
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Category/UpdateCategory",
           data: JSON.stringify({ CategoryName: CategoryName, CategoryId: CategoryId }),
           success: function (result) {
               if (result == true) {
                   RefreshExecute();
               }
               else {

               }
           }
       });
}
function DeleteCategory(CategoryId) {
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Category/DeleteCategory",

           data: JSON.stringify({ CategoryId: CategoryId }),
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
//**************************************PercentJob*******************************************************
//Create Get
$("div .PercentJob").on("click", "input[name='CreatePercentJob']", function () {
    debugger
    CreatePercentJobGet();
});
function CreatePercentJobGet() {
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/PercentJob/Create",
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
function EditPercentJobGet(thiss) {
    var PercentId=$(thiss).attr('PercentId');
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/PercentJob/Edit?PercentId=" + PercentId,
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
function CreatePercentJobPost() {

    var PercentValue = $("#MasterModal input[name='PercentValue'").val();
    var JobId = $("#MasterModal option:selected").val();
    // var Mohasebe = $("#MasterModal input[name=Moh]:checked").val();
    // var Show = $("#MasterModal input[name=Show]:checked").val();
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/PercentJob/Create",
           data: JSON.stringify({ PercentValue: PercentValue, JobId: JobId }),
           success: function (result) {
               if (result == true) {
                   ListPercentJob();
               }
               else {
                   alert("خطا در ثبت");
               }
           }
       });
}
function UpdatePercentJobPost() {
    debugger
    var PercentValue = $("#MasterModal input[name='PercentValue'").val();
    var JobId = $("#MasterModal table tr td span").attr("JobId");
    // var Mohasebe = $("#MasterModal input[name=Moh]:checked").val();
    // var Show = $("#MasterModal input[name=Show]:checked").val();
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/PercentJob/Create",
           data: JSON.stringify({ PercentValue: PercentValue, JobId: JobId }),
           success: function (result) {
               if (result == true) {
                   ListPercentJob();
               }
               else {
                   alert("خطا در ثبت");
               }
           }
       });
}
function ListPercentJob() {
    debugger
    var urll = "/PercentJob/List";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {

            $(".PercentJob").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}