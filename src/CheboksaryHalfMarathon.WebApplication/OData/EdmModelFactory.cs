using Microsoft.OData.Edm;

namespace CheboksaryHalfMarathon.WebAplication.OData
{
    public class EdmModelFactory : IConventionModelFactory
    {
        private readonly object _lockObject = new object();
        private IEdmModel _model;

        public IEdmModel CreateOrGet()
        {
            throw new NotImplementedException();
        }

    }
}
