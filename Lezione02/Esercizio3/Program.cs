using Esercizio3.Services;

namespace Esercizio3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddHttpClient();
            builder.Services.AddHttpClient("namedHttpClient", c => {
                c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            });
            builder.Services.AddHttpClient<ExternalService>();
            //services.AddHttpClient<ExternalService>(c =>
            //{
            //    c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            //});

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}