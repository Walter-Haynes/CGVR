using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

using System;
using UnityEngine.XR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;

namespace SimpleVR
{
	public class PoseProvider : BasePoseProvider
	{
		/// <summary>
		/// Useful to attach Grabbables
		/// Something can track it with UnityEngine.SpatialTracking.TrackedPoseDriver to follow position and rotation.
		/// More accurate than UnityEngine.Animations Constraints classes and maybe more than kinematic parenting.
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public override PoseDataFlags GetPoseFromProvider(out Pose output)
		{
			output = new Pose(AttachPosition, AttachRotation);
			//output = new Pose(transform.TransformPoint(attachPositionOffset), transform.rotation * attachRotationOffset);
			return PoseDataFlags.Position;
		}

		[SerializeField] private Vector3 attachPositionOffset = Vector3.zero;
		[SerializeField] private Quaternion attachRotationOffset = Quaternion.identity;

		public Vector3 AttachPositionOffset 
		{ 
			get => attachPositionOffset;
			set => attachPositionOffset = value;
		}
		
		public Quaternion AttachRotationOffset 
		{
			get => attachRotationOffset;
			set => attachRotationOffset = value;
		}
		
		public Vector3 AttachEulerAnglesOffset 
		{
			get => AttachRotationOffset.eulerAngles;
			set => AttachRotationOffset = Quaternion.Euler(value);
		}

		public Vector3 AttachPosition => transform.TransformPoint(Quaternion.Inverse(AttachRotationOffset) * -AttachPositionOffset);
		public Quaternion AttachRotation => transform.rotation * Quaternion.Inverse(AttachRotationOffset);
	}
}
