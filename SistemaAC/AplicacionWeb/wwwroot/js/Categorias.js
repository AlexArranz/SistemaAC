
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
                    document.getElementById("mensaje").innerHTML = "Seleccione un estado";
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
                            window.location.href = "Categorias";
                        }
                    });
                }
            }
        }
    }
}
