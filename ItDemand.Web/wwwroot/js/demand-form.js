// Initialize Datepicker and setup click event for calander icon.
//

$(".datepicker:not(input[name='BenefitStart'])").datepicker({ dateFormat: 'M dd yy' });
$("input[name='BenefitStart']").datepicker({ dateFormat: 'yy M' }); // they only want [Year Month] display for Benefit Start field

$(document).off("click", "div.input-group:has(i.fa-calendar)");
$(document).on("click",
   "div.input-group:has(i.fa-calendar)",
   function () {
      var datepicker = $(this).find(".datepicker");
      $(datepicker).datepicker("show");
   });

// People Picker
// Make named function to be able to assign separately to the assigned-sme
// people picker. That people picker needs to be able to be disabled separately
// from the others.
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

// Input Clear (X) on People Pickers
// 
function clearPeoplePicker() {
   $(this).parent().find('input').val("");
}

$(document).off("click", "div.input-group-overlay:has(i.fa-times)");
$(document).on("click", "div.input-group-overlay:has(i.fa-times):not(.assigned-sme)", clearPeoplePicker);
$(document).on("click", "div.assigned-sme:has(i.fa-times)", clearPeoplePicker);


$(document).off("click", "span.input-group-text:has(i.fa-user)");
$(document).on("click", "span.input-group-text:has(i.fa-user):not(.assigned-sme)", peoplePickerFunc);
$(document).on("click", "div.assigned-sme:has(i.fa-user)", peoplePickerFunc);

function showSaveMsg(msg) { $("#saveMsg").html(msg); $("#saveAlert").show(); }
function hideSaveMsg() { $("#saveAlert").hide(); }
function showValidationErrorsMsg() { $("#validationErrorsAlert").show(); }
function hideValidationErrorsMsg() { $("#validationErrorsAlert").hide(); }

// Show the status alert at the top of the page based on type and message passed in.
//
function showStatusAlert(msg, type) {
   let bootstrapAlert;

   switch (type) {
      case "info":
         bootstrapAlert = "alert-info";
         break;
      case "warning":
         bootstrapAlert = "alert-warning";
         break;
      case "success":
         bootstrapAlert = "alert-success";
         break;
      default:
         bootstrapAlert = "alert-info";
         break;
   }

   $('#demandStatusFeedback').html(
      "<div class='alert " + bootstrapAlert + " role='alert'>" +
      "<a class='close' data-bs-dismiss='alert'></a>" +
      "<p style='margin-bottom: 0px;'>" + msg + "</p>" +
      "</div>");
}

function clearStatusAlert() { $('#demandStatusFeedback').html(''); }

// Functions to display how many characters the field has when inputting their Name or Executive Short Description.
//
function count_chars_demandName() {
   document.getElementById('demandNameCharsCount').innerHTML = document.getElementById('Name').value.length;
}

function count_chars() {
   document.getElementById('executiveDescCharsCount').innerHTML = document.getElementById('ExecutiveShortDescription').value.length;
}

// Function used to determine if Medical Validation has been selected in the Compliance
// Relevent multi-select drop-down.
//
function isMedicalValidationSelected() {
   const optionSelected = $("option:selected", '#ComplianceRelevant').text();
   return optionSelected.indexOf('Medical Validation') >= 0 ? true : false;
}

$('#ComplianceRelevant').on('change',
   function () {
      const $medicalReferenceLabelSpan = $("#MedicalValidationReference").parent().find('span');

      if (isMedicalValidationSelected()) {
         $("#medicalReferenceCol").show();

         // Add required indicator to Medical Validation Reference Label.
         $medicalReferenceLabelSpan.addClass('required-asterisk');
      } else {
         $("#medicalReferenceCol").hide();

         // Remove required indicator from Medical Validation Reference Label.
         $medicalReferenceLabelSpan.removeClass('required-asterisk');
      }
   });

// When someone changes the Business Unit selection get and pre-populate
// the IT Head for that Business Unit into the IT Head people picker control.
//
$('#BusinessUnitId').on('change',
   function () {
      const buCtrl = document.getElementById('BusinessUnitId');
      const buIdSelected = buCtrl.options[buCtrl.selectedIndex].value;

      // For the Business Unit selected get the assigned IT Head from the database.
      fetch('/Demand/GetItHead?businessUnitId=' + buIdSelected, {
         method: 'GET',
      })
         .then(response => response.json())
         .then(response => {
            if (response.success === true) {
               document.getElementById('ItHead.UserName').value = response.itHead.userName;
               document.getElementById('ItHead.Email').value = response.itHead.email;
               document.getElementById('ItHead.DisplayName').value = response.itHead.displayName;
            } else {
               alert(response.message);
            }
         });

      
   });

