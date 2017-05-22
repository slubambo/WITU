using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Validations;

namespace WITU.Core.Model
{
    public class Syllabus
    {
        public Syllabus()
        {
            SyllabusAttachments = new Iesi.Collections.Generic.HashedSet<SyllabusAttachment>();
        }

        public virtual int Id
        {
            get;
            set;
        }

        public virtual Course Course
        {
            get;
            set;
        }

        public virtual int Week
        {
            get;
            set;
        }

        public virtual string Title
        {
            get;
            set;
        }

        public virtual string Overview
        {
            get;
            set;
        }

        public virtual string Graded
        {
            get;
            set;
        }

        public virtual Iesi.Collections.Generic.ISet<SyllabusAttachment> SyllabusAttachments
        {
            get;
            set;
        }
    }
}
