using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using WebAPIAutores.Filtros;
using WebAPIAutores.Middleware;
using WebAPIAutores.Servicios;
using WebAPIAutores.Utilidades;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace WebAPIAutores
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddControllers(opciones =>
            {
                opciones.Filters.Add(typeof(FiltroDeExcepcion));
                opciones.Conventions.Add(new SwaggerAgrupaPorVersion());
            }).AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles).AddNewtonsoftJson();
            
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            
            // Tiempos de vida de los servicios
            /*AddTransient: cuando necesitemos resolver el IServicio, se nos va a dar una nueva instancia de ServicioA*/
            /*AddScoped: en este caso se va a dar siempre la misma intancia en el mismo contexto http 
             * (entre distantas peticiones http se obtienen distintas intancias de la misma clase )*/
            /*AddSingleton: siempre se obtiene la misma intancia, incluso para distintas peticions http*/           


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["llavejwt"])),
                    ClockSkew = TimeSpan.Zero
                });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    // Aca pongo todas las caracteristicas que quiero mostrar en el swagger
                    // Se agrega informacion extra
                    Title = "WebAPIAutores", 
                    Version = "v1",
                    Description = "Este es un web api para trabajer con autores y libros",
                    Contact = new OpenApiContact
                    {
                        Email = "alvo@gmail.com",
                        Name = "Alvo",
                        Url = new Uri("https://gavilan.blog")
                    }
                });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "WebAPIAutores", Version = "v2" });
                c.OperationFilter<AgregarParametroHATEOAS>();
                c.OperationFilter<AgregarParametroXVersion>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                var archivoXML = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var rutaXML = Path.Combine(AppContext.BaseDirectory, archivoXML);
                c.IncludeXmlComments(rutaXML);
            });

            services.AddAutoMapper(typeof(Startup));


            // Configuracion del servicio de Identity (para logueo y roles)
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configuracion para una autorizacion vasada en claims
            services.AddAuthorization(opciones =>
            {
                opciones.AddPolicy("EsAdmin", politica => politica.RequireClaim("esAdmin"));
            });

            /*
             Encriptar: aca se encripta con una llame en un extremo y se desencripta con la misma llave en el otro extromo
            Hash: aca apenas se crea el dato se obtiene su hash y directamente se guarda el hash. Este es el caso de los passwords
             */
            // Servicios de proteccion de datos ( para encriptar por ejemplo ).
            services.AddDataProtection();
            // Servicio para poder realizar un Hash a un dato
            services.AddTransient<HashService>();

            // Configuracion de los Cors
            services.AddCors(opciones =>
            {
                opciones.AddDefaultPolicy(builder => 
                {
                    builder.WithOrigins("https://www.apirequest.io").AllowAnyMethod().AllowAnyHeader()
                    .WithExposedHeaders(new string[] { "cantidadTotalRegistros" });
                });
            });

            services.AddTransient<GeneradorEnlaces>();
            services.AddTransient<HATEOASAutorFilterAttribute>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddApplicationInsightsTelemetry(Configuration["ApplicationInsights:ConnectionString"]);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.UseLoguearRespuestaHTTP();

                                 

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIAutores v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "WebAPIAutores v2");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
