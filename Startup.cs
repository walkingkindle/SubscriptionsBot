using Application.Interface;
using Application.Services;
using Domain.Interface;
using Infrastructure;

namespace SubscriptionsBot;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // This is where you register services like DB, authentication, etc.
        services.AddControllers();

        services.AddDbContext<ApplicationDbContext>();

        services.AddTransient<ISubscriberService, SubscriberService>();

        services.AddTransient<ISubscriptionService, SubscriptionService>();

        services.AddTransient<IPaymentService, PaymentService>();

        services.AddTransient<IOutstandingPaymentsService, OutstandingPaymentsService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}