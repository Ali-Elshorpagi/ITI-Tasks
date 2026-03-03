import { renderTable } from './table.js';
import { data, updateEmployeeCount } from './app.js';
import { sortCol, sortOrder } from './sort.js';
import { currentPage, pageSize } from './pagination.js';
import { addEmployee } from './add.js';

export function initEdit() {
    const tableBody = document.getElementById('tableBody');
    const employeeEditPopup = document.getElementById('employeeEditPopup');
    const employeeEditForm = document.getElementById('employeeEditForm');
    const closeButton = document.querySelector('.closeButton');
    const cancelButton = document.getElementById('cancelEditButton');

    tableBody.addEventListener('click', (e) => {
        const clickedButton = e.target;
        const isEditButton = clickedButton.classList.contains('edit-btn');

        if (isEditButton) {
            const employeeId = clickedButton.dataset.id;
            showEditForm(employeeId);
        }
    });

    closeButton.addEventListener('click', () => {
        employeeEditPopup.style.display = 'none';
    });

    cancelButton.addEventListener('click', () => {
        employeeEditPopup.style.display = 'none';
    });

    employeeEditForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        if (employeeEditForm.dataset.mode === 'edit') {
            await updateEmployee();
        } else {
            await addEmployee();
        }
    });

    function showEditForm(id) {
        const employee = data.find(item => item.id == id);

        if (!employee) {
            alert('Employee not found!');
            return;
        }

        document.getElementById('employeeIdInput').value = employee.id;
        document.getElementById('employeeNameInput').value = employee.name;
        document.getElementById('employeePositionInput').value = employee.position;
        document.getElementById('employeeOfficeInput').value = employee.office;
        document.getElementById('employeeAgeInput').value = employee.age;
        document.getElementById('employeeStartDateInput').value = employee.startDate;
        document.getElementById('employeeSalaryInput').value = employee.salary;

        const formTitle = employeeEditPopup.querySelector('h2');
        formTitle.textContent = 'Edit Employee';

        employeeEditForm.dataset.mode = 'edit';
        employeeEditPopup.style.display = 'flex';
    }

    async function updateEmployee() {
        const employeeId = document.getElementById('employeeIdInput').value;

        const updatedEmployee = {
            id: employeeId,
            name: document.getElementById('employeeNameInput').value,
            position: document.getElementById('employeePositionInput').value,
            office: document.getElementById('employeeOfficeInput').value,
            age: parseInt(document.getElementById('employeeAgeInput').value),
            startDate: document.getElementById('employeeStartDateInput').value,
            salary: parseFloat(document.getElementById('employeeSalaryInput').value)
        };

        try {
            const response = await fetch(`http://localhost:3000/employees/${employeeId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(updatedEmployee)
            });

            if (response.ok) {
                const index = data.findIndex(item => item.id == employeeId);
                if (index !== -1) {
                    data[index] = updatedEmployee;
                    updateEmployeeCount();

                    const searchInput = document.getElementById('searchInput');
                    const searchText = searchInput ? searchInput.value.trim().toLowerCase() : '';
                    renderTable(data, searchText, sortCol, sortOrder, currentPage, pageSize);

                    employeeEditPopup.style.display = 'none';
                }
            } else {
                alert('Failed to update employee');
            }
        } catch (error) {
            console.error('Error updating employee:', error);
            alert('Error updating employee');
        }
    }
}