// Toggle Request Corrections comment box.
$('#RequestCorrections').on('change',
   function () {
      const optionSelected = $("option:selected", this).val();

      if (optionSelected === 'true') {
         $("#requestCorrectionsCommentsRow").slideDown();
      } else {
         $("#requestCorrectionsCommentsRow").slideUp();
      }
   });

// Required indicators for Benefits per Year and Benefit Duration
// based on selection of Business Driver drop-down.
function SetBusinessDriverRequiredIndicators() {
   const businessDriver = $("#BusinessDriverId option:selected").text();
   const $benefitsPerYearSpan = $("#BenefitsPerYear").parent().parent().find('label span');
   const $benefitDurationSpan = $("#BenefitDuration").parent().find('span');

   if (businessDriver === 'Cost Savings / Cost Reduction' ||
      businessDriver === 'Growth (Price / Volume / Increase Capability)' ||
      businessDriver === 'Strategic Investment') {
      // Add required indicators.
      $benefitsPerYearSpan.addClass('required-asterisk');
      $benefitDurationSpan.addClass('required-asterisk');
   } else {
      // Remove required indicators.
      $benefitsPerYearSpan.removeClass('required-asterisk');
      $benefitDurationSpan.removeClass('required-asterisk');
   }
}

$('#BusinessDriverId').on('change', SetBusinessDriverRequiredIndicators);

// For inputs with numbers-only class, only allow numbers to be entered.
$('.numbers-only').keyup(function () { this.value = this.value.replace(/[^0-9]/g, ''); });

// When someone chooses Local from the Application Type drop-down:
// 1. Remove the required indicator on the Process Area drop-down.
// 2. Show the Application Description textarea.
//
$('#ApplicationTypeId').on('change',
   function () {
      const optionSelected = $("option:selected", this).text();
      const $processAreaLabelSpan = $("#ProcessArea").parent().find('span');

      if (optionSelected === 'Local') {
         $("#localApplicationRow").slideDown();

         // Remove required indicator from Process Area Label.
         $processAreaLabelSpan.removeClass('required-asterisk');
      } else {
         $("#localApplicationRow").slideUp();

         // Add required indicator to Process Area.
         $processAreaLabelSpan.addClass('required-asterisk');
      }
   });

// Cancel Demand
// Located on the bottom left of the Demand Form/Information page.
// Can be clicked by Demand Creator or PMO.
// Will not delete the demand from the system, just marks it as cancelled (so it may be reinstated at a later date).
//
$("#btnCancelDemand").click(function () {
   const demandId = parseInt(document.getElementsByName("Id")[0].value);
   App.Dialogs.confirm("<p>Are you sure you want to cancel this Demand?</p><p>A cancelled Demand may be reopened, but will reset to it's initially created state.</p>", function (answer) {
      if (answer === false) return;

      fetch('/Demand/Cancel/' + demandId, {
         method: 'POST',
      })
         .then(response => response.json())
         .then(response => {
            if (response.success === true) {
               document.location = response.redirectUrl;
            } else {
               alert(response.message);
            }
         });
   });
});

// Will appear in header for a Cancelled Demand. Will reinstate the demand
// so it can be edited and submitted again.
// To Do: reinstate button should only be available to Admins and PMO
$("#btnReinstate").click(function () {
   const demandId = parseInt(document.getElementsByName("Id")[0].value);

   fetch('/Demand/Reinstate/' + demandId, {
      method: 'POST',
   })
      .then(response => response.json())
      .then(response => {
         if (response.success === true) {
            document.location = response.redirectUrl;
         } else {
            alert(response.message);
         }
      });
});

