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
    
    public partial class lan_sp_B2B_SubmittedOrdersByAccountByDateRange_Result
    {
        public string Account { get; set; }
        public string OrderNumber { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string PONumber { get; set; }
        public string CustomerPO { get; set; }
        public System.DateTime RequestedShipDate { get; set; }
        public int MinShipQty { get; set; }
        public string AddedBy { get; set; }
        public string AccountName { get; set; }
        public string ShipVia { get; set; }
        public string ShipToName { get; set; }
        public string ShipToAddress { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToState { get; set; }
        public string ShipToZip { get; set; }
        public string CreatedBy { get; set; }
        public string OrderComment { get; set; }
        public Nullable<int> OrderQty { get; set; }
        public Nullable<int> ShipQty { get; set; }
        public Nullable<int> CancelQty { get; set; }
        public Nullable<int> OpenQty { get; set; }
    }
}
