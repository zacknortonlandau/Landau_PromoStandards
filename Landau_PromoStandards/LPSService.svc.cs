
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Landau_PromoStandards
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    
    public class LPSService : InventoryService
    {

        public getFilterValuesResponse getFilterValues(getFilterValuesRequest1 request)
        {
            string errorMessage = string.Empty;
            var response = new getFilterValuesResponse();
            if (Valid(request, ref errorMessage))
            {
                if (Authenticated(request, ref errorMessage))
                {
                    List<string> sizes = new List<string>();
                    List<string> colors = new List<string>();
                    List<string> selections = new List<string>();

                    using (var context = new LAN_AX2012_PRODEntities())
                    {
                        var products = context.LAN_VW_GETAVAILABLETOSHIP
                                .Where(x => x.ITEMID == request.GetFilterValuesRequest.productID);

                        if(products.Count() == 0)
                        {
                            errorMessage += "200: ProductID not found";

                            response = new getFilterValuesResponse();
                            response.GetFilterValuesReply = new GetFilterValuesReply();
                            response.GetFilterValuesReply.errorMessage = errorMessage;

                            return response;
                        }

                        colors = products.GroupBy(x => x.INVENTCOLORID).Select(y => y.Key).ToList();
                        sizes = products.GroupBy(x => x.INVENTSIZEID).Select(y => y.Key).ToList();
                    }

                        

                    response.GetFilterValuesReply = new GetFilterValuesReply();
                    response.GetFilterValuesReply.productID = request.GetFilterValuesRequest.productID;
                    response.GetFilterValuesReply.FilterSizeArray = sizes.ToArray();
                    response.GetFilterValuesReply.FilterColorArray = colors.ToArray();
                    response.GetFilterValuesReply.FilterSelectionArray = selections.ToArray();
                }
                else
                {
                    response = new getFilterValuesResponse();
                    response.GetFilterValuesReply = new GetFilterValuesReply();
                    response.GetFilterValuesReply.errorMessage = errorMessage;
                }
            }
            else
            {
                response = new getFilterValuesResponse();
                response.GetFilterValuesReply = new GetFilterValuesReply();
                response.GetFilterValuesReply.errorMessage = errorMessage;
            }
            


            return response;
        }

        private bool Authenticated(getFilterValuesRequest1 request, ref string errorMessage)
        {
            using(var context = new Landau_PromoStandardsEntities())
            {
                var authQuery = context.Authentications.Where(x => x.UserId == request.GetFilterValuesRequest.id).FirstOrDefault();

                if (!string.IsNullOrEmpty(authQuery.UserId))
                {
                    if (authQuery.Password.Equals(request.GetFilterValuesRequest.password))
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

        private bool Valid(getFilterValuesRequest1 request, ref string errorMessage)
        {
            bool isValidRequest = true;

            if(request.GetFilterValuesRequest is null)
            {
                isValidRequest = false;
                errorMessage += "999: General Error – Contact the System Service Provider";
            }
            else
            {
                if(request.GetFilterValuesRequest.wsVersion != "1.2.1")
                {
                    isValidRequest = false;
                    errorMessage += "115: wsVersion not found";
                }
                if (string.IsNullOrEmpty(request.GetFilterValuesRequest.id))
                {
                    isValidRequest = false;
                    errorMessage += "110: Authentication Credentials required";
                }
                if (string.IsNullOrEmpty(request.GetFilterValuesRequest.password))
                {
                    isValidRequest = false;
                    errorMessage += "110: Authentication Credentials required";
                }
                if (string.IsNullOrEmpty(request.GetFilterValuesRequest.productID))
                {
                    isValidRequest = false;
                    errorMessage += "200: ProductID not found";
                }
            }

            return isValidRequest;
        }


        //public Task<getFilterValuesResponse> getFilterValuesAsync(getFilterValuesRequest request)
        //{
        //    return Task.FromResult(getFilterValues(request));
        //}


        public getInventoryLevelsResponse getInventoryLevels(getInventoryLevelsRequest request)
        {
            getInventoryLevelsResponse response = new getInventoryLevelsResponse();
            string errorMessage = string.Empty;

            if (Valid(request, ref errorMessage))
            {
                if (Authenticated(request, ref errorMessage))
                {
                    string productid = request.Request.productID;
                    string[] filterColors = request.Request.FilterColorArray;
                    string[] filterSize = request.Request.FilterSizeArray;

                    List<ReplyProductVariationInventory> productVariations = new List<ReplyProductVariationInventory>();
                    List<ReplyProductCompanionInventory> productAlternatives = new List<ReplyProductCompanionInventory>();

                    //get available to ship
                    //filter based on filter values
                    using (var context = new LAN_AX2012_PRODEntities())
                    {
                        
                        //get color codes
                        var colorQuery = context.LAN_VW_COLORDATA
                            .Where(x => x.ITEMID == productid && filterColors.Contains(x.NAME))
                            .GroupBy(y => y.InventColorId)
                            .ToList();

                        List<string> colorCodes = colorQuery.Select(x => x.Key).ToList();

                        var availableQuery = context.LAN_VW_GETAVAILABLETOSHIP
                            .Where(x => x.ITEMID == productid);

                        if(availableQuery.Count() == 0)
                        {
                            errorMessage += "200: ProductID not found";

                            response.Reply = new Reply
                            {
                                errorMessage = errorMessage
                            };

                            return response;
                        }

                        if(filterColors.Length > 0)
                        {
                            //check colors
                            availableQuery = availableQuery.Where(x => filterColors.Contains(x.INVENTCOLORID));
                            if(availableQuery.Count() == 0)
                            {
                                errorMessage += "205: ProductColor not found";

                                response.Reply = new Reply
                                {
                                    errorMessage = errorMessage
                                };

                                return response;
                            }
                        }

                        if(filterSize.Length > 0)
                        {
                            //check sizes
                            availableQuery = availableQuery.Where(x => filterSize.Contains(x.INVENTSIZEID));
                            if (availableQuery.Count() == 0)
                            {
                                errorMessage += "210: ProductSize not found";

                                response.Reply = new Reply
                                {
                                    errorMessage = errorMessage
                                };

                                return response;
                            }
                        }
                        var sizeLength = filterSize.Length;
                        var colorLength = filterColors.Length;

                        var skuQuery = context.LAN_VW_SKUMASTERDATA
                            .Where(x => x.ITEMID == productid
                                && (sizeLength == 0 || filterSize.Contains(x.INVENTSIZEID))
                                && (colorLength == 0 || filterColors.Contains(x.INVENTCOLORID)))
                            .ToList();
                            

                        foreach (var prod in availableQuery)
                        {
                            var sku = skuQuery.Where(x => x.ITEMID == prod.ITEMID
                                                    && x.INVENTSIZEID == prod.INVENTSIZEID
                                                    && x.INVENTCOLORID == prod.INVENTCOLORID)
                                                .First();

                            productVariations.Add(new ReplyProductVariationInventory
                            {
                                partID = productid,
                                partDescription = sku.ItemName,
                                partBrand = sku.DATAAREAID,
                                priceVariance = string.Empty,
                                quantityAvailable = prod.GetAvailToShipQty.ToString(),
                                attributeColor = prod.INVENTCOLORID,
                                attributeSize = prod.INVENTSIZEID,
                                attributeSelection = string.Empty,
                                validTimestamp = DateTime.Now,
                                entryType = "exact"
                                
                            });

                            switch (sku.SUNTAFITEMSTATUS)
                            {
                                case "DIS":
                                    productVariations.Last().customProductMessage = "Discontinued: Supply limited";
                                    break;
                                case "HIS":
                                    productVariations.Last().customProductMessage = "No longer available";
                                    break;
                                case "ACT":
                                    productVariations.Last().customProductMessage = "Active";
                                    break;
                                case "WSL":
                                    productVariations.Last().customProductMessage = "While Supply Lasts";
                                    break;
                                default:
                                    productVariations.Last().customProductMessage = string.Empty;
                                    break;

                            }

                            
                        }
                    }

                    

                    response.Reply = new Reply
                    {
                        productID = productid,
                        ProductVariationInventoryArray = productVariations.ToArray()
                    };
                }
                else
                {
                    response.Reply = new Reply
                    {
                        errorMessage = errorMessage
                    };
                }
            }
            else
            {
                response.Reply = new Reply
                {
                    errorMessage = errorMessage
                };
            }
            
            return response;
        }

        private bool Authenticated(getInventoryLevelsRequest request, ref string errorMessage)
        {
            using (var context = new Landau_PromoStandardsEntities())
            {
                var authQuery = context.Authentications.Where(x => x.UserId == request.Request.id).FirstOrDefault();

                if (!string.IsNullOrEmpty(authQuery.UserId))
                {
                    if (authQuery.Password.Equals(request.Request.password))
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

        private bool Valid(getInventoryLevelsRequest request, ref string errorMessage)
        {
            bool isValidRequest = true;
            if(request.Request is null)
            {
                isValidRequest = false;
                errorMessage += "999: General Error – Contact the System Service Provider";
            }
            else
            {
                if (request.Request.wsVersion != "1.2.1")
                {
                    isValidRequest = false;
                    errorMessage += "115: wsVersion not found";
                }
                if (string.IsNullOrEmpty(request.Request.id))
                {
                    isValidRequest = false;
                    errorMessage += "110: Authentication Credentials required";
                }
                if (string.IsNullOrEmpty(request.Request.password))
                {
                    isValidRequest = false;
                    errorMessage += "110: Authentication Credentials required";
                }
                if (string.IsNullOrEmpty(request.Request.productID))
                {
                    isValidRequest = false;
                    errorMessage += "200: ProductID not found";
                }
            }

            return isValidRequest;
        }


        //public Task<getInventoryLevelsResponse> getInventoryLevelsAsync(getInventoryLevelsRequest request)
        //{
        //    return Task.FromResult(getInventoryLevels(request));
        //}
    }
}
