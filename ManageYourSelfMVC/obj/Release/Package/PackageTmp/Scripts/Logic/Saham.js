$(document).ready(function(){
   
    var SName=  getCookie("SName")
    $("#Moshakhasat").empty()
    $("#Moshakhasat").append("<p><span>خوش آمدید : </span><span>"+SName+"</span><span onclick='removeCookie()' style='color:red;cursor:pointer'> خروج </span></p>")
 
})
function removeCookie(){
    document.cookie = "SPassword=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    document.cookie = "SName=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    document.cookie = "SUsername=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    CheckLocalStorage()
}
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for(var i = 0; i <ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
function setCookie(cname,cvalue,exdays) {
    
    var d = new Date();
    d.setTime(d.getTime() + (exdays*24*60*60*1000));
    var expires = "expires=" + d.toGMTString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
     
}

async function CheckLocalStorage(){
    
    //Read Form Cookie
    var Password=  getCookie("SPassword")
    var UserName=  getCookie("SUsername")
    //--read form local storage
    //var UserName = localStorage.getItem("SUserName");
    //var Password= localStorage.getItem("SPassword");
    
   // setCookie("username", user, 30);

    if(UserName=="" || Password=="")
    {
        AthenticationForm()
        var SName=  getCookie("SName")
        $("#Moshakhasat").empty()
        $("#Moshakhasat").append("<p><span>خوش آمدید : </span><span>"+SName+"</span><span onclick='removeCookie()' style='color:red;cursor:pointer'> خروج </span></p>")
 
    }
    else
    {
        GetCompareToAvg('Rate')

        var SName=  getCookie("SName")
        $("#Moshakhasat").empty()
        $("#Moshakhasat").append("<p><span>خوش آمدید : </span><span>"+SName+"</span><span onclick='removeCookie()' style='color:red;cursor:pointer'> خروج </span></p>")
 
       
    }
    
   
}
async function AthenticationForm(){
    $.LoadingOverlay("show");
    var table = "<table class='table table-bordered table-responsive'>"+
        "<tr><td>نام کاربری</td><td><input type='text' name='SUserName'/></td></tr>"+
          "<tr><td>پسورد</td><td><input type='password' name='SPassword'/></td></tr>"+
          "</table>"
          

    var tablebutt = "<table class='table' style='font-size: 9px;'>"
    tablebutt += "<tr>" +
        "<td><input type='button' class='btn btn-success' value='ذخیره' onclick='SaveAthenticate()'/> | " +
        "<input type='button'  class='btn btn-danger' value='بستن' onclick='closeModal()'/></td>" +
        "</tr>"
    tablebutt += "</table>"
       
    $(".modal-footer").empty();
    $(".modal-footer").append(tablebutt);

    $(".BodyModal").empty();
    $(".BodyModal").append(table);
    $("#MasterModal").modal();

    $.LoadingOverlay("hide");
}
async function SaveAthenticate(){
    
    var Username=$("table input[name='SUserName']").val();
    var Password=$("table input[name='SPassword']").val();
    //Fill Cookie
    setCookie("SUsername",Username, 30);
    setCookie("SPassword",Password, 30);

    //localStorage.setItem("SUserName", Username);
    //localStorage.setItem("SPassword", Password);
    $("#MasterModal").modal("toggle");
    
}
async function GetLastPositiveAlMinus(TedadRooz) {
    
    $.LoadingOverlay("show");

    var obj={}
    obj.url="/Saham/LastPositiveAlMinus"
    obj.dataType="json"
    obj.type="post"
    obj.data={TedadRooz:TedadRooz}
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
   // console.log(ListObj)
    
    ShowLastPositiveAlMinus(ListObj)
   
    $.LoadingOverlay("hide");
}

async function GetCompareToAvg(SortBy) {
    //var Username = localStorage.getItem("SUserName");
    //var Password= localStorage.getItem("SPassword");

    //Read Form Cookie
    //var Password=  getCookie("SPassword")
    //var Username=  getCookie("SUsername")
    //
    //setCookie("SUsername",Username, 30);
    //setCookie("SPassword",Password, 30);
    $.LoadingOverlay("show");

    const m = moment();
    var today = m.format('jYYYY/jM/jD');//Today
    today=convertDateToslashless(today)
    m.add(-1, 'day')
    var yesterday=m.format('jYYYY/jM/jD');//yesterday
    yesterday=convertDateToslashless(yesterday)

    var obj={}
    obj.url="/Saham/CompareToAvg"
    obj.dataType="json"
    obj.type="post"
    obj.data={SortBy:SortBy,today:today,yesterday:yesterday}
    
    var results = await Promise.all([
        service(obj)
    ]);
    
    if( results[0].message!=undefined)
    {
        $.LoadingOverlay("hide");
        alert(results[0].message)
      
        return
    }
    var ListObj = results[0]
    
   // console.log(ListObj)
    ShowCompareToAvg(ListObj)

    $.LoadingOverlay("hide");
}

async    function ShowLastPositiveAlMinus(ListObj)
{
    
    var table = "<table class='table-bordered table-responsive'>"+
      "<tr><th>نماد</th><th>تعداد</th><th>تعداد مثبت</th><th>تعداد منفی</th><th>مجموع درصد ها</th><th>Rate</th><th>رهاورد</th><th>جزئیات</th></tr>"
    for (var i = 0; i <  ListObj.lstNamadVM.length; i++) {
        table += "<tr><td>"+ ListObj.lstNamadVM[i].NamadName+"</td>"+
              "<td>"+ ListObj.lstNamadVM[i].Tedad+"</td>"+
               "<td>"+ ListObj.lstNamadVM[i].TedadP+"</td>"+
                "<td>"+ ListObj.lstNamadVM[i].TedadM+"</td>"+
           "<td>"+ ListObj.lstNamadVM[i].SumDarsadGheymatPayany+"</td>"+
        "<td>"+ ListObj.lstNamadVM[i].Rate+"</td>"
        if(ListObj.lstNamadVM[i].IdRahavard=="")
        {
            table +=  "<td><a href="+ListObj.lstNamadVM[i].IdRahavard+" target='_blank'>تعریف نشده</a></td>"
        }
        else
        {
            table +=  "<td><a href="+"https://rahavard365.com/asset/"+ListObj.lstNamadVM[i].IdRahavard+" target='_blank'>رهاورد</a></td>"
        }
        table += "<td><input type='button' value='جزئیات' onclick='NamadDetail("+ListObj.lstNamadVM[i].IdNamad+")'/></td>"+
       "</tr>"

    }
    table +="</table>"
 
    $(".LastPositiveAlMinus").empty();
    $(".LastPositiveAlMinus").append(table);

}

async    function ShowCompareToAvg(ListObj)
{
    var table = "<table class='table-bordered table-responsive' style='font-size:10px'>"+
      "<tr><th>نماد</th><th>تاریخ</th><th>آخرین حجم</th><th>میانگین حجم</th><th onclick='GetCompareToAvg(\""+"Rate"+"\")'>Rate</th>"+
      "<th onclick='GetCompareToAvg(\""+"SumDarsadGheymatPayany"+"\")'>مجموع</th><th onclick='GetCompareToAvg(\""+"TedadP"+"\")'>تعداد مثبت</th>"+
      "<th onclick='GetCompareToAvg(\""+"TedadM"+"\")'>تعداد منفی</th><th>جزئیات</th><th>tse</th><th>رهاورد</th><th>tse</th></tr>"
    for (var i = 0; i <  ListObj.lstNamadVM.length; i++) {
        table += "<tr><td style='cursor:pointer' onclick='AddtseAddress("+ListObj.lstNamadVM[i].IdNamad+")'>"+ ListObj.lstNamadVM[i].NamadName+"</td>"+
           " <td>"+ foramtDate(ListObj.lstNamadVM[i].ShamsyDate)+"</td>"+
           "<td>"+ SeparateThreeDigits(ListObj.lstNamadVM[i].Hajm)+"</td>"+          
           "<td>"+ SeparateThreeDigits(ListObj.lstNamadVM[i].Avgg)+"</td>"+
            "<td>"+ ListObj.lstNamadVM[i].Rate+"</td>"+
             "<td>"+ ListObj.lstNamadVM[i].SumDarsadGheymatPayany+"</td>"+
              "<td>"+ ListObj.lstNamadVM[i].TedadP+"</td>"+
               "<td>"+ ListObj.lstNamadVM[i].TedadM+"</td>"+
            "<td><input type='button' value='جزئیات' onclick='NamadDetail("+ListObj.lstNamadVM[i].IdNamad+")'/></td>"
        
        if(ListObj.lstNamadVM[i].tseAddress=="")
        {
            table +=  "<td><input style='background-color:red' type='button' value='tse' onclick='AddtseAddress("+ListObj.lstNamadVM[i].IdNamad+")'/></td>"
        }
        else
        {
            table +=  "<td><a href="+ListObj.lstNamadVM[i].tseAddress+" target='_blank'>نمایش</a></td>"
        }

        if(ListObj.lstNamadVM[i].IdRahavard=="")
        {
            table +=  "<td><a href="+ListObj.lstNamadVM[i].IdRahavard+" target='_blank'>تعریف نشده</a></td>"
        }
        else
        {
            table +=  "<td><a href="+"https://rahavard365.com/asset/"+ListObj.lstNamadVM[i].IdRahavard+" target='_blank'>رهاورد</a></td>"
        }
        if(ListObj.lstNamadVM[i].tseId=="")
        {
            table +=  "<td><a href="+ListObj.lstNamadVM[i].tseId+" target='_blank'>تعریف نشده</a></td>"
        }
        else
        {
            table +=  "<td><a href="+"https://tse.ir/instrument/"+ListObj.lstNamadVM[i].namadNameTse+"_"+ListObj.lstNamadVM[i].tseId+".html"+" target='_blank'>tse</a></td>"
        }
        table += "</tr>"

    }
    table +="</table>"
 
    $(".CompareToAvg").empty();
    $(".CompareToAvg").append(table);
    // $("#MasterModal").modal();

}
async    function NamadDetail(IdNamad)
{
    $.LoadingOverlay("show");

    var obj={}
    obj.url="/Saham/NamadDetail"
    obj.dataType="json"
    obj.type="post"
    obj.data={NamadId:IdNamad}
    
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    //console.log(ListObj)
    // ShowCompareToAvg(ListObj)

   
    var sum=0
    var sumHajm=0
    var count=0
    var table = "<table class='table table-bordered table-responsive'>"+
      "<tr><th>نماد</th><th>تاریخ</th><th>Hajm</th><th>تعداد معاملات</th><th>آخرین قیمت</th></tr>"
    for (var i = 0; i <  ListObj.lstNamadVM.length; i++) {
        count+=1
        sumHajm+=ListObj.lstNamadVM[i].Hajm
        sum+= ListObj.lstNamadVM[i].DarsadGheymatPayany
        table += "<tr><td>"+ ListObj.lstNamadVM[i].NamadName+"</td>"+
           " <td>"+ foramtDate(ListObj.lstNamadVM[i].ShamsyDate)+"</td>"+
           "<td>"+ SeparateThreeDigits(ListObj.lstNamadVM[i].Hajm)+"</td>"+
           "<td>"+ SeparateThreeDigits(ListObj.lstNamadVM[i].TedadMoamelat)+"</td>"+
           "<td>"+ ListObj.lstNamadVM[i].DarsadGheymatPayany+"</td>"+
           "</tr>"
          

    }
    table += "<tr style='font-size:11px'>"+
         "<td colspan='2'>میانگین</td>"+
           "<td>"+SeparateThreeDigits((sumHajm/count).toFixed(0))+"</td>"+
          "<td>مجموع</td>"+
           "<td>"+ sum.toFixed(1)+"</td>"+
          "</tr>"
    table +="</table>"
 
   // $(".NamadDetail").empty();
  //  $(".NamadDetail").append(table);

    $(".BodyModal").empty();
    $(".BodyModal").append(table);
    $("#MasterModal").modal();

    $.LoadingOverlay("hide");
    // $("#MasterModal").modal();

}
async function AddtseAddress(IdNamad){
    var table = "<table class='table-bordered table-responsive'>"+

     "<tr><td>Address : </td><td><input style='width:400px' type='text' name='tseAdrs'/></td>"+
           "</tr></table>"

    var tablebutt = "<table class='table' style='font-size: 9px;'>"
    tablebutt += "<tr>" +
        "<td><input type='button' class='btn btn-success' value='ذخیره' onclick='SaveTseAdrs("+IdNamad+")'/> | " +
        "<input type='button'  class='btn btn-danger' value='بستن' onclick='closeModal()'/></td>" +
        "</tr>"
    tablebutt += "</table>"
       
    $(".modal-footer").empty();
    $(".modal-footer").append(tablebutt);
    $(".BodyModal").empty();
    $(".BodyModal").append(table);
    $("#MasterModal").modal();
}
async function SaveTseAdrs(IdNamad)
{
    $.LoadingOverlay("show");
    // var Name = $("#MasterModal table textarea[name='Name']").val()
    var tseAdrs = $("#MasterModal table input[name='tseAdrs']").val()
    
 
    if(tseAdrs.trim().length==0)
    {
        $.LoadingOverlay("hide");
        alert("آدرسی برای ذخیره وجود ندارد")
        $("#MasterModal").modal("toggle");
        return;
    }

    var obj={}
    obj.url="/Saham/SaveTseAdrs"
    obj.dataType="json"
    obj.type="post"
    obj.data={IdNamad:IdNamad,tseAdrs:tseAdrs}
    
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    $.LoadingOverlay("hide");
    GetCompareToAvg()
    $("#MasterModal").modal("toggle");
    
}

async function conv(){
    //روش ایجاد جیسون از اطلاعات رهاورد365
 /*  
 var lst=[]
$("#DataTables_Table_0_wrapper table tr").each(function(){
var obj = {}
if($(this).attr("id")!=null)
{
var id=$(this).attr("id")
id=id.split("-")[1]
var title=$(this).find("td:eq(0)").find("div:eq(1)").find("div:eq(0)").find("a:eq(0)").text()
obj.id=id
obj.title=title
lst.push(obj)
//console.log(id+" : "+title)


}
})
var myJsonString = JSON.stringify(lst);
//console.log(myJsonString)
   */
    var obj={}
    obj.url="/Saham/GetNamad"
    obj.dataType="json"
    obj.type="post"
    // obj.data={TaskId:TaskId}
    
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
 
   // var objRahavard = jQuery.parseJSON( '[{"id":"583","title":"آ س پ"},{"id":"1775","title":"آبین"},{"id":"2023","title":"آپ"},{"id":"1879","title":"اپرداز"},{"id":"740","title":"اتکام"},{"id":"544","title":"اتکای"},{"id":"489","title":"اخابر"},{"id":"617","title":"ارفع"},{"id":"650","title":"آرمان"},{"id":"522","title":"آریان"},{"id":"474","title":"اعتلا"},{"id":"649","title":"افرا"},{"id":"5166","title":"افق"},{"id":"495","title":"البرز"},{"id":"759","title":"انرژی3"},{"id":"2025","title":"اوان"},{"id":"656","title":"آینده"},{"id":"582","title":"بالاس"},{"id":"140","title":"بالبر"},{"id":"653","title":"بپاس"},{"id":"2535","title":"بجهرم"},{"id":"1571","title":"بخاور"},{"id":"2051","title":"برکت"},{"id":"5115","title":"بزاگرس"},{"id":"657","title":"بساما"},{"id":"366","title":"بسویچ"},{"id":"136","title":"بشهاب"},{"id":"501","title":"بفجر"},{"id":"375","title":"بکاب"},{"id":"412","title":"بکام"},{"id":"1765","title":"بکهنوج"},{"id":"728","title":"بمپنا"},{"id":"138","title":"بموتو"},{"id":"655","title":"بمیلا"},{"id":"810","title":"بنو"},{"id":"390","title":"بنیرو"},{"id":"43","title":"بهپاک"},{"id":"541","title":"بورس"},{"id":"438","title":"پارتا"},{"id":"4599","title":"پارس"},{"id":"647","title":"پارسان"},{"id":"520","title":"پارسیان"},{"id":"378","title":"پاسا"},{"id":"753","title":"پاکشو"},{"id":"174","title":"پتایر"},{"id":"602","title":"پترول"},{"id":"319","title":"پخش"},{"id":"177","title":"پدرخش"},{"id":"2182","title":"پرداخت"},{"id":"308","title":"پردیس"},{"id":"180","title":"پسهند"},{"id":"285","title":"پکرمان"},{"id":"539","title":"پکویر"},{"id":"178","title":"پلاست"},{"id":"179","title":"پلاسک"},{"id":"360","title":"پلوله"},{"id":"726","title":"تابا"},{"id":"4107","title":"تاپکیش"},{"id":"515","title":"تاپیکو"},{"id":"5559","title":"تاصیکو"},{"id":"191","title":"تایرا"},{"id":"4073","title":"تبرک"},{"id":"270","title":"تپکو"},{"id":"188","title":"تپمپی"},{"id":"472","title":"تپولا"},{"id":"195","title":"تشتاد"},{"id":"392","title":"تکشا"},{"id":"203","title":"تکمبا"},{"id":"674","title":"تکنار"},{"id":"406","title":"تکنو"},{"id":"1543","title":"تلیسه"},{"id":"356","title":"تمحرکه"},{"id":"7803","title":"تملت"},{"id":"5111","title":"تنوین"},{"id":"606","title":"توریل"},{"id":"490","title":"تیپیکو"},{"id":"427","title":"ثاباد"},{"id":"428","title":"ثاخت"},{"id":"445","title":"ثاژن"},{"id":"353","title":"ثاصفا"},{"id":"1576","title":"ثالوند"},{"id":"312","title":"ثامان"},{"id":"619","title":"ثباغ"},{"id":"585","title":"ثپردیس"},{"id":"587","title":"ثتران"},{"id":"1548","title":"ثتوسا"},{"id":"486","title":"ثجوان"},{"id":"584","title":"ثرود"},{"id":"664","title":"ثزاگرس"},{"id":"448","title":"ثشاهد"},{"id":"586","title":"ثشرق"},{"id":"529","title":"ثعتما"},{"id":"588","title":"ثعمرا"},{"id":"739","title":"ثغرب"},{"id":"400","title":"ثفارس"},{"id":"561","title":"ثقزوی"},{"id":"396","title":"ثمسکن"},{"id":"719","title":"ثنظام"},{"id":"745","title":"ثنور"},{"id":"425","title":"ثنوسا"},{"id":"712","title":"جم"},{"id":"5525","title":"جمپیلن"},{"id":"578","title":"جهرم"},{"id":"133","title":"چافست"},{"id":"2537","title":"چدن"},{"id":"126","title":"چفیبر"},{"id":"1768","title":"چکاپا"},{"id":"127","title":"چکارن"},{"id":"238","title":"چکاوه"},{"id":"686","title":"حاریا"},{"id":"2544","title":"حآسا"},{"id":"503","title":"حبندر"},{"id":"1771","title":"حپارسا"},{"id":"380","title":"حپترو"},{"id":"424","title":"حتاید"},{"id":"410","title":"حتوکا"},{"id":"595","title":"حخزر"},{"id":"426","title":"حرهشا"},{"id":"1569","title":"حریل"},{"id":"2036","title":"حسیر"},{"id":"750","title":"حسینا"},{"id":"635","title":"حفارس"},{"id":"532","title":"حفاری"},{"id":"481","title":"حکشتی"},{"id":"350","title":"خاذین"},{"id":"265","title":"خاهن"},{"id":"169","title":"خاور"},{"id":"652","title":"خبازرس"},{"id":"431","title":"خپویش"},{"id":"296","title":"ختراک"},{"id":"279","title":"ختور"},{"id":"163","title":"ختوقا"},{"id":"194","title":"خچرخش"},{"id":"570","title":"خدیزل"},{"id":"340","title":"خراسان"},{"id":"280","title":"خریخت"},{"id":"397","title":"خرینگ"},{"id":"315","title":"خزامیا"},{"id":"275","title":"خزر"},{"id":"394","title":"خشرق"},{"id":"284","title":"خصدرا"},{"id":"749","title":"خعمرا"},{"id":"562","title":"خفناور"},{"id":"266","title":"خفنر"},{"id":"730","title":"خفولا"},{"id":"267","title":"خکار"},{"id":"170","title":"خکاوه"},{"id":"2840","title":"خکرمان"},{"id":"276","title":"خکمک"},{"id":"162","title":"خلنت"},{"id":"164","title":"خمحرکه"},{"id":"441","title":"خمحور"},{"id":"383","title":"خموتور"},{"id":"433","title":"خنصیر"},{"id":"508","title":"خودکفا"},{"id":"165","title":"خوساز"},{"id":"15","title":"دابور"},{"id":"579","title":"داراب"},{"id":"357","title":"دارو"},{"id":"20","title":"داسوه"},{"id":"251","title":"دالبر"},{"id":"25","title":"دامین"},{"id":"502","title":"دانا"},{"id":"4616","title":"داوه"},{"id":"622","title":"دبالک"},{"id":"27","title":"دپارس"},{"id":"358","title":"دتماد"},{"id":"6701","title":"دتوزیع"},{"id":"564","title":"دتولید"},{"id":"14","title":"دجابر"},{"id":"343","title":"دحاوی"},{"id":"12","title":"ددام"},{"id":"16","title":"درازک"},{"id":"6274","title":"درهآور"},{"id":"395","title":"دروز"},{"id":"347","title":"دزهراوی"},{"id":"1553","title":"دسانکو"},{"id":"362","title":"دسبحا"},{"id":"565","title":"دسبحان"},{"id":"23","title":"دسینا"},{"id":"13","title":"دشیری"},{"id":"359","title":"دشیمی"},{"id":"293","title":"دعبید"},{"id":"24","title":"دفارا"},{"id":"326","title":"دفرا"},{"id":"221","title":"دقاضی"},{"id":"5728","title":"دکپسول"},{"id":"22","title":"دکوثر"},{"id":"17","title":"دکیمی"},{"id":"318","title":"دلر"},{"id":"469","title":"دماوند"},{"id":"577","title":"دهدشت"},{"id":"607","title":"دی"},{"id":"354","title":"دیران"},{"id":"246","title":"ذوب"},{"id":"369","title":"رانفور"},{"id":"592","title":"رتاپ"},{"id":"408","title":"رتکو"},{"id":"666","title":"رکیش"},{"id":"442","title":"رمپنا"},{"id":"6273","title":"رنیک"},{"id":"2662","title":"ریشمک"},{"id":"648","title":"زاگرس"},{"id":"2909","title":"زبینا"},{"id":"4432","title":"زپارس"},{"id":"4714","title":"زدشت"},{"id":"5347","title":"زشریف"},{"id":"2084","title":"زشگزا"},{"id":"5306","title":"زفکا"},{"id":"2967","title":"زقیام"},{"id":"4473","title":"زکشت"},{"id":"7802","title":"زکوثر"},{"id":"1893","title":"زگلدشت"},{"id":"5178","title":"زماهان"},{"id":"625","title":"زنجان"},{"id":"542","title":"زنگان"},{"id":"121","title":"ساذری"},{"id":"304","title":"ساراب"},{"id":"331","title":"ساربیل"},{"id":"1585","title":"ساروج"},{"id":"117","title":"ساروم"},{"id":"510","title":"سامان"},{"id":"399","title":"ساوه"},{"id":"118","title":"سایرا"},{"id":"566","title":"ساینا"},{"id":"746","title":"سباقر"},{"id":"391","title":"سبجنو"},{"id":"623","title":"سبزوا"},{"id":"373","title":"سبهان"},{"id":"4076","title":"سپ"},{"id":"247","title":"سپاها"},{"id":"120","title":"سپرمی"},{"id":"111","title":"ستران"},{"id":"557","title":"سجام"},{"id":"309","title":"سخاش"},{"id":"115","title":"سخزر"},{"id":"682","title":"سخواف"},{"id":"589","title":"سخوز"},{"id":"504","title":"سدبیر"},{"id":"454","title":"سدشت"},{"id":"364","title":"سدور"},{"id":"482","title":"سرچشمه"},{"id":"302","title":"سرود"},{"id":"114","title":"سشرق"},{"id":"112","title":"سشمال"},{"id":"384","title":"سصفها"},{"id":"113","title":"سصوفی"},{"id":"109","title":"سغرب"},{"id":"420","title":"سفار"},{"id":"108","title":"سفارس"},{"id":"381","title":"سفارود"},{"id":"220","title":"سفاسی"},{"id":"517","title":"سفانو"},{"id":"248","title":"سقاین"},{"id":"297","title":"سکارون"},{"id":"249","title":"سکرد"},{"id":"110","title":"سکرما"},{"id":"706","title":"سلار"},{"id":"116","title":"سمازن"},{"id":"641","title":"سمایه"},{"id":"678","title":"سمتاز"},{"id":"523","title":"سمگا"},{"id":"545","title":"سنوین"},{"id":"449","title":"سنیر"},{"id":"236","title":"سهرمز"},{"id":"372","title":"سهگمت"},{"id":"479","title":"سیدکو"},{"id":"563","title":"سیستم"},{"id":"389","title":"سیلام"},{"id":"716","title":"سیمرغ"},{"id":"323","title":"شاراک"},{"id":"252","title":"شاملا"},{"id":"710","title":"شاوان"},{"id":"492","title":"شبریز"},{"id":"629","title":"شبصیر"},{"id":"591","title":"شبندر"},{"id":"8","title":"شپارس"},{"id":"509","title":"شپاس"},{"id":"11","title":"شپاکسا"},{"id":"185","title":"شپترو"},{"id":"636","title":"شپدیس"},{"id":"484","title":"شپنا"},{"id":"637","title":"شتران"},{"id":"603","title":"شتوکا"},{"id":"281","title":"شتولی"},{"id":"700","title":"شجم"},{"id":"301","title":"شخارک"},{"id":"234","title":"شدوص"},{"id":"696","title":"شراز"},{"id":"672","title":"شرانل"},{"id":"7","title":"شرنگی"},{"id":"478","title":"شسپا"},{"id":"713","title":"شستان"},{"id":"429","title":"شسم"},{"id":"222","title":"شسینا"},{"id":"6836","title":"شصدف"},{"id":"322","title":"شصفها"},{"id":"475","title":"شغدیر"},{"id":"2867","title":"شفا"},{"id":"186","title":"شفارا"},{"id":"292","title":"شفارس"},{"id":"459","title":"شفن"},{"id":"336","title":"شکبیر"},{"id":"184","title":"شکربن"},{"id":"4","title":"شکف"},{"id":"374","title":"شکلر"},{"id":"2054","title":"شگامرن"},{"id":"338","title":"شگل"},{"id":"7801","title":"شگویا"},{"id":"531","title":"شلرد"},{"id":"91","title":"شلعاب"},{"id":"217","title":"شمواد"},{"id":"182","title":"شنفت"},{"id":"4081","title":"شوینده"},{"id":"230","title":"شیران"},{"id":"7831","title":"صبا"},{"id":"423","title":"غاذر"},{"id":"416","title":"غالبر"},{"id":"5524","title":"غبهار"},{"id":"213","title":"غبهنوش"},{"id":"815","title":"غپآذر"},{"id":"34","title":"غپاک"},{"id":"4542","title":"غپونه"},{"id":"31","title":"غپینو"},{"id":"35","title":"غچین"},{"id":"40","title":"غدام"},{"id":"1551","title":"غدیس"},{"id":"352","title":"غسالم"},{"id":"407","title":"غشاذر"},{"id":"434","title":"غشان"},{"id":"32","title":"غشهد"},{"id":"1577","title":"غشهداب"},{"id":"210","title":"غشوکو"},{"id":"44","title":"غصینو"},{"id":"528","title":"غفارس"},{"id":"30","title":"غگرجی"},{"id":"2056","title":"غگز"},{"id":"313","title":"غگل"},{"id":"1963","title":"غگلپا"},{"id":"1892","title":"غگلستا"},{"id":"33","title":"غمارگ"},{"id":"216","title":"غمهرا"},{"id":"732","title":"غمینو"},{"id":"45","title":"غنوش"},{"id":"205","title":"فاراک"},{"id":"288","title":"فاسمین"},{"id":"725","title":"فافزا"},{"id":"143","title":"فالوم"},{"id":"147","title":"فاما"},{"id":"157","title":"فاهواز"},{"id":"240","title":"فایرا"},{"id":"160","title":"فباهنر"},{"id":"158","title":"فبیرا"},{"id":"144","title":"فپنتا"},{"id":"159","title":"فجام"},{"id":"415","title":"فجر"},{"id":"458","title":"فخاس"},{"id":"455","title":"فخوز"},{"id":"535","title":"فرابورس"},{"id":"355","title":"فرآور"},{"id":"250","title":"فروس"},{"id":"7129","title":"فروی"},{"id":"683","title":"فزرین"},{"id":"580","title":"فسا"},{"id":"5522","title":"فسازان"},{"id":"351","title":"فسپا"},{"id":"403","title":"فسدید"},{"id":"514","title":"فلات"},{"id":"148","title":"فلامی"},{"id":"152","title":"فلوله"},{"id":"439","title":"فمراد"},{"id":"451","title":"فملی"},{"id":"645","title":"فن آوا"},{"id":"2005","title":"فنفت"},{"id":"149","title":"فنوال"},{"id":"145","title":"فنورد"},{"id":"385","title":"فوکا"},{"id":"453","title":"فولاد"},{"id":"569","title":"فولاژ"},{"id":"624","title":"فولای"},{"id":"306","title":"قاسم"},{"id":"225","title":"قپیرا"},{"id":"47","title":"قثابت"},{"id":"150","title":"قجام"},{"id":"332","title":"قچار"},{"id":"7697","title":"قرن"},{"id":"314","title":"قزوین"},{"id":"224","title":"قشکر"},{"id":"226","title":"قشهد"},{"id":"227","title":"قشیر"},{"id":"53","title":"قلرست"},{"id":"48","title":"قمرو"},{"id":"52","title":"قنقش"},{"id":"50","title":"قنیشا"},{"id":"55","title":"قهکمت"},{"id":"97","title":"کابگن"},{"id":"371","title":"کاذر"},{"id":"533","title":"کازرو"},{"id":"491","title":"کاسپین"},{"id":"537","title":"کالا"},{"id":"404","title":"کاما"},{"id":"767","title":"کاوه"},{"id":"98","title":"کایتا"},{"id":"273","title":"کبافق"},{"id":"107","title":"کپارس"},{"id":"724","title":"کپرور"},{"id":"96","title":"کپشیر"},{"id":"339","title":"کترام"},{"id":"605","title":"کتوکا"},{"id":"382","title":"کچاد"},{"id":"123","title":"کحافظ"},{"id":"245","title":"کخاک"},{"id":"335","title":"کدما"},{"id":"432","title":"کرازی"},{"id":"346","title":"کرماشا"},{"id":"228","title":"کرمان"},{"id":"414","title":"کساپا"},{"id":"295","title":"کساوه"},{"id":"92","title":"کسرام"},{"id":"122","title":"کسعدی"},{"id":"668","title":"کشرق"},{"id":"651","title":"کصدف"},{"id":"405","title":"کطبس"},{"id":"307","title":"کفپارس"},{"id":"620","title":"کفرآور"},{"id":"94","title":"کقزوی"},{"id":"104","title":"کگاز"},{"id":"419","title":"کگل"},{"id":"7536","title":"کلر"},{"id":"106","title":"کلوند"},{"id":"105","title":"کماسه"},{"id":"540","title":"کمرجان"},{"id":"363","title":"کمنگنز"},{"id":"1769","title":"کمینا"},{"id":"463","title":"کنور"},{"id":"409","title":"کهرام"},{"id":"499","title":"کوثر"},{"id":"6874","title":"کویر"},{"id":"468","title":"کی بی سی"},{"id":"699","title":"کیمیا"},{"id":"1568","title":"گپارس"},{"id":"2192","title":"گکوثر"},{"id":"695","title":"گکیش"},{"id":"1564","title":"گوهران"},{"id":"200","title":"لابسا"},{"id":"208","title":"لازما"},{"id":"264","title":"لبوتان"},{"id":"88","title":"لپارس"},{"id":"89","title":"لپیام"},{"id":"207","title":"لخزر"},{"id":"197","title":"لسرما"},{"id":"421","title":"لکما"},{"id":"5225","title":"لوتوس"},{"id":"638","title":"ما"},{"id":"4431","title":"مادیرا"},{"id":"485","title":"مارون"},{"id":"500","title":"مبین"},{"id":"344","title":"مداران"},{"id":"446","title":"مرقام"},{"id":"568","title":"معیار "},{"id":"644","title":"مفاخر"},{"id":"465","title":"ملت"},{"id":"530","title":"ممسنی"},{"id":"507","title":"میدکو"},{"id":"534","title":"میهن"},{"id":"2670","title":"نطرین"},{"id":"73","title":"نمرینو"},{"id":"673","title":"نوری"},{"id":"543","title":"نوین"},{"id":"576","title":"نیرو"},{"id":"2795","title":"های وب"},{"id":"325","title":"هجرت"},{"id":"494","title":"هرمز"},{"id":"747","title":"وآتوس"},{"id":"436","title":"واتی"},{"id":"685","title":"واحصا"},{"id":"527","title":"واحیا"},{"id":"324","title":"وآذر"},{"id":"518","title":"وارس"},{"id":"621","title":"وآرین"},{"id":"166","title":"واعتبار"},{"id":"667","title":"وآفری"},{"id":"61","title":"والبر"},{"id":"456","title":"وامید"},{"id":"6839","title":"وآوا"},{"id":"229","title":"وایرا"},{"id":"546","title":"وایران"},{"id":"743","title":"وآیند"},{"id":"65","title":"وبانک"},{"id":"2039","title":"وبرق"},{"id":"59","title":"وبشهر"},{"id":"498","title":"وبصادر"},{"id":"462","title":"وبملت"},{"id":"430","title":"وبهمن"},{"id":"386","title":"وبوعلی"},{"id":"321","title":"وبیمه"},{"id":"519","title":"وپاسار"},{"id":"21","title":"وپخش"},{"id":"634","title":"وپسا"},{"id":"604","title":"وپست"},{"id":"461","title":"وتجارت"},{"id":"701","title":"وتعاون"},{"id":"387","title":"وتوس"},{"id":"437","title":"وتوسم"},{"id":"56","title":"وتوشه"},{"id":"316","title":"وتوصا"},{"id":"320","title":"وثخوز"},{"id":"483","title":"وثنو"},{"id":"552","title":"وثوق"},{"id":"1567","title":"وجامی"},{"id":"752","title":"وحافظ"},{"id":"1953","title":"وحکمت"},{"id":"470","title":"وخارزم"},{"id":"708","title":"وخاور"},{"id":"512","title":"ودی"},{"id":"631","title":"وزمین"},{"id":"9","title":"وساپا"},{"id":"290","title":"وساخت"},{"id":"742","title":"وسالت"},{"id":"480","title":"وسبحان"},{"id":"63","title":"وسپه"},{"id":"232","title":"وسدید"},{"id":"741","title":"وسرمد"},{"id":"684","title":"وسکاب"},{"id":"608","title":"وسنا"},{"id":"735","title":"وسین"},{"id":"457","title":"وسینا"},{"id":"640","title":"وشمال"},{"id":"401","title":"وصنا"},{"id":"334","title":"وصندوق"},{"id":"294","title":"وصنعت"},{"id":"66","title":"وغدیر"},{"id":"473","title":"وکادو"},{"id":"333","title":"وکار"},{"id":"547","title":"وگستر"},{"id":"599","title":"ولانا"},{"id":"571","title":"ولبهمن"},{"id":"698","title":"ولتجار"},{"id":"661","title":"ولراز"},{"id":"444","title":"ولساپا"},{"id":"2671","title":"ولشرق"},{"id":"435","title":"ولصنم"},{"id":"1531","title":"ولملت"},{"id":"418","title":"ولیز"},{"id":"702","title":"ومشان"},{"id":"4689","title":"ومعلم"},{"id":"64","title":"وملت"},{"id":"639","title":"وملل"},{"id":"60","title":"وملی"},{"id":"7209","title":"ومهان"},{"id":"337","title":"ونفت"},{"id":"417","title":"ونیرو"},{"id":"62","title":"ونیکی"},{"id":"681","title":"وهور"}]' );
    var objRahavard=jQuery.parseJSON('[{"id":"583","title":"آ س پ"},{"id":"1775","title":"آبین"},{"id":"2023","title":"آپ"},{"id":"1879","title":"اپرداز"},{"id":"740","title":"اتکام"},{"id":"544","title":"اتکای"},{"id":"489","title":"اخابر"},{"id":"617","title":"ارفع"},{"id":"650","title":"آرمان"},{"id":"522","title":"آریان"},{"id":"476","title":"آسیا"},{"id":"649","title":"افرا"},{"id":"5166","title":"افق"},{"id":"495","title":"البرز"},{"id":"2147","title":"امید"},{"id":"759","title":"انرژی3"},{"id":"2025","title":"اوان"},{"id":"656","title":"آینده"},{"id":"582","title":"بالاس"},{"id":"140","title":"بالبر"},{"id":"283","title":"بایکا"},{"id":"653","title":"بپاس"},{"id":"137","title":"بترانس"},{"id":"142","title":"بتک"},{"id":"2535","title":"بجهرم"},{"id":"1571","title":"بخاور"},{"id":"2051","title":"برکت"},{"id":"5115","title":"بزاگرس"},{"id":"657","title":"بساما"},{"id":"366","title":"بسویچ"},{"id":"136","title":"بشهاب"},{"id":"501","title":"بفجر"},{"id":"375","title":"بکاب"},{"id":"412","title":"بکام"},{"id":"1765","title":"بکهنوج"},{"id":"728","title":"بمپنا"},{"id":"138","title":"بموتو"},{"id":"655","title":"بمیلا"},{"id":"810","title":"بنو"},{"id":"390","title":"بنیرو"},{"id":"541","title":"بورس"},{"id":"438","title":"پارتا"},{"id":"4599","title":"پارس"},{"id":"647","title":"پارسان"},{"id":"520","title":"پارسیان"},{"id":"378","title":"پاسا"},{"id":"753","title":"پاکشو"},{"id":"174","title":"پتایر"},{"id":"177","title":"پدرخش"},{"id":"2182","title":"پرداخت"},{"id":"308","title":"پردیس"},{"id":"180","title":"پسهند"},{"id":"285","title":"پکرمان"},{"id":"178","title":"پلاست"},{"id":"179","title":"پلاسک"},{"id":"360","title":"پلوله"},{"id":"726","title":"تابا"},{"id":"4107","title":"تاپکیش"},{"id":"515","title":"تاپیکو"},{"id":"5559","title":"تاصیکو"},{"id":"191","title":"تایرا"},{"id":"4073","title":"تبرک"},{"id":"270","title":"تپکو"},{"id":"188","title":"تپمپی"},{"id":"472","title":"تپولا"},{"id":"195","title":"تشتاد"},{"id":"392","title":"تکشا"},{"id":"203","title":"تکمبا"},{"id":"674","title":"تکنار"},{"id":"406","title":"تکنو"},{"id":"1543","title":"تلیسه"},{"id":"356","title":"تمحرکه"},{"id":"5111","title":"تنوین"},{"id":"606","title":"توریل"},{"id":"490","title":"تیپیکو"},{"id":"427","title":"ثاباد"},{"id":"428","title":"ثاخت"},{"id":"445","title":"ثاژن"},{"id":"353","title":"ثاصفا"},{"id":"1576","title":"ثالوند"},{"id":"312","title":"ثامان"},{"id":"619","title":"ثباغ"},{"id":"585","title":"ثپردیس"},{"id":"587","title":"ثتران"},{"id":"1548","title":"ثتوسا"},{"id":"486","title":"ثجوان"},{"id":"584","title":"ثرود"},{"id":"664","title":"ثزاگرس"},{"id":"448","title":"ثشاهد"},{"id":"586","title":"ثشرق"},{"id":"529","title":"ثعتما"},{"id":"588","title":"ثعمرا"},{"id":"739","title":"ثغرب"},{"id":"400","title":"ثفارس"},{"id":"561","title":"ثقزوی"},{"id":"396","title":"ثمسکن"},{"id":"719","title":"ثنظام"},{"id":"745","title":"ثنور"},{"id":"425","title":"ثنوسا"},{"id":"712","title":"جم"},{"id":"5525","title":"جمپیلن"},{"id":"578","title":"جهرم"},{"id":"133","title":"چافست"},{"id":"2537","title":"چدن"},{"id":"126","title":"چفیبر"},{"id":"1768","title":"چکاپا"},{"id":"127","title":"چکارن"},{"id":"238","title":"چکاوه"},{"id":"686","title":"حاریا"},{"id":"2544","title":"حآسا"},{"id":"503","title":"حبندر"},{"id":"380","title":"حپترو"},{"id":"424","title":"حتاید"},{"id":"410","title":"حتوکا"},{"id":"595","title":"حخزر"},{"id":"426","title":"حرهشا"},{"id":"1569","title":"حریل"},{"id":"2036","title":"حسیر"},{"id":"750","title":"حسینا"},{"id":"635","title":"حفارس"},{"id":"532","title":"حفاری"},{"id":"481","title":"حکشتی"},{"id":"350","title":"خاذین"},{"id":"265","title":"خاهن"},{"id":"169","title":"خاور"},{"id":"652","title":"خبازرس"},{"id":"168","title":"خبهمن"},{"id":"330","title":"خپارس"},{"id":"431","title":"خپویش"},{"id":"296","title":"ختراک"},{"id":"279","title":"ختور"},{"id":"163","title":"ختوقا"},{"id":"194","title":"خچرخش"},{"id":"570","title":"خدیزل"},{"id":"340","title":"خراسان"},{"id":"280","title":"خریخت"},{"id":"397","title":"خرینگ"},{"id":"315","title":"خزامیا"},{"id":"275","title":"خزر"},{"id":"394","title":"خشرق"},{"id":"284","title":"خصدرا"},{"id":"749","title":"خعمرا"},{"id":"562","title":"خفناور"},{"id":"266","title":"خفنر"},{"id":"730","title":"خفولا"},{"id":"267","title":"خکار"},{"id":"170","title":"خکاوه"},{"id":"2840","title":"خکرمان"},{"id":"276","title":"خکمک"},{"id":"162","title":"خلنت"},{"id":"164","title":"خمحرکه"},{"id":"441","title":"خمحور"},{"id":"398","title":"خمهر"},{"id":"383","title":"خموتور"},{"id":"433","title":"خنصیر"},{"id":"508","title":"خودکفا"},{"id":"165","title":"خوساز"},{"id":"15","title":"دابور"},{"id":"579","title":"داراب"},{"id":"357","title":"دارو"},{"id":"20","title":"داسوه"},{"id":"251","title":"دالبر"},{"id":"25","title":"دامین"},{"id":"502","title":"دانا"},{"id":"4616","title":"داوه"},{"id":"622","title":"دبالک"},{"id":"27","title":"دپارس"},{"id":"358","title":"دتماد"},{"id":"6701","title":"دتوزیع"},{"id":"564","title":"دتولید"},{"id":"14","title":"دجابر"},{"id":"343","title":"دحاوی"},{"id":"12","title":"ددام"},{"id":"16","title":"درازک"},{"id":"6274","title":"درهآور"},{"id":"395","title":"دروز"},{"id":"347","title":"دزهراوی"},{"id":"1553","title":"دسانکو"},{"id":"362","title":"دسبحا"},{"id":"565","title":"دسبحان"},{"id":"23","title":"دسینا"},{"id":"13","title":"دشیری"},{"id":"359","title":"دشیمی"},{"id":"293","title":"دعبید"},{"id":"24","title":"دفارا"},{"id":"326","title":"دفرا"},{"id":"221","title":"دقاضی"},{"id":"5728","title":"دکپسول"},{"id":"22","title":"دکوثر"},{"id":"17","title":"دکیمی"},{"id":"318","title":"دلر"},{"id":"19","title":"دلقما"},{"id":"469","title":"دماوند"},{"id":"577","title":"دهدشت"},{"id":"607","title":"دی"},{"id":"246","title":"ذوب"},{"id":"369","title":"رانفور"},{"id":"592","title":"رتاپ"},{"id":"408","title":"رتکو"},{"id":"666","title":"رکیش"},{"id":"442","title":"رمپنا"},{"id":"6273","title":"رنیک"},{"id":"648","title":"زاگرس"},{"id":"2909","title":"زبینا"},{"id":"4714","title":"زدشت"},{"id":"2084","title":"زشگزا"},{"id":"5306","title":"زفکا"},{"id":"2967","title":"زقیام"},{"id":"4473","title":"زکشت"},{"id":"1893","title":"زگلدشت"},{"id":"345","title":"زمگسا"},{"id":"625","title":"زنجان"},{"id":"542","title":"زنگان"},{"id":"304","title":"ساراب"},{"id":"331","title":"ساربیل"},{"id":"1585","title":"ساروج"},{"id":"117","title":"ساروم"},{"id":"510","title":"سامان"},{"id":"399","title":"ساوه"},{"id":"118","title":"سایرا"},{"id":"566","title":"ساینا"},{"id":"746","title":"سباقر"},{"id":"391","title":"سبجنو"},{"id":"623","title":"سبزوا"},{"id":"373","title":"سبهان"},{"id":"4076","title":"سپ"},{"id":"247","title":"سپاها"},{"id":"120","title":"سپرمی"},{"id":"111","title":"ستران"},{"id":"557","title":"سجام"},{"id":"115","title":"سخزر"},{"id":"682","title":"سخواف"},{"id":"589","title":"سخوز"},{"id":"504","title":"سدبیر"},{"id":"454","title":"سدشت"},{"id":"364","title":"سدور"},{"id":"302","title":"سرود"},{"id":"114","title":"سشرق"},{"id":"112","title":"سشمال"},{"id":"384","title":"سصفها"},{"id":"113","title":"سصوفی"},{"id":"109","title":"سغرب"},{"id":"420","title":"سفار"},{"id":"108","title":"سفارس"},{"id":"381","title":"سفارود"},{"id":"220","title":"سفاسی"},{"id":"517","title":"سفانو"},{"id":"248","title":"سقاین"},{"id":"297","title":"سکارون"},{"id":"110","title":"سکرما"},{"id":"706","title":"سلار"},{"id":"116","title":"سمازن"},{"id":"641","title":"سمایه"},{"id":"678","title":"سمتاز"},{"id":"523","title":"سمگا"},{"id":"545","title":"سنوین"},{"id":"449","title":"سنیر"},{"id":"236","title":"سهرمز"},{"id":"372","title":"سهگمت"},{"id":"479","title":"سیدکو"},{"id":"563","title":"سیستم"},{"id":"389","title":"سیلام"},{"id":"716","title":"سیمرغ"},{"id":"323","title":"شاراک"},{"id":"252","title":"شاملا"},{"id":"710","title":"شاوان"},{"id":"492","title":"شبریز"},{"id":"629","title":"شبصیر"},{"id":"591","title":"شبندر"},{"id":"183","title":"شبهرن"},{"id":"8","title":"شپارس"},{"id":"509","title":"شپاس"},{"id":"11","title":"شپاکسا"},{"id":"185","title":"شپترو"},{"id":"636","title":"شپدیس"},{"id":"484","title":"شپنا"},{"id":"637","title":"شتران"},{"id":"603","title":"شتوکا"},{"id":"281","title":"شتولی"},{"id":"700","title":"شجم"},{"id":"301","title":"شخارک"},{"id":"234","title":"شدوص"},{"id":"696","title":"شراز"},{"id":"672","title":"شرانل"},{"id":"7","title":"شرنگی"},{"id":"4435","title":"شساخت"},{"id":"478","title":"شسپا"},{"id":"713","title":"شستان"},{"id":"429","title":"شسم"},{"id":"222","title":"شسینا"},{"id":"6836","title":"شصدف"},{"id":"322","title":"شصفها"},{"id":"475","title":"شغدیر"},{"id":"2867","title":"شفا"},{"id":"186","title":"شفارا"},{"id":"292","title":"شفارس"},{"id":"459","title":"شفن"},{"id":"336","title":"شکبیر"},{"id":"184","title":"شکربن"},{"id":"4","title":"شکف"},{"id":"374","title":"شکلر"},{"id":"338","title":"شگل"},{"id":"531","title":"شلرد"},{"id":"91","title":"شلعاب"},{"id":"217","title":"شمواد"},{"id":"182","title":"شنفت"},{"id":"440","title":"شیراز"},{"id":"230","title":"شیران"},{"id":"423","title":"غاذر"},{"id":"416","title":"غالبر"},{"id":"42","title":"غبشهر"},{"id":"5524","title":"غبهار"},{"id":"213","title":"غبهنوش"},{"id":"815","title":"غپآذر"},{"id":"34","title":"غپاک"},{"id":"4542","title":"غپونه"},{"id":"31","title":"غپینو"},{"id":"35","title":"غچین"},{"id":"40","title":"غدام"},{"id":"1551","title":"غدیس"},{"id":"352","title":"غسالم"},{"id":"407","title":"غشاذر"},{"id":"434","title":"غشان"},{"id":"32","title":"غشهد"},{"id":"1577","title":"غشهداب"},{"id":"210","title":"غشوکو"},{"id":"44","title":"غصینو"},{"id":"528","title":"غفارس"},{"id":"30","title":"غگرجی"},{"id":"2056","title":"غگز"},{"id":"313","title":"غگل"},{"id":"1963","title":"غگلپا"},{"id":"1892","title":"غگلستا"},{"id":"216","title":"غمهرا"},{"id":"732","title":"غمینو"},{"id":"45","title":"غنوش"},{"id":"41","title":"غویتا"},{"id":"328","title":"فاذر"},{"id":"205","title":"فاراک"},{"id":"703","title":"فارس"},{"id":"288","title":"فاسمین"},{"id":"725","title":"فافزا"},{"id":"143","title":"فالوم"},{"id":"147","title":"فاما"},{"id":"157","title":"فاهواز"},{"id":"240","title":"فایرا"},{"id":"158","title":"فبیرا"},{"id":"144","title":"فپنتا"},{"id":"159","title":"فجام"},{"id":"415","title":"فجر"},{"id":"458","title":"فخاس"},{"id":"455","title":"فخوز"},{"id":"535","title":"فرابورس"},{"id":"355","title":"فرآور"},{"id":"250","title":"فروس"},{"id":"7129","title":"فروی"},{"id":"683","title":"فزرین"},{"id":"580","title":"فسا"},{"id":"5522","title":"فسازان"},{"id":"351","title":"فسپا"},{"id":"403","title":"فسدید"},{"id":"253","title":"فسرب"},{"id":"514","title":"فلات"},{"id":"148","title":"فلامی"},{"id":"152","title":"فلوله"},{"id":"439","title":"فمراد"},{"id":"451","title":"فملی"},{"id":"645","title":"فن آوا"},{"id":"714","title":"فنرژی"},{"id":"2005","title":"فنفت"},{"id":"149","title":"فنوال"},{"id":"145","title":"فنورد"},{"id":"385","title":"فوکا"},{"id":"453","title":"فولاد"},{"id":"569","title":"فولاژ"},{"id":"624","title":"فولای"},{"id":"225","title":"قپیرا"},{"id":"47","title":"قثابت"},{"id":"150","title":"قجام"},{"id":"332","title":"قچار"},{"id":"7697","title":"قرن"},{"id":"314","title":"قزوین"},{"id":"224","title":"قشکر"},{"id":"226","title":"قشهد"},{"id":"227","title":"قشیر"},{"id":"287","title":"قصفها"},{"id":"48","title":"قمرو"},{"id":"52","title":"قنقش"},{"id":"50","title":"قنیشا"},{"id":"55","title":"قهکمت"},{"id":"97","title":"کابگن"},{"id":"371","title":"کاذر"},{"id":"533","title":"کازرو"},{"id":"491","title":"کاسپین"},{"id":"537","title":"کالا"},{"id":"404","title":"کاما"},{"id":"767","title":"کاوه"},{"id":"98","title":"کایتا"},{"id":"273","title":"کبافق"},{"id":"107","title":"کپارس"},{"id":"724","title":"کپرور"},{"id":"96","title":"کپشیر"},{"id":"339","title":"کترام"},{"id":"605","title":"کتوکا"},{"id":"382","title":"کچاد"},{"id":"123","title":"کحافظ"},{"id":"245","title":"کخاک"},{"id":"335","title":"کدما"},{"id":"432","title":"کرازی"},{"id":"346","title":"کرماشا"},{"id":"228","title":"کرمان"},{"id":"341","title":"کروی"},{"id":"414","title":"کساپا"},{"id":"413","title":"کسرا"},{"id":"92","title":"کسرام"},{"id":"122","title":"کسعدی"},{"id":"668","title":"کشرق"},{"id":"651","title":"کصدف"},{"id":"405","title":"کطبس"},{"id":"307","title":"کفپارس"},{"id":"124","title":"کفرا"},{"id":"620","title":"کفرآور"},{"id":"94","title":"کقزوی"},{"id":"104","title":"کگاز"},{"id":"419","title":"کگل"},{"id":"7536","title":"کلر"},{"id":"106","title":"کلوند"},{"id":"105","title":"کماسه"},{"id":"540","title":"کمرجان"},{"id":"363","title":"کمنگنز"},{"id":"1769","title":"کمینا"},{"id":"463","title":"کنور"},{"id":"409","title":"کهرام"},{"id":"103","title":"کهمدا"},{"id":"499","title":"کوثر"},{"id":"6874","title":"کویر"},{"id":"468","title":"کی بی سی"},{"id":"505","title":"کیسون"},{"id":"699","title":"کیمیا"},{"id":"1568","title":"گپارس"},{"id":"2192","title":"گکوثر"},{"id":"695","title":"گکیش"},{"id":"1564","title":"گوهران"},{"id":"200","title":"لابسا"},{"id":"208","title":"لازما"},{"id":"264","title":"لبوتان"},{"id":"88","title":"لپارس"},{"id":"89","title":"لپیام"},{"id":"204","title":"لخانه"},{"id":"207","title":"لخزر"},{"id":"197","title":"لسرما"},{"id":"421","title":"لکما"},{"id":"5225","title":"لوتوس"},{"id":"638","title":"ما"},{"id":"4431","title":"مادیرا"},{"id":"485","title":"مارون"},{"id":"500","title":"مبین"},{"id":"344","title":"مداران"},{"id":"446","title":"مرقام"},{"id":"568","title":"معیار "},{"id":"644","title":"مفاخر"},{"id":"465","title":"ملت"},{"id":"530","title":"ممسنی"},{"id":"507","title":"میدکو"},{"id":"534","title":"میهن"},{"id":"74","title":"نبروج"},{"id":"2670","title":"نطرین"},{"id":"690","title":"نکالا"},{"id":"73","title":"نمرینو"},{"id":"673","title":"نوری"},{"id":"576","title":"نیرو"},{"id":"2795","title":"های وب"},{"id":"325","title":"هجرت"},{"id":"494","title":"هرمز"},{"id":"549","title":"همراه"},{"id":"747","title":"وآتوس"},{"id":"436","title":"واتی"},{"id":"685","title":"واحصا"},{"id":"527","title":"واحیا"},{"id":"324","title":"وآذر"},{"id":"518","title":"وارس"},{"id":"621","title":"وآرین"},{"id":"166","title":"واعتبار"},{"id":"667","title":"وآفری"},{"id":"61","title":"والبر"},{"id":"456","title":"وامید"},{"id":"229","title":"وایرا"},{"id":"546","title":"وایران"},{"id":"743","title":"وآیند"},{"id":"65","title":"وبانک"},{"id":"2039","title":"وبرق"},{"id":"59","title":"وبشهر"},{"id":"498","title":"وبصادر"},{"id":"462","title":"وبملت"},{"id":"386","title":"وبوعلی"},{"id":"321","title":"وبیمه"},{"id":"519","title":"وپاسار"},{"id":"58","title":"وپترو"},{"id":"21","title":"وپخش"},{"id":"634","title":"وپسا"},{"id":"604","title":"وپست"},{"id":"461","title":"وتجارت"},{"id":"701","title":"وتعاون"},{"id":"387","title":"وتوس"},{"id":"437","title":"وتوسم"},{"id":"56","title":"وتوشه"},{"id":"316","title":"وتوصا"},{"id":"361","title":"وتوکا"},{"id":"320","title":"وثخوز"},{"id":"483","title":"وثنو"},{"id":"552","title":"وثوق"},{"id":"1567","title":"وجامی"},{"id":"752","title":"وحافظ"},{"id":"1953","title":"وحکمت"},{"id":"470","title":"وخارزم"},{"id":"708","title":"وخاور"},{"id":"512","title":"ودی"},{"id":"525","title":"ورازی"},{"id":"57","title":"ورنا"},{"id":"631","title":"وزمین"},{"id":"9","title":"وساپا"},{"id":"290","title":"وساخت"},{"id":"742","title":"وسالت"},{"id":"480","title":"وسبحان"},{"id":"63","title":"وسپه"},{"id":"232","title":"وسدید"},{"id":"741","title":"وسرمد"},{"id":"684","title":"وسکاب"},{"id":"608","title":"وسنا"},{"id":"735","title":"وسین"},{"id":"640","title":"وشمال"},{"id":"401","title":"وصنا"},{"id":"294","title":"وصنعت"},{"id":"66","title":"وغدیر"},{"id":"473","title":"وکادو"},{"id":"333","title":"وکار"},{"id":"547","title":"وگستر"},{"id":"599","title":"ولانا"},{"id":"571","title":"ولبهمن"},{"id":"698","title":"ولتجار"},{"id":"661","title":"ولراز"},{"id":"444","title":"ولساپا"},{"id":"2671","title":"ولشرق"},{"id":"435","title":"ولصنم"},{"id":"447","title":"ولغدر"},{"id":"1531","title":"ولملت"},{"id":"418","title":"ولیز"},{"id":"702","title":"ومشان"},{"id":"639","title":"وملل"},{"id":"7209","title":"ومهان"},{"id":"337","title":"ونفت"},{"id":"393","title":"ونوین"},{"id":"417","title":"ونیرو"}]');

    var CreatePromise = []

    for (var i = 0; i <ListObj.length; i++) {
       // if(ListObj[i].NamadSahih=='فارس') 
        for (var j = 0; j < objRahavard.length; j++) {
            if( ListObj[i].NamadSahih==objRahavard[j].title)
            {
                
                var obj={}
                obj.url="/Saham/UpdateNamad"
                obj.dataType="json"
                obj.type="post"
                obj.data={IdNamad:ListObj[i].Id,IdRahavard:objRahavard[j].id}
                CreatePromise.push(service(obj))
                // console.log(objRahavard[j].title +" : "+objRahavard[j].id)
                // 
            }
        }
    }
     
    var results = await Promise.all(CreatePromise);

    
}
async function InsertTseJsonToDB(){
    var ShamsyDate= $("input[name='DateForUpToDateSaham']").val();
    
    if(ShamsyDate.length!=8)
    {
        alert("فرمت تاریخ درست نمیباشد")
        return
    }
    //if(todayShamsy8char()!=ShamsyDate)
    //{
    //    alert("فقط همین امروز را میتوان ویرایش کرد")
    //    return
    //}
    
    $.LoadingOverlay("show");
    var tseJson = $("textarea[name='tseJson']").val()
    var CreatePromise = []
    var obj={}
    obj.url="/Saham/InsertUpdateJson"
    obj.dataType="json"
    obj.type="post"


    var lstObj=jQuery.parseJSON(tseJson)

    for (var j = 0; j < lstObj.length; j++) {
        
        var tedadMoamele= removeComma(lstObj[j].tedadMoamele);
        
        var hajm= lstObj[j].hajm;
        hajm=removeComma(hajm)
        if (hajm.indexOf('M') > -1)
        {
            hajm= removeChara('M',hajm)
            hajm=hajm.trim()
            hajm=hajm*1000000
        }
        else
        {
            
            hajm= removeChara('M',hajm)
            hajm=hajm.trim()
       
        }
        hajm=parseInt(hajm)

        var darsad=parseFloat(lstObj[j].darsad);
        darsad=darsad.toFixed(2)
        //console.log(darsad)
        //console.log(darsad.toFixed(2))
       
        //hajm= removeChara('M',hajm)
        //hajm=hajm.trim()
        //hajm=hajm*1000000
        //hajm=parseInt(hajm)
        var GheymatPayany= removeComma(lstObj[j].meghdar);
        
        obj.data={
            title:lstObj[j].title,
            tseId:lstObj[j].id,
            TedadMoamelat:tedadMoamele,
            hajm:hajm,
            DarsadGheymatPayany: darsad,
            ShamsyDate:ShamsyDate,
            GheymatPayany:GheymatPayany
        }
                CreatePromise.push(service(obj))

           
        
    }

    var results = await Promise.all(CreatePromise);
    
    if( results[0].message!=undefined)
    {
        $.LoadingOverlay("hide");
        alert(results[0].message)
      
        return
    }
    $.LoadingOverlay("hide");
}

async function convTse(){
    //روش ایجاد جیسون از اطلاعات tse
    /*  
    var lst=[]
$("#main-table table tbody tr").each(function(){
var obj = {}
if($(this).attr("id")!=null)
{
var id=$(this).attr("id")
var title=$(this).find("td:eq(1)").text()
obj.id=id
obj.title=title
lst.push(obj)
}
})
var myJsonString = JSON.stringify(lst);
console.log(myJsonString)
      */
    var obj={}
    obj.url="/Saham/GetNamad"
    obj.dataType="json"
    obj.type="post"
    // obj.data={TaskId:TaskId}
    
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
 
    // var objRahavard = jQuery.parseJSON( '[{"id":"583","title":"آ س پ"},{"id":"1775","title":"آبین"},{"id":"2023","title":"آپ"},{"id":"1879","title":"اپرداز"},{"id":"740","title":"اتکام"},{"id":"544","title":"اتکای"},{"id":"489","title":"اخابر"},{"id":"617","title":"ارفع"},{"id":"650","title":"آرمان"},{"id":"522","title":"آریان"},{"id":"474","title":"اعتلا"},{"id":"649","title":"افرا"},{"id":"5166","title":"افق"},{"id":"495","title":"البرز"},{"id":"759","title":"انرژی3"},{"id":"2025","title":"اوان"},{"id":"656","title":"آینده"},{"id":"582","title":"بالاس"},{"id":"140","title":"بالبر"},{"id":"653","title":"بپاس"},{"id":"2535","title":"بجهرم"},{"id":"1571","title":"بخاور"},{"id":"2051","title":"برکت"},{"id":"5115","title":"بزاگرس"},{"id":"657","title":"بساما"},{"id":"366","title":"بسویچ"},{"id":"136","title":"بشهاب"},{"id":"501","title":"بفجر"},{"id":"375","title":"بکاب"},{"id":"412","title":"بکام"},{"id":"1765","title":"بکهنوج"},{"id":"728","title":"بمپنا"},{"id":"138","title":"بموتو"},{"id":"655","title":"بمیلا"},{"id":"810","title":"بنو"},{"id":"390","title":"بنیرو"},{"id":"43","title":"بهپاک"},{"id":"541","title":"بورس"},{"id":"438","title":"پارتا"},{"id":"4599","title":"پارس"},{"id":"647","title":"پارسان"},{"id":"520","title":"پارسیان"},{"id":"378","title":"پاسا"},{"id":"753","title":"پاکشو"},{"id":"174","title":"پتایر"},{"id":"602","title":"پترول"},{"id":"319","title":"پخش"},{"id":"177","title":"پدرخش"},{"id":"2182","title":"پرداخت"},{"id":"308","title":"پردیس"},{"id":"180","title":"پسهند"},{"id":"285","title":"پکرمان"},{"id":"539","title":"پکویر"},{"id":"178","title":"پلاست"},{"id":"179","title":"پلاسک"},{"id":"360","title":"پلوله"},{"id":"726","title":"تابا"},{"id":"4107","title":"تاپکیش"},{"id":"515","title":"تاپیکو"},{"id":"5559","title":"تاصیکو"},{"id":"191","title":"تایرا"},{"id":"4073","title":"تبرک"},{"id":"270","title":"تپکو"},{"id":"188","title":"تپمپی"},{"id":"472","title":"تپولا"},{"id":"195","title":"تشتاد"},{"id":"392","title":"تکشا"},{"id":"203","title":"تکمبا"},{"id":"674","title":"تکنار"},{"id":"406","title":"تکنو"},{"id":"1543","title":"تلیسه"},{"id":"356","title":"تمحرکه"},{"id":"7803","title":"تملت"},{"id":"5111","title":"تنوین"},{"id":"606","title":"توریل"},{"id":"490","title":"تیپیکو"},{"id":"427","title":"ثاباد"},{"id":"428","title":"ثاخت"},{"id":"445","title":"ثاژن"},{"id":"353","title":"ثاصفا"},{"id":"1576","title":"ثالوند"},{"id":"312","title":"ثامان"},{"id":"619","title":"ثباغ"},{"id":"585","title":"ثپردیس"},{"id":"587","title":"ثتران"},{"id":"1548","title":"ثتوسا"},{"id":"486","title":"ثجوان"},{"id":"584","title":"ثرود"},{"id":"664","title":"ثزاگرس"},{"id":"448","title":"ثشاهد"},{"id":"586","title":"ثشرق"},{"id":"529","title":"ثعتما"},{"id":"588","title":"ثعمرا"},{"id":"739","title":"ثغرب"},{"id":"400","title":"ثفارس"},{"id":"561","title":"ثقزوی"},{"id":"396","title":"ثمسکن"},{"id":"719","title":"ثنظام"},{"id":"745","title":"ثنور"},{"id":"425","title":"ثنوسا"},{"id":"712","title":"جم"},{"id":"5525","title":"جمپیلن"},{"id":"578","title":"جهرم"},{"id":"133","title":"چافست"},{"id":"2537","title":"چدن"},{"id":"126","title":"چفیبر"},{"id":"1768","title":"چکاپا"},{"id":"127","title":"چکارن"},{"id":"238","title":"چکاوه"},{"id":"686","title":"حاریا"},{"id":"2544","title":"حآسا"},{"id":"503","title":"حبندر"},{"id":"1771","title":"حپارسا"},{"id":"380","title":"حپترو"},{"id":"424","title":"حتاید"},{"id":"410","title":"حتوکا"},{"id":"595","title":"حخزر"},{"id":"426","title":"حرهشا"},{"id":"1569","title":"حریل"},{"id":"2036","title":"حسیر"},{"id":"750","title":"حسینا"},{"id":"635","title":"حفارس"},{"id":"532","title":"حفاری"},{"id":"481","title":"حکشتی"},{"id":"350","title":"خاذین"},{"id":"265","title":"خاهن"},{"id":"169","title":"خاور"},{"id":"652","title":"خبازرس"},{"id":"431","title":"خپویش"},{"id":"296","title":"ختراک"},{"id":"279","title":"ختور"},{"id":"163","title":"ختوقا"},{"id":"194","title":"خچرخش"},{"id":"570","title":"خدیزل"},{"id":"340","title":"خراسان"},{"id":"280","title":"خریخت"},{"id":"397","title":"خرینگ"},{"id":"315","title":"خزامیا"},{"id":"275","title":"خزر"},{"id":"394","title":"خشرق"},{"id":"284","title":"خصدرا"},{"id":"749","title":"خعمرا"},{"id":"562","title":"خفناور"},{"id":"266","title":"خفنر"},{"id":"730","title":"خفولا"},{"id":"267","title":"خکار"},{"id":"170","title":"خکاوه"},{"id":"2840","title":"خکرمان"},{"id":"276","title":"خکمک"},{"id":"162","title":"خلنت"},{"id":"164","title":"خمحرکه"},{"id":"441","title":"خمحور"},{"id":"383","title":"خموتور"},{"id":"433","title":"خنصیر"},{"id":"508","title":"خودکفا"},{"id":"165","title":"خوساز"},{"id":"15","title":"دابور"},{"id":"579","title":"داراب"},{"id":"357","title":"دارو"},{"id":"20","title":"داسوه"},{"id":"251","title":"دالبر"},{"id":"25","title":"دامین"},{"id":"502","title":"دانا"},{"id":"4616","title":"داوه"},{"id":"622","title":"دبالک"},{"id":"27","title":"دپارس"},{"id":"358","title":"دتماد"},{"id":"6701","title":"دتوزیع"},{"id":"564","title":"دتولید"},{"id":"14","title":"دجابر"},{"id":"343","title":"دحاوی"},{"id":"12","title":"ددام"},{"id":"16","title":"درازک"},{"id":"6274","title":"درهآور"},{"id":"395","title":"دروز"},{"id":"347","title":"دزهراوی"},{"id":"1553","title":"دسانکو"},{"id":"362","title":"دسبحا"},{"id":"565","title":"دسبحان"},{"id":"23","title":"دسینا"},{"id":"13","title":"دشیری"},{"id":"359","title":"دشیمی"},{"id":"293","title":"دعبید"},{"id":"24","title":"دفارا"},{"id":"326","title":"دفرا"},{"id":"221","title":"دقاضی"},{"id":"5728","title":"دکپسول"},{"id":"22","title":"دکوثر"},{"id":"17","title":"دکیمی"},{"id":"318","title":"دلر"},{"id":"469","title":"دماوند"},{"id":"577","title":"دهدشت"},{"id":"607","title":"دی"},{"id":"354","title":"دیران"},{"id":"246","title":"ذوب"},{"id":"369","title":"رانفور"},{"id":"592","title":"رتاپ"},{"id":"408","title":"رتکو"},{"id":"666","title":"رکیش"},{"id":"442","title":"رمپنا"},{"id":"6273","title":"رنیک"},{"id":"2662","title":"ریشمک"},{"id":"648","title":"زاگرس"},{"id":"2909","title":"زبینا"},{"id":"4432","title":"زپارس"},{"id":"4714","title":"زدشت"},{"id":"5347","title":"زشریف"},{"id":"2084","title":"زشگزا"},{"id":"5306","title":"زفکا"},{"id":"2967","title":"زقیام"},{"id":"4473","title":"زکشت"},{"id":"7802","title":"زکوثر"},{"id":"1893","title":"زگلدشت"},{"id":"5178","title":"زماهان"},{"id":"625","title":"زنجان"},{"id":"542","title":"زنگان"},{"id":"121","title":"ساذری"},{"id":"304","title":"ساراب"},{"id":"331","title":"ساربیل"},{"id":"1585","title":"ساروج"},{"id":"117","title":"ساروم"},{"id":"510","title":"سامان"},{"id":"399","title":"ساوه"},{"id":"118","title":"سایرا"},{"id":"566","title":"ساینا"},{"id":"746","title":"سباقر"},{"id":"391","title":"سبجنو"},{"id":"623","title":"سبزوا"},{"id":"373","title":"سبهان"},{"id":"4076","title":"سپ"},{"id":"247","title":"سپاها"},{"id":"120","title":"سپرمی"},{"id":"111","title":"ستران"},{"id":"557","title":"سجام"},{"id":"309","title":"سخاش"},{"id":"115","title":"سخزر"},{"id":"682","title":"سخواف"},{"id":"589","title":"سخوز"},{"id":"504","title":"سدبیر"},{"id":"454","title":"سدشت"},{"id":"364","title":"سدور"},{"id":"482","title":"سرچشمه"},{"id":"302","title":"سرود"},{"id":"114","title":"سشرق"},{"id":"112","title":"سشمال"},{"id":"384","title":"سصفها"},{"id":"113","title":"سصوفی"},{"id":"109","title":"سغرب"},{"id":"420","title":"سفار"},{"id":"108","title":"سفارس"},{"id":"381","title":"سفارود"},{"id":"220","title":"سفاسی"},{"id":"517","title":"سفانو"},{"id":"248","title":"سقاین"},{"id":"297","title":"سکارون"},{"id":"249","title":"سکرد"},{"id":"110","title":"سکرما"},{"id":"706","title":"سلار"},{"id":"116","title":"سمازن"},{"id":"641","title":"سمایه"},{"id":"678","title":"سمتاز"},{"id":"523","title":"سمگا"},{"id":"545","title":"سنوین"},{"id":"449","title":"سنیر"},{"id":"236","title":"سهرمز"},{"id":"372","title":"سهگمت"},{"id":"479","title":"سیدکو"},{"id":"563","title":"سیستم"},{"id":"389","title":"سیلام"},{"id":"716","title":"سیمرغ"},{"id":"323","title":"شاراک"},{"id":"252","title":"شاملا"},{"id":"710","title":"شاوان"},{"id":"492","title":"شبریز"},{"id":"629","title":"شبصیر"},{"id":"591","title":"شبندر"},{"id":"8","title":"شپارس"},{"id":"509","title":"شپاس"},{"id":"11","title":"شپاکسا"},{"id":"185","title":"شپترو"},{"id":"636","title":"شپدیس"},{"id":"484","title":"شپنا"},{"id":"637","title":"شتران"},{"id":"603","title":"شتوکا"},{"id":"281","title":"شتولی"},{"id":"700","title":"شجم"},{"id":"301","title":"شخارک"},{"id":"234","title":"شدوص"},{"id":"696","title":"شراز"},{"id":"672","title":"شرانل"},{"id":"7","title":"شرنگی"},{"id":"478","title":"شسپا"},{"id":"713","title":"شستان"},{"id":"429","title":"شسم"},{"id":"222","title":"شسینا"},{"id":"6836","title":"شصدف"},{"id":"322","title":"شصفها"},{"id":"475","title":"شغدیر"},{"id":"2867","title":"شفا"},{"id":"186","title":"شفارا"},{"id":"292","title":"شفارس"},{"id":"459","title":"شفن"},{"id":"336","title":"شکبیر"},{"id":"184","title":"شکربن"},{"id":"4","title":"شکف"},{"id":"374","title":"شکلر"},{"id":"2054","title":"شگامرن"},{"id":"338","title":"شگل"},{"id":"7801","title":"شگویا"},{"id":"531","title":"شلرد"},{"id":"91","title":"شلعاب"},{"id":"217","title":"شمواد"},{"id":"182","title":"شنفت"},{"id":"4081","title":"شوینده"},{"id":"230","title":"شیران"},{"id":"7831","title":"صبا"},{"id":"423","title":"غاذر"},{"id":"416","title":"غالبر"},{"id":"5524","title":"غبهار"},{"id":"213","title":"غبهنوش"},{"id":"815","title":"غپآذر"},{"id":"34","title":"غپاک"},{"id":"4542","title":"غپونه"},{"id":"31","title":"غپینو"},{"id":"35","title":"غچین"},{"id":"40","title":"غدام"},{"id":"1551","title":"غدیس"},{"id":"352","title":"غسالم"},{"id":"407","title":"غشاذر"},{"id":"434","title":"غشان"},{"id":"32","title":"غشهد"},{"id":"1577","title":"غشهداب"},{"id":"210","title":"غشوکو"},{"id":"44","title":"غصینو"},{"id":"528","title":"غفارس"},{"id":"30","title":"غگرجی"},{"id":"2056","title":"غگز"},{"id":"313","title":"غگل"},{"id":"1963","title":"غگلپا"},{"id":"1892","title":"غگلستا"},{"id":"33","title":"غمارگ"},{"id":"216","title":"غمهرا"},{"id":"732","title":"غمینو"},{"id":"45","title":"غنوش"},{"id":"205","title":"فاراک"},{"id":"288","title":"فاسمین"},{"id":"725","title":"فافزا"},{"id":"143","title":"فالوم"},{"id":"147","title":"فاما"},{"id":"157","title":"فاهواز"},{"id":"240","title":"فایرا"},{"id":"160","title":"فباهنر"},{"id":"158","title":"فبیرا"},{"id":"144","title":"فپنتا"},{"id":"159","title":"فجام"},{"id":"415","title":"فجر"},{"id":"458","title":"فخاس"},{"id":"455","title":"فخوز"},{"id":"535","title":"فرابورس"},{"id":"355","title":"فرآور"},{"id":"250","title":"فروس"},{"id":"7129","title":"فروی"},{"id":"683","title":"فزرین"},{"id":"580","title":"فسا"},{"id":"5522","title":"فسازان"},{"id":"351","title":"فسپا"},{"id":"403","title":"فسدید"},{"id":"514","title":"فلات"},{"id":"148","title":"فلامی"},{"id":"152","title":"فلوله"},{"id":"439","title":"فمراد"},{"id":"451","title":"فملی"},{"id":"645","title":"فن آوا"},{"id":"2005","title":"فنفت"},{"id":"149","title":"فنوال"},{"id":"145","title":"فنورد"},{"id":"385","title":"فوکا"},{"id":"453","title":"فولاد"},{"id":"569","title":"فولاژ"},{"id":"624","title":"فولای"},{"id":"306","title":"قاسم"},{"id":"225","title":"قپیرا"},{"id":"47","title":"قثابت"},{"id":"150","title":"قجام"},{"id":"332","title":"قچار"},{"id":"7697","title":"قرن"},{"id":"314","title":"قزوین"},{"id":"224","title":"قشکر"},{"id":"226","title":"قشهد"},{"id":"227","title":"قشیر"},{"id":"53","title":"قلرست"},{"id":"48","title":"قمرو"},{"id":"52","title":"قنقش"},{"id":"50","title":"قنیشا"},{"id":"55","title":"قهکمت"},{"id":"97","title":"کابگن"},{"id":"371","title":"کاذر"},{"id":"533","title":"کازرو"},{"id":"491","title":"کاسپین"},{"id":"537","title":"کالا"},{"id":"404","title":"کاما"},{"id":"767","title":"کاوه"},{"id":"98","title":"کایتا"},{"id":"273","title":"کبافق"},{"id":"107","title":"کپارس"},{"id":"724","title":"کپرور"},{"id":"96","title":"کپشیر"},{"id":"339","title":"کترام"},{"id":"605","title":"کتوکا"},{"id":"382","title":"کچاد"},{"id":"123","title":"کحافظ"},{"id":"245","title":"کخاک"},{"id":"335","title":"کدما"},{"id":"432","title":"کرازی"},{"id":"346","title":"کرماشا"},{"id":"228","title":"کرمان"},{"id":"414","title":"کساپا"},{"id":"295","title":"کساوه"},{"id":"92","title":"کسرام"},{"id":"122","title":"کسعدی"},{"id":"668","title":"کشرق"},{"id":"651","title":"کصدف"},{"id":"405","title":"کطبس"},{"id":"307","title":"کفپارس"},{"id":"620","title":"کفرآور"},{"id":"94","title":"کقزوی"},{"id":"104","title":"کگاز"},{"id":"419","title":"کگل"},{"id":"7536","title":"کلر"},{"id":"106","title":"کلوند"},{"id":"105","title":"کماسه"},{"id":"540","title":"کمرجان"},{"id":"363","title":"کمنگنز"},{"id":"1769","title":"کمینا"},{"id":"463","title":"کنور"},{"id":"409","title":"کهرام"},{"id":"499","title":"کوثر"},{"id":"6874","title":"کویر"},{"id":"468","title":"کی بی سی"},{"id":"699","title":"کیمیا"},{"id":"1568","title":"گپارس"},{"id":"2192","title":"گکوثر"},{"id":"695","title":"گکیش"},{"id":"1564","title":"گوهران"},{"id":"200","title":"لابسا"},{"id":"208","title":"لازما"},{"id":"264","title":"لبوتان"},{"id":"88","title":"لپارس"},{"id":"89","title":"لپیام"},{"id":"207","title":"لخزر"},{"id":"197","title":"لسرما"},{"id":"421","title":"لکما"},{"id":"5225","title":"لوتوس"},{"id":"638","title":"ما"},{"id":"4431","title":"مادیرا"},{"id":"485","title":"مارون"},{"id":"500","title":"مبین"},{"id":"344","title":"مداران"},{"id":"446","title":"مرقام"},{"id":"568","title":"معیار "},{"id":"644","title":"مفاخر"},{"id":"465","title":"ملت"},{"id":"530","title":"ممسنی"},{"id":"507","title":"میدکو"},{"id":"534","title":"میهن"},{"id":"2670","title":"نطرین"},{"id":"73","title":"نمرینو"},{"id":"673","title":"نوری"},{"id":"543","title":"نوین"},{"id":"576","title":"نیرو"},{"id":"2795","title":"های وب"},{"id":"325","title":"هجرت"},{"id":"494","title":"هرمز"},{"id":"747","title":"وآتوس"},{"id":"436","title":"واتی"},{"id":"685","title":"واحصا"},{"id":"527","title":"واحیا"},{"id":"324","title":"وآذر"},{"id":"518","title":"وارس"},{"id":"621","title":"وآرین"},{"id":"166","title":"واعتبار"},{"id":"667","title":"وآفری"},{"id":"61","title":"والبر"},{"id":"456","title":"وامید"},{"id":"6839","title":"وآوا"},{"id":"229","title":"وایرا"},{"id":"546","title":"وایران"},{"id":"743","title":"وآیند"},{"id":"65","title":"وبانک"},{"id":"2039","title":"وبرق"},{"id":"59","title":"وبشهر"},{"id":"498","title":"وبصادر"},{"id":"462","title":"وبملت"},{"id":"430","title":"وبهمن"},{"id":"386","title":"وبوعلی"},{"id":"321","title":"وبیمه"},{"id":"519","title":"وپاسار"},{"id":"21","title":"وپخش"},{"id":"634","title":"وپسا"},{"id":"604","title":"وپست"},{"id":"461","title":"وتجارت"},{"id":"701","title":"وتعاون"},{"id":"387","title":"وتوس"},{"id":"437","title":"وتوسم"},{"id":"56","title":"وتوشه"},{"id":"316","title":"وتوصا"},{"id":"320","title":"وثخوز"},{"id":"483","title":"وثنو"},{"id":"552","title":"وثوق"},{"id":"1567","title":"وجامی"},{"id":"752","title":"وحافظ"},{"id":"1953","title":"وحکمت"},{"id":"470","title":"وخارزم"},{"id":"708","title":"وخاور"},{"id":"512","title":"ودی"},{"id":"631","title":"وزمین"},{"id":"9","title":"وساپا"},{"id":"290","title":"وساخت"},{"id":"742","title":"وسالت"},{"id":"480","title":"وسبحان"},{"id":"63","title":"وسپه"},{"id":"232","title":"وسدید"},{"id":"741","title":"وسرمد"},{"id":"684","title":"وسکاب"},{"id":"608","title":"وسنا"},{"id":"735","title":"وسین"},{"id":"457","title":"وسینا"},{"id":"640","title":"وشمال"},{"id":"401","title":"وصنا"},{"id":"334","title":"وصندوق"},{"id":"294","title":"وصنعت"},{"id":"66","title":"وغدیر"},{"id":"473","title":"وکادو"},{"id":"333","title":"وکار"},{"id":"547","title":"وگستر"},{"id":"599","title":"ولانا"},{"id":"571","title":"ولبهمن"},{"id":"698","title":"ولتجار"},{"id":"661","title":"ولراز"},{"id":"444","title":"ولساپا"},{"id":"2671","title":"ولشرق"},{"id":"435","title":"ولصنم"},{"id":"1531","title":"ولملت"},{"id":"418","title":"ولیز"},{"id":"702","title":"ومشان"},{"id":"4689","title":"ومعلم"},{"id":"64","title":"وملت"},{"id":"639","title":"وملل"},{"id":"60","title":"وملی"},{"id":"7209","title":"ومهان"},{"id":"337","title":"ونفت"},{"id":"417","title":"ونیرو"},{"id":"62","title":"ونیکی"},{"id":"681","title":"وهور"}]' );
    var objRahavard=jQuery.parseJSON('[{"id":"IRO1BPAR0001","title":"وپارس1"},{"id":"IRO1PKLJ0001","title":"فارس1"},{"id":"IRO1BARZ0001","title":"پكرمان1"},{"id":"IRO1SFKZ0001","title":"سفارس1"},{"id":"IRO1SHOY0001","title":"شوينده1"},{"id":"IRR1BDAN0101","title":"داناح1"},{"id":"IRO1BMEL0001","title":"ملت1"},{"id":"IRO1PFAN0001","title":"شفن1"},{"id":"IRO1PARK0001","title":"شاراك1"},{"id":"IRO1PRDZ0001","title":"شپديس1"},{"id":"IRO1TAIR0001","title":"پتاير1"},{"id":"IRO1KSIM0001","title":"فاسمين1"},{"id":"IRO1FKAS0001","title":"فخاس1"},{"id":"IRO1INFO0001","title":"رانفور1"},{"id":"IRO1BOTA0001","title":"لبوتان1"},{"id":"IRO1DJBR0001","title":"دجابر1"},{"id":"IRO1PNBA0001","title":"شبندر1"},{"id":"IRO1TMEL0001","title":"وتوسم1"},{"id":"IRO1NPRS0001","title":"شنفت1"},{"id":"IRO1KRSN0001","title":"خراسان1"},{"id":"IRO1CIDC0001","title":"سيدكو1"},{"id":"IRO1NIRO0001","title":"بنيرو1"},{"id":"IRO1SGRB0001","title":"سغرب1"},{"id":"IRO1VLMT0001","title":"ولملت1"},{"id":"IRO1KCHI0001","title":"كخاك1"},{"id":"IRO1SSIN0001","title":"شيران1"},{"id":"IRO1RKSH0001","title":"ركيش1"},{"id":"IRO1NMOH0001","title":"خمحركه1"},{"id":"IRO1SHFA0001","title":"شفا1"},{"id":"IRO1PIRN0001","title":"تپمپي1"},{"id":"IRO1PRKT0001","title":"پرداخت1"},{"id":"IRO1DRZK0001","title":"درازك1"},{"id":"IRO1MINO0001","title":"غپينو1"},{"id":"IRO1NOVN0001","title":"ونوين1"},{"id":"IRO1DSBH0001","title":"دسبحان1"},{"id":"IRO1INDM0001","title":"خكمك1"},{"id":"IRO1TMKH0001","title":"خمحور1"},{"id":"IRO1ATDM0001","title":"واتي1"},{"id":"IRO1PASN0001","title":"پارسان1"},{"id":"IRO1ATIR0001","title":"خاهن1"},{"id":"IRO1CHML0001","title":"كچاد1"},{"id":"IRO1PTEH0001","title":"شتران1"},{"id":"IRO1GOLG0001","title":"كگل1"},{"id":"IRO1SADR0001","title":"تاصيكو1"},{"id":"IRO1PNTB0001","title":"شبريز1"},{"id":"IRO1FIBR0001","title":"چفيبر1"},{"id":"IRO1DRKH0001","title":"پدرخش1"},{"id":"IRO1TRIR0001","title":"تايرا1"},{"id":"IRO1GMRO0001","title":"قمرو1"},{"id":"IRO1GDIR0001","title":"وغدير1"},{"id":"IRO1BTEJ0001","title":"وتجارت1"},{"id":"IRO1SORB0001","title":"فسرب1"},{"id":"IRO1TSHE0001","title":"وتوشه1"},{"id":"IRO1BDAN0001","title":"دانا1"},{"id":"IRO1EPRS0001","title":"رتاپ1"},{"id":"IRO1DARO0001","title":"وپخش1"},{"id":"IRO1BKHZ0001","title":"وخاور1"},{"id":"IRO1KRTI0001","title":"چكارن1"},{"id":"IRO1BANK0001","title":"وبانك1"},{"id":"IRO1IKHR0001","title":"وخارزم1"},{"id":"IRO1DOSE0001","title":"داسوه1"},{"id":"IRO1RINM0001","title":"خرينگ1"},{"id":"IRO1PTAP0001","title":"تاپيكو1"},{"id":"IRO1SHFS0001","title":"شفارس1"},{"id":"IRO1SAHD0001","title":"ثشاهد1"},{"id":"IRO1OFRS0001","title":"ثفارس1"},{"id":"IRO1GGAZ0001","title":"قزوين1"},{"id":"IRO1PNES0001","title":"شپنا1"},{"id":"IRO1KHOC0001","title":"سخوز1"},{"id":"IRO1BMLT0001","title":"وبملت1"},{"id":"IRO1BSDR0001","title":"وبصادر1"},{"id":"IRO1DKSR0001","title":"دكوثر1"},{"id":"IRO1ABDI0001","title":"دعبيد1"},{"id":"IRO1SLMN0001","title":"غسالم1"},{"id":"IRO1LTOS0001","title":"لوتوس1"},{"id":"IRO1KRIR0001","title":"خكار1"},{"id":"IRO1SKER0001","title":"سكرما1"},{"id":"IRO1SSAP0001","title":"وساپا1"},{"id":"IRR1NPRS0101","title":"شنفتح1"},{"id":"IRO1MNGZ0001","title":"كمنگنز1"},{"id":"IRO1EXIR0001","title":"دلر1"},{"id":"IRO1FOLD0001","title":"فولاد1"},{"id":"IRO1SMAZ0001","title":"سمازن1"},{"id":"IRO1DTIP0001","title":"تيپيكو1"},{"id":"IRO1RSAP0001","title":"ولساپا1"},{"id":"IRO1KVEH0001","title":"كاوه1"},{"id":"IRO1KIMI0001","title":"دكيمي1"},{"id":"IRO1KGND0001","title":"بكام1"},{"id":"IRO1MARK0001","title":"فاراك1"},{"id":"IRO1MAGS0001","title":"زمگسا1"},{"id":"IRO1TKNO0001","title":"تكنو1"},{"id":"IRO1TOKA0001","title":"وتوكا1"},{"id":"IRO1BALB0001","title":"البرز1"},{"id":"IRO1HSHM0001","title":"حفاري1"},{"id":"IRO1MSMI0001","title":"فملي1"},{"id":"IRO1NOSH0001","title":"غنوش1"},{"id":"IRO1SDOR0001","title":"سدور1"},{"id":"IRO1KHSH0001","title":"خشرق1"},{"id":"IRO1HTOK0001","title":"حتوكا1"},{"id":"IRO1GSKE0001","title":"تكشا1"},{"id":"IRO1CHAR0001","title":"خچرخش1"},{"id":"IRO1PJMZ0001","title":"جم1"},{"id":"IRO1MOBN0001","title":"مبين1"},{"id":"IRO1AYEG0001","title":"پرديس1"},{"id":"IRO1GARN0001","title":"قرن1"},{"id":"IRO1PLKK0001","title":"پلاسك1"},{"id":"IRO1HJPT0001","title":"حپترو1"},{"id":"IRO1PDRO0001","title":"دپارس1"},{"id":"IRO1BPAS0001","title":"وپاسار1"},{"id":"IRO1BRKT0001","title":"بركت1"},{"id":"IRO1SHZG0001","title":"سهرمز1"},{"id":"IRO1BALI0001","title":"وبوعلي1"},{"id":"IRO1ALIR0001","title":"فايرا1"},{"id":"IRO1TKIN0001","title":"رتكو1"},{"id":"IRO1SKAZ0001","title":"سخزر1"},{"id":"IRO1LZIN0001","title":"وليز1"},{"id":"IRO1BHMN0001","title":"خبهمن1"},{"id":"IRO1YASA0001","title":"پاسا1"},{"id":"IRO1SSOF0001","title":"سصوفي1"},{"id":"IRO1SHGN0001","title":"سهگمت1"},{"id":"IRO1SEFH0001","title":"سصفها1"},{"id":"IRO1SSHR0001","title":"سشرق1"},{"id":"IRO1DALZ0001","title":"دالبر1"},{"id":"IRO1BAMA0001","title":"كاما1"},{"id":"IRO1FAIR0001","title":"فولاژ1"},{"id":"IRO1SIMS0001","title":"سشمال1"},{"id":"IRO1ABAD0001","title":"ثاباد1"},{"id":"IRO1TBAS0001","title":"كطبس1"},{"id":"IRO1SSEP0001","title":"سپاها1"},{"id":"IRO1TMLT0001","title":"تملت1"},{"id":"IRO1GESF0001","title":"قصفها1"},{"id":"IRO1DSOB0001","title":"دسبحا1"},{"id":"IRO1ASAL0001","title":"لابسا1"},{"id":"IRO1KPRS0001","title":"كپارس1"},{"id":"IRO1SEIL0001","title":"سيلام1"},{"id":"IRO1LIRZ0001","title":"وايران1"},{"id":"IRO1LMIR0001","title":"فلوله1"},{"id":"IRO1KSHJ0001","title":"حكشتي1"},{"id":"IRO1BAFG0001","title":"كبافق1"},{"id":"IRO1FAJR0001","title":"فجر1"},{"id":"IRO1PKHA0001","title":"شخارك1"},{"id":"IRO1BSTE0001","title":"ثاخت1"},{"id":"IRO1ASIA0001","title":"آسيا1"},{"id":"IRO1CRBN0001","title":"شكربن1"},{"id":"IRO1SSNR0001","title":"سنير1"},{"id":"IRO1JOSH0001","title":"بكاب1"},{"id":"IRO1SKBV0001","title":"وسكاب1"},{"id":"IRO1SEPK0001","title":"سپ1"},{"id":"IRO1DFRB0001","title":"دفارا1"},{"id":"IRO1MHKM0001","title":"خمهر1"},{"id":"IRO1LKGH0001","title":"ولغدر1"},{"id":"IRO1SRMA0001","title":"لسرما1"},{"id":"IRO1DABO0001","title":"دابور1"},{"id":"IRO1BVMA0001","title":"ما1"},{"id":"IRO1DLGM0001","title":"دلقما1"},{"id":"IRO1SPAH0001","title":"وسپه1"},{"id":"IRO1DMVN0001","title":"كدما1"},{"id":"IRO1ZMYD0001","title":"خزاميا1"},{"id":"IRO1FKHZ0001","title":"فخوز1"},{"id":"IRO1GNBO0001","title":"قنيشا1"},{"id":"IRO1KRAF0001","title":"وكار1"},{"id":"IRO1RENA0001","title":"ورنا1"},{"id":"IRO1SAMA0001","title":"فاما1"},{"id":"IRO1SAKH0001","title":"وساخت1"},{"id":"IRO1SHSI0001","title":"شسينا1"},{"id":"IRO1BENN0001","title":"غبهنوش1"},{"id":"IRO1DZAH0001","title":"دزهراوي1"},{"id":"IRO1SHND0001","title":"پسهند1"},{"id":"IRO1HWEB0001","title":"هاي وب1"},{"id":"IRO1SEPP0001","title":"شسپا1"},{"id":"IRO1MAPN0001","title":"رمپنا1"},{"id":"IRO1OMID0001","title":"اميد1"},{"id":"IRO1LENT0001","title":"خلنت1"},{"id":"IRO1PSHZ0001","title":"شيراز1"},{"id":"IRO1AMIN0001","title":"دامين1"},{"id":"IRO1GHND0001","title":"قشهد1"},{"id":"IRO1MKBT0001","title":"اخابر1"},{"id":"IRO1MSKN0001","title":"ثمسكن1"},{"id":"IRO1COMB0001","title":"تكمبا1"},{"id":"IRO1HFRS0001","title":"حفارس1"},{"id":"IRO1HMRZ0001","title":"همراه1"},{"id":"IRO1TAZB0001","title":"وآذر1"},{"id":"IRO1NALM0001","title":"فنوال1"},{"id":"IRO1PMSZ0001","title":"ثشرق1"},{"id":"IRO1DODE0001","title":"شدوص1"},{"id":"IRO1SDAB0001","title":"ساراب1"},{"id":"IRO1DPAK0001","title":"دارو1"},{"id":"IRO1LPAK0001","title":"غپاك1"},{"id":"IRO1PSER0001","title":"كسرام1"},{"id":"IRO1ALBZ0001","title":"والبر1"},{"id":"IRO1SGEN0001","title":"سقاين1"},{"id":"IRO1ROOI0001","title":"كروي1"},{"id":"IRO1SBHN0001","title":"سبهان1"},{"id":"IRO1FTIR0001","title":"دفرا1"},{"id":"IRO1SBOJ0001","title":"سبجنو1"},{"id":"IRO1BPST0001","title":"وپست1"},{"id":"IRO1GHEG0001","title":"قهكمت1"},{"id":"IRO1SHPZ0001","title":"غشاذر1"},{"id":"IRO1KALZ0001","title":"بالبر1"},{"id":"IRO1KHFZ0001","title":"كحافظ1"},{"id":"IRO1NAFT0001","title":"ونفت1"},{"id":"IRO1GHAT0001","title":"ختوقا1"},{"id":"IRO1TAMN0001","title":"شستا1"},{"id":"IRO1IAGM0001","title":"مرقام1"},{"id":"IRO1MRIN0001","title":"نمرينو1"},{"id":"IRO1KLBR0001","title":"غالبر1"},{"id":"IRO1NBEH0001","title":"شبهرن1"},{"id":"IRO1OIMC0001","title":"واميد1"},{"id":"IRO1TNOV0001","title":"تنوين1"},{"id":"IRO1DSIN0002","title":"دسينا2"},{"id":"IRO1PARS0001","title":"پارس1"},{"id":"IRO1AZIN0001","title":"خاذين1"},{"id":"IRO1SROD0001","title":"سرود1"},{"id":"IRO1RTIR0001","title":"ختراك1"},{"id":"IRO1TMVD0001","title":"دتماد1"},{"id":"IRO1NGFO0001","title":"فنورد1"},{"id":"IRO1FSAZ0001","title":"فسازان1"},{"id":"IRO1DSIN0001","title":"دسينا1"},{"id":"IRO1LAPS0001","title":"بشهاب1"},{"id":"IRO1SNMA0001","title":"وصنعت1"},{"id":"IRO1DADE0001","title":"مداران1"},{"id":"IRO1IPAR0001","title":"پارسيان1"},{"id":"IRO1SPKH0001","title":"غشان1"},{"id":"IRO1CHCH0001","title":"غچين1"},{"id":"IRO1PKER0001","title":"كرماشا1"},{"id":"IRO1BIME0001","title":"وبيمه1"},{"id":"IRO1ROZD0001","title":"دروز1"},{"id":"IRO1SISH0001","title":"كساپا1"},{"id":"IRO1KFAN0001","title":"خفنر1"},{"id":"IRO1FRVR0001","title":"فرآور1"},{"id":"IRO1SADB0001","title":"ساربيل1"},{"id":"IRO1MSTI0001","title":"خموتور1"},{"id":"IRO1SZPO0001","title":"خپويش1"},{"id":"IRO1AZAB0001","title":"فاذر1"},{"id":"IRO1DAML0001","title":"ددام1"},{"id":"IRO1TAMI0001","title":"كماسه1"},{"id":"IRO1KSAD0001","title":"كسعدي1"},{"id":"IRO1ZPRS0001","title":"زپارس1"},{"id":"IRO1TOSA0001","title":"وتوصا1"},{"id":"IRO1AMLH0001","title":"شاملا1"},{"id":"IRO1BROJ0001","title":"نبروج1"},{"id":"IRO1CHDN0001","title":"چدن1"},{"id":"IRO1LSMD0001","title":"ولصنم1"},{"id":"IRO1NKLA0001","title":"نكالا1"},{"id":"IRO1NORI0001","title":"نوري1"},{"id":"IRO1SBAH0001","title":"وبهمن1"},{"id":"IRO1SFRS0001","title":"سفار1"},{"id":"IRR1CHML0101","title":"كچادح1"},{"id":"IRO1GPSH0001","title":"قپيرا1"},{"id":"IRR1DALZ0101","title":"دالبرح1"},{"id":"IRO1PASH0001","title":"پاكشو1"},{"id":"IRO1KNRZ0001","title":"كنور1"},{"id":"IRO1LAMI0001","title":"فلامي1"},{"id":"IRO1NSPS0001","title":"كفپارس1"},{"id":"IRO1APPE0001","title":"آپ1"},{"id":"IRO1SMRG0001","title":"سيمرغ1"},{"id":"IRO1ALVN0001","title":"كلوند1"},{"id":"IRO1GSBE0001","title":"قثابت1"},{"id":"IRO1TRNS0001","title":"بترانس1"},{"id":"IRO1KHAZ0001","title":"لخزر1"},{"id":"IRO1NSTH0001","title":"ثنوسا1"},{"id":"IRO1PSIR0001","title":"كپشير1"},{"id":"IRO1JPPC0001","title":"جم پيلن1"},{"id":"IRO1FNAR0001","title":"خزر1"},{"id":"IRO1DDPK0001","title":"دشيمي1"},{"id":"IRO1ALMR0001","title":"فمراد1"},{"id":"IRO1BMPS0001","title":"شپارس1"},{"id":"IRO1GBEH0001","title":"وصنا1"},{"id":"IRO1TGOS0001","title":"وتوس1"},{"id":"IRO1SPTA0001","title":"فپنتا1"},{"id":"IRO1BFJR0001","title":"بفجر1"},{"id":"IRO1GCOZ0001","title":"غگل1"},{"id":"IRO1SYSM0001","title":"سيستم1"},{"id":"IRO1RIIR0001","title":"خريخت1"},{"id":"IRO1TSBE0001","title":"وبشهر1"},{"id":"IRO1MOTJ0001","title":"بموتو1"},{"id":"IRO1FRIS0001","title":"فروس1"},{"id":"IRO1IDOC0001","title":"واعتبار1"},{"id":"IRO1SGOS0001","title":"ثامان1"},{"id":"IRO1OFST0001","title":"چافست1"},{"id":"IRO1SBEH0001","title":"غبشهر1"},{"id":"IRO1SNRO0001","title":"ونيرو1"},{"id":"IRO1PETR0001","title":"وپترو1"},{"id":"IRO1NASI0001","title":"كفرا1"},{"id":"IRO1PKOD0001","title":"خپارس1"},{"id":"IRR1PASH0101","title":"پاكشوح1"},{"id":"IRO1GTSH0001","title":"شگل1"},{"id":"IRO1MESI0001","title":"خوساز1"},{"id":"IRO1KDPS0001","title":"غدام1"},{"id":"IRO1SDST0001","title":"سدشت1"},{"id":"IRO1MNSR0001","title":"خنصير1"},{"id":"IRO1TSRZ0001","title":"كرازي1"},{"id":"IRO1TKSM0001","title":"كترام1"},{"id":"IRO1STEH0001","title":"ستران1"},{"id":"IRO1RADI0001","title":"ختور1"},{"id":"IRO1TAYD0001","title":"حتايد1"},{"id":"IRO1SURO0001","title":"ساروم1"},{"id":"IRO1MRAM0001","title":"غمهرا1"},{"id":"IRO1JAMD0001","title":"فجام1"},{"id":"IRO1PAKS0001","title":"شپاكسا1"},{"id":"IRO1ARDK0001","title":"كسرا1"},{"id":"IRO1OFOG0001","title":"افق1"},{"id":"IRO1LEAB0001","title":"شلعاب1"},{"id":"IRO1SWIC0001","title":"بسويچ1"},{"id":"IRO1MNMH0001","title":"تمحركه1"},{"id":"IRO1KVIR0001","title":"كوير1"},{"id":"IRO1SEPA0001","title":"فسپا1"},{"id":"IRO1KSKA0001","title":"چكاوه1"},{"id":"IRO1PIAZ0001","title":"غاذر1"},{"id":"IRO1GORJ0001","title":"غگرجي1"},{"id":"IRO1SHKR0001","title":"قشكر1"},{"id":"IRO1SGAZ0001","title":"كگاز1"},{"id":"IRO1NSAZ0001","title":"كاذر1"},{"id":"IRO1SFNO0001","title":"سفانو1"},{"id":"IRO1SHMD0001","title":"كهمدا1"},{"id":"IRO1KALA0001","title":"كالا1"},{"id":"IRO1NKOL0001","title":"شكلر1"},{"id":"IRO1BORS0001","title":"بورس1"},{"id":"IRO1SHAD0001","title":"غشهد1"},{"id":"IRO1PELC0001","title":"لپارس1"},{"id":"IRO1KOSR0001","title":"زكوثر1"},{"id":"IRR1SEIL0101","title":"سيلامح1"},{"id":"IRR1SNRO0101","title":"ونيروح1"}]')
    var CreatePromise = []

    for (var i = 0; i <ListObj.length; i++) {
        // if(ListObj[i].NamadSahih=='فارس') 
        for (var j = 0; j < objRahavard.length; j++) {
            
            var convTitle=objRahavard[j].title.split("1")[0]
            convTitle=convTitle.split("2")[0]
            if( ListObj[i].NamadSahih==convTitle)
            {
                
                var obj={}
                obj.url="/Saham/UpdateNamad"
                obj.dataType="json"
                obj.type="post"
                obj.data={IdNamad:ListObj[i].Id,tseId:objRahavard[j].id}
                CreatePromise.push(service(obj))
                // console.log(objRahavard[j].title +" : "+objRahavard[j].id)
                // 
            }
        }
    }
     
    var results = await Promise.all(CreatePromise);

    
}

async function GetMoreMinusFive() {
    
    $.LoadingOverlay("show");
    /*
    const m = moment();
    var today = m.format('jYYYY/jM/jD');//Today
    today=convertDateToslashless(today)
    m.add(-1, 'day')
    var yesterday=m.format('jYYYY/jM/jD');//yesterday
    yesterday=convertDateToslashless(yesterday)
    */
    var obj={}
    obj.url="/Saham/MoreMinusFive"
    obj.dataType="json"
    obj.type="post"
   // obj.data={SortBy:SortBy,today:today,yesterday:yesterday}
    
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]
    // console.log(ListObj)
    ShowMoreMinusFive(ListObj)

    $.LoadingOverlay("hide");
}

