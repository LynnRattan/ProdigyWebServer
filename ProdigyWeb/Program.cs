using System.Text.Json.Serialization;
using ProdigyServerBL.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

#region DB context
string connection = builder.Configuration.GetConnectionString("ProdigyDB");
builder.Services.AddDbContext<ProdigyDbContext>(options => options.UseSqlServer(connection));
#endregion

#region Json handling
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
#endregion

#region Session support

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(180);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

#endregion


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

#region use files and session
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
#endregion

app.MapControllers();

app.Run();
