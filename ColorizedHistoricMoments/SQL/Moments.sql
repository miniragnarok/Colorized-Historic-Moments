
drop table if exists temp.TempMoments;

create temp table temp.TempMoments(ExistingName text);

insert into temp.TempMoments (ExistingName)
values
	('Moment_ShipwreckExcavated.dds')
	,('Moment_ClearBarbMedieval6Tiles.dds')
	,('Moment_MaxReligionBeliefs.dds')
	,('Moment_DistrictBuiltAerodrome.dds')
	,('Moment_DistrictBuiltEncampment.dds')
	,('Moment_DistrictBuiltEntertainment.dds')
	,('Moment_DistrictBuiltWaterpark.dds')
	,('Moment_DistrictBuiltCampus.dds')
	,('Moment_DistrictBuilt_Commercial.dds')
	,('Moment_DistrictBuiltHarbor.dds')
	,('Moment_DistrictBuiltHolySite.dds')
	,('Moment_DistrictBuiltIndustrial.dds')
	,('Moment_DistrictBuiltTheatre.dds')
	,('Moment_DistrictBuiltNeighborhood.dds')
	,('Moment_3MoreCitiesThanNextPlayer.dds')
	,('Moment_NewContinentCity.dds')
	,('Moment_ConvertEnemyHolyCityDuringWar.dds')
	,('Moment_ConvertCity.dds')
	,('Moment_FirstCityToReach25Pop.dds')
	,('Moment_FirstCityToReach20Pop.dds')
	,('Moment_FirstCityToReach15Pop.dds')
	,('Moment_FirstCityToReach10Pop.dds')
	,('Moment_CityTransferCulturalFromFreeCity.dds')
	,('Moment_EliminatedCiv.dds')
	,('Moment_JoinEmergency.dds')
	,('Moment_DiscoverContinent.dds')
	,('Moment_FirstArmada.dds')
	,('Moment_FirstArmy.dds')
	,('Moment_FirstCorps.dds')
	,('Moment_FirstFleet.dds')
	,('Moment_EarnGreatPersonWithFaith.dds')
	,('Moment_EarnGreatPersonWithGold.dds')
	,('Moment_CreatedSeasideResort.dds')
	,('Moment_BeganInquisition.dds')
	,('Moment_NationalPark.dds')
	,('Moment_FoundedPantheon.dds')
	,('Moment_FirstSuzerain.dds')
	,('Moment_CancelledSuzerianLeviedMilitary.dds')
	,('Moment_CancelledSuzerianWar.dds')
	,('Moment_LevyMilitaryNearEnemy.dds')
	,('Moment_MetCivPlayer.dds')
	,('Moment_MetAllCivs.dds')
	,('Moment_FirstMarsComponent.dds')
	,('Moment_MoonLanding.dds')
	,('Moment_LaunchedSatellite.dds')
	,('PM_FoundedReligion.dds')
	,('Moment_SpyPromoted.dds')
	,('Moment_TradePostNewCiv.dds')
	,('Moment_Tradepost_AllCities.dds')
	,('Moment_StrategicResourceUnit.dds')
	,('Moment_4StarUnit.dds')
	,('Moment_KillEnemyUnit_AdmiralNearby.dds')
	,('Moment_KillEnemy_GreatGeneralNearby.dds')
	,('Moment_KillEnemyUnit_HigherFormation.dds')
	,('Moment_KillEnemyUnit_TwoMorePromos.dds')
	,('Moment_PromoteGovernor_AllGovernors.dds')
	,('Moment_Circumnavigation.dds')
	,('MomentXP2_City_Built_Near_FloodableRiver.dds')
	,('MomentXP2_City_Built_Near_Volcano.dds')
	,('MomentXP2_Resource_FossilFuel.dds')
	,('MomentXP2_ClimateChangeAlert.dds')
	,('MomentXP2_BuildingTheCanal.dds')
	,('MomentXP2_BuildingMountainTunnel.dds')
	,('MomentXP2_ImproveNaturalDisasterYieldTile.dds')
	,('MomentXP2_TrainingRenewableEnergyImprovement.dds')
	,('MomentXP2_Project_Founded_Exoplanet.dds')
	,('MomentXP2_BuildingRailroadConnecting2Cities.dds')
	,('MomentXP2_TourismBomb.dds')
	,('MomentSTK_HeroClaimed.dds')
	,('MomentSTK_HeroRecalled.dds')
	,('MomentSTK_HeroLost.dds')
	,('MomentSTK_FirstCorporation.dds')
	,('MomentSTK_FirstIndustry.dds')
	,('MomentSTK_FirstMonopoly.dds');

update Moments
set BackgroundTexture = 'CHM_' || BackgroundTexture
where BackgroundTexture in (
	select ExistingName from temp.TempMoments
);

drop table if exists temp.TempMoments;


-- Small Moments
drop table if exists temp.TempSmallMoments;

create temp table temp.TempSmallMoments(ExistingName text);

insert into temp.TempSmallMoments (ExistingName)
values
	('MomentSmall_City')
	,('MomentSmall_Combat')
	,('MomentSmall_Culture')
	,('MomentSmall_Diplomacy')
	,('MomentSmall_Espionage')
	,('MomentSmall_Exploration')
	,('MomentSmall_GreatPerson')
	,('MomentSmall_Military')
	,('MomentSmall_Religion')
	,('MomentSmall_Science')
	,('MomentSmall_Trade')
	,('MomentSmall_Wonder')
	,('MomentSmall_Wonder')
	,('MomentSmall_Heroes');

update Moments
set IconTexture = 'CHM_' || IconTexture || '.dds'
where IconTexture in (
	select ExistingName from temp.TempSmallMoments
);

drop table if exists temp.TempSmallMoments;