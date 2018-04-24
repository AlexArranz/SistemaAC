// Write your JavaScript code.
$('#modalEditar').on('shown.bs.modal', function () {
    $('#myInput').focus()
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
                document.getElementById('Select').options[i+1] = new Option(response[i].text, response[i].value);
            }            
        }
    });
}

//Funcion que recibe los datos escritos en los input de la ventana modal
function editarUsuario(action) {
    id = $('input[name=Id]')[0].value;
    email = $('input[name=Email]')[0].value;
    phoneNumber = $('input[name=PhoneNumber')[0].value;

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
        data: { id, userName, email, phoneNumber, accessFailedCount, concurrencyStamp, emailConfirmed, lookoutEnabled, lookoutEnd, normalizedEmail, normalizedUserName, passwordHash, phoneNumberConfirmed, securityStamp, twoFactorEnabled },

        success: function (response) {
            if (response == "Save") {
                window.location.href = "Usuarios";
            } else {
                alert("No se ha podido editar los datos del usuario.");
            }
        }
    });
}