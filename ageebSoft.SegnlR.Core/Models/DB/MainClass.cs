using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ageebSoft.SignlR.Core.Models.DB
{
    public class MainClass

    {

        /// <summary>
        /// دالة الإنشاء
        /// </summary>
        public MainClass()
        {

            Date1 = DateTime.Now;


        }

        #region GenaralSection


        /// <summary>
        /// تسلسلي الملف
        /// </summary>
        [Display(Name = "تسلسلي")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public Guid Id { get; set; }

        /// <summary>
        /// تاريخ الإدخال
        /// </summary>
        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        [Display(Name = "التاريخ")]
        public DateTime? Date1 { get; set; }

        /// <summary>
        /// ملاحظات لكل ملف مدخل - ملاحظات عامة
        /// </summary>
        [Display(Name = "ملاحظات")]
        public string Note { get; set; }


        #endregion


    }
}
