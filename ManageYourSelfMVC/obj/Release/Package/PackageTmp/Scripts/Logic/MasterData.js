//--Execute All List when click Tab
$("ul li a[href='#MasterData']").on("click", function () {
    ListMasterData();
});

//***************************************************MasterData
function ListMasterData() {
    $.LoadingOverlay("show");
    var urll = "/MasterData/List";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListMasterData").html(data);
            $.LoadingOverlay("hide");
        },
        error: function (error) {
            console.log(error);
            $.LoadingOverlay("hide");
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
                   GetData()
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
    $.LoadingOverlay("show");
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/MasterData/Edit?Id=" + Id,
           success: function (result) {
               
               var tablebutt = "<table class='table' style='font-size: 9px;'>"
               tablebutt += "<tr>" +
                   "<td><input type='button' style='background-color:green' value='ذخیره' onclick='UpdateMasterDataPost(" + Id + ")'/> | " +
                   "<input type='button' value='بستن' onclick='closeModal()'/></td>" +
                   "</tr>"
               tablebutt += "</table>"
               $(".modal-footer").empty();
               $(".modal-footer").append(tablebutt);


               $(".BodyModal").html(result);
               $("#MasterModal").modal();
               $.LoadingOverlay("hide");
           },
           error: function (error) {
               console.log(error);
               $.LoadingOverlay("hide");
           }
       });
}
function UpdateMasterDataPost(CatId) {
    
    $.LoadingOverlay("show");
    //var CatId = $("#MasterModal div[name='UpdateMaterData'] table").attr("CatId");
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
               $("#MasterModal").modal("toggle");
               RefreshMasterData();
               ListTaskGeneral();
               ListTaslLevelHigh();
               ListTaskFutureChk();

              // ListSportChk();
               GetData()
               // RefreshChk();
               $.LoadingOverlay("hide");
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


