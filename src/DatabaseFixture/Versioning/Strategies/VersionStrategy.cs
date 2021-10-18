using System;
using Ardalis.GuardClauses;

namespace DatabaseFixture.Versioning.Strategies
{
    public abstract class VersionStrategy<TSpecificVersion> : 
        IComparable<TSpecificVersion> where TSpecificVersion : VersionStrategy<TSpecificVersion>,
        IVersion
    {
        public abstract string DisplayName { get; }

        private bool Equals(VersionStrategy<TSpecificVersion> other)
        {
            return this == other;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((VersionStrategy<TSpecificVersion>)obj);
        }

        public override int GetHashCode()
        {
            return DisplayName.GetHashCode();
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public abstract int CompareTo(TSpecificVersion obj);

        public int CompareTo(object obj)
        {
            Guard.Against.Null(obj, nameof(obj));

            if (obj is not TSpecificVersion version)
            {
                throw new NotSupportedException(
                    "Versioning possible only within the same strategy type." +
                    $" If you would like to change the strategy from {GetType().Name} to {obj.GetType().Name}, create snapshot."
                    );
            }

            return CompareTo(version);
        }

        public static int operator >(VersionStrategy<TSpecificVersion> a, VersionStrategy<TSpecificVersion> b)
            => a.CompareTo(b);

        public static int operator <(VersionStrategy<TSpecificVersion> a, VersionStrategy<TSpecificVersion> b)
            => a.CompareTo(b);

        public static bool operator ==(VersionStrategy<TSpecificVersion> a, VersionStrategy<TSpecificVersion> b)
            => a.CompareTo(b) == 0;

        public static bool operator !=(VersionStrategy<TSpecificVersion> a, VersionStrategy<TSpecificVersion> b)
            => a.CompareTo(b) != 0;
    }
}