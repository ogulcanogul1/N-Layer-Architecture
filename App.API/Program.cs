using App.API;
using App.Repositories;
using App.Repositories.Extentions;
using App.Service.Extentions;
using App.Service.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

RepositoryExtentitions.AddRepository(builder.Services, builder.Configuration);
ServiceExtentions.AddService(builder.Services);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>(); // kendi custom filterimizi ekledik.
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    // .Net taraf�ndan gelir, projede nullable �zelli�i a��ksa kar��la��lacak bir durumdu.E�er nullable de�ilse ve de�ere null de�eri girilirse fluenValidation ile bu hata kar��lansa bile .Net kendi de bir hata mesaj�n� yollayacakt�r.Bunu engeller.

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);   // varsay�lan filter kald�r�ld� kendi filterimizi ekleyece�iz.


var app = builder.Build();

app.UseExceptionHandler(x => { });

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
