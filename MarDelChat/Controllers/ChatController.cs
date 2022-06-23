using API_CoreBusiness.Entities;
using API_LoggerCore.CustomLogger;
using API_UsesCases.UnitOfWork;
using API_Validations;
using Microsoft.AspNetCore.Mvc;

namespace MarDelChat.Controllers
{
    [Tags("CHAT")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IUnitOfWork context;
        private readonly ILogger<ChatController> logger;
        private CustomLogger customLogger { get; set; }

        public ChatController(IUnitOfWork context, ILogger<ChatController> logger)
        {
            this.context = context;
            this.logger = logger;
            customLogger = new CustomLogger(logger);
        }

        /// <summary>
        /// Todos los Chats
        /// </summary>
        /// <response code="200">Se creo correctamente</response>
        /// <response code="404">Chat no encontrado</response>

        [HttpGet]
        public ActionResult<IEnumerable<Chat>> Get()
        {
            customLogger.Info("[Get] Chat");
            var entidadaux = context.ChatRepo.GetAll();
            return Ok(entidadaux);
        }

        /// <summary>
        /// Crear Nuevo Chat
        /// </summary>
        /// <param name="chat"></param>
        /// <response code="200">Se creo correctamente</response>
        /// <response code="404">Chat no encontrado</response>

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public ActionResult Post([FromBody] Chat chat)
        {
            customLogger.Info("[Post] Chat");
            context.ChatRepo.Insert(chat);
            context.Save();
            return Ok();
        }

        /// <summary>
        /// Eliminar Chat
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Se creo correctamente</response>
        /// <response code="404">Chat no encontrado</response>

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var entidadaux = context.ChatRepo.GetById(id);

            customLogger.Info("[Delete] Chat");
            context.ChatRepo.Delete(id);
            context.Save();
            return Ok();
        }
    }

}

