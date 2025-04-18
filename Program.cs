using CommunalServices.Domain;
using CommunalServices.Domain.Contracts;
using CommunalServices.Domain.ContractsRealization;
using CommunalServices.Storage;
using Microsoft.EntityFrameworkCore;

namespace CommunalServices
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<CommunalServicesPaymentContext>(options =>
			options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddTransient<IRepository, Repository>();
			builder.Services.AddTransient<IBankPaymentService, MockBankPaymentService>();
			builder.Services.AddTransient<IAbonentAuthenticationService, AbonentAuthenticationService>();
			builder.Services.AddTransient<IDebtPaymentService, DebtPaymentService>();
			builder.Services.AddTransient<IFlatDebtsQueryService, FlatDebtsQueryService>();
			builder.Services.AddTransient<IAbonentFlatsQueryService, AbonentFlatsQueryService>();
			builder.Services.AddTransient<IEmailNoticeService, EmailNoticeService>();

            var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.MapControllers();

            app.Run();
		}
	}
}
