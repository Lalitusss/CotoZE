using CotoReservaApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar política CORS para permitir solicitudes desde localhost:3000 (donde corre el frontend React)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ReservaService>();

var app = builder.Build();
// Middleware para redirigir la raíz "/" a "/swagger/index.html"
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html");
        return;
    }
    await next();
});

//Comento para que funcione en Docker
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
//app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("../swagger/v1/swagger.json", "DemoAPI v1");
});

app.UseHttpsRedirection();

// Aplicar política CORS antes de la autorización
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
