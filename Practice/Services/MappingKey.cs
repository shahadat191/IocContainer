using System;

namespace Practice.Services
{
    internal class MappingKey
    {
        public Type Type { get; set; }
        public string InstanceName { get; set; }

        public MappingKey(Type type, string instanceName)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            Type = type;
            InstanceName = instanceName; 
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int multiplier = 31;
                int hash = GetType().GetHashCode();
                hash = hash * multiplier + Type.GetHashCode();
                hash = hash * multiplier + (InstanceName == null ? 0 : InstanceName.GetHashCode());
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var compareTo = obj as MappingKey;
            if (ReferenceEquals(this, compareTo))
                return true;
            if (compareTo == null)
                return false;
            return Type.Equals(compareTo.Type) && string.Equals(InstanceName, compareTo.InstanceName);

        }

        public override string ToString()
        {
            return $"{InstanceName ?? "[null]"} - ({Type.FullName}) - hash code - ${this.GetHashCode()}";
        }

        public string ToTraceString()
        {
            return $"Instance Name - {InstanceName ?? "[null]"} (${Type.FullName})";
        }
    }
}