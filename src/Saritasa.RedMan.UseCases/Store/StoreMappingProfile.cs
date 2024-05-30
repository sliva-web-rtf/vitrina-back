using AutoMapper;
using Saritasa.RedMan.Domain.Store;
using Saritasa.RedMan.UseCases.Store.CreateProduct;
using Saritasa.RedMan.UseCases.Store.GetProductById;
using Saritasa.RedMan.UseCases.Store.SearchProducts;
using Saritasa.RedMan.UseCases.Store.UpdateProduct;

namespace Saritasa.RedMan.UseCases.Store;

/// <summary>
/// Store mapping profile.
/// </summary>
public class StoreMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public StoreMappingProfile()
    {
        CreateMap<Product, ProductSummaryDto>();
        CreateMap<CreateProductCommand, Product>(MemberList.Source);
        CreateMap<UpdateProductCommand, Product>(MemberList.Source);
        CreateMap<Product, ProductDto>();
    }
}
