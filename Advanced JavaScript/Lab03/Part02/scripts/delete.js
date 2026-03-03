
import { renderTable } from './table.js';
import { data, updateEmployeeCount } from './app.js';
import { sortCol, sortOrder } from './sort.js';
import { currentPage, pageSize } from './pagination.js';

export function initDelete() {
    const tableBody = document.getElementById('tableBody');

    tableBody.addEventListener('click', (e) => {
        const clickedElement = e.target;
        const isDeleteButton = clickedElement.classList.contains('delete-btn');

        if (isDeleteButton) {
            const employeeId = clickedElement.dataset.id;
            deleteRow(employeeId);
        }
    });
}

async function deleteRow(id) {
    const userConfirmed = confirm('Are you sure you want to delete this employee?');

    if (!userConfirmed)
        return;

    try {
        const response = await fetch(`http://localhost:3000/employees/${id}`, { method: 'DELETE' });

        if (response.ok) {
            const index = data.findIndex(item => item.id == id);
            if (index !== -1) {
                data.splice(index, 1);
                updateEmployeeCount();
                const searchInput = document.getElementById('searchInput');
                const searchText = searchInput ? searchInput.value.trim().toLowerCase() : '';
                renderTable(data, searchText, sortCol, sortOrder, currentPage, pageSize);
            }
        } else {
            alert('Failed to delete employee');
        }
    } catch (error) {
        console.error('Error deleting employee:', error);
        alert('Error deleting employee');
    }
}
