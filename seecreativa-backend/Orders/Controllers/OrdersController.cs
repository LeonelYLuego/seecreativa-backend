using Microsoft.AspNetCore.Mvc;
using seecreativa_backend.Clients.Repositories;
using seecreativa_backend.Orders.Models;
using seecreativa_backend.Orders.Repositories;
using seecreativa_backend.Prices.Repositories;
using seecreativa_backend.Products.Repositories;
using seecreativa_backend.Users.Attributes;
using seecreativa_backend.Utils.Attributes;

namespace seecreativa_backend.Orders.Controllers {

    [ApiController]
    [Route("Api/[controller]")]
    public class OrdersController : Controller {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IClientsRepository _clientsRepository;
        private readonly IPricesRepository _pricesRepository;
        private readonly IProductsRepository _productsRepository;

        public OrdersController(
            IOrdersRepository ordersRepository,
            IClientsRepository clientsRepository,
            IPricesRepository pricesRepository,
            IProductsRepository productsRepository
        ) {
            _ordersRepository = ordersRepository;
            _clientsRepository = clientsRepository;
            _pricesRepository = pricesRepository;
            _productsRepository = productsRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderResponseDto>> Create([FromBody] OrderCreateDto createDto) {
            if ((await _clientsRepository.GetByIdAsync(createDto.ClientId)) == null)
                return NotFound($"Client with the Id {createDto.ClientId} not found");
            if ((await _pricesRepository.GetByIdAsync(createDto.PriceId)) == null)
                return NotFound($"Price with the Id {createDto.PriceId} not found");
            foreach (var product in createDto.Products) {
                if ((await _productsRepository.GetByIdAsync(product.ProductId)) == null)
                    return NotFound($"Product with the Id {createDto.PriceId} not found");
            }
            return (await _ordersRepository.CreateAsync(createDto)).ToResponse();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> ListBy() {
            var orders = await _ordersRepository.GetAllAsync();
            var responseOrders = orders.Select(u => u.ToResponse()).ToList();
            return Ok(responseOrders);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrderResponseDto>> GetById([ValidateId] string id) {
            var order = await _ordersRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound($"Order with the Id {id} not found");
            return Ok(order.ToResponse());
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteById([ValidateId] string id) {
            var result = await _ordersRepository.DeleteByIdAsync(id);
            if (!result)
                return NotFound($"Order with the Id {id} not found");
            return Ok(result);
        }
    }
}
