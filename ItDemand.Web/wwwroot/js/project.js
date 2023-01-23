$("#navigation").navtree({
   onNodeClicked: function (node) {
      if (node.attr("target") === "_blank") return;
      setActiveLink(node);
   }
});

const routes = {
   "status": function () { loadStatus(); },
   "checklist": function (tokens) { loadChecklist(tokens.checklist); },
   "information": function () { loadInformation(); }
};

function getParameterByName(name, url) {
   if (!url) url = window.location.href;
   name = name.replace(/[\[\]]/g, '\\$&');
   var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
      results = regex.exec(url);
   if (!results) return null;
   if (!results[2]) return '';
   return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function updateBreadcrumb(text) {
   $(".breadcrumb li.active").html(text);
}

function setActiveLink(node) {
   $("#navigation a").removeClass("active");
   node.addClass("active");
}

function loadPartial(url, waitMessage, onLoaded) {
   App.Dialogs.busyBox.show(waitMessage);

   fetch(url).then(function (response) {
      return response.text();
   }).then(function (html) {
      $("#project-details").html(html);
      if (typeof onLoaded === 'function') onLoaded($(this));
   }).catch(function (err) {
      alert('Something went wrong.', err);
   }).finally(function () {
      App.Dialogs.busyBox.hide();
   });
}

function loadChecklist(checklistId) {
   loadPartial('/Project/Checklist/' + checklistId,
      "Loading checklist, Please wait...",
      function () {
         $.getScript("/js/checklist.js");
      }
   );
   updateBreadcrumb($('#navigation a[href$="#checklist=' + checklistId + '"]').text());
}

function loadInformation() {
   const id = document.getElementById('projectPage').getAttribute('data-demandId');
   loadPartial('/Demand/DemandRequestForm/' + id,
      "Loading information page, Please wait...",
      function () {
         $.getScript("/js/demand-attachment.js");
         $.getScript("/js/demand-form.js");        
         // If the Attachments link was clicked to get here, go directly
         // to it.
         if (getParameterByName("showAttachmentTab") === 'true')
            $('button#attachments-tab').tab('show');

      });
   updateBreadcrumb('Information');
}

function loadStatus() {
   const id = document.getElementById('projectPage').getAttribute('data-demandId');
   loadPartial('/Project/Status/' + id,
      "Loading status page, Please wait...",
      function () {
         $("#diagram").gateflow({
            url: '/Project/WorkflowData/' + id
         });
         $("#history").navtree({ collapsed: true });
      }
   );
   updateBreadcrumb('Status');
}

function getRouteTokens() {
   var hash = window.location.hash.slice(1);
   return $.deparam(hash, true);
}

$(window).bind("hashchange",
   function () {
      const tokens = getRouteTokens();
      const token = Object.keys(tokens)[0];
      var routeName = token;

      if (routeName && routeName.indexOf('?') > -1) {
         routeName = token.substr(0, token.indexOf('?')); // strip off any query params
      }

      if (routeName === undefined || routeName === null)
         routes[Object.keys(routes)[0]](tokens);
      else
         routes[routeName.toLowerCase()](tokens);
   });

$(document).ready(function () {
   // To Do: bind the rich text editor
   
   $(window).trigger('hashchange'); // Fire the hashchange event when the page first loads

   // Handle case where someone navigates directly to a page
   // in the Project.
   if (document.referrer === "") {
      $('a[href="#status"]').removeClass("active");
      $('a[href="' + window.location.hash + '"]').addClass("active");
   }
});