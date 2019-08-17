//--Execute All List when click Tab
$("ul li a[href='#MojoodyBank']").on("click", function () {
    //ListMojoodyBank();
    //ListTypeHazineh();
    //ListDaramd();
    RefreshListMojoodyBank()
});
//****************************************************************MojoodyBank
function ListMojoodyBank() {
    var urll = "/DakhloKharj/ListMojoodyBank";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListMojoodyBank").html(data);
        },
        error: function (error) {
            alert(error);
        }
    })
}
function CreateMojoodyBankPost() {
   
    var MojoodyName = $("#MasterModal input[name='MojoodyName']").val();
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/DakhloKharj/CreateMojoodyBank",
           data: JSON.stringify({ MojoodyName: MojoodyName }),
           success: function (result) {
               if (result == true) {
                   RefreshListMojoodyBank();
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
function CreateMojoodyBankGet() {
 
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/DakhloKharj/CreateMojoodyBank",
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
function EditMojoodyBank(MojoodyBankId) {
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/DakhloKharj/EditMojoodyBank?MojoodyBankId=" + MojoodyBankId,
           success: function (result) {
               $(".BodyModal").html(result);
               $("#MasterModal").modal();
           },
           error: function (error) {
               alert(error);
           }
       });
}
function UpdateMojoodyBank() {
 
    var MojoodyName = $("#MasterModal input[name='MojoodyName']").val();
    var MojoodyBankId = $("#MasterModal div[name='EditMojoodyBank'] table").attr("MojoodyBankId");
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/DakhloKharj/UpdateMojoodyBank",
           data: JSON.stringify({ MojoodyName: MojoodyName, MojoodyBankId: MojoodyBankId }),
           success: function (result) {
                   RefreshListMojoodyBank();
           }
       });
}
function DeleteMojoodyBank(MojoodyBankId) {
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/DakhloKharj/DeleteMojoodyBank",

           data: JSON.stringify({ MojoodyBankId: MojoodyBankId }),
           success: function (result) {
               if (result == true) {
                   RefreshListMojoodyBank();
               }
               else {
                   alert("خطا در ثبت");
               }
           }
       });
}
function RefreshListMojoodyBank() {
    ListMojoodyBank();
    ListTypeHazineh();
    ListDaramd();
    Rpt_ListGroupHazine();
}
//--------------------------Events
//--Create Get
$(".ListMojoodyBank").on("click", "input[name='CreateMojoodyBank']", function () {
    CreateMojoodyBankGet();
});
//--Delete
$(".ListMojoodyBank").on("click", ".fa-remove", function () {
    var res = confirm("آیا حذف انجام شود؟");
    if (res == true) {
        var MojoodyBankId = $(this).attr("MojoodyBankId");
        DeleteMojoodyBank(MojoodyBankId);
    }
});
//--Edit
$(".ListMojoodyBank").on("click", ".fa-edit", function () {
    var MojoodyBankId = $(this).attr("MojoodyBankId");
    EditMojoodyBank(MojoodyBankId);
});
//---Balance
$(".ListMojoodyBank").on("click", ".fa-balance-scale", function () {
    var MojoodyBankId = $(this).attr("MojoodyBankId");
    EditMojoodyBankBalance(MojoodyBankId);
});
//---exchange
$(".ListMojoodyBank").on("click", ".fa-exchange", function () {
    var MojoodyBankId = $(this).attr("MojoodyBankId");
    EditMojoodyBankExchange(MojoodyBankId);
});
//---plus
$(".ListMojoodyBank").on("click", ".fa-plus", function () {
    var MojoodyBankId = $(this).attr("MojoodyBankId");
    // EditMojoodyBankExchange(MojoodyBankId);
    CreateDaramdGet(MojoodyBankId);
});
//---minus
$(".ListMojoodyBank").on("click", ".fa-minus", function () {
    var MojoodyBankId = $(this).attr("MojoodyBankId");
    EditMojoodyBankExchange(MojoodyBankId);
});

