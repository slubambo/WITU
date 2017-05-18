using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace WITU.Core.Utils
{
    public class BoolAsTinyInt : IUserType
    {
        public bool Equals(object x, object y)
        {
            return object.Equals(x, y);
        }

        public int GetHashCode(object x)
        {
            if (x == null)
                return 0;
            return x.GetHashCode();
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var obj = NHibernateUtil.String.NullSafeGet(rs, names[0]);
            if (obj == null)
                return false;
            byte b = 0;
            try
            {
                if (obj is string)
                    b = byte.Parse(obj as string);
                else
                    b = (byte) obj;
            }
            catch (Exception e)
            {
                return false;
            }
            return b == 1;
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (value == null)
            {
                ((IDataParameter) cmd.Parameters[index]).Value = DBNull.Value;
            }
            else
            {
                var boolValue = (bool) value;
                ((IDataParameter) cmd.Parameters[index]).Value = boolValue ? (byte) 1 : (byte) 0;
            }
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public SqlType[] SqlTypes
        {
            get { return new[] {NHibernateUtil.Int16.SqlType}; }
        }

        public Type ReturnedType
        {
            //get { return typeof (BoolAsTinyInt); }
            get { return typeof (bool); }
        }

        public bool IsMutable
        {
            get
            {
                return false;
            }
        }
    }
}
