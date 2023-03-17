using System;

namespace Libary.Migrations.Interfaces
{
    public interface IMergeDataOrMatchSyntax<T>
    {
        IMergeDataOrMatchSyntax<T> Row(T dataAsAnonymousType);
        void Match<M>(Func<T, M> f);
    }
}
