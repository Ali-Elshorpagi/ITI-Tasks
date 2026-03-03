import { renderTable } from './table.js';
import { data } from './app.js';
import { currentPage, pageSize } from './pagination.js';
import { searchText } from './search.js';

export let sortCol = null;
export let sortOrder = 'asc';

function updateSortArrows() {
    const headers = document.querySelectorAll('th[id]');
    headers.forEach(header => {
        const arrow = header.querySelector('.sort-arrow');
        if (arrow) {
            arrow.classList.remove('asc', 'desc');
            if (header.id === sortCol) {
                arrow.classList.add(sortOrder);
            }
        }
    });
}

export function sort() {
    const headers = document.querySelectorAll('th[id]');

    for (const header of headers) {
        header.addEventListener('click', function () {
            const clickedCol = header.id;
            if (sortCol === clickedCol)
                sortOrder = sortOrder === 'asc' ? 'desc' : 'asc';
            else {
                sortCol = clickedCol;
                sortOrder = 'asc';
            }
            updateSortArrows();
            renderTable(data, searchText, sortCol, sortOrder, currentPage, pageSize);
        });
    }
}