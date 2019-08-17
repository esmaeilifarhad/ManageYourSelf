$(document).ready(function () {
   
    CalculatePriceGold1();
    CalculatePriceGold2();
    CalculatePriceGold3();
})
function transferGeramTala()
{
    alert($("iframe html body .first .content table tr:nth-child(3) td:nth-child(2) span").text())
}

function CalculatePriceGold1()
{
    var currentPrice = parseInt($("#Gold .ListGold1 input[name='currentPrice']").val())
    $("#Gold .ListGold2 input[name='currentPrice']").val(currentPrice)
    $("#Gold .ListGold3 input[name='currentPrice']").val(currentPrice)
    var ojratPrice = parseInt($("#Gold .ListGold1 input[name='ojratPrice']").val())
    var soodPrice = parseInt($("#Gold .ListGold1 input[name='soodPrice']").val())
    var maliatPrice = parseInt($("#Gold .ListGold1 input[name='maliatPrice']").val())
    var vazn = $("#Gold .ListGold1 input[name='vazn']").val()
    $("#Gold .ListGold2 input[name='vazn']").val(vazn)
    $("#Gold .ListGold3 input[name='vazn']").val(vazn)
    var resultPrice = (currentPrice + ojratPrice)

    resultPrice = (resultPrice * soodPrice) / 100 + resultPrice
 
    resultPrice = (resultPrice * maliatPrice) / 100 + resultPrice
  
    resultPrice = parseInt(resultPrice * vazn)
  
    $("#Gold .ListGold1 input[name='resultPrice']").val(resultPrice)
    //console.log(x)
    CalculatePriceGold2()
    CalculatePriceGold3()

}
function CalculatePriceGold2() {
    var currentPrice = parseInt($("#Gold .ListGold2 input[name='currentPrice']").val())
    $("#Gold .ListGold1 input[name='currentPrice']").val(currentPrice)
    $("#Gold .ListGold3 input[name='currentPrice']").val(currentPrice)
    var ojratPrice = parseInt($("#Gold .ListGold2 input[name='ojratPrice']").val())
    var soodPrice = parseInt($("#Gold .ListGold2 input[name='soodPrice']").val())
    var maliatPrice = parseInt($("#Gold .ListGold2 input[name='maliatPrice']").val())
    var vazn = $("#Gold .ListGold2 input[name='vazn']").val()
    $("#Gold .ListGold1 input[name='vazn']").val(vazn)
    $("#Gold .ListGold3 input[name='vazn']").val(vazn)
    var resultPrice = (currentPrice + ojratPrice)

    resultPrice = (resultPrice * soodPrice) / 100 + resultPrice

    resultPrice = (resultPrice * maliatPrice) / 100 + resultPrice

    resultPrice = parseInt(resultPrice * vazn)

    $("#Gold .ListGold2 input[name='resultPrice']").val(resultPrice)
    //console.log(x)

}
function CalculatePriceGold3() {
    var currentPrice = parseInt($("#Gold .ListGold3 input[name='currentPrice']").val())
    $("#Gold .ListGold1 input[name='currentPrice']").val(currentPrice)
    $("#Gold .ListGold2 input[name='currentPrice']").val(currentPrice)
    var ojratPrice = parseInt($("#Gold .ListGold3 input[name='ojratPrice']").val())
    var soodPrice = parseInt($("#Gold .ListGold3 input[name='soodPrice']").val())
    var maliatPrice = parseInt($("#Gold .ListGold3 input[name='maliatPrice']").val())
    var vazn = $("#Gold .ListGold3 input[name='vazn']").val()
    $("#Gold .ListGold1 input[name='vazn']").val(vazn)
    $("#Gold .ListGold2 input[name='vazn']").val(vazn)
    var resultPrice = (currentPrice + ojratPrice)

    resultPrice = (resultPrice * soodPrice) / 100 + resultPrice

    resultPrice = (resultPrice * maliatPrice) / 100 + resultPrice

    resultPrice = parseInt(resultPrice * vazn)

    $("#Gold .ListGold3 input[name='resultPrice']").val(resultPrice)
    //console.log(x)

}