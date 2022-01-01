# AspNetCore6_MVC_EmbeddedResources
<p>Proyecto ASP.NET Core 6.0 enfocado en cargar recursos incrustados de otros proyectos (controladores y vistas cshtml).</p>
<p>El proyecto que he utilizado como proyecto principal es una "Aplicación web de ASP.NET Core (Modelo-Vista-Controlador)" el cual ya viene con un ejemplo de serie (el ejemplo que hay es el HomeController y sus vistas, pero en este repositorio no haremos uso de este ejemplo).</p>

# Objetivos de este repositorio
<p>El primer objetivo es tener un proyecto MVC (Modelo, Vista, Controlador) el cual tenga sus vistas y sus controladores separados en otros dos proyectos. Es decir, tener un proyecto para almacenar los controladores, otro proyecto para almacenar las vistas y otro proyecto que será el proyecto principal para ejecutar la aplicación.</p>
<p>El segundo objetivo es poder sobrescribir los controladores y las vistas que tengamos. De esta forma, podemos tener sobrescrita la vista de usuarios sin necesidad de modificar la vista de usuarios original.</p>
<br />

# Detalles del repositorio
<p>Una vez hayas descargado o clonado el repositorio y ejecutes la aplicación (el proyecto "Web"), las páginas de prueba son las siguientes:</p>
<ul>
	<li>https://localhost:7215/Users</li>
	<li>https://localhost:7215/Projects</li>
	<li>https://localhost:7215/Reports</li>
</ul>
<p>La página de usuarios utiliza el controlador del proyecto Web.Master.Controllers y la vista del proyecto Web.Master.Views.</p>
<p>La página de proyectos utiliza el controlador del proyecto Web (el cual está sobrescribiendo al controlador del proyecto Web.Master.Controllers) y la vista del proyecto Web (que al tener sobrescrito el controlador tenemos que crear la ruta de la vista dentro de este mismo proyecto).</p>
<p>La página de reportes utiliza el controlador del proyecto Web.Master.Controllers y la vista del proyecto Web.</p>
<br />

# ¿Cómo hacer para que los controladores puedan almacenarse en un proyecto distinto?
<p>Simplemente necesitamos:</p>
<ul>
	<li>Crear un nuevo proyecto de tipo "Biblioteca de clases" (en la misma solución o en otra).</li>
	<li>Instalar el nuget "Microsoft.AspNetCore.Mvc" a este nuevo proyecto.</li>
	<li>Crear la carpeta "Controllers" dentro del proyecto nuevo.</li>
	<li>Importar la referencia de este nuevo proyecto en nuestro proyecto principal.</li>
</ul>
<p>Realizando estos cuatro pasos, cada controlador que creemos dentro de la carpeta "Controllers" dentro de este nuevo proyecto serán procesados automáticamente por el proyecto principal. Es decir, Si yo creo el controlador "UsersController" con una función "Index" que devuelva una página, compilamos y ejecutamos la aplicación, si yo especifico la ruta "/Users" o "/Users/Index" es cierto que todavía no me devuelve ninguna página (porque todavía no se la hemos creado), pero sí que ejecutará este nuevo controlador. De echo, se puede poner un punto de parada en su función "Index" para comprobar que realmente entra.</p>
<br />

# ¿Cómo hacer para que las vistas puedan almacenarse en un proyecto distinto?
<p>Simplemente necesitamos:</p>
<ul>
	<li>Crear un nuevo proyecto de tipo "Biblioteca de clases de Razor" (en la misma solución o en otra).</li>
	<li>Instalar el nuget "Microsoft.AspNetCore.Mvc" a este nuevo proyecto.</li>
	<li>Crear la carpeta "Views" dentro del proyecto nuevo.</li>
	<li>Editar el fichero con extensión ".csproj" de este nuevo proyecto para añadirle el tag "EmbeddedResource" (este tag es para indicarle qué ficheros del proyecto serán "recursos incrustados").</li>
	<li>Importar la referencia de este nuevo proyecto en nuestro proyecto principal.</li>
	<li>Instalar el nuget "Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation" en nuestro proyecto principal.</li>
	<li>Modificar el Startup (o Program, en mi caso) del proyecto principal para añadirle la llamada a la función "AddRazorRuntimeCompilation" después de la llamada a "AddControllersWithViews" (indicándole en las opciones el Assembly de nuestro nuevo proyecto que contiene las vistas).</li>
