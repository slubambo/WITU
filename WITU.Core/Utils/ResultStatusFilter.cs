using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Utils
{
    public class ResultStatusFilter : FilterDefinition
    {
        public ResultStatusFilter()
        {
            WithName("ExcludeDeletedStudentCourse") //.WithCondition("ResultStatus != " + (int)ResultStatus.DeletedApproved)
                .AddParameter("resultStatus", NHibernate.NHibernateUtil.Int32)
                ;

        }
    }
}
