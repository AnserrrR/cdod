﻿namespace cdod.Schema.OutputTypes
{
    public class TokenSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }

        public bool isAdmin { get; set; }
    }
}