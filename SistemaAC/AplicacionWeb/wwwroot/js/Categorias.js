
class Categorias {
    constructor(nombre, descripcion, estado, action) {
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.estado = estado;
        this.action = action;
    }

    agregarCategoria() {
        if (this.nombre == "") {
            document.getElementById("Nombre").focus();
        } else {
            if (this.descripcion == "") {
                document.getElementById("Descripcion").focus();
            } else {
                if (this.estado == "0") {
                    document.getElementById("Mensaje").innerHTML = "Seleccione un estado";
                } else {
                    var nombre = this.nombre;
                    var descripcion = this.descripcion;
                    var estado = this.estado;
                    var action = this.action;
                    var mensaje = "";
                    $.ajax({
                        type: "POST",
                        url: action,
                        data: { nombre, descripcion, estado },
                        success: (response) => {
                            $.each(response, (index, val) => {
                                mensaje = val.code;
                            });
                            if (mensaje === "Save") {
                                this.restablecer();
                            } else {
                                document.getElementById("Mensaje").innerHTML = "No se puede guardar la categoría.";
                            }
                        }
                    });
                }
            }
        }
    }

    filtrarDatos(numPagina) {
        var valor = this.nombre;
        var action = this.action;
        if (valor == "") {
            valor = "null";
        }
        $.ajax({
            type: "POST",
            url: action,
            data: { valor, numPagina },
            success: (response) => {
                $.each(response, (index, val) => {
                    $("#resultSearch").html(val[0]);
                    $("#paginado").html(val[1]);
                });
            }
        });
    }

    restablecer() {
        document.getElementById("Nombre").value = "";
        document.getElementById("Descripcion").value = "";
        document.getElementById("Mensaje").innerHTML = "";
        document.getElementById("Estado").selectedIndex = 0;
        $('#modalAgregarCategoria').modal('hide');
    }
}