// Delete Demand
// Delete the demand request and any data associated with it (attachments, workflow, etc.).
//
$("#btnDeleteDemand").click(function () {
   const demandId = parseInt(document.getElementsByName("Id")[0].value);
   App.Dialogs.confirm("<p>Are you sure you want to delete this Demand?<p><p><strong>Warning!</strong> This action will delete this Demand Entry from the system and cannot be undone!</p>", function (answer) {
      if (answer === false) return;

      fetch('/Demand/Delete/' + demandId, {
         method: 'POST',
      })
      .then(response => response.json())
      .then(response => {
         if (response.success === true) {
            document.location = response.redirectUrl;
         } else {
            alert(response.message);
         }
      });

      // Using this fetch config expects object containing one field (id) on server method end.
      //fetch('/Demand/Delete', {
      //   method: 'POST',
      //   headers: { 'Content-Type': 'application/json' },
      //   body: JSON.stringify({id: demandId})
      //})
      //.then(response => response.json())
      //.then(response => {
      //   if (response.success === true) {
      //      //document.location = response.redirectUrl;
      //      console.log('Delete successful!');
      //   } else {
      //      alert(response.message);
      //   }
      //});
   });
});

// Business Process cascading drop-down option queries.
$('#BusinessProcessL1Id').on('change',
   function () {
      const l1OptionSelected = $("option:selected", this).val();

      // if this is (re)selected reset L2 & L3 (clear out and disable)
      const $l2SelectCtrl = $('#BusinessProcessL2Id');
      const $l3SelectCtrl = $('#BusinessProcessL3Id');
      $l2SelectCtrl.find('option').not(':first').remove();
      $l3SelectCtrl.find('option').not(':first').remove();
      $l2SelectCtrl.prop('disabled', true);
      $l3SelectCtrl.prop('disabled', true);

      // query database for L2 options passing this selections Id as the parent Id.
      fetch('/Demand/BusinessProcessL2Options?parentId=' + l1OptionSelected, {
         method: 'GET',
      })
      .then(response => response.json())
      .then(response => {
         if (response.success === true) {
            if (response.options.length > 0) {
               response.options.map(function (option) {
                  const $option = $('<option>');

                  $option
                     .val(option['id'])
                     .text(option['name']);

                  $l2SelectCtrl.append($option);
               });

               //enable the L2 select box
               $('#BusinessProcessL2Id').prop('disabled', false);
            }
         } else {
            alert(response.message);
         }
      });
   });

$('#BusinessProcessL2Id').on('change',
   function () {
      const l2OptionSelected = $("option:selected", this).val();

      // if this is (re)selected reset L2 & L3 (clear out and disable)
      const $l3SelectCtrl = $('#BusinessProcessL3Id');
      $l3SelectCtrl.find('option').not(':first').remove();
      $l3SelectCtrl.prop('disabled', true);

      // query database for L3 options passing this selections Id.
      fetch('/Demand/BusinessProcessL3Options?parentId=' + l2OptionSelected, {
         method: 'GET',
      })
      .then(response => response.json())
      .then(response => {
         if (response.success === true) {
            if (response.options.length > 0) {
               response.options.map(function (option) {
                  const $option = $('<option>');

                  $option
                     .val(option['id'])
                     .text(option['name']);

                  $l3SelectCtrl.append($option);
               });

               //enable the L3 select box
               $('#BusinessProcessL3Id').prop('disabled', false);
            }
         } else {
            alert(response.message);
         }
      });
   });

$('#ProcessAreaId').on('change',
   function () {
      const optionSelected = $("option:selected", this).text();
      let l1PreSelect = '';
      switch (optionSelected) {
         case 'HR': l1PreSelect = 'Human Resources'; break;
         case 'Procurement': l1PreSelect = 'Procurement'; break;
         case 'Finance & Controlling': l1PreSelect = 'Finance & Controlling'; break;
         case 'Customer Management': l1PreSelect = 'Customer Management'; break;
         case 'Order to Cash': l1PreSelect = 'Order to Cash'; break;
         case 'Cylinder Supply Chain': l1PreSelect = 'Supply Chain - Cylinder & Hardgood'; break;
         case 'Bulk & On-site Supply Chain': l1PreSelect = 'Supply Chain - Bulk'; break;
         default:
      }

      $('#BusinessProcessL1Id option:contains(' + l1PreSelect + ')').attr('selected', true).change();
   });

