using System;
namespace Evento.InfraStructure.DTO
{
    public class JwtDto
    {
          public string Token { get; set; }
          public long Expires { get; set; }
    }
}
