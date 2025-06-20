using DomainDrivenDesign.Application;
using DomainDrivenDesign.Domain.Dtos;
using DomainDrivenDesign.Infrastructure.DataContext;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainDrivenDesign.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DataController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly AppDbContext _context;

    public DataController(IMediator mediator, AppDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct(CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { Id = id });
    }

    [HttpGet("GetProducts")]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _context.Products
            .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price.Amount
                })
            .ToListAsync();

        return Ok(products);
    }

    [HttpPost("CreateSale")]
    public async Task<IActionResult> CreateSale(MakeSaleCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { Id = id });
    }

    [HttpGet("GetSales")]
    public async Task<IActionResult> GetSales()
    {
        var sales = await _context.Sales
            .Select(s => new SaleDto
            {
                Id = s.Id,
                Date = s.Date,
                Total = s.Total,
                Customer = new CustomerDto
                {
                    Id = s.Customer.Id,
                    Name = s.Customer.Name,
                    LoyaltyPoints = s.Customer.LoyaltyPoints
                },
                Items = s.Items.Select(i => new SaleItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    UnitPrice = i.UnitPrice.Amount,
                    Quantity = i.Quantity,
                    Total = i.Total.Amount
                }).ToList()
            })
            .ToListAsync();

        return Ok(sales);
    }

    [HttpPost("CreateCustomer")]
    public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { Id = id });
    }

    [HttpGet("GetCustomers")]
    public async Task<IActionResult> GetCustomers()
    {
        var customers = await _context.Customers
            .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    LoyaltyPoints = c.LoyaltyPoints
                })
            .ToListAsync();
        return Ok(customers);
    }
}
