# AspNetCore6_MVC_EmbeddedResources
Proyecto ASP.NET Core 6.0 enfocado en cargar recursos incrustados de otros proyectos (controladores y vistas cshtml).

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
<p></p>
