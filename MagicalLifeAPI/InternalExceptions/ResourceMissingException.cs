﻿using System;

namespace MagicalLifeAPI.InternalExceptions
{
    public class ResourceMissingException : Exception
    {
        public ResourceMissingException() : base("A resource was not found in its expected location!")
        {
        }

        public ResourceMissingException(string msg) : base(msg)
        {
        }
    }
}