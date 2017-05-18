using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Utils;
using WITU.Validations;

namespace WITU.Models
{
    [FluentValidation.Attributes.Validator(typeof(EditPersonValidator))]
    public class EditPerson
    {
        /// <summary>
        /// Can either be the Staff Id or the Student Id
        /// </summary>
        public int ParentClassId { get; set; }

        public int PersonOwnerType { get; set; }

        [Display(Name = "Alt. Email Address")]
        public   string AltEmailAddress { get; set; }

        [Display(Name = "Alt. Telephone Contact")]
        public   string AltTelephoneContact { get; set; }


        //Birthday Validation
        [Person.DateOfBirthAttribute(MinAge = 0, MaxAge = 15)]
        [Display(Name = "Date of Birth")]
        public  DateTime? DateOfBirth { get; set; }

        [Display(Name = "Email Address")]
        public  string EmailAddress { get; set; }

        public  Gender Gender { get; set; }

        

        public  int Id { get; set; }

        [Display(Name = "Marital Status")]
        public  string MaritalStatus { get; set; }

        [Display(Name = "NOK Address")]
        public  string NextOfKinAddress { get; set; }

        [Display(Name = "NOK Contact")]
        public  string NextOfKinContact { get; set; }

        [Display(Name = "NOK Name")]
        public  string NextOfKinName { get; set; }

        [Display(Name = "NOK Relationship")]
        public  string NextOfKinRelationship { get; set; }

        [Display(Name = "Postal Address")]
        public  string PostalAddress { get; set; }

        
        public  string ProfilePhotoName { get; set; }

        [Display(Name = "Profile Photo")]
        public HttpPostedFileBase ProfilePhotoFile { get; set; }

        public string CurrentProfilePhotoUrl { get; set; }

        public  string Religion { get; set; }

        [Display(Name = "Telephone")]
        public  string TelephoneContact { get; set; }

        public  string Title { get; set; }

        [Display(Name = "Website")]
        public  string WebsiteUrl { get; set; }


        [Display(Name = "Nationality")]
        public  int NationalityId { get; set; }

        [Display(Name = "Hall of Residence / Attachment")]
        public int ResidentHallId { get; set; }

        public List<SelectListItem> CountryOptions { get; set; }

        public List<SelectListItem> ResidentHallOptions { get; set; }


        public EditPerson()
        {
            if(CountryOptions == null)
                CountryOptions = new List<SelectListItem>();

            if(ResidentHallOptions == null)
                ResidentHallOptions = new List<SelectListItem>();
        }

        public EditPerson(Instructor staff)
        {
            Id = staff.Person.Id;
            ParentClassId = staff.Id;
            Title = staff.Person.Title;
            NationalityId = staff.Person.Country2 != null ? staff.Person.Country2.Id : 0;
            WebsiteUrl = staff.Person.WebsiteUrl;
            PersonOwnerType = int.Parse(ApplicationConstants.StaffUserType);
            TelephoneContact = staff.Person.TelephoneContact;
            AltTelephoneContact = staff.Person.AltTelephoneContact;
            EmailAddress = staff.Person.EmailAddress;
            AltEmailAddress = staff.Person.AltEmailAddress;
            Religion = staff.Person.Religion;
            ProfilePhotoName = staff.Person.ProfilePhotoName??staff.User.ProfilePhotoName;
            PostalAddress = staff.Person.PostalAddress;
            NextOfKinAddress = staff.Person.NextOfKinAddress;
            NextOfKinContact = staff.Person.NextOfKinContact;
            NextOfKinName = staff.Person.NextOfKinName;
            NextOfKinRelationship = staff.Person.NextOfKinRelationship;
            MaritalStatus = staff.Person.MaritalStatus;
            Gender = staff.Person.Gender??Gender.Unknown;
            DateOfBirth = staff.Person.DateOfBirth;

            if (CountryOptions == null)
                CountryOptions = new List<SelectListItem>();

            if (ResidentHallOptions == null)
                ResidentHallOptions = new List<SelectListItem>();

        }

        public EditPerson(Student student)
        {
            Id = student.Person.Id;
            ParentClassId = student.Id;
            Title = student.Person.Title;
            NationalityId = student.Person.Country2 != null ? student.Person.Country2.Id : 0;
            WebsiteUrl = student.Person.WebsiteUrl;
            PersonOwnerType = int.Parse(ApplicationConstants.StudentUserType);
            TelephoneContact = student.Person.TelephoneContact;
            AltTelephoneContact = student.Person.AltTelephoneContact;
            EmailAddress = student.Person.EmailAddress;
            AltEmailAddress = student.Person.AltEmailAddress;
            Religion = student.Person.Religion;
            ProfilePhotoName = student.Person.ProfilePhotoName??student.User.ProfilePhotoName;
            PostalAddress = student.Person.PostalAddress;
            NextOfKinAddress = student.Person.NextOfKinAddress;
            NextOfKinContact = student.Person.NextOfKinContact;
            NextOfKinName = student.Person.NextOfKinName;
            NextOfKinRelationship = student.Person.NextOfKinRelationship;
            MaritalStatus = student.Person.MaritalStatus;
            Gender = student.Person.Gender ?? Gender.Unknown;
            DateOfBirth = student.Person.DateOfBirth;
            //ResidentHallId = student.ResidentHall != null ? student.ResidentHall.Id : 0;

            if (CountryOptions == null)
                CountryOptions = new List<SelectListItem>();

            if (ResidentHallOptions == null)
                ResidentHallOptions = new List<SelectListItem>();
        }
    }
}