function disablePmoReviewControls() {
   // Disable the controls for PMO review if the current
   // user is not part of the IT PMO group.
   let $isPmo = $("#IsPmo").val();
   let $isBusinessConsulting = $("#IsBusinessConsulting").val();

   if ($isPmo === "False") {
      $(".pmo-only").attr("disabled", "disabled");
   }

   // Disable the controls for Categorization on the PMO Review
   // tab if not part of PMO or Global Business Consulting groups.
   if ($isPmo === "False" && $isBusinessConsulting === "False") {
      $(".gbc-only").attr("disabled", "disabled");
      // Disable Assigned SME people picker.
      $(document).off("click", "div.assigned-sme:has(i.fa-user)", peoplePickerFunc);
      $("body").off("click", "div.assigned-sme:has(i.fa-times)", clearPeoplePicker);
   }
}

function handleAjaxError(jqXHR, textStatus, errorThrown) {
   const errorText =
      '<div class="alert alert-danger" role="alert">' +
      '<a class="close" data-bs-dismiss="alert"></a>' +
      '<p><strong>An error occurred while saving the form.</strong></p>' +
      '<p>Status Code: ' + jqXHR.status + '</p>' +
      '<p>Error Thrown: ' + errorThrown + '</p>' +
      '</div>';

   $('#errorMsg').html(errorText);
   console.log('ajax error [textStatus]: ' + textStatus);
   $(this).scrollTop(0);
}

function PmoReviewValidator() {
   // Linde Gases IT wants the fields listed in rules below
   // to be required when the Demand is in the PMO Review state. 
   //
   // Remove requirement of Application Categorization fields if corrections
   // have been requested since this going back to the user and not forward
   // in the Demand Review process.
   let $demandState = $("#DemandState").val();
   const requestCorrections = $("#RequestCorrections option:selected").val();

   if ($demandState === "PmoReview" && requestCorrections === 'false') {

      const $pmoReviewFormValidator = $('form').validate({
         rules: {
            NewApplication: { required: true },
            ProposedPlatform: { required: true },
            DecommissionRequired: { required: true }
         },
         highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
         },
         unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
         },
         errorElement: 'span',
         errorClass: 'help-block',
         errorPlacement: function (error, element) {
            const attr = element.attr('multiple');
            if (typeof attr !== typeof undefined && attr !== false) {
               error.insertAfter(element.next('.btn-group'));
            } else if (element.parent('.input-group').length) {
               error.insertAfter(element.parent());
            } else {
               error.insertAfter(element);
            }
         }
      });

      const isValid = $pmoReviewFormValidator.form();
      return isValid;
   } else {
      return true;
   }
}

//
$("#btnSaveSubmit").click(function () {
   $.validator.addMethod("multiSelectCheck",
      function (value, element) {
         var count = $(element).find('option:selected').length;
         return count > 0;
      });

   const $demandFormValidator = $('form').validate({
      rules: {
         Name: { required: true },
         ProblemStatement: { required: true },
         BusinessUnitId: { required: true },
         AffectedBusinessUnits: { required: true, multiSelectCheck: true }, // https://stackoverflow.com/questions/10225928/validating-multiselect-with-jquery-validation-plugin
         ComplianceRelevant: { required: true, multiSelectCheck: true },
         UsersImpactedId: { required: true },
         BusinessBenefit: { required: true },
         ConsequenceNotImplemented: { required: true },
         ScopeSummary: { required: true },
         'RequestOwner.DisplayName': { required: true },
         'RequestSponsor.DisplayName': { required: true },
         'ProjectManager.DisplayName': { required: true },
         'ItHead.DisplayName': { required: true },
         EstimatedInternalEffort: { required: true },
         EstimatedTotalCost: { required: true },
         ApplicationTypeId: { required: true },
         ApplicationDescription: {
            required: {
               depends: function () {
                  return $("#ApplicationTypeId option:selected").text() === 'Local';
               }
            }
         },
         BusinessDriverId: { required: true },
         BenefitsPerYear: {
            required: {
               depends: function () {
                  let businessDriver = $("#BusinessDriverId option:selected").text();
                  return (businessDriver === 'Cost Savings / Cost Reduction' ||
                     businessDriver === 'Growth (Price / Volume / Increase Capability)' ||
                     businessDriver === 'Strategic Investment');
               }
            }
         },
         BenefitDuration: {
            required: {
               depends: function () {
                  let businessDriver = $("#BusinessDriverId option:selected").text();
                  return (businessDriver === 'Cost Savings / Cost Reduction' ||
                     businessDriver === 'Growth (Price / Volume / Increase Capability)' ||
                     businessDriver === 'Strategic Investment');
               }
            }
         },
         MedicalValidationReference: {
            required: {
               depends: function () {
                  return isMedicalValidationSelected();
               }
            }
         },
         InterfaceChanges: { required: true },
         SecurityAssessment: { required: true },
         ItHead: { required: true },
         ProcessAreaId: {
            required: {
               depends: function () {
                  return $("#ApplicationTypeId option:selected").text() !== 'Local';
               }
            }
         },
         BaselineCapex: { number: true }
      },
      ignore: [':hidden:not("#AffectedBusinessUnits")', ':hidden:not("#ComplianceRelevant")'], // tells the validator to check the hidden input of the multiselect
      highlight: function (element) {
         $(element).closest('.form-control').addClass('is-invalid');
         $(element).closest('.form-select').addClass('is-invalid');
         $(element).closest('button.form-select').addClass('is-invalid');
      },
      unhighlight: function (element) {
         $(element).closest('.form-control').removeClass('is-invalid');
         $(element).closest('.form-select').removeClass('is-invalid');
         $(element).closest('.button.form-select').removeClass('is-invalid');
      },
      errorElement: 'span',
      errorClass: 'text-danger',
      errorPlacement: function (error, element) {
         const attr = element.attr('multiple');
         if (typeof attr !== typeof undefined && attr !== false) {
            error.insertAfter(element.next('.btn-group'));
         } else if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
         } else {
            error.insertAfter(element);
         }
      }
   });

   const isValid = $demandFormValidator.form();

   if (isValid) {
      $("#SubmittedForReview").val(true); // mark the hidden form field that tracks if submitted as true
      demandSubmit(true);
   }
   hideSaveMsg();
   showValidationErrorsMsg();
});

