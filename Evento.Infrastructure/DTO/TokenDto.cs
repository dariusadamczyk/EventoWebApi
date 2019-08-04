using System;
namespace Evento.InfraStructure.DTO
{
    public class TokenDto
    {
        public string Token { get; set; }
        public long Expires { get; set; }
        public string Role { get; set; }
    }
}
