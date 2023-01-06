var App = App || {};
App.Dialogs = App.Dialogs || {};

App.Dialogs.userSearch = (function () {
   return {
      show: function (callback) {
         let dlgModal = new bootstrap.Modal(document.getElementById('user-dialog'), {
            backdrop: 'static',
            keyboard: false
         });

         let $dlg = $(dlgModal._element);

         document.getElementById('searchTerm').value = "";

         var search = function (keyword, attribute) {
            if (keyword.length == 0) return;

            const tableBody = document.getElementById('userResultsBody');
            tableBody.innerHTML = ""; // clear out any previous data
            const rows = document.createDocumentFragment();

            // send query with user entered keyword to UsersController
            document.getElementById("userResultsLoading").style.display = "block";
            fetch('/User/Search?query=' + keyword + '&attribute=' + attribute)
               .then(function (response) {
                  return response.json();
               })
               .then(function (data) {
                  // hide Searching... message and show results
                  document.getElementById("userResultsLoading").style.display = "none";

                  // map each returned row into an HTML table row
                  data.map((user, idx) => {
                     let tr = document.createElement('tr');
                     let tdCheckbox = document.createElement('td');
                     let tdDisplayName = document.createElement('td');
                     let tdUserName = document.createElement('td');
                     let tdCountry = document.createElement('td');
                     let tdTitle = document.createElement('td');

                     // create the checkbox used to select a specific user
                     let checkbox = document.createElement('input');
                     checkbox.type = 'checkbox';
                     checkbox.id = 'userSelect' + idx;
                     checkbox.value = user.userName;

                     tdCheckbox.appendChild(checkbox);

                     tdDisplayName.innerHTML = user.displayName;
                     tdDisplayName.setAttribute('data-field', 'DisplayName');
                     tdUserName.innerHTML = user.userName;
                     tdUserName.setAttribute('data-field', 'UserName');
                     tr.setAttribute('data-email', user.email);
                     tdCountry.innerHTML = user.country;
                     tdTitle.innerHTML = user.title;

                     tr.appendChild(tdCheckbox);
                     tr.appendChild(tdDisplayName);
                     tr.appendChild(tdUserName);
                     tr.appendChild(tdCountry);
                     tr.appendChild(tdTitle);
                     rows.appendChild(tr);
                     tableBody.appendChild(rows);

                     // bind event watcher to click of a checkbox
                     $dlg.find('input[type=checkbox]').change(function () {
                        const btn = document.getElementById('btnUserSelect');
                        if ($("#userResultsBody input:checkbox:checked").length > 0) {
                           btn.removeAttribute('disabled');
                        }
                        else {
                           btn.setAttribute('disabled', '');
                        }
                     })

                     // show the table with returned users
                     document.getElementById("userResultsTable").style.display = "block";
                  });
               })
               .catch(function (error) {
                  document.getElementById("userResultsLoading").style.display = "none";
                  console.log("User Search error: ", error);
                  // handle the error
               });
         }

         // Wire up event to watch for hitting the enter button on the search input control.
         function searchKeypress(e) {
            if (e.which != 13) return;
            const keyword = this.value.trim();
            const attribute = document.getElementById('attribute').value;
            search(keyword, attribute);
         }
         const searchInput = document.getElementById('searchTerm');
         searchInput.addEventListener('keypress', searchKeypress);

         // Wire up event to watch for click the magnifying glass icon.
         function searchIconClick() {
            const keyword = document.getElementById('searchTerm').value;
            const attribute = document.getElementById('attribute').value;
            search(keyword, attribute);
         }
         const searchIcon = document.getElementById('userSearchIcon');
         searchIcon.addEventListener('click', searchIconClick);

         // Wire up event to watch for click of Select button.
         function selectButtonClick() {
            var row = $($dlg.find('input:checkbox:checked').closest("tr"));
            var displayNameCol = row.find("td[data-field='DisplayName']");
            var userNameCol = row.find("td[data-field='UserName']");
            var email = row.data("email");
            close();
            callback(displayNameCol.text(), userNameCol.text(), email);
         }
         const selectButton = document.getElementById('btnUserSelect');
         selectButton.addEventListener('click', selectButtonClick);

         // Wire up event to watch for close icon click.
         function closeIconClick() {
            close();
         }
         const closeIcon = document.getElementById('userSearchCloseIcon');
         closeIcon.addEventListener('click', closeIconClick);

         let close = function () {
            searchInput.removeEventListener('keypress', searchKeypress);
            searchIcon.removeEventListener('click', searchIconClick);
            selectButton.removeEventListener('click', selectButtonClick);
            document.getElementById("userResultsTable").style.display = "none";
            document.getElementById('btnUserSelect').setAttribute('disabled', '');
            dlgModal.hide();
         }

         dlgModal.show();
      }
   }
})();