$(function () {
   $('#tblTonerInfo').DataTable();
   getCustomers();
});

var BaseApi = "https://localhost:7284/toner-api/";

function getCustomers() {
   $("#CustomerId option").remove();

   $.ajax({
      url: BaseApi + 'customers',
      type: 'GET',
      dataType: 'json',
      contextType: 'application/json',
      success: function (res) {
         $("#CustomerId").append($('<option>').text('Select Customer').attr({ 'value': '' }));
         $.each(res, function (index, v) {
            $("#CustomerId").append($('<option>').text(v.customerName).attr({ 'value': v.customerId }));
         })
      },
      error: function (err) {
         console.log(err);
      }
   });
};

function getProject(e) {
   var CustomerId = e.target.value;
   $("#ProjectId option").remove();
   $.ajax({
      /*url: BaseApi + "projects",*/
      url: BaseApi + "project/customers/" + CustomerId,
      type: "GET",
      dataType: "json",
      contentType: "application/json",
      data: { key: CustomerId },
      success: function (res) {
         $("#ProjectId").append($("<option>").text("Select Project").attr({ "value": "" }));
         $.each(res, function (index, v) {
            $("#ProjectId").append($("<option>").text(v.projectName).attr({"value" : v.projectId}));
         });
      }
   });
};

var tonerUse=[];

function getMachine(e) {
   var ProjectId = e.target.value;
   $("#MachineId option").remove();

   $.ajax({
      url: BaseApi + "machine/projects/" + ProjectId,
      type: "GET",
      dataType: "json",
      contextType: "application/json",
      data: { key: ProjectId },
      success: function (res) {
         console.log(res);
         tonerUse.push(res);
         $("#MachineId").append($("<option>").text("Select Machine").attr({ "value": "" }));
         $.each(res, function (index, v) {
            $("#MachineId").append($("<option>").text(v.machineModel).attr({ "value": v.colourType}));
         });
      }
   });
}


function getToner(e) {
   var colourType = e.target.value;
   console.log(tonerUse);
   console.log(colourType);
   console.log(typeof (colourType));

   if (parseInt(colourType) == 0) {

   }

   if (parseInt(colourType) == 1) {

   }
}