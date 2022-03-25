// PrivateBuildingAI
using ColossalFramework;
using ColossalFramework.Math;

public override BuildingInfo GetUpgradeInfo(ushort buildingID, ref Building data)
{
	if (data.m_level == 4)
	{
		return null;
	}
	Randomizer r = new Randomizer(buildingID);
	for (int i = 0; i <= data.m_level; i++)
	{
		r.Int32(1000u);
	}
	ItemClass.Level level = (ItemClass.Level)(data.m_level + 1);
	DistrictManager instance = Singleton<DistrictManager>.instance;
	byte district = instance.GetDistrict(data.m_position);
	ushort style = instance.m_districts.m_buffer[district].m_Style;
	return Singleton<BuildingManager>.instance.GetRandomBuildingInfo(ref r, m_info.m_class.m_service, m_info.m_class.m_subService, level, data.Width, data.Length, m_info.m_zoningMode, style);
}
