(function ($) {
   $.fn.gateflow = function (config) {
      var options = $.extend({}, $.fn.gateflow.defaults, config);

      function createTerminator(text) {
         return $("<li class='terminator'>" +
            "<div class='workflow-item'>" +
            "<div class='description'>" + text + "</div>" +
            "<div class='terminator'></div>" +
            "</div>" +
            "</li>");
      }

      function createArrow() {
         return $("<li class='arrow'><i class='fa fa-arrow-right fa-lg'></i></li>");
      }

      return this.each(function () {
         var container = $(this);

         $.ajax({
            url: options.url,
            method: 'POST'
         })
            .done(function (data) {
               var workflowDiv = $("<div>");
               var ul = $("<ul class='gateflow'>");

               $.each(data.stages, function (index, val) {
                  var li = $("<li class='click-target'>");
                  li.addClass(val.shape);
                  li.data("stage", val.stage);

                  if (val.stage === data.currentStage) {
                     li.addClass("current");
                  }

                  var shapeContainer = $("<div>");
                  shapeContainer.addClass("workflow-item");

                  var textDiv = $("<div>");
                  textDiv.addClass("description");
                  textDiv.text(val.title);

                  var shapeDiv = $("<div>");
                  shapeDiv.addClass(val.shape === "process" ? "rectangle" : "diamond");

                  shapeContainer.append(textDiv).append(shapeDiv);
                  li.append(shapeContainer);

                  if (index === 0) {
                     ul.append(createTerminator(options.startText));
                     ul.append(createArrow());
                  }

                  ul.append(li);

                  if (index !== data.stages.length - 1) {
                     ul.append(createArrow());
                  }
                  else {
                     ul.append(createArrow());
                     ul.append(createTerminator(options.finishText));
                  }

                  workflowDiv.append(ul);
               });

               container.append(workflowDiv);

               var infoDiv = $("<div class='well gateInfo'>");
               container.append(infoDiv);

               var currentElement = $("ul.gateflow").find("li.current");
               $("ul.gateflow").scrollLeft(currentElement.position().left - 100);

               $(container).on("click", "li.click-target", function () {
                  if ($(this).hasClass("arrow")) return;
                  var isCurrent = $(this).hasClass("current");
                  $(container).find("li").removeClass("active");
                  $(this).addClass("active");
                  var html = "<h4>Workflow Stage: " + $(this).find(".description").html() + (isCurrent ? " <span class='text-primary'>(Current)</span>" : "") + "</h4>";
                  var selectedStage = $(this).data("stage");
                  var description = $.map(data.stages, function (val) { return val.stage === selectedStage ? val.description : ''; });
                  var gateInfo = $(container).find(".gateInfo");
                  gateInfo.empty();
                  gateInfo.append(html);
                  gateInfo.append($("<div>").html(description).text());
               });

               var current = container.find("li.current");
               if (current.length === 1) current.click();
            })
            .fail(function (jqXhr, textStatus, errorThrown) { //catch                
               alert("An error occured: " + errorThrown);
            });
      });
   };

   $.fn.gateflow.defaults = {
      url: $(location).attr('href'),
      startText: "Start",
      finishText: "Finish"
   };
})(jQuery);