</ul>
<p>Realizando estos siete pasos, cada vista que creemos dentro de la carpeta "Views" dentro de este nuevo proyecto serán procesadas automáticamente por el proyecto principal.</p>
<p>Siguiendo con el ejemplo del último párrafo del punto anterior, si creamos una carpeta llamada "Users" dentro de la carpeta "Views" y el fichero "Index.cshtml" dentro de la carpeta "Users", cuando ejecutemos la ruta "/Users" o "Users/Index" cargará el controlador "UsersControllers" del otro proyecto y nos retornará la vista "/Users/Index.cshtml" que tenemos en este otro proyecto, y todo esto se encarga de llevarlo a cabo el proyecto principal.</p>
<br />

# ¿Cómo sobrescribir vistas?
<p>Para sobrescribir únicamente vistas no hay ningún problema. En el ejemplo del punto anterior hemos creado la vista "/Users/Index.cshtml" dentro del proyecto de bibliotecas de clases Razor, y al ser la única vista "Index" de "Users" va a cogerla directamente de la librería, pero si nosotros creamos la misma vista "/Users/Index.cshtml" dentro de la carpeta "Views" de nuestro proyecto principal nos cogería esta vista antes que la vista que tenemos en el proyecto de bibliotecas de clases Razor. Es decir, que los recursos que se encuentran dentro del proyecto principal tienen "prioridad" sobre los recursos que cargamos mediante referencia.</p>
<br />

# ¿Cómo sobrescribir controladores?
<p>El punto anterior no aplica a los controladores, porque en el caso de tener dos controladores con el mismo nombre la aplicación producirá un error (aunque dichos controladores tengan un namespace diferente). Así que la solución que he aplicado en este caso ha sido asignar un prefijo al nuevo controlador que creemos en el proyecto principal. Por ejemplo, si quisiéramos sobrescribir el "UsersController" se puede crear el controlador "CustomUsersController" en la carpeta "Controllers" del proyecto principal. Una vez creado, también podemos hacer que herede del "UsersController" en caso de querer mantener algunas funcionalidades ya creadas (en el caso de hacer esto, no olvidar poner la palabra "virtual" a las funciones y métodos de la clase padre para así poder sobrescribirlas).</p>
<p>Pero haciendo esto solamente hemos conseguido crear otra ruta en nuestra aplicación, realmente no estamos sobrescribiendo nada (todavía). Así que para hacer que el nuevo controlador "CustomUsersController" sobrescriba al "UsersController" necesitamos sobrescribir la ruta del controlador. Ahora mismo la ruta que tenemos es "/CustomUsers", y lo que nos interesa es que la ruta sea "/Users". Para ello, podemos usar el atributo "Route" sobre la clase "CustomUsersController", especificándole de forma explícita que su nombre ahora será "Users".</p>
<p>Ahora que ya tenemos el controlador "CustomUsersController" y que le hemos modificado la ruta, tenemos un problema: Al llamar el controlador "CustomUsers" ya no cogerá la vista "/Users/Index.cshtml" sino que intentará coger la vista "/CustomUsers/Index.cshtml". Cómo mínimo, hay dos formas de solucionar este problema:</p>
<ul>
	<li>Podemos especificar la ruta completa de la vista (siguiente el ejemplo del "UserController" y su vista "Index", tendríamos que especificar la ruta de la vista "~/Views/Users/Index.cshtml").</li>
	<li>Otra solución es crear una nueva carpeta con el nombre del controlador dentro de "Views" y crear una nueva vista (en base también al ejemplo anterior, tendríamos que crear la carpeta "CustomUsers" dentro de la carpeta "Views" y el fichero "Index.cshtml" dentro de la carpeta "CustomUsers"). Con esto sería algo similar a sobrescribir también la vista del controlador, por lo cual tendría sentido que esta nueva vista la creásemos también en el proyecto principal.</li>
</ul>
<br />

# Otras ideas que quizás puedan funcionar
<p>Algo parecido se podría hacer con los proyectos API REST que usan ASP.NET Core, para tener los controladores fuera del proyecto principal y tener la opción de sobrescribirlos, para así poder cambiar su funcionalidad (por ejemplo, llamando a otros servicios o llamando al mismo servicio, pero uno que esté también sobrescrito).</p>
