using UnityEngine;
using Wolf3D.ReadyPlayerMe.AvatarSDK;
using Photon.Pun;

public class ReadyPlayerMeLoadAvatar: MonoBehaviour
{
	[Tooltip("URL of the RPM Avatar.")]
	public string avatarUrl;
	public GameObject avatarGameObject;
	PhotonView pv;

	// Code that runs on entering the state.
	private void Start() {
		pv = gameObject.GetPhotonView();
		LoadAvatar();
	}

	[PunRPC]
	private void LoadAvatar()
	{
		AvatarLoader avatarLoader = new AvatarLoader();
		avatarLoader.LoadAvatar((avatarUrl), AvatarImportedCallback, AvatarLoadedCallback);
	}

	private void AvatarImportedCallback(GameObject avatar)
	{
		// called after GLB file is downloaded and imported
		Debug.Log("Avatar Imported!");
	}

	private void AvatarLoadedCallback(GameObject avatar, AvatarMetaData metaData)
	{
		// called after avatar is prepared with components and anim controller 
		avatarGameObject = avatar;
		try
		{
			avatarGameObject.AddComponent<AvatarInputConverter>();
			avatarGameObject.transform.SetParent(transform);
			GameObject armature = GetChildWithName(avatarGameObject,"Armature");
			GameObject remoteHips = GetChildWithName(armature,"Hips");
			GameObject remoteSpine = GetChildWithName(remoteHips,"Spine");
			AvatarInputConverter RemoteInputConverter = avatarGameObject.GetComponent<AvatarInputConverter>();
			RemoteInputConverter.MainAvatarTransform = avatarGameObject.transform;
			RemoteInputConverter.AvatarHead = GetChildWithName(remoteSpine,"Neck").transform.GetChild(0).transform;
			RemoteInputConverter.AvatarBody = GetChildWithName(remoteSpine,"Neck").transform;
			RemoteInputConverter.AvatarHand_Left = GetChildWithName(remoteSpine,"LeftHand").transform;
			RemoteInputConverter.AvatarHand_Right = GetChildWithName(remoteSpine,"RightHand").transform;
			RemoteInputConverter.XRHead = GetChildWithName(gameObject,"Head").transform;
			RemoteInputConverter.XRHand_Left = GetChildWithName(gameObject,"LeftController").transform;
			RemoteInputConverter.XRHand_Right = GetChildWithName(gameObject,"RightController").transform;
			RemoteInputConverter.headPositionOffset.y = -0.64f; 
			GetChildWithName(remoteSpine,"RightHand").transform.localScale = new Vector3(0,0,0);
			GetChildWithName(remoteSpine,"LeftHand").transform.localScale = new Vector3(0,0,0);
			if (pv.IsMine)
			{
				avatarGameObject.SetActive(false);
			}
		}
		catch (System.Exception)
		{
			avatarGameObject.SetActive(false);
		}
		Debug.Log("Avatar Loaded!");
	}

	GameObject GetChildWithName(GameObject obj, string name) {
		Transform trans = obj.transform;
		Transform childTrans = trans.Find(name);
		if (childTrans != null) {
			return childTrans.gameObject;
		} else {
			return null;
		}
	}
}

