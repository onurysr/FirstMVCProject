using ITServiceApp.MapperProfiles;
using ITServiceApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Xunit.Abstractions;

//[assembly: OwinStartup(typeof(ITServiceTest.Startup))]

namespace ITServiceTest
{
    public class Startup
    {

        public void Configuration(IServiceCollection services)
        {
            services.AddScoped<IPaymentService, IyzicoPaymentService>();
            services.AddAutoMapper(options =>
            {
                options.AddProfile(typeof(PaymentProfile));
            });
        }
    }
}
