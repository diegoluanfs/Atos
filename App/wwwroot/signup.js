

$(document).ready(function () {
    var filterEmail = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    //setPickadate();

    $('.signup').on('click', function () {
        if ($('#full-name').val() == '') {
            $('#full-name').focus();
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'You need to fill in the Full Name field!'
            });
        }
        else if ($('#document').val() == '') {
            $('#document').focus();
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'You need to fill in the document field!'
            })
        }
        //else if ($('#birth-date').val() == '') {
        //    $('#birth-date').focus();
        //    Swal.fire({
        //        icon: 'error',
        //        title: 'Oops...',
        //        text: 'You need to fill in the birth date field!'
        //    })
        //}
        else if (!filterEmail.test($('#email').val())) {
            $('#email').focus();
            if ($('#email').val() == '') {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'You need to fill in the email field!'
                });
            }
        }
        else {

            var user = getData();

            $.ajax({
                url: 'https://localhost:7280/users/create',
                type: 'POST',
                headers: { 'APIKey': localStorage.getItem('l'), 'APIVersion': '1.0' },
                contentType: 'application/json',
                async: false,
                processData: false,
                crossDomain: true,
                data: JSON.stringify(user),
                success: function (resp) {
                    var response = resp.result;
                    if (resp.data.length != 0) {
                        Swal.fire(
                            'Verify your email!',
                            'Email successfully sent',
                            'success'
                        ).then(function () {
                            window.location = "https://localhost:44324/login.html";
                        });
                    }
                },
                error: function (resp) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: resp.responseJSON.message + '!'
                    })
                }
            });

        }

        function getData() {
            var obj = {
                fullName: $('#full-name').val(),
                document: $('#document').val(),
                //birthDate: $('#birth-date').val(),
                login: $('#email').val()
            }
            console.log(obj);
            return obj;
        }
    });
});
