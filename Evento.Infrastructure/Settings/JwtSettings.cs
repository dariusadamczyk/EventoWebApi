﻿using System;
namespace Evento.InfraStructure.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer  { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
