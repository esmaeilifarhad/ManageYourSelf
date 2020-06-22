$(document).ready(function(){
    GetData()
})
//------Update Post
$("#MasterModal .btnSave").on("click", function () {
    var NameOperator = $("#MasterModal .BodyModal div").attr("Name");
    if (NameOperator == "UpdateSport")
        UpdateSportPost();
});
//***************************************************

async function GetData(){
    
    var objListSportChk={}
    objListSportChk.url="/Sport/ListSportChk"
    objListSportChk.dataType="json"
    objListSportChk.type="post"
    //var res=await service(obj);
    var results = await Promise.all([
        service(objListSportChk)
    ]);
    var ListSportChk = results[0]
    showListSportChk(ListSportChk)
   
    

}
$(".ListSportChk").on("click", "input", function () {
   // ListSportFilter();
});
async function ListSportFilter(CatId) {
   
   // $(".saveData input[name='Tedad']").val();
    var obj={}
    obj.url="/Sport/ListSportFilter"
    obj.dataType="json"
    obj.type="post"
    obj.data={_CatId:CatId}
    //var res=await service(obj);
    var results = await Promise.all([
        service(obj)
    ]);
    
    var resListSportFilter = results[0]
    showListSportFilter(resListSportFilter,CatId)
    

}
$("body").on("click", ".SaveNewSport", function () {
    SaveNewSport();
});
async function SaveNewSport(CatId)
{
    $.LoadingOverlay("show");
    var Date = $("#bd-root-PersianDatePickerSport2 input[name='DateEnd']").val();
    Date=convertDateToslashless(Date)
    var Tedad = $(".saveData input[name='Tedad']").val();
    
    var obj={}
    obj.url="/Sport/CreateNewSport"
    obj.dataType="json"
    obj.type="post"
    obj.data={
        Date: Date,
        Tedad: Tedad,
        CatId: CatId
    }
    var results = await Promise.all([
        service(obj)
    ]);
    
    var res = results[0]
    ListSportFilter(CatId)

    $.LoadingOverlay("hide");
   
}

