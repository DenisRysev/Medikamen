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
    
    public partial class Поставка
    {
        public int КодПоставки { get; set; }
        public Nullable<int> КодДокументаПоставки { get; set; }
        public Nullable<int> Количество { get; set; }
        public Nullable<int> КодПозицииСклада { get; set; }
    
        public virtual ДокументПоставкии ДокументПоставкии { get; set; }
        public virtual Склад Склад { get; set; }
    }
}
