using Microsoft.OData.Edm;

namespace CheboksaryHalfMarathon.WebAplication.OData
{
    public interface IConventionModelFactory
    {
        IEdmModel CreateOrGet();
    }
}
