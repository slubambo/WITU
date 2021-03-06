using System;
using System.ComponentModel.DataAnnotations;
using Iesi.Collections.Generic;

namespace WITU.Core.Model
{
    public enum Gender
    {
        Unknown = 0,

        Male = 1,

        Female = 2
    }
    [Serializable]
    public class Person
    {
        public Person()
        {
            ProspectiveStudents = new HashedSet<ProspectiveStudent>();
            References = new HashedSet<Reference>();
            Instructors = new HashedSet<Instructor>();
            Students = new HashedSet<Student>();
        }

        [Display(Name = "Alt. Email Address")]
        public virtual string AltEmailAddress { get; set; }

        [Display(Name = "Alt. Telephone Contact")]
        public virtual string AltTelephoneContact { get; set; }

        public virtual DateTime? CreatedOn { get; set; }

        //Birthday Validation
        [DateOfBirth(MinAge = 0, MaxAge = 15)]
        [Display(Name = "Date of Birth")]
        [Required]
        public virtual DateTime? DateOfBirth { get; set; }

        [Display(Name = "Email Address")]
        [Required]
        public virtual string EmailAddress { get; set; }

        [Required]
        public virtual Gender? Gender { get; set; }

        [Display(Name = "Given Name")]
        [Required]
        public virtual string GivenName { get; set; }

        [Display(Name = "Home Language")]
        public virtual string HomeLanguage { get; set; }

        public virtual int Id { get; set; }
        public virtual DateTime? LastModified { get; set; }

        [Display(Name = "Marital Status")]
        [Required]
        public virtual string MaritalStatus { get; set; }

        [Display(Name = "NOK Address")]
        public virtual string NextOfKinAddress { get; set; }

        [Display(Name = "NOK Contact")]
        [Required]
        public virtual string NextOfKinContact { get; set; }

        [Display(Name = "NOK Name")]
        [Required]
        public virtual string NextOfKinName { get; set; }

        [Display(Name = "NOK Relationship")]
        [Required]
        public virtual string NextOfKinRelationship { get; set; }

        public virtual string Occupation { get; set; }

        public virtual string Organisation { get; set; }

        [Display(Name = "Other Name")]
        public virtual string OtherName { get; set; }

        [Display(Name = "Permanent Address")]
        public virtual string PermentAddress { get; set; }

        public virtual int? PersonOwnerType { get; set; }

        [Display(Name = "Place of Birth")]
        public virtual string PlaceOfBirth { get; set; }

        [Display(Name = "Postal Address")]
        public virtual string PostalAddress { get; set; }

        [Display(Name = "Preferred Language")]
        public virtual string PreferredLanguage { get; set; }

        [Display(Name = "Profile Photo")]
        public virtual string ProfilePhotoName { get; set; }

        [Required]
        public virtual string Religion { get; set; }

        [Required]
        public virtual string Surname { get; set; }

        [Display(Name = "Telephone")]
        [Required]
        public virtual string TelephoneContact { get; set; }

        public virtual string Title { get; set; }

        [Display(Name = "Website")]
        public virtual string WebsiteUrl { get; set; }

        public virtual Country Country { get; set; }

        public virtual Country Country1 { get; set; }

        [Display(Name = "Nationality")]
        public virtual Country Country2 { get; set; }
        public virtual County County { get; set; }
        public virtual County County1 { get; set; }
        public virtual District District { get; set; }
        public virtual District District1 { get; set; }
        public virtual ISet<ProspectiveStudent> ProspectiveStudents { get; set; }

        public virtual ISet<Reference> References { get; set; }
        public virtual ISet<Instructor> Instructors { get; set; }
        public virtual ISet<Student> Students { get; set; }
        public virtual SubCounty SubCounty { get; set; }
        public virtual SubCounty SubCounty1 { get; set; }

