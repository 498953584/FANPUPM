namespace com.jwsoft.pm.entpm.model
{
    using System;

    public interface IResourcePrice : IPriceItem
    {
        decimal Price { get; set; }
    }
}

