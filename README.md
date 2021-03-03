# Índice <!-- omit in toc -->
- [FullStackTest](#fullstacktest)
  - [Composición de la solución](#composición-de-la-solución)
  - [Correr la solución](#correr-la-solución)
    - [Dashboard WebStatus](#dashboard-webstatus)
    - [Documentación generada para API](#documentación-generada-para-api)
    - [Frontend MVC](#frontend-mvc)
  - [Consideraciones](#consideraciones)
  - [Mejoras deseables](#mejoras-deseables)

# FullStackTest
Esta solución es una pequeña prueba FullStack montada en contenedores Docker y orquestrada por Docker-Compose. 

En la figura de abajo se pueden apreciar los diferentes componentes y las conexiones. Más abajo se hablará de la composición de la solución.

Los colores de los componentes representan:
- Naranja: Proyecto ASP.NET CORE
- Amarillo: Librería de clases embebida
- Verde: Imágenes cargadas por configuración con docker
- Negro: Componentes externos

![Mapa de componentes](https://github.com/adearriba/FullStackTest/blob/main/img/ComponentMap.png?raw=true)

## Composición de la solución
Dentro la solución se encuentran 4 proyectos .NET:
|Proyecto|Descripción  |
|--|--|
|FullStack.API  |Contiene las APIs y conexión a DB utilizando ASP .NET CORE. |
|FullStack.MVC |Encargado del FrontEnd por medio de ASP .NET CORE MVC|
|FullStack.WebStatus|Dashboard para entender la salud de los servicios antes mencionados|
|FullStack.Models|Libreria para compartir modelos de datos entre FullStack.API y FullStack.MVC|

Adicionalmente hay 3 contenedores, 2 corriendo ``mssql/server:2019-latest`` y 1 ``redis``:
|Contenedor|Descripción  |
|--|--|
|sqldata-api  |Almacena todos los datos de marcas y móviles|
|sqldata-mvc  |Contiene los datos requeridos por Identity para manejo de usuarios |
|redis  |Cache para servicios terceros con restricciones de consultas |

## Correr la solución
Para correr la solución se requiere tener Docker instalado. Una vez teniendo Docker instalado, se deben ejecutar los siguientes comandos desde la carpeta donde se encuentra el archivo ``docker-compose.yml``:

Para compilar la solución completa:
``docker-compose build``

Para levantar la solución:
``docker-compose up``

Los servicios se levantarán en las siguientes direcciones y puertos:
|Proyecto|Dirección:Puerto  |
|--|--|
|FullStack.WebStatus  |http://localhost:5003/hc-ui#/healthchecks  |
|FullStack.MVC |https://localhost:4432/ |
|FullStack.API |http://localhost:5001/swagger/index.html |


### Dashboard WebStatus
En este dashboard se podrá ver la salud de los servicios corriendo. 
![Dashboard WebStatus](https://github.com/adearriba/FullStackTest/blob/main/img/WebStatus_Dashboardpng.png?raw=true)

Cada servicio expone en la ruta ``dirección:puerto/hc`` un json con datos sobre la salud de sí mismo y sus dependencias. Esto se configura en ``startup.cs`` por medio de un método de extensión ubicado en ``Extensions\HealthChecksExtensions.cs`` dentro de cada proyecto.

```C#
public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
{
    var hcBuilder = services.AddHealthChecks();

    hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
    
    hcBuilder
        .AddSqlServer(
            configuration["ConnectionString"],
            name: "FullStackDb-check",
            tags: new string[] { "fullstackdb-api" });

    return services;
}
```

###  Documentación generada para API
Se ha optado por documentar automáticamente el API utilizando Swagger por las bondades que brinda. Dirigiéndose a http://localhost:5001/swagger/index.html se puede acceder a esta documentación y probar el API.
![Swagger](https://github.com/adearriba/FullStackTest/blob/main/img/API_Generated_Documentation.png?raw=true)

En producción, no se abriría un puerto hacia este servicio para que solo sea accesible por los contenedores del grupo y no por alguien externo.

###  Frontend MVC
Para agilizar en el corto tiempo tareas importante del alcance, como lo son la autenticación y autorización, se optó por usar ASPNET CORE MVC, dada la facilidad de configurar el sistema de usuario/login/roles.

Para acceder al frontend basta con dirigirse a: https://localhost:4432/ 
![Página de inicio](https://github.com/adearriba/FullStackTest/blob/main/img/HomePage.png?raw=true)

Para iniciar sesión, se han creado cuentas con los roles necesarios (``FullStack.MVC.Extensions.DbInitializer.cs``) de manera de poder simplificar el uso del proyecto desde el momento de lanzarse:

|Rol|User|Password|
|--|--|--|
|admin|admin@fullstack.com|Admin12345_|
|operador|operator@fullstack.com|Operator12345_|


## Consideraciones
Para mantener una estructura homogénea de trabajo, desacoplar el código y tener resistencia a fallos, se tomaron ciertas decisiones de programación y estructura:

 - Seguir los principios SOLID de programación.
	 - Cada componente tiene responsabilidades simples y bien definidas
	 - Todas las dependencias utilizan interfaces y por medio de la inyección de dependencias se decide cuál implementación usar.
	 - Las interfaces son simples y segregadas.
 - Compartir estructura de proyecto similares. Por ejemplo: Todos los proyectos definen Extensiones dentro de la carpeta ``Extensions``, al igual que todos los servicios en diferentes proyectos están en la carpeta ``Services``.
 - Se utiliza la librería ``Polly`` para crear políticas de reintento con tiempos exponenciales para recuperarse en caso de que un servicio del que se dependa esté creándose todavía.
 - Se utiliza la librería ``AspNetCore.HealthChecks`` para tener un dashboard de control de servicios y dependencias.
 - Cada servicio tiene su propia BD y es Owner de sus datos. La única manera de acceder a los datos es por medio de la interfaz que el servicio ofrece.

## Mejoras deseables
Por el corto tiempo, quedaron algunos puntos pendientes deseables:

 - Utilizar MongoDB para el servicio que almacena Marcas y Móviles. Dado que estaba usando MSSQL con Identity, por practicidad y rapidez quise mantener MSSQL en ambos servicios. 
	 - Al tener BD independientes y utilizar interfaces de repositorios, cambiar uno por otro no es una tarea muy compleja, solamente habría que implementar la interfaz de repositorio y cambiar la conexión e imagen en docker-compose. 
 - Usar alguna librería para FrontEnd como Vue, Angular o React. Aunque las conozco y las he usado en proyectos pequeños, la velocidad para montar todo, además de incorporar Identity hubiera sido más lenta. Por lo tanto, decidí optimizar tiempo utilizando las bondades de ASPNET CORE MVC + Identity.
 - Separar el servicio de Identity del proyecto MVC y llevarlo a un servicio propio para poder utilizarlo en diferentes Frontends independientes de la tecnología.
