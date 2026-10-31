// Mensajes predeterminados de validación para la interfaz en español.
$.extend($.validator.messages, {
    required: "Este campo es obligatorio.",
    remote: "Revisa este campo.",
    email: "Introduce un correo electrónico válido.",
    url: "Introduce una dirección web válida.",
    date: "Introduce una fecha válida.",
    dateISO: "Introduce una fecha válida.",
    number: "Introduce un número válido.",
    digits: "Introduce solo dígitos.",
    equalTo: "Introduce el mismo valor de nuevo.",
    maxlength: $.validator.format("Introduce como máximo {0} caracteres."),
    minlength: $.validator.format("Introduce al menos {0} caracteres."),
    rangelength: $.validator.format("Introduce entre {0} y {1} caracteres."),
    range: $.validator.format("Introduce un valor entre {0} y {1}."),
    max: $.validator.format("Introduce un valor menor o igual a {0}."),
    min: $.validator.format("Introduce un valor mayor o igual a {0}."),
    step: $.validator.format("Introduce un múltiplo de {0}.")
});