        //Helper Parameter
        public virtual string FullName
        {
            get
            {
                var fullName = "";
                if (!string.IsNullOrEmpty(Title))
                    fullName += string.Format("{0}. ", Title.Trim());

                if (!string.IsNullOrEmpty(Surname))
                    fullName += string.Format("{0} ", Surname.Trim());

                if (!string.IsNullOrEmpty(GivenName))
                    fullName += string.Format("{0} ", GivenName.Trim());

                if (!string.IsNullOrEmpty(OtherName))
                    fullName += string.Format("{0}", OtherName.Trim());

                

                return fullName;
//                return string.Format("{0} {1} {2} {3}",Title, GivenName, OtherName, Surname);
            }
        }

        public virtual string FullNameWithNoTitle
        {
            get
            {
                var fullName = "";

                if (!string.IsNullOrEmpty(Surname))
                    fullName += string.Format("{0} ", Surname.Trim());
                
                if (!string.IsNullOrEmpty(GivenName))
                    fullName += string.Format("{0} ", GivenName.Trim());

                if (!string.IsNullOrEmpty(OtherName))
                    fullName += string.Format("{0}", OtherName.Trim());

                

                return fullName;
                //                return string.Format("{0} {1} {2} {3}",Title, GivenName, OtherName, Surname);
            }
        }

        public virtual string FullNameWithNoTitleForGradList
        {
            //Capitalise Surname and Title Case others
            get
            {
                var fullName = "";

                //Surname is UPPERCASE
                if (!string.IsNullOrEmpty(Surname))
                    fullName += string.Format("{0} ", Surname.Trim().ToUpper());

                // Given Name has only first letter Capitalised
                if (!string.IsNullOrEmpty(GivenName))
                    fullName += string.Format("{0} ", char.ToUpper(GivenName.Trim()[0]) + GivenName.Trim().Substring(1).ToLower());

                // Other Name has only first letter Capitalised
                if (!string.IsNullOrEmpty(OtherName))
                    fullName += string.Format("{0}", char.ToUpper(OtherName.Trim()[0]) + OtherName.Trim().Substring(1).ToLower());



                return fullName;
                //                return string.Format("{0} {1} {2} {3}",Title, GivenName, OtherName, Surname);
            }
        }

        public virtual string ProfilePhotoUrl { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as Person);
        }

