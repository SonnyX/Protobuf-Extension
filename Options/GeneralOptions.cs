using Microsoft.VisualStudio.Settings;
using System.ComponentModel;

namespace ALittle
{
    internal class GeneralOptions : BaseOptionModel<GeneralOptions>
    {
        [Category("Customization")]
        [DisplayName("Project Team")]
        [Description("Choose your project group ，Plug-ins can open different functions according to different project groups")]
        [DefaultValue(ProjectTeamTypes.None)]
        [TypeConverter(typeof(EnumConverter))]
        public ProjectTeamTypes ProjectTeam { get; set; }

        protected override void LoadProperty(SettingsStore store)
        {
            var value = store.GetString(CollectionName, nameof(ProjectTeam), "None");
            if (value == "LW") ProjectTeam = ProjectTeamTypes.LW;
            else ProjectTeam = ProjectTeamTypes.None;
        }
        protected override void SaveProperty(WritableSettingsStore store)
        {
            if (ProjectTeam == ProjectTeamTypes.LW)
                store.SetString(CollectionName, nameof(ProjectTeam), "LW");
            else
                store.SetString(CollectionName, nameof(ProjectTeam), "None");
        }
    }

    public enum ProjectTeamTypes
    {
        None,
        LW
    }
}
