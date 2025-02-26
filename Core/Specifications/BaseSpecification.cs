using Core.Interfaces;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        private readonly Expression<Func<T, bool>>? criteria;
        public Expression<Func<T, bool>>? Criteria => criteria;

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDesc { get; private set; }

        protected BaseSpecification() : this(null)
        {
        }

        public BaseSpecification(Expression<Func<T, bool>>? criteria)
        {
            this.criteria = criteria;
        } 
        
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            this.OrderBy = orderByExpression;
        }

        protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            this.OrderByDesc = orderByDescExpression;
        }
    }
}
