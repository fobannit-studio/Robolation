using System.Collections.Generic;
namespace Simulation.Common{
    // class Container
    // {
    //     private Dictionary<string, int> _materials = new Dictionary<string, int>();
    //     private int _freePlace;

    //     Container(int freePlace)
    //     {
    //         _freePlace = freePlace;
    //     } 
    //     public float FreePlace
    //     {
    //         get
    //         {
    //             return _freePlace;
    //         }
    //         set
    //         {
    //             if(value > 0){
    //                 _freePlace = value;
    //             }
    //             else
    //             {
    //                 throw new ContainerIsFullException();
    //             }
    //         }
    //     }
 
    //     public void putMaterial(Material material, int amount)
    //     {
    //         FreePlace -= material.Size * amount;
    //         if(!_materials.TryAdd(material.type, amount)){
    //             materials[material.type] += amount;
    //         }
    //     }
        
    //     // Will be improved.
    //     public Material getMaterial(string type, int requestedAmount, out int returnedAmount)
    //     {
    //         int amountInContainer;
    //         if(this._materials.TryGetValue(type, out amountInContainer))
    //         {
    //             if(amountInContainer > requestedAmount)
    //             {
    //                 this._materials[type] -= requestedAmount;
    //                 returnedAmount = requestedAmount;
    //             }
    //             else
    //             {
    //                 returnedAmount = this._materials[type];  
    //                 this._materials[type] = 0;
    //             }
    //             // Will be replaced
    //             Vector3 dim = new Vector3(1,1,1);
    //             Material returnedMaterial = new Material(type, dim,requestedAmount);
    //             FreePlace += dim * returnedAmount;
    //             return returnedMaterial;
    //         }
    //         return null;
    //     }
    // }
}