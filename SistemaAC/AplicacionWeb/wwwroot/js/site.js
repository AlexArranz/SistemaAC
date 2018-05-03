// Write your JavaScript code.
$('#modalEditar').on('shown.bs.modal', function () {
    $('#myInput').focus()
})
$('#modalAgregarCategoria').on('shown.bs.modal', function () {
    $('#Nombre').focus()
})

$(function () {
    getRoles($('#urlroles').val());
});

function getUsuario(id, action) {
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            mostrarUsuario(response);
        }
    })
}

var items;
//Variables globales por cada propiedad de usuario
var id;
var userName;
var email;
var phoneNumber;
var role;
var selectRole;

//Otras variables donde almacenaremos los datos del registro, pero estos datos no serán modificados
var accessFailedCount;
var concurrencyStamp;
var emailConfirmed;
var lookoutEnabled;
var lookoutEnd;
var normalizedUserName;
var normalizedEmail;
var passwordHash;
var phoneNumberConfirmed;
var securityStamp;
var twoFactorEnabled;


function mostrarUsuario(response) {
    items = response;

    $.each(items, function (index, val) {
        $('input[name=Id]').val(val.id);
        $('input[name=UserName]').val(val.userName);
        $('input[name=Email]').val(val.email);
        $('input[name=PhoneNumber]').val(val.phoneNumber);
        $('select[name=Select]').val(val.roleID);

        //Mostrar los detalles del usuario
        $("#dUserName").text(val.userName);
        $("#dEmail").text(val.email);
        $("#dRole").text(val.role);
        $("#dPhoneNumber").text(val.phoneNumber);

        //Mostrar los datos del usuario que deseo eliminar
        $("#eUsuario").text(val.email);
        $('input[name=eIdUsuario]').val(val.id);
    });
}


function getRoles(action) {
    $.ajax({
        type: "POST",
        url: action,
        data: {},
        success: function (response) {
            document.getElementById('Select').options[0] = new Option('No Role', null);
            for (var i = 0; i < response.length; i++) {
                document.getElementById('Select').options[i + 1] = new Option(response[i].text, response[i].value);
                document.getElementById('SelectNuevo').options[i + 1] = new Option(response[i].text, response[i].value);
            }
        }
    });
}

//Funcion que recibe los datos escritos en los input de la ventana modal
function editarUsuario(action) {
    id = $('input[name=Id]')[0].value;
    email = $('input[name=Email]')[0].value;
    phoneNumber = $('input[name=PhoneNumber')[0].value;
    role = document.getElementById('Select');
    selectRole = role.options[role.selectedIndex].text;

    $.each(items, function (index, val) {
        accessFailedCount = val.accessFailedCount;
        concurrencyStamp = val.concurrencyStamp;
        emailConfirmed = val.emailConfirmed;
        lookoutEnabled = val.lookoutEnabled;
        lookoutEnd = val.lookoutEnd;
        userName = val.userName;
        normalizedUserName = val.normalizedUserName;
        normalizedEmail = val.normalizedEmail;
        passwordHash = val.passwordHash;
        phoneNumberConfirmed = val.phoneNumberConfirmed;
        securityStamp = val.securityStamp;
        twoFactorEnabled = val.twoFactorEnabled;
    });

    $.ajax({
        type: "POST",
        url: action,
        data: { id, userName, email, phoneNumber, accessFailedCount, concurrencyStamp, emailConfirmed, lookoutEnabled, lookoutEnd, normalizedEmail, normalizedUserName, passwordHash, phoneNumberConfirmed, securityStamp, twoFactorEnabled, selectRole },

        success: function (response) {
            if (response == "Save") {
                window.location.href = "Usuarios";
            } else {
                alert("No se ha podido editar los datos del usuario.");
            }
        }
    });
}

function ocultarDetalleUsuario() {
    $("#modalDetalle").modal("hide");
}

function eliminarUsuario(action) {
    var id = $('input[name=eIdUsuario]')[0].value;

    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            if (response === "Delete") {
                window.location.href = "Usuarios";
            }
            else {
                alert("No se puede eliminar el usuario");
            }
        }
    });
}

function crearUsuario(action) {
    //Obtener los datos ingresados en los inputs del formulario de la ventana modal
    email = $('input[name=EmailNuevo]')[0].value;
    phoneNumber = $('input[name=PhoneNumberNuevo]')[0].value;
    passwordHash = $('input[name=PasswordHashNuevo')[0].value;
    role = document.getElementById('SelectNuevo');
    selectRole = role.options[role.selectedIndex].text;

    //Validacion de datos del usuario para que no esten vacios
    if (email == "") {
        $('#EmailNuevo').focus();
        alert("El email es obligatorio.")
    } else {
        if (passwordHash == "") {
            $('#PasswordHashNuevo').focus();
            alert("Debe ingresar una contraseña.")
        } else {
            $.ajax({
                type: "POST",
                url: action,
                data: {
                    email, phoneNumber, passwordHash, selectRole
                },
                success: function (response) {
                    if (response === "Save") {
                        window.location.href = "Usuarios";
                    }
                    else {
                        $('#mensajeNuevo').html("No se ha podido agregar el usuario. <br/> Seleccione un rol. <br/> Ingrese un email correcto. <br/> El password debe tener de 6 a 100 caracteres, al menos un caracter especial, una letra mayúscula y un número.");
                    }
                }
            });
        }
    }

}