// Save the data on the form but keep the form open.
$("#btnSave").click(function () {
   let isValid = PmoReviewValidator();

   if (isValid) {
      demandSubmit(false);
   }

   hideSaveMsg();
   showValidationErrorsMsg();
   return false;
});

// Save the data on the form and redirect to the Status page.
$('#demandRequestForm').submit(function () {
   let isValid = PmoReviewValidator();

   if (isValid) {
      demandSubmit(true);
   }

   hideSaveMsg();
   showValidationErrorsMsg();
   return false;
});

function demandSubmit(willRedirect) {
   hideSaveMsg();
   hideValidationErrorsMsg();

   App.Dialogs.busyBox.show("Saving, please wait...");

   // re-enable disabled controls from submit - do before getting form data!
   $(".pmo-only").removeAttr("disabled");
   $(".gbc-only").removeAttr("disabled");

   const demandForm = document.getElementById('demandRequestForm');
   const formData = new FormData(demandForm);

   $('form:dirty').dirtyForms('setClean'); // disable 'dirty' flag when saving data

   // ToDo: switch to fetch api for posting form data
   $.ajax({
      type: "POST",
      url: "/Demand/Save",
      processData: false,  // https://www.mattlunn.me.uk/blog/2012/05/sending-formdata-with-jquery-ajax/
      contentType: false,
      data: formData,
      cache: false,
      dataType: "json"
   })
      .done(function (data, textStatus, jqXHR) {
         if (data.success === true) {
            if (willRedirect) {
               document.location = data.redirectUrl;
            }
            if (data.demandId) {
               $("input[name='Id']").val(data.demandId);
            }
            const id = $("input[name='Id']").val();
            showSaveMsg("<i class='fa fa-check' aria-hidden='true'></i> <strong>Save Successful:</strong> Demand-" + id + " was successfully saved.");

            // make sure this field tracks if a request corrections was done and
            // the user just hits the save button and not save and close
            if (typeof data.submittedForReview !== 'undefined') {
               console.log('submit for pmo review: ', data.submittedForReview);
               document.getElementById("SubmittedForReview").value = data.submittedForReview;
            }
            if (typeof data.demandState !== 'undefined') {
               console.log('demandState: ', data.demandState);
               document.getElementById("DemandState").value = data.demandState;
            }
         } else {
            handleAjaxError(jqXHR, textStatus, data.message);
         }
      })
      .fail(function (jqXHR, textStatus, errorThrown) {
         handleAjaxError(jqXHR, textStatus, errorThrown);
      })
      .always(function () {
         disablePmoReviewControls();
         App.Dialogs.busyBox.hide();
      });

   return false;
}

