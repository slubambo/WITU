using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
    [Serializable]
    public class Position
    {
        public Position()
        {
            StaffPositions = new Iesi.Collections.Generic.HashedSet<InstructorPosition>();
        }
        public virtual int Id
        {
            get;
            set;
        }
        public virtual string Level
        {
            get;
            set;
        }
        public virtual string Name
        {
            get;
            set;
        }

        public virtual PositionCategory PositionCategory
        {
            get;
            set;
        }

        public virtual Iesi.Collections.Generic.ISet<InstructorPosition> StaffPositions
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as Position);
        }

        public virtual bool Equals(Position obj)
        {
            if (obj == null) return false;

            if (Equals(Id, obj.Id) == false) return false;
            if (Equals(Level, obj.Level) == false) return false;
            if (Equals(Name, obj.Name) == false) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int result = 1;

            result = (result * 397) ^ Id.GetHashCode();
            result = (result * 397) ^ (Level != null ? Level.GetHashCode() : 0);
            result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            return result;
        }
    }
}