using System;


namespace Peg.Util
{
    /// <summary>
    /// Concrete, non-generic serializable hashmaps. Generic version will serialize just fine,
    /// but in order for us to define a custom inspector we need to supply it a concrete type.
    /// Therefore we always need to create a concrete dictionary type and a concrete hashmap drawer.
    /// Stupid Unity... not supporting generics >:(
    /// </summary> 
    [Serializable]
    public class StringIntHashMap : HashMap<string, int> { }

    [Serializable]
    public class StringFloatHashMap : HashMap<string, float> { }

    [Serializable]
    public class StringStringHashMap : HashMap<string, string> { }

    [Serializable]
    public class StringBoolHashMap : HashMap<string, bool> { }
}

