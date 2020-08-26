//************************************Dictionary*****************************************************
var _wordId=0
//-------------execute List When Click on Tab
function MenuDicBaMesalPro(){
    ListDictionaryHamrahBaExamplePro();
    ShowLevel();
}

$("ul li a[href='#MenuDicBaMesalProUnSuccess']").on("click", function () {
    ListDictionaryHamrahBaExampleProUnSuccess();
    ShowLevel();
});
$("ul li a[href='#MenuDicPersianToEnglish']").on("click", function () {
    ListPersianToEnglish();
    ShowLevel();
});
$("ul li a[href='#home']").on("click", function () {
    ListDictionary();
});
$("ul li a[href='#MenuDicNRecords']").on("click", function () {
    //<a class="nav-link" data-toggle="tab" href="#MenuDicNRecords">ده تا</a>
    ShouldExecute();
});
$("ul li a[href='#MenuDicRandomWord']").on("click", function () {
    RandomWord();
    RandomWord_HardWord();
    RandomWord_HardWordExample();
});
//-------------------------------------
//Save new Example
$("#ModalNewExample .btnSave").on("click", function () {
    var Example = $("#ModalNewExample textarea").val();
    var wordId = $("#ModalNewExample .modal-body textarea").attr("wordid");
    CreateNewExamplePost(wordId, Example);
});
function CreateNewExamplePost(wordId, Example) {
    $.ajax({
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        url: "/Dictionary/CreateExample",
        data: JSON.stringify({ id_dic_tbl: wordId, example: Example }),
        success: function (result) {
            if (result == true) {
                //ListDictionaryHamrahBaExamplePro();
                RefreshListWithCheckedCheckbox();
            }
            else {
                alert("خطا در ثبت")
            }
        },
        error: function (error) {
            console.log(error);
        }
    })
}
//------Up Level
$("Body").on("click", ".LevelEngUp", function () {
    var Wordid = $(this).attr("data_id");
    LevelChangeUp(Wordid);
    $(this).parent().parent().css({ "display": "none" });
    //alert(Wordid);
    $("table tr .Examplecls").each(function () {
        var i = $(this).attr("data_id");
        if (i == Wordid)
            $(this).css({ "display": "none" });
    });
});
//------Down Level
$("Body").on("click", ".LevelEngDown", function () {
    // alert(1);
    var Wordid = $(this).attr("data_id");
    LevelChangeDown(Wordid);
    $(this).parent().parent().css({ "display": "none" });
    //alert(Wordid);
    $("table tr .Examplecls").each(function () {
        var i = $(this).attr("data_id");
        if (i == Wordid)
            $(this).css({ "display": "none" });
    });
});

//------CreateWord
$(".MyDictionary input[name='CreateWord']").on("click", function () {
    ShowCreateWord();
});
function ShowCreateWord() {
    var urll = "/Dictionary/CreateWord";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (result) {
            // var obj = data;
            //var obj = JSON.parse(data);
            $(".BodyModal").html(result);
            $("#MasterModal").modal();
        }
    })
}
//------SearchWord
$(".MyDictionary input[name='SerchWordList']").on("click", function () {
    SearchWordList();
});
function SearchWordList() {
    var urll = "/Dictionary/SearchWordList";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (result) {
            // var obj = data;
            //var obj = JSON.parse(data);
            $(".BodyModal").html(result);
            $("#MasterModal").modal();
        }
    })
}
//---------------SearchInDatabse
$("#SeachInDB").blur(function () {
    var str = $(this).val();
    ListDictionaryHamrahBaExamplePro(str);
});
function ListDictionaryHamrahBaExamplePro(str) {
    if (str == null) {
        str = "";
    }
    var urll = "/Dictionary/ListWordExampleDiv?str=" + str;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {

            $("#DicBaMesalPro").html(data);
            DateRefreshShow()
        },
        error: function (error) {
            alert('Dictionary/ListWordExampleDiv' + error);
        }
    })
}
//---------------SearchInExamples
$("#SeachInExample").blur(function () {
    
    var str = $(this).val();
    SearchInExamples(str);
    speakText();
});

