using Point.Domain.Enumerators;

namespace Point.API.Domain.Services
{
    public class Point
    {
        public Point(Guid id, Guid userId, PointTypeEnum pointType, DateTime dateHour)
        {
            Id = id;
            UserId = userId;
            PointType = pointType;
            DateHour = dateHour;
        }

        public virtual Guid Id { get; protected set; }
        public virtual Guid UserId { get; protected set; }
        public virtual PointTypeEnum PointType { get; protected set; } = PointTypeEnum.None;
        public virtual DateTime DateHour { get; protected set; } = DateTime.MinValue;
    }
}
