// Initialize Date Picker controls
//
$(".datepicker").datepicker({ dateFormat: 'M dd yy' });

$(document).off("click", "div.input-group:has(i.fa-calendar)");
$(document).on("click",
   "div.input-group:has(i.fa-calendar)",
   function () {
      var datepicker = $(this).find(".datepicker");
      $(datepicker).datepicker("show");
   });

// Initialize People Picker controls
function peoplePickerFunc() {
   var input = $(this).parent().find("input:not(:hidden)");
   var hiddenUserName = $(this).parent().find(":hidden[name*='UserName']");
   var hiddenEmail = $(this).parent().find(":hidden[name*='Email']");
   var hiddenId = $(this).parent().find(":hidden[name*='.Id']");

   App.Dialogs.userSearch.show(function (displayName, userName, email) {
      if (displayName) input.val(displayName);
      if (userName) hiddenUserName.val(userName);
      if (email) hiddenEmail.val(email);
      hiddenId.val(0);
   });
}

$(document).off("click", "span.input-group-text:has(i.fa-user)");
$(document).on("click", "span.input-group-text:has(i.fa-user)", peoplePickerFunc);

// Input Clear (X) on People Pickers
// 
function clearPeoplePicker() {
   $(this).parent().find('input').val("");
}

$(document).off("click", "div.input-group-overlay:has(i.fa-times)");
$(document).on("click", "div.input-group-overlay:has(i.fa-times)", clearPeoplePicker);

function setApprovalDate($approvalElement) {
   $approvalElement.closest('.approver').find("input[name*='ApprovalDate']").datepicker('setDate', new Date());
}

$("span:has(i.fa-pen-to-square)").click(function () {
   $(this).empty();
   $(this).append("<i class='fa fa-check icon-spacer'></i> Signed");
   setApprovalDate($(this));
});

$("#btnInProgress").click(function () {
   $("#Status").val(App.StatusTypes.InProgress);
   $("#checklistForm").submit();
});

$("#btnReject").click(function () {
   $("#Status").val(App.StatusTypes.Rejected);
   $("#checklistForm").submit();
});

$("#btnWaitingApproval").click(function (e) {
   $("#Status").val(App.StatusTypes.WaitingApproval);
   $("#checklistForm").submit();
});

function disableForm() {
   let $form = $('#checklistForm');
   $form.find("input, textarea, select, button").attr("disabled", "disabled");

   // keep the save button available
   $('#btnSave').prop('disabled', false);
   $('#btnInProgress').prop('disabled', false);
   $('#btnReject').prop('disabled', false);

   // Disable date pickers.
   $(".datepicker").datepicker("disable");
   $('div.input-group-addon:has(i.fa-calendar)').removeClass('pointer');

   // Disable people pickers.
   $(document).off("click", "div.input-group-addon:has(i.fa-user)");
   $(document).off("click", "div.input-group-overlay:has(i.fa-times)");
   $('div.input-group-addon:has(i.fa-user)').removeClass('pointer');
   $('div.input-group-overlay:has(i.fa-times)').removeClass('pointer');
}

function handleAjaxError(jqXHR, textStatus, errorThrown) {
   const errorText =
      '<div class="alert alert-danger" role="alert">' +
      '<a class="close" data-dismiss="alert">×</a>' +
      '<p><strong>An error occurred while saving the form.</strong></p>' +
      '<p>Status Code: ' + jqXHR.status + '</p>' +
      '<p>Error Thrown: ' + errorThrown + '</p>' +
      '</div>';

   $('#feedbackMsg').html(errorText);
   console.log('ajax error [textStatus]: ' + textStatus);
}

$("form").submit(function (e) {
   // enable any disabled controls for submit
   $('#checklistForm').find("input, textarea, select").removeAttr("disabled");

   const checklistForm = document.getElementById('checklistForm');
   const formData = new FormData(checklistForm);

   App.Dialogs.busyBox.show("Saving, please wait...");
   $.ajax({
      type: "POST",
      url: "/Project/ChecklistSave",
      processData: false,
      contentType: false,
      data: formData,
      cache: false,
      dataType: "json"
   })
      .done(function (data, textStatus, jqXHR) {
         if (data.success === true) {
            App.Dialogs.busyBox.hide();
            App.Dialogs.messageBox("The checklist was saved successfully.", function () {
               document.location = '/Project/Index/' + data.demandId;
            });
         }
         else {
            $('#feedbackMsg').html('<div class="alert alert-danger alert-sm" role="alert"><i class="fa fa-circle-exclaimation"></i> An error occurred while saving the checklist.</div>');
         }
      })
      .fail(function (jqXHR, textStatus, errorThrown) {
         handleAjaxError(jqXHR, textStatus, errorThrown);
      })
      .always(function () {
         App.Dialogs.busyBox.hide();
      });

   return false;
});

$(document).ready(function () {
   $('#feedbackMsg').empty();
   if ($("#Status").val() === "WaitingApproval" || $("#Status").val() === "Approved") {
      disableForm();
   }
});