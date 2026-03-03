import { renderTable } from './table.js';
import { data, updateEmployeeCount } from './app.js';
import { sortCol, sortOrder } from './sort.js';
import { currentPage, pageSize } from './pagination.js';

export function initAdd() {
    const addButton = document.getElementById('addBtn');
    const employeeEditPopup = document.getElementById('employeeEditPopup');
    const employeeEditForm = document.getElementById('employeeEditForm');
    const closeButton = document.querySelector('.closeButton');
    const cancelButton = document.getElementById('cancelEditButton');

    addButton.addEventListener('click', () => {
        showAddForm();
    });

    closeButton.addEventListener('click', () => {
        employeeEditPopup.style.display = 'none';
    });

    cancelButton.addEventListener('click', () => {
        employeeEditPopup.style.display = 'none';
    });

    function showAddForm() {
        document.getElementById('employeeIdInput').value = '';
        document.getElementById('employeeNameInput').value = '';
        document.getElementById('employeePositionInput').value = '';
        document.getElementById('employeeOfficeInput').value = '';
        document.getElementById('employeeAgeInput').value = '';
        document.getElementById('employeeStartDateInput').value = '';
        document.getElementById('employeeSalaryInput').value = '';

        const formTitle = employeeEditPopup.querySelector('h2');
        formTitle.textContent = 'Add New Employee';

        employeeEditForm.dataset.mode = 'add';
        employeeEditPopup.style.display = 'flex';
    }
}

export async function addEmployee() {
    const newEmployee = {
        name: document.getElementById('employeeNameInput').value,
        position: document.getElementById('employeePositionInput').value,
        office: document.getElementById('employeeOfficeInput').value,
        age: parseInt(document.getElementById('employeeAgeInput').value),
        startDate: document.getElementById('employeeStartDateInput').value,
        salary: parseFloat(document.getElementById('employeeSalaryInput').value)
    };

    try {
        const response = await fetch('http://localhost:3000/employees', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newEmployee)
        });

        if (response.ok) {
            const addedEmployee = await response.json();
            data.push(addedEmployee);
            updateEmployeeCount();

            const searchInput = document.getElementById('searchInput');
            const searchText = searchInput ? searchInput.value.trim().toLowerCase() : '';
            renderTable(data, searchText, sortCol, sortOrder, currentPage, pageSize);

            const employeeEditPopup = document.getElementById('employeeEditPopup');
            employeeEditPopup.style.display = 'none';
        } else {
            alert('Failed to add employee');
        }
    } catch (error) {
        console.error('Error adding employee:', error);
        alert('Error adding employee');
    }
}

