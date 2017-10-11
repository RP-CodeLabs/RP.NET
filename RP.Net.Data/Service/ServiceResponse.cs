using RP.Net.Data.Entities;

namespace RP.Net.Data.Service
{
    public class ServiceResponse<TEntity> where TEntity : Entity
    {
        public ServiceResponse(TEntity enity)
        {
            Enity = enity;
        }

        public TEntity Enity { get; }

        public bool IsSuccess { get; set; }
    }
}