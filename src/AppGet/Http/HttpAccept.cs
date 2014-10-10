﻿using System;

namespace AppGet.Http
{
    public sealed class HttpAccept
    {
        public static readonly HttpAccept Rss = new HttpAccept("application/rss+xml, text/rss+xml, text/xml");
        public static readonly HttpAccept Json = new HttpAccept("application/json");
        public static readonly HttpAccept Html = new HttpAccept("text/html");
        
        public String Value { get; private set; }

        public HttpAccept(String accept)
        {
            Value = accept;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}