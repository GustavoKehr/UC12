using Chapter.Contexts;
using Chapter.Interfaces;
using Chapter.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ChapterContext, ChapterContext>();
builder.Services.AddTransient<ILivroRepository, LivroRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddControllers();

// Add services cors policy
builder.Services.AddCors(options =>
{

    options.AddPolicy("CorsPolicy", builder =>

    {

        builder.WithOrigins("http://localhost:3000")

        .AllowAnyHeader()

        .AllowAnyMethod();
    });
});

// Add services Jwt Bearer : forma de autenticação
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})

//define os parâmetros de validação do token
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //valida quem está solicitando
        ValidateIssuer = true,

        //valida quem está recebendo
        ValidateAudience = true,

        //define se o tempo de expiração sera validado
        ValidateLifetime = true,

        //forma de criptografia e ainda valida a chave de autenticação
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao")),

        //valida o tempo de expiração do token
        ClockSkew = TimeSpan.FromMinutes(60),

        //nome do issuer, de onde está vindo
        ValidIssuer = "Chapter",

        //nome do audience, para onde está indo 
        ValidAudience = "Chapter"
    };
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Add services documentation Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Chapter API",
        Description = "API de livros desenvolvida na UC de Desenvolvimento de API",        
        Contact = new OpenApiContact
        {
            Name = "Carlos Roque",            
        },
        License = new OpenApiLicense
        {
            Name = "Chapter License"           
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));


    //add implement authentication in Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });

});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();