using SharedKernel;

namespace SharedKernel
{
    public abstract class AggregateRoot : Entity
    {
        public AggregateRoot()
        {
        }

        public AggregateRoot(string id) : base(id)
        {
        }
    }
}