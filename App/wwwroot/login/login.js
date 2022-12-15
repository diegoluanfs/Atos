

$(document).ready(function () {

    $('.signIn').on('click', function () {
        if ($('#login').val() == '') {
            $('#login').focus();
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'You need to fill in the Login field!'
            });
        }
        else if ($('#password').val() == '') {
            $('#password').focus();
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'You need to fill in the Password field!'
            });
        }
        else {
            var login = getData();

            console.log(login);

            $.ajax({
                url: 'https://localhost:7280/auth/signin',
                type: 'POST',
                headers: { 'APIKey': localStorage.getItem('l'), 'APIVersion': '1.0' },
                contentType: 'application/json',
                async: false,
                processData: false,
                crossDomain: true,
                data: JSON.stringify(login),
                success: function (resp) {
                    var response = resp.result;
                    if (resp.data.length != 0) {
                        window.location = "https://localhost:44324/bo-register/bo-register.html";
                    }
                },
                error: function (ex) {
                    console.log(ex);
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!',
                        footer: '<a href="">' + ex + '</a>'
                    })
                }
            });
        }
    });
    function getData() {
        var obj = {
            password: $('#password').val(),
            login: $('#login').val()
        }
        console.log(obj);
        return obj;
    }
});
