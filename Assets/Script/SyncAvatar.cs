using Unity.VisualScripting;
using UnityEngine;
using Fusion;
using ReadyPlayerMe.Samples.AvatarCreatorWizard;
using ReadyPlayerMe.Core;
public class SyncAvatar : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject AvatarPrefab;
    void OnEnable(){
        Debug.Log("has INput Autority " + this.HasInputAuthority);
        GameManager.OnAvatarLoaded += SetAvatarPosition;
        //AvatarPrefab.SetActive(false);
        Debug.Log("Avatar Prefab Set to false"); 
    }

   void SetAvatarPosition(GameObject Character){
        Character.SetActive(false);
        Debug.Log("Transfer Avatar Position Set");
        if(this.HasInputAuthority){
            AvatarMeshHelper.TransferMesh(Character, this.gameObject );
            Debug.Log("Transfer Avatar Position Set on State Authority");
            AvatarPrefab.SetActive(true);
            Debug.Log("Avatar Prefab Set to true");
        }
    }
}
