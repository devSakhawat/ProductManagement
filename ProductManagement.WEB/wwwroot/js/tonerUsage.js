$(function () {
   getCustomers();


});

var BaseApi = "https://localhost:7284/toner-api/";
//================  Global Varialbe  ========================
var currentDate = new Date();


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
      },
      error: function (err) {
         console.log(err);
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
         tonerUse.push(res);
         $("#MachineId").append($("<option>").text("Select Machine").attr({ "value": "" }));
         $.each(res, function (index, v) {
            $("#MachineId").append($("<option>").text(v.machineSN).attr({ "value": v.colourType}));
         });
      },
      error: function (err) {
         console.log(err);
      }
   });
}

//================= Page count Calculation ===============================
function getToner(e) {
   var colourType = e.target.value;

   var currentMonth = new Date().getMonth() + 1;
   var returnMonth = 0;
   $.ajax({
      url: BaseApi + "delivery-toner/machine/{machineId}",
      type : "GET",
      dataType: "json",
      contentType: "application/json",
      success: function (res) {
         console.log(res);
         /*returnMonth = res;*/
      },
      error: function (err) {
         console.log(err);
      }
   });

   if (parseInt(colourType) == 0) {
      $("#TonerPercentage tr:eq(0)").remove();

      $("#TonerPercentage").append(
         `<tr id="tonerPercentage">
            <td colspan=4">
               <input type="text" class="form-control" placeholder="Black N White" autocomplete="off" id="txt_C" />
            </td>
         </tr`
      );
   }

   if (parseInt(colourType) == 1) {
      $("#TonerPercentage tr:eq(0)").remove();

      $("#TonerPercentage").append(
         `<tr id="tonerPercentage">
            <td width="25%">
               <input class="form-control" placeholder="Cyan" autocomplete="off" id="txt_C" />
            </td>
            <td width="25%">
               <input class="form-control" placeholder="Magenta" autocomplete="off" id="txt_M" />
            </td>
            <td width="25%">
               <input class="form-control" placeholder="Yellow" autocomplete="off" id="txt_Y" />
            </td>
            <td width="25%">
               <input class="form-control" placeholder="Black" autocomplete="off" id="txt_K" />
            </td>
         </tr>`
      );
   }
}

//================= Page count Calculation ===============================
$(".curtCounter, .prevCounter, .totalCounter").on("keydown keyup click", Counteralculation);
function Counteralculation() {
   // Page count calculation
   var totalColourToner = Number($("#CurtCounter").val()) - Number($("#PrevCounter").val());
   $("#TotalCounter").val(totalColourToner);
}

function CalculateValues() {
   var cyan = $("#txt_C").val();
   var magenta = $("#txt_M").val();
   var yellow = $("#txt_Y").val();
   var black = $("#txt_K").val();
   var prevCounter = $("#PrevCounter").val();
   var curtCounter = $("#CurtCounter").val();
   var totalCounter = $("#TotalCounter").val();

}



//==============  =========================================

// object variable for toner delivery
var deliveryTonerObj = {};

function getMachineForDelivery(e) {
   var ProjectId = e.target.value;
   $("#MachineId option").remove();

   $.ajax({
      url: BaseApi + "machine/projects/" + ProjectId,
      type: "GET",
      dataType: "json",
      contentType: "application/json",
      data: { key: ProjectId },
      success: function (res) {
         tonerUse.push(res);
         $("#MachineId").append($("<option>").text("Select Machine").attr({ "value": "" }));
         $.each(res, function (index, v) {
            $("#MachineId").append($("<option>").text(v.machineSN).attr({ "value": v.machineId }));
         });
      },
      error: function (err) {
         console.log(err);
      }
   });
}

//================  without data how pass perameter to api   ===========================
function deliveryToner(e) {
   var machineId = e.target.value;
/*   var currentMonth = new Date().getMonth() + 1;*/

   $.ajax({
      url: BaseApi + "delivery-toner/machine/" + machineId,
      type : "GET",
      dataType: "json",
      contentType: "application/json",
      success: function (res) {
         console.log(res);
         console.log(typeof (res.colourType));

         var currentMonthNumber = currentDate.getMonth() + 1;
         var currentMonthString = currentDate.toLocaleString("default", { month: "long" });
         if (currentMonthNumber == res.currentMonth) {
            $("#DeliveryToner tr:eq(0)").remove();

            $("#DeliveryToner").append(
               `<tr id="deliveryTonerItem">
                  <td colspan=4">
                     <div class="alert alert-success" role="alert">
                        You allready insert <b>${currentMonthString}</b> month delivery toner for <b>${res.machineSN}</b>.
                     </div>
                  </td>
               </tr`
            );
         }
         if (currentMonthNumber != res.currentMonth) {
            $("#DeliveryToner tr:eq(0)").remove();
            if (res.colourType == 0) {
               $("#DeliveryToner").append(
                  `<tr id="deliveryTonerItem">
                  <td colspan=4">
                     <input class="form-control" placeholder="Black and White" autocomplete="off" id="toner_BW" />
                  </td>
               </tr`
               );
            }
            else {
               $("#DeliveryToner").append(
                  `<tr id="deliveryTonerItem">
                  <td width="25%">
                     <input class="form-control" placeholder="Cyan" autocomplete="off" id="toner_C" />
                  </td>
                  <td width="25%">
                     <input class="form-control" placeholder="Magenta" autocomplete="off" id="toner_M" />
                  </td>
                  <td width="25%">
                     <input class="form-control" placeholder="Yellow" autocomplete="off" id="toner_Y" />
                  </td>
                  <td width="25%">
                     <input class="form-control" placeholder="Black" autocomplete="off" id="toner_K" />
                  </td>
               </tr`
               );
            }
         }
      }
   });
}

