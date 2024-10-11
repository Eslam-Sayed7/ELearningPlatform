// Elements
const appUrl = `http://localhost:5164/api/`; // Base URL for your API
const coursesContainer = document.querySelector(".courses-section"); // Select the container
const prevButton = document.getElementById('prevButton'); // Select the Previous button
const nextButton = document.getElementById('nextButton'); // Select the Next button

let currentPage = 0; // Initialize current page
const coursesPerPage = 9; // Number of courses to display per page
let allCourses = []; // Array to hold all fetched courses

// Function to fetch courses
async function fetchCourses() {
    try {
        const response = await fetch(`${appUrl}Course/Popular`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json' // Adjust this based on your API's requirements
            }
        });

        // Check if the response is ok
        if (!response.ok) {
            throw new Error('Network response was not ok: ' + response.statusText);
        }

        const courses = await response.json(); // Parse the JSON response
        return courses;
    } catch (error) {
        console.error('Error fetching courses:', error);
        return []; // Return an empty array on error
    }
}

// Create and append course elements
function createCourseElements(courses) {
    // Clear existing courses
    coursesContainer.innerHTML = ''; 

    // Check if courses array is empty
    if (courses.length === 0) {
        const message = document.createElement('p');
        message.textContent = 'No courses available.';
        coursesContainer.appendChild(message);
        return;
    }

    // Create and append course elements
    courses.forEach(course => {
        const courseCard = document.createElement('div');
        courseCard.className = 'course-card'; // Use the course-card class
        
        courseCard.id = `${course.courseId}`; // Assigning a unique ID based on courseId

        courseCard.addEventListener('click', () => toggleCourseSelection(courseCard));
        const thumbnail = document.createElement('img');
        thumbnail.src = course.thumbnailUrl;
        thumbnail.alt = `${course.courseName} thumbnail`;

        const courseName = document.createElement('h3'); // Updated to h3
        courseName.textContent = course.courseName;

        const description = document.createElement('p');
        description.textContent = course.category;

        const price = document.createElement('p');
        price.innerHTML = `<strong>Price:</strong> $${course.price.toFixed(2)}`;

        courseCard.appendChild(thumbnail);
        courseCard.appendChild(courseName);
        courseCard.appendChild(description);
        courseCard.appendChild(price);

        coursesContainer.appendChild(courseCard);
    });
}

// Function to render the current page of courses
function renderPage(page) {
    const start = page * coursesPerPage; // Calculate starting index
    const end = start + coursesPerPage; // Calculate ending index
    const coursesToDisplay = allCourses.slice(start, end); // Get the courses for the current page
    createCourseElements(coursesToDisplay); // Render the courses
}

// Toggle course selection
function toggleCourseSelection(card) {
    card.classList.toggle('selected');
}

// Function to handle pagination
function setupPagination() {
    prevButton.addEventListener('click', () => {
        if (currentPage > 0) {
            currentPage--;
            renderPage(currentPage);
            updatePaginationButtons();
        }
    });

    nextButton.addEventListener('click', () => {
        if (currentPage < Math.ceil(allCourses.length / coursesPerPage) - 1) {
            currentPage++;
            renderPage(currentPage);
            updatePaginationButtons();
        }
    });

    updatePaginationButtons(); // Initialize button states
}

// Update pagination buttons
function updatePaginationButtons() {
    prevButton.disabled = currentPage === 0; // Disable on the first page
    nextButton.disabled = currentPage >= Math.ceil(allCourses.length / coursesPerPage) - 1; // Disable on the last page
}

// Fetch and display courses when the page loads
window.addEventListener('load', async () => {
    allCourses = await fetchCourses(); // Await the fetchCourses function
    loadCoursesCounts(allCourses.length); // Initialize counts based on fetched data
    renderPage(currentPage); 
    setupPagination(); 
});

// Function to count up numbers in the stats section
function countUp(element, targetNumber, duration) {
    let startNumber = 0;
    let increment = targetNumber / (duration / 16);  // roughly 16ms per frame (60fps)
    let currentNumber = startNumber;

    function updateCounter() {
        currentNumber += increment;
        if (currentNumber >= targetNumber) {
            currentNumber = targetNumber;  // Ensure it doesn't go beyond the target
        }

        element.innerHTML = Math.floor(currentNumber);  // Display the number
        if (currentNumber < targetNumber) {
            requestAnimationFrame(updateCounter);  // Continue the animation
        }
    }

    requestAnimationFrame(updateCounter);  // Start the animation
}

// Function to load and count course statistics
function loadCoursesCounts(totalCourses) {
    const totalStudentsElement = document.getElementById('total-students');
    const activeCoursesElement = document.getElementById('active-courses');
    const newStudentsElement = document.getElementById('new-students');
    const completedCoursesElement = document.getElementById('completed-courses');

    const activeCoursesCount = totalCourses; // This can be dynamic based on course status
    const newStudentsCount = totalCourses * 10; // Example calculation for demo purposes
    const completedCoursesCount = totalCourses / 2; // Example: assuming half of courses are completed

    countUp(totalStudentsElement, newStudentsCount * 2, 2000);  // Just a placeholder, could be dynamic
    countUp(activeCoursesElement, activeCoursesCount, 2000);
    countUp(newStudentsElement, newStudentsCount, 2000);
    countUp(completedCoursesElement, completedCoursesCount, 2000);
}


const courseForm = document.getElementById('courseForm');

courseForm.addEventListener('submit', async (e) => {
    e.preventDefault(); // Prevent the default form submission

    // Get form data
    const formData = new FormData(courseForm);

    // Extract the values from the form
    const courseData = {
        courseName: formData.get('courseName'),
        description: formData.get('courseDescription'),
        categoryId: formData.get('courseCategory'), // Ensure this is a valid Guid in the backend
        level: formData.get('courseLevel'),
        price: parseFloat(formData.get('coursePrice')),
        duration: parseInt(formData.get('courseDuration'), 10),
        language: formData.get('courseLanguage'),
        thumbnailUrl: '', // This will be set after image upload
        sections: [] // Add logic to handle sections if needed
    };

    // Handle image upload
    const courseImage = formData.get('courseImage');
    if (courseImage) {
        try {
            const uploadResponse = await uploadImage(courseImage);
            courseData.thumbnailUrl = uploadResponse.url; // Assuming the response contains the URL
        } catch (error) {
            console.error('Error uploading image:', error);
            return;
        }
    }

    // Submit course data to the API
    try {
        const response = await fetch(`${appUrl}Course/AddCourse`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(courseData),
        });

        if (response.ok) {
            alert('Course added successfully');
            courseForm.reset(); // Reset the form
        } else {
            const errorData = await response.json();
            console.error('Error adding course:', errorData);
        }
    } catch (error) {
        console.error('Error submitting course:', error);
    }
});

// Helper function to upload the image
async function uploadImage(file) {
    const formData = new FormData();
    formData.append('file', file);

    const response = await fetch(`${appUrl}Course/Upload`, {
        method: 'POST',
        body: formData,
    });

    if (!response.ok) {
        throw new Error('Image upload failed');
    }

    return await response.json(); // Assuming the API returns a JSON response with the uploaded file's URL
}
