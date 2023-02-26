﻿using Microsoft.AspNetCore.Mvc;

namespace CaaS.Api.Controllers;

public static class StatusInfo
{
    public static ProblemDetails CustomerAlreadyExists(String customerId) => new ProblemDetails
    {
        Title = "Conflicting customer IDs",
        Detail = $"Customer wid ID '{customerId}' already exists"
    };

    public static ProblemDetails InvalidPersonId(String customerId) => new ProblemDetails
    {
        Title = "Invalid Person ID",
        Detail = $"Customer with ID '{customerId}' does not exist"
    }; 
    
    public static ProblemDetails InvalidEmail(String email) => new ProblemDetails
    {
        Title = "Invalid Person ID",
        Detail = $"Customer with ID '{email}' does not exist"
    }; 
    public static ProblemDetails InvalidProductId(String productId) => new ProblemDetails
    {
        Title = "Invalid product ID",
        Detail = $"Product with ID '{productId}' does not exist"
    };
    public static ProblemDetails InvalidCartId(String cartId) => new ProblemDetails
    {
        Title = "Invalid cart ID",
        Detail = $"Cart with ID '{cartId}' does not exist"
    };
    public static ProblemDetails InvalidProductNameOrDescription(String nameOrDescription) => new ProblemDetails
    {
        Title = "Invalid product name",
        Detail = $"Product with a name or description '{nameOrDescription}' does not exist"
    };

    public static ProblemDetails OrderAlreadyExists(String orderId) => new ProblemDetails
    {
        Title = "Conflicting order IDs",
        Detail = $"Order wid ID '{orderId}' already exists"
    };


    public static ProblemDetails InvalidOrderId(String orderId) => new ProblemDetails
    {
        Title = "Invalid order ID",
        Detail = $"Order with ID '{orderId}' does not exist"
    };
    
    
}
