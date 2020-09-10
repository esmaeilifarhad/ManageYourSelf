//------------------************************Category*************************************************
//List Job and Category بروز آوری هر دو
$("#Category input[name='List']").on("click", function () {

    ListKarkardNew();
    ListCategory();
    ListJob();
    ListPercentJob();
    ListKarkard();
    SumMounthKarkard();
});
////Create Get
//$("div .Category").on("click", "input[name='CreateCategory']", function () {
//    CreateCategoryGet();
//});
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
//$("div .PercentJob").on("click", "input[name='CreatePercentJob']", function () {

//    CreatePercentJobGet();
//});
function CreatePercentJobGet() {
    $.ajax(
        {
            type: 'get',
            contentType: "application/json;charset=utf-8",
            dataType: "html",
            url: "/PercentJob/Create",
            success: function (result) {

                var tablebutt = "<table class='table' style='font-size: 9px;'>"
                tablebutt += "<tr>" +
                    "<td><input type='button' class='btn btn-success' value='ذخیره' onclick='CreatePercentJobPost()'/> | " +
                    "<input type='button' class='btn btn-danger' value='بستن' onclick='closeModal()'/></td>" +
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
function EditPercentJobGet(PercentId) {
    // var PercentId=$(thiss).attr('PercentId');
    $.ajax(
        {
            type: 'get',
            contentType: "application/json;charset=utf-8",
            dataType: "html",
            url: "/PercentJob/Edit?PercentId=" + PercentId,
            success: function (result) {
                var tablebutt = "<table class='table' style='font-size: 9px;'>"
                tablebutt += "<tr>" +
                    "<td><input type='button' class='btn btn-success' value='ذخیره' onclick='UpdatePercentJobPost(" + PercentId + ")'/> | " +
                    "<input type='button' class='btn btn-danger' value='بستن' onclick='closeModal()'/></td>" +
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
                $("#MasterModal").modal("toggle");
                if (result == true) {
                    ListPercentJob();
                }
                else {
                    alert("خطا در ثبت");
                }
            }
        });
}
function UpdatePercentJobPost(PercentId) {

    var PercentValue = $("#MasterModal input[name='PercentValue'").val();
    var JobId = $("#MasterModal table tr td span").attr("JobId");


    $.ajax(
        {
            type: 'POST',
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            url: "/PercentJob/Create",
            data: JSON.stringify({ PercentValue: PercentValue, JobId: JobId }),
            success: function (result) {
                $("#MasterModal").modal("toggle");
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
async function ListKarkardNew() {
    var arrayPercentJob =await ListPercentJobThisMounth()
    
    SumMounthKarkard();
    SumKarkardPerJob();
    SumMounthKarkardPerJob();
    ListSumKarkardUntilToToday();
    
    ListTopBestKarkard(localStorage.getItem("Offset") == null ? 5 : localStorage.getItem("Offset"), localStorage.getItem("fetch") == null ? 5 : localStorage.getItem("fetch"))
    ListDaysOfWeek();
    ListSumDaysOfWeek();
    ListToCurrentTime();
    const m = moment();
    var month = m.jMonth() +1
    var day = m.jDate()
    var year = m.jYear()
    
    var today = (year) + "" + ((month) < 7 ? "0" + month : month) + "01"
    var obj = {}
    obj.url = "/Karkard/ListKarkard"
    obj.dataType = "json"
    obj.type = "post"
    obj.data = { dateParam: today }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]

    var header = []
    var body = []
    var date = []


    for (var i = 0; i < ListObj.length; i++) {

        var res = header.find(x => x.Name == ListObj[i].Name);
        if (res == undefined) {
            
            header.push({ Name: ListObj[i].Name, JobId: ListObj[i].JobId})
        }

        var res = date.find(x => x.DayDate == ListObj[i].DayDate);
        if (res == undefined) {
            date.push({ DayDate: ListObj[i].DayDate })
        }

        if (!body[ListObj[i].DayDate]) {
            body[ListObj[i].DayDate] = [];
        }
        var KarkardObj = {}
        KarkardObj.DayDate = ListObj[i].DayDate
        KarkardObj.Name = ListObj[i].Name
        KarkardObj.JobId = ListObj[i].JobId
        KarkardObj.SpendTimeMinute = ListObj[i].SpendTimeMinute


        body[ListObj[i].DayDate].push({ KarkardObj })

    }


    var table = "<table class='table table-bordered' id='SumKarkard'>"
    table += "<tr>"
    table += "<th><p>تاریخ</p></th>"
    for (var k = 0; k < header.length; k++) {
        var res = arrayPercentJob.find(x => x.JobId == header[k].JobId)
        
        table += "<th><p JobId=" + header[k].JobId + ">" + header[k].Name + "</p><p> PE : % " + res.PercentValue + "</p></th>"
    }
    table += "<th><p>مجموع</p></th>"
    table += "</tr>"
    var sumAllDay = 0
    for (var i = 0; i < date.length; i++) {

        table += "<tr>"
        table += "<td>" + foramtDate(date[i].DayDate) + " - " + calDayOfWeek(date[i].DayDate) + "</td>"
        for (var k = 0; k < header.length; k++) {
            table += "<td>"
            var sumDay = 0
            var minute = 0

            for (j = 0; j < body[date[i].DayDate].length; j++) {
                sumDay += parseInt(body[date[i].DayDate][j].KarkardObj.SpendTimeMinute)
               
                if (header[k].Name == body[date[i].DayDate][j].KarkardObj.Name) {
                    minute += parseInt(body[date[i].DayDate][j].KarkardObj.SpendTimeMinute / 60)
                    sumAllDay += parseInt(body[date[i].DayDate][j].KarkardObj.SpendTimeMinute)
                }
                else {

                }

            }
            table += minute 
            table += "</td>"
        }
        table += "<td>" + sumDay / 60 + "</td>"
        table += "</tr>"

    }
    table += "</table>"
    $(".sss").empty();
    $(".sss").append(table);
    
    var count = $('#SumKarkard th').length + 1
    for (var i =2; i < count; i++) {
        var sum = 0
        $("#SumKarkard tr").not(':first').each(function () {
            sum += parseInt($(this).find('td:nth-child(' + i + ')').text())
        })
        $("#SumKarkard").find('th:nth-child(' + i + ')').append("<p style='color:red'>P : " + " % " + ((sum * 100) / (sumAllDay / 60)).toFixed(1) + "</p>")
   
        $("#SumKarkard").find('th:nth-child(' + i + ')').append("<p style='color:blue'>h : " + (sum / 60).toFixed(1) + "</p>")
        $("#SumKarkard").find('th:nth-child(' + i + ')').append("<p style='color:green'>M : " + (sum) + "</p>")
       // $("#SumKarkard").find('th:nth-child(' + i + ')').append("<p style='color:red'>P : " +" % "+ ((sum * 100) / (sumAllDay / 60)).toFixed(1)  + "</p>")
    }

    

}
async function SumMounthKarkard() {
    var obj = {}
    obj.url = "/Karkard/SumMounthKarkard"
    obj.dataType = "json"
    obj.type = "post"
   // obj.data = { dateParam: today }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    var table ="<h3>گزارش ماهانه</h3><table  class='table table-bordered'>"
    for (var i = 0; i < ListObj.length; i++) {
        
        table += "<tr>"
        table += "<td>" + ListObj[i].Date + "</td>"
        table += "<td>" + ListObj[i].TimePer + "</td>"
        table += "<td><input type='button' class='btn btn-danger' value='Delete' onclick='DeleteRangeKarkard(" + ListObj[i].Date+")'/></td>"
        table += "</tr>"
    }
    table += "</table>"
    $(".ReportsKarkard").empty();
    $(".ReportsKarkard").append(table);
    
    
}
async function SumKarkardPerJob() {
    var obj = {}
    obj.url = "/Karkard/SumKarkardPerJob"
    obj.dataType = "json"
    obj.type = "post"
    // obj.data = { dateParam: today }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    var table = "<table  class='table table-bordered'>"
    for (var i = 0; i < ListObj.length; i++) {
        
        table += "<tr>"
        table += "<td>" + ListObj[i].JobName + "</td>"
        table += "<td>" + ListObj[i].TimePer + "</td>"
        table += "</tr>"
    }
    table += "</table>"
    $(".SumKarkardPerJob").empty();
    $(".SumKarkardPerJob").append(table);


}
async function SumMounthKarkardPerJob() {
    var obj = {}
    obj.url = "/Karkard/SumMounthKarkardPerJob"
    obj.dataType = "json"
    obj.type = "post"
    // obj.data = { dateParam: today }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    var table = "<table  class='table table-bordered'>"
    for (var i = 0; i < ListObj.length; i++) {

        table += "<tr>"
        table += "<td>" + ListObj[i].Date + "</td>"
        table += "<td>" + ListObj[i].JobName + "</td>"
        table += "<td>" + ListObj[i].TimePer + "</td>"
       
        table += "</tr>"
    }
    table += "</table>"
    $(".SumMounthKarkardPerJob").empty();
    $(".SumMounthKarkardPerJob").append(table);


}
async function ListPercentJobThisMounth() {
    const m = moment();
    var month = m.jMonth() + 1
    var day = m.jDate()
    var year = m.jYear()

    var today = (year) + "" + ((month) < 7 ? "0" + month : month) //+ "01"
    var obj = {}
    obj.url = "/PercentJob/ListPercentJob"
    obj.dataType = "json"
    obj.type = "post"
    obj.data = { Date: today }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    return ListObj
}
async function ListSumKarkardUntilToToday(thiss) {
   var daytxt= $("#txtSumKarkardUntilToToday").val();
    const m = moment();
    var month = m.jMonth() + 1
    var day = m.jDate()
    var year = m.jYear()

    var today = ((day) < 10 ? "0" + day : day) //+ "01"
    if (daytxt != undefined) {
        today = daytxt
    }
    var obj = {}
    obj.url = "/Karkard/SumKarkardUntilToToday"
    obj.dataType = "json"
    obj.type = "post"
    obj.data = { today: today }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    
    var table = "<table  class='table table-bordered'>"
    table += "<tr>"
    table += "<td><input id='txtSumKarkardUntilToToday' type='text' value=" + today+" /></td>"
    table += "<td><input class='btn btn-info' type='button' value='search' onclick='ListSumKarkardUntilToToday()'/></td>"
    table += "</tr>"
    for (var i = 0; i < ListObj.length; i++) {
       

        table += "<tr>"
        table += "<td>" + ListObj[i].Date + "</td>"
        table += "<td>" + ListObj[i].TimePer + "</td>"
        table += "</tr>"
    }
    table += "</table>"
    $(".SumKarkardUntilToToday").empty();
    $(".SumKarkardUntilToToday").append(table);

    
}
async function ListTopBestKarkard(Offset, FETCH) {


    var obj = {}
    obj.url = "/Karkard/TopBestKarkard"
    obj.dataType = "json"
    obj.type = "post"
    obj.data = { today: todayShamsy8char(), Offset: Offset, FETCH: FETCH }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    
    var table = "<table><tr><th>چند تا از قبل</th><th>تعداد نمایش</th></tr>" +
        "<tr><td><input type='number' name='offset' placeholder='offset' value=" + localStorage.getItem("Offset")+" onchange='fillLocalStorage(this,\""+'Offset'+"\")'/></td>" +
        "<td><input type='number' name='fetch' placeholder='fetch' value=" + localStorage.getItem("fetch") +"  onchange='fillLocalStorage(this,\"" + 'fetch' +"\")'/></td></tr></table > " +
        "<table class='table table-bordered'>"
        var today = todayShamsy8char()
         
    for (var i = 0; i < ListObj.length; i++) {

        if (today == ListObj[i].Date) {
            table += "<tr style='background-color:green;color:white'>"
        }
        else {
            table += "<tr>"
        }
        table += "<td>" + ListObj[i].Row + "</td>"
        table += "<td>" + foramtDate(ListObj[i].Date) + " " + calDayOfWeek(foramtDate(ListObj[i].Date)) + "</td>"
        table += "<td>" + minuteToTime(ListObj[i].TimePer)+/*" _ " + ListObj[i].TimePer + " _ " + (ListObj[i].TimePer / 60).toFixed(2) +*/ "</td>"
        table += "</tr>"
    }
    table += "</table>"
    $(".TopBestKarkard").empty();
    $(".TopBestKarkard").append(table);


}
async function fillLocalStorage(thiss, type) {
   // var offset = thiss.value
    localStorage.setItem(type, thiss.value);

    ListTopBestKarkard(localStorage.getItem("Offset"), localStorage.getItem("fetch"))
}
async function ListDaysOfWeek() {
    
    //var daytxt = $("#txtSumKarkardUntilToToday").val();
    const m = moment();
    var month = m.jMonth() + 1
    var day = m.jDate()
    var year = m.jYear()

    var today = (year) + "" + ((month) < 10 ? "0" + month : month)+ ((day) < 10 ? "0" + day : day) 

    today = today.substr(today.length - 6);

    
  //  const m = moment();
    const numberWeek = moment(today, 'jYYjMMjDD').weekday();
    
   // let day;
    switch (numberWeek) {
        case 0:
            day = 1;//یکشنبه
            break;
        case 1:
            day = 2;
            break;
        case 2:
            day = 3;
            break;
        case 3:
            day = 4;
            break;
        case 4:
            day = 5;
            break;
        case 5:
            day = 6;
            break;
        case 6:
            day = 7;//شنبه
    }

    
    var obj = {}
    obj.url = "/Karkard/ListDaysOfWeek"
    obj.dataType = "json"
    obj.type = "post"
    obj.data = { weekday: day }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    
    var table = "<table  class='table table-bordered'>"
    var today = todayShamsy8char()

    for (var i = 0; i < ListObj.length; i++) {

        if (today == ListObj[i].Date) {
            table += "<tr style='background-color:green;color:white'>"
        }
        else {
            table += "<tr>"
        }
        table += "<td>" + ListObj[i].Row + "</td>"
        table += "<td>" + foramtDate(ListObj[i].Date) + " " + calDayOfWeek(foramtDate(ListObj[i].Date))+"</td>"
        table += "<td>" + minuteToTime(ListObj[i].TimePer)  + "</td>"
        table += "</tr>"
    }
    table += "</table>"
    $(".ListDaysOfWeek").empty();
    $(".ListDaysOfWeek").append(table);


}

async function ListSumDaysOfWeek() {
    /*
    const m = moment();
    var month = m.jMonth() + 1
    var day = m.jDate()
    var year = m.jYear()
    var today = (year) + "" + ((month) < 10 ? "0" + month : month) + ((day) < 10 ? "0" + day : day)
    today = today.substr(today.length - 6);
    const numberWeek = moment(today, 'jYYjMMjDD').weekday();
    */
    
    var obj = {}
    obj.url = "/Karkard/ListSumDaysOfWeek"
    obj.dataType = "json"
    obj.type = "post"
   // obj.data = { weekday: day }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]

    var table = "<table  class='table table-bordered'>"
    var today = todayShamsy8char()

    for (var i = 0; i < ListObj.length; i++) {

        if (today == ListObj[i].Date) {
            table += "<tr style='background-color:green;color:white'>"
        }
        else {
            table += "<tr>"
        }
        table += "<td>" + ListObj[i].Row + "</td>"
        table += "<td>" + NameOfDayWeek(ListObj[i].weekday) + "</td>"
        table += "<td>" + minuteToTime(ListObj[i].TimePer) + "</td>"
        table += "</tr>"
    }
    table += "</table>"
    $(".ListSumDaysOfWeek").empty();
    $(".ListSumDaysOfWeek").append(table);


}

async function ListToCurrentTime() {

    var obj = {}
    obj.url = "/Karkard/ListToCurrentTime"
    obj.dataType = "json"
    obj.type = "post"
    // obj.data = { weekday: day }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]

    var table = "<table  class='table table-bordered'>"
    var today = todayShamsy8char()

    for (var i = 0; i < ListObj.length; i++) {

        if (today == ListObj[i].DayDate) {
            table += "<tr style='background-color:green;color:white'>"
        }
        else {
            table += "<tr>"
        }
        table += "<td>" + ListObj[i].Row + "</td>"
        table += "<td>" + foramtDate(ListObj[i].DayDate) + " " + calDayOfWeek(foramtDate(ListObj[i].DayDate)) + "</td>"
       // table += "<td>" + NameOfDayWeek(ListObj[i].weekday) + "</td>"
        table += "<td>" + minuteToTime(ListObj[i].TimePer/60) + "</td>"
        table += "</tr>"
    }
    table += "</table>"
    $(".ListToCurrentTime").empty();
    $(".ListToCurrentTime").append(table);


}

function NameOfDayWeek(dayWeek) {
    
     let day;
    switch (dayWeek) {
       
        case 1:
            day ="یکشنبه";
            break;
        case 2:
            day = "دوشنبه";
            break;
        case 3:
            day = "سه شنبه";
            break;
        case 4:
            day = "چهارشنبه";
            break;
        case 5:
            day = "پنجشنبه";
            break;
        case 6:
            day = "جمعه";
            break;
        case 7:
            day = "شنبه";
            break;
    }
    return day
}

async function DeleteRangeKarkard(date) {


    var res2 = await customConfirm({ title: "<p style='font-size:10px;font-weight:100'>" + date + "</p>", text: "آیا حذف انجام شود ؟", cancelButtonText: "خیر", confirmButtonText: "بلی" })
    if (res2.value == true) {
        $.LoadingOverlay("show");
    
    var obj = {}
    obj.url = "/Karkard/DeleteRange"
    obj.dataType = "json"
    obj.type = "post"
    obj.data = { date: date }
    var results = await Promise.all([
        service(obj)
        
    ]);
    var ListObj = results[0]
   // alert(ListObj)
    showAlert("تعداد موارد حذف شده : " + ListObj)
    $.LoadingOverlay("hide");
        RefreshExecute()
    }
}



