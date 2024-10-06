const passwordStrengthText = document.getElementById('passwordStrengthText');
const togglePasswordButton = document.getElementById('togglePassword');
const passwordField = document.getElementById('password');

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