async    function ShowMoreMinusFive(ListObj)
{
    
    var table = "<table class='table-bordered table-responsive' style='font-size:10px'>"+
      "<tr><th>نماد</th><th>تاریخ</th><th>آخرین حجم</th><th>میانگین حجم</th><th onclick='GetCompareToAvg(\""+"Rate"+"\")'>Rate</th>"+
      "<th onclick='GetCompareToAvg(\""+"SumDarsadGheymatPayany"+"\")'>مجموع</th><th onclick='GetCompareToAvg(\""+"TedadP"+"\")'>تعداد مثبت</th>"+
      "<th onclick='GetCompareToAvg(\""+"TedadM"+"\")'>تعداد منفی</th><th>جزئیات</th><th>tse</th></tr>"
    for (var i = 0; i <  ListObj.lstNamadVM.length; i++) {
        table += "<tr><td style='cursor:pointer' onclick='AddtseAddress("+ListObj.lstNamadVM[i].IdNamad+")'>"+ ListObj.lstNamadVM[i].NamadName+"</td>"+
           " <td>"+ foramtDate(ListObj.lstNamadVM[i].ShamsyDate)+"</td>"+
           "<td>"+ SeparateThreeDigits(ListObj.lstNamadVM[i].Hajm)+"</td>"+          
             "<td>"+ ListObj.lstNamadVM[i].DarsadGheymatPayany.toFixed(2)+"</td>"+
            "<td><input type='button' value='جزئیات' onclick='NamadDetail("+ListObj.lstNamadVM[i].IdNamad+")'/></td>"
        
        if(ListObj.lstNamadVM[i].tseAddress=="")
        {
            table +=  "<td><input style='background-color:red' type='button' value='tse' onclick='AddtseAddress("+ListObj.lstNamadVM[i].IdNamad+")'/></td>"
        }
        else
        {
            table +=  "<td><a href="+ListObj.lstNamadVM[i].tseAddress+" target='_blank'>نمایش</a></td>"
        }

        if(ListObj.lstNamadVM[i].IdRahavard=="")
        {
            table +=  "<td><a href="+ListObj.lstNamadVM[i].IdRahavard+" target='_blank'>تعریف نشده</a></td>"
        }
        else
        {
            table +=  "<td><a href="+"https://rahavard365.com/asset/"+ListObj.lstNamadVM[i].IdRahavard+" target='_blank'>رهاورد</a></td>"
        }
        table += "</tr>"

    }
    table +="</table>"
 
    $(".CompareToAvg").empty();
    $(".CompareToAvg").append(table);
    // $("#MasterModal").modal();

}
function getDateFromLocalStorage(){
    
 // var valuee=  $("input[name='DateForUpToDateSaham']").val();
    var DateForUpToDateSaham = localStorage.getItem("DateForUpToDateSaham");
    $("input[name='DateForUpToDateSaham']").val(DateForUpToDateSaham);
   // localStorage.setItem("DateForUpToDateSaham", DateForUpToDateSaham);
    ShamsyDateCount()

}
function DateForUpToDateSahamChange(thiss){
    
    localStorage.setItem("DateForUpToDateSaham",  thiss.value);
   
}
async function ShamsyDateCount() {
    $.LoadingOverlay("show");

    var obj = {}
    obj.url = "/Saham/ShamsyDateCount"
    obj.dataType = "json"
    obj.type = "post"
    //obj.data = { TedadRooz: TedadRooz }
    var results = await Promise.all([
        service(obj)
    ]);
    var ListObj = results[0]

    var table = "<table class='table table-responsive'>" +
        "<tr><th>تاریخ</th><th>تعداد</th></tr>"
    for (var i = 0; i < ListObj.lstNamadVM.length; i++) {
        table += "<tr>" +
            "<td>" + ListObj.lstNamadVM[i].ShamsyDate + "</td>" +
            "<td>" + ListObj.lstNamadVM[i].Tedad + "</td>" +
            "</tr>"

    }
    table += "</table>"

    $(".ShamsyDateCount").empty();
    $(".ShamsyDateCount").append(table);


    $.LoadingOverlay("hide");
}
function UploaderFile() {
    $.LoadingOverlay("show");
    // Checking whether FormData is available in browser  
    if (window.FormData !== undefined) {

        var fileUpload = $("#fileInput").get(0);
        var files = fileUpload.files;

        // Create FormData object  
        var fileData = new FormData();

        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        // Adding one more key to FormData object  
        //  fileData.append('username','Manas');  

        $.ajax({
            url: '/Saham/UploadFiles',
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            success: function (result) {
                ShamsyDateCount();
                alert(result);
                $.LoadingOverlay("hide");
              //  Sheet();
            },
            error: function (err) {
                console.log(err.statusText)
                alert(err.statusText);
            }
        });
    } else {
        alert("FormData is not supported.");
    }

}
//async function Sheet() {
//    var obj = {}
//    obj.url = "/Saham/Sheet"
//    obj.dataType = "json"
//    obj.type = "post"
//    // obj.data={TaskId:TaskId}

