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
         console.log(res);
         $("#ProjectId").append($("<option>").text("Select Project").attr({ "value": "" }));
         $.each(res, function (index, v) {
            $("#ProjectId").append($("<option>").text(v.projectName).attr({"value" : v.projectId}));
         });
      }
   });
};

function addTonerPercentage() {
   
}