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
    // .Net tarafýndan gelir, projede nullable özelliði açýksa karþýlaþýlacak bir durumdu.Eðer nullable deðilse ve deðere null deðeri girilirse fluenValidation ile bu hata karþýlansa bile .Net kendi de bir hata mesajýný yollayacaktýr.Bunu engeller.

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);   // varsayýlan filter kaldýrýldý kendi filterimizi ekleyeceðiz.


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
