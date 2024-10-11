// Function to toggle course selection
function toggleCourseSelection(card) {
    card.classList.toggle('selected');
}
// Function to delete selected courses
async function deleteSelectedCourses() {
    const selectedCourses = document.querySelectorAll('.course-card.selected');

    if (selectedCourses.length === 0) {
        alert("No courses selected for deletion.");
        return;
    }

    const confirmDelete = confirm("Are you sure you want to delete the selected courses?");
    if (!confirmDelete) return;

    // Show a loading spinner or disable the delete button (optional)
    deleteButton.disabled = true;
    deleteButton.textContent = "Deleting...";

    const failedDeletions = [];

    for (const course of selectedCourses) {
        const courseId = course.id; // Use a data attribute like data-course-id instead of the card id

        try {
            const response = await fetch(`${appUrl}Courses/delete/${courseId}`, {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error(`Failed to delete course with ID: ${courseId}`);
            }

            course.remove(); // Remove the course element from the DOM if successful

        } catch (error) {
            console.error('Error deleting course:', error);
            failedDeletions.push(courseId); // Track any course deletions that failed
        }
    }

    // Re-enable the delete button
    deleteButton.disabled = false;
    deleteButton.textContent = "Delete Selected";

    // Notify the user of the result
    if (failedDeletions.length === 0) {
        alert("Selected courses were successfully deleted.");
    } else {
        alert(`Some courses could not be deleted: ${failedDeletions.join(', ')}`);
    }
}

// Add an event listener to the delete button to trigger deletion
const deleteButton = document.querySelector(".delete-btn");
deleteButton.addEventListener('click', deleteSelectedCourses);


// Get the modal
   const modal = document.getElementById("courseModal");

   // Get the button that opens the modal
   const btn = document.getElementById("addCourseBtn");

   // Get the <span> element that closes the modal
   const span = document.getElementsByClassName("close-btn")[0];

   // When the user clicks the button, open the modal 
   btn.onclick = function() {
       modal.style.display = "block";
   }

   // When the user clicks on <span> (x), close the modal
   span.onclick = function() {
       modal.style.display = "none";
   }

   // When the user clicks anywhere outside of the modal, close it
   window.onclick = function(event) {
       if (event.target == modal) {
           modal.style.display = "none";
       }
   }

   // Handle form submission
   document.getElementById("courseForm").onsubmit = function(e) {
       e.preventDefault(); // Prevent page reload
       const title = document.getElementById("courseTitle").value;
       const description = document.getElementById("courseDescription").value;
       const imageUrl = document.getElementById("courseImage").value;

       // Add your logic to save the course information (e.g., API call or updating the UI)

       // Close the modal after submission
       modal.style.display = "none";
       
       // Optionally reset the form
       this.reset();
   }

   document.getElementById("courseForm").onsubmit = function(e) {
    e.preventDefault(); // Prevent page reload

    // Collecting values from the form
    const courseName = document.getElementById("courseName").value;
    const courseDescription = document.getElementById("courseDescription").value;
    const courseCategory = document.getElementById("courseCategory").value;
    const courseLevel = document.getElementById("courseLevel").value;
    const coursePrice = document.getElementById("coursePrice").value;
    const courseDuration = document.getElementById("courseDuration").value;
    const courseImage = document.getElementById("courseImage").value;
    const courseLanguage = document.getElementById("courseLanguage").value;

    // Add your logic to save the course information
    console.log({
        courseName,
        courseDescription,
        courseCategory,
        courseLevel,
        coursePrice,
        courseDuration,
        courseImage,
        courseLanguage,
    });

    // Close the modal after submission
    modal.style.display = "none";
    
    // Optionally reset the form
    this.reset();
}

  
document.addEventListener("DOMContentLoaded", function() {
});


// Handle search input
document.getElementById('search-course').addEventListener('input', function() {
    const query = this.value.toLowerCase(); // Get the search query
    const courses = document.querySelectorAll('.course-card'); // Assuming each course is in a div with class "course-card"

    // Loop through the courses and filter them
    courses.forEach(course => {
        const courseName = course.querySelector('.course-card h3').textContent.toLowerCase(); // Assuming course name is in an element with class "course-name"
        const courseId = course.getAttribute('id').toLowerCase(); // Assuming each course card has a data attribute like data-course-id

        // Check if the course name or course ID matches the search query
        if (courseName.includes(query) || courseId.includes(query)) {
            course.style.display = 'block'; // Show matching courses
        } else {
            course.style.display = 'none'; // Hide non-matching courses
        }
    });
});
