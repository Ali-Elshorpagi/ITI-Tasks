import { renderTable } from './table.js';
import { sort, sortCol, sortOrder } from './sort.js';
import { search, searchText } from './search.js';
import { initPagination, currentPage, pageSize } from './pagination.js';
import { initDelete } from './delete.js';
import { initAdd } from './add.js';
import { initEdit } from './edit.js';

export let data = [];

export function updateEmployeeCount() {
    const countElement = document.getElementById('employeeCount');
    if (countElement) {
        countElement.textContent = `Total Employees: ${data.length}`;
    }
}

async function loadDataFromJson() {
    try {
        const response = await fetch('http://localhost:3000/employees', { method: 'GET' });
        const fetchedData = await response.json();
        data.push(...fetchedData);
        updateEmployeeCount();
        renderTable(data, searchText, sortCol, sortOrder, currentPage, pageSize);
        search();
        sort();
        initPagination();
        initDelete();
        initAdd();
        initEdit();
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}

loadDataFromJson();