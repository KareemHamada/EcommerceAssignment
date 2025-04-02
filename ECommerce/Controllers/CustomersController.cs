using Core.Entities;
using Core;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ECommerce.DTOs;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerDto> _validator;

        public CustomersController(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CustomerDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CustomerDto customerDto)
        {
            var validationResult = await _validator.ValidateAsync(customerDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var customer = _mapper.Map<Customer>(customerDto);
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, _mapper.Map<CustomerDto>(customer));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerDto customerDto)
        {
            if (id != customerDto.Id) return BadRequest("ID mismatch");

            var validationResult = await _validator.ValidateAsync(customerDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var existingCustomer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (existingCustomer == null) return NotFound();

            _mapper.Map(customerDto, existingCustomer);
            await _unitOfWork.Customers.UpdateAsync(existingCustomer);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
