using Saritasa.Tools.Domain.Exceptions;

namespace Saritasa.RedMan.UseCases.Store.Common.Exceptions;

/// <summary>
/// Invalid SKU exception.
/// </summary>
[Serializable]
public class InvalidSkuException : DomainException
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public InvalidSkuException() : base("Invalid SKU number. It must start with SK characters.")
    {
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public InvalidSkuException(string message) : base(message)
    {
    }

    /// <summary>
    /// Constructor for deserialization.
    /// </summary>
    /// <param name="serializationInfo">Stores all the data needed to serialize or deserialize an object.</param>
    /// <param name="streamingContext">Describes the source and destination of a given serialized stream,
    /// and provides an additional caller-defined context.</param>
    protected InvalidSkuException(System.Runtime.Serialization.SerializationInfo serializationInfo,
        System.Runtime.Serialization.StreamingContext streamingContext)
    {
    }
}
