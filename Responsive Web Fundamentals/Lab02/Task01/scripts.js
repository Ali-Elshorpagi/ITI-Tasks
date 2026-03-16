let childs = document.querySelectorAll('.child');
let left = document.querySelector('.left');
let right = document.querySelector('.drop-zone');

childs.forEach(child => {
    child.addEventListener('dragstart', (e) => {
        e.dataTransfer.setData('myChild', e.target.outerHTML);
    });

    child.addEventListener('dragend', (e) => {
        e.target.style.display = 'none';
    });
});

right.addEventListener('dragenter', (e) => {
    e.preventDefault();
});

right.addEventListener('dragover', (e) => {
    e.preventDefault();
});

right.addEventListener('drop', (e) => {
    right.innerHTML += e.dataTransfer.getData('myChild');
});
