//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Landau_PromoStandards
{
    using System;
    
    public partial class lan_sp_B2B_Reports_InvoiceSummary_Result
    {
        public string Account { get; set; }
        public string SalesId { get; set; }
        public Nullable<System.DateTime> SalesDate { get; set; }
        public string PurchaseOrder { get; set; }
        public string Invoice { get; set; }
        public decimal InvoiceAmount { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public Nullable<System.DateTime> InvoiceDueDate { get; set; }
        public string Terms { get; set; }
        public decimal InvoiceAmountDecimal { get; set; }
        public decimal AmountCurrent { get; set; }
        public decimal AmountThirtyDays { get; set; }
        public decimal AmountSixtyDays { get; set; }
        public decimal AmountSixtyPlusDays { get; set; }
    }
}