function intervalSport() {
    
   
    setInterval(function () {
      //  ListSportFilter();
    },300000);
}
//------------------------
function showListSportChk(ListSportChk){  
    var catData= ListSportChk.ListCat
    var countCol=3
    $.LoadingOverlay("show");
    var showRateTaskDays="<div style='font-size:11px'>"+
        "<input type='button' value='جدید' onclick='CreateMasterDataGet()'/>"+
        "<table class='table-bordered'>"  
    for (let index = 0; index < catData.length; index++) {
        
        if(index%countCol==0)
        {
            showRateTaskDays+="<tr><td><input onclick='ListSportFilter("+catData[index].CatId+")' type='radio' value="+catData[index].CatId+" name='rdbSport'></td><td>"+catData[index].Title+"</td><td><input type='button' value='ویرایش' onclick='EditMasterData("+catData[index].CatId+")'></td>"
        }
        else
        {  
            showRateTaskDays+="<td><input  onclick='ListSportFilter("+catData[index].CatId+")' type='radio' value="+catData[index].CatId+" name='rdbSport'></td><td>"+catData[index].Title+"</td><td><input type='button' value='ویرایش' onclick='EditMasterData("+catData[index].CatId+")'></td>"
        } 
        if(index%countCol==(countCol-1))
        {
            showRateTaskDays+="</tr>"
        }
    }
    showRateTaskDays+="</table></div>" 
    $(".ListSportChk").empty()
    $(".ListSportChk").append(showRateTaskDays)
    $.LoadingOverlay("hide"); 
   
}
function showListSportFilter(resListSportFilter,CatId){
$.LoadingOverlay("show");
var showRateTaskDays="<div style='font-size:11px'><table class='table-bordered'>"  
var oldDate=""
for (let index = 0; index < resListSportFilter.length; index++) {
    //فقط بار اول اجرا میشود
    if( oldDate=="")
    {
        showRateTaskDays+="<tr>"
        showRateTaskDays+="<td>"+resListSportFilter[index].Title+"</td>"+
          "<td>"+foramtDate(resListSportFilter[index].Date)+"   "+calDayOfWeek(resListSportFilter[index].Date)+"</td>"+
       
          "<td class='tedad' onclick='DeleteSport({Date:"+resListSportFilter[index].Date+",Title:\""+resListSportFilter[index].Title+"\",SportId:"+resListSportFilter[index].SportId+",CatId:"+resListSportFilter[index].CatId+",Tedad:"+resListSportFilter[index].Tedad+"})'>"+resListSportFilter[index].Tedad+"</td>"
      
    }
    if(resListSportFilter[index].Date!=oldDate && oldDate!="")
    {
        showRateTaskDays+="</tr><tr>"
        showRateTaskDays+="<td>"+resListSportFilter[index].Title+"</td>"+
           "<td>"+foramtDate(resListSportFilter[index].Date)+"   "+calDayOfWeek(resListSportFilter[index].Date)+"</td>"+
           "<td class='tedad' onclick='DeleteSport({Date:"+resListSportFilter[index].Date+",Title:\""+resListSportFilter[index].Title+"\",SportId:"+resListSportFilter[index].SportId+",CatId:"+resListSportFilter[index].CatId+",Tedad:"+resListSportFilter[index].Tedad+"})'>"+resListSportFilter[index].Tedad+"</td>" 
    }
    if(resListSportFilter[index].Date==oldDate && oldDate!="")
    {
        showRateTaskDays+= "<td class='tedad' onclick='DeleteSport({Date:"+resListSportFilter[index].Date+",Title:\""+resListSportFilter[index].Title+"\",SportId:"+resListSportFilter[index].SportId+",CatId:"+resListSportFilter[index].CatId+",Tedad:"+resListSportFilter[index].Tedad+"})'>"+resListSportFilter[index].Tedad+"</td>"
    }
    if(index==resListSportFilter.length-1)
    {
        showRateTaskDays+="</tr>"
    }
    oldDate=resListSportFilter[index].Date
}
showRateTaskDays+="</table></div>" 
$(".ListSportCatId").empty()

$(".ListSportCatId").append(showRateTaskDays)
$.LoadingOverlay("hide"); 
    
    ColorAvgMax(CatId)
colorSum()
findRotbeh()
}
function ListSport() {

    var urll = "/Sport/List";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListSport").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function ShowPivotSport() {

    var urll = "/Sport/ShowPivot";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ShowPivotSport").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function ShowPivotSportOrder() {

    var urll = "/Sport/ShowPivotOrder";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ShowPivotSportOrder").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function ShowPivotGroupingSets(CatId) {

    var urll = "/Sport/ShowPivotGroupingSets?CatId=" + CatId;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ShowPivotGroupingSets").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function CreateSportPost() {
    var Date = $("#bd-root-PersianDatePicker input[type=text]").val();
    var Tedad = $("#MasterModal input[name='Tedad']").val();
    var CatId = $("#MasterModal Select option:selected").val();
    var Set = $("#MasterModal input[name='Set']").val();

    var StrTedad = Tedad;
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Sport/Create?StrTedad=" + StrTedad,
               data: JSON.stringify({
                   Date: Date,
                   Tedad: Tedad,
                   CatId: CatId,
                   Set:Set
               }),
               success: function (result) {
                   if (result != "")
                       alert(result);
                   RefreshSport();
           },
           error: function (error) {
               console.log(error);
           }
       });
}
function CreateSportGet() {
 
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/Sport/Create",
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
function EditSport(Id) {
    $.ajax(
       {
           type: 'get',
           contentType: "application/json;charset=utf-8",
           dataType: "html",
           url: "/Sport/Edit?Id=" + Id,
           success: function (result) {
               $(".BodyModal").html(result);
               $("#MasterModal").modal();
           },
           error: function (error) {
               console.log(error);
           }
       });
}
function UpdateSportPost() {
    var CatId = $("#MasterModal div[name='UpdateMaterData'] table").attr("CatId");
    var Code = $("#MasterModal input[name='Code']").val();
    var Dsc = $("#MasterModal input[name='Dsc']").val();
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Sport/Update",
           data: JSON.stringify({
               Title: Title,
               Code: Code,
               Dsc: Dsc,
              CatId:CatId
           }),
           success: function (result) {
               RefreshSport();
           }
       });
}
function DeleteSport(obj) {
   
    var res = confirm("آیا حذف انجام شود؟"+"\n"+obj.Title+"\n تاریخ : "+foramtDate(obj.Date)+"\n تعداد : "+obj.Tedad);

    if (res == true) {
        $.LoadingOverlay("show"); 

        $.ajax(
           {
               type: 'POST',
               contentType: "application/json;charset=utf-8",
               dataType: "json",
               url: "/Sport/Delete",

               data: JSON.stringify({ Id: obj.SportId }),
               success: function (result) {
                   if (result == true) {
                       $.LoadingOverlay("hide"); 
                      // RefreshSport();
                       ListSportFilter(obj.CatId)
                   }
                  
                   else {
                       $.LoadingOverlay("hide"); 
                       alert("خطا در ثبت");
                   }
               }
           });
    }
       } 
function RefreshSport() {
    ListSport();
    ShowPivotSport();
    ShowPivotSportOrder();
    ShowPivotGroupingSets();
}

