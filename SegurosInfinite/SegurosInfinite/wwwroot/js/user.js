let backend = "http://localhost:8080/SegurosBackEnd/api";
function getUserData() {
    let userData = localStorage.getItem('user');
    if (userData) {
        return JSON.parse(userData);
    }
    return null;
}
var link = document.createElement("link");
link.href = "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css";
link.rel = "stylesheet";

// Luego, añades el elemento <link> al <head> del documento.
document.head.appendChild(link);

// Función para almacenar los datos del usuario en el almacenamiento local
function setUserData(userData) {
    localStorage.setItem('user', JSON.stringify(userData));
}

// Obtener los datos del usuario al cargar la página
let userGlobal = getUserData() || {
    cedula: '',
    nombre: '',
    usuario: {
        cedula: '',
        clave: '',
        nombre: '',
        telefono: '',
        correo: '',
        datos_tarjeta: '',
        tipo: ''
    },
    polizas: []
};


const registro = document.getElementById('registrar');

if (registro !== null) {
    registro.addEventListener('click', function (event) {
        event.preventDefault();
        registrar();
        document.getElementById("id").value = '';
        document.getElementById("clave").value = '';
        document.getElementById("nombre").value = '';
        document.getElementById("telefono").value = '';
        document.getElementById("correo").value = '';
        document.getElementById("tarjeta").value = '';
        document.getElementById("checkbox-politicas").checked = false;
    });
}

async function registrar() {
    let id = document.getElementById("id").value;
    let clave = document.getElementById("clave").value;
    let nombre = document.getElementById("nombre").value;
    let telefono = document.getElementById("telefono").value;
    let correo = document.getElementById("correo").value;
    let datos_tarjeta = document.getElementById("tarjeta").value;
    let usuario = {
        cedula: id,
        clave: clave,
        nombre: nombre,
        telefono: telefono,
        correo: correo,
        datos_tarjeta: datos_tarjeta,
        tipo: 1,
        polizas: null
    };
    let cliente = {
        cedula: id,
        nombre: nombre,
        usuario: usuario
    };
    try {
        const response = await fetch(`${backend}/registrar`, {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(cliente)
        });
        if (response.ok) {
            Swal.fire({
                icon: 'success',
                title: '¡Te registraste exitosamente!',
                text: 'Ahora puedes entrar al sistema con tus credenciales',
                showConfirmButton: false,
                timer: 2000 // El alert se cerrará automáticamente después de 2 segundos
            });


        } else {
            console.error('Error');
        }

    } catch (error) {
        console.error('Error:', error);
    }
}


async function login() {
    let identificacion = document.getElementById('identificacion').value;
    let clave = document.getElementById('clave').value;
    let usuario = {
        cedula: identificacion,
        clave: clave
    };
    // Realizar la lógica de autenticación o envío de datos al servidor aquí
    try {
        const response = await fetch(`${backend}/login`, {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(usuario)
        });

        if (response.ok) {
            userGlobal = await response.json();
            if (userGlobal.cedula !== "000") {
                let paginaEscoger = '';
                switch (userGlobal.usuario.tipo) {
                    case 1:
                        paginaEscoger = '/SegurosInfinite/wwwroot/html/cliente/polizas/View.html';
                        // Redireccionar a la página correspondiente
                        window.location.href = paginaEscoger;
                        console.log('Inicio de sesión exitoso - Tipo 1');
                        break;
                    case 2:
                        paginaEscoger = '/SegurosInfinite/wwwroot/html/administrador/menu/View.html';
                        // Redireccionar a la página correspondiente
                        window.location.href = paginaEscoger;
                        console.log('Inicio de sesión exitoso - Tipo 20');
                        break;
                    default:
                }
            } else {
                const identificacionInput = document.getElementById('identificacion');
                const claveInput = document.getElementById('clave');
                const errorMessage = document.createElement('p');

                identificacionInput.classList.add('border-red-500');
                claveInput.classList.add('border-red-500');

                errorMessage.textContent = 'Identificación o clave incorrectos. ¡Inténtelo de nuevo!';
                errorMessage.classList.add('text-red-500', 'text-sm', 'mb-2');

                const form = document.querySelector('form');

                // Agregar evento de escucha de entrada para el input de identificación
                identificacionInput.addEventListener('input', clearErrorMessage);
                // Agregar evento de escucha de entrada para el input de clave
                claveInput.addEventListener('input', clearErrorMessage);

                function clearErrorMessage() {
                    const existingErrorMessage = form.querySelector('p');
                    if (existingErrorMessage) {
                        form.removeChild(existingErrorMessage);
                    }
                }

                form.insertBefore(errorMessage, form.firstChild);
                localStorage.clear();
                userGlobal = {}; // Limpiar los datos en userGlobal
            }
            // Verificar el tipo de usuario y realizar alguna acción correspondiente


        } else {
            // Autenticación fallida, mostrar algún mensaje de error
            console.error('Error en el inicio de sesión');

            // Agregar clases CSS para resaltar el error

        }
        setUserData(userGlobal);
    } catch (error) {
// Error en la solicitud o en la lógica de autenticación
        console.error('Error en la solicitud:', error);

    }
}

