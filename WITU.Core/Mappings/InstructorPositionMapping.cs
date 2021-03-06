using System;
using WITU.Core.Model;
using WITU.Core.Utils;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class InstructorPositionMapping : ClassMap<InstructorPosition>
	{
		public InstructorPositionMapping()
		{
			Table("`instructor_position`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.AcademicQaulification, "academicQaulification");
			Map(x => x.AssignedDate, "assignedDate");
			Map(x => x.DeactivatedDate, "deactivatedDate");
			Map(x => x.IdNumber, "idNumber");
            Map(x => x.IsActive, "isActive").CustomType<BoolAsTinyInt>();
			Map(x => x.LevelId, "levelId");
			Map(x => x.WorkStatus, "workStatus");
			References(x => x.Position)
				.Class(typeof(Position))
				.Not.Nullable()	
				.Column("positionId")
				.Fetch.Select()
				.Cascade.All();
			References(x => x.Staff)
				.Class(typeof(Instructor))
				.Not.Nullable()	
				.Column("instructorId")
				.Fetch.Select()
				.Cascade.All();
            References(x => x.Campus)
                .Class(typeof(Campus))
                .Nullable()
                .Column("campusId")
                .Fetch.Select()
                .Cascade.All();
		    Cache.ReadWrite();

            HasMany(x => x.StaffCourses)
                .KeyColumn("InstructorPositionId")
                .Inverse()
                .Cascade.All()
                .Fetch.Select()
                .AsSet();
		}
	}
}