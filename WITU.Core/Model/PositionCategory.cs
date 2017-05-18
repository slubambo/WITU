using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WITU.Core.Model
{
    [Serializable]
    public class PositionCategory
    {
        public PositionCategory()
		{
            Positions = new Iesi.Collections.Generic.HashedSet<Position>();	
		}

        public virtual Iesi.Collections.Generic.ISet<Position> Positions
        {
            get;
            set;
        }

        public virtual int Id
        {
            get;
            set;
        }
        public virtual string Name
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as PositionCategory);
        }

        public virtual bool Equals(PositionCategory obj)
        {
            if (obj == null) return false;

            if (Equals(Id, obj.Id) == false) return false;
            if (Equals(Name, obj.Name) == false) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int result = 1;

            result = (result * 397) ^ Id.GetHashCode();
            result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            return result;
        }
    }
}
