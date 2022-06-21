using API_LoggerCore.CustomLogger;
using API_UsesCases.UnitOfWork;
using API_CoreBusiness.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarDelChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly IUnitOfWork context;
        private readonly ILogger<ContactoController> logger;

        private CustomLogger customLogger { get; set; }

        public ContactoController(IUnitOfWork context, ILogger<ContactoController> logger)
        {
            this.context = context;
            this.logger = logger;
            customLogger = new CustomLogger(logger);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contactos>> Get()
        {
            customLogger.Info("FROM CONTACTO");
            var contacto = context.ContactoRepository.GetAll();
            return Ok(contacto);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Contactos contacto)
        {
            customLogger.Info("FROM CONTACTO");
            context.ContactoRepository.Insert(contacto);
            context.Save();
            return Ok();
        }
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Contactos contacto, int id)
        {
            customLogger.Info("FROM CONTANCTO");
            context.ContactoRepository.Update(contacto, id);
            context.Save();
            return Ok();

        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            customLogger.Info("FROM CONTACTO");
            context.ContactoRepository.Delete(id);
            context.Save();
            return Ok();
        }
    }
}
