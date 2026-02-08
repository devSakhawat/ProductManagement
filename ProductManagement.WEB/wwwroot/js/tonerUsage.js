$(function () {
   getCustomers();
});

var BaseApi = "https://localhost:7284/toner-api/";
//================  Global Varialbe  ========================
var currentDate = new Date();

// Per Toner Page for profite
var bwPerTonerPage = 600;
var colourPerTonerPage = 400;

// store project base machine related all data.
var tonerUse = [];
// get object from array by property value
var UsageDetail = {};

// current month and year
var currentMonthNumber = currentDate.getMonth() + 1;
var currentMonthString = currentDate.toLocaleString("default", { month: "long" })
var currentYear = new Date().getFullYear();

// Reset function for TonerDelivery and UsageDetails
function ResetInput() {
   $("#toner_C").val(0);
   $("#toner_M").val(0);
   $("#toner_Y").val(0);
   $("#toner_K").val(0);
   $("#toner_BW").val(0);
}



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
         console.log(tonerUse);
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

// print array after function execution

//================= toner usage and paper usage ===============================
function getToner(e) {
   var machine = parseInt(e.target.value);

   // get object from array by property value
   UsageDetail = tonerUse.find(item => item.machineId === machine);
   console.log(UsageDetail);

   // condition check for database contain current month data or not
   /*if (currentMonthNumber != UsageDetail.deliveryMonth && currentYear != UsageDetail.deliveryYear) {*/
   if (UsageDetail.tonerUsageMonth != currentMonthNumber && currentMonthNumber != UsageDetail.deliveryMonth && currentYear != UsageDetail.deliveryYear) {
      $("#MachineToner tr:eq(1)").remove();
      $("#MachineToner tr:eq(0)").remove();
      $("#DeliveryToner tr:eq(1)").remove();
      $("#DeliveryToner tr:eq(0)").remove();
      $("#AddUsageDetailItem").removeAttr("disabled");

      if (UsageDetail.colourType == 0) {
         $("#PrevCounter").val('');
         $("#TotalCounter").val('');
         $("#CurtCounter").val('');

         $("#MachineToner").append(
            `<thead>
               <th colspan="4" class="text-center">Machine Toner</th>
            </thead>
            <tr id="MachineTonerItem">
               <td colspan="4">
                  <input class="form-control" placeholder="Black N White" autocomplete="off" id="machine_BW" />
               </td>
            </tr>`
         );

         $("#DeliveryToner").append(
            `<thead>
               <th colspan="4" class="text-center">Delivery Toner</th>
            </thead>
            <tr id="DeliveryTonerItem">
               <td colspan="4">
                  <input class="form-control" placeholder="Black N White" autocomplete="off" id="delivery_BW" />
               </td>
            </tr>`
         );

         if (currentMonthNumber == UsageDetail.paperUsageMonth && currentYear == UsageDetail.paperUsageYear) {
            $("#PrevCounter").val(UsageDetail.previousCounter);
            $("#CurtCounter").val(UsageDetail.currentCounter);
            $('#CurtCounter').attr('readonly', true);
            $("#TotalCounter").val(UsageDetail.monthlyTotalCounter);
         }
         else {
            $("#PrevCounter").val(UsageDetail.currentCounter);
            $('#CurtCounter').removeAttr('readonly');
         }
      }
      else {
         $("#PrevCounter").val('');
         $("#TotalCounter").val('');
         $("#CurtCounter").val('');

         $("#MachineToner").append(
            `<thead>
               <th colspan="4" class="text-center">Machine Toner</th>
            </thead>
            <tr id="MachineTonerItem">
               <td width="25%">
                  <input class="form-control" placeholder="Cyan" autocomplete="off" id="machine_C" />
               </td>
               <td width="25%">
                  <input class="form-control" placeholder="Magenta" autocomplete="off" id="machine_M" />
               </td>
               <td width="25%">
                  <input class="form-control" placeholder="Yellow" autocomplete="off" id="machine_Y" />
               </td>
               <td width="25%">
                  <input class="form-control" placeholder="Black" autocomplete="off" id="machine_K" />
               </td>
            </tr>`
         );

         $("#DeliveryToner").append(
            `<thead>
               <th colspan="4" class="text-center">Delivery Toner</th>
            </thead>
            <tr id="DeliveryTonerItem">
               <td width="25%">
                  <input class="form-control" placeholder="Cyan" autocomplete="off" id="delivery_C" />
               </td>
               <td width="25%">
                  <input class="form-control" placeholder="Magenta" autocomplete="off" id="delivery_M" />
               </td>
               <td width="25%">
                  <input class="form-control" placeholder="Yellow" autocomplete="off" id="delivery_Y" />
               </td>
               <td width="25%">
                  <input class="form-control" placeholder="Black" autocomplete="off" id="delivery_K" />
               </td>
            </tr>`
         );

         if (currentMonthNumber == UsageDetail.paperUsageMonth && currentYear == UsageDetail.paperUsageYear) {
            $("#PrevCounter").val(UsageDetail.previousCounter);
            $("#CurtCounter").val(UsageDetail.currentCounter);
            $('#CurtCounter').attr('readonly', true);
            $("#TotalCounter").val(UsageDetail.monthlyTotalCounter);
         }
         else {
            $("#PrevCounter").val(UsageDetail.currentCounter);
            $('#CurtCounter').removeAttr('readonly');
         }
      }
   }
   else if (UsageDetail.tonerUsageMonth != currentMonthNumber && currentMonthNumber == UsageDetail.deliveryMonth && currentYear == UsageDetail.deliveryYear) {
      $("#MachineToner tr:eq(1)").remove();
      $("#MachineToner tr:eq(0)").remove();
      $("#DeliveryToner tr:eq(1)").remove();
      $("#DeliveryToner tr:eq(0)").remove();
      $("#AddUsageDetailItem").removeAttr("disabled");

      if (UsageDetail.colourType == 0) {
         $("#PrevCounter").val('');
         $("#TotalCounter").val('');
         $("#CurtCounter").val('');

         $("#MachineToner").append(
            `<thead>
               <th colspan="4" class="text-center">Machine Toner</th>
            </thead>
            <tr id="MachineTonerItem">
               <td colspan="4">
                  <input class="form-control" placeholder="Black N White" autocomplete="off" id="machine_BW" />
               </td>
            </tr>`
         );

         $("#DeliveryToner").append(
            `<thead>
               <th colspan="4" class="text-center">Delivery Toner</th>
            </thead>
            <tr id="DeliveryTonerItem">
               <td colspan=4">
                  <div class="alert alert-primary" role="alert">
                     Allready inserted ${currentMonthString}</b> month delivery toner for <b>${UsageDetail.machineSN}</b>.
                  </div>
               </td>
            </tr>`
         );
         // last month currentCounter value is for currentMonth previous counter

         if (currentMonthNumber == UsageDetail.paperUsageMonth && currentYear == UsageDetail.paperUsageYear) {
            $("#PrevCounter").val(UsageDetail.previousCounter);
            $("#CurtCounter").val(UsageDetail.currentCounter);
            $('#CurtCounter').attr('readonly', true);
            $("#TotalCounter").val(UsageDetail.monthlyTotalCounter);
         }
         else {
            $("#PrevCounter").val(UsageDetail.currentCounter);
            $('#CurtCounter').removeAttr('readonly');
         }
      }
      else {
         $("#PrevCounter").val('');
         $("#TotalCounter").val('');
         $("#CurtCounter").val('');

         $("#MachineToner").append(
            `<thead>
               <th colspan="4" class="text-center">Machine Toner</th>
            </thead>
            <tr id="MachineTonerItem">
               <td width="25%">
                  <input class="form-control" placeholder="Cyan" autocomplete="off" id="machine_C" />
               </td>
               <td width="25%">
                  <input class="form-control" placeholder="Magenta" autocomplete="off" id="machine_M" />
               </td>
               <td width="25%">
                  <input class="form-control" placeholder="Yellow" autocomplete="off" id="machine_Y" />
               </td>
               <td width="25%">
                  <input class="form-control" placeholder="Black" autocomplete="off" id="machine_K" />
               </td>
            </tr>`
         );


         $("#DeliveryToner").append(
            `<thead>
               <th colspan="4" class="text-center">Delivery Toner</th>
            </thead>
            <tr id="DeliveryTonerItem">
            <td colspan=4">
               <div class="alert alert-primary" role="alert">
                  Allready inserted ${currentMonthString}</b> month delivery toner for <b>${UsageDetail.machineSN}</b>.
               </div>
            </td>
         </tr>`
         );

         if (currentMonthNumber == UsageDetail.paperUsageMonth && currentYear == UsageDetail.paperUsageYear) {
            $("#PrevCounter").val(UsageDetail.previousCounter);
            $("#CurtCounter").val(UsageDetail.currentCounter);
            $('#CurtCounter').attr('readonly', true);
            $("#TotalCounter").val(UsageDetail.monthlyTotalCounter);
         }
         else {
            $("#PrevCounter").val(UsageDetail.currentCounter);
            $('#CurtCounter').removeAttr('readonly');
         }
      }
   }
   else if (UsageDetail.tonerUsageMonth == currentMonthNumber && currentMonthNumber != UsageDetail.deliveryMonth && currentYear != UsageDetail.deliveryYear) {
      $("#MachineToner tr:eq(1)").remove();
      $("#MachineToner tr:eq(0)").remove();
      $("#DeliveryToner tr:eq(1)").remove();
      $("#DeliveryToner tr:eq(0)").remove();
      $("#AddUsageDetailItem").removeAttr("disabled");

      if (UsageDetail.colourType == 0) {
         $("#PrevCounter").val('');
         $("#TotalCounter").val('');
         $("#CurtCounter").val('');

         $("#MachineToner").append(
            `<thead>
               <th colspan="4" class="text-center">Machine Toner</th>
            </thead>
            <tr id="MachineTonerItem">
            <td colspan=4">
               <div class="alert alert-primary" role="alert">
                  Allready inserted ${currentMonthString}</b> month machine toner for <b>${UsageDetail.machineSN}</b>.
               </div>
            </td>
          </tr>`
         );

         $("#DeliveryToner").append(
            `<thead>
               <th colspan="4" class="text-center">Delivery Toner</th>
            </thead>
            <tr id="DeliveryTonerItem">
               <td colspan="4">
                  <input class="form-control" placeholder="Black N White" autocomplete="off" id="delivery_BW" />
               </td>
            </tr>`
         );

         // last month currentCounter value is for currentMonth previous counter
         if (currentMonthNumber == UsageDetail.paperUsageMonth && currentYear == UsageDetail.paperUsageYear) {
            $("#PrevCounter").val(UsageDetail.previousCounter);
            $("#CurtCounter").val(UsageDetail.currentCounter);
            $('#CurtCounter').attr('readonly', true);
            $("#TotalCounter").val(UsageDetail.monthlyTotalCounter);
         }
         else {
            $("#PrevCounter").val(UsageDetail.currentCounter);
            $('#CurtCounter').removeAttr('readonly');
         }
      }
      else {
         $("#PrevCounter").val('');
         $("#TotalCounter").val('');
         $("#CurtCounter").val('');

         $("#MachineToner").append(
            `<thead>
               <th colspan="4" class="text-center">Machine Toner</th>
            </thead>
            <tr id="MachineTonerItem">
            <td colspan=4">
               <div class="alert alert-primary" role="alert">
                  Allready inserted ${currentMonthString}</b> month machine toner for <b>${UsageDetail.machineSN}</b>.
               </div>
            </td>
          </tr>`
         );

         $("#DeliveryToner").append(
            `<thead>
               <th colspan="4" class="text-center">Delivery Toner</th>
            </thead>
            <tr id="DeliveryTonerItem">
               <td width="25%">
                  <input class="form-control" placeholder="Cyan" autocomplete="off" id="delivery_C" />
               </td>
               <td width="25%">
                  <input class="form-control" placeholder="Magenta" autocomplete="off" id="delivery_M" />
               </td>
               <td width="25%">
                  <input class="form-control" placeholder="Yellow" autocomplete="off" id="delivery_Y" />
               </td>
               <td width="25%">
                  <input class="form-control" placeholder="Black" autocomplete="off" id="delivery_K" />
               </td>
            </tr>`
         );
         // last month currentCounter value is for currentMonth previous counter
         if (currentMonthNumber == UsageDetail.paperUsageMonth && currentYear == UsageDetail.paperUsageYear) {
            $("#PrevCounter").val(UsageDetail.previousCounter);
            $("#CurtCounter").val(UsageDetail.currentCounter);
            $('#CurtCounter').attr('readonly', true);
            $("#TotalCounter").val(UsageDetail.monthlyTotalCounter);
         }
         else {
            $("#PrevCounter").val(UsageDetail.currentCounter);
            $('#CurtCounter').removeAttr('readonly');
         }
      }
   }
   else {
      $("#MachineToner tr:eq(1)").remove();
      $("#MachineToner tr:eq(0)").remove();
      $("#DeliveryToner tr:eq(1)").remove();
      $("#DeliveryToner tr:eq(0)").remove();
      $("#PrevCounter").val('');
      $("#CurtCounter").val('');
      $("#TotalCounter").val('');

      $("#MachineToner").append(
         `<thead>
               <th colspan="4" class="text-center">Machine Toner</th>
            </thead>
            <tr id="MachineTonerItem">
            <td colspan=4">
               <div class="alert alert-primary" role="alert">
                  Allready inserted ${currentMonthString}</b> month machine toner for <b>${UsageDetail.machineSN}</b>.
               </div>
            </td>
          </tr>`
      );

      $("#DeliveryToner").append(
         `<thead>
            <th colspan="4" class="text-center">Delivery Toner</th>
          </thead>
         <tr id="DeliveryTonerItem">
            <td colspan=4">
               <div class="alert alert-primary" role="alert">
                  Allready inserted ${currentMonthString}</b> month delivery toner for <b>${UsageDetail.machineSN}</b>.
               </div>
            </td>
         </tr>`
      );
      // last month currentCounter value is for currentMonth previous counter
      if (currentMonthNumber == UsageDetail.paperUsageMonth && currentYear == UsageDetail.paperUsageYear) {
         $("#PrevCounter").val(UsageDetail.previousCounter);
         $("#CurtCounter").val(UsageDetail.currentCounter);
         $('#CurtCounter').attr('readonly', true);
         $("#TotalCounter").val(UsageDetail.monthlyTotalCounter);
      }
      else {
         $("#PrevCounter").val(UsageDetail.currentCounter);
         $('#CurtCounter').removeAttr('readonly');
      }

      // disable add item button beacuse of all information inserted for current month
      $("#AddUsageDetailItem").attr("disabled", "disabled");
   }
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


//================= Page count Calculation ===============================
$(".curtCounter, .prevCounter, .totalCounter").on("keydown keyup click", Counteralculation);
function Counteralculation() {
   // Page count calculation
   var totalColourToner = Number($("#CurtCounter").val()) - Number($("#PrevCounter").val());
   $("#TotalCounter").val(totalColourToner);
}

//================= Post Usages Value ===============================
// add UsageDetailItem to array.
var UsageDetailsItemContainer = [];
var UsageDetialItem = {};
var i = 1;

function AddUsageDetailItem() {
   if (UsageDetail.colourType == 0) {
      if (true) {
         
      }
      UsageDetialItem = {
         // machine id for all table
         machineId: UsageDetail.machineId,
         machineModel: UsageDetail.machineModel,
         machineSN: UsageDetail.machineSN,

         // delivery toner
         bw: Number($("#delivery_BW").val()),

         // TonerUsage
         machine_BW: Number($("#machine_BW").val()),
         InHouse: UsageDetail.bw,

         // PaperUsage
         previousCounter : Number($("#PrevCounter").val()),
         currentCounter : Number($("#CurtCounter").val()),
         monthlyTotalCounter: Number($("#TotalCounter").val()),

         // Profit
         CounterPerToner: UsageDetail.monthlyTotalCounter /UsageDetail.monthlyUsedToner,
      }


      //inMachineToner: UsageDetail.bw / 100,
      //   machineSN : UsageDetail.machineSN
      UsageDetail.colourTotal = 0;
      UsageDetail.bw = Number($("#machine_BW").val());
      UsageDetail.previousCounter = Number($("#PrevCounter").val());
      UsageDetail.currentCounter = Number($("#CurtCounter").val());
      UsageDetail.monthlyTotalCounter = Number($("#TotalCounter").val());
      var inMachineToner = UsageDetail.bw / 100;

      UsageDetail.cunterPerToner = Number(UsageDetail.monthlyTotalCounter / UsageDetail.monthlyUsedToner);
      if (UsageDetail.cunterPerToner > 400) {
         UsageDetail.isProfitable = true;
      }
      else {
         UsageDetail.isProfitable = false;
      }

      $("#AddRemoveUsageDetailItem").append(
         `<tr>
            <td class="text-center machineSN" id="machineSN_${i}">${UsageDetail.machineSN}</td>
            <td class="text-center inMachineToner" id="inMachineToner_${i}">${inMachineToner}</td>
            <td class="text-center delivery" id="delivery_${i}">${UsageDetail.bw}</td>
            <td class="text-center monthlyCount" id="monthlyCount_${i}">${UsageDetail.monthlyTotalCounter}</td>
            <td class="text-center IsProfitable" id="IsProfitable_${i}">${UsageDetail.isProfitable}</td>
            <td class="text-center td-remove">
               <button type="button" name="add" class="btn btn-sm btn-outline-danger waves-effect remove-tr">
                  Remove
               </button>
            </td>
         </tr>`
      );

   }
   else {

      UsageDetialItem = {
         // delivery toner
         machineId: UsageDetail.machineId,
         bw: Number($("#delivery_C").val()),
         cyan: Number($("#delivery_C").val()),
         magenta: Number($("#delivery_C").val()),
         yellow: Number($("#delivery_C").val()),
         black: Number($("#delivery_C").val()),
         colourTotal: 0,


         bw: Number($("#machine_BW").val()),
         previousCounter: Number($("#PrevCounter").val()),
         currentCounter: Number($("#CurtCounter").val()),
         monthlyTotalCounter: Number($("#TotalCounter").val()),
         inMachineToner: UsageDetail.bw / 100,
         machineSN: UsageDetail.machineSN
      }



      UsageDetail.percentageCyan = Number($("#machine_C").val());
      UsageDetail.percentageMagenta = Number($("#machine_M").val());
      UsageDetail.percentageYellow = Number($("#machine_Y").val());
      UsageDetail.percentageBlack = Number($("#machine_B").val());
      UsageDetail.totalColurParcentage = (UsageDetail.percentageCyan + UsageDetail.percentageMagenta + UsageDetail.percentageYellow + UsageDetail.percentageBlack)
      var inMachineToner = UsageDetail.totalColurParcentage /100

      UsageDetail.cyan = Number($("#toner_C").val());
      UsageDetail.magenta = Number($("#toner_M").val());
      UsageDetail.yellow = Number($("#toner_Y").val());
      UsageDetail.black = Number($("#toner_B").val());
      UsageDetail.colourTotal = UsageDetail.cyan + UsageDetail.magenta + UsageDetail.yellow + UsageDetail.black;

      UsageDetail.previousCounter = Number($("#PrevCounter").val());
      UsageDetail.currentCounter = Number($("#CurtCounter").val());
      UsageDetail.monthlyTotalCounter = Number($("#TotalCounter").val());
      /*var isProfitable = colourPerTonerPage*/

      $("#AddRemoveUsageDetailItem").append(
         `<tr>
            <td class="text-center machineSN" id="machineSN_${i}">${UsageDetail.machineSN}</td>
            <td class="text-center inMachineToner" id="inMachineToner_${i}">${inMachineToner}</td>
            <td class="text-center delivery" id="delivery_${i}">${UsageDetail.colourTotal}</td>
            <td class="text-center monthlyCount" id="monthlyCount_${i}">${UsageDetail.monthlyTotalCounter}</td>
            <td class="text-center IsProfitable" id="IsProfitable_${i}">${UsageDetail.IsProfitable}</td>
            <td class="text-center td-remove">
               <button type="button" name="add" class="btn btn-sm btn-outline-danger waves-effect remove-tr">
                  Remove
               </button>
            </td>
         </tr>`
      );
   }
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

var ItemContainer = [];
var i = 1;
function CalculateAddItemContainer() {
   if (deliveryTonerGetResult.colourType == 0) {
      deliveryTonerGetResult.bw = Number($("#toner_BW").val());
      deliveryTonerGetResult.colourTotal = 0;

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




