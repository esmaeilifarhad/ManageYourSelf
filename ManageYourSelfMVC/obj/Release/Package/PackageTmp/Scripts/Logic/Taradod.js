//--Execute All List when click Tab
$("ul li a[href='#Taradod']").on("click", function () {
    ListTaradod();
});
$("ul li a[href='#Rotbeh']").on("click", function () {
    ListRotbeh();
});
//***************************************************HolyDay
function ListTaradod() {
    var urll = "/PersonnelGsystem/ListTaradod";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListTaradod").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function ListRotbeh() {
    var urll = "/PersonnelGsystem/ListRotbeh";
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListRotbeh").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function ListRotbehByDate(SDate) {
    var urll = "/PersonnelGsystem/ListRotbehByDate?SDate=" + SDate;
    $.ajax({
        type: "Get",
        contentType: "application/json;charset=utf-8",
        //dataType: "json",
        url: "/HolyDay/CreateHolyDay",
        data: JSON.stringify({ SDate: SDate }),
        dataType: "html",
        url: urll,
        success: function (data) {
            $(".ListRotbehByDate").html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}

$(".ListTaradod").on("click", "table tbody tr", function () {
    var SDate = $(this).find("td:nth-child(2)").text();
    SDate = SDate.substring(0, 4) + SDate.substring(5, 7) + SDate.substring(8,10)
    ListRotbehByDate(SDate);
    //alert("swss");
});