$(".ListSport").on("click", ".fa-edit", function () {
    var Id = $(this).attr("catid");
    EditSport(Id);
});
//----------
function ColorAvgMax(CatId)
{
    
    var MaxNum = 0;
    var MinNum = 1000000;
    var AvgNum = 0;
    var CountNum = 0;
    $(".ListSportCatId table tr .tedad").each(function () {
        var y = parseInt($(this).text());
        AvgNum = AvgNum + y
        CountNum = CountNum + 1
        if (y > MaxNum) {
            MaxNum = y;
            //this.setAttribute("style","color:blue")
        }
        if (y < MinNum) {
            MinNum = y;
            //this.setAttribute("style","color:blue")
        }

    })
    $(".ListSportCatId table tr .tedad").each(function () {
      
        var y = parseInt($(this).text());
        if (y > (AvgNum / CountNum)) {
            this.setAttribute("style", "color:green")
        }
        if (y < (AvgNum / CountNum)) {
            this.setAttribute("style", "color:red")
        }
        if (y == MaxNum) {
            //MaxNum=y;
            this.setAttribute("style", "color:blue")
        }
        if (y == MinNum) {
            //MaxNum=y;
            this.setAttribute("style", "color:gray")
        }

    })
    var sum=0
    $(".ListSportCatId table tr").each(function () {
        sum=0
        $(this).find(".tedad").each(function () {
            
            sum += parseInt($(this).text());
        })

        $(this).append("<td class='sum' style='font-size:13px;background-color:yellow'>"+sum+"</td>")
    })

    $(".ListSportChk .saveData").empty();
   
    var table = "<table class='table-bordered saveData'>"+
        "<tr style='text-align:center'><td style='color:blue'>بهترین</td><td>میانگین</td><td>بدترین</td></tr>"+
        "<tr style='text-align:center'><td>" + MaxNum + "</td><td>" + (AvgNum / CountNum).toFixed(1) + "</td><td>" + MinNum + "</td></tr>" +
   
          "<tr><td> <input type='number' value="+ (AvgNum / CountNum).toFixed(0)+" placeholder='تعداد' name='Tedad'></td>"+
          "<td><input type='text' name='DateEnd' class='PersianDatePickerSport2' value=" + todayShamsy() + " autocomplete='off'  ></td>" +
          "<td><input type='button' class='btn btn-danger' value='Save' style='color:forestgreen' onclick='SaveNewSport("+CatId+")'></td></tr>"+
        
        "<table>"
    $(".ListSportChk").append(table);
    $(".saveData input[name='Tedad']").focus()
    //$(".ListSportChk").append("<li>Prepended item</li>");
    kamaDatepicker('PersianDatePickerSport2', {
        nextButtonIcon: "../Scripts/Persian-Jalali-Calendar-Data-Picker-Plugin-With-jQuery-kamaDatepicker/demo/timeir_next.png"
                   , previousButtonIcon: "../Scripts/Persian-Jalali-Calendar-Data-Picker-Plugin-With-jQuery-kamaDatepicker/demo/timeir_prev.png"
                   , forceFarsiDigits: true
                   , markToday: true
                   , markHolidays: true
                   , highlightSelectedDay: true
                   , sync: true
    });

}
function colorSum(){
    var MaxNum = 0;
    var MinNum = 1000000;
    var AvgNum = 0;
    var CountNum = 0;
    $(".ListSportCatId table tr .sum").each(function () {
        var y = parseInt($(this).text());
        AvgNum = AvgNum + y
        CountNum = CountNum + 1
        if (y > MaxNum) {
            MaxNum = y;
            //this.setAttribute("style","color:blue")
        }
        if (y < MinNum) {
            MinNum = y;
            //this.setAttribute("style","color:blue")
        }

    })
    $(".ListSportCatId table tr .sum").each(function () {
      
        var y = parseInt($(this).text());
        if (y > (AvgNum / CountNum)) {
            this.setAttribute("style", "background-color:green;color:white")
        }
        if (y < (AvgNum / CountNum)) {
            this.setAttribute("style", "background-color:red;color:white")
        }
        if (y == MaxNum) {
            //MaxNum=y;
            this.setAttribute("style", "background-color:blue;color:white")
        }
        if (y == MinNum) {
            //MaxNum=y;
            this.setAttribute("style", "background-color:gray;color:white")
        }

    })
}
function findRotbeh()
{
    
    var i = 0
    //var arraySport = new Array();
    //arraySport
    arraySport = [];
    $(".tedad").each(function () {
        arraySport.push(parseInt($(this)[0].textContent));
        //console.log($(this)[0].textContent);
        //i = i + 1
    })
    //---------find rotbe
   // arraySport.sort();
   // arraySport.reverse();

    arraySport.sort(function (a, b) {
        return parseInt(b) - parseInt(a)  ;
    });

   // console.log(arraySport);
    $(".tedad").each(function () {
        for (i = 0; i < arraySport.length; ++i) {
            //console.log(arraySport[i]);
            if (arraySport[i] == $(this)[0].textContent) {
                $(this).append(" - <span style='color:black'>" + (i + 1) + "/" + arraySport.length + "</span>")
               // console.log(arraySport[i] + " - " + (i + 1) + "/" + arraySport.length)
                break;
            }
        }
    });

   

}

