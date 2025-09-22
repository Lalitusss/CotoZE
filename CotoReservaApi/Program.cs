using CotoReservaApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar pol�tica CORS para permitir solicitudes desde localhost:3000 (donde corre el frontend React)
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

// Aplicar pol�tica CORS antes de la autorizaci�n
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
