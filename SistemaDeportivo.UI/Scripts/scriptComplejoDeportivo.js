$(document).ready(function () {

    /* Cerrar modal con el boton escape */
    $(document).keydown(function (event) {
        if (event.keyCode == 27) {
            $('#modal').modal('hide');
        }
    });

    $('#area').on('keypress', function (event) {
        // Permitir solo números
        if (event.which < 48 || event.which > 57) {
            event.preventDefault();
        }
    });

    $('#area').on('input', function () {
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


$('#btnAgregar').click(function () {
    $('#modal').modal('show');
    $('#localizacion').val("");
    $('#jefe').val("");
    $('#area').val("0");
    $('#presupuesto').val("0.00");
    $('#id_cd').val(0);
    $("#idSede").val('');
});

$('#btnCerrar').click(function () {
    $('#modal').modal('hide');
});


$('#btnGuardar').click(function () {

    var _idSede = $("#idSede").val();
        _localizacion = $('#localizacion').val(),
        _jefe = $('#jefe').val(),
        _area = $('#area').val(),
        _presupuesto = $('#presupuesto').val(),
        _IdComplejoDeportivo = $('#id_cd').val();

    //Validaciones 

    if (_idSede == 0 || _idSede == '') {
        $.notify("Ingrese una sede", "error");
        return false;
    }

    if ((_localizacion.trim()).length == 0) {
        $.notify("Ingrese una localizacion", "error");
        return false;
    }

    if ((_jefe.trim()).length == 0) {
        $.notify("Ingrese un jefe", "error");
        return false;
    }

    if (_area == 0 || _area == '') {
        $.notify("Ingrese una area", "error");
        return false;
    }

    if (_presupuesto == 0.00 || _presupuesto == '') {
        $.notify("Ingrese un presupuesto", "error");
        return false;
    }

    _presupuesto = parseFloat($('#presupuesto').val());

    var complejoDeportivo = {
        IdComplejoDeportivo: _IdComplejoDeportivo,
        IdSedeOlimpica: _idSede,
        Localizacion: _localizacion,
        JefeOrganizacion: _jefe,
        AreaTotal: _area,
        Presupuesto: _presupuesto
    };

    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(complejoDeportivo),
        url: _urlGeneral + 'ComplejoDeportivo/GuardarComplejoDeportivo',
        success: function (data) {

            if (data == "True") {

                toastr.success('Los datos se guardaron correctamente', 'Mensaje', { timeOut: 5000 });
                $('#modal').modal('hide');
                EfectoCargarDatos(function () {

                    $('#id_cd').val(0);
                    window.location.href = '/ComplejoDeportivo/Index';

                });
            }
            else if (data == "Presupuesto Excedio") {
                toastr.warning('¡Atencion! Excede el presupuesto total de la sede seleccionada', 'Mensaje', { timeOut: 5000 });
            }
            else if (data == "Complejo Excedio") {
                toastr.warning('No se puede agregar mas complejos a la sede seleccionada', 'Mensaje', { timeOut: 5000 });
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


var Editar = function (id) {

    EfectoCargarDatosEditar();
    $('#id_cd').val(id);
    ObtenerComplejo(id);
    $('#modal').modal('show');

}

function ObtenerComplejo(id) {

    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: "{ id: " + JSON.stringify(id) + "}",
        url: _urlGeneral + 'ComplejoDeportivo/ObtenerComplejoDeportivo',
        success: function (data) {
            var data = JSON.parse(data);
            if (data.length > 0) {

                $("#idSede").val(data[0].IdSedeOlimpica);
                $('#localizacion').val(data[0].Localizacion);
                $('#jefe').val(data[0].JefeOrganizacion);
                $('#area').val(data[0].AreaTotal);
                $('#presupuesto').val(data[0].Presupuesto);
            }
            else {
                toastr.warning('Ocurrio un error al momento de obtener los datos', 'Mensaje', { timeOut: 3000 }); alert("Error");
                $('#modal').modal('hide');
            }

        },
        error: function (result) {
            toastr.warning('Ocurrio un error al momento de obtener los datos', 'Mensaje', { timeOut: 3000 }); alert("Error");
            $('#modal').modal('hide');
        }
    })

}

var Eliminar = function (id) {

    $('#id_cd').val(id);
    $('#modalConfirmacion').modal('show');
}

$('#btnSalir').click(function () {
    $('#modalConfirmacion').modal('hide');
});



$('#btnConfirmar').click(function () {

    var id = $('#id_cd').val();

    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: "{ id: " + JSON.stringify(id) + "}",
        url: _urlGeneral + 'ComplejoDeportivo/EliminarComplejoDeportivo',
        success: function (data) {

            if (data == "True") {

                toastr.success('El registro se elimino correctamente', 'Mensaje', { timeOut: 5000 });
                $('#modalConfirmacion').modal('hide');
                EfectoCargarDatos(function () {

                    $('#id_cd').val(0);
                    window.location.href = '/ComplejoDeportivo/Index';

                });
            }
            else {
                toastr.warning('Ocurrio un error al momento de obtener los datos', 'Mensaje', { timeOut: 3000 }); alert("Error");
                $('#modalConfirmacion').modal('hide');
            }

        },
        error: function (result) {
            toastr.warning('Ocurrio un error al momento de obtener los datos', 'Mensaje', { timeOut: 3000 }); alert("Error");
            $('#modalConfirmacion').modal('hide');
        }
    })

});
