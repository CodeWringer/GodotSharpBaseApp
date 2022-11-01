using app.business.model;
using System;
using System.Collections.Generic;

namespace app.business.dataaccess.example.dto
{
    /// <summary>
    /// Represents a type that can be serialized to/from json. 
    /// <br></br>
    /// In order for this to be properly (de-/)serializable, this data type and its members must be public 
    /// and it must provide a public default constructor. 
    /// </summary>
    public class ExampleSerializable
    {
        public IEnumerable<AnItem> items;

        public ExampleSerializable()
        {
        }
    }
}
