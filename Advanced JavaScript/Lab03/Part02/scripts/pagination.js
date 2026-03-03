import { renderTable } from './table.js';
import { data } from './app.js';
import { sortCol, sortOrder } from './sort.js';
import { searchText } from './search.js';

export let currentPage = 1;
export let pageSize = 10;

export function initPagination() {
    let pageSizeSelect = document.getElementById('pageSize');

    pageSizeSelect.addEventListener('change', function () {
        pageSize = parseInt(pageSizeSelect.value);
        currentPage = 1;
        renderTable(data, searchText, sortCol, sortOrder, currentPage, pageSize);
    });
}

export function renderPagination(totalItems) {
    let paginationContainer = document.getElementById('pagination');
    paginationContainer.innerHTML = '';

    let totalPages = Math.ceil(totalItems / pageSize);

    for (let page = 1; page <= totalPages; ++page) {
        let pageButton = document.createElement('button');
        pageButton.innerText = page;

        if (page === currentPage)
            pageButton.classList.add('active');

        pageButton.addEventListener('click', function () {
            currentPage = page;
            renderTable(data, searchText, sortCol, sortOrder, currentPage, pageSize);
        });

        paginationContainer.append(pageButton);
    }
}