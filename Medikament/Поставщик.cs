//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Medikament
{
    using System;
    using System.Collections.Generic;
    
    public partial class Поставщик
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Поставщик()
        {
            this.ДокументПоставкии = new HashSet<ДокументПоставкии>();
        }
    
        public int КодПоставщика { get; set; }
        public string НаименованиеПоставщика { get; set; }
        public string Адрес { get; set; }
        public string Телефон { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ДокументПоставкии> ДокументПоставкии { get; set; }
    }
}
