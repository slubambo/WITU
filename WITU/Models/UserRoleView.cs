using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WITU.Core.Model;

namespace WITU.Models
{
    public enum ServiceCategory
    {
        Reports = 1,
        Settings = 2,
        [Description("My Profile")]
        MyProfile = 3,
        Messenger = 4,
        [Description("Information Desk")]
        InformationDesk = 5,
        Notifications = 6,
        [Description("Terms Of Service")]
        TermsOfService = 7,
        [Description("Privacy Policy")]
        PrivacyPolicy = 8,
        [Description("About Arms")]
        AboutArms = 9,
        Registration = 10,
        [Description("Course Allocation")]
        CourseAllocation = 11,
        Results = 12,
        Teach = 13,
        Examinations = 14,
        Finance = 15,
        [Description("User Management")]
        UserManagement = 16,
        Graduation = 17
    }

    public enum ServiceDetail
    {
        //Results = 1,
        [Description("Student Documents")] StudentDocuments = 2,
        Progression = 3,
        //Graduation = 4,
        [Description("Missing Results")] MissingResults = 5,
        Registration = 6,
        [Description("Certified Students")] CertifiedStudents = 7,
        Staff = 8,
        [Description("Special Reports")] SpecialReports = 9,
        Semester = 10,
        [Description("Grading Scheme")] GradingScheme = 11,
        [Description("Classification Of Awards")] ClassificationOfAwards = 12,
        University = 13,
        Campus = 14,
        Faculty = 15,
        Department = 16,
        Programme = 17,
        Course = 18,
        [Description("Profile Information")] ProfileInformation = 19,
        Password = 20,
        [Description("Profile Photo")] ProfilePhoto = 21,
        Messenger = 22,
        [Description("News And Events")] NewsAndEvents = 23,
        Notifications = 24,
        [Description("Terms Of Service")] TermsOfService = 25,
        [Description("Privacy Policy")] PrivacyPolicy = 26,
        [Description("About Arms")] AboutArms = 27,
        Approval = 28,
        Exemptions = 29,
        [Description("Credit Transfers")] CreditTransfers = 30,
        [Description("Course Allocation")] CourseAllocation = 31,
        [Description("Edit Results")] EditResults = 32,
        [Description("Publish Results")] PublishResults = 33,
        [Description("Academic Board")]
        AcademicBoard = 34,
        [Description("Changes")]
        Changes = 35,
        Import = 36,
        [Description("CGPA Calculation")]
        CGPACalculation = 37,
        Purge = 38,
        Teach = 39,
        [Description("User Managment")] UserManagment = 40,
        [Description("User Managment")] Contacts = 41,
        Audit = 42,
        [Description("Examination Attendance")]
        ExamAttendance = 43,
        [Description("Financial Reports")]
        FinancialReports = 44,
        [Description("Student Finance")]
        StudentFinance = 45,
        [Description("Financial Configurations")]
        FinancialConfigurations = 46,
        [Description("Financial Returns")]
        FinancialReturns = 47,
        [Description("General Information")]
        GeneralInformation = 48,
        [Description("Information Desk")]
        InformationDesk = 49,
        Calender = 50,
        [Description("Student Results")]
        StudentResults = 51,
        [Description("Reset Student Password")]
        ResetStudentPassword = 52,
        [Description("Create Staff Accounts And Roles")]
        CreateStaffAccountsAndRoles = 53,
        [Description("Rules and Regulations")]
        RulesAndRegulations = 54,
        [Description("Student Accounts")]
        StudentAccounts = 55,
        [Description("Manage Student Specializations")]
        ManageStudentSpecializations = 56,
        Testimonial = 57,
        Transcript = 58,
        [Description("Qualified Lists")]
        QualifiedLists = 59,
        [Description("Graduation Ceremonies")]
        GraduationCeremonies = 60,
        [Description("Graduation Lists")]
        GraduationLists = 61,
        [Description("Unpublish Results")]
        UnpublishResults = 62
        
    }

    public class UserInfo
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public Person Person { get; set; }

        public virtual List<InstructorPosition> Positions { get; set; }

        public virtual List<StaffCourse> StaffCourses { get; set; }
    }

    public class UserInfoViewModel
    { 
        public User User { get; set; }

        public Person Person { get; set; }
      
        public IEnumerable<Service> Services { get; set; }

        public IEnumerable<UserInfo> UserInfos { get; set; }
       
        public List<Responsibility> Responsibilities { get; set; }
      
        public List<UserRole> UserRoles { get; set; }

        public List<InstructorPosition> StaffPositions { get; set; }

        public  InstructorPosition  StaffPosition { get; set; }
    }

    public class Responsibility
    {
        public int ServiceId { get; set; }
        public int Type { get; set; }

        public bool RequiresAccessScope { get; set; }
        public virtual int Id { get; set; }

        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }

        public Service Service { get; set; }

        public bool CanDelete { get; set; }
    }

    public class StaffUser
    {
        public int Id { get; set; }

        public virtual int UserId { get; set; }

        public string Username { get; set; }

        public string Surname { get; set; }

        public string GivenName { get; set; }

        public string OtherName { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1} {2}", GivenName, OtherName, Surname); }
        }
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
            DateTime test1 = val.AddYears(MinAge);
            DateTime test2 = val.AddYears(MaxAge);

            if (val.AddYears(MinAge) > DateTime.Now || val.AddYears(MaxAge) > DateTime.Now)
                return false;


            if (val.AddYears(MinAge) < DateTime.Now && val.AddYears(MaxAge) < DateTime.Now)
                return true;

            return true;
        }
    }
}