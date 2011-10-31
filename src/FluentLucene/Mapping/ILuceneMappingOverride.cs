namespace FluentLucene.Mapping
{
    public interface ILuceneMappingOverride<T>
    {
        void Override(LuceneMapping<T> mapping);
    }
}