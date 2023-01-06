var DashboardSettingsCache = (function () {

   // Corporate IT Dashboard Page Settings
   if (sessionStorage.getItem("itBusinessUnit") === null) {
      sessionStorage.setItem("itBusinessUnit", '');
   }

   if (sessionStorage.getItem("itProjectType") === null) {
      sessionStorage.setItem("itProjectType", '0');
   }

   if (sessionStorage.getItem("itDemandState") === null) {
      sessionStorage.setItem("itDemandState", '0');
   }

   if (sessionStorage.getItem("itDataTablePageSize") === null) {
      sessionStorage.setItem("itDataTablePageSize", 50);
   }

   return {

      getItDashboardSettings: function () {
         return {
            itBusinessUnit: sessionStorage.getItem("itBusinessUnit"),
            itProjectType: sessionStorage.getItem("itProjectType"),
            itDemandState: sessionStorage.getItem("itDemandState"),
            itDataTablePageSize: sessionStorage.getItem("itDataTablePageSize")
         };
      },

      setItBusinessUnit: function (businessUnit) {
         sessionStorage.setItem("itBusinessUnit", businessUnit);
      },

      setItProjectType: function (projectType) {
         sessionStorage.setItem("itProjectType", projectType);
      },

      setItDemandState: function (demandState) {
         sessionStorage.setItem("itDemandState", demandState);
      },

      setItDataTablePageSize: function (pageSize) {
         sessionStorage.setItem("itDataTablePageSize", pageSize);
      },

      getItDataTablePageSize: function () {
         return sessionStorage.getItem("itDataTablePageSize");
      },

      initialize: function () {
         setItBusinessUnit('All');
         setItProjectType('All');
         setItDemandState('All');
         setItDataTablePageSize(50);
      }

   };

}());