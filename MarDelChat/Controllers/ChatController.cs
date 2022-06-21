using API_CoreBusiness.Entities;
using API_LoggerCore.CustomLogger;
using API_UsesCases.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace MarDelChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly ILogger<ChatController> _logger;
        private CustomLogger loggerCustom { get; set; }

        public ChatController(IUnitOfWork context, ILogger<ChatController> logger)
        {
            _context = context;
            _logger = logger;
            loggerCustom = new CustomLogger(_logger);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Chat>> Get()
        {
            loggerCustom.Info("[Get] Chat");
            var entidadaux = _context.ChatRepo.GetAll();
            return Ok(entidadaux);
        }

        public ActionResult CrearChat([FromBody] Chat chat)
        {
            loggerCustom.Info("[Post] Chat");
            _context.ChatRepo.Insert(chat);
            _context.Save();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult EliminarChat(int id)
        {
            var entidadaux = _context.ChatRepo.GetById(id);
          
            loggerCustom.Info("[Delete] Chat");
            _context.ChatRepo.Delete(id);
            _context.Save();
            return Ok();
        }
    }



}

