using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Data.Dtos;
using MovieAPI.Data;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly MovieContext _movieContext;
        private readonly IMapper _mapper;

        public AddressController(MovieContext movieContext, IMapper mapper)
        {
            _movieContext = movieContext;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddAddress([FromBody] CreateAddressDto createAddressDto)
        {
            Address Address = _mapper.Map<Address>(createAddressDto);
            _movieContext.Address.Add(Address);
            _movieContext.SaveChanges();
            return CreatedAtAction(nameof(GetAddressById), new { Id = Address.Id }, createAddressDto);
        }

        [HttpGet]
        public IActionResult GetAddresss()
        {
            var Address = _movieContext.Address.ToList();

            return Ok(Address);
        }

        [HttpGet("{id}")]
        public IActionResult GetAddressById(int id)
        {
            var Address = _mapper.Map<List<ReadAddressDto>>(_movieContext.Address.FirstOrDefault(Address => Address.Id == id));
            if (Address != null)
            {
                ReadAddressDto AddressDto = _mapper.Map<ReadAddressDto>(Address);
                return Ok(AddressDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAddress(int id, [FromBody] UpdateAddressDto AddressDto)
        {
            var Address = _mapper.Map<List<ReadAddressDto>>(_movieContext.Address.FirstOrDefault(Address => Address.Id == id));
            if (Address == null)
            {
                return NotFound();
            }
            _mapper.Map(AddressDto, Address);
            _movieContext.SaveChanges();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            var Address = _movieContext.Address.FirstOrDefault(Address => Address.Id == id);
            if (Address != null)
            {
                _movieContext.Address.Remove(Address);
                _movieContext.SaveChanges();
                return Ok(Address);
            }
            return NotFound();

        }
    }
}
