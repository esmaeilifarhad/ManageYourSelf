//$(document).ready(function () {
var IsPause = true;
var myVar;
function ShowTime() {

    IsPause = false;
    myVar = setInterval(function () {
        if (IsPause != true)
            Timerr();
    }, 1000);
}
var Second = 0;
function Timerr() {
    Second = Second + 1;
    $(".T_Second").text(Second);
    $(".T_Minute").text(parseInt(Second / 60));
    $(".T_Hour").text(parseInt(Second / 3600));
}
function ChangeIsPause() {

    if (IsPause == false)
        IsPause = true
    else
        IsPause = false
}
function myStopFunction() {

    clearInterval(myVar);
}
function formatAMPM() {
    var date = new Date
    var hours = date.getHours();
    hours = hours < 10 ? '0' + hours : hours;
    var minutes = date.getMinutes();
    minutes = minutes < 10 ? '0' + minutes : minutes;
    //console.log(minutes)
    /*
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0'+minutes : minutes;
    */
    var strTime = hours + ':' + minutes;
    //alert(strTime);
    //return strTime;

    CreateSTime(0, strTime, "StartJobTime")
}
function formatAMPMEnd() {
    var date = new Date
    var hours = date.getHours();
    hours = hours < 10 ? '0' + hours : hours;
    var minutes = date.getMinutes();
    minutes = minutes < 10 ? '0' + minutes : minutes;
    //console.log(minutes)
    /*
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0'+minutes : minutes;
    */
    var strTime = hours + ':' + minutes;
    //alert(strTime);
    //return strTime;

    CreateSTime(1, strTime, "EndJobTime")
}
function CreateSTime(sta, value, key) {

    $.ajax(
      {
          type: 'POST',
          contentType: "application/json;charset=utf-8",
          dataType: "json",
          url: "/Setting/CreateStime",
          data: JSON.stringify({
              Value: value,
              Key: key
          }),
          success: function (result) {

              if (result.result == true) {
                  if (sta == 1) {
                      $(".T_EndTime input").val(result.message);
                  }
                  else {
                      $(".T_StartTime input").val(result.message);
                  }
                  CalMinute(result.message)
              }
              if (result.result == false) {
                  alert(result.message)
              }
          },
          error: function (error) {
              console.log(error);
          }
      });

}
function BindTimes() {
    CreateSTime(0, "0", "StartJobTime");
    CreateSTime(1, "1", "EndJobTime")
}
function CalMinute(thiss) {
    
    if ($(thiss).is("input")) {
    
        var res = $(thiss).val()
         res = res.replace(/\:/g, "")
        // res = res.replace(/-/g, "")
        $(thiss).val(res.slice(0, 2) + ":" + res.slice(2, 4))
    }
    var T1 = $(".T_StartTime input").val();
    var T2 = $(".T_EndTime input").val();
    T1 = T1.replace(":", "")
    T2 = T2.replace(":", "")

    //  alert(T1)
    var start_time = T1;
    var end_time = T2;

    var start_hour = start_time.slice(0, -2);
    var start_minutes = start_time.slice(-2);

    var end_hour = end_time.slice(0, -2);
    var end_minutes = end_time.slice(-2);

    var startDate = new Date(0, 0, 0, start_hour, start_minutes);
    var endDate = new Date(0, 0, 0, end_hour, end_minutes);

    var millis = endDate - startDate;
    var minutes = millis / 1000 / 60;
    // alert(minutes);

    // --output:--
   
    $(".T_MinTime input").val(minutes);


}
//function ConvertToTime(thiss) {
//    
//    var res = $(thiss).val()
//    res = res.replace(/:/g, "")
//    res = res.replace(/-/g, "")
//    if (res.slice(0, 1) > 2) {
//        $(thiss).val("--:--")
//    }
//    else {
//        switch (res.length) {
//            case 1: $(thiss).val(res + "-:--")
//                break;
//           case 2: $(thiss).val(res + ":--")
//               break;
//            case 3: $(thiss).val(res.slice(0,2) + ":" + res.slice(2,1)+"-")
//                break;
//            case 4: $(thiss).val(res.slice(0,2) + ":"+res.slice(1,2))
//                break;
//            default: $(thiss).val("--:--")
//                break;
//        }
//    }
//}
function SaveInKarkard() {
   var JobId=$("#SelectJob  option:selected").attr("JobId")
    var StartTime=$(".T_StartTime input").val()
    var EndTime=$(".T_EndTime input").val()
    var SpendTimeMinute=$(".T_MinTime input").val()


    $.ajax(
      {
          type: 'POST',
          contentType: "application/json;charset=utf-8",
          dataType: "json",
          url: "/Karkard/Create",
          data: JSON.stringify({
              JobId: JobId,
              StartTime: StartTime,
              EndTime:EndTime,
              SpendTimeMinute:SpendTimeMinute
          }),
          success: function (result) {

              if (result.result == true) {
                  
                  alert(result.message)
                  RefreshExecute();
              }
              if (result.result == false) {
                  alert(result.message+"\n"+result.description)
              }
          },
          error: function (error) {
              console.log(error);
          }
      });
}