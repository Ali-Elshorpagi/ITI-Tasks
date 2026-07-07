# SignalRLab01 — Testing Guide

## Prerequisites

| Tool | Version |
|------|---------|
| .NET SDK | 10.0+ |
| SQL Server | LocalDB / Express / Full |
| Browser | Any modern browser (Chrome recommended) |

## Setup

### 1. Configure the connection string

Open `SignalRLab01/appsettings.json` and set `DefaultConnection` to point at your SQL Server instance. Example for LocalDB:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SignalRLab01;Trusted_Connection=True;"
}
```

### 2. Apply migrations

```bash
cd SignalRLab01
dotnet ef database update
```

### 3. Run the app

```bash
dotnet run --project SignalRLab01
```

The terminal will print the URL, typically `https://localhost:7xxx`. Open it in your browser — the app opens **Student Hub** (`students.html`) automatically at the root URL.

---

## Feature Tests

### Real-time (open two browser tabs side by side — changes in one tab appear instantly in the other)

---

### Departments

#### Add a department
1. Fill in **Name** (max 10 chars) and **Description**.
2. Click **Add Department**.
3. The new row appears in both tabs immediately via SignalR.

#### Edit a department
1. Click **Edit** on any department row.
2. The **Edit Department** modal opens pre-filled with current values.
3. Change the name or description and click **Save Changes**.
4. The row updates in both tabs instantly.
5. **Error case:** try saving with no change — the server returns a conflict error shown inside the modal.
6. **Error case:** try a name that already exists — conflict error shown in modal.

#### Delete a department
1. Click **Delete** on any department row.
2. Confirm the prompt.
3. The row disappears in both tabs instantly.
4. The deleted department also disappears from the **Add Student** and **Edit Student** dropdowns.

---

### Students

#### Add a student
1. Fill in **Name**, **Age**, and select a **Department**.
2. Click **Add Student**.
3. The new row appears at the top of the Students table in both tabs.
4. **Error case:** try adding a student with the same name in the same department — the server returns an error shown below the form.

#### Edit a student
1. Click **Edit** on any student row.
2. The **Edit Student** modal opens pre-filled (name, age, department).
3. Change any field and click **Save Changes**.
4. The row updates in both tabs instantly, highlighted in yellow for 1.5 s.
5. **Error case:** try saving a name that already exists in the same department — conflict error shown in modal.

#### Delete a student
1. Click **Delete** on any student row.
2. Confirm the prompt.
3. The row disappears in both tabs instantly.

---

### Connection resilience

1. Stop the server while the page is open — the badge turns **Disconnected**.
2. Restart the server — the badge turns **Reconnecting…** then **Connected** and the tables reload automatically with fresh data.

---

## API (Swagger)

The app exposes a Swagger UI at `/swagger` in Development mode. You can test all endpoints there directly:

| Method | URL | Purpose |
|--------|-----|---------|
| GET | `/api/departments` | List all departments |
| POST | `/api/departments` | Add department |
| PUT | `/api/departments/{id}` | Edit department |
| DELETE | `/api/departments/{id}` | Delete department |
| GET | `/api/students` | List all students |
| POST | `/api/students` | Add student |
| PUT | `/api/students/{id}` | Edit student |
| DELETE | `/api/students/{id}` | Delete student |

Any write operation made through Swagger will trigger the corresponding SignalR event, so open the root URL (`/`) in a second tab to see the real-time push on the Student Hub page.
