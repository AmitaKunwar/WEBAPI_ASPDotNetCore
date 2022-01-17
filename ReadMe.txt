Document Link =>
	https://docs.microsoft.com/en-us/aspnet/core/fundamentals/startup?view=aspnetcore-5.0
	https://www.tutorialspoint.com/what-is-the-addsingleton-vs-addscoped-vs-add-transient-chash-asp-net-core
	https://code-maze.com/getting-started-with-efcore/
	https://code-maze.com/net-core-web-development-part4/
	https://gavilan.blog/2019/03/28/asp-net-core-2-2-creating-resources-with-http-post/
	


App startup in ASP.NET Core - The Startup class configures services and the app's request pipeline. ASP.NET Core use startup class to include ConfigureServices method to configure app's services. A service is a resuable component that provide app functionality. Services are registered in ConfigureServices and consumed across the app via Dependency Injection. 
It includes a configure method to create the app's request processing pipeline.

					public class Startup
					{
						public Startup(IConfiguration configuration)
						{
							Configuration = configuration;
						}

						public IConfiguration Configuration { get; }

						public void ConfigureServices(IServiceCollection services)
						{

							services.AddDbContext<EmployeeContext>(options =>
								options.UseSqlServer(Configuration.GetConnectionString("Connectionstring")));
							services.AddTransient<IEmployeeRepository, EmployeeRepository>();
							services.AddControllers();

							services.AddRazorPages();
						}
						
There are three ways by which dependencies can be registered in Startup.cs. i.e. AddSingleton, AddScoped and AddTransient.
						
Add Singleton
When we register a type as singleton, only one instance is available throughout the application and for every request.

It is similar to having a static object.

The instance is created for the first request and the same is available throughout the application and for each subsequent requests.

public void ConfigureServices(IServiceCollection services){
   services.AddSingleton<ILog,Logger>()
}
Add Scoped
When we register a type as Scoped, one instance is available throughout the application per request. When a new request comes in, the new instance is created. Add scoped specifies that a single object is available per request.

public void ConfigureServices(IServiceCollection services){
   services.AddScoped<ILog,Logger>()
}
Add Transient
When we register a type as Transient, every time a new instance is created. Transient creates new instance for every service/ controller as well as for every request and every user.

public void ConfigureServices(IServiceCollection services){
   services.AddTransient<ILog,Logger>()
}


What is a Repository pattern and why should we use it?

With the Repository pattern, we create an abstraction layer between the data access and the business logic layer of an application. By using it, we are promoting a more loosely coupled approach to access our data from the database. Also, the code is cleaner and easier to maintain and reuse. Data access logic is in a separate class, or sets of classes called a repository, with the responsibility of persisting the application’s business model.

Entity Framework is an open source object–relational mapping framework for ADO.NET. It was originally shipped as an integral part of .NET Framework. Starting with Entity Framework version 6, it has been delivered separately from the .NET Framework.Entity Framework Core is a modern object-database mapper for .NET. It supports LINQ queries, change tracking, updates, and schema migrations. EF Core works with many databases, including SQL Database (on-premises and Azure), SQLite, MySQL, PostgreSQL, and Azure Cosmos DB.

The this keyword refers to the current instance of the class and is also used as a modifier of the first parameter of an extension method.

IQueryable is suitable for querying data from out-memory (like remote database, service) collections. While querying data from a database, IQueryable executes a "select query" on server-side with all filters. IQueryable is beneficial for LINQ to SQL queries
