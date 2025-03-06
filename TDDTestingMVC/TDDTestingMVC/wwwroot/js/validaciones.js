$(document).ready(function () {
    var today = new Date().toISOString().split("T")[0];
    $("#FechaNacimiento").attr("max", today);

    $("form").validate({
        rules: {
            Cedula: {
                required: true,
                digits: true,
                minlength: 10,
                maxlength: 10
            },
            Apellidos: {
                required: true,
                maxlength: 50,
                pattern: "^[A-Za-zÁÉÍÓÚáéíóúñÑ ]+$"
            },
            Nombres: {
                required: true,
                maxlength: 50,
                pattern: "^[A-Za-zÁÉÍÓÚáéíóúñÑ ]+$"
            },
            FechaNacimiento: {
                required: true,
                date: true,
                max: today 
            },
            Mail: {
                required: true,
                email: true
            },
            Telefono: {
                required: true,
                digits: true,
                minlength: 10,
                maxlength: 10
            },
            Direccion: {
                required: true
            },
            Estado: {
                required: true
            }
        },
        messages: {
            Cedula: {
                required: "La cédula es obligatoria",
                digits: "Solo se permiten números",
                minlength: "Debe tener 10 dígitos",
                maxlength: "Debe tener 10 dígitos"
            },
            Apellidos: {
                required: "El apellido es obligatorio",
                maxlength: "Máximo 50 caracteres",
                pattern: "Solo se permiten letras y espacios"
            },
            Nombres: {
                required: "El nombre es obligatorio",
                maxlength: "Máximo 50 caracteres",
                pattern: "Solo se permiten letras y espacios"
            },
            FechaNacimiento: {
                required: "La fecha de nacimiento es obligatoria",
                date: "Formato de fecha inválido",
                max: "No puede seleccionar una fecha futura"
            },
            Mail: {
                required: "El correo es obligatorio",
                email: "Debe ser un correo válido"
            },
            Telefono: {
                required: "El teléfono es obligatorio",
                digits: "Solo se permiten números",
                minlength: "Debe tener 10 dígitos",
                maxlength: "Debe tener 10 dígitos"
            },
            Direccion: {
                required: "La dirección es obligatoria"
            },
            Estado: {
                required: "Debe seleccionar un estado"
            }
        },
        errorClass: "text-danger",
        errorElement: "div"
    });
});
