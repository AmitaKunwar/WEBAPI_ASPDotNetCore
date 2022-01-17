For Signup and Login, Identity core is used. Use IdentityDbContext (IdentityDbContext implements DbContext.his is the Entity Framework DbContext that is used by the ASP.NET Identity libraries to manage user records. )

1. Implement AuthenticationContext : IdentityDbContext

	Add from nuget package manager => dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 5 (The dotnet core version I am using is also 5)
	
2. Add context class in the startup.cs
	services.AddDbContext<AuthenticationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
	Add nuget package Microsoft.EntityFrameworkCore.SqlServer
	
3. The context class (derived from DbContext ) must include the DbSet type properties for the entities which map to database tables and views.

4. Configure Identity core in the startup.cs
	 services.AddDefaultIdentity<AuthenticationUser>()
              .AddEntityFrameworkStores<AuthenticationContext>();
			  
	Add nuget package "Microsoft.AspNetCore.Identity.UI"
	
5. Add migration using nuger package manager console . Migration is a way to keep the database schema in sync with the EF Core model by preserving data. EF Core migrations are a set of commands which you can execute in NuGet Package Manager Console or in dotnet Command Line Interface (CLI). 

To do migration, the package "Microsoft.EntityFrameworkCore.Design" is required for the Entity Framework Core Tools to work . Install Microsoft.EntityFrameworkCore.Design. 

	Type as below-

	Add-Migration "InitialCreate"
	Update-Database
	
	
6. Add Controller => Inside this controller , we need web api method for user registration. In order to work with user registration or authentication, we have to use two classes from Identity Core. Which are UserManager and SignIn manager. First of all, define the properties of this two class in the controller and  we have to inject two properties in the controller constructor. Because of this dependency injection in the constructor both of these parameter have instance.

		 [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserController(UserManager<ApplicationUser> userMgr , SignInManager<ApplicationUser> signinMgr)
        {
            _userManager = userMgr;
            _signInManager = signinMgr;
        }

    }
	
	
	Dependency Injection takes place as a result of services added in the startup.cs 
	 services.AddDefaultIdentity<ApplicationUser>()
              .AddEntityFrameworkStores<AuthenticationContext>();
			 
Integrating JWT in the Web API Project =>

let’s start by installing the Microsoft.AspNetCore.Authentication.JwtBearer library in the main project
Add configuration parameter in appsettings.json.
Then, let’s modify the ConfigureServices method and register the JWT authentication right below the AddEntityFrameworkStores<RepositoryContext>() method:



https://jwt.io/#libraries
			 
			 
============== Example ================

I have developed a personal responsive website using angular 11 framework as front end and web api dotnet core as back end and used SQL server as database. To make the website responsive, used Bootstrap. I have used reactive forms to handle form inputs to register and login user and used form control to do validation of the fields before sending data to the server. For authentication, I have used Identity core  and authorization, I have used JWT token in the web api and used GUID as secret code and used SHA256 algorith to encrypt the secret code and generate the token. Used Angular service for consuming data from the REST API server. 

https://github.com/AmitaKunwar/AngularApplication.git
https://github.com/AmitaKunwar/WEBAPI_ASPDotNetCore.git


Register user using  Identity core .
Build an Angular 11 Token based Authentication and Authorization with the Web Api Applictaion and implements JWT authentication. Used JSON web tokens to transfer informations btween client and server. create login and signup component using form validations. Form data (Reactive form) is validated by angular application before sending data to the server. Depending on User’s roles (admin, moderator, user), Navigation Bar changes its items automatically. Services contains method for sending HTTP request and response. Installed Microsoft.AspNetCore.Authentication.JWTBearer library in the Identity project.
Used template driven form for LOGIN page. 
			  
	
	
	
	

	
	
	