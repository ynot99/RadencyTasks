﻿namespace HomeTask2.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() { }
        public EntityNotFoundException(string message) : base(message) { }
    }
}
