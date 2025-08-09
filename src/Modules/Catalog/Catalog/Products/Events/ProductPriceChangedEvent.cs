using Catalog.Products.Models;
using System.Net.Http.Headers;

namespace Catalog.Products.Events
{
    public record ProductPriceChangedEvent(Product Product) : IDomainEvent;
}
