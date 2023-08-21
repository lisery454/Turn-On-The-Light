using System;

namespace Moss
{
    public struct BindIdentifier : IEquatable<BindIdentifier>
    {
        public string Id { get; set; }
        public Type OriginalType { get; set; }


        public static BindIdentifier New(Type originalType, string id)
        {
            return new BindIdentifier(originalType, id);
        }

        private BindIdentifier(Type originalType, string id)
        {
            Id = id;
            OriginalType = originalType;
        }

        public override string ToString()
        {
            return $"BindIdentifier<original type:{OriginalType}, id: {Id}>";
        }

        public bool Equals(BindIdentifier other)
        {
            return Id == other.Id && OriginalType == other.OriginalType;
        }

        public override bool Equals(object obj)
        {
            return obj is BindIdentifier other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, OriginalType);
        }
    }
}