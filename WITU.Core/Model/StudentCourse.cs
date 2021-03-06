using System;
using System.ComponentModel;

namespace WITU.Core.Model
{
    public enum StudentCoursePerformanceTrack
    {
        [Description("MIS")]
        NoResults = 0,
        [Description("PASS")]
        PassAttempt1 = 1,
        [Description("RT(1)")]
        PassAttempt2 = 2,
        [Description("RT(2)")]
        PassAttempt3 = 3,
        [Description("CTR(2)")]
        FailAttempt1 = 4,
        [Description("CTR(1)")]
        FailAttempt2 = 5,
        [Description("FAIL")]
        FailAttempt3 = 6,
        ResultsRevoked = 8,
        [Description("MIS CA/EXAM")]
        MissingResults = 9,
        [Description("AC")]
        Audited = 10,
        [Description("*")]
        Exempted = 11,
        [Description("CT")]
        CreditTransfer = 12,
        Abs = 13,
        Registered = 23,
        [Description("*")]
        ExemptPending = 14,
        ExemptRejected = 15,
        CreditTransferPending = 20,
        CreditTransferRejected = 21,
        //CreditTransferAccepted = 22,
        //RegisterationPending = 19,


        //ExemptPendingHOD = 14,
        //ExemptRejectedHOD = 15,
        //ExemptPendingDean = 16, //14
        //ExemptRejectedDean = 17, //15
        //ExemptPendingAR = 18, //15
        //ExemptRejectedAR = 19, //16
        //CreditTransferPendingAR = 20,
        

        UnknownPerformanceTrack = 25
    }

    public enum ResultStatus
    {
        NoMarks = 0,

        MarksEntered = 1,

        PublishedMarks = 2,

        ChangedPendingApproval = 3,

        ChangeApproved = 4,

        DeletedPendingApproval = 5,

        DeletedApproved = 6
    }

    public enum ConfirmationStatus
    {
        Registration = 0,

        Exemption = 1,

        Audit = 2,

        CreditTransfer = 3
    }

    [Serializable]
    public class StudentCourse
    {
        public virtual decimal? ContinousAssessment { get; set; }
        public virtual decimal? ExamMark { get; set; }
        public virtual decimal? FinalMark { get; set; }
        public virtual bool HasAssessed { get; set; }
        public virtual int Id { get; set; }
        public virtual DateTime LastModified { get; set; }
        public virtual StudentCoursePerformanceTrack PerformanceTrack { get; set; }
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual CourseType? CourseType { get; set; }
        public virtual DateTime? ResultUploadDate { get; set; }
        public virtual StaffCourse StaffCourse { get; set; }

        public virtual Course Course { get; set; }

        public virtual CohortRegistration SemesterRegistration { get; set; }

        public virtual Cohort Semester { get; set; }

        public virtual Student Student { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as StudentCourse);
        }

//        public virtual bool Equals(StudentCourse obj)
//        {
//            if (obj == null) return false;
//
//            if (Equals(ContinousAssessment, obj.ContinousAssessment) == false) return false;
//            if (Equals(ExamMark, obj.ExamMark) == false) return false;
//            if (Equals(FinalMark, obj.FinalMark) == false) return false;
//            if (Equals(HasAssessed, obj.HasAssessed) == false) return false;
//            if (Equals(Id, obj.Id) == false) return false;
//            if (Equals(LastModified, obj.LastModified) == false) return false;
//            if (Equals(PerformanceTrack, obj.PerformanceTrack) == false) return false;
//            if (Equals(ResultStatus, obj.ResultStatus) == false) return false;
//            if (Equals(ResultUploadDate, obj.ResultUploadDate) == false) return false;
//            return true;
//        }
//
//        public override int GetHashCode()
//        {
//            int result = 1;
//
//            result = (result*397) ^ (ContinousAssessment != null ? ContinousAssessment.GetHashCode() : 0);
//            result = (result*397) ^ (ExamMark != null ? ExamMark.GetHashCode() : 0);
//            result = (result*397) ^ (FinalMark != null ? FinalMark.GetHashCode() : 0);
//            result = (result*397) ^ HasAssessed.GetHashCode();
//            result = (result*397) ^ Id.GetHashCode();
//            result = (result*397) ^ LastModified.GetHashCode();
//            result = (result*397) ^ PerformanceTrack.GetHashCode();
//            result = (result*397) ^ ResultStatus.GetHashCode();
//            result = (result*397) ^ ResultUploadDate.GetHashCode();
//            return result;
//        }
    }
}