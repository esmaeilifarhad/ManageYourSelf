//--Execute All List when click Tab
$("ul li a[href='#MasterData']").on("click", function () {
    ListMasterData();
});
//***************************************************MasterData
function ListMasterData() {

    var urll = "/MasterData/List";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListMasterData").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function CreateMasterDataPost() {
   
    var Title = $("#MasterModal input[name='Title']").val();
    var Code = $("#MasterModal input[name='Code']").val();
    var Dsc = $("#MasterModal input[name='Dsc']").val();
    var Order = $("#MasterModal input[name='Order']").val();
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/MasterData/Create",
               data: JSON.stringify({
                   Title: Title,
                   Code: Code,
                   Dsc: Dsc,
                   Order: Order
               }),
           success: function (result) {
               if (result == true) {
                   RefreshMasterData();
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
function CreateMasterDataGet() {
 
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/MasterData/Create",
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
function EditMasterData(Id) {
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/MasterData/Edit?Id=" + Id,
           success: function (result) {
               $(".BodyModal").html(result);
               $("#MasterModal").modal();
           },
           error: function (error) {
               console.log(error);
           }
       });
}
function UpdateMasterDataPost() {
    var CatId = $("#MasterModal div[name='UpdateMaterData'] table").attr("CatId");
    var Title = $("#MasterModal input[name='Title']").val();
    var Code = $("#MasterModal input[name='Code']").val();
    var Dsc = $("#MasterModal input[name='Dsc']").val();
    var Order = $("#MasterModal input[name='Order']").val();
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/MasterData/Update",
           data: JSON.stringify({
               Title: Title,
               Code: Code,
               Dsc: Dsc,
               Order: Order,
              CatId:CatId
           }),
           success: function (result) {
               RefreshMasterData();
               ListTaskGeneral();
               ListTaslLevelHigh();
               ListTaskFutureChk();

               ListSportChk();
              // RefreshChk();
           }
       });
}
function DeleteMasterData(Id) {
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/MasterData/Delete",

           data: JSON.stringify({ Id: Id }),
           success: function (result) {
               if (result == true) {
                   RefreshMasterData();
               }
               else {
                   alert("خطا در ثبت");
               }
           }
       });
       } 
function RefreshMasterData() {
    ListMasterData();
}
//--------------------------Events
//--Create Get
$(".ListMasterData").on("click", "input[name='CreateMasterData']", function () {
    CreateMasterDataGet();
});
//--Delete
$(".ListMasterData").on("click", ".RemoveMaster", function () {
    var res = confirm("آیا حذف انجام شود؟");
    
    if (res == true) {
        var Id = $(this).attr("catid");
        DeleteMasterData(Id);
    }
});
//--Edit
//$(".ListMasterData").on("click", ".fa-edit", function () {
//    var Id = $(this).attr("catid");
//    EditMasterData(Id);
//});
$("body").on("click", ".EditCat", function () {
    var Id = $(this).attr("catid");
    EditMasterData(Id);
})