function SearchInExamples(str) {
    if (str == null) {
        str = "";
    }
    var urll = "/Dictionary/SearchWordInExamples?str=" + str;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {

            $("#DicBaMesalPro").html(data);
            //CheckedInput();
            //RefreshListWithCheckedCheckbox();
        },
        error: function (error) {
            alert('Dictionary/SearchWordInExamples' + error);
        }
    })
}
//---------------SpeechText
$("#SpeechText").keyup(function () {
    var str = $(this).val();
    speakText(str);
});


$(".MyDictionary  td").dblclick(function () {
    var str = $(this).text();
    //speakText(str);
    TestSound(str);
});

function speakText(str) {
    $.ajax(
       {
           type: 'Post',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Dictionary/TTS?str=" + str,
           success: function (result) {

           },
           error: function (error) {
               console.log(error)
               // alert(error.message);
           }
       });
}

function MakeSound(thiss) {
    // 
    
    var str = $(thiss).parent().text();
    // var str = $(thiss).text();
    // speakText(str);
    TestSound(str);
    
    // TestSound(str)
}
function MakeSound2(thiss) {
    // 

    // var str = $(thiss).parent().text();
    var str = $(thiss).text();
    // speakText(str);
    TestSound(str);

    // TestSound(str)
}
function MakeSoundStr(txt) {
   
    TestSound(txt);
}

function MakeSoundExample(thiss) {
     
    //  console.log(thiss)
    // var str = $(thiss).parent().text();
    var str = ($(thiss).parent().parent().find(".ExampleSound")).text()
     
    //var str = $(thiss).text();
    // speakText(str);
    TestSound(str);

    // TestSound(str)
}

function TestSound(str)
{
    
    var x = varx = $("Body input[name='SpeedSpeach']").val();
    var y = varx = $("Body input[name='SoundSpeach']").val();

    text = str;
    var msg = new SpeechSynthesisUtterance();
    var voices = window.speechSynthesis.getVoices();
    // msg.voice = voices[$('#voices').val()];
    msg.rate = x/10;// $('#rate').val() / 10;
    msg.pitch = y/10;//$('#pitch').val();
    msg.text = text;

    msg.onend = function (e) {
        console.log('Finished in ' + event.elapsedTime + ' seconds.');
    };

    speechSynthesis.speak(msg);

}
//----------ListDictionary
$("input[name='ListDictionary']").on("click", function () {
    ListDictionary();
});
//------------Remove Word
$("body .MyDictionary").on("click", ".RemoveWord", function () {
    var WordId = $(this).attr("data_id");
    var result = confirm("آیا حذف انجام شود");
    if (result) {
        RemoveWord(WordId);
        // ListDictionary();
    }
});
//------------Archive Word
$("body .MyDictionary").on("click", ".ArchieveWord", function () {
    var WordId = $(this).attr("data_id");
    //if (confirm('Are you sure you want to save this thing into the database?')) {
    //    // Save it!
    //} else {
    //    // Do nothing!
    //}
    var result = confirm("آیا آرشیو انجام شود");
    if (result) {
        ArchieveWord(WordId,true);
        // ListDictionary();
    }
    else {
        ArchieveWord(WordId,false);
    }

});
//------------Edit Word
$("body .MyDictionary").on("click", ".EditWord ", function () {
    var WordId = $(this).attr("data_id");
    EditWord(WordId);
});
//------------Remove Example
$("body").on("click", ".RemoveExampleEng", function () {

    var r = confirm("آیا حذف انجام شود ؟!");
    if (r == true) {
        var ExampleId = $(this).attr("ExampleId");
        RemoveExamplePost(ExampleId);
    } else {
       
    }

   
});
//------------Remove Example fa-trash show modal
$("body .MyDictionary").on("click", ".fa-trash", function () {
    var WordId = $(this).attr("data_id");
    RemoveExample(WordId);
});
//-----------RemoveExample Post
$("#MasterModal").on("click", "table[nametbl='RemoveExample'] td .fa-eraser", function () {
    var ExampleId = $(this).attr("Example_id");
    RemoveExamplePost(ExampleId);
});
//---------------Ajax Show Example     
//$("#DivDictionary").on("click", ".btnExa", function () {
   
