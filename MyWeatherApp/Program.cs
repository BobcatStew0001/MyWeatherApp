using Microsoft.AspNetCore.Builder;
/*To Do
 Add the wwwroot folder to the project
 inside it add the css and a site.css file
 Then link it in the head of the _Layout.cshtml file in the Views folder*/

    
    
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Weather}/{action=Index}/{id?}");

app.Run();
             
    
