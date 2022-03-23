// ToDo - Modify code
// BuildingManager
using System;
using ColossalFramework;
using UnityEngine;

public bool UpgradeBuilding(ushort buildingID, bool useConstructionCost)
{
	if (buildingID == 0 || (m_buildings.m_buffer[buildingID].m_flags & Building.Flags.Created) == 0)
	{
		return false;
	}
	BuildingInfo info = m_buildings.m_buffer[buildingID].Info;
	if ((object)info == null)
	{
		return false;
	}
	BuildingInfo upgradeInfo = info.m_buildingAI.GetUpgradeInfo(buildingID, ref m_buildings.m_buffer[buildingID]);
	if ((object)upgradeInfo == null || (object)upgradeInfo == info)
	{
		return false;
	}
	UpdateBuildingInfo(buildingID, upgradeInfo);
	upgradeInfo.m_buildingAI.BuildingUpgraded(buildingID, ref m_buildings.m_buffer[buildingID]);
	if (useConstructionCost)
	{
		int constructionCost = upgradeInfo.m_buildingAI.GetConstructionCost();
		Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Construction, constructionCost, upgradeInfo.m_class);
	}
	int num = 0;
	while (buildingID != 0)
	{
		BuildingInfo info2 = m_buildings.m_buffer[buildingID].Info;
		if ((object)info2 != null)
		{
			Vector3 position = m_buildings.m_buffer[buildingID].m_position;
			float angle = m_buildings.m_buffer[buildingID].m_angle;
			BuildingTool.DispatchPlacementEffect(info2, 0, position, angle, info2.m_cellWidth, info2.m_cellLength, bulldozing: false, collapsed: false);
		}
		buildingID = m_buildings.m_buffer[buildingID].m_subBuilding;
		if (++num > 49152)
		{
			CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
			break;
		}
	}
	return true;
}
