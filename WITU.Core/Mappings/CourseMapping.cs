using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class CourseMapping : ClassMap<Course>
	{
		public CourseMapping()
		{
			Table("`course`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Code, "code");
			Map(x => x.CourseType, "courseType");
			Map(x => x.Weight, "weight");
		    Map(x => x.Content, "content");
		    Map(x => x.Status, "status");
            Map(x => x.TimeFrame, "timeFrame");
            Map(x => x.Name, "name");
			References(x => x.Subject)
				.Class(typeof(Subject))
				.Column("subjectId")
				.Fetch.Select()
				.Cascade.All();
            References(x => x.Campus)
                .Class(typeof(Campus))
                .Column("campusId")
                .Fetch.Select()
                .Cascade.All();
            References(x => x.CourseCore)
                .Class(typeof(CourseCore))
                .Column("courseCoreId")
                .Fetch.Select()
                .Cascade.All();
            References(x => x.CourseCategory)
                .Class(typeof(CourseCategory))
                .Column("courseCategoryId")
                .Fetch.Select()
                .Cascade.All();
            HasManyToMany(x => x.Courses)
				.ChildKeyColumn("courseId")
				.ParentKeyColumn("id")
				.Cascade.All()
				.Table("prerequisite_courses")
				.Fetch.Select()
				.AsSet();
			HasManyToMany(x => x.Courses1)
				.ChildKeyColumn("requiredCourseId")
				.ParentKeyColumn("id")
				.Cascade.All()
				.Table("prerequisite_courses")
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.StaffCourses)
				.KeyColumn("courseId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
            HasMany(x => x.StudentCourses)
                .KeyColumn("courseId")
                .KeyNullable()
                .Inverse()
                .Cascade.All()
                .Fetch.Select()
                .AsSet()
                //deleted courses are not to be included...
                .Where(x => x.ResultStatus != ResultStatus.DeletedApproved)
                ;
		    Cache.ReadWrite();
		}
	}
}