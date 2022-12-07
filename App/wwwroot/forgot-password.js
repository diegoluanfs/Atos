

$(document).ready(function () {
    var filterEmail = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    $('.forgot-password').on('click', function () {
        if (!filterEmail.test($('#email').val())) {
            $('#email').focus();
            if ($('#email').val() == '') {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'You need to fill in the email field!'
                });
            }
        }else {
            Swal.fire(
                'Good job!',
                'Email successfully sent',
                'success'
            ).then(function () {
                window.location = "https://localhost:44324/login.html";
            });
        }
    });
});
