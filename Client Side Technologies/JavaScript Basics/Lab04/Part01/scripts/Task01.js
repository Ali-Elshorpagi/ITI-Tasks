// [a].
console.log('Task [a]: Select all images using two ways');
var imgs1 = document.getElementsByTagName('img');
console.log(imgs1);
var imgs2 = document.querySelectorAll('img');
console.log(imgs2);

// [b].
console.log('Task [b]: Select all option elements of cities drop down list');
var cities = document.getElementsByTagName('option');
console.log(cities);

// [c].
console.log('Task [c]: Get all rows of the second table');
var tables = document.querySelectorAll('table');
var secTable = tables[1];
var allRows = secTable.querySelectorAll('tr');
console.log(allRows);

// [d].
console.log('Task [c]: Get all elements that contain class name fontBlue');
var classes = document.getElementsByClassName('fontBlue');
console.log(classes);