using DotNetEnv;
using SE171089_Services.AccountService;
using SE171089_Services.BookService;
using SE171089_Services.RentService;
namespace SE171089_RazorPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSingleton<IAccountService>(AccountService.Instance);
            builder.Services.AddSingleton<IBookService>(BookService.Instance);
            builder.Services.AddSingleton<IRentService>(RentService.Instance);
            builder.Services.AddSession();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}