        public virtual bool Equals(Person obj)
        {
            if (obj == null) return false;

            if (Equals(AltEmailAddress, obj.AltEmailAddress) == false) return false;
            if (Equals(AltTelephoneContact, obj.AltTelephoneContact) == false) return false;
            if (Equals(CreatedOn, obj.CreatedOn) == false) return false;
            if (Equals(DateOfBirth, obj.DateOfBirth) == false) return false;
            if (Equals(EmailAddress, obj.EmailAddress) == false) return false;
            if (Equals(Gender, obj.Gender) == false) return false;
            if (Equals(GivenName, obj.GivenName) == false) return false;
            if (Equals(HomeLanguage, obj.HomeLanguage) == false) return false;
            if (Equals(Id, obj.Id) == false) return false;
            if (Equals(LastModified, obj.LastModified) == false) return false;
            if (Equals(MaritalStatus, obj.MaritalStatus) == false) return false;
            if (Equals(NextOfKinAddress, obj.NextOfKinAddress) == false) return false;
            if (Equals(NextOfKinContact, obj.NextOfKinContact) == false) return false;
            if (Equals(NextOfKinName, obj.NextOfKinName) == false) return false;
            if (Equals(NextOfKinRelationship, obj.NextOfKinRelationship) == false) return false;
            if (Equals(Occupation, obj.Occupation) == false) return false;
            if (Equals(Organisation, obj.Organisation) == false) return false;
            if (Equals(OtherName, obj.OtherName) == false) return false;
            if (Equals(PermentAddress, obj.PermentAddress) == false) return false;
            if (Equals(PersonOwnerType, obj.PersonOwnerType) == false) return false;
            if (Equals(PlaceOfBirth, obj.PlaceOfBirth) == false) return false;
            if (Equals(PostalAddress, obj.PostalAddress) == false) return false;
            if (Equals(PreferredLanguage, obj.PreferredLanguage) == false) return false;
            if (Equals(ProfilePhotoName, obj.ProfilePhotoName) == false) return false;
            if (Equals(Religion, obj.Religion) == false) return false;
            if (Equals(Surname, obj.Surname) == false) return false;
            if (Equals(TelephoneContact, obj.TelephoneContact) == false) return false;
            if (Equals(Title, obj.Title) == false) return false;
            if (Equals(WebsiteUrl, obj.WebsiteUrl) == false) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int result = 1;

            result = (result*397) ^ (AltEmailAddress != null ? AltEmailAddress.GetHashCode() : 0);
            result = (result*397) ^ (AltTelephoneContact != null ? AltTelephoneContact.GetHashCode() : 0);
            result = (result*397) ^ (CreatedOn != null ? CreatedOn.GetHashCode() : 0);
            result = (result*397) ^ (DateOfBirth != null ? DateOfBirth.GetHashCode() : 0);
            result = (result*397) ^ (EmailAddress != null ? EmailAddress.GetHashCode() : 0);
            result = (result*397) ^ (Gender != null ? Gender.GetHashCode() : 0);
            result = (result*397) ^ (GivenName != null ? GivenName.GetHashCode() : 0);
            result = (result*397) ^ (HomeLanguage != null ? HomeLanguage.GetHashCode() : 0);
            result = (result*397) ^ Id.GetHashCode();
            result = (result*397) ^ (LastModified != null ? LastModified.GetHashCode() : 0);
            result = (result*397) ^ (MaritalStatus != null ? MaritalStatus.GetHashCode() : 0);
            result = (result*397) ^ (NextOfKinAddress != null ? NextOfKinAddress.GetHashCode() : 0);
            result = (result*397) ^ (NextOfKinContact != null ? NextOfKinContact.GetHashCode() : 0);
            result = (result*397) ^ (NextOfKinName != null ? NextOfKinName.GetHashCode() : 0);
            result = (result*397) ^ (NextOfKinRelationship != null ? NextOfKinRelationship.GetHashCode() : 0);
            result = (result*397) ^ (Occupation != null ? Occupation.GetHashCode() : 0);
            result = (result*397) ^ (Organisation != null ? Organisation.GetHashCode() : 0);
            result = (result*397) ^ (OtherName != null ? OtherName.GetHashCode() : 0);
            result = (result*397) ^ (PermentAddress != null ? PermentAddress.GetHashCode() : 0);
            result = (result*397) ^ (PersonOwnerType != null ? PersonOwnerType.GetHashCode() : 0);
            result = (result*397) ^ (PlaceOfBirth != null ? PlaceOfBirth.GetHashCode() : 0);
            result = (result*397) ^ (PostalAddress != null ? PostalAddress.GetHashCode() : 0);
            result = (result*397) ^ (PreferredLanguage != null ? PreferredLanguage.GetHashCode() : 0);
            result = (result*397) ^ (ProfilePhotoName != null ? ProfilePhotoName.GetHashCode() : 0);
            result = (result*397) ^ (Religion != null ? Religion.GetHashCode() : 0);
            result = (result*397) ^ (Surname != null ? Surname.GetHashCode() : 0);
            result = (result*397) ^ (TelephoneContact != null ? TelephoneContact.GetHashCode() : 0);
            result = (result*397) ^ (Title != null ? Title.GetHashCode() : 0);
            result = (result*397) ^ (WebsiteUrl != null ? WebsiteUrl.GetHashCode() : 0);
            return result;
        }


        //Birthday Validation
        public class DateOfBirthAttribute : ValidationAttribute
        {
            public int MinAge { get; set; }
            public int MaxAge { get; set; }

            public override bool IsValid(object value)
            {
                if (value == null)
                    return true;

                var val = (DateTime) value;
                var test1 = val.AddYears(MinAge);
                var test2 = val.AddYears(MaxAge);

                if (val.AddYears(MinAge) > DateTime.Now || val.AddYears(MaxAge) > DateTime.Now)
                    return false;

                
                if (val.AddYears(MinAge) < DateTime.Now && val.AddYears(MaxAge) < DateTime.Now)
                    return true;

                return true;
            }
        }
    }
}