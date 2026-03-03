import { renderTable } from './table.js';
import { data } from './app.js';
import { sortCol, sortOrder } from './sort.js';
import { pageSize } from './pagination.js';

export let searchText = '';

export function search() {
    let searchInput = document.getElementById('searchInput');

    searchInput.addEventListener('keyup', function () {
        searchText = searchInput.value.trim().toLowerCase();
        renderTable(data, searchText, sortCol, sortOrder, 1, pageSize);
    });
}
