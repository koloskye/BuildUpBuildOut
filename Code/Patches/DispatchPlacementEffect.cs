// DispatchPlacementEffect Class from base game, subclass to BuildingTool

// BuildingTool
using ColossalFramework;
using ColossalFramework.Math;
using UnityEngine;

public static void DispatchPlacementEffect(BuildingInfo info, ushort buildingID, Vector3 pos, float angle, int width, int length, bool bulldozing, bool collapsed)
{
	EffectInfo effectInfo = ((!bulldozing) ? Singleton<BuildingManager>.instance.m_properties.m_placementEffect : Singleton<BuildingManager>.instance.m_properties.m_bulldozeEffect);
	if ((object)effectInfo == null)
	{
		return;
	}
	if (collapsed)
	{
		BuildingInfoBase collapsedInfo = info.m_collapsedInfo;
		if (collapsedInfo != null)
		{
			int num = new Randomizer(buildingID).Int32(4u);
			if (((1 << num) & info.m_collapsedRotations) == 0)
			{
				num = (num + 1) & 3;
			}
			Vector3 min = info.m_generatedInfo.m_min;
			Vector3 max = info.m_generatedInfo.m_max;
			float num2 = (float)width * 4f;
			float num3 = (float)length * 4f;
			float num4 = Building.CalculateLocalMeshOffset(info, length);
			min = Vector3.Max(min - new Vector3(4f, 0f, 4f + num4), new Vector3(0f - num2, 0f, 0f - num3));
			max = Vector3.Min(max + new Vector3(4f, 0f, 4f - num4), new Vector3(num2, 0f, num3));
			Vector3 vector = (min + max) * 0.5f;
			Vector3 vector2 = max - min;
			float x = ((((uint)num & (true ? 1u : 0u)) != 0) ? vector2.z : vector2.x) / Mathf.Max(1f, collapsedInfo.m_generatedInfo.m_size.x);
			float z = ((((uint)num & (true ? 1u : 0u)) != 0) ? vector2.x : vector2.z) / Mathf.Max(1f, collapsedInfo.m_generatedInfo.m_size.z);
			Quaternion q = Quaternion.AngleAxis((float)num * 90f, Vector3.down);
			Matrix4x4 matrix4x = Matrix4x4.TRS(new Vector3(vector.x, 0f, vector.z + num4), q, new Vector3(x, 1f, z));
			Vector3 pos2 = Building.CalculateMeshPosition(info, pos, angle, length);
			Quaternion q2 = Building.CalculateMeshRotation(angle);
			Matrix4x4 matrix4x2 = Matrix4x4.TRS(pos2, q2, Vector3.one);
			EffectInfo.SpawnArea spawnArea = new EffectInfo.SpawnArea(matrix4x2 * matrix4x, collapsedInfo.m_lodMeshData);
			Singleton<EffectManager>.instance.DispatchEffect(effectInfo, default(InstanceID), spawnArea, Vector3.zero, 0f, 1f, Singleton<AudioManager>.instance.DefaultGroup, avoidMultipleAudio: true);
		}
	}
	else
	{
		Vector3 pos3 = Building.CalculateMeshPosition(info, pos, angle, length);
		Quaternion q3 = Building.CalculateMeshRotation(angle);
		Matrix4x4 matrix = Matrix4x4.TRS(pos3, q3, Vector3.one);
		EffectInfo.SpawnArea spawnArea2 = new EffectInfo.SpawnArea(matrix, info.m_lodMeshData);
		Singleton<EffectManager>.instance.DispatchEffect(effectInfo, spawnArea2, Vector3.zero, 0f, 1f, Singleton<AudioManager>.instance.DefaultGroup, 0u, avoidMultipleAudio: true);
	}
}
