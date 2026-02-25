// [a].
console.log('Task [a]: Get first anchor inside the second table');
var tables = document.querySelectorAll('table');
var secTable = tables[1];
var firstAnchor = document.querySelector('a');
firstAnchor.href = 'training.com';
firstAnchor.textContent = 'Training';
console.log(firstAnchor);

// [b].
console.log('Task [b]: Get first anchor inside the second table');
var allImages = document.querySelectorAll('img');
var lastImage = allImages[allImages.length - 1];
lastImage.style.border = "solid pink 2px";
console.log(lastImage);

// [c].
console.log('Task [c]: function to Find all checkboxes (checked) in userData form and alert their values');
function getCheckedValues() {
    var form = document.getElementById('UserData');
    var checkboxes = form.querySelectorAll('input[type="checkbox"]:checked');
    var values = [];
    for (var i = 0; i < checkboxes.length; ++i)
        values.push(checkboxes[i].value);

    if (values.length > 0)
        alert('Checked values: ' + values.join(', '));
    else
        alert('No checkboxes are checked');
}
// getCheckedValues();

// [d].
console.log('Task [d]: Find element with id value "example" then change its background color to pink');
var element = document.getElementById('example');
element.style.backgroundColor = 'pink';
console.log(element);