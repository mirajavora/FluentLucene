using System;
using System.Collections.Generic;

namespace FluentLucene.Tests.Model.Domain
{
    public class ComplexObject
    {
        public string Title { get; set; }
        public IList<InnerObject> ListOfItems { get; set; }
    }

    public class InnerObject
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
    }
}