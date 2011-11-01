using FluentLucene.Members;
using FluentLucene.Reflection;
using FluentLucene.Tests.Model.Domain;
using FluentLucene.Tests.Specification;
using NUnit.Framework;

namespace FluentLucene.Tests.Extensions
{
    public class ReflectionExtensionsSpec
    {
        public class when_transorming_expression_to_member : ContextSpecification
        {
            private Member _result;

            protected override void SharedContext()
            {
                _result = ReflectionExtensions.ToMember<TestObject, object>(x => x.Text);
            }

            [Test]
            public void should_not_be_null()
            {
                _result.ShouldNotBeNull();
            }

            [Test]
            public void should_have_correct_type()
            {
                _result.PropertyType.ShouldEqual(typeof (string));
            }

            [Test]
            public void should_have_member_info()
            {
                _result.MemberInfo.ShouldNotBeNull();
            }
        }
    }
}