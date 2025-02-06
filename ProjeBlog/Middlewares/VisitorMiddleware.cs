using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using LoggerPrototype.Interfaces.Concrete;
using ProjeBlog.RepositoryPattern.Services.Api;

namespace ProjeBlog.Middlewares
{
    public class VisitorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<VisitorMiddleware> _logger;
        private readonly ICustomDbLogger _customDbLogger;
        private readonly IpApiService _ipApiService;
        public VisitorMiddleware(RequestDelegate next, ILogger<VisitorMiddleware> logger, ICustomDbLogger customDbLogger, IpApiService ipApiService)
        {
            _next = next;
            _logger = logger;
            _customDbLogger = customDbLogger;
            _ipApiService = ipApiService;
        }

        public async Task Invoke(HttpContext context, IServiceScopeFactory scopeFactory)
        {
            string cookieName = "Visitor";

            // Kullanıcının IP adresini al
            string? ip = context.Connection.RemoteIpAddress?.ToString();
            string? Country = await _ipApiService.GetCountryFromIpAsync(ip);
            // Çerez var mı kontrol et
            if (!context.Request.Cookies.ContainsKey(cookieName))
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    // Daha önce IP ile kayıt olup olmadığını kontrol et
                    bool exists = await dbContext.VisitorLogs.AnyAsync(v => v.IP == ip);

                    if (!exists)
                    {
                        dbContext.VisitorLogs.Add(new VisitorLog { IP = ip, Date = DateTime.UtcNow });
                        await dbContext.SaveChangesAsync();

                        // Çerezi oluştur (30 gün boyunca saklanacak)
                        context.Response.Cookies.Append(cookieName, Guid.NewGuid().ToString(), new CookieOptions
                        {
                            Expires = DateTime.UtcNow.AddDays(30),
                            HttpOnly = true
                        });

                        _logger.LogInformation("Yeni tekil ziyaretçi eklendi.");
                    }
                }
            }

            // Bir sonraki middleware'e devam et
            await _next(context);
        }
    }
}
