function renderDate(value) {
	const monthShortNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

	if (value === null) return "";

	const dt = new Date(value);
	 
	let hours = dt.getHours(); // 0 - 23
	let minutes = dt.getMinutes(); // 0 - 59
	let seconds = dt.getSeconds(); // 0 - 59

	var ampm = hours >= 12 ? 'PM' : 'AM';
	hours = hours % 12;
	hours = hours ? hours : 12; // the hour '0' should be '12'

	hours = hours < 10 ? '0' + hours : hours;
	minutes = minutes < 10 ? '0' + minutes : minutes;
	seconds = seconds < 10 ? '0' + seconds : seconds;

	return dt.getDate() + " " + monthShortNames[dt.getMonth()] + " " + dt.getFullYear() + " " + hours + ":" + minutes + ":" + seconds + " " + ampm;
}

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
			{ data: 'entryDate', "render": function (value) { return renderDate(value); /*return new Date(value.toLocaleString());*/ } },
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