$(document).ready(function(){
    
    GetBook()
    setInterval(async function () {
        GetBook()
    }, 30000);
})
async function GetBook(){
    var obj={}
    obj.url="/Book/GetBook"
    obj.dataType="json"
    obj.type="post"
    //obj.data={TedadRooz:TedadRooz}
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    var inputs ="<input  style='color:black;cursor:pointer' class='smoke' type='button'  class='pointer' value='ایجاد' onclick='ShowBookForm()'/>"+
        "<input style='color:black;cursor:pointer' class='smoke' type='button'  class='pointer' value='ویرایش' onclick='EditBook(" + ListObj.BookId +")'/>"+
        "<input style='color:black;cursor:pointer' class='smoke' type='button'  class='pointer' value='حذف' onclick='DeleteBook("+ListObj.BookId+")'/>"
    $(".smokyText").empty()
    var res = ListObj.dsc.split(" ");
    var spanSmoke=""
    for(var i=0;i<res.length;i++)
    {
        spanSmoke+="<span class='smoke'>"+res[i]+"</span>"
       // console.log(res[i])
    }
    var DateTime = "<span  class='smoke'>" + foramtDate(ListObj.date) + "</span>" + "<span  class='smoke' style='color:yellow'>" + showDays(todayShamsy(),foramtDate(ListObj.date))+"</span>"+
        "<span  class='smoke'>" + foramtTime(ListObj.time) + "</span>"
    var RepeatedNumber = "<span class='smoke'><input type='checkbox' onclick='inreaseRepeatedNumber(" + ListObj.BookId+")'/></span>"+"<span  class='smoke'>  " + ListObj.RepeatedNumber + " - </span>"
    $(".smokyText").append(RepeatedNumber+spanSmoke + DateTime +inputs)
}
async function ShowBookForm(){
    
    var table="<table>"+
        "<tr><td><textarea name='BookName' rows='4' cols='55' autocomplete='off'></textarea></td></tr>"+
        "</table>"
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
async function CreateBook(){
    var dsc= $("#MasterModal table textarea[name='BookName']").val()
     $.LoadingOverlay("show");
                
    var obj={}
    obj.url="/Book/CreateBook"
    obj.dataType="json"
    obj.type = "post"
    var time = CurrentTime()
    var date = todayShamsy8char()
    obj.data = { dsc: dsc, time: time, date: date}
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    $("#MasterModal").modal("toggle");
    $.LoadingOverlay("hide");
}
async function DeleteBook(BookId){

    $.LoadingOverlay("show");
                
    var obj={}
    obj.url="/Book/DeleteBook"
    obj.dataType="json"
    obj.type="post"
    obj.data={BookId:BookId}
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
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
    var table = "<input type='checkbox' onclick='inreaseRepeatedNumber(" + ListObj.BookId +")'/> Readed<table>" +
        "<tr><td><textarea name='BookName' rows='4' cols='55' autocomplete='off'>" + ListObj.dsc+"</textarea></td></tr>" +
        "</table>"
    var tablebutt = "<tr>" +
        "<td><input type='button' class='btn btn-success' value='ویرایش' onclick='UpdateBook(" + ListObj.BookId+")'/> | " +
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
    var dsc = $("#MasterModal table textarea[name='BookName']").val()
    var obj = {}
    obj.url = "/Book/UpdateBook"
    obj.dataType = "json"
    obj.type = "post"
    obj.data = { BookId: BookId, dsc: dsc }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    showAlert("با موفقیت ویرایش شد")
    $("#MasterModal").modal("toggle");
}
async function inreaseRepeatedNumber(BookId) {
    var obj = {}
    obj.url = "/Book/inreaseRepeatedNumber"
    obj.dataType = "json"
    obj.type = "post"
    obj.data = { BookId: BookId}
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    
}
