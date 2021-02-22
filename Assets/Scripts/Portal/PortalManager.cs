using System;
using Zenject;

public class PortalManager : ObjectManager<PortalController, PortalDto, PortalCreateParam> {
    
    public PortalManager(TubeMapService tubeMapService, GameSettingsInstaller.GameSetting setting,
        GameSettingsInstaller.PrefabSettings prefabSettings,
        PortalController.Factory factory, DiContainer container) : base(tubeMapService,
        setting, prefabSettings, factory, container) { }

    public override PortalCreateParam Convert(PortalDto dto) {
        return new PortalCreateParam(dto.GetPrefab(prefabs), dto.position);
    }

    
    public void CreateDebug() {
        if (!_setting.isDebug) {
            throw new Exception("Unsupport");
        }

        Create(new PortalDto(Constants.CREATE_POSITION));
    }

    public void Clear() {
        foreach (PortalController controller in Objects) {
            controller.Clear();
        }
    }
}