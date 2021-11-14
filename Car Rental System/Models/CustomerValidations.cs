using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental_System.Models
{
    [MetadataType(typeof(CustomerMetaData))]
    public partial class Customer
    {
        

        public class CustomerMetaData
        {
            [DisplayName("Customer Name")]
            public string custname { get; set; }
            [DisplayName("Address")]
            public string address { get; set; }
            [DisplayName("Mobile")]
            public string mobile { get; set; }
        }

    }
}