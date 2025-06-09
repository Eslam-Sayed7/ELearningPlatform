const passwordStrengthText = document.getElementById('passwordStrengthText');
const togglePasswordButton = document.getElementById('togglePassword-1');
const passwordField = document.getElementById('password-1');
const appurl = `http://localhost:5164/api/`;
passwordField.addEventListener('input', function () {
    const passwordValue = passwordField.value;
    let strength = 0;

    if (/[a-zA-Z]/.test(passwordValue)) strength += 1; // Contains letter
    if (/[0-9]/.test(passwordValue)) strength += 1; // Contains number
    if (passwordValue.length >= 8) strength += 1; // At least 8 characters

    // Update progress bar based on strength
    if (strength === 1) {
        passwordBar.style.width = '33%';
        passwordBar.className = 'progress-bar weak';
        passwordStrengthText.textContent = 'Weak password';
        passwordStrengthText.style.color = 'red';
    } else if (strength === 2) {
        passwordBar.style.width = '66%';
        passwordBar.className = 'progress-bar medium';
        passwordStrengthText.textContent = 'Medium password';
        passwordStrengthText.style.color = 'rgb(230, 230, 0)';
    } else if (strength === 3) {
        passwordBar.style.width = '100%';
        passwordBar.className = 'progress-bar strong';
        passwordStrengthText.textContent = 'Strong password';
        passwordStrengthText.style.color = 'green';
    } else {
        passwordBar.style.width = '0';
        passwordStrengthText.textContent = '';
    }
});


// password reveal

togglePasswordButton.addEventListener('click', () => {
    const type = passwordField.type === 'password' ? 'text' : 'password';
    passwordField.type = type;
    togglePasswordButton.innerHTML = type === 'password' ? '<i class="fas fa-eye"></i>' : '<i class="fas fa-eye-slash"></i>';
});


// function apiRequest(endpoint, reqtype) {
//     const token = localStorage.getItem('authToken');

//     if (!token) {
//         // Dynamically redirect to register page if token is not found
//         const currentUrl = window.location.href;
//         const registerUrl = currentUrl.replace(/\/[^\/]*$/, '/register.html');
//         window.location.href = registerUrl;
//         return;
//     }

//     return fetch(`${appurl}${endpoint}`, {
//         method: reqtype, // Send request type dynamically
//         headers: {
//             'Content-Type': 'application/json',
//             'Authorization': `Bearer ${token}` // Include token in Authorization header
//         }
//     })
//         .then(response => {
//             if (!response.ok) {
//                 throw new Error('Request failed with status ' + response.status);
//             }
//             return response.json();
//         })
//         .catch(error => {
//             console.error('Error during API request:', error);
//             throw error; // rethrow to handle it in the caller
//         });
// }

document.addEventListener('DOMContentLoaded', function () {
    document.querySelector('.register-form').addEventListener('submit', async function (event) {
        event.preventDefault();

            let Password = document.getElementById('password-1').value;
            let ConfirmPassword = document.getElementById('password-1').value;

            if( Password == ConfirmPassword ){
                const data = {
                    FirstName: document.getElementById('firstname').value,
                    LastName: document.getElementById('lastname').value,
                    Username : document.getElementById('username').value,
                    Email: document.getElementById('email').value,
                    Password: document.getElementById('password-1').value
                };
                try {
                    const response = await fetch('http://localhost:5164/api/Auth/register', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(data),
                    });

                    if (response.ok) {
                        alert('Registered successfully!');
                        
                    } else {
                        alert('Error submitting form.');
                    }
                } catch (error) {
                    console.error('Error:', error);
                }
            } else{
                alert('The passwords are not matching');
            }
        });
    });
