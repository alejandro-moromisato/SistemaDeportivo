
function EfectoCargarDatos(callback) {
    $.preloader.start({
        modal: true,
        src: 'sprites2.png'
    });

    setTimeout(function () {
        $.preloader.stop();

        // Llamamos a la devolución de llamada al final para indicar que la carga de datos ha terminado
        if (typeof callback === 'function') {
            callback();
        }
    }, 3000);
}


$('#btnGuardar').click(function () {

    var _nombreUsuario = $('#inputUsuario').val(),
        _inputPassword = $('#inputPassword').val(),
        inputPasswordConfirm = $('#inputPasswordConfirm').val();

    //Validaciones 

    if ((_nombreUsuario.trim()).length == 0) {
        $.notify("Ingrese su nombre de usuario", "error");
        return false;
    }

    if ((_inputPassword.trim()).length == 0) {
        $.notify("Ingrese su Clave", "error");
        return false;
    }

    if ((inputPasswordConfirm.trim()).length == 0) {
        $.notify("Ingrese su Clave", "error");
        return false;
    }

    if (_inputPassword.trim() != inputPasswordConfirm.trim()) {
        $.notify("Las claves ingresadas son diferentes", "error");
        return false;
    }



    var usuario = {
        nombreUsuario: _nombreUsuario,
        clave: _inputPassword
    };

    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(usuario),
        url: _urlGeneral + 'Acceso/GuardarUsuario',
        success: function (data) {

            if (data == "True") {

                toastr.success('Los datos se guardaron correctamente', 'Mensaje', { timeOut: 5000 });
                EfectoCargarDatos(function () {

                    window.location.href = '/Acceso/Index';

                });
            }
            else if (data = "Usuario Existente") {
                toastr.warning('El nombre del usuario ya se encuentra registrado', 'Mensaje', { timeOut: 5000 });
            }
            else {
                toastr.warning('Ocurrio un error al momento de enviar los datos', 'Mensaje', { timeOut: 5000 });
            }

        },
        error: function (result) {
            toastr.warning('Ocurrio un error al momento de enviar los datos', 'Mensaje', { timeOut: 5000 });
        }
    });


});