using Core.Entities;
using Core;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ECommerce.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<OrderCreateUpdateDto> _validator;
        private readonly ECommerceContext _context;
        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper, IValidator<OrderCreateUpdateDto> validator,ECommerceContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _context = context;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return Ok(_mapper.Map<OrderDto>(order));
        }



        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] OrderCreateUpdateDto orderDto)
        {
            var validationResult = await _validator.ValidateAsync(orderDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            // Validate customer exists
            var customer = await _unitOfWork.Customers.GetByIdAsync(orderDto.CustomerId);
            if (customer == null) return BadRequest("Customer not found");

            // Process order items
            var orderProducts = new List<OrderProduct>();
            double totalPrice = 0;

            foreach (var item in orderDto.Items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                if (product == null) return BadRequest($"Product with ID {item.ProductId} not found");
                if (product.Stock < item.Quantity) return BadRequest($"Not enough stock for product {product.Name}");

                orderProducts.Add(new OrderProduct
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity
                });

                totalPrice += product.Price * item.Quantity;
            }

            var order = new Order
            {
                CustomerId = orderDto.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                TotalPrice = totalPrice,
                OrderProducts = orderProducts
            };

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CompleteAsync();

            var orderReadDto = _mapper.Map<OrderDto>(order);
            return CreatedAtAction(nameof(GetById), new { id = orderReadDto.Id }, orderReadDto);
        }

        [HttpPut("status/{id}")]
        public async Task<ActionResult<OrderDto>> UpdateStatus(int id, [FromBody] OrderStatusUpdateDto statusDto)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == id);


            if (order == null) return NotFound();

            if (order.Status == "Delivered") return BadRequest("Order is already delivered");

            order.Status = statusDto.Status;

            // Update product stocks if order is delivered
            if (statusDto.Status == "Delivered")
            {
                foreach (var orderProduct in order.OrderProducts)
                {
                    var product = await _unitOfWork.Products.GetByIdAsync(orderProduct.ProductId);
                    if (product != null)
                    {
                        product.Stock -= orderProduct.Quantity;
                        await _unitOfWork.Products.UpdateAsync(product);
                    }
                }
            }

            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<OrderDto>(order));
        }

    }
}
