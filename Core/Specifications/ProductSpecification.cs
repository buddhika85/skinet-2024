using Core.Entities;

namespace Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecificationParams specParams) : base(x =>
            (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
            (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.Brand)) &&
            (specParams.Types.Count == 0 || specParams.Types.Contains(x.Type))
        )
        {
            // assume page size = 5
            // ApplyPaging(skip, take)
            // page 1
            // skip = 5 * 1-1 = 0
            // take = 5
            // --
            // page 2
            // skip = 5 * 2-1 = 5
            // take = 5   -- means take next file
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            switch (specParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(x => x.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(x => x.Price);
                    break;
                case "nameAsc":
                    AddOrderBy(x => x.Name);
                    break;
                case "nameDesc":
                    AddOrderByDesc(x => x.Name);
                    break;
                default:
                    AddOrderBy(x => x.Name);
                    break;
            }
        }
    }
}
