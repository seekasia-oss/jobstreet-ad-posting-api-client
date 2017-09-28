﻿using System;

namespace JobStreet.AdPostingApi.Client.Hal
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FromHeaderAttribute : Attribute
    {
        public FromHeaderAttribute(string header)
        {
            this.Header = header;
        }

        public string Header { get; set; }
    }
}