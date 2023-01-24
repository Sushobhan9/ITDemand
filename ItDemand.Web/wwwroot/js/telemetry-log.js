$(document).ready(function () {
	$('#tblTelemetry').DataTable({
		"processing": true,
		"serverSide": true,
		"order": [[1, "desc"]],
		"pageLength": 50,
		"ajax": {
			"url": "/Telemetry/LogEntries",
			"type": "POST",
			"datatype": "json"
		},
		"columns": [
			{ data: 'category' },
			{ data: 'entryDate', "render": function (value) { return new Date(value.toString()); } },
			{ data: 'type' },
			{
				data: 'userDisplayName',
				"render":
					function (data, type, row) {
						if (type === "display") {
							return "<a target='_blank' href='http://adlookup.le.grp/#/search?type=User&attrs=samaccountname&q=" + row.userAccountName + "'>" + data + "</a>";
						}
						return data;
					}
			},
			{ data: 'userRegion' },
			{ data: 'userBusinessUnit' },
			{ data: 'text' },
			{ data: 'browser' },
			{ data: 'hostAddress' },
			{ data: 'url' },
		]
	});
});