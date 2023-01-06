// Attachments tab/table
$("#btnAddAttachment").click(function (e) {
   addAttachmentRow($("#attachmentTable"), null);
   toggleAttachmentMsg();
   e.preventDefault();
});

$(document).off("click", "#attachmentTable .deleteRow");
$(document).on("click", "#attachmentTable .deleteRow", function (e) {
   deleteAttachmentRow($(this), "Are you sure you want to delete this File Attachment?");
   toggleAttachmentMsg();
   e.preventDefault();
});

// Hook up to the event for when a file is selected.
$(document).off("change", ":file");
$(document).on('change', ':file', function () {
   let $fileInput = $(this),
      fileSize = 0,
      fileName = "unknown.txt";

   if ($fileInput[0].files.length > 0) {
      fileSize = $fileInput[0].files[0].size;
      fileName = $fileInput[0].files[0].name;
   }

   let $sizeDisplay = $(this).closest('td').siblings('.file-size-cell').find('span.file-size-display');
   $sizeDisplay.text(formatBytes(fileSize, 1));
});

function addAttachmentRow(table, eventHandler) {
   const index = table.find("tbody > tr").length;

   const row =
      "<tr>" +
      "<td>" +
      "<input type='file' name='Attachments[" + index + "].File' id='Attachments[" + index + "].File' style='width: 100%; display: inline-block; margin-top: 6px;' />" +
      "<input type='hidden' name='Attachments[" + index + "].FileName' value=''></input>" + // we need to second input for some reason to get the model binder to see this
      "</td>" +
      "<td></td>" +
      "<td></td>" +
      "<td class='file-size-cell text-center'>" +
      "<span class='file-size-display' style='display: inline-block; margin-top: 6px;'></span>" +
      "</td>" +
      "<td class='text-center'><a href='javascript:void(0)' class='btn pointer deleteRow'><i class='fa fa-times'></i></a></td>" +
      "</tr>";

   table.append(row);
   if (eventHandler !== null) {
      $(row).find('i.fa-pencil-square-o').on('click', eventHandler).trigger('click');
   }
}

function deleteAttachmentRow(icon, confirmText) {
   let table = icon.parents("table");
   App.Dialogs.confirm(confirmText, function (result) {
      if (result === true) {
         icon.parents("tr").remove();
         table.find("tbody > tr").each(function (rowIndex, row) {
            $(row).find("input, textarea, select").each(function (ctrlIndex, ctrl) {
               let name = ctrl.name.replace(new RegExp("[0-9+]"), rowIndex);
               $(ctrl).attr({ "id": name, "name": name });
               if ($(ctrl).attr("name").indexOf("Index") > -1) {
                  $(ctrl).attr("value", rowIndex);
               }
            });
         });
         toggleAttachmentMsg();
      }
   });
}

function toggleAttachmentMsg() {
   if ($("#attachmentTable tbody").children().length === 0) {
      $("#noAttachmentsMsg").show();
   } else {
      $("#noAttachmentsMsg").hide();
   }
}

function formatBytes(bytes, decimals) {
   if (bytes === 0) return '0 Byte';
   const k = 1000; // or 1024 for binary
   const dm = decimals + 1 || 3;
   const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
   const i = Math.floor(Math.log(bytes) / Math.log(k));
   return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
}