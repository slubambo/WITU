using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class PersonMapping : ClassMap<Person>
	{
		public PersonMapping()
		{
			Table("`person`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.AltEmailAddress, "altEmailAddress");
			Map(x => x.AltTelephoneContact, "altTelephoneContact");
			Map(x => x.CreatedOn, "createdOn");
			Map(x => x.DateOfBirth, "dateOfBirth");
			Map(x => x.EmailAddress, "emailAddress");
			Map(x => x.Gender, "gender");
			Map(x => x.GivenName, "givenName");
			Map(x => x.HomeLanguage, "homeLanguage");
			Map(x => x.LastModified, "lastModified");
			Map(x => x.MaritalStatus, "maritalStatus");
			Map(x => x.NextOfKinAddress, "nextOfKinAddress");
			Map(x => x.NextOfKinContact, "nextOfKinContact");
			Map(x => x.NextOfKinName, "nextOfKinName");
			Map(x => x.NextOfKinRelationship, "nextOfKinRelationship");
			Map(x => x.Occupation, "occupation");
			Map(x => x.Organisation, "organisation");
			Map(x => x.OtherName, "otherName");
			Map(x => x.PermentAddress, "permentAddress");
			Map(x => x.PersonOwnerType, "personOwnerType");
			Map(x => x.PlaceOfBirth, "placeOfBirth");
			Map(x => x.PostalAddress, "postalAddress");
			Map(x => x.PreferredLanguage, "preferredLanguage");
			Map(x => x.ProfilePhotoName, "profilePhotoName");
			Map(x => x.Religion, "religion");
			Map(x => x.Surname, "surname");
			Map(x => x.TelephoneContact, "telephoneContact");
			Map(x => x.Title, "title");
			Map(x => x.WebsiteUrl, "websiteUrl");
			References(x => x.Country)
				.Class(typeof(Country))	
				.Column("countryOfBirthId")
				.Fetch.Select()
				.Cascade.All();
			References(x => x.Country1)
				.Class(typeof(Country))	
				.Column("countryOfResidenceId")
				.Fetch.Select()
				.Cascade.All();
			References(x => x.Country2)
				.Class(typeof(Country))	
				.Column("nationalityId")
				.Fetch.Select()
				.Cascade.All();
			References(x => x.County)
				.Class(typeof(County))	
				.Column("countyOfBirthId")
				.Fetch.Select()
				.Cascade.All();
			References(x => x.County1)
				.Class(typeof(County))	
				.Column("countyOfResidenceId")
				.Fetch.Select()
				.Cascade.All();
			References(x => x.District)
				.Class(typeof(District))	
				.Column("districtOfBirthId")
				.Fetch.Select()
				.Cascade.All();
			References(x => x.District1)
				.Class(typeof(District))	
				.Column("districtOfResidenceId")
				.Fetch.Select()
				.Cascade.All();
			HasMany(x => x.ProspectiveStudents)
				.KeyColumn("personId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.References)
				.KeyColumn("personId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.Instructors)
				.KeyColumn("personId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.Students)
				.KeyColumn("personId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			References(x => x.SubCounty)
				.Class(typeof(SubCounty))	
				.Column("subCountyOfBirthId")
				.Fetch.Select()
				.Cascade.All();
			References(x => x.SubCounty1)
				.Class(typeof(SubCounty))	
				.Column("subCountyOfResidenceId")
				.Fetch.Select()
				.Cascade.All();
		    Cache.ReadWrite();
		}
	}
}