//    var results = await Promise.all([
//        service(obj)
//    ]);
//    var ListObj = results[0]
//    var message=""
//    for (var i = 0; i < ListObj.length; i++) {
//        message += "<p>" + ListObj[i].FileName+"</p>"
//    }



//    var tablebutt = "<table class='table' style='font-size: 9px;'>"
//    tablebutt += "<tr>" +
//        "<td><input type='button'  class='btn btn-danger' value='بستن' onclick='closeModal()'/></td>" +
//        "</tr>"
//    tablebutt += "</table>"

//    $(".modal-footer").empty();
//    $(".modal-footer").append(tablebutt);
//    $(".BodyModal").empty();
//    $(".BodyModal").append(message);
//    $("#MasterModal").modal();
//    SheetName()


    
//}
//async function SheetName() {
//    var obj = {}
//    obj.url = "/Saham/SheetName"
//    obj.dataType = "json"
//    obj.type = "post"
//    // obj.data={TaskId:TaskId}

//    var results = await Promise.all([
//        service(obj)
//    ]);
//    var ListObj = results[0]
//    

//    var obj = {}
//    obj.url = "/Saham/ExcelToDataTable"
//    obj.dataType = "json"
//    obj.type = "post"
//    // obj.data={TaskId:TaskId}

//    var results = await Promise.all([
//        service(obj)
//    ]);
//    var ListObj = results[0]
//    
    
//    //var message = ""
//    //for (var i = 0; i < ListObj.length; i++) {
//    //    message += "<p>" + ListObj[i].FileName + "</p>"
//    //}



//    //var tablebutt = "<table class='table' style='font-size: 9px;'>"
//    //tablebutt += "<tr>" +
//    //    "<td><input type='button'  class='btn btn-danger' value='بستن' onclick='closeModal()'/></td>" +
//    //    "</tr>"
//    //tablebutt += "</table>"

//    //$(".modal-footer").empty();
//    //$(".modal-footer").append(tablebutt);
//    //$(".BodyModal").empty();
//    //$(".BodyModal").append(message);
//    //$("#MasterModal").modal();



//    
//}
function closeModal() {
    $("#MasterModal").modal("toggle");
}
