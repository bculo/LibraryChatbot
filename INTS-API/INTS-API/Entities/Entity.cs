﻿namespace INTS_API.Entities
{
    public abstract class Entity<T>
    {
        public virtual T Id { get; set; }
    }
}
