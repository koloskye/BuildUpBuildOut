// BuildingManager
private static int GetAreaIndex(ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, int width, int length, BuildingInfo.ZoningMode zoningMode)
{
	int privateSubServiceIndex = ItemClass.GetPrivateSubServiceIndex(subService);
	int num = ((privateSubServiceIndex == -1) ? ItemClass.GetPrivateServiceIndex(service) : (8 + privateSubServiceIndex));
	num = (int)(num * 5 + level);
	if (zoningMode == BuildingInfo.ZoningMode.CornerRight)
	{
		num = num * 4 + length - 1;
		num = num * 4 + width - 1;
		return num * 2 + 1;
	}
	num = num * 4 + width - 1;
	num = num * 4 + length - 1;
	return (int)(num * 2 + zoningMode);
}
