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
    
    public partial class blog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public blog()
        {
            this.yazı = new HashSet<yazı>();
        }
    
        public int BlogID { get; set; }
        public string img { get; set; }
        public string title { get; set; }
        public int userid { get; set; }
    
        public virtual admin admin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<yazı> yazı { get; set; }
    }
}
