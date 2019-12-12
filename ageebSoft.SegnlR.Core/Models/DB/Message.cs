
 using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ageebSoft.SignlR.Core.Models.DB
{
   
    public class Message : MainClass
    {
       
        public string GroupName { set; get; }
        /// <summary>
        /// نص رسالة الطلب
        /// </summary>
        [Display(Name = "نص الرسالة")]
        [Required(ErrorMessage = "ادخل نص الرسالة")]
        public string Msg { set; get; }

  
         

        /// <summary>
        /// تسلسلي المرسل
        /// </summary>
        [Display(Name = "تسلسلي المرسل")]
        public Guid SenderId { set; get; }

        [ForeignKey(nameof(SenderId))]
        public MyUser Sender { set; get; }
        /// <summary>
        /// وقت الإرسال
        /// </summary>
        [Display(Name = "وقت الإرسال")]
        public DateTime Time { set; get; }

        
         

    }

}
