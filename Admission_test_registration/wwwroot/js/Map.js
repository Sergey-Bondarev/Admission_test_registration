
function initializeMap(universities) {
    var map = L.map('map').setView([49.686423945474424, 32.085129918935706], 6);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);
    universities.forEach(function (university) {
        var popupContent = '<i>' + university.name + '</i><hr />' +
            '<strong>Adress:</strong> ' + university.adress + '<hr />' +
            '<ul><li><strong>_Exam Shedule_</strong></li>';
        university.examScheduleElements.forEach(function (elem) {
            popupContent += '<li>' + elem.examTime.substring(0, elem.examTime.lastIndexOf(":")) + '</li>';
        });
        popupContent += '</ul>';
        L.marker([university.latitude, university.longitude])
            .addTo(map)
            .bindPopup(popupContent);
    });  
}