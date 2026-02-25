using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Domain.Enums
{
    public enum TipoNotificacion
    {
        PrestamoCreado = 1,
        PrestamoPorVencer = 2,
        PrestamoVencido = 3,
        PenalizacionGenerada = 4
    }
}
