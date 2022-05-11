using System;

namespace Blogifier.Shared.Exceptions;

[Serializable]
public class DiscardedFunctionalityException : NotImplementedException
{
    public DiscardedFunctionalityException() :
        base()
    { }
    public DiscardedFunctionalityException(string message) :
        base(message)
    { }
    public DiscardedFunctionalityException(string message, Exception inner) :
        base(message, inner)
    { }
}
