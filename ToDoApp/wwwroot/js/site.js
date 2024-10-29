// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const dateFormatOptions = {
	year: 'numeric',
	month: '2-digit',
	day: '2-digit',
	hour: '2-digit',
	minute: '2-digit',
	hour12: false
};

toastr.options = {
	closeButton: true,
	progressBar: true,
	preventDuplicates: true,
	positionClass: "toast-bottom-right",
};

function getFirstMonday(year) {
	const firstDay = new Date(year, 0, 1, 9, 0);
	const daysToMonday = (8 - firstDay.getDay()) % 7;
	const firstMonday = new Date(firstDay);
	firstMonday.setDate(firstDay.getDate() + daysToMonday);
	
	return firstMonday;
}

function parseDateToISOString(dateString) {
	const [day, month, year] = dateString.split(".");
	const date = new Date(`${year}-${month}-${day}T09:00`);
	
	return date.toISOString().slice(0, 16);
}

function handleError(xhr, status, error) {
	if (xhr.responseJSON.errors)
		toastr.error(xhr.responseJSON.errors.map(x => x.ErrorMessage).join('<br />'), "Error");
	else
		toastr.error(status, error);
}

function getMondayOfWeek(year, week) {
	const firstMonday = getFirstMonday(year);
	const mondayOfSelectedWeek = new Date(firstMonday);
	mondayOfSelectedWeek.setDate(firstMonday.getDate() + (week - 1) * 7);
	
	return mondayOfSelectedWeek;
};