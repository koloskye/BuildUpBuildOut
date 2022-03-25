// BuildingManager
using ColossalFramework;
using ColossalFramework.Math;

public BuildingInfo GetRandomBuildingInfo(ref Randomizer r, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, int width, int length, BuildingInfo.ZoningMode zoningMode, int style)
{
	if (!m_buildingsRefreshed)
	{
		CODebugBase<LogChannel>.Error(LogChannel.Core, "Random buildings not refreshed yet!");
		return null;
	}
	int areaIndex = GetAreaIndex(service, subService, level, width, length, zoningMode);
	FastList<ushort> fastList;
	if (style > 0)
	{
		style--;
		DistrictStyle districtStyle = Singleton<DistrictManager>.instance.m_Styles[style];
		fastList = ((style > m_styleBuildings.Length || m_styleBuildings[style] == null || m_styleBuildings[style].Count <= 0 || !districtStyle.AffectsService(service, subService, level)) ? m_areaBuildings[areaIndex] : ((!m_styleBuildings[style].ContainsKey(areaIndex)) ? null : m_styleBuildings[style][areaIndex]));
	}
	else
	{
		fastList = m_areaBuildings[areaIndex];
	}
	if (fastList == null)
	{
		return null;
	}
	if (fastList.m_size == 0)
	{
		return null;
	}
	areaIndex = r.Int32((uint)fastList.m_size);
	return PrefabCollection<BuildingInfo>.GetPrefab(fastList.m_buffer[areaIndex]);
}
