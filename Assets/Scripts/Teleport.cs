// Teleport.cs by DaveA

// Drop this on an OVRPlayerController, or not. If not, drag the OVRPlayerController onto this in the Inspector. Or not.
// Place 'target' transforms in scene where you want to appear. Put them into 'targets' array.
// Hitting the 'activate key' will jump you there, with target rotation too.
using UnityEngine;
using System.Collections.Generic;

public class Teleport : MonoBehaviour
{
   public KeyCode activateKey = KeyCode.T;
   public Transform[] targets;
   public Transform player;
   public int curTarget = 0;
   public OVRPlayerController ovrPlayer;

   void Start ()
   {
      if (ovrPlayer == null)
         ovrPlayer = GetComponent<OVRPlayerController>();
      if (ovrPlayer == null)
         ovrPlayer = Object.FindObjectOfType<OVRPlayerController>();
   }

   void Update ()
   {
      if (Input.GetKeyDown(activateKey))
      {
         player.position = targets[curTarget].position;
         ovrPlayer.SetYRotation (targets[curTarget].eulerAngles.y -90f);
         curTarget = ++curTarget % targets.Length;
      }
   }
}
