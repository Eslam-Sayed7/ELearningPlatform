document.getElementById('paymentForm').addEventListener('submit', function(event) {
    event.preventDefault();
    
    const studentId = document.getElementById('studentId').value;
    const courseId = document.getElementById('courseId').value;
    const amount = document.getElementById('amount').value;
    const discount = document.getElementById('discount').value;

    if (!studentId || !courseId || !amount) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please fill in all required fields!'
        });
        return;
    }

    const paymentData = {
        studentId,
        courseId,
        amount: parseFloat(amount),
        discount: parseFloat(discount)
    };

    Swal.fire({
        icon: 'success',
        title: 'Payment Processed!',
        text: 'Your payment has been successfully submitted.',
        showConfirmButton: true
    });

    console.log('Payment Data:', paymentData);
});