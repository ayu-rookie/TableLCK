//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LCK_players.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class TableLCKs1081720
    {
        public string 暱稱 { get; set; }
        public string 譯名 { get; set; }
        public string 韓文名 { get; set; }
        public string 定位 { get; set; }
        public string 先發 { get; set; }
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> 生日 { get; set; }
        public string 國籍 { get; set; }
        public string 隊伍 { get; set; }
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> 加入時間 { get; set; }
    }
}
