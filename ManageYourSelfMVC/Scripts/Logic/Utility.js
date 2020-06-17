//--------------------------------------------------------------------Utility
function calDayOfWeek(date) {
    /*
    str.substr(1, 4);
    */

    date = date.toString()

    var mounth = ""
    var rooz = ""

    if (date.length == 8) {
        date = date.substr(0, 4) + "/" + date.substr(4, 2) + "/" + date.substr(6, 2)
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
function calDayOfWeeknumber(date) {
    /*
    str.substr(1, 4);
    */

    date = date.toString()

    var mounth = ""
    var rooz = ""

    if (date.length == 8) {
        date = date.substr(0, 4) + "/" + date.substr(4, 2) + "/" + date.substr(6, 2)
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
            day = 1;
            break;
        case 1:
            day =2;
            break;
        case 2:
            day =3;
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
            day = 0;
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
    str = str.toString()

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
function convertDateToslashless(str) {
    if (str == "") {
        return undefined
    }
    if (str == undefined) {
        return undefined
    }
    var todayarray = str.split("/")
    mounth = (parseInt(todayarray[1]) <= 9) ? "0" + parseInt(todayarray[1]) : parseInt(todayarray[1])
    rooz = (parseInt(todayarray[2]) <= 9) ? "0" + parseInt(todayarray[2]) : parseInt(todayarray[2])
    year = todayarray[0].substring(2, 4)
    today = "13" + year + "" + mounth + "" + rooz
    return today
    /*
    str = str.replace(/\//g, '')
    str = str.toString()
    if (str.length == 6) {
        return "13" + str.slice(0, 2) + str.slice(2, 4) + str.slice(4, 6)
    }
    if (str.length == 8) {
        return str.slice(0, 2) + str.slice(3, 5) + str.slice(5, 7)
    }
    if (str.length == 10) {
        return str.slice(0, 4) + str.slice(5, 7) + str.slice(8, 10)
    }
    */
}
function foramtTime(str) {
    if (str == undefined) {
        return "undefined"
    }

    return str.slice(0, 2) + ":" + str.slice(2, 4) + ":" + str.slice(4, 6)


}
function minuteToTime(str) {
    if (str == undefined) {
        return "undefined"
    }
    var H = Math.floor(str / 60); 
    var M = str % 60
    return H + ":" + ((M) < 10 ? "0" + M : M)
}
function IsKabise(str) {
    //moment.jIsLeapYear(1391) // true
    //moment.jIsLeapYear(1392) // false
    if (str == undefined) {
        return "undefined"
    }
    str = str.replace(/\//g, '')
    str = str.toString()
    if (str.length == 6) {
        if (parseInt("13" + str.slice(0, 2)) % 4 == 3)
            return true
        else
            return false
    }
    if (str.length == 8) {
        if (parseInt(str.slice(0, 4)) % 4 == 3)
            return true
        else
            return false

        return str.slice(0, 4)
    }

}
function baghyMandeYaer(str) {
    const m = moment();
    //366
    if (IsKabise(str) == true) {
        var second = (m.jDayOfYear() - 1) * 86400
        var x = CurrentTime()
        second = parseInt(x.slice(0, 2)) * 3600 + parseInt(x.slice(2, 4)) * 60 + parseInt(x.slice(4, 6)) + second
        return 100 - ((second * 100) / (366 * 86400)).toFixed(3)
    }
    //365
    if (IsKabise(str) == false) {
        var second = (m.jDayOfYear() - 1) * 86400
        var x = CurrentTime()
        second = parseInt(x.slice(0, 2)) * 3600 + parseInt(x.slice(2, 4)) * 60 + parseInt(x.slice(4, 6)) + second
        return 100 - ((second * 100) / (365 * 86400)).toFixed(3)
    }
}
function baghyMandeDay() {
    //86400
    var x = CurrentTime()
    var second = parseInt(x.slice(0, 2)) * 3600 + parseInt(x.slice(2, 4)) * 60 + parseInt(x.slice(4, 6))
    return (100 - (second * 100) / 86400).toFixed(2)


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
function removeChara(char,str) {
    
    var noCommas = str.replace(char, '')
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
function NewOldDate(str) {

    const m = moment();
    switch (str) {
        case "1"://فردا
            m.add(1, 'day')
            var newDate = m.format('jYYYY/jM/jD')
            newDate = convertDateToslashless(newDate)
            return newDate
            break;
        case "0101"://اول ماه بعد

            var month = m.jMonth() + 1
            var day = m.jDate()
            var year = m.jYear()
            var isKabise = IsKabise(todayShamsy8char())

            if (month == 12) {
                return (year + 1) + "0101"
            }
            else {
                month += 1
                return (year) + "" + ((month) < 7 ? "0" + month : month) + "01"
            }

            break;
        case "00"://شنبه هفته بعد
 
            var dayOfWeek = calDayOfWeeknumber(todayShamsy8char())
            dayOfWeek=7 - dayOfWeek
            m.add(dayOfWeek, 'day')
            var newDate = m.format('jYYYY/jM/jD')
            newDate = convertDateToslashless(newDate)
            return newDate
            break;
        case "30"://ماه بعد
            
            var isKabise = IsKabise(todayShamsy8char())
            var month = m.jMonth() + 1
            if (month < 7) {
                m.add(31, 'day')
            }
            else {
                m.add(30, 'day')
            }
            if (month == 12 && isKabise == true) {
                m.add(30, 'day')
            }
            if (month == 12 && isKabise == false) {
                m.add(29, 'day')
            }
            var newDate = m.format('jYYYY/jM/jD')
            newDate = convertDateToslashless(newDate)
            return newDate
            break;
    }
    return undefined
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
function showAlert(message) {
    const Toast = Swal.mixin({
        toast: true,
        // position: 'top-end',
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true,
        onOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })

    Toast.fire({
        icon: 'success',
        title: message
    })
}

function customConfirm(obj) {

    return new Promise(resolve => {
        Swal.fire({
            title: obj.title,
            text: obj.text,
            icon: 'info',
            showCancelButton: true,
            cancelButtonText: obj.cancelButtonText,
            confirmButtonColor: '#3085d6!important',
            cancelButtonColor: 'red!important',
            confirmButtonText: obj.confirmButtonText
        }).then((result) => {
            resolve(result)
        })
    });
}