//*************************************************************TypeHazineh

function ListTypeHazineh() {
    var urll = "/DakhloKharj/ListTypeHazineh";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListTypeHazineh").html(data);
        },
        error: function (error) {
            alert(error);
        }
    })
}
function DeleteTypeHazineh(TypeHazinehId) {
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/DakhloKharj/DeleteTypeHazineh",

           data: JSON.stringify({ TypeHazinehId: TypeHazinehId }),
           success: function (result) {
               if (result == true) {
                   RefreshListMojoodyBank();
               }
               else {
                   alert("خطا در ثبت");
               }
           },
           error: function (result) {
               alert(result);
           }
       });
}
function CreateTypeHazinehGet() {

    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/DakhloKharj/CreateTypeHazineh",
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
function CreateTypeHazinehPost() {
    var name = $("#MasterModal input[name='name']").val();
    var DaramadOrKharj = $("#MasterModal  input[type='radio'][name='TypeHazineh']:checked").val();
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/DakhloKharj/CreateTypeHazineh",
           data: JSON.stringify({ name: name, DaramadOrKharj: DaramadOrKharj }),
           success: function (result) {
               if (result == true) {
                   RefreshListMojoodyBank();
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
function EditTypeHazineh(TypeHazinehId) {
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/DakhloKharj/EditTypeHazineh?TypeHazinehId=" + TypeHazinehId,
           success: function (result) {
               $(".BodyModal").html(result);
               $("#MasterModal").modal();
           },
           error: function (error) {
               alert(error);
           }
       });
}
function UpdateTypeHazineh() {
    var TypeHazinehId = $("#MasterModal div[name='EditTypeHazineh'] table").attr("TypeHazinehId");
    var name = $("#MasterModal input[name='name']").val();
    var DaramadOrKharj = $("#MasterModal  input[type='radio'][name='TypeHazineh']:checked").val();
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/DakhloKharj/UpdateTypeHazineh",
           data: JSON.stringify({ name: name, DaramadOrKharj: DaramadOrKharj, TypeHazinehId: TypeHazinehId }),
           success: function (result) {
               RefreshListMojoodyBank();
           }
       });
}
//---------------------------------------------------Events
//--Delete
$(".ListTypeHazineh").on("click", ".fa-remove", function () {
    var res = confirm("آیا حذف انجام شود؟");
    if (res == true) {
        var TypeHazinehId = $(this).attr("TypeHazinehId");
        DeleteTypeHazineh(TypeHazinehId);
    }
});
//--Create Get
$(".ListTypeHazineh").on("click", "input[name='CreateTypeHazineh']", function () {
    CreateTypeHazinehGet();
});
//--Edit
$(".ListTypeHazineh").on("click", ".fa-edit", function () {
    var TypeHazinehId = $(this).attr("TypeHazinehId");
    EditTypeHazineh(TypeHazinehId);
});


