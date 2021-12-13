using System;
using MediatR;
using System.Reflection;
using KlirTechChallenge.Domain;
using KlirTechChallenge.Domain.Quotes;
using KlirTechChallenge.Domain.Orders;
using KlirTechChallenge.Domain.Payments;
using KlirTechChallenge.Domain.Products;
using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Domain.Core.Events;
using KlirTechChallenge.Application.Orders;
using KlirTechChallenge.Domain.SharedKernel;
using KlirTechChallenge.Infrastructure.Events;
using KlirTechChallenge.Infrastructure.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using KlirTechChallenge.Infrastructure.Domain.Quotes;
using KlirTechChallenge.Infrastructure.Domain.Orders;
using KlirTechChallenge.Infrastructure.Identity.Users;
using KlirTechChallenge.Infrastructure.Identity.Claims;
using KlirTechChallenge.Infrastructure.Domain.Payments;
using KlirTechChallenge.Infrastructure.Domain.Products;
using KlirTechChallenge.Infrastructure.Domain.Customers;
using KlirTechChallenge.Infrastructure.Identity.Services;
using KlirTechChallenge.Application.Customers.DomainServices;
using KlirTechChallenge.Infrastructure.Domain.CurrencyExchange;
using KlirTechChallenge.Application.Customers.RegisterCustomer;
using KlirTechChallenge.Domain.Promotions;
using KlirTechChallenge.Domain.Promotion.Rule;
using KlirTechChallenge.Infrastructure.Domain.Promotions;

namespace KlirTechChallenge.Infrastructure.IoC;

public static class ServicesInjectionExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // Domain services
        services.AddScoped<ICustomerUniquenessChecker, CustomerUniquenessChecker>();
        services.AddScoped<ICurrencyConverter, CurrencyConverter>();
        services.AddScoped<IApllyPromotionBusinessRule, ApplyPromotionBusinessRule>();

        services.AddScoped<IOrderStatusWorkflow, OrderStatusWorkflow>();
            
        // Application - Handlers            
        services.AddMediatR(typeof(RegisterCustomerCommandHandler).GetTypeInfo().Assembly);
        services.AddScoped<IOrderStatusBroadcaster, OrderStatusBroadcaster>();

        // Infra - Domain persistence
        services.AddScoped<IEcommerceUnitOfWork, EcommerceUnitOfWork>();
        services.AddScoped<ICustomers, Customers>();
        services.AddScoped<IProducts, Products>();
        services.AddScoped<IOrders, Orders>();
        services.AddScoped<IQuotes, Quotes>();
        services.AddScoped<IPayments, Payments>();
        services.AddScoped<IPromotions, Promotions>();

        // Infrastructure - Data EventSourcing
        services.AddScoped<IStoredEvents, StoredEvents>();
        services.AddSingleton<IEventSerializer, EventSerializer>();

        // Infrastructure - Identity     
        services.AddTransient<IAuthorizationHandler, ClaimsRequirementHandler>();
        services.AddTransient<IApplicationUserDbAccessor, ApplicationUserDbAccessor>();
        services.AddTransient<IJwtService, JwtService>();
        services.AddTransient<IApplicationUser, ApplicationUser>();
        services.AddHttpContextAccessor();

        // Messaging
        services.AddScoped<IMessagePublisher, MessagePublisher>();
        services.AddScoped<IMessageProcessor, MessageProcessor>();
    }
}