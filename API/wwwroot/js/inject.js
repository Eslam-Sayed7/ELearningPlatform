
function injectHeadContent() {
    fetch('combonents/head.html')
        .then(response => response.text())
        .then(data => {
            const parser = new DOMParser();
            const doc = parser.parseFromString(data, 'text/html');
            const headContent = doc.head.childNodes;
            
            headContent.forEach(node => {
                document.head.appendChild(node.cloneNode(true));
            });
        })
        .catch(error => {
            console.error('Error loading head content:', error);
        });
}

document.addEventListener('DOMContentLoaded', injectHeadContent);

function injectNavbar() {
    fetch(`combonents/navbar/navbar.html`)
        .then(response => response.text())
        .then(data => {
            const parser = new DOMParser();
            const doc = parser.parseFromString(data, 'text/html');
            const navbarContent = doc.body.innerHTML; // Grab the inner HTML of the navbar
            
            // Insert the navbar at the top of the body
            document.body.insertAdjacentHTML('afterbegin', navbarContent);
        })
        .catch(error => {
            console.error('Error loading navbar:', error);
        });
}

document.addEventListener('DOMContentLoaded', injectNavbar);
