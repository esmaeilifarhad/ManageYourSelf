var _MojoodyBankId = 0;
//--Execute All List when click Tab
$("ul li a[href='#MojoodyBank']").on("click", function () {
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
            // 
            $("#footer #MojoodyBank div").remove()
            $("#footer #MojoodyBank").append(data)
            // $("#footer #MojoodyBank").append("<h1>farhad</h1>")
        },
        error: function (error) {
            console.log(error);
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
                console.log(error);
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

                var tablebutt = "<table class='table' style='font-size: 9px;'>"
                tablebutt += "<tr>" +
                    "<td><input type='button' style='background-color:green' value='ذخیره' onclick='CreateMojoodyBankPost()'/> | " +
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
                console.log(error);
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
                $('#MasterModal').modal('hide');
            }
        });
}
function DeleteMojoodyBank(MojoodyBankId) {
    var res = confirm("آیا حذف انجام شود؟");
    if (res == true) {



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
}
function RefreshListMojoodyBank() {
    ListMojoodyBank();
    ListTypeHazineh();
    //ListDaramd();
    Rpt_ListGroupHazine();
    $('#MasterModal').modal('hide');
}

//$(".ListMojoodyBank").on("click", ".fa-minus", function () {
//    var MojoodyBankId = $(this).attr("MojoodyBankId");
//    EditMojoodyBankExchange(MojoodyBankId);
//});

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
            console.log(error);
        }
    })
}
function DeleteTypeHazineh(TypeHazinehId) {
    var res = confirm("آیا حذف انجام شود؟");
    if (res == true) {
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
}
function CreateTypeHazinehGet() {

    $.ajax(
        {
            type: 'get',
            contentType: "application/json;charset=utf-8",
            dataType: "html",
            url: "/DakhloKharj/CreateTypeHazineh",
            success: function (result) {

                var tablebutt = "<table class='table' style='font-size: 9px;'>"
                tablebutt += "<tr>" +
                    "<td><input type='button' style='background-color:green' value='ذخیره' onclick='CreateTypeHazinehPost()'/> | " +
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
                console.log(error);
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
                console.log(error);
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

//***************************************************Daramad
async function ListDaramd(MojoodyBankId) {

 
       
       
   

  

    var firstDate =""

    const m = moment();
    m.add(-10, 'day')
    var newDate = m.format('jYYYY/jM/jD')
    firstDate = convertDateToslashless(newDate)

    _MojoodyBankId = (MojoodyBankId == undefined ? _MojoodyBankId : MojoodyBankId)


    var obj = {}
    obj.url = "/DakhloKharj/ListDaramad"
    obj.dataType = "json"
    obj.type = "post"

    // var time = CurrentTime()
    // var date = todayShamsy8char()
    obj.data = { firstDate: firstDate, MojoodyBankId: _MojoodyBankId }
    var results = await Promise.all([
        service(obj)
    ]);

    var ListObj = results[0]

    var tablebutt = "<tr>" +
        "<td><input type='button'  class='btn btn-danger' value='بستن' onclick='closeModal()'/></td>" +
        "</tr>"
    tablebutt += "</table>"

    $(".modal-footer").empty();
    $(".modal-footer").append(tablebutt);

    var table = " <table>"+
       "<tr>"+
        " <td><input type='text' name='DateStart' autocomplete='off' placeholder='شروع' value=" + foramtDate(todayShamsy8char())+"  /></td>"+
        " <td><input type='text' name='DateEnd' autocomplete='off' placeholder='پایان' value=" + foramtDate(todayShamsy8char()) +" /></td>"+
       " <td><input type='button' value='جستجو' /></td>"+
       " </tr >"+
   " </table >" +
        "<table class='table table-responsive'>" +
        "<tr><th>بانک</th><th>نوع هزینه</th><th>تاریخ</th><th>پرداخت | دریافت</th><th>توضیحات</th><th>ریال</th><th>قبل</th><th>بعد</th></tr>"
    for (var i = 0; i < ListObj.length; i++) {

        table += "<tr>" +
            "<td>" + ListObj[i].MojoodyBankName + "</td>" +
            "<td>" + ListObj[i].TypeHazineh.name + "</td>" +
            "<td>" + ListObj[i].Date + "</td>"
        if (ListObj[i].DaramadORHazineh == true) {
            table += "<td style='color:green'>دریافت</td>"
        }
        else {

            table += "<td style='color:red'>پرداخت</td>"
        }
        table += "<td>" + ListObj[i].Description + "</td>" +
            "<td>" + SeparateThreeDigits(ListObj[i].Rial) + "</td>" +
            "<td>" + SeparateThreeDigits(ListObj[i].Before) + "</td>" +
            "<td>" + SeparateThreeDigits(ListObj[i].After) + "</td>" +
            "<td><span style='color:green' class='fa fa-edit pointer' onclick='EditDaramad("+ListObj[i].DaramadId+")'></span></td>"+
            "<td><span style='color:red' class='fa fa-remove pointer' onclick='DeleteDaramad("+ListObj[i].DaramadId+")'></span></td>"+
            "</tr >"
    }

    table += "</table>"


    //var tablebutt = "<tr>" +
    //    "<td><input type='button' class='btn btn-success' value='ویرایش' onclick='UpdateBook(" + ListObj.BookId + ")'/> | " +
    //    "<input type='button'  class='btn btn-danger' value='بستن' onclick='closeModal()'/></td>" +
    //    "</tr>"
    //tablebutt += "</table>"

    //$(".modal-footer").empty();
    //$(".modal-footer").append(tablebutt);

    $(".BodyModal").empty();
    $(".BodyModal").append(table);
    $("#MasterModal").modal();

    /*
    var urll = "/DakhloKharj/ListDaramad"
    $.ajax({
        type: "post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        url: urll,
        data: { Datee: Datee, MojoodyBankId: _MojoodyBankId},
        success: function (data) {
            
            var tablebutt = "<table class='table' style='font-size: 9px;'>"
            tablebutt += "<tr>" +
                "<td> " +
                "<input type='button' value='بستن' onclick='closeModal()'/></td>" +
                "</tr>"
            tablebutt += "</table>"
            $(".modal-footer").empty();
            $(".modal-footer").append(tablebutt);


            $(".ListDaramad").html(data);
            $(".BodyModal").html(data);
            $("#MasterModal").modal();
        },
        error: function (error) {
            console.log(error);
        }
    })
    */
}
function CreateDaramdGet(MojoodyBankId) {
    $.ajax(
        {
            type: 'get',
            contentType: "application/json;charset=utf-8",
            dataType: "html",
            url: "/DakhloKharj/CreateDaramad?MojoodyBankId=" + MojoodyBankId,
            success: function (result) {

                var tablebutt = "<table class='table' style='font-size: 9px;'>"
                tablebutt += "<tr>" +
                    "<td><input type='button' style='background-color:green' value='ذخیره' onclick='CreateDaramadPost()()'/> | " +
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
function CreateDaramadPost() {

    var MojoodyBankId = $("#MasterModal .MYSelect option:selected").val();
    _MojoodyBankId = (MojoodyBankId == undefined ? _MojoodyBankId : MojoodyBankId)
    var TypeHazinehId = $("#MasterModal .SelectlstTypeHazineh option:selected").val();
    var Rial = $("#MasterModal input[name='Rial']").val();

    Rial = parseInt(removeComma(Rial))

    var Description = $("#MasterModal input[name='Description']").val();
    var Date = $("#MasterModal input[name='Date']").val();
    //  var DaramadOrKharj = $("#MasterModal  input[type='radio'][name='TypeHazineh']:checked").val();
    $.ajax(
        {
            type: 'POST',
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            url: "/DakhloKharj/CreateDaramad",
            data: JSON.stringify({ MojoodyBankId: MojoodyBankId, TypeHazinehId: TypeHazinehId, Rial: Rial, Description: Description, Date: Date }),
            success: function (result) {

                RefreshListMojoodyBank();
                //$("#MasterModal").hide();
                $('#MasterModal').modal('hide');

            },
            error: function (error) {
                console.log(error);
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
                console.log(error);
            }
        });
}
function UpdateDaramad(DaramadId) {

    var MojoodyBankId = $("#MasterModal #MojoodyBank option:selected").val();
    var TypeHazinehId = $("#MasterModal .SelectlstTypeHazineh option:selected").val();
    var Rial = $("#MasterModal input[name='Rial']").val();
    Rial = parseInt(removeComma(Rial))
    //Rial=parseInt(Rial)
    var Description = $("#MasterModal input[name='Description']").val();
    var Date = $("#MasterModal input[name='Date']").val();

    $.ajax(
        {
            type: 'POST',
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            url: "/DakhloKharj/UpdateDaramad",
            data: JSON.stringify({ MojoodyBankId: MojoodyBankId, TypeHazinehId: TypeHazinehId, Rial: Rial, Description: Description, Date: Date, DaramadId: DaramadId }),
            success: function (result) {
                //if (result == true) {
                RefreshListMojoodyBank();
                $('#MasterModal').modal('hide');
                //}
                //else {
                //    alert("خطا در ثبت");
                //}
            },
            error: function (error) {
                console.log(error);
            }
        });



}
//--------------------------Events
////--Delete
//$(".ListDaramad").on("click", ".fa-remove", function () {
//    var res = confirm("آیا حذف انجام شود؟");
//    if (res == true) {
//        var DaramadId = $(this).attr("DaramadId");
//        DeleteDaramad(DaramadId);
//    }
//});
////--Create Get
//$(".ListDaramad").on("click", "input[name='CreateDaramad']", function () {
//    CreateDaramdGet(0);
//});
////--Edit
//$(".ListDaramad").on("click", ".fa-edit", function () {
//    var DaramadId = $(this).attr("DaramadId");
//    EditDaramad(DaramadId);
//});
//***************************************************Balance
function EditMojoodyBankBalance(MojoodyBankId) {
    _MojoodyBankId = (MojoodyBankId == undefined ? _MojoodyBankId : MojoodyBankId)
    $.ajax(
        {
            type: 'get',
            contentType: "application/json;charset=utf-8",
            dataType: "html",
            url: "/DakhloKharj/EditMojoodyBankBalance?MojoodyBankId=" + MojoodyBankId,
            success: function (result) {


                var tablebutt = "<table class='table' style='font-size: 9px;'>"
                tablebutt += "<tr>" +
                    "<td><input type='button' style='background-color:green' value='ذخیره' onclick='UpdateMojoodyBankBalance()'/> | " +
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
        });
}
function UpdateMojoodyBankBalance() {
    var FalseRial = $("#MasterModal input[name='FalseRial']").val();
    var OkRial = $("#MasterModal input[name='OkRial']").val();


    OkRial = parseInt(removeComma(OkRial))

    var MojoodyBankId = $("#MasterModal div[name='EditMojoodyBankBalance'] table").attr("MojoodyBankId");
    _MojoodyBankId = (MojoodyBankId == undefined ? _MojoodyBankId : MojoodyBankId)
    $.ajax(
        {
            type: 'POST',
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            url: "/DakhloKharj/UpdateMojoodyBankBalance?MojoodyBankId=" + MojoodyBankId + "&&FalseRial=" + FalseRial + "&&OkRial=" + OkRial,
            //data: JSON.stringify({ MojoodyName: MojoodyName, MojoodyBankId: MojoodyBankId }),
            success: function (result) {
                RefreshListMojoodyBank();
                $('#MasterModal').modal('hide');
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


                var tablebutt = "<table class='table' style='font-size: 9px;'>"
                tablebutt += "<tr>" +
                    "<td><input type='button' style='background-color:green' value='ذخیره' onclick='UpdateMojoodyBankExchange()'/> | " +
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
        });
}
function UpdateMojoodyBankExchange() {
    var Rial = $("#MasterModal input[name='Rial']").val();
    Rial = parseInt(removeComma(Rial))
    var Date = $("#MasterModal input[name='Date']").val();
    var MojoodyBankIdSource = $("#MasterModal div[name='Exchange'] table tr td").attr("MojoodyBankIdSource");
    var MojoodyBankIdDestination = $("#MasterModal .MYSelect option:selected").val();
    _MojoodyBankId = (MojoodyBankIdDestination == undefined ? _MojoodyBankId : MojoodyBankIdDestination)
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
            console.log(error);
        }
    })
}
function changeInputToThreeDigit(thiss) {

    var x = removeComma(thiss.value)
    x = SeparateThreeDigits(x)
    thiss.value = (x == 'NaN' ? 0 : x)
}
function removeComma(str) {

    var noCommas = str.replace(/,/g, '')
    //  asANumber = +noCommas;
    return noCommas
}
function SeparateThreeDigits(str) {
    var x = parseInt(str);
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

    // return parseInt(str);

}
