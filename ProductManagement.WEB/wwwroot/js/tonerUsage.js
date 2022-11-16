$(function () {
   getCustomers();
});

var BaseApi = "https://localhost:7284/toner-api/";
//================  Global Varialbe  ========================
var currentDate = new Date();
// store toner use data as container
var tonerUse = [];
// get object from array by property value
var UsageDetail = {};

// current month and year
var currentMonthNumber = currentDate.getMonth() + 1;
var currentMonthString = currentDate.toLocaleString("default", { month: "long" })
var currentYear = new Date().getFullYear();


// Get Customers list
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
// get Project list based on projectId
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

// get project base current month machine usage toner list
function getMachine(e) {
   var ProjectId = e.target.value;
   $("#MachineId option").remove();

   $.ajax({
      url: BaseApi + "machine/project/" + ProjectId,
      type: "GET",
      dataType: "json",
      contextType: "application/json",
      data: { key: ProjectId },
      success: function (res) {
         $.each(res, function(index, v) {
            tonerUse.push(v);
         });
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

//================= Page count Calculation ===============================
function getToner(e) {
   var machine = parseInt(e.target.value);

   // get object from array by property value
   UsageDetail = tonerUse.find(item => item.machineId === machine);
   console.log(UsageDetail);

   // condition check for database contain current month data or not
   if (currentMonthNumber == UsageDetail.deliveryMonth && currentYear == UsageDetail.deliveryYear) {
      $("#TonerPercentage tr:eq(0)").remove();
      $("#PrevCounter").val('');

      $("#TonerPercentage").append(
         `<tr id="deliveryTonerItem">
            <td colspan=4">
               <div class="alert alert-primary" role="alert">
                  You allready insert <b style="font-color: black">${currentMonthString}</b> month delivery toner for <b>${UsageDetail.machineSN}</b>.
               </div>
            </td>
         </tr>`
      );
   }
   else {
      if (currentMonthNumber != UsageDetail.deliveryMonth) {
         $("#TonerPercentage tr:eq(0)").remove();

         if (UsageDetail.colourType == 0) {
            $("#PrevCounter").val('');
            $("#TonerPercentage").append(
               `<tr id="deliveryTonerItem">
               <td colspan=4">
                  <input class="form-control" placeholder="Black and White" autocomplete="off" id="toner_BW" />
               </td>
            </tr>`
            );
            $("#PrevCounter").val(UsageDetail.currentCounter);
         }
         else {
            $("#PrevCounter").val('');
            $("#TonerPercentage").append(
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
            </tr>`
            );
            $("#PrevCounter").val(UsageDetail.currentCounter);
         }
      };

      //$.ajax({
      //   url: BaseApi + "delivery-toner/machine/" + machine,
      //   type: "GET",
      //   dataType: "json",
      //   contentType: "application/json",
      //   data: { machineId: machine },
      //   success: function (res) {
      //      console.log(res);
      //      /*returnMonth = res;*/
      //   },
      //   error: function (err) {
      //      console.log(err);
      //   }
      //});
   }
}


//================= Page count Calculation ===============================
$(".curtCounter, .prevCounter, .totalCounter").on("keydown keyup click", Counteralculation);
function Counteralculation() {
   // Page count calculation
   var totalColourToner = Number($("#CurtCounter").val()) - Number($("#PrevCounter").val());
   $("#TotalCounter").val(totalColourToner);
}

//================= Post Usage Value ===============================



function CalculateValues() {
   var cyan = $("#txt_C").val();
   var magenta = $("#txt_M").val();
   var yellow = $("#txt_Y").val();
   var black = $("#txt_K").val();
   var prevCounter = $("#PrevCounter").val();
   var curtCounter = $("#CurtCounter").val();
   var totalCounter = $("#TotalCounter").val();

}



//============== Delivery toner part =========================================
//============== Delivery toner part =========================================
//============== Delivery toner part =========================================

// object variable for get toner delivery data
var deliveryTonerGetResult = {};

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
// global varial for post

function deliveryToner(e) {
   var machineId = e.target.value;
   $.ajax({
      url: BaseApi + "delivery-toner/machine/" + machineId,
      type : "GET",
      dataType: "json",
      contentType: "application/json",
      success: function (res) {
         // insert result value to golobal variable
         deliveryTonerGetResult = res.find(obj => obj);

         // condition check for database contain current month data or not
         //if (currentMonthNumber == result.currentMonth) {
         if (currentMonthNumber == deliveryTonerGetResult.currentMonth) {
            $("#DeliveryToner tr:eq(0)").remove();

            $("#DeliveryToner").append(
               `<tr id="deliveryTonerItem">
                  <td colspan=4">
                     <div class="alert alert-success" role="alert">
                        You allready insert <b style="font-color: black">${currentMonthString}</b> month delivery toner for <b>${deliveryTonerGetResult.machineSN}</b>.
                     </div>
                  </td>
               </tr`
            );
         };
         if (currentMonthNumber != deliveryTonerGetResult.currentMonth) {
            $("#DeliveryToner tr:eq(0)").remove();

            if (deliveryTonerGetResult.colourType == 0) {
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
         };
      }
   });
}

//================  post DeliveryToner  ====================
// reset input filed

function ResetInput() {
   $("#toner_C").val(0);
   $("#toner_M").val(0);
   $("#toner_Y").val(0);
   $("#toner_K").val(0);
   $("#toner_BW").val(0);
}

var ItemContainer = [];
var i = 1;
function CalculateAddItemContainer() {
   if (deliveryTonerGetResult.colourType == 0) {
      deliveryTonerGetResult.bw = Number($("#toner_BW").val());
      deliveryTonerGetResult.colourTotal = 0;
      //Item = {
      //   MachineId: deliveryTonerGetResult.machineId,
      //   MachineSN: deliveryTonerGetResult.machineSN,
      //   ColourType: deliveryTonerGetResult.colourType,
      //   TotalColour : 0,
      //   BW: Number($("#toner_BW").val())
      //};
      //console.log(Item);

      $("#AddRemoveItem").append(
         `<tr>
         <td class = "text-center machineSN" id="machineSN_${i}">${deliveryTonerGetResult.machineSN}</td>
         <td class = "text-center colourType" id="colourType_${i}">${deliveryTonerGetResult.colourType}</td>
         <td class = "text-center bw" id="bw_${i}">${deliveryTonerGetResult.bw}</td>
         <td class = "text-center totalColour" id="totalColour_${i}">${deliveryTonerGetResult.colourTotal}</td>
         <td class="text-center td2">
		      <button type="button" name="add" class="btn btn-sm btn-outline-danger waves-effect remove-tr" id="remove-tr">
			      Remove
		      </button>
	      </td>
      </tr`
      );
      ItemContainer.push(deliveryTonerGetResult);
      i++;
   }
   else {
      // value assign to a global object that comes from deliveryToner();
      deliveryTonerGetResult.cyan = parseFloat($("#toner_C").val());
      deliveryTonerGetResult.magenta = parseFloat($("#toner_M").val());
      deliveryTonerGetResult.yellow = parseFloat($("#toner_Y").val());
      deliveryTonerGetResult.black = parseFloat($("#toner_K").val());
      deliveryTonerGetResult.colourTotal = parseFloat($("#toner_C").val()) + parseFloat($("#toner_M").val()) + parseFloat($("#toner_Y").val()) + parseFloat($("#toner_K").val());
      
      $("#AddRemoveItem").append(
         `<tr>
         <td class = "text-center machineSN" id="machineSN_${i}">${deliveryTonerGetResult.machineSN}</td>
         <td class = "text-center colourType" id="colourType_${i}">${deliveryTonerGetResult.colourType}</td>
         <td class = "text-center bw" id="bw_${i}">${deliveryTonerGetResult.bw}</td>
         <td class = "text-center totalColour" id="totalColour_${i}">${deliveryTonerGetResult.colourTotal}</td>
         <td class="text-center td2">
		      <button type="button" name="add" class="btn btn-sm btn-outline-danger waves-effect remove-tr">
			      Remove
		      </button>
	      </td>
      </tr`
      );
      ItemContainer.push(deliveryTonerGetResult);
      i++;
   }
   ResetInput();
   console.log(ItemContainer);
}

 // remove parent tr
$(document).on('click', '.remove-tr', function () {
   $(this).parents('tr').remove();
   ItemContainer.pop();
   console.log(ItemContainer);
});

function SubmitDeliveryToner() {
   console.log(ItemContainer);
   $.ajax({
      url: BaseApi + "delivery-toner",
      type: "POST",
      dataType: "json",
      contentType: "application/json",
      /*contentType: "application/x-www-form-urlencoded",*/
      data: JSON.stringify(ItemContainer),
      /*data: "{deliveryToner : " + JSON.stringify(ItemContainer) + "}",*/
      success: function (res) {
         console.log(res);
         location.reload();
      },
      error: function (err) {
         console.log(err);
      }
   });
   
}

//$("#AddRemoveItem").on("click", ".remove-tr", removeItemTr);
//function removeItemTr() {
//   $("#remove-tr").parents('tr').remove();
//   ItemContainer.pop();
//   console.log(ItemContainer);
//}

// look carefully. which id i was selected and
//$("#remove-tr").on("click", function () {
//   $(this).parents('tr').remove();
//});

//================  post DeliveryToner  ====================
//function getDeliveryToenrs() {
//   $.ajax({
//      url: BaseApi + "delivery-toners",
//      typ: "POST",
//      dataType: "json",
//      contentType: "application/json",
//      success: function (res) {
//         console.log(res);
//      },
//      error: function (err) {
//         console.log(err);
//      }
//   });
//}




