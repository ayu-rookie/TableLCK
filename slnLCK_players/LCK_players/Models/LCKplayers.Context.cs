﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Common;
    using System.Data.Entity.Core.EntityClient;
    using System.Configuration;

    public partial class LCKEntities : DbContext
    {
        public LCKEntities()
            : base("name=LCKEntities")
        {
            var originalConnectionString = ConfigurationManager.ConnectionStrings["LCKEntities"].ConnectionString;
            var entityBuilder = new EntityConnectionStringBuilder(originalConnectionString);
            var factory = DbProviderFactories.GetFactory(entityBuilder.Provider);
            var providerBuilder = factory.CreateConnectionStringBuilder();

            providerBuilder.ConnectionString = entityBuilder.ProviderConnectionString;

            providerBuilder.Add("Password", "yzuim1092netdbB");  //加入SQL密碼資料

            entityBuilder.ProviderConnectionString = providerBuilder.ToString();

            this.Database.Connection.ConnectionString = entityBuilder.ProviderConnectionString;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TableLCKs1081720> TableLCKs1081720 { get; set; }
    }
}
