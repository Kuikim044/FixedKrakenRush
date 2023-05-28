using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.UIManager.Containers;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{

   public void OnPurachaseComplete(Product product)
   {
      if (product.definition.id == "25 Star")
      {
         Debug.Log("25 Star");
         
         var popup = UIPopup.Get("25Star");
         if (popup == null) return;
         popup.Show();
      }
   }

   public void OmPuarchaseFailed(Product product, PurchaseFailureReason failureReason)
   {
      Debug.Log(product.definition.id + "Failed" + failureReason);
   }
}
