function renderDate(value) {
	const monthShortNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

	if (value === null) return "";

	const dt = new Date(value);

	return dt.getDate() + " " + monthShortNames[dt.getMonth()] + " " + dt.getFullYear();
}

function getStatusFilter() {
	debugger;
	const statusFilterCtrl = document.getElementById('checklistStatusFilter');
	return statusFilterCtrl.options[statusFilterCtrl.selectedIndex].value;
}

$(document).ready(function () {
	let myApprovalsTable = $('#tblMyApprovals').DataTable({
		"processing": true,
		"serverSide": true,
		"order": [[0, "asc"]],
		"pageLength": 10,
		"ajax": {
			"url": "/User/MyApprovals",
			"type": "POST",
			"datatype": "json",
			"data": function(d) { d.statusFilter = getStatusFilter() }
		},
		"columns": [
			{
				data: 'demandRequestId',
				"render":
					function (data, type, row) {
						if (type === "display") {
							return "<a href='/Project/Index/" + data + "'>GPS-IT-" + data + "</a>";
						}
						return data;
					},
			},
			{ data: 'requestName' },
			{
				data: 'checklistName',
				"render":
					function (data, type, row) {
						if (type === "display") {
							return "<a href='/Project/Index/" + row.demandRequestId + "/#checklist=" + row.checklistId + "'>" + data + "</a>";
						}
						return data;
					},
			},
			{ data: 'approvalDate', "render": function (value) { return renderDate(value); } },
		]
	});

	$('#tblMyRequests').DataTable({
		"processing": true,
		"serverSide": true,
		"order": [[0, "asc"]],
		"pageLength": 10,
		"ajax": {
			"url": "/User/MyDemandRequests",
			"type": "POST",
			"datatype": "json"
		},
		"columns": [
			{
				data: 'id',
				"render":
					function (data, type, row) {
						if (type === "display") {
							if (row.executionType) {
								return "<a href='/Project/Index/" + data + "'>GPS-IT-" + data + "</a>";
							} else {
								return "<a href='/Demand/DemandRequestForm/" + data + "'>GPS-IT-" + data + "</a>";
							}
						}
						return data;
					},
			},
			{ data: 'requestName' },
			{ data: 'requestOwner' },
			{ data: 'requestSponsor' },
			{ data: 'projectManager' },
			{ data: 'executionType' },
			{ data: 'demandState' },
			{ data: 'createdDate', "render": function (value) { return renderDate(value); } },
		]
	});

	$('#checklistStatusFilter').change(function () {
		myApprovalsTable.ajax.reload();
	})
});