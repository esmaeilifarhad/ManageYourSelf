//***********************************************MasterModal*****************************************
$('#MasterModal .btnSave').on("click", function () {
    debugger;
    var NameTbl = $("#MasterModal .BodyModal table").attr("nametbl")
    if (NameTbl == "tbltask")
        CreateTask();
    if (NameTbl == "tblword")
        CreateWord();
    if (NameTbl == "UpdateTask")
        UpdateTask();
    //if (NameTbl == "UpdateWord") {
    //    UpdateWord();
    //}
});
//Create Post  Update
$("#MasterModal .btnSave").on("click", function () {
    debugger;
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