async function obtenerPolizas() {
    try {
        const response = await fetch(`${backend}/polizas`, {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(userGlobal)
        });

        if (response.ok) {
            userGlobal = await response.json();
            // Aquí puedes realizar las acciones necesarias con los datos de respuesta
            // como actualizar el contenido de la tabla utilizando innerHTML


            const tableBody = document.getElementById('polizasTableBody');
            let tableHtml = '';
            document.getElementById('placa').value = '';

            userGlobal.polizas.forEach(poliza => {
                tableHtml += `
                <tr>
                    
                    <td class="border border-gray-300 px-4 py-2">#${poliza.idPoliza}</td>
                    <td class="border border-gray-300 px-4 py-2">${poliza.placa}</td>
                    <td class="border border-gray-300 px-4 py-2">${poliza.fechaInicio}</td>
                    <td class="border border-gray-300 px-4 py-2">${poliza.auto}</td>
                    <td class="border border-gray-300 px-4 py-2"><img class="imagen" src="${backend}/polizas/${poliza.idPolizaModelo}/imagen" style="width: 50px; height: 50px;"></td>
                    <td class="border border-gray-300 px-4 py-2">₡${poliza.costoTotal}</td>
                    <td class="border border-gray-300 px-4 py-2">${poliza.plazoPago}</td>
                    <td class="border border-gray-300 px-4 py-2"> <a href="/SegurosInfinite/wwwroot/html/cliente/polizas/Detalles.html?idPoliza=${poliza.idPoliza}"><i class="fas fa-eye"></i></a> </td>
                </tr>
            `;
            });

            tableBody.innerHTML = tableHtml;
        } else {
            // Error en la respuesta del servidor
            console.error('Error al obtener las pólizas:', response.status);
        }
        ocultarElementosHeader();
    } catch (error) {
        // Error en la solicitud o en la lógica de obtener las pólizas
        console.error('Error al obtener las pólizas:', error);
    }
}

async function obtenerPolizasPorPlaca(event, placa) {
    event.preventDefault(); // Esta línea evita la acción por defecto
    try {
        const cedula = userGlobal.cedula;

        const response = await fetch(`${backend}/polizas/findPoliza?placa=${encodeURIComponent(placa)}&cedula=${encodeURIComponent(cedula)}`, {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(userGlobal)
        });

        if (response.ok) {
            const polizas = await response.json();

            const tableBody = document.getElementById('polizasTableBody');
            let tableHtml = '';

            polizas.forEach(poliza => {
                tableHtml += `
                <tr>
                    <td class="border border-gray-300 px-4 py-2">#${poliza.idPoliza}</td>
                    <td class="border border-gray-300 px-4 py-2">${poliza.placa}</td>
                    <td class="border border-gray-300 px-4 py-2">${poliza.fechaInicio}</td>
                    <td class="border border-gray-300 px-4 py-2">${poliza.auto}</td>
                    <td class="border border-gray-300 px-4 py-2"><img class="imagen" src="${backend}/polizas/${poliza.idPolizaModelo}/imagen" style="width: 50px; height: 50px;"></td>
                    <td class="border border-gray-300 px-4 py-2">₡${poliza.costoTotal}</td>
                    <td class="border border-gray-300 px-4 py-2">${poliza.plazoPago}</td>
                   <td class="border border-gray-300 px-4 py-2"> <a href="/SegurosInfinite/wwwroot/html/cliente/polizas/Detalles.html?idPoliza=${poliza.idPoliza}"><i class="fas fa-eye"></i></a> </td>
                </tr>
            `;
            });

            tableBody.innerHTML = tableHtml;
        } else {
            // Error en la respuesta del servidor
            console.error('Error al obtener las pólizas:', response.status);
        }
        ocultarElementosHeader();
    } catch (error) {
        // Error en la solicitud o en la lógica de obtener las pólizas
        console.error('Error al obtener las pólizas:', error);
    }
}

