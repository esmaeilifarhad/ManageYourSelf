$(document).ready(function () {

    GetBook()
    setInterval(async function () {
        GetBook()
    }, 30000);
})
async function GetBook() {
    
    var ShowFooterAlert = localStorage.getItem("ShowFooterAlert");
    if (ShowFooterAlert=='false') return
    var obj = {}
    obj.url = "/Book/GetBook"
    obj.dataType = "json"
    obj.type = "post"
    //obj.data={TedadRooz:TedadRooz}
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    var inputs = "<input  style='color:black;cursor:pointer' class='smoke' type='button'  class='pointer' value='ایجاد' onclick='ShowBookForm()'/>" +
        "<input style='color:black;cursor:pointer' class='smoke' type='button'  class='pointer' value='ویرایش' onclick='EditBook(" + ListObj.BookId + ")'/>" +
        "<input style='color:black;cursor:pointer' class='smoke' type='button'  class='pointer' value='حذف' onclick='DeleteBook(" + ListObj.BookId + ")'/>"
    $(".smokyText").empty()
    var res = ListObj.dsc.split(" ");
    var spanSmoke = ""
    for (var i = 0; i < res.length; i++) {
        spanSmoke += "<span class='smoke'>" + res[i] + "</span>"
        // console.log(res[i])
    }
    var DateTime = "<span  class='smoke'>" + foramtDate(ListObj.date) + "</span>" + "<span  class='smoke' style='color:yellow'>" + showDays(todayShamsy(), foramtDate(ListObj.date)) + "</span>" +
        "<span  class='smoke'>" + foramtTime(ListObj.time) + "</span>"
    var RepeatedNumber = "<span class='smoke'><input type='checkbox' onclick='inreaseRepeatedNumber(" + ListObj.BookId + ")'/></span>" + "<span  class='smoke'>  " + ListObj.RepeatedNumber + " - </span>"
    $(".smokyText").append(RepeatedNumber + spanSmoke + DateTime + inputs)
}
async function GetBooks() {
    var obj = {}
    obj.url = "/Book/GetBooks"
    obj.dataType = "json"
    obj.type = "post"
    //obj.data={TedadRooz:TedadRooz}
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    var table = "<table  style='font-size:11px;background-image: linear-gradient(to left, #1838d4, #031225); color: white;' class='table table-bordered table-responsive'>" +
        "<tr><th>ردیف</th><th>توضیحات</th>" +
        "<th>تاریخ</th>" +
        "<th>چند روز</th>" +
        "<th>تعداد</th>" +
        "<th>ویرایش</th>" +
        "<th>حذف</th>" +
        "</tr > "
    for (var i = 0; i < ListObj.length; i++) {
        table += "<tr>" +
            "<td>" + (i+1) + "</td>" +
            "<td style='white-space: pre'>" + ListObj[i].dsc + "</td>" +
            "<td>" + foramtDate(ListObj[i].date) + "<br>" + calDayOfWeek(ListObj[i].date) +"</td>" +
            "<td>" + showDays(todayShamsy(), foramtDate(ListObj[i].date)) + "</td>" +
            "<td>" + ListObj[i].RepeatedNumber + "</td>" +
            "<td><input style='color: black; cursor: pointer'  type='button' value='ویرایش' onclick='EditBook(" + ListObj[i].BookId + ")'></td>" +
            "<td><input style='color: black; cursor: pointer'  type='button' value='حذف' onclick='DeleteBook(" + ListObj[i].BookId + ")'></td>" +
            "</tr > "
    }
    table += "</table>"
    $(".ListBook").empty()
    $(".ListBook").append(table)


}
async function ShowBookForm() {
    var table = "<div class='form-group'>" +
        "<textarea  name='BookName' class='form-control'  rows='3'></textarea>" +
        "</div >"
    var tablebutt = "<tr>" +
        "<td><input type='button' class='btn btn-success' value='ذخیره' onclick='CreateBook()'/> | " +
        "<input type='button'  class='btn btn-danger' value='بستن' onclick='closeModal()'/></td>" +
        "</tr>"
    tablebutt += "</table>"

    $(".modal-footer").empty();
    $(".modal-footer").append(tablebutt);

    $(".BodyModal").empty();
    $(".BodyModal").append(table);
    $("#MasterModal").modal();
}
async function CreateBook() {
    var dsc = $("#MasterModal  textarea[name='BookName']").val()
    $.LoadingOverlay("show");

    var obj = {}
    obj.url = "/Book/CreateBook"
    obj.dataType = "json"
    obj.type = "post"
    var time = CurrentTime()
    var date = todayShamsy8char()
    obj.data = { dsc: dsc, time: time, date: date }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    $("#MasterModal").modal("toggle");
    $.LoadingOverlay("hide");
}
async function DeleteBook(BookId) {

    $.LoadingOverlay("show");

    var obj = {}
    obj.url = "/Book/DeleteBook"
    obj.dataType = "json"
    obj.type = "post"
    obj.data = { BookId: BookId }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    GetBooks()
    $.LoadingOverlay("hide");
}
async function EditBook(BookId) {
    var obj = {}
    obj.url = "/Book/EditBook"
    obj.dataType = "json"
    obj.type = "post"
    obj.data = { BookId: BookId }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    var table = "<div class='form-group'>" +
        "<input type='checkbox' onclick='inreaseRepeatedNumber(" + ListObj.BookId + ")' /> Readed" +
        "<textarea  name='BookName' class='form-control'  rows='3'>" + ListObj.dsc + "</textarea>"+
  "</div >"

    var tablebutt = "<table><tr>" +
        "<td><input type='button' class='btn btn-success' value='ویرایش' onclick='UpdateBook(" + ListObj.BookId + ")'/> | " +
        "<input type='button'  class='btn btn-danger' value='بستن' onclick='closeModal()'/></td>" +
        "</tr>"
    tablebutt += "</table>"

    $(".modal-footer").empty();
    $(".modal-footer").append(tablebutt);

    $(".BodyModal").empty();
    $(".BodyModal").append(table);
    $("#MasterModal").modal();
}
async function UpdateBook(BookId) {
    var dsc = $("#MasterModal  textarea[name='BookName']").val()
    var obj = {}
    obj.url = "/Book/UpdateBook"
    obj.dataType = "json"
    obj.type = "post"
    obj.data = { BookId: BookId, dsc: dsc }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    GetBooks()
    showAlert("با موفقیت ویرایش شد")
    $("#MasterModal").modal("toggle");
}
async function inreaseRepeatedNumber(BookId) {
    var obj = {}
    obj.url = "/Book/inreaseRepeatedNumber"
    obj.dataType = "json"
    obj.type = "post"
    obj.data = { BookId: BookId }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    GetBooks()
}

