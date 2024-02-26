
$('#btnLogin').click(function () {

    var _id = $('#idUsuario').val(),
        _idClave = $('#idClave').val();

    //Validaciones 

    if ((_id.trim()).length == 0 && (_idClave.trim()).length == 0) {
        $.notify("Debe ingresar su usuario y clave", "error");
        return false;
    }

    if ((_id.trim()).length == 0) {
        $.notify("Debe ingresar su Usuario", "error");
        return false;
    }

    if ((_idClave.trim()).length == 0) {
        $.notify("Debe ingresar su clave", "error");
        return false;
    }

    var usuario = {
        nombreUsuario: _id,
        clave: _idClave
    };

    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(usuario),
        url: _urlGeneral + 'Acceso/ValidarAccesoUsuario',
        success: function (data) {
            if (data == "true") {
                toastr.success('Acceso correcto', 'Mensaje', { timeOut: 5000 });
                EfectoCargarDatos(function () {
                    window.location.href = '/Sede/Index';
                });
            }
            else if(data = "invalido"){
                toastr.warning('Usuario/clave invalido', 'Mensaje', { timeOut: 5000 });
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