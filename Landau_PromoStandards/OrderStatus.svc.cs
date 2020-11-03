using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.ModelBinding;

namespace Landau_PromoStandards
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrderStatus" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OrderStatus.svc or OrderStatus.svc.cs at the Solution Explorer and start debugging.
    public class OrderStatus : OrderStatusService
    {
        public getOrderStatusDetailsResponse1 getOrderStatusDetails(getOrderStatusDetailsRequest1 request)
        {
            string errorMessage = string.Empty;
            var response = new getOrderStatusDetailsResponse1();
            if(Valid(request, ref errorMessage))
            {
                if(Authorized(request, ref errorMessage))
                {
                    List<GetOrderStatusDetailsResponseOrderStatus> listOrderStatus = new List<GetOrderStatusDetailsResponseOrderStatus>();


                    switch (request.GetOrderStatusDetailsRequest.queryType)
                    {
                        case 1:
                            //PO Search
                            using (var context = new LAN_AX2012_PRODEntities1())
                            {
                                var orders = context.LAN_SP_B2B_SUBMITTEDORDERBYPONUMBER(request.GetOrderStatusDetailsRequest.referenceNumber).ToList();

                                foreach( var order in orders)
                                {
                                    var details = new GetOrderStatusDetailsResponseOrderStatusOrderStatusDetail()
                                    {
                                        factoryOrderNumber = order.OrderNumber,
                                        expectedShipDate = order.RequestedShipDate,
                                        expectedShipDateSpecified = true,
                                        validTimestamp = DateTime.UtcNow
                                    };

                                    switch (order.SALESSTATUS)
                                    {
                                        case 0:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.None;
                                            details.statusName = "None";
                                            break;
                                        case 1:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.OpenOrder;
                                            details.statusName = "Open Order";
                                            break;
                                        case 2:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.Delivered;
                                            details.statusName = "Delivered";
                                            break;
                                        case 3:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.Invoiced;
                                            details.statusName = "Invoiced";
                                            break;
                                        case 4:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.Canceled;
                                            details.statusName = "Canceled";
                                            break;
                                        default:
                                            break;
                                    }

                                    GetOrderStatusDetailsResponseOrderStatus ordersdetails = new GetOrderStatusDetailsResponseOrderStatus()
                                    {
                                        purchaseOrderNumber = order.PONumber,
                                        OrderStatusDetailArray = new GetOrderStatusDetailsResponseOrderStatusOrderStatusDetail[] { details }
                                    };

                                    listOrderStatus.Add(ordersdetails);
                                }
                                
                                response.GetOrderStatusDetailsResponse = new GetOrderStatusDetailsResponse();
                                response.GetOrderStatusDetailsResponse.OrderStatusArray = listOrderStatus.ToArray();
                            }
                            break;
                        case 2:
                            //SO Search
                            using(var context = new LAN_AX2012_PRODEntities1())
                            {
                                var orders = context.LAN_SP_B2B_SUBMITTEDORDERBYORDERIDWITHSTATUS(request.GetOrderStatusDetailsRequest.referenceNumber).ToList();

                                foreach (var order in orders)
                                {
                                    var details = new GetOrderStatusDetailsResponseOrderStatusOrderStatusDetail()
                                    {
                                        factoryOrderNumber = order.OrderNumber,
                                        expectedShipDate = order.RequestedShipDate,
                                        expectedShipDateSpecified = true,
                                        validTimestamp = DateTime.UtcNow
                                    };

                                    switch (order.SALESSTATUS)
                                    {
                                        case 0:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.None;
                                            details.statusName = "None";
                                            break;
                                        case 1:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.OpenOrder;
                                            details.statusName = "Open Order";
                                            break;
                                        case 2:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.Delivered;
                                            details.statusName = "Delivered";
                                            break;
                                        case 3:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.Invoiced;
                                            details.statusName = "Invoiced";
                                            break;
                                        case 4:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.Canceled;
                                            details.statusName = "Canceled";
                                            break;
                                        default:
                                            break;
                                    }

                                    GetOrderStatusDetailsResponseOrderStatus ordersdetails = new GetOrderStatusDetailsResponseOrderStatus()
                                    {
                                        purchaseOrderNumber = order.PONumber,
                                        OrderStatusDetailArray = new GetOrderStatusDetailsResponseOrderStatusOrderStatusDetail[] { details }
                                    };

                                    listOrderStatus.Add(ordersdetails);
                                }

                                response.GetOrderStatusDetailsResponse = new GetOrderStatusDetailsResponse();
                                response.GetOrderStatusDetailsResponse.OrderStatusArray = listOrderStatus.ToArray();
                            }
                            break;
                        case 3:
                            //Last Update Search
                            using (var context = new LAN_AX2012_PRODEntities1())
                            {
                                var orders = context.LAN_SP_B2B_SUBMITTEDORDERSBYACCOUNTWITHSTATUS(request.GetOrderStatusDetailsRequest.id)
                                    .Where(x => x.MODIFIEDDATETIME.ToUniversalTime() > request.GetOrderStatusDetailsRequest.statusTimeStamp).ToList();

                                foreach (var order in orders)
                                {
                                    var details = new GetOrderStatusDetailsResponseOrderStatusOrderStatusDetail()
                                    {
                                        factoryOrderNumber = order.OrderNumber,
                                        expectedShipDate = order.RequestedShipDate,
                                        expectedShipDateSpecified = true,
                                        validTimestamp = DateTime.UtcNow
                                    };

                                    switch (order.SALESSTATUS)
                                    {
                                        case 0:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.None;
                                            details.statusName = "None";
                                            break;
                                        case 1:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.OpenOrder;
                                            details.statusName = "Open Order";
                                            break;
                                        case 2:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.Delivered;
                                            details.statusName = "Delivered";
                                            break;
                                        case 3:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.Invoiced;
                                            details.statusName = "Invoiced";
                                            break;
                                        case 4:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.Canceled;
                                            details.statusName = "Canceled";
                                            break;
                                        default:
                                            break;
                                    }

                                    GetOrderStatusDetailsResponseOrderStatus ordersdetails = new GetOrderStatusDetailsResponseOrderStatus()
                                    {
                                        purchaseOrderNumber = order.PONumber,
                                        OrderStatusDetailArray = new GetOrderStatusDetailsResponseOrderStatusOrderStatusDetail[] { details }
                                    };

                                    listOrderStatus.Add(ordersdetails);
                                }

                                response.GetOrderStatusDetailsResponse = new GetOrderStatusDetailsResponse();
                                response.GetOrderStatusDetailsResponse.OrderStatusArray = listOrderStatus.ToArray();
                            }
                            break;
                        case 4:
                            //All Open Search
                            using (var context = new LAN_AX2012_PRODEntities1())
                            {
                                var orders = context.LAN_SP_B2B_SUBMITTEDORDERSBYACCOUNTWITHSTATUS(request.GetOrderStatusDetailsRequest.id)
                                    .Where(x => x.SALESSTATUS == 1).ToList();

                                foreach (var order in orders)
                                {
                                    var details = new GetOrderStatusDetailsResponseOrderStatusOrderStatusDetail()
                                    {
                                        factoryOrderNumber = order.OrderNumber,
                                        expectedShipDate = order.RequestedShipDate,
                                        expectedShipDateSpecified = true,
                                        validTimestamp = DateTime.UtcNow
                                    };

                                    switch (order.SALESSTATUS)
                                    {
                                        case 0:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.None;
                                            details.statusName = "None";
                                            break;
                                        case 1:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.OpenOrder;
                                            details.statusName = "Open Order";
                                            break;
                                        case 2:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.Delivered;
                                            details.statusName = "Delivered";
                                            break;
                                        case 3:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.Invoiced;
                                            details.statusName = "Invoiced";
                                            break;
                                        case 4:
                                            details.statusID = GetOrderStatusDetailsResponseOrderStatusOrderStatusDetailStatusID.Canceled;
                                            details.statusName = "Canceled";
                                            break;
                                        default:
                                            break;
                                    }

                                    GetOrderStatusDetailsResponseOrderStatus ordersdetails = new GetOrderStatusDetailsResponseOrderStatus()
                                    {
                                        purchaseOrderNumber = order.PONumber,
                                        OrderStatusDetailArray = new GetOrderStatusDetailsResponseOrderStatusOrderStatusDetail[] { details }
                                    };

                                    listOrderStatus.Add(ordersdetails);
                                }

                                response.GetOrderStatusDetailsResponse = new GetOrderStatusDetailsResponse();
                                response.GetOrderStatusDetailsResponse.OrderStatusArray = listOrderStatus.ToArray();
                            }
                            break;
                        default:
                            errorMessage += "Unknown Query Type.";
                            response.GetOrderStatusDetailsResponse.errorMessage = errorMessage;
                            break;
                    }
                }
            }
            else
            {
                response.GetOrderStatusDetailsResponse = new GetOrderStatusDetailsResponse();
                response.GetOrderStatusDetailsResponse.errorMessage = errorMessage;
            }

            return response;
        }

        private bool Authorized(getOrderStatusDetailsRequest1 request, ref string errorMessage)
        {
            using (var context = new Landau_PromoStandardsEntities())
            {
                var authQuery = context.Authentications.Where(x => x.UserId == request.GetOrderStatusDetailsRequest.id).FirstOrDefault();

                if (!string.IsNullOrEmpty(authQuery.UserId))
                {
                    if (authQuery.Password.Equals(request.GetOrderStatusDetailsRequest.password))
                    {
                        return true;
                    }
                    else
                    {
                        errorMessage += "105: Authentication Credentials failed";
                        return false;
                    }
                }
                else
                {
                    errorMessage += "100: ID (customerID) not found";
                    return false;
                }
            }
        }

        private bool Valid(getOrderStatusDetailsRequest1 request, ref string errorMessage)
        {
            bool isValidRequest = true;

            if(request.GetOrderStatusDetailsRequest is null)
            {
                isValidRequest = false;
                errorMessage += "999: General Error – Contact the System Service Provider";
            }
            else
            {
                if(request.GetOrderStatusDetailsRequest.wsVersion != "1.0.0")
                {
                    isValidRequest = false;
                    errorMessage += "115: wsVersion not found";
                }
                if (string.IsNullOrEmpty(request.GetOrderStatusDetailsRequest.id))
                {
                    isValidRequest = false;
                    errorMessage += "110: Authentication Credentials required";
                }
                if (string.IsNullOrEmpty(request.GetOrderStatusDetailsRequest.password))
                {
                    isValidRequest = false;
                    errorMessage += "110: Authentication Credentials required";
                }
            }

            return isValidRequest;
        }

        public getOrderStatusTypesResponse1 getOrderStatusTypes(getOrderStatusTypesRequest1 request)
        {
            string errorMessage = string.Empty;
            var response = new getOrderStatusTypesResponse1();

            if(Valid(request, ref errorMessage))
            {
                if(Authorized(request, ref errorMessage))
                {
                    response.GetOrderStatusTypesResponse = new GetOrderStatusTypesResponse();
                    
                    List<GetOrderStatusTypesResponseStatus> statuses = new List<GetOrderStatusTypesResponseStatus>();
                    statuses.Add(new GetOrderStatusTypesResponseStatus
                    {
                        id = 0,
                        name = "None"
                    });

                    statuses.Add(new GetOrderStatusTypesResponseStatus
                    {
                        id = 1,
                        name = "Open Order"
                    });

                    statuses.Add(new GetOrderStatusTypesResponseStatus
                    {
                        id = 2,
                        name = "Delivered"
                    });

                    statuses.Add(new GetOrderStatusTypesResponseStatus
                    {
                        id = 3,
                        name = "Invoiced"
                    });

                    statuses.Add(new GetOrderStatusTypesResponseStatus
                    {
                        id = 4,
                        name = "Canceled"
                    });

                    response.GetOrderStatusTypesResponse.StatusArray = statuses.ToArray();
                }
                else
                {
                    response.GetOrderStatusTypesResponse = new GetOrderStatusTypesResponse();
                    response.GetOrderStatusTypesResponse.errorMessage = errorMessage;
                }
            }
            else
            {
                response.GetOrderStatusTypesResponse = new GetOrderStatusTypesResponse();
                response.GetOrderStatusTypesResponse.errorMessage = errorMessage;
            }

            return response;
        }

        private bool Authorized(getOrderStatusTypesRequest1 request, ref string errorMessage)
        {
            using (var context = new Landau_PromoStandardsEntities())
            {
                var authQuery = context.Authentications.Where(x => x.UserId == request.GetOrderStatusTypesRequest.id).FirstOrDefault();

                if (!string.IsNullOrEmpty(authQuery.UserId))
                {
                    if (authQuery.Password.Equals(request.GetOrderStatusTypesRequest.password))
                    {
                        return true;
                    }
                    else
                    {
                        errorMessage += "105: Authentication Credentials failed";
                        return false;
                    }
                }
                else
                {
                    errorMessage += "100: ID (customerID) not found";
                    return false;
                }
            }
        }

        private bool Valid(getOrderStatusTypesRequest1 request, ref string errorMessage)
        {
            bool isValidRequest = true;

            if(request.GetOrderStatusTypesRequest is null)
            {
                isValidRequest = false;
                errorMessage += "999: General Error – Contact the System Service Provider";
            }
            else
            {
                if (request.GetOrderStatusTypesRequest.wsVersion != "1.0.0")
                {
                    isValidRequest = false;
                    errorMessage += "115: wsVersion not found";
                }
                if (string.IsNullOrEmpty(request.GetOrderStatusTypesRequest.id))
                {
                    isValidRequest = false;
                    errorMessage += "110: Authentication Credentials required";
                }
                if (string.IsNullOrEmpty(request.GetOrderStatusTypesRequest.password))
                {
                    isValidRequest = false;
                    errorMessage += "110: Authentication Credentials required";
                }
            }

            return isValidRequest;
        }
    }
}