//***************************************************Daramad
function ListDaramd() {
    var urll = "/DakhloKharj/ListDaramad";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListDaramad").html(data);
        },
        error: function (error) {
            alert(error);
        }
    })
}
function CreateDaramdGet(MojoodyBankId) { 
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/DakhloKharj/CreateDaramad?MojoodyBankId=" + MojoodyBankId,
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
function CreateDaramadPost() {
        var MojoodyBankId = $("#MasterModal .MYSelect option:selected").val();
        var TypeHazinehId = $("#MasterModal .SelectlstTypeHazineh option:selected").val();
        var Rial = $("#MasterModal input[name='Rial']").val();
        var Description = $("#MasterModal input[name='Description']").val();
        var Date = $("#MasterModal input[name='Date']").val();
  //  var DaramadOrKharj = $("#MasterModal  input[type='radio'][name='TypeHazineh']:checked").val();
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/DakhloKharj/CreateDaramad",
           data: JSON.stringify({ MojoodyBankId: MojoodyBankId, TypeHazinehId: TypeHazinehId, Rial: Rial, Description: Description, Date:Date }),
           success: function (result) {
               if (result == true) {
                   RefreshListMojoodyBank();
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
function DeleteDaramad(DaramadId) {
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/DakhloKharj/DeleteDaramad",

           data: JSON.stringify({ DaramadId: DaramadId }),
           success: function (result) {
               if (result == true) {
                   RefreshListMojoodyBank();
               }
               else {
                   alert("خطا در ثبت");
               }
           },
           error: function (result) {
               alert(result);
           }
       });
}
function EditDaramad(DaramadId) {
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/DakhloKharj/EditDaramad?DaramadId=" + DaramadId,
           success: function (result) {
               $(".BodyModal").html(result);
               $("#MasterModal").modal();
           },
           error: function (error) {
               alert(error);
           }
       });
}
//--------------------------Events
//--Delete
$(".ListDaramad").on("click", ".fa-remove", function () {
    var res = confirm("آیا حذف انجام شود؟");
    if (res == true) {
        var DaramadId = $(this).attr("DaramadId");
        DeleteDaramad(DaramadId);
    }
});
//--Create Get
$(".ListDaramad").on("click", "input[name='CreateDaramad']", function () {
    CreateDaramdGet(0);
});
//--Edit
$(".ListDaramad").on("click", ".fa-edit", function () {
    var DaramadId = $(this).attr("DaramadId");
    EditDaramad(DaramadId);
});
//***************************************************Balance
function EditMojoodyBankBalance(MojoodyBankId) {
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/DakhloKharj/EditMojoodyBankBalance?MojoodyBankId=" + MojoodyBankId,
           success: function (result) {
               $(".BodyModal").html(result);
               $("#MasterModal").modal();
           },
           error: function (error) {
               alert(error);
           }
       });
}
function UpdateMojoodyBankBalance() {
    var FalseRial = $("#MasterModal input[name='FalseRial']").val();
    var OkRial = $("#MasterModal input[name='OkRial']").val();
    var MojoodyBankId = $("#MasterModal div[name='EditMojoodyBankBalance'] table").attr("MojoodyBankId");
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/DakhloKharj/UpdateMojoodyBankBalance?MojoodyBankId=" + MojoodyBankId + "&&FalseRial=" + FalseRial + "&&OkRial=" + OkRial,
           //data: JSON.stringify({ MojoodyName: MojoodyName, MojoodyBankId: MojoodyBankId }),
           success: function (result) {
               RefreshListMojoodyBank();
           }
       });
}
//***************************************************Exchange
function EditMojoodyBankExchange(MojoodyBankId) {
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/DakhloKharj/EditMojoodyBankExchange?MojoodyBankId=" + MojoodyBankId,
           success: function (result) {
               $(".BodyModal").html(result);
               $("#MasterModal").modal();
           },
           error: function (error) {
               alert(error);
           }
       });
}
function UpdateMojoodyBankExchange() {
    var Rial = $("#MasterModal input[name='Rial']").val();
    var Date = $("#MasterModal input[name='Date']").val();
    var MojoodyBankIdSource = $("#MasterModal div[name='Exchange'] table tr td").attr("MojoodyBankIdSource");
    var MojoodyBankIdDestination = $("#MasterModal .MYSelect option:selected").val();
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/DakhloKharj/UpdateMojoodyBankExchange?MojoodyBankIdSource=" + MojoodyBankIdSource + "&&MojoodyBankIdDestination=" + MojoodyBankIdDestination + "&&Rial=" + Rial + "&&Date=" + Date,
           //data: JSON.stringify({ MojoodyName: MojoodyName, MojoodyBankId: MojoodyBankId }),
           success: function (result) {
               RefreshListMojoodyBank();
           }
       });
}
//****************************************************Report
function Rpt_ListGroupHazine() {
    var urll = "/DakhloKharj/Rpt_ListGroupHazine";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".Rpt_ListGroupHazine").html(data);
        },
        error: function (error) {
            alert(error);
        }
    })
}
