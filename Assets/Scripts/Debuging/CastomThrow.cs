using System;
using UnityEngine;

public class CastomThrow : Exception
{
    public CastomThrow() : base() { }
    public CastomThrow(string str) : base(str) { }
    public CastomThrow(string str, Exception inner) : base(str, inner) { }
    protected CastomThrow(
    System.Runtime.Serialization.SerializationInfo si,
    System.Runtime.Serialization.StreamingContext sc) :
    base(si, sc)
    { }

    public override string ToString()
    {
        return Message;
    }
}
