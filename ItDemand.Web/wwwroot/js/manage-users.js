function renderDate(value) {
	const monthShortNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

	if (value === null) return "";

	const dt = new Date(value);

	return dt.getDate() + " " + monthShortNames[dt.getMonth()] + " " + dt.getFullYear();
}

$(document).ready(function () {
	$('#tblUsers').DataTable({
		"processing": true,
		"serverSide": true,
		"order": [[0, "asc"]],
		"pageLength": 50,
		"ajax": {
			"url": "/User/Users",
			"type": "POST",
			"datatype": "json"
		},
		"columns": [
			{ data: 'displayName' },
			{ data: 'userName' },
			{ data: 'email' },
			{ data: 'securityRoleNames' },
			{ data: 'isActive' },
			{ data: 'created', "render": function (value) { return renderDate(value); } },
			{ data: 'lastModified', "render": function (value) { return renderDate(value); } },
			{
				data: 'id',
				"render":
					function (data, type, row) {
						if (type === "display") {
							return "<a href='/User/EditUser/" + data + "'><i class='fa fa-pen-to-square title='Edit User'></i></a>";
						}
						return data;
					},
				"orderable": false
			}
		]
	});
});