

$(document).ready(function () {

    $('.signIn').on('click', function () {

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
                    Swal.fire(
                        'success'
                    ).then(function () {
                        window.location = "https://localhost:44324/bo-register.html";
                    });
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
