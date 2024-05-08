using Point.Domain.Enumerators;

namespace Point.API.Domain.Services
{
    public class UserPoint
    {
        public UserPoint( Guid userId, PointTypeEnum pointType)
        {
            UserId = userId;
            PointType = pointType;
            DateHour = DateTime.Now;
        }

        public virtual Guid Id { get; protected set; }
        public virtual Guid UserId { get; protected set; }
        public virtual PointTypeEnum PointType { get; protected set; } = PointTypeEnum.None;
        public virtual DateTime DateHour { get; protected set; } = DateTime.MinValue;
    }
}
