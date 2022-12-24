using Microsoft.AspNetCore.Mvc;

namespace CaaS.Api.Controllers;

public static class StatusInfo
{
    public static ProblemDetails CustomerAlreadyExists(String customerId) => new ProblemDetails
    {
        Title = "Conflicting customer IDs",
        Detail = $"Customer wid ID '{customerId}' already exists"
    };

    public static ProblemDetails InvalidCustomerId(String customerId) => new ProblemDetails
    {
        Title = "Invalid customer ID",
        Detail = $"Customer with ID '{customerId}' does not exist"
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
