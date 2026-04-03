using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MasterDetails_MobileFixCenter.Models.ViewModels
{
    public class CustomerVM
    {
        public int CustomerId { get; set; }
        [Required, Display(Name = "Customer Name"), StringLength(50)]
        public string CustomerName { get; set; }
        [Required, Display(Name = "Phone"), StringLength(15)]
        public string Phone { get; set; }
        [Required, Display(Name = "Entry Date"), Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime EntryDate { get; set; }
        [Required, Display(Name = "Device Name"), StringLength(30)]
        public string DeviceName { get; set; }
        [Required, Display(Name = "Device Problem"), StringLength(30)]

        public string Problem { get; set; }
        public string DevicePicture { get; set; }
        public HttpPostedFileBase DevicePictureFile { get; set; }
        [Display(Name = "IsRegular")]
        public bool IsRegular { get; set; }
        public string Address { get; set; }

        public List<int> ServiceList { get; set; } = new List<int>();
    }
}