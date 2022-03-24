// StartUpgrading Class from base game, subclass to PrivateBuildingAI
public void StartUpgrading(ushort buildingID, ref Building buildingData)
{
	BuildingManager instance = Singleton<BuildingManager>.instance;
	EffectInfo levelupEffect = instance.m_properties.m_levelupEffect;
	if ((object)levelupEffect != null)
	{
		InstanceID instance2 = default(InstanceID);
		instance2.Building = buildingID;
		buildingData.CalculateMeshPosition(out var meshPosition, out var meshRotation);
		Matrix4x4 matrix = Matrix4x4.TRS(meshPosition, meshRotation, Vector3.one);
		EffectInfo.SpawnArea spawnArea = new EffectInfo.SpawnArea(matrix, m_info.m_lodMeshData);
		Singleton<EffectManager>.instance.DispatchEffect(levelupEffect, instance2, spawnArea, Vector3.zero, 0f, 1f, instance.m_audioGroup);
	}
	Vector3 position = buildingData.m_position;
	position.y += m_info.m_size.y;
	Singleton<NotificationManager>.instance.AddEvent(NotificationEvent.Type.LevelUp, position, 1f);
	if (((uint)buildingData.m_flags & 0x80000000u) != 0)
	{
		Building.Flags flags = buildingData.m_flags;
		flags &= ~Building.Flags.LevelUpEducation;
		flags = (buildingData.m_flags = flags & ~Building.Flags.LevelUpLandValue);
		buildingData.m_level++;
		BuildingUpgraded(buildingID, ref buildingData);
		ManualActivation(buildingID, ref buildingData);
	}
	else
	{
		buildingData.m_frame0.m_constructState = 0;
		buildingData.m_frame1.m_constructState = 0;
		buildingData.m_frame2.m_constructState = 0;
		buildingData.m_frame3.m_constructState = 0;
		Building.Flags flags2 = buildingData.m_flags;
		flags2 |= Building.Flags.Upgrading;
		flags2 &= ~Building.Flags.Completed;
		flags2 &= ~Building.Flags.LevelUpEducation;
		flags2 = (buildingData.m_flags = flags2 & ~Building.Flags.LevelUpLandValue);
		instance.UpdateBuildingRenderer(buildingID, updateGroup: true);
		Singleton<SimulationManager>.instance.m_currentBuildIndex++;
	}
}
