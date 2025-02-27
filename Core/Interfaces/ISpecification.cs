using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }       // where in LINQ

        Expression<Func<T, object>>? OrderBy { get; }

        Expression<Func<T, object>>? OrderByDesc { get; }

        bool IsDistinct { get; }
    }

    public interface ISpecification<T, TResult> : ISpecification<T>
    {
        Expression<Func<T, TResult>>? Select { get; }       // Select in LINQ
    }
}
