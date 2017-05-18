using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WITU.Core.Model
{
    public class CourseCategory
    {
        public CourseCategory()
        {
            Courses = new Iesi.Collections.Generic.HashedSet<Course>();
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

        public virtual Iesi.Collections.Generic.ISet<Course> Courses
        {
            get;
            set;
        }
    }
}
