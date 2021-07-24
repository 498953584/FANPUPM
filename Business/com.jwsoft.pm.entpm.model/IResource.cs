namespace com.jwsoft.pm.entpm.model
{
    using System;

    public interface IResource
    {
        string ResourceCode { get; set; }

        string ResourceName { get; set; }

        string Specification { get; set; }
    }
}

