var App = App || {};

App.StatusTypes = {
   New: 0,
   InProgress: 1,
   WaitingApproval: 2,
   Approved: 3,
   Rejected: 4
};

App.WorkflowTypes = {
   ItDemandReview: 1,
   ProceedLocallyL1: 2,
   ProceedLocallyL2: 3,
   FastTrackL3: 4,
   BigProjectL4: 5,
   Unknown: 99
};

App.ApprovalType = {
   ClickToSign: 1,
   PassFailWithNoApprover: 2,
   PassFailWithApprover: 3
};

App.ApprovalChoiceType = {
   Pass: 1,
   NotPassed: 2
};

App.getFormattedDate = function () {
   var date = new Date();
   var year = date.getFullYear();
   var month = (1 + date.getMonth()).toString();
   month = month.length > 1 ? month : '0' + month;
   var day = date.getDate().toString();
   day = day.length > 1 ? day : '0' + day;
   return month + '/' + day + '/' + year;
};

App.Dialogs = {
   busyBox: (function () {
      // uses partial _LoadingIndictor.cshtml loaded in Layout.cshtml
      let loadingModal = new bootstrap.Modal(document.getElementById('indicator-dialog'), {
         backdrop: 'static',
         keyboard: false
      });
      
      return {
         show: function (text) {
            document.getElementById('loadingMessage').innerHTML = text;
            loadingModal.show();
         },
         hide: function () {
            loadingModal.hide();
         }
      };
   }()),
   //messageBox: (function (msg) {
   //   // uses partial _MessageModal.cshtml loaded in Layout.cshtml
   //   let messageModal = new bootstrap.Modal(document.getElementById('message-dialog'), {
   //      backdrop: 'static',
   //      keyboard: false
   //   });

   //   document.getElementById('messageDialogContent').innerHTML = msg;

   //   const btnOk = document.getElementById('btnMessageOk');

   //   function okButtonClick() {
   //      close();
   //   }

   //   let close = function () {
   //      btnOk.removeEventListener('click', okButtonClick);
   //      messageModal.hide();
   //   }

   //   btnOk.addEventListener('click', okButtonClick);

   //   messageModal.show();
   //}()),
   confirm: function (prompt, callback) {
      // uses partial _ConfirmModal.cshtml loaded in Layout.cshtml
      let confirmModal = new bootstrap.Modal(document.getElementById('confirm-dialog'), {
         backdrop: 'static',
         keyboard: false
      });
      
      document.getElementById('confirmPrompt').innerHTML = prompt;

      const btnYes = document.getElementById('btnConfirmYes');
      const btnNo = document.getElementById('btnConfirmNo');

      function yesButtonClick() {
         if (typeof callback === 'function') { callback(true); }
         close();
      }

      function noButtonClick() {
         if (typeof callback === 'function') { callback(false); }
         close();
      }

      let close = function () {
         btnYes.removeEventListener('click', yesButtonClick);
         btnNo.removeEventListener('click', noButtonClick);
         confirmModal.hide();
      }

      btnYes.addEventListener('click', yesButtonClick);
      btnNo.addEventListener('click', noButtonClick);

      confirmModal.show();
   }
};