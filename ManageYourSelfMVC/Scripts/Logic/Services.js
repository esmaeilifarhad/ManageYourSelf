function PostData(MyArray) {
     
    $.ajax(
     {
         type: 'Post',
         data: JSON.stringify({ MyData: MyArray }),
         contentType: "application/json;charset=utf-8",
         dataType: "html",
         url: "/Task/ListTaskFutureChkPost",
         success: function (result) {
             
             if (result.result == false) {
                 alert(result.message)
             }
             else {
                 $(".ListTaskFuture").html(result);
             }

         },
         error: function (error) {
             
             $(".ListTaskFuture").html("<p>موردی برای مشاهده وجود ندارد</p>")
         }
     });
}

 function service(obj) {
    // document.cookie = "username=FarhadCookie";
   
    // setCookie("username", user, 365);
     
    // var username = getCookie("username");
     //-----------------
     //var Username = localStorage.getItem("SUserName");
     //var Password = localStorage.getItem("SPassword");
     
     //if (obj.data == undefined)
     //{
         
     //    obj.data = {}
     //}
     //obj.data.SPassword = Password
     //obj.data.SUsername = Username

     
     return new Promise(resolve => {
         
        //var serviceURL =objHeader.serviceURL// "https://portal.golrang.com/_vti_bin/SPService.svc/ICTRequestTadarokat"
        //var request = objData.request//{ CID: CurrentCID, Date: myDate, PortalReqHeaderID: PortalReqHeaderID, Kalasn: Kalasn, BuyStock: BuyStock, DarkhastKonandehID: DarkhastKonandehID, TaeedKonandehID: TaeedKonandehID, TasvibKonandehID: TasvibKonandehID, Tozih: Tozih }
        // {"CID":"50","Date":"980917","PortalReqHeaderID":"68","Kalasn":"7.1","BuyStock":2}
        $.ajax({
            type:obj.type,
            url: obj.url,
            contentType: "application/json; charset=utf-8",
            xhrFields: {
                'withCredentials': true
            },
            dataType: obj.dataType,
            data: JSON.stringify(obj.data),
            //processData: false,
            success: function (data) {
                
                resolve(data)
            },
            error: function (a) {
                console.log("Start Service.js Service Error ...................");
                console.log(a);
                console.log("End Service.js Service Error ......................");
                alert("خطا در اجرای سرویس باید مشخص شود که از قطعی اینترنت یا نه");
                $.LoadingOverlay("hide");
            }
        });
    })
}