$(document).ready(function () {

    /* Cerrar modal con el boton escape */
    $(document).keydown(function (event) {
        if (event.keyCode == 27) {
            $('#modal').modal('hide');
        }
    });

    $('#numero').on('keypress', function (event) {
        // Permitir solo números
        if (event.which < 48 || event.which > 57) {
            event.preventDefault();
        }
    });

    $('#numero').on('input', function () {
        // Primero, elimina cualquier cosa que no sea un dígito.
        var valorActual = this.value.replace(/[^0-9]/g, '');

        // Convierte el valor a un número para eliminar ceros a la izquierda, y después a una cadena
        // Esto también asegura que el valor no sea mayor a 5 dígitos.
        valorActual = String(parseInt(valorActual, 10) || 0);

        // Si el valor convertido supera los 5 dígitos, truncarlo.
        if (valorActual.length > 5) {
            valorActual = valorActual.substring(0, 5);
        }

        // Establece el valor procesado de vuelta al input.
        this.value = valorActual;
    });

    $('#presupuesto').on('input', function () {
        // Permitir solo números y punto decimal en la entrada.
        var valor = this.value.replace(/[^0-9.]/g, '');
        this.value = valor;
    }).on('blur', function () {
        // Aplicar formateo cuando el campo pierde el foco.
        var valor = this.value;

        // Si el campo está vacío o solo contiene un punto, establecer a '0.00'
        if (valor === '' || valor === '.') {
            this.value = '0.00';
            return;
        }

        // Convertir a número para eliminar ceros a la izquierda y luego a string para poder dividir en parte entera y decimal.
        valor = parseFloat(valor).toString();

        // Separar parte entera y decimal, añadiendo '.00' si no hay parte decimal.
        var partes = valor.split('.');
        var parteEntera = partes[0];
        var parteDecimal = partes.length > 1 ? partes[1] : '00';

        // Asegurar dos dígitos en la parte decimal
        if (parteDecimal.length === 1) {
            parteDecimal += '0';
        } else if (parteDecimal.length > 2) {
            parteDecimal = parteDecimal.substring(0, 2);
        }

        // Combinar las partes y asignar
        this.value = parteEntera + '.' + parteDecimal;
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

function EfectoCargarDatosEditar(callback) {
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
    }, 1500);
}

/* Funciones */

$('#btnAgregar').click(function () {
    $('#modal').modal('show');
    $('#nombre').val("");
    $('#ubicacion').val("");
    $('#numero').val("0");
    $('#presupuesto').val("0.00");
    $('#id_sede').val(0);
});

$('#btnCerrar').click(function () {
    $('#modal').modal('hide');
});



$('#btnGuardar').click(function () {

    var _nombre = $('#nombre').val(),
        _ubicacion = $('#ubicacion').val(),
        _numero = $('#numero').val(),
        _presupuesto = $('#presupuesto').val(),
        _IdSede = $('#id_sede').val();

    //Validaciones 

    if ((_nombre.trim()).length == 0) {
        $.notify("Ingrese el nombre de la sede", "error");
        return false;
    }

    if ((_ubicacion.trim()).length == 0) {
        $.notify("Ingrese la ubicacion de la sede", "error");
        return false;
    }

    if (_numero == 0 || _numero == '') {
        $.notify("Ingrese un numero", "error");
        return false;
    }

    if (_presupuesto == 0.00 || _presupuesto == '') {
        $.notify("Ingrese un presupuesto", "error");
        return false;
    }

    _presupuesto = parseFloat($('#presupuesto').val());

    var sede = {
        IdSedeOlimpica: _IdSede,
        nombre: _nombre,
        Presupuesto: _presupuesto,
        NumeroComplejo: _numero,
        ubicacion: _ubicacion
    };

    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(sede),
        url: _urlGeneral + 'Sede/GuardarSede',
        success: function (data) {

            if (data == "True") {

                toastr.success('Los datos se guardaron correctamente', 'Mensaje', { timeOut: 5000 });
                $('#modal').modal('hide');
                EfectoCargarDatos(function () {

                    $('#id_sede').val(0);
                    window.location.href = '/Sede/Index';

                });
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


var Editar = function (id) {

    EfectoCargarDatosEditar();
    $('#id_sede').val(id);
    ObtenerSede(id);
    $('#modal').modal('show');

}

function ObtenerSede(id) {

    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: "{ id: " + JSON.stringify(id) + "}",
        url: _urlGeneral + 'Sede/ObtenerSede',
        success: function (data) {
            var data = JSON.parse(data);
            if (data.length > 0) {

                $('#nombre').val(data[0].Nombre);
                $('#ubicacion').val(data[0].Ubicacion);
                $('#numero').val(data[0].NumeroComplejo);
                $('#presupuesto').val(data[0].Presupuesto);
            }
            else {
                toastr.warning('Ocurrio un error al momento de obtener los datos', 'Mensaje', { timeOut: 3000 });
                $('#modal').modal('hide');
            }

        },
        error: function (result) {
            toastr.warning('Ocurrio un error al momento de obtener los datos', 'Mensaje', { timeOut: 3000 });
            $('#modal').modal('hide');
        }
    })

}


var Eliminar = function (id) {

    $('#id_sede').val(id);
    $('#modalConfirmacion').modal('show');
}

$('#btnSalir').click(function () {
    $('#modalConfirmacion').modal('hide');
});



$('#btnConfirmar').click(function () {

    var id = $('#id_sede').val();

    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: "{ id: " + JSON.stringify(id) + "}",
        url: _urlGeneral + 'Sede/EliminarSede',
        success: function (data) {

            if (data == "True") {

                toastr.success('El registro se elimino correctamente', 'Mensaje', { timeOut: 5000 });
                $('#modalConfirmacion').modal('hide');
                EfectoCargarDatos(function () {

                    $('#id_sede').val(0);
                    window.location.href = '/Sede/Index';

                });
            }
            else {
                toastr.warning('Ocurrio un error al momento de eliminar el registro', 'Mensaje', { timeOut: 3000 });
                $('#modalConfirmacion').modal('hide');
            }

        },
        error: function (result) {
            toastr.warning('Ocurrio un error al momento de eliminar el registro', 'Mensaje', { timeOut: 3000 }); 
            $('#modalConfirmacion').modal('hide');
        }
    })

});