async function obtenerPolizasYCoberturas() {

    const urlParams = new URLSearchParams(window.location.search);
    const idPoliza = urlParams.get('idPoliza');

    try {

        const response = await fetch(`${backend}/polizas/findUnaPoliza?idPoliza=${encodeURIComponent(idPoliza)}`, {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(userGlobal)
        });

        if (response.ok) {
            const policy = await response.json();
            let policyInfo = document.getElementById('policy-info');
            policyInfo.innerHTML = `
                    <tr>
                        <td class="border px-4 py-2">${policy.cliente.nombre}</td>
                        <td class="border px-4 py-2">${policy.placa}</td>
                        <td class="border px-4 py-2">${policy.plazoPago}</td>
                        <td class="border px-4 py-2">${policy.auto}</td>
                        <td class="border border-gray-300 px-4 py-2"><img class="imagen" src="${backend}/polizas/${policy.idPolizaModelo}/imagen" style="width: 50px; height: 50px;"></td>
                        <td class="border px-4 py-2">${policy.annio}</td>
                        <td class="border px-4 py-2">₡${policy.costoTotal}</td>
                    </tr>
                `;
        } else {
            console.error('Error al obtener la poliza:', response.status);
        }

        const coverageResponse = await fetch(`${backend}/polizas/PolizaCobertura?idPoliza=${encodeURIComponent(idPoliza)}`, {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(userGlobal)
        });

        if (coverageResponse.ok) {
            const coverages = await coverageResponse.json();
            var coverageInfo = document.getElementById('coverage-info');
            coverages.forEach(function (coverage) {
                coverageInfo.innerHTML += `
                        <tr>
                            <td class="border px-4 py-2">${coverage.descripcion}</td>
                            <td class="border px-4 py-2">₡${coverage.costoMinimo}</td>
                            <td class="border px-4 py-2">${coverage.costoPorcentual}%</td>
                        </tr>
                    `;
            });
        } else {
            console.error('Error al obtener las coberturas:', coverageResponse.status);
        }
        ocultarElementosHeader();
    } catch (error) {
        // Error en la solicitud o en la lógica de obtener las pólizas y coberturas
        console.error('Error al obtener la poliza y las coberturas:', error);
    }


}

const actualizar = document.getElementById('actualizar');
if (actualizar !== null) {
    actualizar.addEventListener('click', function (event) {
        event.preventDefault();
        actualizarDatosCliente();
    });
}

// Función para actualizar los datos del cliente
async function actualizarDatosCliente() {
    let nombre = document.getElementById('nombreFld').value;
    let telefono = document.getElementById('telefono').value;
    let correo = document.getElementById('correo').value;
    let tarjeta = document.getElementById('tarjeta').value;
    let clave = document.getElementById('clave').value;
    userGlobal.nombre = nombre;
    userGlobal.usuario.nombre = nombre;
    userGlobal.usuario.clave = clave;
    userGlobal.usuario.telefono = telefono;
    userGlobal.usuario.correo = correo;
    userGlobal.usuario.datos_tarjeta = tarjeta;
    setUserData(userGlobal);
    let request = new Request(`${backend}/actualizarDatosCliente`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userGlobal)
    });
    try {
        let response = await fetch(request);
        if (!response.ok) {
            errorMessage(response.status);
            return;
        } else {
            Swal.fire({
                icon: 'success',
                title: '¡Datos actualizados correctamente!',
                text: '',
                showConfirmButton: false,
                timer: 2000 // El alert se cerrará automáticamente después de 2 segundos
            });
            obtenerDatosCliente();
        }

    } catch (error) {
        console.error('Error:', error);
    }
}

function obtenerDatosCliente() {
    try {
        document.getElementById('nombreFld').value = userGlobal.nombre;
        document.getElementById('telefono').value = userGlobal.usuario.telefono;
        document.getElementById('correo').value = userGlobal.usuario.correo;
        document.getElementById('tarjeta').value = userGlobal.usuario.datos_tarjeta;
        document.getElementById('clave').value = userGlobal.usuario.clave;
    } catch (error) {
        console.error('Error en la solicitud:', error);
    }
}
;

function ocultarElementosHeader() {
    const polizasElement = document.getElementById('polizasElement');
    const datosElement = document.getElementById('datosElement');
    const logoutElement = document.getElementById('logoutElement');
    const loginElement = document.getElementById('loginElement');

    if (!userGlobal || userGlobal.cedula === '') {
        // Si no hay usuario logueado, ocultar elementos del encabezado
        polizasElement.style.display = 'none';
        datosElement.style.display = 'none';
        logoutElement.style.display = 'none';
        loginElement.style.display = 'block';
    } else if (userGlobal.usuario.tipo === 2) {
        // Si hay un usuario admin logueado, ocultar elementos del encabezado
        polizasElement.style.display = 'none';
        loginElement.style.display = 'none';
        logoutElement.style.display = 'block';
        datosElement.style.display = 'block';
    } else if (userGlobal.usuario.tipo === 1) {
        // Si hay un usuario cliente logueado, ocultar elementos del encabezado
        polizasElement.style.display = 'block';
        datosElement.style.display = 'block';
        logoutElement.style.display = 'block';
        loginElement.style.display = 'none';
    }
}

