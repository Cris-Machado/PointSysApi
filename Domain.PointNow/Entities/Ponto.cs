using Point.Domain.Enumerators;

namespace Point.API.Domain.Services
{
    public class Ponto
    {
        public virtual Guid Id { get; protected set; }
        public virtual Guid UserId { get; protected set; }
        public virtual PointTypeEnum PontoType { get; protected set; } = PointTypeEnum.None;
        public virtual DateTime Hora { get; protected set; } = DateTime.MinValue;
    }
}
