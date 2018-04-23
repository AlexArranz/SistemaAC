// Write your JavaScript code.
$('#modalEditar').on('shown.bs.modal', function () {
  $('#myInput').focus()
})

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
function mostrarUsuario(response) {
    items = response;
    $.each(items, function (index, val) {
        $('input[name=Id]').val(val.id);
        $('input[name=UserName]').val(val.userName);
        $('input[name=Email]').val(val.email);
        $('input[name="PhoneNumber]').val(val.phoneNumber);
    })
}