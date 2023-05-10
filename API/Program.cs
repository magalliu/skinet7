using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using API.Middleware;
using Microsoft.AspNetCore.Mvc;
using API.Errors;
using API.Extensions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Core.Entities.Identity;

var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddIdentityService(builder.Configuration);
        builder.Services.AddSwaggerDocumentation();

        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        /*
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<StoreContext>(opt =>
        {
            opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.Configure<ApiBehaviorOptions>(options => 
        {
            options.InvalidModelStateResponseFactory=actionContext =>
        {
            var errors=actionContext.ModelState
                    .Where (e=>e.Value.Errors.Count>0)
                    .SelectMany(x=>x.Value.Errors)
                    .Select(x=>x.ErrorMessage).ToArray();

                    var errorResponse=new ApiValidationErrorResponse
                    {
                       Errors=errors     

                    };
                return new BadRequestObjectResult(errorResponse);
        };


        });
*/
        var app = builder.Build();

        // Configure the HTTP request pipeline.middlware

        app.UseStatusCodePagesWithReExecute("/errors/{0}");

        app.UseMiddleware<ExceptionMiddleware>();


        //app.UseSwagger();
        //app.UseSwaggerUI();

        app.UseSwaggerDocumentation();
        
        app.UseStaticFiles();
        app.UseCors("CorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<StoreContext>();
        var identitycontext = services.GetRequiredService<AppIdentityDbContext>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            await context.Database.MigrateAsync();
            await identitycontext.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context);
            await AppIdentityDbContextSeed.SeedUsersAsync(userManager);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error during migration");
        }


        app.Run();

