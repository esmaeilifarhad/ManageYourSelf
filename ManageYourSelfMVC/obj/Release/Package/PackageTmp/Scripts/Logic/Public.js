//***********************************************MasterModal*****************************************
$('#MasterModal .btnSave').on("click", function () {
    
    var NameTbl = $("#MasterModal .BodyModal table").attr("nametbl")
    if (NameTbl == "tbltask")
        CreateTask();
    if (NameTbl == "tblword")
        //2
        CreateWord();
    if (NameTbl == "UpdateTask")
        UpdateTask();
    //if (NameTbl == "UpdateWord") {
    //    UpdateWord();
    //}
});
//Create Post  Update
$("#MasterModal .btnSave").on("click", function () {
    //  CreateRoutineJob
    var NameOperator = $("#MasterModal .BodyModal div").attr("Name");
    if (NameOperator == "Create")
        CreateCategoryPost();
    if (NameOperator == "Update") {
        UpdateCategory();
    }
    if (NameOperator == "CreateJob") {
        CreateJobPost();
    }
    if (NameOperator == "CreateRoutineJob") {
        CreateRoutineJobPost();
    }
    if (NameOperator == "EditJob") {
        var JobId = $("#MasterModal table").attr("jobid");
        UpdateJob(JobId);
    }
    if (NameOperator == "CreateKarkard") {
        CreateKarkardPost();
    }
    if (NameOperator == "CreatePercentJob") {
        CreatePercentJobPost();
    }
    if (NameOperator == "EditPercentJob") {
        UpdatePercentJobPost();
    }
    if (NameOperator == "CreateMojoodyBank") {
        CreateMojoodyBankPost();
    }
    if (NameOperator == "EditMojoodyBank") {
        UpdateMojoodyBank();
    }
    if (NameOperator == "CreateTypeHazineh") {
        CreateTypeHazinehPost();
    }
    if (NameOperator == "EditTypeHazineh") {
        UpdateTypeHazineh();
    }
    if (NameOperator == "CreateDaramad") {
        CreateDaramadPost();
    }
    if (NameOperator == "EditMojoodyBankBalance") {
        UpdateMojoodyBankBalance();
    }
    if (NameOperator == "Exchange") {
        UpdateMojoodyBankExchange();
    }
    if (NameOperator == "CreateMaterData") {
        CreateMasterDataPost();
    }
    if (NameOperator == "UpdateMaterData") {
        UpdateMasterDataPost();
    }
    if (NameOperator == "UpdateTiming") {
        TimingPost();
    }
    if (NameOperator == "ChangeTodayTask") {
        ChangeTodayTaskPost();
    }
});

/*
چندمین روز هفته
1398/02/03 فرمت پارامتر وردی
*/
function calDayOfWeek(date) {
    console.log(date)
    date = date.replace(/\//g, '');
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
    return(Math.floor(days));

}
/*
this function get date and return day of week and how days pass to today
*/
function changeDateToRoozHateAndDayPass(date) {
    
    var myDate;
    if (date.toString().length == 8) {
        myDate = date.toString().slice(0, 4) + "/" + date.toString().slice(4, 6) + "/" + date.toString().slice(6, 8)
    }
    
    var today = moment().format('jYYYY/jM/jD');//Today
    var diffDays = showDays(today, myDate);
    var dayOfWeek = calDayOfWeek(myDate);
    
    return dayOfWeek
    
}
setInterval(function () {
   
    ListTaslLevelHigh();
   
    ListTiming(0);
    
    ShowLevel();
    RoutineJobListMasterPage();
   
    //--------------------
    var dt = new Date();
    var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
   // console.log("exe ListTaslLevelHigh : " + time);
}, 60000);


