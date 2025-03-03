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

        public bool IsDistinct { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

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

        protected void ApplyDistinct()
        {
            IsDistinct = true;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Take = take;
            Skip = skip;
            IsPagingEnabled = true;
        }

        public IQueryable<T> ApplyCriteria(IQueryable<T> query)
        {
            if (Criteria != null)
            {
                query = query.Where(Criteria);
            }
            return query;
        }
    }

    public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria) :
        BaseSpecification<T>(criteria),
        ISpecification<T, TResult>
    {
        public Expression<Func<T, TResult>>? Select { get; private set; }

        protected BaseSpecification() : this(null!)
        {
        }

        protected void AddSelect(Expression<Func <T, TResult>> selectExpression) 
        {
            this.Select = selectExpression; 
        }
    }
}
