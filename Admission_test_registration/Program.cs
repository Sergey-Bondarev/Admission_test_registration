using Admission_test_registration.Models;

var builder = WebApplication.CreateBuilder(args);
//Add default data into DB
//using (ExamDBContext db = new ExamDBContext())
//{
//    db.FirstInitializeDB();
//}

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
