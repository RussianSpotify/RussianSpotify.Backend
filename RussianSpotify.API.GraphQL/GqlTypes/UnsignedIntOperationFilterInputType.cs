using HotChocolate.Data.Filters;

namespace RussianSpotify.API.GraphQL.GqlTypes;

public class UnsignedIntOperationFilterInputType
    : ComparableOperationFilterInputType<UnsignedIntType>
{
    protected override void Configure(IFilterInputTypeDescriptor descriptor)
    {
        descriptor.Name("UnsignedIntOperationFilterInputType");
        base.Configure(descriptor);
    }
}