async function logout() {
    try {


        // Enviar una solicitud al servidor para realizar el logout
        const response = await fetch(`${backend}/login/logout`, {
            method: 'DELETE'
        });

        if (response.ok) {
            const responseText = await response.text();
            // Desconexión exitosa
            console.log('Desconexión exitosa:', responseText);
            // Limpiar localStorage
            localStorage.clear();

            // Limpiar los datos en userGlobal
            userGlobal = {};

            // Ocultar elementos del encabezado
            ocultarElementosHeader();
        } else {
            throw new Error('Error al desconectar');
        }
    } catch (error) {
        // Ocurrió un error durante la desconexión
        console.error('Error al desconectar:', error);
    }
}

function esconderFormsAdminMarcas() {
    const admMarcas = document.getElementById('admMarcas');

    if (!userGlobal || userGlobal.cedula === '' || userGlobal.usuario.tipo === 1) {
        if (admMarcas) {
            // Verificar si el elemento existe antes de acceder a su propiedad 'style'
            admMarcas.style.display = 'none';
            alert("Error: Usuario no logueado")
        }
    }
}
;

function esconderFormsAdminClientes() {
    const admClientes = document.getElementById('admClientes');

    if (!userGlobal || userGlobal.cedula === '' || userGlobal.usuario.tipo === 1) {
        if (admClientes) {
            // Verificar si el elemento existe antes de acceder a su propiedad 'style'
            admClientes.style.display = 'none';
            alert("Error: Usuario no logueado");
        }
    }
}
;

function esconderFormsAdminCoberturas() {
    const admCoberturas = document.getElementById('admCoberturas');

    if (!userGlobal || userGlobal.cedula === '' || userGlobal.usuario.tipo === 1) {
        if (admCoberturas) {
            // Verificar si el elemento existe antes de acceder a su propiedad 'style'
            admCoberturas.style.display = 'none';
            alert("Error: Usuario no logueado");
        }
    }
}
;

function esconderFormsClientePolizas() {
    const cliPolizas = document.getElementById('cliPolizas');

    if (!userGlobal || userGlobal.cedula === '') {
        // Si no hay usuario logueado, ocultar elementos
        if (cliPolizas) {
            // Verificar si el elemento existe antes de acceder a su propiedad 'style'
            cliPolizas.style.display = 'none';
            alert("Error: Usuario no logueado");
        }
    }
}
;

function esconderFormsDatos() {
    const actualizacionDatos = document.getElementById('actualizacionDatos');

    if (!userGlobal || userGlobal.cedula === '') {
        if (actualizacionDatos) {
            // Verificar si el elemento existe antes de acceder a su propiedad 'style'
            actualizacionDatos.style.display = 'none';
            alert("Error: Usuario no logueado");
        }
    }
}
;

function esconderMenu() {
    const menu = document.getElementById('menu');

    if (!userGlobal || userGlobal.cedula === '' || userGlobal.usuario.tipo === 1) {
        // Si no hay usuario logueado, ocultar elementos
        if (menu) {
            // Verificar si el elemento existe antes de acceder a su propiedad 'style'
            menu.style.display = 'none';
            alert("Error: Usuario no logueado");
        }
    }
}
;

document.addEventListener('DOMContentLoaded', function () {
    // Obtener la URL actual
    var url = window.location.href;

    // Verificar si la URL coincide con la página específica
    if ( url.includes("/SegurosInfinite/wwwroot/html/cliente/datos/View.html")) {
        obtenerDatosCliente();
    }
});


document.addEventListener('DOMContentLoaded', function () {
    // Obtener la URL actual
    var url = window.location.href;

    // Verificar si la URL coincide con la página específica
    if ( url.includes("/SegurosInfinite/wwwroot/html/cliente/polizas/View.html")) {
        obtenerPolizas();
    }
});

document.addEventListener('DOMContentLoaded', obtenerPolizasYCoberturas);
document.addEventListener('DOMContentLoaded', esconderFormsAdminMarcas);
document.addEventListener('DOMContentLoaded', esconderFormsAdminClientes);
document.addEventListener('DOMContentLoaded', esconderFormsAdminCoberturas);
document.addEventListener('DOMContentLoaded', esconderFormsClientePolizas);
document.addEventListener('DOMContentLoaded', esconderFormsDatos);
document.addEventListener('DOMContentLoaded', esconderMenu);
document.addEventListener('DOMContentLoaded', ocultarElementosHeader);

const form = document.getElementById('findPolizasForm'); // Asegúrate de reemplazar 'form-id' por el ID de tu formulario
if (form) {
    form.addEventListener('submit', (event) => obtenerPolizasPorPlaca(event, form.placa.value));
} else {
    // Verifica si estás en la página correcta antes de mostrar el mensaje de error
    if (window.location.pathname === '/ruta-de-la-pagina-correcta') {
        console.error('El formulario no se encontró en el documento HTML');
    }
}


