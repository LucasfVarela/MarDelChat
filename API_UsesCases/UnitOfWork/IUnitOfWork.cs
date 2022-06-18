using API_DataCore.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_UsesCases.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

       
        IUsuarioRepository UsuarioRepo { get; }
        void Save();
    }
}
