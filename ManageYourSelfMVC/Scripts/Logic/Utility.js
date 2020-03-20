//--------------------------------------------------------------------Utility
function calDayOfWeek(date) {
    /*
    str.substr(1, 4);
    */

    date = date.toString()
    
    var mounth = ""
    var rooz = ""

    if(date.length==8)
    {
        date=date.substr(0, 4) + "/" + date.substr(4, 2) + "/" + date.substr(6, 2)
    }
    var arrayDate = date.split("/")
    mounth = (parseInt(arrayDate[1]) <= 9) ? "0" + parseInt(arrayDate[1]) : parseInt(arrayDate[1])
    rooz = (parseInt(arrayDate[2]) <= 9) ? "0" + parseInt(arrayDate[2]) : parseInt(arrayDate[2])

    date = arrayDate[0] + mounth + rooz;

    //date = date.replace(/\//g, '');
    date = date.substr(date.length - 6); // 13980203=> 980203

    const m = moment();
    const numberWeek = moment(date, 'jYYjMMjDD').weekday();
    let day;
    switch (numberWeek) {
        case 0:
            day = "یکشنبه";
            break;
        case 1:
            day = "دوشنبه";
            break;
        case 2:
            day = "سه شنبه";
            break;
        case 3:
            day = "چهارشنبه";
            break;
        case 4:
            day = "پنج شنبه";
            break;
        case 5:
            day = "جمعه";
            break;
        case 6:
            day = "شنبه";
    }
    return day;
}
function showDays(firstDate, secondDate) {
    
    /*
Pass you dates to this function like this:  showDays('1/1/2014','12/25/2014')

پارامتر وردی تابع شمسی میباشد
1367/07/09
*/
    var firstDate = moment(firstDate, 'jYYYY/jM/jD ').format('M/D/YYYY')//'1/1/2014'
    var secondDate = moment(secondDate, 'jYYYY/jM/jD ').format('M/D/YYYY')//'1/1/2014'


    var startDay = new Date(firstDate);
    var endDay = new Date(secondDate);
    var millisecondsPerDay = 1000 * 60 * 60 * 24;

    var millisBetween = startDay.getTime() - endDay.getTime();
    var days = millisBetween / millisecondsPerDay;

    // Round down.
    return (Math.floor(days));

}
//980809|13980809  =>1398/08/09  input parameter
function foramtDate(str) {
    str=str.toString()
    
    if (str == undefined) {
        return "undefined"
    }
    if (str.length == 6) {
        return "13" + str.slice(0, 2) + "/" + str.slice(2, 4) + "/" + str.slice(4, 6)
    }
    if (str.length == 8) {
        return str.slice(0, 4) + "/" + str.slice(4, 6) + "/" + str.slice(6, 8)
    }

}
function foramtTime(str) {
    if (str == undefined) {
        return "undefined"
    }

    return str.slice(0,2) + ":" + str.slice(2, 4) + ":" + str.slice(4,6)
    

}
function splitString(str, char) {
    
    if (str == null) return ""
    return str.split(char)
}
//سه رقم سه رقم جدا کنه برای پول   SeparateThreeDigits
function SeparateThreeDigits(str) {
    var x = parseInt(str);
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

    // return parseInt(str);

}
function removeComma(str) {
    
    var noCommas = str.replace(/,/g, '')
    return noCommas
}
function removeLastChar(str) {
    return str.slice(0, -1)
}
function removeCountChar(str, n) {
    return str.slice(0, -n)
}
function numberDaysTwoDate(firstDate, secondDate) {

    var firstDate = moment(firstDate, 'jYYYY/jM/jD ').format('M/D/YYYY')//'1/1/2014'
    var secondDate = moment(secondDate, 'jYYYY/jM/jD ').format('M/D/YYYY')//'1/1/2014'


    var startDay = new Date(firstDate);
    var endDay = new Date(secondDate);
    var millisecondsPerDay = 1000 * 60 * 60 * 24;

    var millisBetween = startDay.getTime() - endDay.getTime();
    var days = millisBetween / millisecondsPerDay;

    // Round down.
    return (Math.floor(days));

}
function todayShamsy() {

    const m = moment();
    var today = moment().format('jYYYY/jM/jD');//Today
    return today;
}
function todayShamsy8char() {
    const m = moment();
    today = moment().format('jYYYY/jM/jD');//Today


    var todayarray = today.split("/")
    mounth = (parseInt(todayarray[1]) <= 9) ? "0" + parseInt(todayarray[1]) : parseInt(todayarray[1])
    rooz = (parseInt(todayarray[2]) <= 9) ? "0" + parseInt(todayarray[2]) : parseInt(todayarray[2])
    year = todayarray[0].substring(2, 4)
    today = "13" + year + "" + mounth + "" + rooz
    return today
}
function CurrentTime() {
    var d = new Date();
    var hour = d.getHours();  /* Returns the hour (from 0-23) */
    var minute = d.getMinutes();  /* Returns the minutes (from 0-59) */
    var second = d.getSeconds();
    return (hour <= 9 ? "0" + hour : hour) + "" + (minute <= 9 ? "0" + minute : minute) + "" + (second <= 9 ? "0" + second : second)
}
/*
برای تگ های ورودی از روش زیر استفاده کن
<input type='text' name='Budget' onkeyup='changeInputToThreeDigit(this)'/>

برای گرفتن مقدار بصورت عدد از روش زیر استفاده کن
 var Budget = $("#takhsisBudget input[name=Budget]").val();
Budget=parseInt(removeComma(Budget))
*/
function changeInputToThreeDigit(thiss) {

    var x = removeComma(thiss.value)
    x = SeparateThreeDigits(x)
    thiss.value = (x == 'NaN' ? 0 : x)
}
//---------------------
/*
 Math.round(2.4) = 2
  Math.round(2.5) = 3
*/