$(document).ready(function () {
   toggleAttachmentMsg();
   hideSaveMsg();
   hideValidationErrorsMsg();
   disablePmoReviewControls();

   // For changes I made to js files to work with Bootstrap 5: https://github.com/davidstutz/bootstrap-multiselect/issues/1230
   $('#AffectedBusinessUnits').multiselect({ buttonWidth: '100%' });
   $('#ComplianceRelevant').multiselect({ buttonWidth: '100%' });

   const demandId = document.getElementById("Id").value;
   const submitForPmoReview = document.getElementById("SubmittedForReview").value;
   const cancelledOn = document.getElementById("CancelledOn").value;
   const executionType = $("input[name='ExecutionType']:checked").val();

   // If this demand has been cancelled, disable all form inputs and controls.
   // Leave the Delete button available to Admins & PMO only.
   if (cancelledOn) {
      $("#demandRequestForm :input").prop("disabled", true);

      // Disable date pickers.
      $(".datepicker").datepicker("disable");
      $("span.input-group-text:has(i.fa-calendar)").removeClass('cursor-pointer');

      // Disable people pickers.
      $(document).off("click", "span.input-group-text:has(i.fa-user)", peoplePickerFunc);
      $(document).off("click", "div.input-group-overlay:has(i.fa-times)", clearPeoplePicker);

      $("span.input-group-text:has(i.fa-user)").removeClass('cursor-pointer');
      $('div.input-group-overlay:has(i.fa-times)').removeClass('cursor-pointer');
   }

   // Initialize any status display messages.
   if (demandId < 1) {
      $("#demandIdDisplay").hide();
   }
   
   if (submitForPmoReview === 'False' && demandId > 0 && !cancelledOn) {
      showStatusAlert(
         '<i class="fa fa-exclamation-circle" aria-hidden="true"></i> This Demand Request has been saved but not yet submitted for review by the Corporate IT Project Management Office.',
         'warning');
   }

   if (submitForPmoReview === 'True' && !executionType) {
      showStatusAlert(
         '<i class="fa fa-info-circle" aria-hidden="true"></i> This Demand Request is currently awaiting review by the Corporate IT Project Management Office.',
         'info');
   }

   // On page load initialize the character count messages for the fields
   // below that are character limited.
   document.getElementById('executiveDescCharsCount').innerHTML = document.getElementById('ExecutiveShortDescription').value.length;
   document.getElementById('demandNameCharsCount').innerHTML = document.getElementById('Name').value.length;

   const applicationType = $("#ApplicationTypeId option:selected").text();
   if (applicationType !== 'Local') {
      $("#localApplicationRow").hide();
   }

   if (!isMedicalValidationSelected()) {
      $("#medicalReferenceCol").hide();
   }

   // Show/Hide the Corrections Comments based on if someone selected 'Yes/No'.
   const requestCorrections = $("#RequestCorrections option:selected").val();
   if (requestCorrections === 'false') {
      $("#requestCorrectionsCommentsRow").hide();
   }

   // Disable L2 and L3 Business Process dropdowns to start if empty.
   if ($('#BusinessProcessL2Id > option').length <= 1) {
      $('#BusinessProcessL2Id').prop('disabled', true);
   }
   if ($('#BusinessProcessL3Id > option').length <= 1) {
      $('#BusinessProcessL3Id').prop('disabled', true);
   }

   // Set initial display of fields depending on the Business Driver
   // selection.
   SetBusinessDriverRequiredIndicators();

   if (requestCorrections === 'false') {
      $("#requestedCorrectionsAlert").hide();
   } else {
      let correctionsRequestedBy = "";
      if (document.getElementById("RequestCorrectionsByDisplayName")) {
         correctionsRequestedBy = document.getElementById("RequestCorrectionsByDisplayName").value;
      }

      let correctionsRequestedOn = "";
      if (document.getElementById("RequestCorrectionsDate")) {
         correctionsRequestedOn = document.getElementById("RequestCorrectionsDate").value;
      }

      $('#requestedCorrectionsAlert').html(
         "<a class='close' data-bs-dismiss='alert'></a>" +
         "<p style='margin-bottom: 0px;'><i class='fa fa-info-circle'></i> Corrections were requested for this Demand by <strong>" + correctionsRequestedBy + "</strong> on <strong>" + correctionsRequestedOn + "</strong>.</p>");
   }

   if (submitForPmoReview === 'True') {
      $("#btnSaveSubmit").hide();
      $("#saveInstructions").hide();
   }
});