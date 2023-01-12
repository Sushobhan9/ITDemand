(function ($) {
   $.fn.navtree = function (config) {
      var options = $.extend({}, $.fn.navtree.defaults, config);

      return this.each(function () {
         var container = $(this);

         container.toggleItem = function (item) {
            var children = $(item).parent('li.parent_li').find(' > ul > li');
            if (children.is(":visible")) {
               children.hide('fast');
               $(item).attr('title', options.expandTooltip).find(' > i').addClass(options.expandIconClass).removeClass(options.collapseIconClass);
            } else {
               children.show('fast');
               $(item).attr('title', options.collapseTooltip).find(' > i').addClass(options.collapseIconClass).removeClass(options.expandIconClass);
            }
         };

         container.find('li:has(ul)').addClass('parent_li').find(' > span').attr('title', options.collapseTooltip);
         container.find('li.parent_li > span').on('click', function (e) {
            container.toggleItem($(this));
            e.stopPropagation();
         });

         container.find("a").click(
            function () {
               if (typeof options.onNodeClicked === 'function')
                  options.onNodeClicked($(this));

               $(".navtree a").each(function (index, item) {
                  $(item).parent("li").removeClass("active");
               });

               $(this).parent("li").addClass("active");
            }
         );

         if (options.collapsed) {
            var parents = container.find('li.parent_li > span');
            parents.each(function () {
               container.toggleItem(this);
            });
         }
      });
   };

   $.fn.navtree.defaults = {
      collapsed: false,
      expandTooltip: 'Expand this branch',
      collapseTooltip: 'Collapse this branch',
      expandIconClass: 'fa-square-plus',
      collapseIconClass: 'fa-square-minus',

      //callbacks
      onNodeClicked: function () { }
   };
})(jQuery);