//    var _eng = $(this).closest("tr")   // Finds the closest row <tr>
//                 .find(".eng")     // Gets a descendent with class="nr"
//                 .text();
//    var _per = $(this).closest("tr")   // Finds the closest row <tr>
//                 .find(".per")     // Gets a descendent with class="nr"
//                 .text();
//    $("#HeaderExample").html("<span>" + _eng + "</span><span class='TranslateWord' style='display:none'>" + _per + "</span>");
//    var _idd = $(this).attr("data_id");
//    ShowExampleRefresh(_idd);
//});
//Create New Example   #DicBaMesalPro
$("Body").on("click", ".btnExa", function () {
    var WordId = $(this).attr("data_id");
    CreateNewExample(WordId)
});
//نمایش معنی لغت
$("#HeaderExample").on("click", "span", function () {
    $("span").css({ "display": "inline" });
});
//----New Example
$("#myModalExa").on("click", ".NewExample", function () {
    var WordId = $("#myModalExa #EditHolder").attr("WordId");
    CreateNewExample(WordId);
});
//----Edit Example
$("Body").on("click", ".ShowEditExample", function () {
    var ExampleId = $(this).attr("ExampleId");
    // alert(ExampleId);
    EditExample(ExampleId);
});
//-----------#DicBaMesalPro
$("Body").on("click", "table tr td", function () {
    $(this).find('span').css({ "display": "inline" });
});
//--------------------#DicBaMesalPro
$("Body").on("click", "table tr td .ShowExample", function () {
    var data_id = $(this).attr("data_id");
    $(".Examplecls").each(function () {
        var i = $(this).attr("data_id");
        if (data_id == i)
            $(this).removeAttr("style");
        $(this).css({ "white-space": "pre-line","direction":"ltr" });

    });
});
$("#DicBaMesal").on("click", ".PageNumberi", function () {
    var PageNumber = $(this).attr("PageNumber");
    ListDictionaryHamrahBaExample(PageNumber);
});
$("#myModalExa").on("click", ".fa-remove", function () {

    var ExampleId = $(this).attr("ExampleId");
    var WordId = $(this).attr("WordId");
    RemoveExa(ExampleId, WordId)
});
//---------------------Next Random Word
$(".btnNextRandomWord").on("click", function () {
    RandomWord();
});
//------------------ checked checkbox  
$("#MenuDicBaMesalPro .chkLevel").on("click", "input", function () {
    var MyArray = [];
    var lvl = '';
    $("#MenuDicBaMesalPro .chkLevel input:checked").each(function () {
        //  console.log($(this).val())
        lvl += $(this).val() + ",";
       
    });
    MyArray.push(lvl);
    ListWordExampleDivChk(MyArray);
});
//-------------------
function RefreshListWithCheckedCheckbox()
{
    var MyArray = [];
    var lvl = '';
    $("#MenuDicBaMesalPro .chkLevel input:checked").each(function () {
        lvl += $(this).val() + ",";
    });
    MyArray.push(lvl);
   
    ListWordExampleDivChk(MyArray);
    ListDictionaryHamrahBaExampleProUnSuccess();
    ListPersianToEnglish();
   
}
//-----------------
//Remove Example
function MainDictionary()
{
    var urll = "/Dictionary/MainDictionary";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        //dataType: "html",
        url: urll,
        success: function (data) {
            //$("#LessMoroor").html(data)
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function RemoveExa(ExampleId, WordId) {
    // alert(ExampleId)
    var urll = "/Dictionary/DeleteExample?ExampleId=" + ExampleId;
    $.ajax({
        type: "Post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        url: urll,
        success: function (data) {
            var obj = JSON.parse(data);
            if (obj == true) {
                alert("حذف با موفقیت انجام شد");

                ShowExampleRefresh(WordId);
                ListDictionary();
            }

        },
        error: function (error) {
            console.log(error);
        }
    })
}
function ShowExampleRefresh(WordId) {
    // var _idd = $(this).attr("data_id");
    var urll = "/Dictionary/ListExample?id=" + WordId;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        url: urll,
        success: function (data) {
            if (data != "[]") {
                var obj = JSON.parse(data);
                // $("#HeaderExample").text(obj[0].Word)
                var HtmlTbl = '<Table style="direction: ltr;">'
                HtmlTbl += '<tr style="border-style: ridge;">'
                HtmlTbl += '<th style="direction: ltr;text-align: left; Padding:10px; border-style: solid;">مثال</th>'
                HtmlTbl += '<th style="direction: ltr;text-align: left; Padding:10px; border-style: solid;">حذف</th>'
                HtmlTbl += '</tr>'
                for (var i = 0; i < obj.lstExample.length; i++) {
                    HtmlTbl += '<tr style="border-style: ridge;">'
                    HtmlTbl += '<td style="Padding:10px; border-style: solid;text-align: left;" Example-Id="' + obj.lstExample[i].id + '">'
                    HtmlTbl += obj.lstExample[i].example
                    HtmlTbl += '</td>'
                    HtmlTbl += '<td style="text-align:center;"><span  class="fa fa-remove" style="color:red" ExampleId=' + obj.lstExample[i].id + ' WordId=' + WordId + '></span></td>'
                    HtmlTbl += '</tr>'
                }
                HtmlTbl += '</Table>'
                $("#EditHolder").html(HtmlTbl);
                $("#myModalExa #EditHolder").attr("WordId", WordId);
                $("#myModalExa").modal();
            }
            else {

                $("#EditHolder").html("<p>مثالی وجود ندارد</p>");
                $("#myModalExa #EditHolder").attr("WordId", WordId)
                $("#myModalExa").modal();
            }
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function ListExampleWord(WordId) {
    return new Promise(resolve => {
        var urll = "/Dictionary/ListExample?id=" + WordId;
        $.ajax({
            type: "Get",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            url: urll,
            success: function (data) {
                resolve(JSON.parse(data))
            },
            error: function (error) {
                console.log(error);
            }
        })
    })
}
function EditWord(WordId) {
    var Word=""
    var urll = "/Dictionary/EditWord?WordId=" + WordId;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (result) {
            // var obj = data;
            //var obj = JSON.parse(data);
            $(".BodyModal").html(result);
            $("#MasterModal").modal();

        }
    })
}
function EditExample(ExampleId,wordId) {
    
    _wordId=wordId
    var Word = ""
    var urll = "/Dictionary/EditExample?ExampleId=" + ExampleId;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (result) {
            $(".BodyModal").html(result);
            $("#MasterModal").modal();

        }
    })
}
function RemoveExample(WordId) {
    var Word = ""
    var urll = "/Dictionary/RemoveExample?WordId=" + WordId;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (result) {
            // var obj = data;
            //var obj = JSON.parse(data);
            $(".BodyModal").html(result);
            $("#MasterModal").modal();

        }
    })
}
function RemoveExamplePost(ExampleId) {
    var urll = "/Dictionary/RemoveExamplePost?ExampleId=" + ExampleId;
    $.ajax({
        type: "post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        url: urll,
        success: function (result) {
            alert(result);
            // $('#MasterModal').modal('toggle');
            RefreshListWithCheckedCheckbox();
        },
        error(result) {
            alert(result);
        }
    })
}
function LevelChangeDown(wordId) {
    return new Promise(resolve => {
        var urll = "/Dictionary/LevelChangeDown?WordId=" + wordId
        $.ajax({
            type: 'POST',
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            url: urll,
            success: function (res) {
                RefreshListWithCheckedCheckbox();
                ShouldExecute();
                resolve(res)
            },
            error: function (res) {
            }
        })
    })
}
function LevelChangeUp(wordId) {
    var urll = "/Dictionary/LevelChangeUp?WordId=" + wordId
    $.ajax({
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        url: urll,
        success: function (res) {
            RefreshListWithCheckedCheckbox();
            ShouldExecute();
           
        },
        error: function (res) {
        }
    })
}
function ShowLevel() {
    var urll = "/Dictionary/ShowLevel";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $("#ShowLevel").html(data);
            $(".ColShowLevel").html(data);
            //$("#MenuDicRandomWord #ShowLevel br").remove()
            //$("#MenuDicRandomWord #ShowLevel div").remove()
            //$("#MenuDicRandomWord #ShowLevel").append(data)

            //$("#ShowLevel").html(data);
            
            GetlLevelByJqueryAndAppend();
        },
        error: function (error) {
            console.log("ShowLevel : ");
            console.log( error);
        }
    })
}
function BadGheleghtarinWord() {
    var urll = "/Dictionary/BadGheleghtarinWord";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $("#BadGheleghtarinWord").html(data)
            //var obj = JSON.parse(data);
            //var obj = data;
            // $("#HazineMohem").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function LessMoroor() {
    var urll = "/Dictionary/LessMoroor";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $("#LessMoroor").html(data)
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function Top10LastMoroor() {
    var urll = "/Dictionary/Top10LastMoroor";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $("#Top10LastMoroor").html(data)
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function Top10MaxGroupBy() {
   
    var urll = "/Dictionary/Top10MaxGroupBy";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $("#Top10MaxGroupBy").html(data)
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function RandomWord() {

    var urll = "/Dictionary/RandomWord";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".DicRandomWord").html(data)
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function RandomWord_HardWord() {
    var urll = "/Dictionary/RandomWord_HardWord";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".DicRandomWord_HardWord").html(data)
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function RandomWord_HardWordExample() {
    var urll = "/Dictionary/RandomWord_HardWordExample";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".DicRandomWord_HardWordExample").html(data)
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function RandomWordWithSound() {
    ClearExamplesTable()
    var res= $("input[name='TestCorretWord']:checked").val()

    var request = { str: res }
    $.ajax(
      {
          type: 'Post',
          contentType: "application/json;charset=utf-8",
          url: "/Dictionary/RandomWordWithSound",
          dataType: "html",
          data: JSON.stringify(request),
          success: function (result) {
              
              $("#RandomWordWithSound table").remove()
              $("#RandomWordWithSound").append(result)
              
              
          },
          error: function (error) {
              console.log(error)
          }
      });
    
}

function CreateWord() {
    var eng = $("#MasterModal .table input[name='DicEng']").val()
    var per = $("#MasterModal .table input[name='DicFar']").val()
    var Phonetic = $("#MasterModal .table input[name='DicPho']").val()
    $.ajax(
       {
           type: 'Post',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Dictionary/CreateWord",


           data: '{"eng": "' + eng + '", "per" : "' + per + '","Phonetic":"' + Phonetic + '"}',

           //data: {
           //    eng: $(".table input[name='DicEng']").val(),
           //    per: $(".table input[name='DicFar']").val(),
           //    Phonetic: $(".table input[name='DicPho']").val()
           //},
           success: function (result) {
               if (result == true) {
                   $("#ShowMessage").text('ثبت شد');
                   //ListDictionary();
                   // ShouldExecute();
               }
               else {
                   $("#ShowMessage").text('خطا در ثبت');
               }
           },
           error: function (error) {
               console.log(error)
           }
       });
}
function UpdateWord() {
    var eng = $("#MasterModal table input[name='eng']").val()
    var per = $("#MasterModal table textarea[name='per']").val()

    //var DateStart = $("#MasterModal table input[name='DateStart']").val()
    //var IsActive = $("input[name='TaskIsActive']").prop('checked')
    //var IsCheck = $("input[name='TaskIsCheck']").prop('checked')
    var Word_id = $("#MasterModal table").attr("Word_id")

    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Dictionary/UpdateWord",
           data: JSON.stringify({ id: Word_id, eng: eng, per: per }),
           success: function (result) {
               if (result == true) {
                   // $("#ShowMessage").text('ثبت شد');
                   // ListDictionary();
                   //  ListTask("anjamnashode");
                   RefreshListWithCheckedCheckbox();
               }
               else {
                   $("#ShowMessage").text('خطا در ثبت');
               }
           },
           error: function (error) {
               console.log(error);
           }
       });
}
function UpdateExample() {
    
    var Example = $("#MasterModal table textarea[name='Example']").val()
    var ExampleId = $("#MasterModal table").attr("ExampleId")
    $.ajax(
       {
           type: 'POST',
           contentType: "application/json;charset=utf-8",
           dataType: "json",
           url: "/Dictionary/UpdateExample",
           data: JSON.stringify({ id: ExampleId, example: Example }),
           success: function (result) {
               if (result.result == true) {
                   RefreshListWithCheckedCheckbox();
                   ShowExamples(_wordId)
               }
               else {
                   alert(result.message);
               }
           },
           error: function (error) {
               alert(error.message);
           }
       });
}
function ListDictionary() {
    var urll = "/Dictionary/ListDictionary";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        url: urll,
        success: function (data) {

            //var obj = JSON.parse(data);
            var obj = data;
            var html = "<table class='table table-responsive table-dark table-striped tblDictionary' style='direction:rtl; text-align: center;font-size:11px'>\
            <thead>\
                <tr>\
                    <th>Up</th>\
                    <th>Down</th>\
                    <th>انگلیسی</th>\
                    <th>فارسی</th>\
                    <th>تاریخ رفرش</th>\
                    <th>سطح</th>\
                    <th>موفق</th>\
                    <th>ناموفق</th>\
                    <th>ویرایش</th>\
                    <th>حذف</th>\
                    <th>مثال</th>\
                </tr>\
            </thead>\
            <tbody class='ListDictionarySearch'>"
            for (var i = 0; i < obj.length; i++) {
                html += "<tr>"
                html += "<td><span style='color:red' class='fa fa-sort-up LevelEngUp' data_id=" + obj[i].id + "></span></td><td><span style='color:green' class='fa fa-sort-down LevelEngDown' data_id=" + obj[i].id + "></span></td>"
                html += "<td class='eng'>" + obj[i].eng + "</td><td  class='per'>" + obj[i].per + "</td><td>" + obj[i].date_refresh + "</td><td>" + obj[i].level + "</td>"
                html += "<td>" + obj[i].SuccessCount + "</td><td>" + obj[i].UnSuccessCount + "</td>"
                html += "<td><span class='fa fa-edit' data_id=" + obj[i].id + "></span></td>"
                html += "<td><span class='fa fa-remove RemoveWord' data_id=" + obj[i].id + "></span></td>"
                if (obj[i].HasExample > 0)
                    html += "<td><span style='color:green' class='fa fa-list-alt btnExa' data_id=" + obj[i].id + "></span></td>"
                else
                    html += "<td><span style='color:red' class='fa fa-list-alt btnExa' data_id=" + obj[i].id + "></span></td>"
                html += " </tr>"
            }
            html += "</tbody> </table>"
            $("#DivDictionary").html(html);
            //CountLevel();
        },
        error: function (error) {
            alert('Dictionary/ListDictionary : ' + error);
        }
    })
}
function ListDictionaryHamrahBaExample(SkipN) {
    var urll = "/Dictionary/ListDictionaryHamrahBaExample?SkipN=" + SkipN;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $("#DicBaMesal").html(data);
            //CountLevel();
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function ListDictionaryHamrahBaExampleProUnSuccess() {
    var urll = "/Dictionary/ListWordExampleSucc_OR_UnSucc";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {

            $("#DicBaMesalProUnSuccess").html(data);
            //  CheckedInput();
        },
        error: function (error) {
            alert('Dictionary/ListWordExampleSucc_OR_UnSucc' + error);
        }
    })
}
function ListPersianToEnglish() {
    var urll = "/Dictionary/ListPersianToEnglish";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {

            $("#DicPersianToEnglish").html(data);
            //  CheckedInput();
        },
        error: function (error) {
            alert('Dictionary/ListPersianToEnglish' + error);
        }
    })
}
function CheckedInput() {

    var levelval = 0;
    var urll = "/Dictionary/MaxLevelCount";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        url: urll,
        success: function (data) {
            
            levelval = data;
            $("#MenuDicBaMesalPro .chkLevel input").each(function () {
                if (levelval == $(this).val()) {
                    this.setAttribute("checked", "checked");
                    this.checked = true;
                }
            });
            // alert(data);
        },
        error: function (error) {
            console.log(error);
        }
    });  
}
function ListWordExampleDivChk(MyArray)
{
    
    $.ajax(
     {
         type: 'Post',
         data: JSON.stringify({ MyData: MyArray }),
         contentType: "application/json;charset=utf-8",
         dataType: "html",
         url: "/Dictionary/ListWordExampleDivChk",
         success: function (data) {

             $("#DicBaMesalPro").html(data);
             DateRefreshShow()
       
         },
         error: function (error) {
             console.log(error);
         }
     });
}
function RemoveWord(WordId) {
    var urll = "/Dictionary/DeleteWord?id=" + WordId;
    $.ajax({
        type: "Post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        url: urll,
        success: function (data) {
            var obj = JSON.parse(data);
            if (obj == true) {
                alert("حذف با موفقیت انجام شد");
                //ListDictionary();
                // ShowExampleRefresh(WordId);
                RefreshListWithCheckedCheckbox();
            }

        },
        error: function (error) {
            console.log(error);
        }
    })

}
function ArchieveWord(WordId, res) {
    var urll
    if (res == true) {
        urll = "/Dictionary/ArchieveWord?id=" + WordId+"&res="+res;
    }
    else {
        urll = "/Dictionary/ArchieveWord?id=" + WordId + "&res=" + res;
    }
    $.ajax({
        type: "Post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        url: urll,
        success: function (data) {
            var obj = JSON.parse(data);
            if (obj == true) {
                // alert("آرشیو با موفقیت انجام شد");
                RefreshListWithCheckedCheckbox();
                ShouldExecute();
            }
            else {
                alert("برای آرشیو شدن حتما باید سطح لغت یک باشد");
            }

        },
        error: function (error) {
            console.log(error);
        }
    })

}
function CreateNewExample(WordId) {
  
    var urll = "/Dictionary/NewExample";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            // var obj = data;
            //var obj = JSON.parse(data);
            $("#ModalNewExample .modal-body").html(data);
            $("#ModalNewExample .modal-body textarea").attr("wordid", WordId)
            $("#ModalNewExample").modal();

            //--------
        }
    })


}
function ShouldExecute() {
   
    ShowLevel();
    //BadGheleghtarinWord();
    //LessMoroor();
    //Top10LastMoroor();
    //Top10MaxGroupBy();
    //ListDictionaryHamrahBaExamplePro();
}
function GetlLevelByJqueryAndAppend() {
    
    $("#MenuDicBaMesalPro .chkLevel div .lvlchk").remove();
    $("#MenuDicRandomWord .chkLevel div .lvlchk").remove();
    
    var i = 0
    var lvl = 1
    $(".ColShowLevel table tr td").each(function () {
        if (i % 2 == 0) {
            lvl = $(this).text()
            //console.log(z)

        }
        else {
            //console.log(i)
            var res = $(this).text()
            $("#MenuDicBaMesalPro .chkLevel div input[name='Level" + lvl + "'] ").after("<span class='lvlchk' style='color:red'>" + res + "</span>")
            
            $("#MenuDicRandomWord .chkLevel div .level" + lvl + " ").after("<span class='lvlchk' style='color:red'>" + res + "</span>")
        }
        i = i + 1
    })

}
function ShowAndHiddenExample(id,eng) {
   
    
    //-----------
    var res = $(".examples_" + id).attr("hidden");
    if (res == "hidden") {
        $(".examples_" + id).attr("hidden", false);
    }
    else {
        $(".examples_" + id).attr("hidden", true);
    }

    
    var eng = eng.toLowerCase();
    $(".examples_" + id +" .ExampleSound").each(function () {

        $(this).html($(this).html().replace(
            new RegExp(eng, 'g'), '<span style="color:red">' + eng + '</span>'
        ));
    });
}
function ShowAndHiddenPersian(id) {
    var eng=$(".per_" + id).parent().prev().text()
    TestSound(eng)
    var res = $(".per_" + id).attr("hidden");
    if (res == "hidden") {
        $(".per_" + id).attr("hidden", false);
    }
    else {
        $(".per_" + id).attr("hidden", true);
    }


}
function ClearExamplesTable(){
    $("#Showexamples table").remove()
}
async function RandomWordWithSoundCheckIsTrue(param) {
   
    if (param.check == true) {
        $("#FindCorretWord input").each(function () {
            if ($(this).attr("IsTrue") == "true") {
                $(this).parent().parent().css("background-color", "#06f976")
            }
        })

        var x=await LevelChangeDown(param.wordId);

        RandomWordWithSound();
        ShowLevel();
    }
    else {
        LevelChangeUp(param.wordId);
       
        $("#FindCorretWord input").each(function () {
            if ($(this).attr("IsTrue") == "true")
            {
                $(this).parent().parent().css("background-color", "#06f976")
            }
        })

    }
   
   
}
async function ShowExamples(wordId){
    _wordId=wordId
    var x=await  ListExampleWord(wordId)
    var table="<table class='table'>"
    for (let index = 0; index <  x.lstExample.length; index++) {
        table+="<tr>"+
             //"<td><input type='button' value='edit' onclick='EditExample("+x.lstExample[index].id+")'/>"+
             //"</td>"+
            "<td style='white-space: pre-line;direction: ltr; text-align: left;'>"+x.lstExample[index].example+"</br>"+
            "<input type='button' value='edit' onclick='EditExample("+x.lstExample[index].id+","+wordId+")'/></td>"+
            "</tr>"
    }
    table+="</table>"
    $("#Showexamples table").remove()
    $("#Showexamples").append(table)

}
function showTheWord(eng) {
    $("#showTheWord span").remove()
    $("#showTheWord").append("<span>"+eng+"</span>")
}
function DateRefreshShow(){
    $("#DicBaMesalPro table tr .dateCol").each(function () {

        var date=$(this).text().trim()

        
        $(this).empty()
        $(this).append("<span>"+calDayOfWeek(date)+" - </span><span>"+foramtDate(date)+"</span><span> - "+showDays(todayShamsy(), foramtDate(date))+"</span>")
    })
}

$(document).ready(function () {
    
    //----------function ListDictionary()
    $("#SeachInTblDicList").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $(".ListDictionarySearch tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
    //--------------------------
    $("#myInputDic").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});



