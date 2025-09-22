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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aplicar política CORS antes de la autorización
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
