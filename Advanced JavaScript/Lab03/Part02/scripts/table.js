import { renderPagination } from './pagination.js';

export function renderTable(data, searchText, sortCol, sortOrder, currentPage, pageSize) {
    let tableBody = document.getElementById('tableBody');
    tableBody.innerHTML = '';

    let tempData = [...data];

    if (searchText) {
        tempData = tempData.filter(item => {
            return item.name.toLowerCase().includes(searchText) ||
                item.position.toLowerCase().includes(searchText) ||
                item.office.toLowerCase().includes(searchText);
        });
    }

    if (sortCol) {
        tempData.sort((a, b) => {
            let aValue = a[sortCol];
            let bValue = b[sortCol];

            if (!isNaN(aValue) && !isNaN(bValue)) {
                aValue = Number(aValue);
                bValue = Number(bValue);
            }

            if (aValue < bValue)
                return sortOrder === 'asc' ? -1 : 1;
            if (aValue > bValue)
                return sortOrder === 'asc' ? 1 : -1;

            return 0;
        });
    }

    let start = (currentPage - 1) * pageSize
    let end = start + pageSize
    let pageData = tempData.slice(start, end)

    renderPagination(tempData.length);

    for (const item of pageData) {
        let row = document.createElement('tr');

        row.innerHTML = `
            <td>${item.id}</td>
            <td>${item.name}</td>
            <td>${item.position}</td>
            <td>${item.office}</td>
            <td>${item.age}</td>
            <td>${item.startDate}</td>
            <td>${item.salary}</td>
            <td>
                <button class="edit-btn" data-id="${item.id}">Edit</button>
                <button class="delete-btn" data-id="${item.id}">Delete</button>
            </td>
        `;

        tableBody.append(row);
    }
}