//--Execute All List when click Tab
$("ul li a[href='#Sport']").on("click", function () {
    RefreshSport();
});
//------Create Post
$("#MasterModal .btnSave").on("click", function () {
    var NameOperator = $("#MasterModal .BodyModal div").attr("Name");

    if (NameOperator == "CreateSport")
        CreateSportPost();
});
//------Update Post
$("#MasterModal .btnSave").on("click", function () {
    var NameOperator = $("#MasterModal .BodyModal div").attr("Name");
    if (NameOperator == "UpdateSport")
        UpdateSportPost();
});
//***************************************************
//------------------------
$("ul li a[href='#MenuSportCrud']").on("click", function () {
    ListSportChk();
});
function ListSportChk() {
    var urll = "/Sport/ListSportChk";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (result) {
            if (result.result == false) {
                alert(result.message)
            }
            else {
                $(".ListSportChk").html(result);
            }

        },
        error: function (error) {
            alert(error);
        }
    })

}
$(".ListSportChk").on("click", "input", function () {
    ListSportFilter();
});
function ListSportFilter() {
    var CatId;
    $(".ListSportChk .Categories  input:checked").each(function () {
        CatId = $(this).val();
    });
   

    var urll = "/Sport/ListSportFilter?_CatId=" + CatId;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListSportCatId").html(data);
            ColorAvgMax();
            findRotbeh();
        },
        error: function (error) {
            alert(error);
        }
    })
}
$("body").on("click", ".SaveNewSport", function () {
    SaveNewSport();
});
function SaveNewSport()
{
    var Date = $("#bd-root-PersianDatePickerSport input[name='DateEnd']").val();
    var Tedad = $("#MenuSportCrud input[name='Tedad']").val();
    var CatId;
    $(".ListSportChk .Categories  input:checked").each(function () {
        CatId = $(this).val();
    });
    //--
    $.ajax(
      {
          type: 'POST',
          contentType: "application/json;charset=utf-8",
          dataType: "json",
          url: "/Sport/CreateNewSport",
          data: JSON.stringify({
              Date: Date,
              Tedad: Tedad,
              CatId: CatId
          }),
          success: function (result) {
              if (result != "")
              alert(result);
              ListSportFilter();
          },
          error: function (error) {
              alert(error);
          }
      });

}
$("body").on("click", ".DeleteSport", function () {
  
    var res = confirm("آیا حذف انجام شود؟");
    if (res == true) {
        var SportId = $(this).attr("SportId");
        DeleteSport(SportId);
    }
    ListSportFilter()
});
function intervalSport() {
    debugger;
   
    setInterval(function () {
        ListSportFilter();
    },300000);
}
//------------------------

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
            alert(error);
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
            alert(error);
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
            alert(error);
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
            alert(error);
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
               alert(error);
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
               alert(error);
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
               alert(error);
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
function DeleteSport(Id) {
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Sport/Delete",

           data: JSON.stringify({ Id: Id }),
           success: function (result) {
               if (result == true) {
                   RefreshSport();
               }
               else {
                   alert("خطا در ثبت");
               }
           }
       });
       } 
function RefreshSport() {
    ListSport();
    ShowPivotSport();
    ShowPivotSportOrder();
    ShowPivotGroupingSets();
}
//--------------------------Events
//--Create Get
$(".ListSport").on("click", "input[name='CreateSport']", function () {
    CreateSportGet();
});
//--Delete
$(".ListSport").on("click", ".fa-remove", function () {
    var res = confirm("آیا حذف انجام شود؟");
    if (res == true) {
        var Id = $(this).attr("catid");
        DeleteSport(Id);
    }
});
//--Edit
$(".ListSport").on("click", ".fa-edit", function () {
    var Id = $(this).attr("catid");
    EditSport(Id);
});
//----------
function ColorAvgMax()
{
    var MaxNum = 0;
    var MinNum = 1000000;
    var AvgNum = 0;
    var CountNum = 0;
    $(".SportPivot .DeleteSport").each(function () {
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
    $(".SportPivot .DeleteSport").each(function () {
      
        var y = parseInt($(this).text());
        if (y > (AvgNum / CountNum)) {
            this.setAttribute("style", "color:green")
        }
        if (y < (AvgNum / CountNum)) {
            this.setAttribute("style", "color:pink")
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
    // alert("Ave : " + (AvgNum / CountNum));
   // console.log($(this))
    $("#tt").remove();
    $(".ListSportChk").append("<P id='tt'>  میانگین :   " + (AvgNum / CountNum)+"</p>");
    //$(".ListSportChk").append("<li>Prepended item</li>");
}
function findRotbeh()
{
    var i = 0
    //var arraySport = new Array();
    //arraySport
    arraySport = [];
    $(".DeleteSport").each(function () {
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

    console.log(arraySport);
    $(".DeleteSport").each(function () {
        for (i = 0; i < arraySport.length; ++i) {
            //console.log(arraySport[i]);
            if (arraySport[i] == $(this)[0].textContent) {
                $(this).append("<span>" + (i + 1) + "/" + arraySport.length + "</span>")
               // console.log(arraySport[i] + " - " + (i + 1) + "/" + arraySport.length)
                break;
            }
        }
    });
}

