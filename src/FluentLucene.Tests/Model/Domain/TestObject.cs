using System;

namespace FluentLucene.Tests.Model.Domain
{
    public class TestObject
    {
        public string IgnoredProperty { get; set; }
        public string ValidProperty { get; set; }
        public Guid Id { get; set; }
        public long LongId { get; set; }
        public string Text { get; set; }
    }   
}