const apiUrl = 'http://localhost:5164/api'; // Replace with your actual API URL

// Function to fetch and display users
async function loadUsers() {
    try {
        const response = await fetch(`${apiUrl}/Users`);
        const users = await response.json();
        const userTableBody = document.getElementById('userTableBody');
        userTableBody.innerHTML = ''; // Clear existing users

        users.forEach((user, index) => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${index + 1}</td>
                <td><img src="${user.profilePicture || 'default.jpg'}" class="user-image" alt="${user.firstName}"></td>
                <td>${user.firstName} ${user.lastName}</td>
                <td>${user.email}</td>
                <td>${new Date(user.createdAt).toLocaleDateString()}</td>
                <td>${user.role}</td>
                <td class="action-buttons">
                    <button class="delete-btn" onclick="deleteUser('${user.id}')">Delete</button>
                </td>
            `;
            userTableBody.appendChild(row);
        });
    } catch (error) {
        console.error('Error loading users:', error);
    }
}
// Function to delete a user
async function deleteUser(userId) {
    try {
        const response = await fetch(`${apiUrl}/Users/${userId}`, {
            method: 'DELETE',
        });

        if (response.ok) {
            loadUsers(); // Reload users after deletion
        } else {
            console.error('Error deleting user:', response.statusText);
        }
    } catch (error) {
        console.error('Error deleting user:', error);
    }
}

// Search function to filter displayed users
function searchUsers() {
    const input = document.getElementById('searchInput').value.toLowerCase();
    const userTableBody = document.getElementById('userTableBody');
    const rows = userTableBody.getElementsByTagName('tr');

    for (let i = 0; i < rows.length; i++) {
        const row = rows[i];
        const cells = row.getElementsByTagName('td');
        const nameCell = cells[2]; // Assuming name is in the third column
        const emailCell = cells[3]; // Assuming email is in the fourth column

        if (nameCell || emailCell) {
            const name = nameCell.textContent.toLowerCase();
            const email = emailCell.textContent.toLowerCase();
            if (name.includes(input) || email.includes(input)) {
                row.style.display = ''; // Show row
            } else {
                row.style.display = 'none'; // Hide row
            }
        }
    }
}


loadUsers();
