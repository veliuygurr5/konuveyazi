//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace profosyonelsite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class yazı
    {
        public int yazıID { get; set; }
        public string resim { get; set; }
        public string header { get; set; }
        public string title { get; set; }
        public int blogid { get; set; }
    
        public virtual blog blog { get; set; }
    }
}
